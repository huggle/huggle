//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Petrb

//Copyright (C) 2011 Huggle team
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
    public class request_config_global : request_core.Request
    {
        public override void Process()
        {
            
        }
    }

    public class request_config_local : request_core.Request
    {
        public override void Process()
        {
            Core.History("Process()");
            try
            {
                ApiResult result;

                result = ApiRequest("action=query&meta=userinfo&uiprop=rights|editcount&list=logevents|watchlistraw&letype=newusers&letitle=" + System.Web.HttpUtility.UrlEncode(new user("").Me.UserPage) + "prop=revisions&rvprop=content&titles=" + Config.ProjectConfigLocation + "|" + Config.UserConfigLocation, "" , Config.Project);

                if (result.ResultInError)
                { 
                    Fail(Languages.Get("login-error-config"), result.Error_Data);
                    return;
                }

                if (Config.ProjectConfigLocation == null )
                {
                    Fail(Languages.Get("login-project-config-is-wrong"), "Invalid path");
                    return;
                }

                Config.Minor.Clear();

                foreach (string minor in Config.EditTypes)
                {
                    Config.Minor.Add(minor, false);
                }

                string projectconfig_file = System.Web.HttpUtility.HtmlDecode(Core.FindString(Core.FindString(Core.FindString(result.ResultText, "<page", "ns=\"" + Core.GetPage(Config.ProjectConfigLocation)._Space.Number + "\"" , "</page>"), "<rev "), ">", "</rev>"));
                

            }
            catch (Exception A)
            {
                Core.ExceptionHandler(A);
            }
        }


    }
   
}
