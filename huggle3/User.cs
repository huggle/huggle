//This is a source code or part of Huggle project
//
//This file contains code for users of wiki

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
    public class User
    {
        /// <summary>
        /// List of all instances of object
        /// </summary>
        public static List<User> UserList = new List<User>();
        /// <summary>
        /// last edit of user
        /// </summary>
        public Edit LastEdit;
        /// <summary>
        /// Anonymous
        /// </summary>
        public bool Anonymous = false;
        /// <summary>
        /// Edit count of user
        /// </summary>
        public int EditCount = 0;
        /// <summary>
        /// Whether it's a shared ip
        /// </summary>
        public bool SharedIP = false;
        /// <summary>
        /// User is a bot
        /// </summary>
        public bool Bot;
        /// <summary>
        /// Level of this user one of None, Notification, Reverted, ReportedUAA, Message, Warning, Warn1, Warn2, Warn3, Warn4im,
        /// WarnFinal, ReportedAIV, Blocked
        /// </summary>
        public UserLevel User_Level;
        /// <summary>
        /// If user is on ignore list
        /// </summary>
        public bool Ignored = false;
        /// <summary>
        /// Real name of user
        /// </summary>
        public string UserName = null;
        /// <summary>
        /// User id
        /// </summary>
        public int ID = 0;
        public int SessionEditCount = 0;
        public Page TalkPage = null;
        public bool IsCurrentUser
        {
            get
            {
                if(Config.Username == UserName)
                {
                    return true;
                }
                return false;
            }
        }
        
        
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
        
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="name">Name of this user</param>
        public User(string name)
        {
            this.UserName = name;
            this.EditCount = -1;
        }
        
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="name">Name of this user</param>
        /// <param name="ID">Wiki ID</param>
        public User(string name, int ID)
        {
            this.UserName = name;
            this.ID = ID;
            this.EditCount = -1;
        }
        
        /// <summary>
        /// Return user (logged in)
        /// </summary>
        public User Me
        {
            get
            {
                return new User(SanitizeUsername(Config.Username));
            }
        }
        
        /// <summary>
        /// Removes wrong chars from user name
        /// </summary>
        /// <param name="Name">Name to be corrected</param>
        /// <returns>Returns corrected user name</returns>
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
