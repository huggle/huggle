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

namespace huggle3
{
    public class LoginRequest : request_core.Request
    {
        public LoginForm Login_Fr;
        public override void Process()
        {
            try {
                Core.History("Login-Request.Process()");

                login.LoggedIn = false;

                LoginResult result;
                result = request_api.DoLogin();

                if (result != LoginResult.Success)
                {
                    login.Status = result;
                    login.LoggingOn = false;
                    return;
                }

                login.phase = login.LoginState.LoggedIn;
                login.LoggedIn = true;
                Complete();
            }
            catch (Exception B)
            {
                Core.ExceptionHandler(B);
                System.Threading.Thread.CurrentThread.Abort();
            }
        }
    }
    
    static class login
    {
        public enum LoginState
        {
            LoggingIn,
            LoadingGlobal,
            LoadingLocal,
            Whitelist,
            LoggedIn,
            LoadedLocal,
            LoadedGlobal,
            Successful,
            Error
        }

        public static string Token;
        public static bool LoggedIn;
        public static bool LoggingOn;
        public static LoginState phase;
        public static request_core.Request.LoginResult Status;
        static public int Login()
        {
            //this function perform login
            return 0;
        }
    }
}
