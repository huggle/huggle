//This is a source code or part of Huggle project
//
//This file contains code for configuration requests

/// <DOCUMENTATION>
/// There is no documentation for this
/// </DOCUMENTATION>

//Copyright (C) 2011-2012 Huggle team
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

using System;
using System.Collections.Generic;
using System.Text;

namespace huggle3.Requests
{
    public class RequestConfigGlobal : RequestCore.Request
    {
        public override void Process()
        {
            Core.History("Process()");

            ApiResult apiResult = ApiRequest("action=query&prop=revisions&rvlimit=1&rvprop=content&titles=" + Config.GlobalConfigLocation, "", "meta");

            if (apiResult == null || apiResult.ResultInError)
            {
                Login.phase = Login.LoginState.Error;
                Fail(Languages.Get("loadglobalconfig-fail"));
            }

            foreach (KeyValuePair<string, string> x in Core_IO.ProcessConfigFile(apiResult.Text))
            {
                Core_IO.SetGlobalConfigOption(x.Key, x.Value);
            }

            Login.phase = Login.LoginState.LoadedGlobal;

            Complete();
        }
    }

    public class RequestConfigLocal : RequestCore.Request
    {
        public override void Fail(string description = "", string reason = "")
        {
            Login.Error = description;
            Login.phase = Login.LoginState.Error;
            base.Fail(description, reason);
        }

        public override void Process()
        {
            Core.History("Process()");
            try
            {
                ApiResult apiResult;
                string query = "action=query&meta=userinfo&uiprop=rights|editcount&list=logevents|watchlistraw&letype=newusers&letitle=" + System.Web.HttpUtility.UrlEncode(new User("").Me.UserPage) + "&prop=revisions&rvprop=content&titles=" + System.Web.HttpUtility.UrlEncode(Config.ProjectConfigLocation) + "|" + System.Web.HttpUtility.UrlEncode(Config.UserConfigLocation);

                apiResult = ApiRequest(query, "", Config.Project);

                if (apiResult.ResultInError)
                {
                    Login.phase = Login.LoginState.Error;
                    Fail(Languages.Get("login-error-config"), apiResult.Error_Data);
                    return;
                }

                if (Config.ProjectConfigLocation == null)
                {
                    Login.phase = Login.LoginState.Error;
                    Fail(Languages.Get("login-project-config-is-wrong"), "Invalid path");
                    return;
                }

                Config.Minor.Clear();

                foreach (string minor in Config.EditTypes)
                {
                    Config.Minor.Add(minor, false);
                }

                string projectconfig_file = System.Web.HttpUtility.HtmlDecode(Core.FindString(Core.FindString(Core.FindString(apiResult.Text, "<page", "ns=\"" + Space.SpaceID(Config.ProjectConfigLocation).ToString() + "\"", "</page>"), "<rev "), ">", "</rev>"));
                string userconfig_file = System.Web.HttpUtility.HtmlDecode(Core.FindString(Core.FindString(Core.FindString(apiResult.Text, "<page", "ns=\"" + Space.SpaceID(Config.UserConfigLocation).ToString() + "\"", "</page>"), "<rev "), ">", "</rev>"));
                try
                {
                    foreach (KeyValuePair<string, string> value in Core_IO.ProcessConfigFile(projectconfig_file))
                    {
                        Core_IO.SetSharedConfigKey(value.Key, value.Value);
                        Core_IO.SetProjectConfigValue(value.Key, value.Value);
                    }
                }
                catch (Exception ignore)
                {
                    // ignore
                    if (Config.devs)
                    {
                        throw ignore;
                    }
                }


                if (userconfig_file != null)
                {
                    foreach (KeyValuePair<string, string> current_item in Core_IO.ProcessConfigFile(userconfig_file))
                    {
                        Core_IO.SetSharedConfigKey(current_item.Key, current_item.Value);
                        Core_IO.SetUserConfigOption(current_item.Key, current_item.Value);
                    }
                }
                // if doesn't contain messages then use global
                if (Config.TemplateMessages.Count == 0)
                {
                    Config.TemplateMessages = Config.TemplateMessagesGlobal;
                }

                if (string.IsNullOrEmpty(Config.IrcChannel))
                {
                    Config.UseIrc = false;
                }

                if (Config.WarnSummary2 == null)
                {
                    Config.WarnSummary2 = Config.WarnSummary;
                }

                if (Config.WarnSummary3 == null)
                {
                    Config.WarnSummary3 = Config.WarnSummary;
                }

                if (Config.WarnSummary4 == null)
                {
                    Config.WarnSummary4 = Config.WarnSummary;
                }

                string userinfo = Core.FindString(apiResult.Text, "<userinfo", "</userinfo>");

                if (userinfo == null)
                {
                    Login.phase = Login.LoginState.Error;
                    return;
                }

                if (userinfo.Contains("<rights>"))
                {
                    if (userinfo.Contains("anon=\"\""))
                    {
                        Fail("Error when loggin in", "Can't check the user rights");
                        return;
                    }

                    Config.Rights = new List<string>(Core.FindString(userinfo, "<rights>", "</rights>").Replace("</r>", "").Split(new string[] { "<r>" }, StringSplitOptions.RemoveEmptyEntries));
                    if (Config.Rights.Contains("block") == false && Config.RequireAdmin == true)
                    {
                        Fail(Languages.Get("login-error-admin"));
                        return;
                    }
                    if (Config.Rights.Contains("autoconfirmed") == false && Config.RequireAutoconfirmed == true)
                    {
                        Fail(Languages.Get("login-error-confirmed"));
                        return;
                    }
                    if (Config.Rights.Contains("rollback") == false && Config.RequireRollback == true)
                    {
                        Fail(Languages.Get("login-error-rollback"));
                        return;
                    }
                    if (Config.Rights.Contains("writeapi") == false)
                    {
                        Fail("error");
                        return;
                    }
                }

                Login.phase = Login.LoginState.LoadedLocal;

                Complete();

            }
            catch (Exception A)
            {
                Core.ExceptionHandler(A);
            }
        }
    }
}
