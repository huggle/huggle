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
    class request_list : request_core.Request
    {
    }

    class request_white_list : request_core.Request
    {
        public override void Process()
        {
            Core.History("request_white_list.Process()");
            string result;

            result = DoWebRequest(Config.WhitelistUrl, "action=read&wp=" + System.Web.HttpUtility.UrlEncode(Config.Project), true);

            if (result == null)
            {
                Fail("unable to get a wl");
                login.phase = login.LoginState.Successful;
                return;
            }

            result = result.Replace("<!-- list -->", "");

            Config.Whitelist.AddRange(result.Split('|'));

            login.phase = login.LoginState.Successful;

            Complete();
        }

    }
}
