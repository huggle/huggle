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
    public class queue
    {
        public Dictionary<string, queue> All;
        public string _name = "";
        private DiffMode _Diffs;
        private int _Limit = 0;
        private string _SourceType = "";
        private string _Source = "";
        private bool _IgnorePages = false;
        private string _ListName = "";
        private bool _NeedsReset = false;
        private System.Text.RegularExpressions.Regex _PageRegex;
        private string  _Pages;
        private bool _Refreshing = false;
        private bool _RefreshAlways = false;
        private bool _RefreshReAdd = false;
        private List<string> _RemovedPages;
        private bool _RemoveOld = false;
        private int _RemoveAfter = 0;
        private bool _RemoveViewed = false;
        private System.Text.RegularExpressions.Regex RevisionRegex;
        private QueueSortOrder _SortOrder;
        private List<space> _Spaces;
        private System.Text.RegularExpressions.Regex _SummaryRegex;
        private bool _TrayNotification = false;
        private QueueType _Type;
        private System.Text.RegularExpressions.Regex _UserRegex;
        private List<string> _Users = new List<string>();

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

        public queue(string Name)
        {
            _name = Name;
            _RemoveViewed = true;
        }

        public string Name
        {
            set { _name = Name; }
            get { return _name; }
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
