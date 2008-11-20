Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Module Processing

    Private RcLineRegex As New Regex _
        ("type=""([^""]*)"" ns=""[^""]*"" title=""([^""]*)"" rcid=""([^""]*)"" pageid=""[^""]*"" " _
        & "revid=""([^""]*)"" old_revid=""([^""]*)"" user=""([^""]*)""( bot="""")?( anon=" _
        & """"")?( new="""")?( minor="""")? oldlen=""([^""]*)"" newlen=""([^""]*)"" times" _
        & "tamp=""([^""]*)""( comment=""([^""]*)"")? />", RegexOptions.Compiled)

    Sub ProcessEdit(ByVal Edit As Edit)
        If Edit Is Nothing Then Exit Sub

        If Edit.Time = Date.MinValue Then Edit.Time = Date.SpecifyKind(Date.UtcNow, DateTimeKind.Utc)
        If Edit.Oldid Is Nothing Then Edit.Oldid = "prev"
        If Edit.Bot Then Edit.User.Bot = True

        'Auto summaries
        If (Config.PageBlankedPattern IsNot Nothing AndAlso Config.PageBlankedPattern.IsMatch(Edit.Summary)) _
            OrElse Edit.Size = 0 Then Edit.Type = EditType.Blanked
        If Config.PageRedirectedPattern IsNot Nothing AndAlso Config.PageRedirectedPattern.IsMatch(Edit.Summary) _
            Then Edit.Type = EditType.Redirect
        If (Config.PageReplacedPattern IsNot Nothing AndAlso Config.PageReplacedPattern.IsMatch(Edit.Summary)) _
            OrElse (Edit.Size >= 0 AndAlso Edit.Size <= 200 AndAlso Edit.Change <= -200) _
            Then Edit.Type = EditType.ReplacedWith

        'Assisted summaries
        If Config.Summary IsNot Nothing AndAlso Edit.Summary.EndsWith(Config.Summary) Then Edit.Assisted = True

        For Each Item As String In Config.AssistedSummaries
            If Edit.Summary.Contains(Item) Then
                Edit.Assisted = True
                Exit For
            End If
        Next Item

        If Edit.User IsNot Nothing AndAlso Edit.Page IsNot Nothing Then
            If Edit.NewPage Then
                Edit.Page.FirstEdit = Edit
                Edit.Prev = NullEdit
            End If

            'Reverts
            If Edit.User.Level < UserLevel.Warning Then
                For Each Item As Regex In Config.RevertPatterns
                    If Item.IsMatch(TrimSummary(Edit.Summary)) Then
                        Edit.Type = EditType.Revert

                        If Edit.Page.Level = PageLevel.None Then Edit.Page.Level = PageLevel.Watch

                        If Edit.Prev IsNot Nothing AndAlso Edit.Prev.User IsNot Nothing _
                            AndAlso Edit.Prev.User IsNot Edit.User AndAlso Edit.Prev.User.Level = UserLevel.None _
                            Then Edit.Prev.User.Level = UserLevel.Reverted

                        Exit For
                    End If
                Next Item
            End If

            If Edit.Summary = Config.UndoSummary & " " & Config.Summary Then Edit.Type = EditType.Revert

            'Reverted users
            If Edit.Type = EditType.Revert AndAlso Edit.Summary.ToLower.Contains("[[special:contributions/") Then
                Dim Username As String = Edit.Summary.Substring(Edit.Summary.ToLower.IndexOf _
                    ("[[special:contributions/") + 24)

                If Username.Contains("]]") OrElse Username.Contains("|") Then
                    If Username.Contains("]]") Then Username = Username.Substring(0, Username.IndexOf("]]"))
                    If Username.Contains("|") Then Username = Username.Substring(0, Username.IndexOf("|"))
                    Username = HtmlDecode(Username)

                    Dim RevertedUser As User = GetUser(Username)

                    If (RevertedUser IsNot Edit.User) AndAlso RevertedUser.Level = UserLevel.None _
                        Then RevertedUser.Level = UserLevel.Reverted
                End If
            End If

            'Reverted edits
            If Edit.Next IsNot Nothing AndAlso Edit.Next.Type = EditType.Revert _
                AndAlso Edit.User.Level = UserLevel.None Then Edit.User.Level = UserLevel.Reverted

            'Warnings / block notifications
            If Edit.Page.Space Is Space.UserTalk AndAlso Not Edit.Page.IsSubpage Then
                Dim SummaryLevel As UserLevel = GetUserLevelFromSummary(Edit)

                If SummaryLevel >= UserLevel.Warning AndAlso Not Edit.Page.Owner.Ignored _
                    AndAlso Edit.Time.AddHours(Config.WarningAge) > Date.UtcNow Then

                    Edit.Type = EditType.Warning
                    Edit.WarningLevel = SummaryLevel
                    If Edit.User.WarnTime < Edit.Time Then Edit.User.WarnTime = Edit.Time

                    If Edit.Page.Owner.Level < SummaryLevel AndAlso SummaryLevel < UserLevel.Warn4im _
                        Then Edit.Page.Owner.Level = SummaryLevel

                ElseIf SummaryLevel = UserLevel.Notification Then
                    Edit.Type = EditType.Notification

                    If Edit.Page.Owner.Level < SummaryLevel AndAlso Not Edit.Page.Owner.Ignored _
                        Then Edit.Page.Owner.Level = SummaryLevel
                End If
            End If

            'AIV/UAA reports
            If Edit.Page.Name = Config.AIVLocation OrElse Edit.Page.Name = Config.UAALocation Then

                If Edit.Summary.Contains("User-reported") _
                    AndAlso Not (Edit.Summary.Contains(" rm ") OrElse Edit.Summary.Contains("remove")) Then
                    Edit.Type = EditType.Report

                    If Edit.Summary.Contains("User-reported - ") _
                        OrElse Edit.Summary.Contains("User-reported */ ") Then

                        Dim Summary As String = ""

                        If Edit.Summary.Contains("User-reported - ") _
                            Then Summary = Edit.Summary.Substring(Edit.Summary.IndexOf("User-reported - ") + 16)
                        If Edit.Summary.Contains("User-reported */ ") _
                            Then Summary = Edit.Summary.Substring(Edit.Summary.IndexOf("User-reported */ ") + 17)

                        If Summary.ToLower.StartsWith("[[special:contributions/") Then
                            Summary = Summary.Substring(22)
                            If Summary.Contains("|") Then Summary = Summary.Substring(0, Summary.IndexOf("|"))
                            If Summary.Contains("]") Then Summary = Summary.Substring(0, Summary.IndexOf("]"))
                            Summary = HtmlDecode(Summary)

                            Dim ReportedUser As User = GetUser(Summary)

                            If Not ReportedUser.Ignored Then
                                If Edit.Page.Name = Config.AIVLocation AndAlso ReportedUser.Level < UserLevel.ReportedAIV _
                                    Then ReportedUser.Level = UserLevel.ReportedAIV
                                If Edit.Page.Name = Config.UAALocation AndAlso ReportedUser.Level < UserLevel.ReportedUAA _
                                    Then ReportedUser.Level = UserLevel.ReportedUAA
                            End If

                        Else
                            If Summary.EndsWith(" reported") _
                                Then Summary = Summary.Substring(0, Summary.IndexOf(" reported"))

                            Dim ReportedUser As User = GetUser(Summary)

                            If ReportedUser IsNot Nothing AndAlso ReportedUser.Anonymous AndAlso _
                                ReportedUser.Level < UserLevel.ReportedUAA AndAlso Not ReportedUser.Ignored Then

                                If Edit.Page.Name = Config.AIVLocation Then ReportedUser.Level = UserLevel.ReportedAIV _
                                    Else ReportedUser.Level = UserLevel.ReportedUAA
                            End If
                        End If
                    End If
                End If

                If Edit.Summary.ToLower.StartsWith("reporting [[special:contributions/") _
                    OrElse Edit.Summary.ToLower.StartsWith("extending report for ") Then

                    Dim Summary As String = Edit.Summary.Substring(34)
                    If Summary.Contains("|") Then Summary = Summary.Substring(0, Summary.IndexOf("|"))
                    If Summary.Contains("]") Then Summary = Summary.Substring(0, Summary.IndexOf("]"))
                    Summary = HtmlDecode(Summary)

                    Dim ReportedUser As User = GetUser(Summary)

                    If ReportedUser.Level < UserLevel.ReportedAIV AndAlso Not ReportedUser.Ignored Then _
                        ReportedUser.Level = UserLevel.ReportedAIV

                    Edit.Type = EditType.Report

                ElseIf Edit.Summary.ToLower.StartsWith("reporting ") AndAlso Edit.Summary.Length > 10 Then
                    Dim ReportedUser As User = GetUser(Edit.Summary.Substring(10))

                    If ReportedUser.Level < UserLevel.ReportedAIV AndAlso Not ReportedUser.Ignored Then _
                        ReportedUser.Level = UserLevel.ReportedAIV

                    Edit.Type = EditType.Report
                End If

                If Edit.Summary.ToLower.StartsWith("added report") _
                    OrElse Edit.Summary.ToLower.StartsWith("reporting") Then Edit.Type = EditType.Report
            End If

            'Bot AIV reports
            If Edit.Page.Name = Config.AIVBotLocation Then

                If Edit.Summary.ToLower.StartsWith("automatically reporting") _
                    AndAlso Edit.Summary.Contains(". (bot)") Then

                    'ClueBot
                    Dim UserName As String = Edit.Summary.Substring(Edit.Summary.IndexOf("/") + 1)
                    If UserName.Contains("/") Then UserName = UserName.Substring(UserName.IndexOf("/") + 1)

                    UserName = UserName.Substring(0, UserName.IndexOf(". (bot)")).Replace("[", "").Replace("]", "")
                    UserName = HtmlDecode(UserName)

                    Dim ReportedUser As User = GetUser(UserName)

                    If ReportedUser.Level < UserLevel.ReportedAIV AndAlso Not ReportedUser.Ignored _
                        Then ReportedUser.Level = UserLevel.ReportedAIV

                    Edit.Type = EditType.Report

                ElseIf Edit.Summary.ToLower.StartsWith("bot - reporting apparent vandalism by ") Then

                    'VoaBot II
                    Dim UserName As String = Edit.Summary.Substring(45)
                    If UserName.Contains("|") Then UserName = UserName.Substring(UserName.IndexOf("|") + 6)
                    If UserName.Contains("]]") Then UserName = UserName.Substring(0, UserName.IndexOf("]]"))

                    Dim ReportedUser As User = GetUser(UserName)

                    If ReportedUser.Level < UserLevel.ReportedAIV AndAlso Not ReportedUser.Ignored _
                        Then ReportedUser.Level = UserLevel.ReportedAIV

                    Edit.Type = EditType.Report
                End If
            End If

            'Bot UAA reports
            If Edit.Page.Name = Config.UAABotLocation Then
                If Edit.Summary.ToLower.StartsWith("reporting [[special:contributions/") _
                    OrElse Edit.Summary.ToLower.StartsWith("extending report for ") Then

                    Dim UserName As String = Edit.Summary.Substring(34)
                    If UserName.Contains("|") Then UserName = UserName.Substring(0, UserName.IndexOf("|"))
                    If UserName.Contains("]") Then UserName = UserName.Substring(0, UserName.IndexOf("]"))
                    UserName = HtmlDecode(UserName)

                    Dim ReportedUser As User = GetUser(UserName)

                    If ReportedUser.Level < UserLevel.ReportedUAA AndAlso Not ReportedUser.Ignored Then _
                        ReportedUser.Level = UserLevel.ReportedUAA

                    Edit.Type = EditType.Report
                End If
            End If

            'Tagging
            If IsTagFromSummary(Edit) AndAlso Edit.Type = EditType.None Then Edit.Type = EditType.Tag
        End If

        If Edit.Id IsNot Nothing AndAlso Not Edit.All.ContainsKey(Edit.Id) Then Edit.All.Add(Edit.Id, Edit)
        Edit.Processed = True
    End Sub

    Sub ProcessNewEdit(ByVal Edit As Edit)
        If Edit.User Is Nothing OrElse Edit.Page Is Nothing OrElse MainForm Is Nothing Then Exit Sub

        Dim Redraw As Boolean

        'Update edit properties
        If Edit.Page.LastEdit IsNot Nothing Then
            Edit.Prev = Edit.Page.LastEdit
            Edit.Prev.Next = Edit

            If Edit.Prev.Size >= 0 AndAlso Edit.Change <> 0 Then Edit.Size = Edit.Prev.Size + Edit.Change
            If Edit.Change <> 0 AndAlso Edit.Size >= 0 Then Edit.Prev.Size = Edit.Size - Edit.Change
        End If

        If Edit.User.LastEdit IsNot Nothing Then
            Edit.PrevByUser = Edit.User.LastEdit
            Edit.PrevByUser.NextByUser = Edit
        End If

        Edit.Page.Exists = True
        Edit.Page.Text = Nothing
        Edit.Page.SpeedyCriterion = Nothing
        Edit.Page.LastEdit = Edit
        Edit.User.LastEdit = Edit

        If CustomReverts.ContainsKey(Edit.Page) Then
            If Edit.User.IsMe AndAlso Edit.Summary = CustomReverts(Edit.Page) Then Edit.Type = EditType.Revert
            CustomReverts.Remove(Edit.Page)
        End If

        'Update statistics and edit counts
        Stats.Update(Edit)
        Edit.User.SessionEditCount += 1
        If Edit.User.EditCount > -1 Then Edit.User.EditCount += 1

        'Add edit to queues
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
                        If Queue.SortOrder = QueueSortOrder.Time AndAlso MainForm.QueueScroll.Value > 0 _
                            Then MainForm.QueueScroll.Value += 1
                        Redraw = True
                    End If
                End If
            End If
        Next Queue

        'Issue warnings
        Dim i As Integer = 0

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

        While j < MainForm.Status.Items.Count
            If TypeOf MainForm.Status.Items(j).Tag Is Page AndAlso CType(MainForm.Status.Items(j).Tag, Page) _
                Is Edit.Page Then MainForm.Status.Items.RemoveAt(j) Else j += 1
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
    End Sub

    Sub UserDeleteRequest(ByVal Page As Page)
        If Config.Rights.Contains("delete") Then
            Dim NewDeleteForm As New DeleteForm
            NewDeleteForm.Page = Page
            NewDeleteForm.Reason.Text = Config.SpeedySummary.Replace("$1", "[[WP:SD#G7|requested by page creator]]")
            NewDeleteForm.ShowDialog()
        Else
            Dim NewTagRequest As New TagRequest
            NewTagRequest.Page = Page
            NewTagRequest.Tag = "{{db-g7}}"
            NewTagRequest.Summary = Config.SpeedySummary.Replace("$1", "[[WP:SD#G7|requested by page creator]]")
            NewTagRequest.Start()
        End If
    End Sub

    Function DoRevert(ByVal Edit As Edit, Optional ByVal Summary As String = Nothing, _
        Optional ByVal Rollback As Boolean = True, Optional ByVal Undoing As Boolean = False, _
        Optional ByVal CurrentOnly As Boolean = False) As Boolean

        If Edit Is Nothing OrElse Edit.Page Is Nothing OrElse Edit.User Is Nothing Then Return False

        Dim LastEditor As User
        If Edit.Page.LastEdit IsNot Nothing Then LastEditor = Edit.Page.LastEdit.User Else LastEditor = Nothing

        'Confirm self-reversion
        If Config.ConfirmSelfRevert AndAlso Not Undoing AndAlso Edit.User.IsMe _
            AndAlso (Edit.Page.FirstEdit Is Nothing OrElse Edit.Id <> Edit.Page.FirstEdit.Id) _
            AndAlso MessageBox.Show(Msg("revert-confirm-self"), "Huggle", MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Return False

        'Confirm reversion of whitelisted user
        If Config.ConfirmIgnored AndAlso Edit.User.Ignored AndAlso Not Edit.User.IsMe AndAlso _
            MessageBox.Show(Msg("revert-confirm-ignored", Edit.User.Name), "Huggle", MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Return False

        'Confirm reversion of multiple edits
        If Config.ConfirmMultiple AndAlso Edit.Prev IsNot Nothing AndAlso Edit.User Is Edit.Prev.User _
            AndAlso MessageBox.Show(Msg("revert-confirm-multiple", Edit.User.Name), "Huggle", _
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return False

        If Not Undoing AndAlso Edit.User.Level = UserLevel.None Then Edit.User.Level = UserLevel.Reverted
        If Not Undoing AndAlso Edit.Page.Level = PageLevel.None Then Edit.Page.Level = PageLevel.Watch

        'If reverting first edit to user talk page, blank it
        If Edit.Page.FirstEdit IsNot Nothing AndAlso Edit.Id = Edit.Page.FirstEdit.Id _
            AndAlso Edit.Page.Space Is Space.UserTalk Then

            Dim NewRequest As New EditRequest
            NewRequest.Text = ""
            NewRequest.Page = Edit.Page
            NewRequest.Minor = Config.Minor("revert")
            NewRequest.Watch = Config.Watch("revert")
            If Undoing Then NewRequest.Summary = Config.UndoSummary Else NewRequest.Summary = _
                "Revert edit by [[Special:Contributions/" & Config.Username & "|" & Config.Username & "]]"
            NewRequest.Start()
            Return False
        End If

        'If reverting first edit to page, offer to tag for speedy deletion
        If Edit.Page.FirstEdit IsNot Nothing AndAlso Edit.Id = Edit.Page.FirstEdit.Id Then
            If Not Config.Speedy Then
                MessageBox.Show(Msg("revert-error-first"))
                Return False
            End If

            Dim Prompt As String = Msg("revert-only") & " "

            If Config.Rights.Contains("delete") _
                Then Prompt &= Msg("revert-delete-instead") Else Prompt &= Msg("revert-speedy-instead")
            If MessageBox.Show(Prompt, "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes _
            Then UserDeleteRequest(Edit.Page)

            Return False
        End If

        'If reverting page creator, offer to tag for speedy deletion
        If Edit.Page.FirstEdit IsNot Nothing AndAlso Config.Speedy AndAlso Edit.User Is Edit.Page.FirstEdit.User Then

            Dim Prompt As String = Msg("revert-creator", Edit.User.Name) & " "
            If Config.Rights.Contains("delete") _
                Then Prompt &= Msg("revert-delete-instead") Else Prompt &= Msg("revert-speedy-instead")

            Select Case MessageBox.Show(Prompt, "Huggle", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                Case DialogResult.Yes
                    UserDeleteRequest(Edit.Page)
                    Return False

                Case DialogResult.Cancel
                    Return False
            End Select
        End If

        'Confirm revert to revision by a warned user
        If Config.ConfirmWarned AndAlso Not Undoing AndAlso Edit.Prev IsNot Nothing AndAlso Edit.Prev.User IsNot Nothing _
            AndAlso Edit.Prev.User IsNot Edit.User AndAlso Edit.Prev.User.Level >= UserLevel.Warn1 AndAlso _
            MessageBox.Show(Msg("revert-confirm-warned", Edit.User.Name), "Huggle", MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Return False

        'Confirm revert to revision by anonymous user in same /16 block as user being reverted
        If Config.ConfirmRange AndAlso Not Undoing AndAlso Edit.Prev IsNot Nothing AndAlso Edit.Prev.User IsNot Nothing _
            AndAlso Edit.Prev.User.Anonymous AndAlso Edit.User.Anonymous AndAlso Edit.Prev.User IsNot Edit.User AndAlso _
            Edit.Prev.User.Range = Edit.User.Range AndAlso MessageBox.Show(Msg("revert-confirm-range", _
            Edit.User.Name, Edit.Prev.User.Name), "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
            MessageBoxDefaultButton.Button2) = DialogResult.No Then Return False

        'Confirm revert of ignored page
        If Config.ConfirmPage AndAlso Not Undoing AndAlso Config.IgnoredPages.Contains(Edit.Page.Name) AndAlso _
            MessageBox.Show(Msg("revert-confirm-page", Edit.Page.Name), "Huggle", MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Return False

        If Not Edit.User.Ignored AndAlso Not Edit.User.IsMe AndAlso Not Edit.User.RecentContribsRetrieved Then
            Edit.User.RecentContribsRetrieved = True

            Dim NewRequest As New ContribsRequest
            NewRequest.BlockSize = 10
            NewRequest.User = Edit.User
            NewRequest.Start()
        End If

        'Use rollback if possible
        If Rollback AndAlso Config.Rights.Contains("rollback") AndAlso Config.UseRollback AndAlso Not Undoing _
            AndAlso (Edit Is Edit.Page.LastEdit) AndAlso (Edit.RollbackToken IsNot Nothing) _
            AndAlso Not (CurrentOnly AndAlso (Edit.Prev Is Nothing OrElse Edit.User Is Edit.Prev.User)) Then

            If Edit Is CurrentEdit Then MainForm.StartRevert()

            Dim NewRequest As New RollbackRequest
            NewRequest.Edit = Edit
            NewRequest.Summary = Summary
            NewRequest.Start()
            Return True
        End If

        'Revert all edits by the last editor of the page, if possible
        If Edit Is Edit.Page.LastEdit AndAlso Not Undoing _
            AndAlso Not (CurrentOnly AndAlso (Edit.Prev Is Nothing OrElse Edit.User Is Edit.Prev.User)) Then

            If Edit Is CurrentEdit Then MainForm.StartRevert()

            Dim NewRequest As New FakeRollbackRequest
            NewRequest.Page = Edit.Page
            NewRequest.ExcludeUser = Edit.User
            NewRequest.LastUser = LastEditor
            NewRequest.Summary = Summary
            NewRequest.Start()
            Return True
        End If

        'Confirm revert to another revision by the same user
        If Not Undoing AndAlso Edit.Prev IsNot Nothing AndAlso Edit.Prev.User Is Edit.User _
            AndAlso Config.ConfirmSame AndAlso Edit.User IsNot User.Me AndAlso _
            MessageBox.Show(Msg("revert-confirm-same", Edit.User.Name), "Huggle", MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Return False

        If Edit Is CurrentEdit Then MainForm.StartRevert()

        If CurrentOnly AndAlso Edit IsNot Edit.Page.LastEdit Then
            'Use 'undo' with single edit if necessary
            Dim NewRequest As New UndoRequest
            NewRequest.Edit = Edit
            NewRequest.Summary = Summary
            NewRequest.Start()
            Return True
        Else
            'Plain old reversion
            Dim NewRequest As New RevertRequest
            NewRequest.Edit = Edit.Prev
            NewRequest.LastUser = LastEditor
            NewRequest.Summary = Summary
            NewRequest.Start()
            Return True
        End If
    End Function

    Sub ProcessBlock(ByVal BlockObject As Object)
        Dim Block As Block = CType(BlockObject, Block)

        If Block IsNot Nothing AndAlso Block.User IsNot Nothing Then
            Stats.Update(Block)

            If Block.User.BlocksCurrent Then
                If Block.User.Blocks Is Nothing Then Block.User.Blocks = New List(Of Block)
                Block.User.Blocks.Insert(0, Block)
            End If

            If Block.Action = "block" Then
                If Not Block.User.Ignored Then Block.User.Level = UserLevel.Blocked
            Else
                Block.User.Level = UserLevel.None
            End If

            'Refresh any open user info form
            For Each Item As Form In Application.OpenForms
                Dim uif As UserInfoForm = TryCast(Item, UserInfoForm)

                If uif IsNot Nothing AndAlso uif.User Is Block.User Then
                    uif.RefreshBlocks()
                End If
            Next Item
        End If
    End Sub

    Sub ProcessDelete(ByVal DeleteObject As Object)
        Dim Delete As Delete = CType(DeleteObject, Delete)

        If Delete IsNot Nothing AndAlso Delete.Page IsNot Nothing Then
            Stats.Update(Delete)

            Delete.Page.Exists = False

            If Delete.Page.DeletesCurrent Then
                If Delete.Page.Deletes Is Nothing Then Delete.Page.Deletes = New List(Of Delete)
                Delete.Page.Deletes.Insert(0, Delete)
            End If

            'Remove page's edits from user contribs and queues
            For Each Edit As Edit In Delete.Page.Edits
                Edit.Deleted = True

                If Edit.PrevByUser IsNot Nothing Then Edit.PrevByUser.NextByUser = Edit.NextByUser
                If Edit.NextByUser IsNot Nothing Then Edit.NextByUser.PrevByUser = Edit.PrevByUser

                For Each Item As Queue In Queue.All.Values
                    Item.RemoveEdit(Edit)
                Next Item
            Next Edit

            'Refresh any browser tab in which a revision to the page is being viewed
            If MainForm IsNot Nothing Then
                For Each Item As TabPage In MainForm.Tabs.TabPages
                    Dim Tab As BrowserTab = CType(Item.Controls(0), BrowserTab)

                    If Tab.ShowNewEdits AndAlso Tab.Edit IsNot Nothing _
                        AndAlso Tab.Edit.Page Is Delete.Page Then DisplayEdit(Tab.Edit, False, Tab)
                Next Item
            End If
        End If
    End Sub

    Sub ProcessRestore(ByVal DeleteObject As Object)
        Dim Restore As Delete = CType(DeleteObject, Delete)

        If Restore IsNot Nothing AndAlso Restore.Page IsNot Nothing Then
            If Restore.Page.DeletesCurrent Then
                If Restore.Page.Deletes Is Nothing Then Restore.Page.Deletes = New List(Of Delete)
                Restore.Page.Deletes.Insert(0, Restore)
            End If
        End If
    End Sub

    Sub ProcessPageMove(ByVal PageMoveObject As Object)
        Dim PageMove As PageMove = CType(PageMoveObject, PageMove)

        GetPage(PageMove.Source).MovedTo(PageMove.Destination)

        'Page move leaves a redirect behind
        Dim RedirectEdit As New Edit

        RedirectEdit.Type = EditType.Redirect
        RedirectEdit.User = PageMove.User
        RedirectEdit.Prev = NullEdit

        GetPage(PageMove.Source).FirstEdit = RedirectEdit
        GetPage(PageMove.Source).LastEdit = RedirectEdit
    End Sub

    Sub ProcessProtection(ByVal ProtectionObject As Object)
        Dim Protection As Protection = CType(ProtectionObject, Protection)

        If Protection IsNot Nothing AndAlso Protection.Page IsNot Nothing Then
            Stats.Update(Protection)

            Dim ThisPage As Page = Protection.Page

            If Protection.Page.ProtectionsCurrent Then
                If Protection.Page.Protections Is Nothing _
                    Then Protection.Page.Protections = New List(Of Protection)
                Protection.Page.Protections.Insert(0, Protection)
            End If

            ThisPage.EditLevel = Protection.EditLevel
            ThisPage.MoveLevel = Protection.MoveLevel
        End If
    End Sub

    Sub ProcessUpload(ByVal UploadObject As Object)
    End Sub

    Sub ProcessDiff(ByVal Edit As Edit, ByVal DiffText As String, ByVal Tab As BrowserTab)

        If Not Edit.Multiple Then
            If DiffText.Contains("<span class=""mw-rollback-link"">") Then
                Dim Rollbacktoken As String = DiffText.Substring(DiffText.IndexOf("<span class=""mw-rollback-link"">"))
                Edit.RollbackToken = UrlDecode(FindString(Rollbacktoken, "<a href=""", "&amp;token=", """"))
            Else
                Edit.RollbackToken = Nothing
            End If

            If Edit.Id = "next" OrElse Edit.Id = "cur" AndAlso DiffText.Contains("<div id=""mw-diff-ntitle1"">") Then
                Edit.Id = FindString(DiffText, "<div id=""mw-diff-ntitle1""><strong><a", "oldid=", "'")
            End If

            If Edit.Id <> "cur" AndAlso Edit.All.ContainsKey(Edit.Id) Then Edit = Edit.All(Edit.Id)

            If Edit.Oldid = "prev" AndAlso DiffText.Contains("<div id=""mw-diff-otitle1"">") Then
                Edit.Oldid = FindString(DiffText, "<div id=""mw-diff-otitle1""><strong><a", "oldid=", "'")
            End If

            If Edit.User Is Nothing AndAlso DiffText.Contains("<div id=""mw-diff-ntitle2"">") Then
                Dim Username As String = FindString(DiffText, "<div id=""mw-diff-ntitle2"">", ">", "<")
                Username = HtmlDecode(Username.Replace(" (page does not exist)", ""))
                Edit.User = GetUser(Username)
            End If

            If Edit.Prev Is Nothing Then
                Edit.Prev = New Edit
                Edit.Prev.Page = Edit.Page
                Edit.Prev.Next = Edit
                Edit.Prev.Id = Edit.Oldid
                Edit.Prev.Oldid = "prev"
            End If

            If Edit.Time = Date.MinValue AndAlso DiffText.Contains("<div id=""mw-diff-ntitle1"">") Then
                Dim Time As String = FindString(DiffText, "<div id=""mw-diff-ntitle1"">", "</div>")

                If Time.Contains("Revision as of ") Then
                    Time = FindString(Time, "Revision as of ")
                    If Date.TryParse(Time, Edit.Time) _
                        Then Edit.Time = Date.SpecifyKind(CDate(Time), DateTimeKind.Local).ToUniversalTime

                ElseIf Time.Contains("Current revision") Then
                    Time = FindString(Time, "Current revision</a> (", ")")
                    If Date.TryParse(Time, Edit.Time) _
                        Then Edit.Time = Date.SpecifyKind(CDate(Time), DateTimeKind.Local).ToUniversalTime
                End If
            End If

            If Edit.Prev.Time = Date.MinValue AndAlso DiffText.Contains("<div id=""mw-diff-otitle1"">") Then
                Dim Time As String = FindString(DiffText, "<div id=""mw-diff-otitle1"">", "<a href=", ">", "<")
                Time = Time.Substring(Time.IndexOf(":") - 2)
                If Date.TryParse(Time, Edit.Prev.Time) _
                    Then Edit.Prev.Time = Date.SpecifyKind(CDate(Time), DateTimeKind.Local).ToUniversalTime
            End If

            If Edit.Prev.User Is Nothing AndAlso DiffText.Contains("<div id=""mw-diff-otitle2"">") Then
                Dim Username As String = HtmlDecode(FindString(DiffText, "<div id=""mw-diff-otitle2"">", ">", "</a>"))
                Edit.Prev.User = GetUser(Username)
            End If

            If Edit.Prev.Summary Is Nothing Then
                If DiffText.Contains("<div id=""mw-diff-otitle3"">") Then

                    Dim Summary As String = FindString(DiffText, "<div id=""mw-diff-otitle3"">")

                    If Summary.Contains("<div id=""mw-diff-ntitle3"">") Then _
                        Summary = Summary.Substring(0, Summary.IndexOf("<div id=""mw-diff-ntitle3"">"))

                    If Summary.Contains("<span class=""comment"">") Then
                        Summary = FindString(Summary, "<span class=""comment"">", "</span></div>")
                        Summary = HtmlToWikiText(Summary)
                        Edit.Prev.Summary = Summary
                    Else
                        Edit.Prev.Summary = ""
                    End If
                Else
                    Edit.Prev.Summary = ""
                End If
            End If

            If Config.PageCreatedPattern IsNot Nothing AndAlso Config.PageCreatedPattern.IsMatch(Edit.Prev.Summary) _
                AndAlso Edit.Prev.Prev Is Nothing Then

                Edit.Prev.NewPage = True
                Edit.Prev.Prev = NullEdit
                Edit.Page.FirstEdit = Edit.Prev
            End If

            If DiffText.Contains("<div id=""mw-diff-ntitle4"">&nbsp;</div>") Then Edit.Page.LastEdit = Edit

            If Not Edit.Prev.Processed Then ProcessEdit(Edit.Prev)

            Edit.ChangedContent = GetChangesFromDiff(DiffText)

            'Queues that have a revision content filter will now know whether they can add this revision
            For Each Queue As Queue In Queue.All.Values
                If Queue.RevisionRegex IsNot Nothing AndAlso Queue.MatchesFilter(Edit) _
                    AndAlso Queue.MatchesContentFilter(Edit) Then Queue.AddEdit(Edit)
            Next Queue
        End If

        Edit.Diff = DiffText
        Edit.DiffCacheState = Edit.CacheState.Cached

        If Tab.Edit IsNot Nothing AndAlso (Tab.Edit Is Edit OrElse (Tab.Edit.Next Is Edit AndAlso HidingEdit)) _
            Then DisplayEdit(Edit, False, Tab, (Edit.User IsNot Nothing AndAlso Not Edit.User.IsMe))
    End Sub

    'Converts an HTML diff into a string containing LF-separated list of changes on 'new' side of diff
    Function GetChangesFromDiff(ByVal Text As String) As String
        Dim Changes As String = "", Pos As Integer = 0, TextPart As String

        Pos = Text.IndexOf("<td class=""diff-addedline"">")

        While Pos > -1
            TextPart = Text.Substring(Pos)
            Dim Line As String = FindString(TextPart, "<td class=""diff-addedline"">", "</td>")

            If Line.Contains("<span class=""diffchange") Then
                While Line.Contains("<span class=""diffchange")
                    Dim Change As String = FindString(Line, "<span class=""diffchange", ">", "</span>")

                    Changes &= HtmlDecode(StripHTML(Change)) & LF
                    Line = Line.Substring(Line.IndexOf("<span class=""diffchange") + 1)
                End While

            ElseIf Line.Length > 0 Then
                'No 'diffchange' spans and nothing on the other side => the whole line was added
                Dim RowStart As String = Text.Substring(Text.Substring(0, Pos).LastIndexOf("<tr>"))
                RowStart = RowStart.Substring(0, RowStart.IndexOf("<td class=""diff-addedline"">"))
                If RowStart.Contains("<td colspan=""2"">&nbsp;</td>") Then Changes &= HtmlDecode(StripHTML(Line)) & LF
            End If

            Pos = Text.IndexOf("<td class=""diff-addedline"">", Pos + 1)
        End While

        Return Changes
    End Function

    Sub ProcessHistory(ByVal Result As String, ByVal Page As Page)

        Dim History As MatchCollection = New Regex("<rev revid=""([^""]+)"" user=""([^""]+)"" (anon="""" )" & _
            "?timestamp=""([^""]+)""( comment=""([^""]+)"")?(>([^<]*)</)?", RegexOptions.Compiled).Matches(Result)

        If History.Count = 0 Then
            If Page.LastEdit Is Nothing Then Page.LastEdit = NullEdit
            Exit Sub
        End If

        Dim NextEdit As Edit = Nothing

        For i As Integer = 0 To History.Count - 1
            Dim Edit As Edit
            Dim Diff As String = History(i).Groups(1).Value

            If Edit.All.ContainsKey(Diff) Then Edit = Edit.All(Diff) Else Edit = New Edit

            Edit.Id = Diff
            If Edit.Oldid Is Nothing Then Edit.Oldid = "prev"
            Edit.Page = Page

            If History(i).Groups(8).Value <> "" Then Edit.Text = HtmlDecode(History(i).Groups(8).Value)

            Edit.User = GetUser(HtmlDecode(History(i).Groups(2).Value))
            If Edit.Summary Is Nothing Then Edit.Summary = HtmlDecode(History(i).Groups(6).Value)
            If Edit.Time = Date.MinValue Then Edit.Time = CDate(History(i).Groups(4).Value)

            If Page.LastEdit Is Nothing Then
                Page.LastEdit = Edit
            ElseIf NextEdit IsNot Nothing Then
                Edit.Next = NextEdit
                Edit.Next.Oldid = Edit.Id
                NextEdit.Prev = Edit
            End If

            NextEdit = Edit
            ProcessEdit(Edit)
        Next i

        If Result.Contains("<revisions rvstartid=""") Then
            Page.HistoryOffset = NextEdit.Id
        Else
            Page.HistoryOffset = Nothing
            NextEdit.Prev = NullEdit
            Page.FirstEdit = NextEdit
        End If

        MainForm.RefreshInterface()
        MainForm.DrawHistory()
    End Sub

    Private ContribsRegex As New Regex _
        ("<item user=""[^""]+"" pageid=""[^""]+"" revid=""([^""]+)"" ns=""([^""]+)"" title=""([^""]+)"" " & _
         "timestamp=""([^""]+)"" (new="""" )?(minor="""" )?(top="""" )?(comment=""([^""]+)"" )?/>", _
         RegexOptions.Compiled)

    Sub ProcessContribs(ByVal Result As String, ByVal User As User)

        Dim Contribs As New List(Of String)(Split(Result, "<item "))

        Contribs.RemoveAt(0)

        If Contribs.Count = 0 Then
            If User.LastEdit Is Nothing Then User.LastEdit = NullEdit
            Exit Sub
        End If

        Dim NextByUser As Edit = Nothing

        For i As Integer = 0 To Contribs.Count - 1
            Dim Edit As Edit
            Dim Diff As String = GetParameter(Contribs(i), "revid")

            If Edit.All.ContainsKey(Diff) Then Edit = Edit.All(Diff) Else Edit = New Edit

            Edit.Id = Diff
            If Edit.Oldid Is Nothing Then Edit.Oldid = "prev"
            Edit.User = User

            Edit.Page = GetPage(GetParameter(Contribs(i), "title"))

            If Edit.Page.LastEdit Is Nothing AndAlso GetParameter(Contribs(i), "top") IsNot Nothing _
                Then Edit.Page.LastEdit = Edit

            If Edit.Summary Is Nothing Then Edit.Summary = GetParameter(Contribs(i), "comment")
            If Edit.Time = Date.MinValue Then Edit.Time = CDate(GetParameter(Contribs(i), "timestamp"))

            If User.LastEdit Is Nothing Then
                User.LastEdit = Edit
            ElseIf NextByUser IsNot Nothing Then
                Edit.NextByUser = NextByUser
                NextByUser.PrevByUser = Edit
            End If

            NextByUser = Edit
            ProcessEdit(Edit)
        Next i

        If Result.Contains("<usercontribs ucstart=""") Then
            User.ContribsOffset = GetParameter(Result, "ucstart")
        Else
            NextByUser.PrevByUser = NullEdit
            User.FirstEdit = NextByUser

            'Count edits
            If User.EditCount = -1 Then
                Dim ThisEdit As Edit = User.LastEdit, Count As Integer

                While ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
                    Count += 1
                    ThisEdit = ThisEdit.PrevByUser
                End While

                User.EditCount = Count
            End If
        End If

        If User Is CurrentUser Then Callback(AddressOf MainForm.DrawContribs)
        Callback(AddressOf MainForm.RefreshInterface)
    End Sub

    Sub ProcessRc(ByVal Result As String)
        Result = FindString(Result, "<recentchanges>", "</recentchanges>")

        If Result IsNot Nothing Then
            Dim RcEntries As String() = Result.Split(New String() {"<rc "}, StringSplitOptions.None)

            For i As Integer = RcEntries.Length - 1 To 0 Step -1
                Dim Item As String = RcEntries(i)
                Dim Match As Match = RcLineRegex.Match(Item)

                If Match.Success AndAlso LastRcTime <= _
                    Date.SpecifyKind(CDate(Match.Groups(13).Value).ToUniversalTime, DateTimeKind.Utc) Then

                    Select Case Match.Groups(1).Value
                        Case "edit" : ProcessRcEdit(Match)
                        Case "new" : ProcessRcNewPage(Match)
                        Case "log" : ProcessRcLog(Match)
                    End Select

                    LastRcTime = Date.SpecifyKind(CDate(Match.Groups(13).Value).ToUniversalTime, DateTimeKind.Utc)
                End If
            Next i
        End If

        If CurrentQueue IsNot Nothing Then
            For i As Integer = 0 To Math.Min(CurrentQueue.Edits.Count - 1, Config.Preloads - 1)
                If CurrentQueue.Edits(i).DiffCacheState = Edit.CacheState.Uncached Then
                    CurrentQueue.Edits(i).DiffCacheState = Edit.CacheState.Caching

                    Dim NewRequest As New DiffRequest
                    NewRequest.Edit = CurrentQueue.Edits(i)
                    NewRequest.Start()
                End If
            Next i
        End If

        MainForm.RefreshInterface()
        MainForm.RcReqTimer.Start()
    End Sub

    Sub ProcessRcEdit(ByVal Match As Match)
        Dim Edit As New Edit

        Edit.Page = GetPage(HtmlDecode(Match.Groups(2).Value))
        Edit.Rcid = Match.Groups(3).Value
        Edit.Id = Match.Groups(4).Value
        Edit.Oldid = Match.Groups(5).Value
        Edit.User = GetUser(HtmlDecode(Match.Groups(6).Value))
        Edit.Change = (CInt(Match.Groups(12).Value) - CInt(Match.Groups(11).Value))
        Edit.Size = CInt(Match.Groups(12).Value)
        If Edit.Page.LastEdit IsNot Nothing Then Edit.Page.LastEdit.Size = CInt(Match.Groups(11).Value)
        Edit.Time = Date.SpecifyKind(CDate(Match.Groups(13).Value).ToUniversalTime, DateTimeKind.Utc)
        Edit.Summary = HtmlDecode(Match.Groups(15).Value)

        If Not Edit.All.ContainsKey(Edit.Id) Then
            ProcessEdit(Edit)
            ProcessNewEdit(Edit)
        End If
    End Sub

    Sub ProcessRcNewPage(ByVal Match As Match)
        Dim Edit As New Edit

        Edit.NewPage = True
        Edit.Page = GetPage(HtmlDecode(Match.Groups(2).Value))
        Edit.Page.FirstEdit = Edit
        Edit.Rcid = Match.Groups(3).Value
        Edit.Page.Rcid = Edit.Rcid
        Edit.Prev = NullEdit
        Edit.Id = Match.Groups(4).Value
        Edit.Oldid = "-1"
        Edit.User = GetUser(HtmlDecode(Match.Groups(6).Value))
        Edit.Change = CInt(Match.Groups(12).Value)
        Edit.Size = CInt(Match.Groups(12).Value)
        Edit.Time = Date.SpecifyKind(CDate(Match.Groups(13).Value).ToUniversalTime, DateTimeKind.Utc)
        Edit.Summary = HtmlDecode(Match.Groups(15).Value)

        If Not Edit.All.ContainsKey(Edit.Id) Then
            ProcessEdit(Edit)
            ProcessNewEdit(Edit)
        End If
    End Sub

    Sub ProcessRcLog(ByVal Match As Match)
        Select Case HtmlDecode(Match.Groups(2).Value)
            Case "Special:Log/block"
                Dim NewBlock As New Block
                Dim Summary As String = HtmlDecode(Match.Groups(14).Value)

                If Summary.StartsWith("blocked") Then
                    NewBlock.Action = "block"
                    NewBlock.Duration = FindString(Summary, "with an expiry time of ", " (")
                    NewBlock.User = GetUser(FindString(Summary, "[[User:", "]]"))
                    NewBlock.Options = FindString(Summary, " (", ")")
                    If NewBlock.Options IsNot Nothing Then NewBlock.Options = NewBlock.Options _
                        .Replace("anonymous users only", "anononly") _
                        .Replace("account creation disabled", "nocreate") _
                        .Replace("autoblock disabled", "noautoblock")
                Else
                    NewBlock.Action = "unblock"
                    NewBlock.User = GetUser(FindString(Summary, "User:"))
                End If

                NewBlock.Comment = FindString(Summary, "): ")
                NewBlock.Admin = GetUser(HtmlDecode(Match.Groups(5).Value))
                NewBlock.Time = Date.SpecifyKind(CDate(Match.Groups(12).Value).ToUniversalTime, DateTimeKind.Utc)

                ProcessBlock(NewBlock)

            Case "Special:Log/delete"
                Dim NewDelete As New Delete
                Dim Summary As String = HtmlDecode(Match.Groups(14).Value)

                If Summary.StartsWith("deleted") Then NewDelete.Action = "delete" Else NewDelete.Action = "restore"
                NewDelete.Page = GetPage(FindString(Summary, "[[", "]]"))
                NewDelete.Admin = GetUser(HtmlDecode(Match.Groups(5).Value))
                NewDelete.Time = Date.SpecifyKind(CDate(Match.Groups(12).Value).ToUniversalTime, DateTimeKind.Utc)
                NewDelete.Comment = FindString(Summary, """: ")

                ProcessDelete(NewDelete)

            Case "Special:Log/protect"
                Dim NewProtection As New Protection
                Dim Summary As String = HtmlDecode(Match.Groups(14).Value)

                If Summary.StartsWith("protected") Then
                    NewProtection.Action = "protect"
                ElseIf Summary.StartsWith("unprotected") Then
                    NewProtection.Action = "unprotect"
                Else
                    NewProtection.Action = "change"
                End If

                NewProtection.Admin = GetUser(HtmlDecode(Match.Groups(5).Value))
                NewProtection.Time = Date.SpecifyKind(CDate(Match.Groups(12).Value).ToUniversalTime, DateTimeKind.Utc)
                NewProtection.Page = GetPage(FindString(Summary, "[[", "]]"))

                NewProtection.Summary = FindString(Summary, """: ")
                If NewProtection.Summary IsNot Nothing AndAlso NewProtection.Summary.Contains(" [") _
                    Then NewProtection.Summary = NewProtection.Summary.Substring(0, NewProtection.Summary.IndexOf(" ["))

                If Summary.Contains("[edit=autoconfirmed") Then NewProtection.EditLevel = "autoconfirmed"
                If Summary.Contains("[edit=sysop") Then NewProtection.EditLevel = "sysop"
                If Summary.Contains("move=autoconfirmed]") Then NewProtection.MoveLevel = "autoconfirmed"
                If Summary.Contains("move=sysop]") Then NewProtection.MoveLevel = "sysop"

                NewProtection.Cascading = Summary.Contains("[cascading]")

                ProcessProtection(NewProtection)
        End Select
    End Sub

    Sub DisplayEdit(ByVal Edit As Edit, Optional ByVal InBrowsingHistory As Boolean = False, _
        Optional ByVal Tab As BrowserTab = Nothing, Optional ByVal ChangeCurrentEdit As Boolean = True)

        If Tab Is Nothing Then Tab = CurrentTab

        If Edit IsNot Nothing AndAlso Edit.Page IsNot Nothing Then

            'Add edit to browsing history
            If Not InBrowsingHistory AndAlso (Tab.History.Count = 0 OrElse (Not Tab.History(0).Edit Is Edit)) _
                Then Tab.AddHistoryItem(New HistoryItem(Edit))

            'Remove edit from queues
            For Each Item As Queue In Queue.All.Values
                Item.RemoveViewedEdit(Edit)
            Next Item

            If Tab Is CurrentTab AndAlso ChangeCurrentEdit Then
                Tab.Edit = Edit
                MainForm.SetCurrentPage(Edit.Page, False)
                MainForm.SetCurrentUser(Edit.User, False)
            End If

            If Edit.Deleted Then
                Tab.Browser.DocumentText = "<div style=""font-family: Arial"">" & Msg("diff-deleted") & "</div>"
                Edit.DiffCacheState = Edit.CacheState.Viewed

            ElseIf Edit.Prev Is NullEdit Then
                'For the first revision to the page, show the revision
                Dim NewRequest As New BrowserRequest
                NewRequest.Tab = CurrentTab
                NewRequest.Url = SitePath() & "index.php?title=" & UrlEncode(Edit.Page.Name) & "&oldid=" & Edit.Id
                NewRequest.Start()

            Else
                If Edit.DiffCacheState = Edit.CacheState.Cached OrElse Edit.DiffCacheState = Edit.CacheState.Viewed Then

                    If Edit.Diff IsNot Nothing Then
                        Dim DocumentText, DiffText As String

                        DiffText = Edit.Diff

                        'Notify user of new messages
                        If Config.ShowNewMessages AndAlso MainForm.SystemMessages.Enabled _
                            AndAlso (Edit.Page IsNot User.Me.TalkPage) Then _
                            DiffText = "<div class=""usermessage"">" & Msg("main-new-messages") & "</div>" & DiffText

                        'Replace relative URLs with absolute ones
                        DiffText = DiffText.Replace("href=""/wiki/", "href=""" & Config.Projects(Config.Project) & "wiki/")
                        DiffText = DiffText.Replace("href='/wiki/", "href='" & Config.Projects(Config.Project) & "wiki/")
                        DiffText = DiffText.Replace("href=""/w/", "href=""" & Config.Projects(Config.Project) & "w/")
                        DiffText = DiffText.Replace("href='/w/", "href='" & Config.Projects(Config.Project) & "w/")

                        DocumentText = MakeHtmlWikiPage(Edit.Page.Name, DiffText)

                        Tab.CurrentUrl = SitePath() & "index.php?title=" & UrlEncode(Edit.Page.Name) & _
                            "&diff=" & Edit.Id & "&oldid=" & Edit.Oldid

                        Try
                            Tab.Browser.DocumentText = DocumentText

                        Catch ex As System.Runtime.InteropServices.COMException
                            'If an attempt is made to set the DocumentText property while it is 
                            'still being  set from a previous call, it seems to throw a COMException
                            'just swallow it for now
                        End Try
                    End If

                    Edit.DiffCacheState = Edit.CacheState.Viewed

                    MainForm.PageB.ForeColor = Color.Black
                    MainForm.RevertTimer.Stop()
                    MainForm.Reverting = False
                    HidingEdit = False

                ElseIf Edit.DiffCacheState = Edit.CacheState.Uncached Then
                    If Tab Is CurrentTab Then
                        For Each Item As ToolStripItem In New ToolStripItem() _
                            {MainForm.RevertWarnB, MainForm.RevertB, MainForm.WarnB, _
                            MainForm.UserReportB, MainForm.PageDeleteB, MainForm.PageTagB, MainForm.ContribsPrevB, _
                            MainForm.ContribsNextB, MainForm.ContribsLastB, MainForm.HistoryPrevB, MainForm.HistoryNextB, _
                            MainForm.HistoryLastB, MainForm.HistoryDiffToCurB, MainForm.PageWatchB}

                            Item.Enabled = False
                        Next Item
                    End If

                    If Not ChangeCurrentEdit Then HidingEdit = True
                    Edit.DiffCacheState = Edit.CacheState.Caching
                    LatestDiffRequest = New DiffRequest
                    LatestDiffRequest.Edit = Edit
                    LatestDiffRequest.Tab = Tab
                    LatestDiffRequest.Start()
                End If
            End If

            MainForm.RefreshInterface()

        ElseIf Edit IsNot Nothing AndAlso Edit.Page IsNot Nothing Then
            If CurrentQueue IsNot Nothing AndAlso CurrentQueue.Edits.Contains(Edit) Then
                CurrentQueue.RemoveViewedEdit(Edit)
                MainForm.DrawQueues()
            End If

            Dim NewHistoryRequest As New HistoryRequest
            NewHistoryRequest.Page = Edit.Page
            NewHistoryRequest.Start(AddressOf GotPageContent)
        End If
    End Sub

    Sub GotPageContent(ByVal Result As RequestResult)
        If CurrentPage.LastEdit IsNot Nothing Then
            CurrentEdit = CurrentPage.LastEdit
            DisplayEdit(CurrentEdit)
        End If
    End Sub

    Sub DisplayUrl(ByVal Url As String, Optional ByVal InBrowsingHistory As Boolean = False, _
        Optional ByVal Tab As BrowserTab = Nothing)

        If Tab Is Nothing Then Tab = CurrentTab

        If (Tab.History.Count = 0 OrElse (Not Tab.History(0).Url Is Url)) Then
            Dim NewHistoryItem As New HistoryItem(Url)
            Tab.AddHistoryItem(NewHistoryItem)
            DisplayHistoryItem(NewHistoryItem, Tab)

        Else
            Dim NewBrowserRequest As New BrowserRequest
            NewBrowserRequest.Tab = Tab
            NewBrowserRequest.Url = Url
            NewBrowserRequest.Start()
        End If
    End Sub

    Sub DisplayHistoryItem(ByVal Item As HistoryItem, Optional ByVal Tab As BrowserTab = Nothing)
        If Tab Is Nothing Then Tab = CurrentTab

        If Item.Text IsNot Nothing Then
            Tab.Browser.DocumentText = Item.Text
        Else
            Dim NewBrowserRequest As New BrowserRequest
            NewBrowserRequest.Tab = Tab
            NewBrowserRequest.Url = Item.Url
            NewBrowserRequest.HistoryItem = Item
            NewBrowserRequest.Start()
        End If
    End Sub

    Sub ShowNextEdit()
        If CurrentQueue.Edits.Count > 0 Then DisplayEdit(CurrentQueue.Edits(0), , CurrentTab)
    End Sub

    Sub DisplayHistoryItem(ByVal Index As Integer)
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Page IsNot Nothing Then
            Dim ThisEdit As Edit = CurrentEdit.Page.LastEdit

            For i As Integer = 0 To Index - 1
                If ThisEdit Is Nothing OrElse ThisEdit.Prev Is Nothing OrElse ThisEdit.Prev Is NullEdit Then Exit Sub
                ThisEdit = ThisEdit.Prev
            Next i

            DisplayEdit(ThisEdit)
        End If
    End Sub

    Sub ShowDiffToCur(ByVal ThisEdit As Edit)
        If ThisEdit IsNot Nothing Then
            Dim NewEdit As New Edit
            NewEdit.Page = CurrentEdit.Page.LastEdit.Page
            NewEdit.User = CurrentEdit.Page.LastEdit.User
            NewEdit.Id = CurrentEdit.Page.LastEdit.Id
            NewEdit.Oldid = ThisEdit.Id
            NewEdit.Time = CurrentEdit.Page.LastEdit.Time
            NewEdit.Summary = CurrentEdit.Page.LastEdit.Summary
            NewEdit.Prev = ThisEdit
            NewEdit.Next = CurrentEdit.Page.LastEdit.Next
            NewEdit.PrevByUser = CurrentEdit.PrevByUser
            NewEdit.NextByUser = CurrentEdit.NextByUser
            NewEdit.Multiple = True

            DisplayEdit(NewEdit)
        End If
    End Sub

    Sub ShowDiffBetween(ByVal OlderEdit As Edit, ByVal NewerEdit As Edit)
        Dim Edit As New Edit
        Edit.Page = NewerEdit.Page
        Edit.User = NewerEdit.User
        Edit.Id = NewerEdit.Id
        Edit.Oldid = OlderEdit.Id
        Edit.Time = NewerEdit.Time
        Edit.Summary = NewerEdit.Summary
        Edit.Prev = OlderEdit.Prev
        Edit.Next = NewerEdit.Next
        Edit.PrevByUser = NewerEdit.PrevByUser
        Edit.NextByUser = NewerEdit.Next
        Edit.Multiple = True

        DisplayEdit(Edit)
    End Sub

    Function IsTagFromSummary(ByVal Edit As Edit) As Boolean
        'Try to interpret as many styles of summary as possible without too many mistakes
        If String.IsNullOrEmpty(Edit.Summary) Then Return False
        If Edit.WarningLevel >= UserLevel.Notification Then Return False

        Dim Summary As String = TrimSummary(Edit.Summary.ToLower)

        For Each Item As String In Config.TagSummaries
            If Regex.IsMatch(Summary, Item) Then Return True
        Next Item

        Return False
    End Function

    Function GetUserLevelFromSummary(ByVal Edit As Edit) As UserLevel
        'Try to interpret as many styles of summary as possible without too many mistakes
        If Edit.User Is Edit.Page.Owner AndAlso Not Edit.User.IsMe Then Return UserLevel.None
        If Edit.Type = EditType.Revert Then Return UserLevel.None
        If String.IsNullOrEmpty(Edit.Summary) Then Return UserLevel.None
        Dim Summary As String = TrimSummary(Edit.Summary.ToLower)

        For Each Item As KeyValuePair(Of Regex, UserLevel) In Config.UserTalkSummaries
            If Item.Key.IsMatch(Summary) AndAlso (Item.Value <> UserLevel.Blocked OrElse Edit.User.Ignored) Then
                Return Item.Value
            End If
        Next Item

        Return UserLevel.None
    End Function

    Function ProcessUserTalk(ByVal Text As String, ByVal User As User) As List(Of Warning)
        Dim RecentWarnings As Integer = 0
        Dim Warnings As New List(Of Warning)

        'Find standard warnings
        For Each Item As Match In Regex.Matches(Text, _
            "<!-- Template:[Uu]w-([a-z]*)(\d)?(im)?(?:}})? -->.*\[\[User(?: talk)?:([^|]*).*(\d{2}:\d{2}, " & _
            "\d+ [a-zA-Z]+ \d{4}) \(UTC\)", RegexOptions.Compiled)

            Dim NewWarning As New Warning
            NewWarning.Time = Date.SpecifyKind(CDate(Item.Groups(5).Value), DateTimeKind.Utc)
            If NewWarning.Time.AddHours(Config.WarningAge) > Date.UtcNow Then RecentWarnings += 1
            If Item.Groups(4).Value <> "" Then NewWarning.User = GetUser(Item.Groups(4).Value)

            NewWarning.Type = Item.Groups(1).Value
            If NewWarning.Type = "cluebotwarning" Then NewWarning.Type = "bot"

            Select Case Item.Groups(2).Value
                Case "1" : NewWarning.Level = UserLevel.Warn1
                Case "2" : NewWarning.Level = UserLevel.Warn2
                Case "3" : NewWarning.Level = UserLevel.Warn3
                Case "4" : NewWarning.Level = UserLevel.WarnFinal
                Case Else : If Item.Groups(1).Value = "bv" Then NewWarning.Level = UserLevel.WarnFinal _
                    Else NewWarning.Level = UserLevel.Warning
            End Select

            If Item.Groups(1).Value.Contains("block") Then NewWarning.Level = UserLevel.Blocked

            Warnings.Add(NewWarning)
        Next Item

        'Sometimes the comment comes after the signature
        For Each Item As Match In Regex.Matches(Text, _
            "\[\[User(?: talk)?:([^|]*).*(\d{2}:\d{2}, \d+ [a-zA-Z]+ \d{4}) \(UTC\).*" & _
            "<!-- Template:uw-([a-z]*)(\d)?(im)? -->", RegexOptions.Compiled)

            Dim NewWarning As New Warning
            If Item.Groups(1).Value <> "" Then NewWarning.User = GetUser(Item.Groups(1).Value)

            NewWarning.Time = Date.SpecifyKind(CDate(Item.Groups(2).Value), DateTimeKind.Utc)
            If NewWarning.Time.AddHours(Config.WarningAge) > Date.UtcNow Then RecentWarnings += 1
            NewWarning.Type = Item.Groups(3).Value.ToLower

            Select Case Item.Groups(4).Value
                Case "1" : NewWarning.Level = UserLevel.Warn1
                Case "2" : NewWarning.Level = UserLevel.Warn2
                Case "3" : NewWarning.Level = UserLevel.Warn3
                Case "4" : NewWarning.Level = UserLevel.WarnFinal
                Case Else : If Item.Groups(1).Value = "bv" Then NewWarning.Level = UserLevel.WarnFinal _
                    Else NewWarning.Level = UserLevel.Warning
            End Select

            If NewWarning.Type.Contains("block") Then NewWarning.Level = UserLevel.Blocked

            Warnings.Add(NewWarning)
        Next Item

        'Find un-substituted standard warnings
        For Each Item As Match In Regex.Matches(Text, _
            "{{uw-([a-z]*)(\d)?(im)?(\|.*)?}}.*\[\[User( talk)?:([^|]*).*(\d{2}:\d{2}, \d+ [a-zA-Z]+ \d{4}) \(UTC\)", _
            RegexOptions.Compiled)

            Dim NewWarning As New Warning
            NewWarning.Time = Date.SpecifyKind(CDate(Item.Groups(7).Value), DateTimeKind.Utc)
            NewWarning.Type = Item.Groups(1).Value
            If Item.Groups(6).Value <> "" Then NewWarning.User = GetUser(Item.Groups(6).Value)

            If NewWarning.Time.AddHours(Config.WarningAge) > Date.UtcNow Then RecentWarnings += 1

            Select Case Item.Groups(2).Value
                Case "1" : NewWarning.Level = UserLevel.Warn1
                Case "2" : NewWarning.Level = UserLevel.Warn2
                Case "3" : NewWarning.Level = UserLevel.Warn3
                Case "4" : NewWarning.Level = UserLevel.WarnFinal
                Case Else : If Item.Groups(1).Value = "bv" Then NewWarning.Level = UserLevel.WarnFinal _
                    Else NewWarning.Level = UserLevel.Warning
            End Select

            Warnings.Add(NewWarning)
        Next Item

        'Find old warning templates
        For Each Item As Match In Regex.Matches(Text, _
            "<!-- Template:(.+block[^>]*|[Ss]pam[^>]*|[Vv]w[^>]*|[Tt]est[^>]*|[Aa]non vandal[^>]*|" & _
            "[Ww]elcome-anon-vandal[^>]*|[Ww]elcomevandal[^>]*|[Bb]latantvandal[^>]*|[Aa]ttack[^>]*) -->" & _
            ".*\[\[User(?: talk)?:([^|]*).*(\d{2}:\d{2}, \d+ [a-zA-Z]+ \d{4}) \(UTC\)", RegexOptions.Compiled)

            Dim NewWarning As New Warning
            NewWarning.Time = Date.SpecifyKind(CDate(Item.Groups(3).Value), DateTimeKind.Utc)
            If Item.Groups(2).Value <> "" Then NewWarning.User = GetUser(Item.Groups(2).Value)
            NewWarning.Type = Item.Groups(1).Value.ToLower

            If NewWarning.Type.StartsWith("test2") Then NewWarning.Level = UserLevel.Warn2 _
                Else If NewWarning.Type.StartsWith("test3") Then NewWarning.Level = UserLevel.Warn3 _
                Else If NewWarning.Type.StartsWith("test4") Then NewWarning.Level = UserLevel.WarnFinal _
                Else If NewWarning.Type.StartsWith("test5") Then NewWarning.Level = UserLevel.Blocked _
                Else If NewWarning.Type.StartsWith("blatantvandal") Then NewWarning.Level = UserLevel.Warn4im _
                Else If NewWarning.Type.Contains("block") Then NewWarning.Level = UserLevel.Blocked _
                Else NewWarning.Level = UserLevel.Warn1

            If NewWarning.Type.StartsWith("test") Then NewWarning.Type = "test"
            If NewWarning.Time.AddHours(Config.WarningAge) > Date.UtcNow Then RecentWarnings += 1

            Warnings.Add(NewWarning)
        Next Item

        'Find old warning templates with signature first
        For Each Item As Match In Regex.Matches(Text, _
            "\[\[User(?: talk)?:([^|]*).*(\d{2}:\d{2}, \d+ [a-zA-Z]+ \d{4}) \(UTC\).*" & _
            "<!-- Template:(.+block[^>]*|[Ss]pam[^>]*|[Vv]w[^>]*|[Tt]est[^>]*|[Aa]non vandal[^>]*|" & _
            "[Ww]elcome-anon-vandal[^>]*|[Ww]elcomevandal[^>]*|[Bb]latantvandal[^>]*|[Aa]ttack[^>]*) -->", _
            RegexOptions.Compiled)

            Dim NewWarning As New Warning
            NewWarning.Time = Date.SpecifyKind(CDate(Item.Groups(2).Value), DateTimeKind.Utc)
            If Item.Groups(1).Value <> "" Then NewWarning.User = GetUser(Item.Groups(1).Value)
            NewWarning.Type = Item.Groups(3).Value.ToLower
            If NewWarning.Type.StartsWith("uw-") Then NewWarning.Type = NewWarning.Type.Substring(3)

            If NewWarning.Type.StartsWith("test2") Then NewWarning.Level = UserLevel.Warn2 _
                Else If NewWarning.Type.StartsWith("test3") Then NewWarning.Level = UserLevel.Warn3 _
                Else If NewWarning.Type.StartsWith("test4") Then NewWarning.Level = UserLevel.WarnFinal _
                Else If NewWarning.Type.StartsWith("test5") Then NewWarning.Level = UserLevel.Blocked _
                Else If NewWarning.Type.StartsWith("blatantvandal") Then NewWarning.Level = UserLevel.Warn4im _
                Else If NewWarning.Type.Contains("block") Then NewWarning.Level = UserLevel.Blocked _
                Else NewWarning.Level = UserLevel.Warn1

            If NewWarning.Type.StartsWith("test") Then NewWarning.Type = "test"
            If NewWarning.Time.AddHours(Config.WarningAge) > Date.UtcNow Then RecentWarnings += 1

            Warnings.Add(NewWarning)
        Next Item

        'Find un-substituted non-standard warnings
        For Each Item As Match In Regex.Matches(Text, _
            "{{(test|attack)(\d)?(im)?}}.*\[\[User( talk)?:([^|]*).*(\d{2}:\d{2}, \d+ [a-zA-Z]+ \d{4}) \(UTC\)", _
            RegexOptions.Compiled)

            Dim NewWarning As New Warning
            NewWarning.Time = Date.SpecifyKind(CDate(Item.Groups(6).Value), DateTimeKind.Utc)
            If Item.Groups(5).Value <> "" Then NewWarning.User = GetUser(Item.Groups(5).Value)

            NewWarning.Type = Item.Groups(1).Value

            If NewWarning.Time.AddHours(Config.WarningAge) > Date.UtcNow Then RecentWarnings += 1

            Select Case Item.Groups(2).Value
                Case "1" : NewWarning.Level = UserLevel.Warn1
                Case "2" : NewWarning.Level = UserLevel.Warn2
                Case "3" : NewWarning.Level = UserLevel.Warn3
                Case "4" : NewWarning.Level = UserLevel.WarnFinal
                Case Else : NewWarning.Level = UserLevel.Warn1
            End Select

            Warnings.Add(NewWarning)
        Next Item

        'Find old AntiVandalBot/MartinBot warnings, and older VoaBot II warnings
        For Each Item As Match In Regex.Matches(Text, _
            "(! // \[\[User:VoABot II\]\]|// \[\[User:(MartinBot|AntiVandalBot)\|(MartinBot|AntiVandalBot)\]\]) " & _
            "(\d{2}:\d{2}, \d+ [a-zA-Z]+ \d{4}) \(UTC\)", RegexOptions.Compiled)

            Dim NewWarning As New Warning
            NewWarning.Time = Date.SpecifyKind(CDate(Item.Groups(4).Value), DateTimeKind.Utc)
            If Item.Groups(3).Value <> "" Then NewWarning.User = GetUser(Item.Groups(3).Value) _
                Else NewWarning.User = GetUser("VoABot II")
            NewWarning.Type = "bot"
            NewWarning.Level = UserLevel.Warn1

            If NewWarning.Time.AddHours(Config.WarningAge) > Date.UtcNow Then RecentWarnings += 1

            NewWarning.Level = UserLevel.Warn1
            Warnings.Add(NewWarning)
        Next Item

        'Find shared IP template tags
        If User.Anonymous Then
            For Each Item As String In Config.SharedIPTemplates
                If Text.ToLower.Contains(Item.ToLower) Then
                    Callback(AddressOf SetSharedIP, CObj(User))
                    Exit For
                End If
            Next Item
        End If

        Warnings.Sort(AddressOf CompareWarnings)
        Return Warnings
    End Function

    Sub SetSharedIP(ByVal UserObject As Object)
        CType(UserObject, User).SharedIP = True
    End Sub

    Function CompareWarnings(ByVal x As Warning, ByVal y As Warning) As Integer
        If x.Time < y.Time Then Return -1
        If y.Time < x.Time Then Return 1
        Return 0
    End Function

End Module
