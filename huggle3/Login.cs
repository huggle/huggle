//This is a source code or part of Huggle project
//
//This file contains code for

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

namespace huggle3
{
	public class LoginRequest : request_core.Request
	{
		public Forms.Login Login_Form;
		public override void Process()
		{
			try
			{
				Core.History("Login-Request.Process()");
				
				Core.WriteLog("Logging in as " + Config.Username);
				
				Login.LoggedIn = false;
				
				LoginResult result;
				result = request_api.DoLogin();
				
				//If the login is not a success then try and find out what went wrong and give the relevant error message
				
				Core.WriteLog("Login resulted as " + result.ToString());
				if (result != LoginResult.Success)
				{
					switch (result)
					{
					case LoginResult.Cancelled:
						Login.Error = Languages.Get("login-error-cancelled");
						break;
					case LoginResult.NotExists:
						Login.Error = Languages.Get("login-error-notexists");
						break;
					case LoginResult.WrongPass:
						Login.Error = Languages.Get("login-error-password");
						break;
					case LoginResult.Throttled:
						Login.Error = Languages.Get("login-error-throttled");
						break;
					case LoginResult.Blocked:
						Login.Error = Languages.Get("login-error-blocked");
						break;
					case LoginResult.NeedToken:
						Login.Error = Languages.Get("login-error-needtoken");
						break;
					case LoginResult.NoName:
						Login.Error = Languages.Get("login-error-noname");
						break;
					case LoginResult.EmptyPass:
						Login.Error = Languages.Get("login-error-emptypass");
						break;
						//There are still some cases this doesnt account for
					default: // If it doesnt match any of the above give the default error message
						Login.Error = Languages.Get("login-error-unknown");
						break;
					}
					Login.Status = result;
					// remove the password from memory for security reasons
					Config.Password = "";
					Login.LoggingOn = false;
					return;
				}
				
				Login.phase = Login.LoginState.LoggedIn;
				Login.LoggedIn = true;
				Complete();
			}
			catch (Exception B)
			{
				Core.ExceptionHandler(B);
				this.Fail();
			}
		}
	}
	
	static class Login
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
		
		public static string Token = null;
		public static bool LoggedIn = false;
		public static bool LoggingOn = false;
		public static string Error = "";
		public static LoginState phase;
		public static request_core.Request.LoginResult Status;
	}
}
