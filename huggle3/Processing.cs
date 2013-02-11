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
            if (edit.Time == DateTime.MinValue)
            {
                edit.Time = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            }

            if (edit.Oldid == null)
            { edit.Oldid = "prev"; }

            // if edit has a bot flag, the user is a bot
            if (edit.Bot == true)
            {
                if (edit.EditUser != null)
                {
                    edit.EditUser.Bot = true;
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
                    edit.Type = Edit.EditType.Blanked;
                }
            }

            // we check the same for redirect pattern

            if (Config.PageRedirectedPattern != null)
            {
                if (Config.PageRedirectedPattern.IsMatch(edit.Summary))
                {
                    edit.Type = Edit.EditType.Redirect;
                }
            }

            if (Config.PageReplacedPattern != null)
            {
                if (Config.PageReplacedPattern.IsMatch(edit.Summary) || (edit.Size >= 0 && edit.Change <= -200))
                {
                    edit.Type = Edit.EditType.ReplacedWith;
                }
            }

            if (Config.Summary != "" && edit.Summary.EndsWith(Config.Summary) && edit.Summary != "")
            {
                edit.Assisted = true;
            }

            foreach (string item in Config.AssistedSummaries)
            {
                if (edit.Summary.Contains(item))
                {
                    edit.Assisted = true;
                    break;
                }
            }

            // if user or page is not unknown object, we retrieve information for that as well

            if (edit.EditUser != null && edit._Page != null)
            {
                if (edit.NewPage)
                {
                    edit._Page.FirstEdit = edit;
                    edit.Prev = Core.NullEdit;
                }

                if (edit.Summary == Config.UndoSummary + " " + Config.Summary)
                {
                    edit.Type = Edit.EditType.Revert;
                }

                if (edit.Type == Edit.EditType.Revert && edit.Summary.ToLower().Contains("[[special:contributions/"))
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

                        if (RevertedUser != edit.EditUser && RevertedUser.User_Level == User.UserLevel.None)
                        {
                            RevertedUser.User_Level = User.UserLevel.Reverted;
                        }
                    }
                }

                if (edit.Next != null)
                {
                    if (edit.Next.Type == Edit.EditType.Revert && edit.EditUser.User_Level == User.UserLevel.None)
                    {
                        edit.EditUser.User_Level = User.UserLevel.Reverted;
                    }

                    if (edit.EditSpace == Space.UserTalk && edit._Page.IsSubpage)
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
        /// Process diff (the html version we are trying to parse) [obsolete]
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="diff"></param>
        /// <param name="browser"></param>
        public static void ProcessDiff(Edit edit, string diff, Controls.SpecialBrowser browser)
        {
            Core.History("Processing.Process_Diff()");
            try
            {
                if (edit.Multiple == false)
                {
                    if (diff.Contains("<span class=\"mw-rollback-link\">"))
                    {
                        // edit contains a rollback token, so we save it for later use
                        string RollbackToken = diff.Substring(diff.IndexOf("span class=\"mw-rollback-link\">"));
                        edit.RollbackToken = System.Web.HttpUtility.HtmlDecode(Core.FindString(RollbackToken, "<a href=\"", "&amp;token=", "\""));
                    }

                    if (edit.Id == "next" | edit.Id == "cur" && diff.Contains("<div id=\"mw-diff-ntitle1\">"))
                    {
                        // check the ID of edit
                        edit.Id = Core.FindString(diff, "<div id=\"mw-diff-ntitle1\"><strong><a", "oldid=", "'");
                    }

                    lock (Edit.All)
                    {
                        if (edit.Id != "cur" && Edit.All.ContainsKey(edit.Id))
                        {
                            // we already parsed an edit with this ID
                            edit = Edit.All[edit.Id];
                        }
                    }

                    if (edit.Oldid == "prev" && diff.Contains("<div id=\"mw-diff-otitle1\">"))
                    {
                        edit.Oldid = Core.FindString(diff, "<div id=\"mw-diff-otitle1\"><strong><a", "oldid=", "'");
                    }

                    if (diff.Contains("<div id=\"mw-diff-ntitle2\">") && edit.EditUser != null)
                    {
                        string D_User = Core.FindString(diff, "<div id=\"mw-diff-ntitle2\">", ">", "<");
                        D_User = System.Web.HttpUtility.HtmlDecode(D_User.Replace(" (page does not exist)", ""));
                        edit.EditUser = Core.GetUser(D_User);
                    }


                    if (edit.Prev == null)
                    {
                        edit.Prev = new Edit();
                        edit.Prev._Page = edit._Page;
                        edit.Prev.Next = edit;
                        edit.Prev.Id = edit.Oldid;
                        edit.Prev.Oldid = "prev";
                    }

                    if (diff.Contains("<div id=\"mw-diff-ntitle1\">") && edit.Time == DateTime.MinValue)
                    {
                        string et = Core.FindString(diff, "<div id=\"mw-diff-ntitle1\">", "</div>");

                        if (et.Contains("Revision as of"))
                        {
                            et = Core.FindString(et, "Revision as of");
                            if (DateTime.TryParse(et, out edit.Time))
                            {
                                edit.Time = DateTime.SpecifyKind(DateTime.Parse(et), DateTimeKind.Local).ToUniversalTime();
                            }
                        }
                        else if (et.Contains("Current revision"))
                        {
                            et = Core.FindString(et, "Current revision</a>", "(");
                            if (DateTime.TryParse(et, out edit.Time))
                            {
                                edit.Time = DateTime.SpecifyKind(DateTime.Parse(et), DateTimeKind.Local).ToUniversalTime();
                            }
                        }
                        if (diff.Contains("<div id=\"mw-diff-otitle1\">") && edit.Prev.Time == DateTime.MinValue)
                        {
                            string etime = Core.FindString(diff, "<div id=\"mw-diff-otitle1\">", "</div>");
                            etime = etime.Substring(etime.IndexOf(":") - 2);
                            if (DateTime.TryParse(etime, out edit.Time))
                            {
                                edit.Time = DateTime.SpecifyKind(DateTime.Parse(etime), DateTimeKind.Local).ToUniversalTime();
                            }
                        }
                    }
                    if (diff.Contains("<div id=\"mw-diff-otitle2\">") && edit.Prev.EditUser == null)
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
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        /// <summary>
        /// This function is used for processing of new edits
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        public static bool ProcessNewEdit(Edit edit)
        {
            Core.History("Processing.ProcessNewEdit( Edit )");
            if (edit.EditUser == null || edit._Page == null)
            {
                return false;
            }
            
            bool Redraw = false;

            // Update edit properties
            if (edit._Page.LastEdit != null)
            {
                edit.Prev.Next = edit;
                edit.Prev = edit._Page.LastEdit;

                if (edit.Prev.Size >= 0 && edit.Change != 0)
                {
                    edit.Size = edit.Prev.Size + edit.Change;
                }

                if (edit.Change != 0 && edit.Size >= 0)
                {
                    edit.Prev.Size = edit.Size - edit.Change;
                }
            }

            if (edit.EditUser == null && edit._Page.LastEdit != null)
            {
                edit.EditUser = edit._Page.LastEdit.EditUser;
            }

            if (edit.EditUser != null && edit.EditUser.LastEdit != null)
            {
                edit.PrevByUser = edit.EditUser.LastEdit;
                edit.PrevByUser.NextByUser = edit;
            }

            edit._Page.Exists = true;
            edit._Page.Text = null;
            edit._Page.SpeedyCrit = null;
            edit._Page.LastEdit = edit;
            edit.EditUser.LastEdit = edit;

            if (Variables.CustomReverts.ContainsKey(edit._Page))
            {
                if (edit.EditUser.Me)
            }

            /*
             * 
             If CustomReverts.ContainsKey(Edit.Page) Then
            If Edit.User.IsMe AndAlso Edit.Summary = CustomReverts(Edit.Page) Then Edit.Type = EditType.Revert
            CustomReverts.Remove(Edit.Page)
        End If

        'Update statistics and edit counts
        Stats.Update(Edit)
        Edit.User.SessionEditCount += 1
        If Edit.User.EditCount > -1 Then Edit.User.EditCount += 1

        'Add edit to the all lists
        For Each Queue As Queue In Queue.All.Values
            If Queue.MatchesFilter(Edit) Then
                If Queue.RevisionRegex IsNot Nothing AndAlso Queue.DiffMode = DiffMode.All Then
                    If Edit.DiffCacheState = Edit.CacheState.Uncached Then
                        Edit.DiffCacheState = Huggle.Edit.CacheState.Caching

                        Dim NewRequest As New DiffRequest
                        NewRequest.Edit = Edit
                        NewRequest.Start()
                    End If
                Else
                    Queue.AddEdit(Edit)

                    If Queue Is CurrentQueue Then
                        'Keep user's viewing position when adding new items, if not looking at the top of the queue
                        If (Queue.SortOrder = QueueSortOrder.Time And (MainForm.QueueScroll.Value) < MainForm.QueueScroll.Maximum And MainForm.QueueScroll.Value > 1) Then MainForm.QueueScroll.Value += 1
                        Redraw = True
                    End If
                End If
            End If
        Next Queue

        'Issue warnings
        Dim i As Integer = 0
        Dim Break As Integer = 0
        While i < PendingWarnings.Count
            If PendingWarnings(i).Page Is Edit.Page Then
                If Edit.User.IsMe Then
                    Dim Last As Edit = PendingWarnings(i).User.TalkPage.LastEdit

                    If Last IsNot Nothing AndAlso Last.Time.AddSeconds(Config.MinWarningWait) > Date.UtcNow Then
                        'Do nothing if there is a very recent warning to try to compensate for
                        'stupid broken tools that warn for other people's reverts *cough* vandalproof *cough*

                        Log(Msg("warn-fail", PendingWarnings(i).User.Name) & ": " & Msg("warn-recent", _
                            CStr(Config.MinWarningWait)))
                    Else

                        Dim NewWarningRequest As New WarningRequest
                        NewWarningRequest.Edit = PendingWarnings(i)
                        NewWarningRequest.Start()
                    End If
                End If

                PendingWarnings.RemoveAt(i)
            Else
                i += 1
            End If
        End While

        'Refresh undo information
        For Each Item As Command In Undo
            If Item.Edit IsNot Nothing AndAlso Item.Edit.Page Is Edit.Page Then
                MainForm.RemoveFromUndoList(Item)
                Exit For
            End If
        Next Item
        'Remove in-progress log entries
        Dim j As Integer = 0
        Break = 0
        While j < MainForm.Status.Items.Count And Break < Misc.GlExcess
            If TypeOf MainForm.Status.Items(j).Tag Is Page AndAlso CType(MainForm.Status.Items(j).Tag, Page) _
                Is Edit.Page Then MainForm.Status.Items.RemoveAt(j) Else j += 1
            Break = Break + 1
        End While

        If Edit.User.IsMe Then
            'Log user's edits
            If Edit.Summary = "" Then Log("Edited '" & Edit.Page.Name & "': (no summary)", Edit) _
                Else Log("Edited '" & Edit.Page.Name & "': " & TrimSummary(Edit.Summary), Edit)

            'Add undo information
            Dim NewCommand As New Command

            NewCommand.Edit = Edit

            Select Case Edit.Type
                Case EditType.Warning
                    NewCommand.Type = CommandType.Warning
                    NewCommand.Description = "Warn " & Edit.Page.Name.Substring(10)

                Case EditType.Revert
                    NewCommand.Type = CommandType.Revert
                    NewCommand.Description = "Revert on " & Edit.Page.Name

                Case EditType.Report
                    NewCommand.Type = CommandType.Report
                    NewCommand.Description = "Report " & TrimSummary(Edit.Summary).Substring(10)

                Case Else
                    NewCommand.Type = CommandType.Edit
                    NewCommand.Description = "Edit " & Edit.Page.Name
            End Select

            If MainForm IsNot Nothing Then MainForm.AddToUndoList(NewCommand)
        End If

        'Check for new messages
        If Edit.Page Is User.Me.TalkPage AndAlso Not Edit.Bot Then
            MainForm.SystemMessages.Enabled = Not Edit.User.IsMe
            If MainForm.SystemMessages.Enabled AndAlso Config.TrayIcon Then MainForm.TrayIcon.ShowBalloonTip(10000)
        End If

        'Warnings
        If Edit.Page.Space Is Space.UserTalk AndAlso Not Edit.Page.IsSubpage Then
            Dim PageOwner As User = GetUser(Edit.Page.Name.Substring(10))
            Dim WarningLevel As UserLevel = GetUserLevelFromSummary(Edit)

            If PageOwner IsNot Nothing Then
                If Edit.User.IsMe AndAlso PageOwner.WarningsCurrent AndAlso WarningLevel >= UserLevel.Warning Then

                    'Add our own warnings straight to the list
                    Dim NewWarning As New Warning

                    NewWarning.Level = WarningLevel
                    NewWarning.Time = Edit.Time
                    NewWarning.Type = "huggle"
                    NewWarning.User = User.Me

                    If PageOwner.Warnings Is Nothing Then PageOwner.Warnings = New List(Of Warning)
                    PageOwner.Warnings.Add(NewWarning)
                    PageOwner.Warnings.Sort(AddressOf SortWarningsByDate)
                Else
                    'Even if we can get the level of others, we can rarely even guess at the type
                    PageOwner.WarningsCurrent = False
                End If

                'Refresh any open user info form
                For Each Item As Form In Application.OpenForms
                    Dim uif As UserInfoForm = TryCast(Item, UserInfoForm)

                    If uif IsNot Nothing AndAlso uif.User Is PageOwner Then uif.RefreshWarnings()
                Next Item
            End If
        End If

        'Get edit counts for non-whitelisted registered users, in batches, and whitelist if appropriate
        If Config.AutoWhitelist AndAlso Not Edit.User.Anonymous AndAlso Not Edit.User.Ignored _
            AndAlso Not Edit.User.EditCount > 0 Then

            NextCount.Add(Edit.User)

            'If the list has more than "CountBatchSize" entrys then...
            If NextCount.Count >= Config.CountBatchSize Then
                Dim NewCountRequest As New CountRequest
                NewCountRequest.Users.AddRange(NextCount)
                NewCountRequest.Start()
                NextCount.Clear()
            End If
        End If

        'Preload diffs
        If CurrentQueue IsNot Nothing AndAlso CurrentQueue.DiffMode = DiffMode.Preload _
            AndAlso DiffRequest.PreloadCount < Config.Preloads + 1 Then

            For k As Integer = 0 To Math.Min(CurrentQueue.Edits.Count, Config.Preloads) - 1
                If CurrentQueue.Edits(k).DiffCacheState = Edit.CacheState.Uncached Then
                    CurrentQueue.Edits(k).DiffCacheState = Huggle.Edit.CacheState.Caching

                    Dim NewRequest As New DiffRequest
                    NewRequest.Edit = CurrentQueue.Edits(k)
                    NewRequest.Start()

                    DiffRequest.PreloadCount += 1
                    If DiffRequest.PreloadCount >= Config.Preloads Then Exit For
                End If
            Next k
        End If

        'Refresh the interface
        If MainForm IsNot Nothing AndAlso MainForm.Visible Then
            If CurrentEdit IsNot Nothing AndAlso CurrentPage IsNot Nothing AndAlso CurrentUser IsNot Nothing AndAlso _
                (Edit.Page Is CurrentPage OrElse Edit.User Is CurrentUser OrElse Edit.Page Is CurrentUser.TalkPage) Then

                MainForm.DrawHistory()
                MainForm.DrawContribs()
            End If

            If Config.ShowQueue AndAlso Redraw Then MainForm.DrawQueues()



            For Each Item As TabPage In MainForm.Tabs.TabPages
                Dim ThisTab As BrowserTab = CType(Item.Controls(0), BrowserTab)

                If ThisTab.Edit IsNot Nothing Then
                    If ThisTab.Edit.Page Is Edit.Page AndAlso ThisTab.ShowNewEdits Then
                        'Show new edits to page
                        DisplayEdit(Edit, False, ThisTab, Not Edit.User.IsMe)

                        If ThisTab Is CurrentTab Then
                            MainForm.RevertB.Enabled = False
                            MainForm.RevertWarnB.Enabled = False
                            MainForm.Reverting = False
                            MainForm.RevertTimer.Stop()
                            MainForm.RevertTimer.Interval = 3000
                            MainForm.RevertTimer.Start()
                        Else
                            ThisTab.Highlight = True
                        End If

                    ElseIf ThisTab.Edit.User Is Edit.User AndAlso ThisTab.ShowNewContribs Then
                        'Show new contribs by user
                        DisplayEdit(Edit, False, ThisTab)

                        If ThisTab Is CurrentTab Then
                            MainForm.RevertB.Enabled = False
                            MainForm.RevertWarnB.Enabled = False
                            MainForm.RevertTimer.Start()
                        Else
                            ThisTab.Highlight = True
                        End If
                    End If
                End If
            Next Item
        End If 
             * 
             */



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
                LastUser = Edit._Page.LastEdit.EditUser;
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
                        _Edit.Text = System.Web.HttpUtility.HtmlDecode(History[i].Groups[9].Value);
                    }

                    _Edit.EditUser = Core.GetUser(System.Web.HttpUtility.HtmlDecode(History[i].Groups[3].Value));

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
                        Program.MainForm.Set_Current_User(_edit.EditUser);
                        Program.MainForm.Set_Current_Page(_edit._Page);
                    }

                    // I have no idea what is this
                    if (_edit.Deleted)
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
