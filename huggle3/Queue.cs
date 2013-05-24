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
    /// <summary>
    /// Queue
    /// </summary>
    public class Queue
    {
        /// <summary>
        /// List of all queues loaded in system
        /// </summary>
        public static Dictionary<string, Queue> All = new Dictionary<string, Queue>();
        /// <summary>
        /// The name.
        /// </summary>
        private string name = "";
        public DiffMode _Diffs;
        /// <summary>
        /// Limit.
        /// </summary>
        public int Limit = 0;
        /// <summary>
        /// The type of the source.
        /// </summary>
        public string SourceType = "";
        /// <summary>
        /// The source.
        /// </summary>
        public string Source = "";
        /// <summary>
        /// Ignore pages
        /// </summary>
        public bool IgnorePages = false;
        /// <summary>
        /// The name of the list.
        /// </summary>
        public string ListName = "";
        /// <summary>
        /// Needs reset.
        /// </summary>
        public bool NeedsReset = false;
        public System.Text.RegularExpressions.Regex _PageRegex = null;
        public string Pages = null;
        public bool Refreshing = false;
        public bool RefreshAlways = false;
        public bool RefreshReAdd = false;
        public List<string> RemovedPages = null;
        public bool RemoveOld = false;
        public int RemoveAfter = 0;
        public bool RemoveViewed = false;
        public System.Text.RegularExpressions.Regex RevisionRegex = null;
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
        public QueueFilter _FilterTags = QueueFilter.None;
        public QueueFilter _FilterWarnings = QueueFilter.None;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">Name</param>
        public Queue(string Name)
        {
            name = Name;
            RemoveViewed = true;
        }

        public bool Matches(Edit _edit)
        {
            bool matches = false;
            // check page name
            if (_PageRegex != null && _edit._Page != null)
            {
                if (_PageRegex.IsMatch(_edit._Page.Name))
                {
                    matches = true;
                }
                else
                {
                    return false;
                }
            }
            
            // check summary
            if (_SummaryRegex != null && _edit.Summary != null)
            {
                if (_SummaryRegex.IsMatch(_edit.Summary))
                {
                    matches = true;
                }
                else
                {
                    return false;
                }
            }
            
            if (_UserRegex != null && _edit.EditUser != null)
            {
                if (_UserRegex.IsMatch(_edit.EditUser.UserName))
                {
                    matches = true;
                }
                else
                {
                    return false;
                }
            }
            
            return matches;
        }
        
        
        
        /// <summary>
        /// Convert string to queue object, if exist
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Queue with the provided name</returns>
        public static Queue fromString(string name)
        {
            Core.History("Queue.fromString(string)");
            lock (All)
            {
                foreach (KeyValuePair<string, Queue> current_queue in All)
                {
                    if (current_queue.Value.name == name)
                    {
                        return current_queue.Value;
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// Return true in case that a queue panel is currently drawing this queue
        /// </summary>
        /// <returns></returns>
        public bool MatchesPanel()
        {
            if (Panel != null)
            {
                if (Panel.queue == this)
                {
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Check if the queue has this edit
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        public bool ContainsEdit(Edit edit)
        {
            lock (Edits)
            {
                if (edit == null)
                {
                    throw new Exception("The edit you were about to check is null");
                }
                
                // performance tweak
                if (Edits.Contains(edit))
                {
                    return true;
                }
                
                foreach (Edit kk in Edits)
                {
                    if (edit.Id != kk.Id)
                    {
                        continue;
                    }
                    
                    if (edit.Summary != kk.Summary)
                    {
                        continue;
                    }
                    
                    if (edit.Rcid != kk.Rcid)
                    {
                        continue;
                    }
                    
                    if (edit._Page != null && kk._Page != null)
                    {
                        if (edit._Page.Name != kk._Page.Name)
                        {
                            continue;
                        }
                    }
                    
                    if (edit.Oldid != kk.Oldid)
                    {
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Insert an edit to any queue which matches the data
        /// </summary>
        /// <param name="_edit"></param>
        public static void Enqueue(Edit _edit)
        {
            lock (All)
            {
                foreach (KeyValuePair<string, Queue> x in All)
                {
                    if (!x.Value.ContainsEdit(_edit))
                    {
                        if (x.Value.Matches(_edit))
                        {
                            lock (x.Value.Edits)
                            {
                                x.Value.Edits.Add(_edit);
                            }
                            // Check if this is currently selected queue
                            if (x.Value.MatchesPanel())
                            {
                                // $fixme$
                                //x.Value.Panel.Add(_edit);
                            }
                        }
                    }
                }
            }
        }
        
        public enum QueueSortOrder
        {
            Time, TimeReverse, Quality
        }
        
        public enum DiffMode
        {
            None, Preload, All
        }
        
        public enum QueueType
        {
            FixedList, LiveList, Live, Dynamic
        }
        
        public enum QueueFilter
        {
            Require, None, Exclude
        }
    }
}
