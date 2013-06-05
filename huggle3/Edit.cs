//This is a source code or part of Huggle project
//
//This file contains code for edit

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
using System.Diagnostics;

namespace huggle3
{
    public class Edit
    {
        /// <summary>
        /// Is the edit a program assisted edit (AWB,HG)
        /// </summary>
        public bool Assisted = false;
        /// <summary>
        /// Is the edit made by a bot
        /// </summary>
        public bool Bot = false;
        /// <summary>
        /// Value of the change in size of the page
        /// </summary>
        public int Change = 0;
        /// <summary>
        /// Diff
        /// </summary>
        public string ChangedContent = null;
        /// <summary>
        /// List of all edits loaded in memory
        /// </summary>
        public static Dictionary<string, Edit> All = new Dictionary<string,Edit>();
        /// <summary>
        /// Removed edit
        /// </summary>
        public bool Deleted = false;
        /// <summary>
        /// Space
        /// </summary>
        public Space EditSpace = null;
        /// <summary>
        /// string data of diff
        /// </summary>
        public string Diff = null;
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
        public User.UserLevel LevelToWarn;
        /// <summary>
        /// Multiple edit
        /// </summary>
        public bool Multiple = false;
        /// <summary>
        /// Is the edit a creation of a new page
        /// </summary>
        public bool NewPage = false;
        /// <summary>
        /// Next edit
        /// </summary>
        public Edit Next = null;
        /// <summary>
        /// Next edit (user)
        /// </summary>
        public Edit NextByUser = null;
        /// <summary>
        /// Previous Id
        /// </summary>
        public string Oldid = "";
        /// <summary>
        /// Page
        /// </summary>
        public Page _Page = null;
        /// <summary>
        /// Previous edit
        /// </summary>
        public Edit Prev = null;
        /// <summary>
        /// Previous edit (user)
        /// </summary>
        public Edit PrevByUser = null;
        /// <summary>
        /// Processed edit
        /// </summary>
        public bool Processed = false;
        /// <summary>
        /// RCID (Recent Change ID)
        /// </summary>
        public string Rcid = null;
        /// <summary>
        /// Rollback token
        /// </summary>
        public string RollbackToken = null;
        /// <summary>
        /// Post data
        /// </summary>
        public string SightPostData = null;
        /// <summary>
        /// Sighted
        /// </summary>
        public bool Sighted = false;
        /// <summary>
        /// Size
        /// </summary>
        public int Size = 0;
        /// <summary>
        /// Summary (edit)
        /// </summary>
        public string Summary = null;
        /// <summary>
        /// Content of the page
        /// </summary>
        public string Text = null;
        /// <summary>
        /// random number
        /// </summary>
        public double _Random = 0;
        /// <summary>
        /// Time of the edit
        /// </summary>
        public System.DateTime Time;
        /// <summary>
        /// Type of edit
        /// </summary>
        public Edit.EditType Type;
        /// <summary>
        /// Warning type
        /// </summary>
        public string _TypeToWarn = null;
        /// <summary>
        /// Edit User
        /// </summary>
        public User EditUser = null;
        /// <summary>
        /// Value of warning the user has
        /// </summary>
        public User.UserLevel WarningLevel;
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
                return (Summary.Contains(Config.Summary) && EditUser.Ignored);
            }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public Edit()
        { 
            this._Random = new Random(DateTime.Now.Millisecond).NextDouble();
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
