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
        private DiffMode _Diffs;
        private int _Limit = 0;
        private string _SourceType = "";
        private string _Source = "";
        private bool _IgnorePages = false;
        private string _ListName = "";
        private bool _NeedsReset = false;
        private System.Text.RegularExpressions.Regex _PageRegex = null;
        private string  _Pages = null;
        private bool _Refreshing = false;
        private bool _RefreshAlways = false;
        private bool _RefreshReAdd = false;
        private List<string> _RemovedPages;
        private bool _RemoveOld = false;
        private int _RemoveAfter = 0;
        private bool _RemoveViewed = false;
        private System.Text.RegularExpressions.Regex RevisionRegex;
        private QueueSortOrder _SortOrder;
        private List<Space> _Spaces = null;
        private System.Text.RegularExpressions.Regex _SummaryRegex;
        private bool _TrayNotification = false;
        private QueueType _Type;
        private System.Text.RegularExpressions.Regex _UserRegex;
        private List<string> _Users = new List<string>();
        private List<Edit> Edits = new List<Edit>();

        private QueueFilter _FilterAnonymous = QueueFilter.None;
        private QueueFilter _FilterAssisted = QueueFilter.None;
        private QueueFilter _FilterBot = QueueFilter.None;
        private QueueFilter _FilterHuggle = QueueFilter.None;
        private QueueFilter _FilterIgnored = QueueFilter.None;
        private QueueFilter _FilterMe = QueueFilter.None;
        private QueueFilter _FilterNewPage = QueueFilter.None;
        private QueueFilter _FilterNotifications = QueueFilter.None;
        private QueueFilter _FilterOwnUserspace = QueueFilter.None;
        private QueueFilter _FilterReverts = QueueFilter.None;
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
            return true;
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
