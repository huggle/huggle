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
using System.Diagnostics;

namespace huggle3
{
    /// <summary>
    /// Page
    /// </summary>
    public class Page
    {
        public class SpeedyCriterion
        {
            public string Code;
            public string DisplayCode;
            public string Description;
            public string Template;
            public string Parameter;
        }
        /// <summary>
        /// Name of the article
        /// </summary>
        public string Name;
        /// <summary>
        /// WP-Space
        /// </summary>
        public Space _Space = new Space();
        /// <summary>
        /// Does the page exist
        /// </summary>
        public bool Exists;
        /// <summary>
        /// List of all pages which are cached
        /// </summary>
        private Dictionary<string, Page> Shared_All = new Dictionary<string,Page>();
        /// <summary>
        /// First Edit
        /// </summary>
        public Edit FirstEdit;
        /// <summary>
        /// Current edit
        /// </summary>
        public Edit LastEdit;
        /// <summary>
        /// Level
        /// </summary>
        public PageLevel level;
        /// <summary>
        /// Deletes (history)
        /// </summary>
        public static List<string> deletes;
        /// <summary>
        /// Current deletes (deleted)
        /// </summary>
        public bool DeletesCurrent;
        /// <summary>
        /// Protections
        /// </summary>
        public List<Core.Protection> protection;
        /// <summary>
        /// Current protection
        /// </summary>
        public bool ProtectionsCurrent;
        /// <summary>
        /// meh
        /// </summary>
        public string HistoryOffset;
        /// <summary>
        /// Has the page been patrolled
        /// </summary>
        public bool patrolled;
        /// <summary>
        /// RcId
        /// </summary>
        public string Rcid;
        /// <summary>
        /// comment
        /// </summary>
        public SpeedyCriterion SpeedyCrit;
        /// <summary>
        /// Page text content
        /// </summary>
        public string Text;
        /// <summary>
        /// Level
        /// </summary>
        public string EditLevel;
        /// <summary>
        /// Pending revision
        /// </summary>
        public bool Pending;
        /// <summary>
        /// Level
        /// </summary>
        public string MoveLevel;

        /// <summary>
        /// Gets a value indicating whether this instance is subpage.
        /// </summary>
        /// <value><c>true</c> if this instance is subpage; otherwise, <c>false</c>.</value>
        public bool IsSubpage
        {
            get {
                if (this.Name.Contains("/"))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="huggle3.Page"/> class.
        /// </summary>
        /// <param name="Name">Name.</param>
        public Page(string Name )
        {
            this.Name = Name;
            _Space = Space.DetectSpace(Name);
            Shared_All.Add(Name, this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="huggle3.Page"/> class.
        /// </summary>
        public Page()
        {
            // special contructor, obsolete
            this.Name = "Special:Unknown";
            _Space = Space.DetectSpace(Name);
            Shared_All.Add(Name, this);
        }
        

        
        public enum PageLevel
        {
            None = 0,
            Ignore = -1,
            Watch = 1,
            Bad = 2        
        }
        
        public enum ProtectionType
        { 
            None,
            Confirmed,
            Sysop,
            PC
        }
    }
}
