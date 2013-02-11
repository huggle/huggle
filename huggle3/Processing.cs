//This is a source code or part of Huggle project
//
//This file contains code for processing

/// <DOCUMENTATION>
/// How this works:
/// Huggle parses all data using this class, edit is either displayed or processed
/// 
/// when you create an instance of edit, it contains only some basic data,
/// mostly what you get from a feed you are currently using, like irc,
/// calling Process on this edit will attempt to retrieve all possible
/// information using mediawiki.
/// 
/// You don't need to process an edit in order to use it, but it will likely
/// be requested to be processed later, if you won't do it, so processing
/// edit after you create is a good way to save resources
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
    public static class Processing
    {
        public static System.Text.RegularExpressions.Regex RC = new System.Text.RegularExpressions.Regex
            ("type=\"([^\"]*)\" ns=\"[^\"]*\" title=\"([^\"]*)\" rcid=\"([^\"]*)\" pageid=\"[^\"]*\"revid=\"([^\"]*)\" " +
             "old_revid=\"([^\"]*)\" user=\"([^\"]*)\"( bot=\")?( anon=\"\")?( new=\")?( minor=\")? oldlen=\"([^\"]*)\" " +
             "newlen=\"([^\"]*)\" timestamp=\"([^\"]*)\"( comment=\"([^\"]*)\")? />", System.Text.RegularExpressions.RegexOptions.Compiled);

        /// <summary>
        /// Check if the object contains all data and if not, if will download them from wikimedia project
        /// </summary>
        /// <param name="edit">Edit</param>
        /// <returns>1 on error (not enough data), 0 on success</returns>
        public static int ProcessEdit(Edit edit)
        {
            Core.History("Processing.ProcessEdit(new edit)");
            // in case edit doesn't contain any basic data, we don't know what to do with it, so we return 1
            if (edit == null)
            {
                return 1;
            }

            // if there is no time for this edit, we give it current time
            if (edit._Time == DateTime.MinValue)
            {
                edit._Time = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            }

            if (edit.Oldid == null)
            { edit.Oldid = "prev"; }

            // if edit has a bot flag, the user is a bot
            if (edit._Bot == true)
            {
                if (edit._User != null)
                {
                    edit._User.Bot = true;
                }
                else
                {
                    Core.DebugLog("User object for edit was null at " + edit.Id);
                }
            }

            // we check if there is config pattern for blanking edit in config, and if so, we check if this edit
            // is removing all text from a page
            if (Config.PageBlankedPattern != null)
            {
                if (Config.PageBlankedPattern.IsMatch(edit.Summary) || edit.Size == 0)
                {
                    edit._Type = Edit.EditType.Blanked;
                }
            }

            // we check the same for redirect pattern

            if (Config.PageRedirectedPattern != null)
            {
                if (Config.PageRedirectedPattern.IsMatch(edit.Summary))
                {
                    edit._Type = Edit.EditType.Redirect;
                }
            }

            if (Config.PageReplacedPattern != null)
            {
                if (Config.PageReplacedPattern.IsMatch(edit.Summary) || (edit.Size >= 0 && edit._Change <= -200))
                {
                    edit._Type = Edit.EditType.ReplacedWith;
                }
            }

            if (Config.Summary != "" && edit.Summary.EndsWith(Config.Summary) && edit.Summary != "")
            {
                edit._Assisted = true;
            }

            foreach (string item in Config.AssistedSummaries)
            {
                if (edit.Summary.Contains(item))
                {
                    edit._Assisted = true;
                    break;
                }
            }

            // if user or page is not unknown object, we retrieve information for that as well

            if (edit._User != null && edit._Page != null)
            {
                if (edit.NewPage)
                {
                    edit._Page.FirstEdit = edit;
                    edit.Prev = Core.NullEdit;
                }

                if (edit.Summary == Config.UndoSummary + " " + Config.Summary)
                {
                    edit._Type = Edit.EditType.Revert;
                }

                if (edit._Type == Edit.EditType.Revert && edit.Summary.ToLower().Contains("[[special:contributions/"))
                {
                    string userName = edit.Summary.Substring(edit.Summary.ToLower().IndexOf("[[special:contributions/") + 24);
                    if (userName.Contains("]]") || userName.Contains("|"))
                    {
                        if (userName.Contains("|")) userName = userName.Substring(0, userName.IndexOf("|"));

                        if (userName.Contains("]]"))
                        {
                            userName = userName.Substring(0, userName.IndexOf("]]"));
                        }

                        User RevertedUser = Core.GetUser(userName);

                        if (RevertedUser != edit._User && RevertedUser.User_Level == User.UserLevel.None)
                        {
                            RevertedUser.User_Level = User.UserLevel.Reverted;
                        }
                    }
                }

                if (edit.Next != null)
                {
                    if (edit.Next._Type == Edit.EditType.Revert && edit._User.User_Level == User.UserLevel.None)
                    {
                        edit._User.User_Level = User.UserLevel.Reverted;
                    }

                    if (edit._Space == Space.UserTalk && edit._Page.IsSubpage)
                    {
                        //User.UserLevel Summary_Level;
                    }
                }
            }

            if (edit.Id != null)
            {
                if (Edit.All.ContainsKey(edit.Id))
                {
                    Edit.All.Add(edit.Id, edit);
                }
            }

            // we successfully processed edit
            edit.Processed = true;

            return 0;
        }

        /// <summary>
        /// Process diff
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="diff"></param>
        /// <param name="browser"></param>
        public static void ProcessDiff(Edit edit, string diff, Controls.SpecialBrowser browser)
        {
            Core.History("Processing.Process_Diff()");
            if (edit._Multiple == false)
            {
                if (diff.Contains("<span class=\"mw-rollback-link\">"))
                {
                    // edit contains a rollback token, so we save it for later use
                    string RollbackToken = diff.Substring(diff.IndexOf("span class=\"mw-rollback-link\">"));
                    edit.RollbackToken = System.Web.HttpUtility.HtmlDecode(Core.FindString(RollbackToken, "<a href=\"", "&amp;token=", "\""));
                }

                if (edit.Id == "next" | edit.Id == "cur" && diff.Contains("<div id=\"mw-diff-ntitle1\">"))
                {
                    edit.Id = Core.FindString(diff, "<div id=\"mw-diff-ntitle1\"><strong><a", "oldid=", "'");
                }

                if (edit.Id != "cur" && Edit.All.ContainsKey(edit.Id))
                {
                    edit = Edit.All[edit.Id];
                }

                if (edit.Oldid == "prev" && diff.Contains("<div id=\"mw-diff-otitle1\">"))
                {
                    edit.Oldid = Core.FindString(diff, "<div id=\"mw-diff-otitle1\"><strong><a", "oldid=", "'");
                }

                if (diff.Contains("<div id=\"mw-diff-ntitle2\">") && edit._User != null)
                {
                    string D_User = Core.FindString(diff, "<div id=\"mw-diff-ntitle2\">", ">", "<");
                    D_User = System.Web.HttpUtility.HtmlDecode(D_User.Replace(" (page does not exist)", ""));
                    edit._User = Core.GetUser(D_User);
                }


                if (edit.Prev != null)
                {
                    //ok
                }
                else
                {
                    edit.Prev = new Edit();
                    edit.Prev._Page = edit._Page;
                    edit.Prev.Next = edit;
                    edit.Prev.Id = edit.Oldid;
                    edit.Prev.Oldid = "prev";
                }
                if (diff.Contains("<div id=\"mw-diff-ntitle1\">") && edit._Time == DateTime.MinValue)
                {
                    string et = Core.FindString(diff, "<div id=\"mw-diff-ntitle1\">", "</div>");

                    if (et.Contains("Revision as of"))
                    {
                        et = Core.FindString(et, "Revision as of");
                        if (DateTime.TryParse(et, out edit._Time))
                        {
                            edit._Time = DateTime.SpecifyKind(DateTime.Parse(et), DateTimeKind.Local).ToUniversalTime();
                        }
                    }
                    else if (et.Contains("Current revision"))
                    {
                        et = Core.FindString(et, "Current revision</a>", "(");
                        if (DateTime.TryParse(et, out edit._Time))
                        {
                            edit._Time = DateTime.SpecifyKind(DateTime.Parse(et), DateTimeKind.Local).ToUniversalTime();
                        }
                    }
                    if (diff.Contains("<div id=\"mw-diff-otitle1\">") && edit.Prev._Time == DateTime.MinValue)
                    {
                        string etime = Core.FindString(diff, "<div id=\"mw-diff-otitle1\">", "</div>");
                        etime = etime.Substring(etime.IndexOf(":") - 2);
                        if (DateTime.TryParse(etime, out edit._Time))
                        {
                            edit._Time = DateTime.SpecifyKind(DateTime.Parse(etime), DateTimeKind.Local).ToUniversalTime();
                        }
                    }
                }
                if (diff.Contains("<div id=\"mw-diff-otitle2\">") && edit.Prev._User == null)
                {
                    string username = System.Web.HttpUtility.HtmlDecode(Core.FindString(diff, "<div id=\"mw-diff-otitle2\">", ">", "</a>"));

                }
                if (edit.Prev.Summary == null)
                {
                    if (diff.Contains("<div id=\"mw-diff-otitle3\">"))
                    {
                        string esummary = Core.FindString(diff, "<div id=\"mw-diff-otitle3\">");
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
                            edit.Prev.Summary = esummary;
                        }
                        else
                        {
                            edit.Prev.Summary = "";
                        }
                    }
                    if (Config.PageCreatedPattern != null && Config.PageCreatedPattern.IsMatch(edit.Prev.Summary))
                    {
                        edit.Prev.Prev = Core.NullEdit;
                        edit._Page.FirstEdit = edit.Prev;

                    }
                    if (diff.Contains("<div id=\"mw-diff-ntitle4\">&nbsp;</div>"))
                    {
                        edit._Page.LastEdit = edit;
                    }

                    if (edit.Prev.Processed == false)
                    {
                        ProcessEdit(edit.Prev);
                    }
                    //_E.ChangedContent =

                }
            }
            edit.Diff = diff;
            edit.DiffCacheState = Edit.CacheState.Cached;

            if (browser != null)
            {
                if ((browser.Edit.Next == edit) || browser.Edit == edit)
                {
                    DisplayEdit(edit, false, browser);
                }
            }
        }

        /// <summary>
        /// This function is used for processing of new edits
        /// </summary>
        /// <param name="_Edit"></param>
        /// <returns></returns>
        public static bool ProcessNewEdit(Edit _Edit)
        {
            Core.History("Processing.ProcessEdit( _Edit )");
            if (_Edit._User == null || _Edit._Page == null)
            {
                return false;
            }
            //bool Redraw = false;

            if (_Edit._Page.LastEdit != null)
            {
                _Edit.Prev.Next = _Edit;
                _Edit.Prev = _Edit._Page.LastEdit;

                if (_Edit.Prev.Size >= 0 && _Edit._Change != 0)
                {
                    _Edit.Size = _Edit.Prev.Size + _Edit._Change;
                }
                if (_Edit._Change != 0 && _Edit.Size >= 0)
                {
                    _Edit.Prev.Size = _Edit.Size - _Edit._Change;
                }
            }

            if (_Edit._User == null)
            {
                _Edit._User = _Edit._Page.LastEdit._User;
            }

            if (_Edit._User.LastEdit != null)
            {
                _Edit.PrevByUser = _Edit._User.LastEdit;
                _Edit.PrevByUser.NextByUser = _Edit;
            }

            _Edit._Page.Exists = true;
            _Edit._Page.Text = null;
            _Edit._Page.SpeedyCrit = null;
            _Edit._Page.LastEdit = _Edit;
            _Edit._User.LastEdit = _Edit;


            return true;
        }

        public static void Process_Revert(Edit Edit, string Summary = "", bool Rollback = true, bool Undo = false, bool Currentonly = false)
        {
            Core.History("Processing.Process_Revert()");
            if (Edit == null)
                return;
            User LastUser = null;

            if (Edit._Page.LastEdit != null)
            {
                LastUser = Edit._Page.LastEdit._User;
            }

            if (Config.ConfirmSelfRevert && !Undo)
            {

            }


        }

        /// <summary>
        /// History
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Page"></param>
        public static void Process_History(string Result, Page Page)
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
                Edit NextEdit = null;
                for (int i = 0; i < History.Count - 1; i++)
                {
                    Edit _Edit = new Edit();
                    string Content = History[i].Groups[1].Value;

                    if (Edit.All.ContainsKey(Content))
                    {
                        _Edit = Edit.All[Content];
                    }

                    _Edit.Id = Content;

                    if (_Edit.Oldid == null)
                    {
                        _Edit.Oldid = "prev";
                    }
                    _Edit._Page = Page;

                    if (History[i].Groups[8].Value != "")
                    {
                        _Edit._Text = System.Web.HttpUtility.HtmlDecode(History[i].Groups[9].Value);
                    }

                    _Edit._User = Core.GetUser(System.Web.HttpUtility.HtmlDecode(History[i].Groups[3].Value));

                    if (_Edit.Summary == null)
                    {
                        _Edit.Summary = System.Web.HttpUtility.HtmlDecode(History[i].Groups[7].Value);
                    }

                    if (_Edit._Time == DateTime.MinValue)
                    {
                        _Edit._Time = DateTime.Parse(History[i].Groups[5].Value);
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
                    if (NextEdit != null)
                    {
                        NextEdit.Prev = Core.NullEdit;
                        Page.FirstEdit = NextEdit;
                    }
                }
                Program.MainForm.Draw_History();
            }
            catch (Exception B)
            {
                Core.ExceptionHandler(B);
            }
        }

        /// <summary>
        /// Display edit in a browser on main form
        /// </summary>
        /// <param name="_edit"></param>
        /// <param name="BrowsingHistory"></param>
        /// <param name="browser"></param>
        public static void DisplayEdit(Edit _edit, bool BrowsingHistory = false, Controls.SpecialBrowser browser = null, bool ChangeEdit = true)
        {
            Core.History("Processing.DisplayEdit()");
            try
            {
                // in case that browser is null we use the currently selected browser
                if (browser == null)
                {
                    browser = main._CurrentBrowser;
                }
                // we will not proceed if edit is null
                if (_edit != null)
                {
                    // in case that page is not null we want to insert it to browsing history
                    if (_edit._Page != null)
                    {
                        if (BrowsingHistory != true && browser.History.Count == 0 || (browser.History[0]._Edit is Edit) == false)
                        {
                            browser.AddToHistory(new Core.HistoryItem(_edit));
                        }
                    }

                    // in case that browser is currently selected, we need to force update
                    if (main._CurrentBrowser == browser && ChangeEdit == true)
                    {
                        browser.Edit = _edit;
                        Program.MainForm.Set_Current_User(_edit._User);
                        Program.MainForm.Set_Current_Page(_edit._Page);
                    }

                    // I have no idea what is this
                    if (_edit._Deleted)
                    {

                    }
                    else if (_edit.Prev == Core.NullEdit)
                    {
                        // Load a html code from mediawiki
                        Requests.request_read.browser_html_data BrowserRequest = new Requests.request_read.browser_html_data();
                        BrowserRequest.address = Core.SitePath() + "index.php?title=" + System.Web.HttpUtility.UrlEncode(_edit._Page.Name) + "&id=" + _edit.Id;
                        BrowserRequest.browser = browser;
                        BrowserRequest.Start();
                    }
                    else
                    {
                        if (_edit.DiffCacheState == Edit.CacheState.Viewed || _edit.DiffCacheState == Edit.CacheState.Cached)
                        {
                            if (_edit.Diff != null)
                            {
                                string DocumentText = "", DiffText = "";
                                DiffText = _edit.Diff;

                                DiffText = DiffText.Replace("href=\"/wiki/", "href=\"" + Config.Projects[Config.Project] + "wiki/");
                                DiffText = DiffText.Replace("href='/wiki/", "href='" + Config.Projects[Config.Project] + "wiki/");
                                DiffText = DiffText.Replace("href=\"/w/", "href=\"" + Config.Projects[Config.Project] + "w/");
                                DiffText = DiffText.Replace("href='/w/", "href='" + Config.Projects[Config.Project] + "w/");

                                DocumentText = huggle3.Properties.Resources.header;
                                DocumentText += DiffText;
                                DocumentText += huggle3.Properties.Resources.footer;

                                // in case we are loading another page, kill it
                                browser.Stop();

                                browser.DocumentText = DocumentText;

                            }
                            _edit.DiffCacheState = Edit.CacheState.Viewed;
                        }
                    }
                    if (_edit.DiffCacheState == Edit.CacheState.Uncached)
                    {
                        _edit.DiffCacheState = Edit.CacheState.Caching;
                        Requests.request_read.diff Request = new Requests.request_read.diff();
                        Request._Edit = _edit;
                        Request.browsertab = browser;
                        Request.Start();
                    }
                    Program.MainForm.Refresh_Interface();
                }
                else
                {
                    Core.DebugLog("NULL edit");
                }
            }
            catch (Exception ex)
            {
                Core.ExceptionHandler(ex);
            }
        }


    }
}
