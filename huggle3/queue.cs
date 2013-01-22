//This is a source code or part of Huggle project
//
//This file contains code for queues

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
    public class Queue
    {
        public static Dictionary<string, Queue> All = new Dictionary<string,Queue>();
        public string _name = "";
        public DiffMode _Diffs;
        public int _Limit = 0;
        public string _SourceType = "";
        public string _Source = "";
        public bool _IgnorePages = false;
        public string _ListName = "";
        public bool _NeedsReset = false;
        public System.Text.RegularExpressions.Regex _PageRegex = null;
        public string _Pages = null;
        public bool _Refreshing = false;
        public bool _RefreshAlways = false;
        public bool _RefreshReAdd = false;
        public List<string> _RemovedPages;
        public bool _RemoveOld = false;
        public int _RemoveAfter = 0;
        public bool _RemoveViewed = false;
        public System.Text.RegularExpressions.Regex RevisionRegex;
        public QueueSortOrder _SortOrder;
        public List<Space> _Spaces = null;
        public System.Text.RegularExpressions.Regex _SummaryRegex;
        public bool _TrayNotification = false;
        public QueueType _Type;
        public System.Text.RegularExpressions.Regex _UserRegex;
        public Controls.QueuePanel Panel = null;
        public List<string> _Users = new List<string>();
        public List<Edit> Edits = new List<Edit>();

        public QueueFilter _FilterAnonymous = QueueFilter.None;
        public QueueFilter _FilterAssisted = QueueFilter.None;
        public QueueFilter _FilterBot = QueueFilter.None;
        public QueueFilter _FilterHuggle = QueueFilter.None;
        public QueueFilter _FilterIgnored = QueueFilter.None;
        public QueueFilter _FilterMe = QueueFilter.None;
        public QueueFilter _FilterNewPage = QueueFilter.None;
        public QueueFilter _FilterNotifications = QueueFilter.None;
        public QueueFilter _FilterOwnUserspace = QueueFilter.None;
        public QueueFilter _FilterReverts = QueueFilter.None;
        private QueueFilter _FilterTags = QueueFilter.None;
        private QueueFilter _FilterWarnings = QueueFilter.None;

        public Queue(string Name)
        {
            _name = Name;
            _RemoveViewed = true;
        }

        public string Name
        {
            set { _name = Name; }
            get { return _name; }
        }

        public bool Matches(Edit _edit)
        {
            return false;
        }

        public static void Enqueue(Edit _edit)
        {
            lock (All)
            {
                foreach (KeyValuePair<string,Queue> x in All)
                {
                    if (x.Value.Matches(_edit))
                    {
                        x.Value.Edits.Add(_edit);
                    }
                }
            }
        }

        enum QueueSortOrder
        {
                Time , TimeReverse , Quality
        }
        enum DiffMode
        {
                None , Preload , All
        }
        enum QueueType
        {
            FixedList , LiveList , Live, Dynamic
        }
        enum QueueFilter
        {
            Require, None, Exclude
        }
    }
}
