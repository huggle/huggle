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
using System.Diagnostics;

namespace huggle3
{
    public static class Processing
    {
        public static System.Text.RegularExpressions.Regex RC = new System.Text.RegularExpressions.Regex( "type=\"([^\"]*)\" ns=\"[^\"]*\" title=\"([^\"]*)\" rcid=\"([^\"]*)\" pageid=\"[^\"]*\"revid=\"([^\"]*)\" old_revid=\"([^\"]*)\" user=\"([^\"]*)\"( bot=\")?( anon=\"\")?( new=\")?( minor=\")? oldlen=\"([^\"]*)\" newlen=\"([^\"]*)\" timestamp=\"([^\"]*)\"( comment=\"([^\"]*)\")? />", System.Text.RegularExpressions.RegexOptions.Compiled );


        public static int Process_NewEdit(edit _Edit)
        {
            Core.History("Processing.ProcessNewEdit()");
            try
            {
                bool Redraw = false;
                if (_Edit.Page.LastEdit != null)
                {
                    _Edit.Prev = _Edit.Page.LastEdit;
                    _Edit.Prev.Next = _Edit;
                    if (_Edit.Prev.Size >= 0 && _Edit.Change != 0)
                    {
                        _Edit.Size = _Edit.Prev.Size + _Edit.Change;
                    }
                }
            }
            catch (Exception A)
            {
                Core.ExceptionHandler(A);
            }
            return 0;
        }


        public static int ProcessEdit(edit _Edit)
        {
            Core.History("Processing.ProcessEdit(new edit)");
            if (_Edit == null)
            {
                return 1;
            }

            if (_Edit.Time == DateTime.MinValue)
            {
                _Edit.Time = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            }

            if (_Edit.Oldid == null)
            { _Edit.Oldid = "prev"; }

            if (_Edit.Bot == true)
            {
                _Edit.User.Bot = true;
            }

            if (Config.PageBlankedPattern != null)
            {
                if (Config.PageBlankedPattern.IsMatch(_Edit.Summary) || _Edit.Size == 0)
                {
                    _Edit.type = edit.EditType.Blanked;
                }
            }
            if (Config.PageRedirectedPattern != null)
            {
                if (Config.PageRedirectedPattern.IsMatch(_Edit.Summary))
                {
                    _Edit.type = edit.EditType.Redirect;
                }
            }
            if (Config.PageReplacedPattern != null)
            {
                if (Config.PageReplacedPattern.IsMatch(_Edit.Summary) || (_Edit.Size >= 0 && _Edit.Change <= -200))
                {
                    _Edit.type = edit.EditType.ReplacedWith;
                }
            }
            if (Config.Summary != "" && _Edit.Summary.EndsWith(Config.Summary) && _Edit.Summary != "")
            {
                _Edit.Assisted = true;
            }

            foreach (string item in Config.AssistedSummaries)
            {
                if (_Edit.Summary.Contains(item))
                {
                    _Edit.Assisted = true;
                    break;
                }
            }

            if (_Edit.User != null && _Edit.Page != null)
            {
                if (_Edit.NewPage)
                {
                    _Edit.Page.FirstEdit = _Edit;
                    _Edit.Prev = Core.NullEdit;
                }


                if (_Edit.Summary == Config.UndoSummary + " " + Config.Summary)
                {
                    _Edit.type = edit.EditType.Revert;
                }
                if (_Edit.type == edit.EditType.Revert && _Edit.Summary.ToLower().Contains("[[special:contributions/"))
                {
                    string userName = _Edit.Summary.Substring(_Edit.Summary.ToLower().IndexOf("[[special:contributions/") + 24);
                    if (userName.Contains("]]") || userName.Contains("|"))
                    {
                        if (userName.Contains("|")) userName = userName.Substring(0, userName.IndexOf("|"));

                        if (userName.Contains("]]"))
                        {
                            userName = userName.Substring(0, userName.IndexOf("]]")); 
                        }

                        user RevertedUser = Core.GetUser(userName);

                        if (RevertedUser != _Edit.User && RevertedUser.User_Level == user.UserLevel.None)
                        {
                            RevertedUser.User_Level = user.UserLevel.Reverted;
                        }
                    }
                }

                if (_Edit.Next != null)
                {
                    if (_Edit.Next.type == edit.EditType.Revert && _Edit.User.User_Level == user.UserLevel.None)
                    {
                        _Edit.User.User_Level = user.UserLevel.Reverted;
                    }

                    if (_Edit._space == space.UserTalk && _Edit.Page.IsSubpage)
                    {
                        user.UserLevel Summary_Level;
                    }
                }
            }

            if (_Edit.Id != null)
                if (edit.All.ContainsKey(_Edit.Id))
                {
                    edit.All.Add(_Edit.Id, _Edit);
                }

            _Edit.Processed = true;

            return 0;
        }

