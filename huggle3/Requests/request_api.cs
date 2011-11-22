//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Addshore

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

namespace huggle3
{
    public class ApiResult
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string Error_Code;
        /// <summary>
        /// Result text
        /// </summary>
        public string Result_Text;
        /// <summary>
        /// Error text
        /// </summary>
        public string Error_Data;

        public bool ResultInError
        {
            get {
                if (Error_Code == "")
                {
                    return false;
                }
                    return (!(Error_Code == null));
                }
        }

        /// <summary>
        /// Constructor of result, with no parameters
        /// </summary>
        public ApiResult()
        {
            this.Result_Text = null;
            this.Error_Data = null;
        }

        /// <summary>
        /// Constructor with input data
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Error"></param>
        /// <param name="Descr"></param>
        public ApiResult(string Text, string Error = null, string Descr = null)
        {
            this.Result_Text = Text;
            this.Error_Data = Descr;
            this.Error_Code = Error;
        }

    }
    public class request_api : request_core.Request
    {
        static string ApiQuery = "";

        public static LoginResult DoLogin()
        {
            Core.History("DoLogin()");
            try
            {
                //Get the result of the api login request
                ApiResult result = new ApiResult();
                result = ApiRequest("action=login", "lgname=" + System.Web.HttpUtility.UrlEncode(Config.Username), Config.Project);

                //If this returns as null then the login has failed
                if (result == null || result.Result_Text == null)
                {
                    return LoginResult.Failed;
                }

                //If no token is found (doesnt match regex) then the login has failed
                if (System.Text.RegularExpressions.Regex.Match(result.Result_Text, "token=\"[0-9A-Za-z]*\"").Success == false)
                {
                    return LoginResult.Failed;
                }

                //This means that there must be a token, So lets get this token
                login.Token = System.Text.RegularExpressions.Regex.Match(result.Result_Text, "token=\"[0-9A-Za-z]*\"").Value;
                //And format it properly
                login.Token = login.Token.Replace("\"", "");
                login.Token = login.Token.Replace("token=", "");

                //Now we will do a request with our new token
                result = ApiRequest("action=login", "lgname=" + System.Web.HttpUtility.UrlEncode(Config.Username) + "&lgpassword=" + System.Web.HttpUtility.UrlEncode(Config.Password) + "&lgtoken=" + login.Token, Config.Project);

                //As this has returned as null the login has probably failed
                if (result.Result_Text == null)
                {
                    return LoginResult.Failed;
                }

                //Now we will try and match all of the other possible values
                if (result.Result_Text.Contains("result=\"Success\""))
                {
                    // Logged in    
                    return LoginResult.Success;
                }
                if (result.Result_Text.Contains("result=\"Illegal\""))
                {
                    return LoginResult.Illegal;
                }
                if (result.Result_Text.Contains("result=\"NotExists\""))
                {
                    return LoginResult.NotExists;
                }
                if (result.Result_Text.Contains("result=\"WrongPass\""))
                {
                    return LoginResult.WrongPass;
                }
                if (result.Result_Text.Contains("result=\"NoName\""))
                {
                    return LoginResult.NoName;
                }
                if (result.Result_Text.Contains("result=\"EmptyPass\""))
                {
                    return LoginResult.EmptyPass;
                }
                if (result.Result_Text.Contains("result=\"Throttled\""))
                {
                    return LoginResult.Throttled;
                }
                if (result.Result_Text.Contains("result=\"Blocked\""))
                {
                    return LoginResult.Blocked;
                }
                if (result.Result_Text.Contains("result=\"NeedToken\""))
                {
                    return LoginResult.NeedToken;
                }
            }
            catch (Exception x)
            {
                Core.ExceptionHandler(x);
            }

            //TODO: This result we have not yet accounted for (see list below)
            //WrongPluginPass
            //CreateBlocked
            //Throttled
            //Blocked
            //mustbeposted
            //NeedToken
            return LoginResult.None;
        }

    }
}
