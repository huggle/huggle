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
    public class user
    {
        public edit LastEdit; // last edit of user
        private bool Anonymous; // no comment
        private int EditCount; // edit count
        private bool SharedIP; // wheter it's a shared ip
        public bool Bot; // user is a bot
        public bool Ignored = false; // if user is on ignore list
        public string UserName; // real name of user


        public string UserPage
        {
            get
            {
                return "User:" + UserName;
            }
        }

        public user(string Name)
        {
            // contructor of class
            this.UserName = Name;
            this.EditCount = -1;
        }

        public user Me
        {
            get
            {
                return new user(SanitizeUsername(Config.Username));
            }
        }

        public string SanitizeUsername(string Name)
        {
            Core.History("user.SanitizeUsername( Name )");
            if (Name == null)
            {
                return null;
            }
            Name = Name.Replace("]", "").Replace("[", "");
            return Name;
        }

        public string TalkPage
        {
            get { return "Talk:"; }
        }

        public enum  UserLevel
        {
            // levels of warn
            None,
            Notification,
            Reverted,
            ReportedUAA,
            Message,
            Warning,
            Warn1,
            Warn2,
            Warn3,
            Warn4im,
            WarnFinal,
            ReportedAIV,
            Blocked
        }
    }
}