        /// <summary>
        /// Process diff
        /// </summary>
        /// <param name="_E"></param>
        /// <param name="Diff"></param>
        /// <param name="browser"></param>
        public static void Process_Diff(edit _E, string Diff, Controls.SpecialBrowser browser)
        {
            Core.History("Processing.Process_Diff()");
            if (_E.Multiple == false)
            {
                if (Diff.Contains("<span class=\"mw-rollback-link\">"))
                {
                    string RollbackToken = Diff.Substring(Diff.IndexOf("span class=\"mw-rollback-link\">"));
                    _E.RollbackToken = System.Web.HttpUtility.HtmlDecode(Core.FindString(RollbackToken, "<a href=\"", "&amp;token=", "\""));
                }
                else
                {
                    _E.RollbackToken = null;
                }
                if (_E.Id == "next" | _E.Id == "cur" && Diff.Contains("<div id=\"mw-diff-ntitle1\">"))
                {
                    _E.Id = Core.FindString(Diff, "<div id=\"mw-diff-ntitle1\"><strong><a", "oldid=", "'");
                }
                if (_E.Id != "cur" && edit.All.ContainsKey(_E.Id))
                { 
                    _E = edit.All[_E.Id];
                }
                if (_E.Oldid == "prev" && Diff.Contains("<div id=\"mw-diff-otitle1\">"))
                {
                    _E.Oldid = Core.FindString(Diff, "<div id=\"mw-diff-otitle1\"><strong><a", "oldid=", "'");
                }
                if (Diff.Contains("<div id=\"mw-diff-ntitle2\">") && _E.User != null)
                {
                    string D_User = Core.FindString(Diff, "<div id=\"mw-diff-ntitle2\">", ">", "<");
                    D_User = System.Web.HttpUtility.HtmlDecode(D_User.Replace(" (page does not exist)", ""));
                    _E.User = Core.GetUser(D_User);
                }


                if (_E.Prev != null)
                {
                    //ok
                }
                else
                {
                    _E.Prev = new edit();
                    _E.Prev.Page = _E.Page;
                    _E.Prev.Next = _E;
                    _E.Prev.Id = _E.Oldid;
                    _E.Prev.Oldid = "prev";
                }
                if (Diff.Contains("<div id=\"mw-diff-ntitle1\">") && _E.Time == DateTime.MinValue)
                {
                    string et = Core.FindString(Diff, "<div id=\"mw-diff-ntitle1\">", "</div>");

                    if (et.Contains("Revision as of"))
                    {
                        et = Core.FindString(et, "Revision as of");
                        if ( DateTime.TryParse(et, out _E.Time) )
                        {
                            _E.Time = DateTime.SpecifyKind(DateTime.Parse(et), DateTimeKind.Local).ToUniversalTime();
                        }
                    }
                    else if (et.Contains("Current revision"))
                    {
                        et = Core.FindString(et, "Current revision</a>", "(");
                        if (DateTime.TryParse(et, out _E.Time))
                        {
                            _E.Time = DateTime.SpecifyKind(DateTime.Parse(et), DateTimeKind.Local).ToUniversalTime();
                        }
                    }
                    if (Diff.Contains("<div id=\"mw-diff-otitle1\">") && _E.Prev.Time == DateTime.MinValue)
                    {
                        string etime = Core.FindString(Diff, "<div id=\"mw-diff-otitle1\">", "</div>");
                        etime = etime.Substring(etime.IndexOf(":") - 2);
                        if (DateTime.TryParse(etime, out _E.Time))
                        {
                            _E.Time = DateTime.SpecifyKind(DateTime.Parse(etime), DateTimeKind.Local).ToUniversalTime();
                        }
                    }
                }
                if (Diff.Contains("<div id=\"mw-diff-otitle2\">") && _E.Prev.User == null)
                {
                    string username = System.Web.HttpUtility.HtmlDecode(Core.FindString(Diff, "<div id=\"mw-diff-otitle2\">", ">", "</a>"));
                    
                }
                if (_E.Prev.Summary == null)
                {
                    if (Diff.Contains("<div id=\"mw-diff-otitle3\">"))
                    {
                        string esummary = Core.FindString(Diff, "<div id=\"mw-diff-otitle3\">");
                        if (esummary.Contains("<div id=\"mw-diff-ntitle3\">"))
                        {
                            esummary = esummary.Substring(0, esummary.IndexOf("<div id=\"mw-diff-ntitle3\">"));
                        }
                        if (esummary.Contains("<span class=\"comment\">"))
                        {
                            if (esummary.Contains("</span></div>"))
                            {
                                esummary = Core.FindString(esummary, "<span class=\"comment\">", "</span></div>");
                            }
                            else if (esummary.Contains("</span>&nbsp;"))
                            {
                                esummary = Core.FindString(esummary, "<span class=\"comment\">", "</span>&nbsp;");
                            }
                            esummary = Core.HtmltoWikitext(esummary);
                            _E.Prev.Summary = esummary;
                        }
                        else
                        {
                            _E.Prev.Summary = "";
                        }
                    }
                    if (Config.PageCreatedPattern != null && Config.PageCreatedPattern.IsMatch(_E.Prev.Summary))
                    {
                        _E.Prev.Prev = Core.NullEdit;
                        _E.Page.FirstEdit = _E.Prev;
                        
                    }
                    if (Diff.Contains("<div id=\"mw-diff-ntitle4\">&nbsp;</div>"))
                    {
                        _E.Page.LastEdit = _E;
                    }

                    if (_E.Prev.Processed == false)
                    {
                        ProcessEdit(_E.Prev);
                    }
                    //_E.ChangedContent =
                    
                }
            }
            _E.Diff = Diff;
            _E.DiffCacheState = edit.CacheState.Cached;

            if (browser != null)
            {
                if ((browser.Edit.Next == _E) || browser.Edit == _E)
                {
                    DisplayEdit(_E, false, browser);
                }
            }
        }

