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
        /// <summary>
        /// Anonymous
        /// </summary>
        private bool Anonymous;
        /// <summary>
        /// Edit count of user
        /// </summary>
        private int EditCount;
        private bool SharedIP; // wheter it's a shared ip
        public static List<user> UserList = new List<user>();
        /// <summary>
        /// User is a bot
        /// </summary>
        public bool Bot;
        /// <summary>
        /// 
        /// </summary>
        public UserLevel User_Level;
        /// <summary>
        /// If user is on ignore list
        /// </summary>
        public bool Ignored = false;
        /// <summary>
        /// Real name of user
        /// </summary>
        public string UserName;

        /// <summary>
        /// Return user page
        /// </summary>
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

        /// <summary>
        /// Return user (logged in)
        /// </summary>
        public user Me
        {
            get
            {
                return new user(SanitizeUsername(Config.Username));
            }
        }

        /// <summary>
        /// Sanitize
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Levels
        /// </summary>
        public enum  UserLevel
        {
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
