//This is a source code or part of Huggle project
//
//This file contains code for spaces

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
    public class Space
    {
        public int number;
        private bool locked;
        public static Space Article = new Space(0);
        private bool subpages;
        private bool unmovable;
        private string name;
        public static Space Talk = new Space(1, "Talk", _Subp: true );
        public static Space User = new Space(2, "User", _Subp: true );
        public static Space UserTalk = new Space(3, "User talk", _Subp: true);
        public static Space Project = new Space(4, "Project", _Subp: true);
        public static Space ProjectTalk = new Space(5, "Project talk", _Subp: true);
        public static Space Image = new Space(6, "Image", _Subp: false);
        public static Space ImageTalk = new Space(7, "Image talk", _Subp: false);
        public static Space MediaWiki = new Space(8, "MediaWiki", _Lckd: true);
        public static Space MediaWikiTalk = new Space(9, "MediaWiki talk", _Subp: true);
        public static Space Template = new Space(10, "Template", _Subp: true);
        public static Space TemplateTalk = new Space(11, "Template talk", _Subp: true);
        public static Space Help = new Space(12, "Help", _Subp: true);
        public static Space HelpTalk = new Space(13, "Help talk", _Subp: true);
        public static Space Category = new Space(14, "Category", _Subp: true);
        public static Space File = new Space(16, "File", _Subp: true);
        public static Space FileTalk = new Space(17, "File talk", _Subp: true);
        
        public static List<Space> _All = new List<Space>();
        
        public List<Space> All
        {
            get { return Core.All.spaces; }
        }
        
        public Space()
        {
            this.name = "";
            //space._All.Add(this);
        }
        
        public static int SpaceID(string page)
        {
            int value = 0;
            if (page.Contains(":"))
            {
                foreach (Space x in Core.All.spaces)
                {
                    if (page.StartsWith(x.Name + ":"))
                    {
                        value = x.Number;
                    }
                }
            }
            return value;
        }
        
        public static Space DetectSpace(string page)
        {
            Core.History("DetectSpace( string )");
            if (page.Contains(":"))
            {
                foreach (Space x in Core.All.spaces)
                {
                    if (page.StartsWith(x.Name + ":"))
                    {
                        return x;
                    }
                }
            }
            else
            {
                return Article;
            }
            return null;
        }
        
        public bool Locked
        { get { return this.locked; } }
        
        public string Name
        { 
            get { return this.name; }
        }
        
        public int Number
        {
            get { return this.number; }
        }
        
        public bool Subpages
        {
            get { return this.subpages; }
        }
        
        public bool Unmovable
        {
            get {
                return this.unmovable;
            }
        }
        
        bool SetName(int _Number)
        {
            return true;
        }
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_Number">ID</param>
        /// <param name="_Name">Name</param>
        /// <param name="_Lckd">Locked</param>
        /// <param name="_Unmovable">Unmovable</param>
        /// <param name="_Subp">Subpage</param>
        public Space(int _Number, string _Name = null, bool _Lckd = false, bool _Unmovable = false, bool _Subp = false)
        {
            Core.History("space.space()");
            this.number = _Number;
            this.unmovable = _Unmovable;
            this.locked = _Lckd;
            this.subpages = _Subp;
            this.name = _Name;
            Core.All.spaces.Add(this);
        }
        
        
    }
}
