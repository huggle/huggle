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
    public class edit
    {
        public bool Assisted;
        public bool Bot;
        public int Change;
        public string ChangedContent;
        public Dictionary<string, edit> All;
        public bool Deleted;
        public string Diff;
        public CacheState DiffCacheState;
        public string Id;
        public user.UserLevel LevelToWarn;
        public bool Multiple;
        public bool NewPage;
        public edit Next;
        public edit NextByUser;
        public string Oldid = "";
        public page Page;
        public edit Prev;
        public edit PrevByUser;
        public bool Processed;
        public string Rcid;
        public string RollbackToken;
        public string SightPostData;
        public bool Sighted;
        public int Size = 0;
        public string Summary;
        public string Text;
        public double random;
        public System.DateTime Time;
        public edit.EditType type;
        public string TypeToWarn;
        public user User;
        public user.UserLevel WarningLevel;

        public bool IsHuggleEdit
            { // check if edit is done by another HG user
                get {
                        if (Config.Summary == "")
                        {
                            // not defined in config
                            return false;
                        }
                        return (Summary.Contains(Config.Summary) && User.Ignored);
                    }
            }

        public edit()
        { // constructor
            this.random = new Random(DateTime.Now.Millisecond).NextDouble();
            this.Multiple = false;
            this.NewPage = false;
            this.Processed = false;
        }

        

        public override string ToString()
        { // convert to string
            return Id;
        }

        public enum CacheState
        {
                Viewed,
                Uncached,
                Caching,
                Cached
        }

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