        /// <summary>
        /// This function is used for processing of new edits
        /// </summary>
        /// <param name="_Edit"></param>
        /// <returns></returns>
        public static bool ProcessNewEdit(edit _Edit)
        {
            Core.History("Processing.ProcessEdit( _Edit )");
            if (_Edit.User == null || _Edit.Page == null)
            {
                return false;
            }
            bool Redraw = false;

            if ( _Edit.Page.LastEdit != null )
            {
                _Edit.Prev.Next = _Edit;
                _Edit.Prev = _Edit.Page.LastEdit;

                if ( _Edit.Prev.Size >= 0 && _Edit.Change != 0 )
                {
                    _Edit.Size = _Edit.Prev.Size + _Edit.Change;
                }
                if (_Edit.Change != 0 && _Edit.Size >= 0)
                {
                    _Edit.Prev.Size = _Edit.Size - _Edit.Change;
                }
            }

            if ( _Edit.User == null )
            {
                _Edit.User = _Edit.Page.LastEdit.User;
            }

            if (_Edit.User.LastEdit != null)
            {
                _Edit.PrevByUser = _Edit.User.LastEdit;
                _Edit.PrevByUser.NextByUser = _Edit;
            }

            _Edit.Page.Exists = true;
            _Edit.Page.Text = null;
            _Edit.Page.SpeedyCrit = null;
            _Edit.Page.LastEdit = _Edit;
            _Edit.User.LastEdit = _Edit;

            

            return true;
        }

        public static void Perform_Revert()
        {
            Core.History("Processing.Perform_Revert()");
        }

