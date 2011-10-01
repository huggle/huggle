//This is a source code or part of Huggle project
//
//This file contains code
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
    public class space
    {
        private int number;
        private bool locked;
        public static space Article = new space(0);
        private bool subpages;
        private bool unmovable;
        private string name;
        public static space Talk = new space(1, "Talk", _Subp: true );
        public static space User = new space(2, "User", _Subp: true );
        public static space UserTalk = new space(3, "User talk", _Subp: true);
        public static space Project = new space(4, "Project", _Subp: true);
        public static space ProjectTalk = new space(5, "Project talk", _Subp: true);
        public static space Image = new space(6, "Image", _Subp: false);
        public static space ImageTalk = new space(7, "Image talk", _Subp: false);
        public static space MediaWiki = new space(8, "MediaWiki", _Lckd: true);
        public static space MediaWikiTalk = new space(9, "MediaWiki talk", _Subp: true);
        public static space Template = new space(10, "Template", _Subp: true);
        public static space TemplateTalk = new space(11, "Template talk", _Subp: true);
        public static space Help = new space(12, "Help", _Subp: true);
        public static space HelpTalk = new space(13, "Help talk", _Subp: true);
        public static space Category = new space(14, "Category", _Subp: true);
        public static space File = new space(16, "File", _Subp: true);
        public static space FileTalk = new space(17, "File talk", _Subp: true);

        private List<space> _All;

        public List<space> All
        {
            get { return _All; }
        }

        public space()
        {
            this.name = "";
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

      

        space(int _Number, string _Name = null, bool _Lckd = false, bool _Unmovable = false, bool _Subp = false)
        {
            this.number = _Number;
            this.unmovable = _Unmovable;
            this.locked = _Lckd;
            this.subpages = _Subp;
            this.name = _Name;
        }


    }
}
