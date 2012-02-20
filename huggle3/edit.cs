//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Petrb

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
using System.Diagnostics;

namespace huggle3
{
    public class edit
    {
        /// <summary>
        /// Is the edit a program assisted edit (AWB,HG)
        /// </summary>
        public bool Assisted;
        /// <summary>
        /// Is the edit made by a bot
        /// </summary>
        public bool Bot;
        /// <summary>
        /// Value of the change in size of the page
        /// </summary>
        public int Change;
        /// <summary>
        /// Diff
        /// </summary>
        public string ChangedContent;
        public static Dictionary<string, edit> All = new Dictionary<string,edit>();
        /// <summary>
        /// Removed edit
        /// </summary>
        public bool Deleted;
        /// <summary>
        /// Space
        /// </summary>
        public space _space;
        /// <summary>
        /// string data of diff
        /// </summary>
        public string Diff;
        /// <summary>
        /// State
        /// </summary>
        public CacheState DiffCacheState = CacheState.Uncached;
        /// <summary>
        /// ID
        /// </summary>
        public string Id = "";
        /// <summary>
        /// Warning level
        /// </summary>
        public user.UserLevel LevelToWarn;
        /// <summary>
        /// Multiple edit
        /// </summary>
        public bool Multiple;
        /// <summary>
        /// Is the edit a creation of a new page
        /// </summary>
        public bool NewPage;
        /// <summary>
        /// Next edit
        /// </summary>
        public edit Next;
        /// <summary>
        /// Next edit (user)
        /// </summary>
        public edit NextByUser;
        /// <summary>
        /// Previous Id
        /// </summary>
        public string Oldid = "";
        /// <summary>
        /// Page
        /// </summary>
        public page Page;
        /// <summary>
        /// Previous edit
        /// </summary>
        public edit Prev;
        /// <summary>
        /// Previous edit (user)
        /// </summary>
        public edit PrevByUser;
        /// <summary>
        /// Processed edit
        /// </summary>
        public bool Processed;
        /// <summary>
        /// RCID (Recent Change ID)
        /// </summary>
        public string Rcid;
        /// <summary>
        /// Rollback token
        /// </summary>
        public string RollbackToken;
        /// <summary>
        /// Post data
        /// </summary>
        public string SightPostData;
        /// <summary>
        /// Sighted
        /// </summary>
        public bool Sighted;
        /// <summary>
        /// Size
        /// </summary>
        public int Size = 0;
        /// <summary>
        /// Summary (edit)
        /// </summary>
        public string Summary;
        /// <summary>
        /// Content of the page
        /// </summary>
        public string Text;
        /// <summary>
        /// random number
        /// </summary>
        public double random;
        /// <summary>
        /// Time of the edit
        /// </summary>
        public System.DateTime Time;
        /// <summary>
        /// Type of edit
        /// </summary>
        public edit.EditType type;
        /// <summary>
        /// Warning type
        /// </summary>
        public string TypeToWarn;
        /// <summary>
        /// Edit User
        /// </summary>
        public user User;
        /// <summary>
        /// Value of warning the user has
        /// </summary>
        public user.UserLevel WarningLevel;

        /// <summary>
        /// Return if edit is done by another HG user
        /// </summary>
        public bool IsHuggleEdit
            {
                get {
                        if (Config.Summary == "")
                        {
                            // not defined in config
                            return false;
                        }
                        return (Summary.Contains(Config.Summary) && User.Ignored);
                    }
            }

        /// <summary>
        /// Constructor
        /// </summary>
        public edit()
        { 
            this.random = new Random(DateTime.Now.Millisecond).NextDouble();
            this.Multiple = false;
            this.NewPage = false;
            this.Processed = false;
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        /// <returns>Id</returns>
        public override string ToString()
        {
            return Id;
        }

        /// <summary>
        /// Cache
        /// </summary>
        public enum CacheState
        {
                Viewed,
                Uncached,
                Caching,
                Cached
        }

        /// <summary>
        /// edit type
        /// </summary>
        public enum EditType
        {
            Blanked = 2,
            ReplacedWith = 1,
            None = 0,
            Revert = -1,
            Notification = -2,
            Tag = -3,
            Warning = -4,
            Report = -5,
            Redirect = -6,
        }
    }
}