        /// <summary>
        /// History
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Page"></param>
        public static void Process_History(string Result, page Page)
        {
            try
            {
                Core.History("Processing.Process_History()");
                if (Result == null) return;

                System.Text.RegularExpressions.MatchCollection History = new System.Text.RegularExpressions.Regex("<rev revid=\"([^\"]+)\" parentid=\"([^\"]+)\" user=\"([^\"]+)\" (anon=\"\" )?timestamp=\"([^\"]+)\"( comment=\"([^\"]+)\")?(>([^<]*)</)?", System.Text.RegularExpressions.RegexOptions.Compiled).Matches(Result);

                if (History.Count == 0)
                {
                    if (Page.LastEdit == null)
                    {
                        Page.LastEdit = Core.NullEdit;
                    }
                }



                edit NextEdit = null;
                for (int i = 0; i < History.Count - 1; i++)
                {
                    edit _Edit = new edit();
                    string Content = History[i].Groups[1].Value;

                    if (edit.All.ContainsKey(Content))
                    {
                        _Edit = edit.All[Content];
                    }

                    _Edit.Id = Content;

                    if (_Edit.Oldid == null)
                    {
                        _Edit.Oldid = "prev";
                    }
                    _Edit.Page = Page;

                    if (History[i].Groups[8].Value != "")
                    {
                        _Edit.Text = System.Web.HttpUtility.HtmlDecode(History[i].Groups[9].Value);
                    }

                    //_Edit.User = Core.GetUser("");

                    if (_Edit.Summary == null)
                    {
                        _Edit.Summary = System.Web.HttpUtility.HtmlDecode(History[i].Groups[7].Value);
                    }

                    if (_Edit.Time == DateTime.MinValue)
                    {
                        _Edit.Time = DateTime.Parse(History[i].Groups[5].Value);
                    }

                    if (Page.LastEdit == null)
                    {
                        Page.LastEdit = _Edit;
                    }
                    else if (NextEdit != null)
                    {
                        _Edit.Next = NextEdit;
                        NextEdit.Prev = _Edit;
                        _Edit.Next.Oldid = _Edit.Id;
                    }


                    NextEdit = _Edit;
                    ProcessEdit(_Edit);
                }

                if (Result.Contains("<revisions rvstartid=\""))
                {
                    Page.HistoryOffset = NextEdit.Id;
                }
                else
                {
                    Page.HistoryOffset = null;
                    NextEdit.Prev = Core.NullEdit;
                    Page.FirstEdit = NextEdit;
                }
                Program.MainForm.Draw_History();
            }
            catch (Exception B)
            {
                Core.ExceptionHandler(B);
            }
        }

        /// <summary>
        /// Display edit
        /// </summary>
        /// <param name="_edit"></param>
        /// <param name="BrowsingHistory"></param>
        /// <param name="browser"></param>
        public static void DisplayEdit(edit _edit, bool BrowsingHistory = false, Controls.SpecialBrowser browser = null)
        {
            Core.History("Processing.DisplayEdit()");
            try
            {
                if (browser == null)
                {
                    browser = main._CurrentBrowser;
                }
                    if (_edit != null)
                    {
                        if (_edit.Page != null)
                        {
                            Program.MainForm.Set_Current_Page(_edit.Page);
                        }

                        if (_edit.Deleted)
                        {
                            
                        }

                        if (_edit.Prev == Core.NullEdit)
                        {
                            Requests.request_read.browser_html_data BrowserRequest = new Requests.request_read.browser_html_data();
                            BrowserRequest.address = Core.SitePath() + "index.php?title=" + System.Web.HttpUtility.UrlEncode(_edit.Page.Name) + "&id=" + _edit.Id;
                            BrowserRequest.browser = browser;
                            BrowserRequest.Start();
                        }
                        else if (_edit.DiffCacheState == edit.CacheState.Viewed || _edit.DiffCacheState == edit.CacheState.Cached)
                        {
                            if (_edit.Diff != null)
                            {
                                string DocumentText = "", DiffText = "";
                                DiffText = _edit.Diff;

                                DiffText = DiffText.Replace("href=\"/wiki/", "href=\"" + Config.Projects[Config.Project] + "wiki/");
                                DiffText = DiffText.Replace("href='/wiki/", "href='" + Config.Projects[Config.Project] + "wiki/");
                                DiffText = DiffText.Replace("href=\"/w/", "href=\"" + Config.Projects[Config.Project] + "w/");
                                DiffText = DiffText.Replace("href='/w/", "href='" + Config.Projects[Config.Project] + "w/");

                                browser.DocumentText = DocumentText;

                            }
                            _edit.DiffCacheState = edit.CacheState.Viewed;
                        }
                        else if (_edit.DiffCacheState == edit.CacheState.Uncached)
                        {
                            _edit.DiffCacheState = edit.CacheState.Caching;
                            Requests.request_read.diff Request = new Requests.request_read.diff();
                            Request._Edit = _edit;
                            Request.browsertab = browser;
                            Request.Start();
                        }
                        Program.MainForm.Refresh_Interface();
                    }
                }
            catch (Exception ex)
            {
                Core.ExceptionHandler( ex );
            }
        }

    }
}
