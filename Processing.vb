Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Module Processing

    Sub ProcessEdit(ByVal Edit As Edit)
        If Edit Is Nothing Then Exit Sub

        'Random value to vary sort order
        Edit.Random = (New Random(Date.UtcNow.Millisecond)).NextDouble

        If Edit.Time = Date.MinValue Then Edit.Time = Date.SpecifyKind(Date.UtcNow, DateTimeKind.Utc)
        If Edit.Oldid Is Nothing Then Edit.Oldid = "prev"

        'Auto summaries
        If Edit.Summary.StartsWith("[[WP:AES|←]]Replaced") OrElse Edit.Summary.StartsWith("←Replaced") _
            OrElse Edit.Summary.StartsWith("[[WP:Automatic edit summaries|←]]Replaced") Then Edit.Type = EditType.ReplacedWith
        If Edit.Summary.StartsWith("[[WP:AES|←]]Blanked") OrElse Edit.Summary.StartsWith("←Blanked") _
            OrElse Edit.Summary.StartsWith("[[WP:Automatic edit summaries|←]]Blanked") Then Edit.Type = EditType.Blanked
        If Edit.Summary.StartsWith("[[WP:AES|←]]Redirected") OrElse Edit.Summary.StartsWith("←Redirected") _
            OrElse Edit.Summary.ToLower.StartsWith("redirected page to ") _
            OrElse Edit.Summary.ToLower.StartsWith("redirected to ") _
            OrElse Edit.Summary.StartsWith("[[WP:Automatic edit summaries|←]]Redirected") Then Edit.Type = EditType.Redirect

        If Edit.User IsNot Nothing AndAlso Edit.Page IsNot Nothing Then
            If Edit.NewPage Then
                Edit.Page.FirstEdit = Edit
                Edit.Prev = NullEdit
            End If

            'Reverts
            For Each Item As String In RevertSummaries
                If Edit.Summary.ToLower.StartsWith(Item) Then
                    Edit.Type = EditType.Revert

                    If Edit.Page.Level = PageL.None Then Edit.Page.Level = PageL.Watch

                    If Edit.Prev IsNot Nothing AndAlso Edit.Prev.User IsNot Nothing _
                        AndAlso Edit.Prev.User IsNot Edit.User AndAlso Edit.Prev.User.Level = UserL.None _
                        Then Edit.Prev.User.Level = UserL.Reverted

                    Exit For
                End If
            Next Item

            'Reverted users
            If Edit.Type <> EditType.Revert AndAlso Edit.Summary.ToLower.Contains("[[special:contributions/") Then
                Dim Username As String = Edit.Summary.Substring(Edit.Summary.ToLower.IndexOf _
                    ("[[special:contributions/") + 24)

                If Username.Contains("]]") OrElse Username.Contains("|") Then
                    If Username.Contains("]]") Then Username = Username.Substring(0, Username.IndexOf("]]"))
                    If Username.Contains("|") Then Username = Username.Substring(0, Username.IndexOf("|"))
                    Username = HtmlDecode(Username)

                    Dim RevertedUser As User = GetUser(Username)

                    If (RevertedUser IsNot Edit.User) AndAlso RevertedUser.Level = UserL.None _
                        Then RevertedUser.Level = UserL.Reverted
                End If
            End If

            'Reverted edits
            If Edit.Next IsNot Nothing AndAlso Edit.Next.Type = EditType.Revert _
                AndAlso Edit.User.Level = UserL.None Then Edit.User.Level = UserL.Reverted

            'Warnings / block notifications
            If Edit.Page.Namespace = "User talk" AndAlso Not Edit.Page.Name.Contains("/") Then
                Dim PageOwner As User = GetUser(Edit.Page.Name.Substring(10))
                Dim SummaryLevel As UserL = GetUserLevelFromSummary(Edit)

                If SummaryLevel >= UserL.Warning AndAlso PageOwner.Level > UserL.Ignore _
                    AndAlso Edit.Time.AddHours(Config.WarningAge) > Date.UtcNow Then

                    Edit.Type = EditType.Warning
                    Edit.WarningLevel = SummaryLevel
                    If Edit.User.WarnTime < Edit.Time Then Edit.User.WarnTime = Edit.Time

                    If PageOwner.Level < SummaryLevel AndAlso PageOwner.Level > UserL.Ignore _
                        AndAlso SummaryLevel < UserL.Warn4im Then PageOwner.Level = SummaryLevel

                ElseIf SummaryLevel = UserL.Notification Then
                    Edit.Type = EditType.Message

                    If PageOwner.Level < SummaryLevel AndAlso PageOwner.Level > UserL.Ignore _
                        Then PageOwner.Level = SummaryLevel
                End If
            End If

            'AIV/UAA reports
            If (Edit.Page.Name = Config.AIVLocation OrElse Edit.Page.Name = Config.UAALocation) Then

                If Edit.Summary.Contains("User-reported") _
                    AndAlso Not (Edit.Summary.Contains(" rm ") OrElse Edit.Summary.Contains("remove")) Then
                    Edit.Type = EditType.Report

                    If Edit.Summary.Contains("User-reported - ") _
                        OrElse Edit.Summary.Contains("User-reported */ ") Then

                        Dim Summary As String = ""

                        If Edit.Summary.Contains("User-reported - ") _
                            Then Summary = Edit.Summary.Substring(Edit.Summary.IndexOf("User-reported - ") + 16)
                        If Edit.Summary.Contains("User-reported */ ") _
                            Then Summary = Edit.Summary.Substring(Edit.Summary.IndexOf("User-reported - ") + 17)

                        If Summary.ToLower.StartsWith("[[special:contributions/") Then
                            Summary = Summary.Substring(22)
                            If Summary.Contains("|") Then Summary = Summary.Substring(0, Summary.IndexOf("|"))
                            If Summary.Contains("]") Then Summary = Summary.Substring(0, Summary.IndexOf("]"))
                            Summary = HtmlDecode(Summary)

                            Dim ReportedUser As User = GetUser(Summary)

                            If ReportedUser.Level > UserL.Ignore Then
                                If Edit.Page.Name = Config.AIVLocation AndAlso ReportedUser.Level < UserL.ReportedAIV _
                                    Then ReportedUser.Level = UserL.ReportedAIV
                                If Edit.Page.Name = Config.UAALocation AndAlso ReportedUser.Level < UserL.ReportedUAA _
                                    Then ReportedUser.Level = UserL.ReportedUAA
                            End If

                        Else
                            If Summary.EndsWith(" reported") _
                                Then Summary = Summary.Substring(0, Summary.IndexOf(" reported"))

                            Dim ReportedUser As User = GetUser(Summary)

                            If ReportedUser.Anonymous AndAlso ReportedUser.Level < UserL.ReportedUAA _
                                AndAlso ReportedUser.Level > UserL.Ignore Then
                                If Edit.Page.Name = Config.AIVLocation Then ReportedUser.Level = UserL.ReportedAIV _
                                    Else ReportedUser.Level = UserL.ReportedUAA
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

                    If ReportedUser.Level < UserL.ReportedAIV AndAlso ReportedUser.Level > UserL.Ignore Then _
                        ReportedUser.Level = UserL.ReportedAIV

                    Edit.Type = EditType.Report

                ElseIf Edit.Summary.ToLower.StartsWith("reporting ") AndAlso Edit.Summary.Length > 10 Then
                    Dim ReportedUser As User = GetUser(Edit.Summary.Substring(10))

                    If ReportedUser.Level < UserL.ReportedAIV AndAlso ReportedUser.Level > UserL.Ignore Then _
                        ReportedUser.Level = UserL.ReportedAIV

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

                    If ReportedUser.Level < UserL.ReportedAIV AndAlso ReportedUser.Level > UserL.Ignore _
                        Then ReportedUser.Level = UserL.ReportedAIV

                    Edit.Type = EditType.Report

                ElseIf Edit.Summary.ToLower.StartsWith("bot - reporting apparent vandalism by ") Then

                    'VoaBot II
                    Dim UserName As String = Edit.Summary.Substring(45)
                    If UserName.Contains("|") Then UserName = UserName.Substring(UserName.IndexOf("|") + 6)
                    If UserName.Contains("]]") Then UserName = UserName.Substring(0, UserName.IndexOf("]]"))

                    Dim ReportedUser As User = GetUser(UserName)

                    If ReportedUser.Level < UserL.ReportedAIV AndAlso ReportedUser.Level > UserL.Ignore _
                        Then ReportedUser.Level = UserL.ReportedAIV

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

                    If ReportedUser.Level < UserL.ReportedUAA AndAlso ReportedUser.Level > UserL.Ignore Then _
                        ReportedUser.Level = UserL.ReportedUAA

                    Edit.Type = EditType.Report
                End If
            End If

            'Tagging
            If IsTagFromSummary(Edit) AndAlso Edit.Type <= 0 AndAlso Edit.Type <> EditType.Tag _
                Then Edit.Type = EditType.Tag
        End If

        If Edit.Id IsNot Nothing AndAlso Not AllEditsById.ContainsKey(Edit.Id) _
            Then AllEditsById.Add(Edit.Id, Edit)

        Edit.Processed = True
    End Sub

    Sub ProcessNewEdit(ByVal Edit As Edit)
        If Edit.User Is Nothing OrElse Edit.Page Is Nothing Then Exit Sub

        Dim Redraw As Boolean

        If Edit.Page.LastEdit IsNot Nothing Then
            Edit.Prev = Edit.Page.LastEdit
            Edit.Prev.Next = Edit
        End If

        If Edit.User.LastEdit IsNot Nothing Then
            Edit.PrevByUser = Edit.User.LastEdit
            Edit.PrevByUser.NextByUser = Edit
        End If

        Edit.Page.LastEdit = Edit
        Edit.User.LastEdit = Edit

        Stats.Edits += 1
        Edit.User.SessionEditCount += 1

        Select Case Edit.Type
            Case EditType.Revert : Stats.Reverts += 1
            Case EditType.Warning : Stats.Warnings += 1
        End Select

        AllEditsByTime.Insert(0, Edit)

        'Add new pages to new pages queue
        If Edit.NewPage AndAlso ((Edit.Page.Namespace = "" AndAlso Config.NamespacesChecked.Contains("article")) _
            OrElse (Config.NamespacesChecked.Contains(Edit.Page.Namespace.ToLower))) Then NewPageQueue.Insert(0, Edit)

        'Remove old edits from queue
        Dim j As Integer = 0

        While j < EditQueue.Count - 1
            If EditQueue(j) IsNot EditQueue(j).Page.LastEdit Then
                EditQueue.RemoveAt(j)
                Redraw = True
            Else
                j += 1
            End If
        End While

        'Edit count, if known
        If Edit.User.EditCount > -1 Then Edit.User.EditCount += 1

        'Issue warnings
        Dim i As Integer = 0

        Do While i < PendingWarnings.Count
            If PendingWarnings(i).Page Is Edit.Page Then
                If Edit.User Is MyUser Then
                    Dim Last As Edit = GetPage("User talk:" & PendingWarnings(i).User.Name).LastEdit

                    If Last IsNot Nothing AndAlso Last.Time.AddSeconds(Config.MinWarningWait) > Date.UtcNow Then
                        'Do nothing if there is a very recent warning to try to compensate for
                        'stupid broken tools that warn for other people's reverts *cough* vandalproof *cough*

                        Log("Did not warn '" & PendingWarnings(i).User.Name & _
                            "', as they were last warned less than " & CStr(Config.MinWarningWait) & " seconds ago")
                    Else

                        Dim NewWarningRequest As New WarningRequest
                        NewWarningRequest.Edit = PendingWarnings(i)
                        NewWarningRequest.Start()
                    End If
                End If

                PendingWarnings.RemoveAt(i)
            End If

            i += 1
        Loop

        'Refresh undo information
        For Each Item As Command In Undo
            If Item.Edit IsNot Nothing AndAlso Item.Edit.Page Is Edit.Page Then
                Main.RemoveFromUndoList(Item)
                Exit For
            End If
        Next Item

        'Remove in-progress log entries
        Dim k As Integer = 0

        While k < Main.Status.Items.Count
            If TypeOf Main.Status.Items(k).Tag Is Page AndAlso CType(Main.Status.Items(k).Tag, Page) Is Edit.Page _
                Then Main.Status.Items.RemoveAt(k) Else k += 1
        End While

        If Edit.User Is MyUser Then
            Stats.EditsMe += 1

            'Log user's edits
            If Edit.Summary = "" Then Log("Edited '" & Edit.Page.Name & "': (no summary)", Edit) _
                Else Log("Edited '" & Edit.Page.Name & "': " & TrimSummary(Edit.Summary), Edit)

            'Add undo information
            Dim NewCommand As New Command

            NewCommand.Edit = Edit

            Select Case Edit.Type
                Case EditType.Warning
                    Stats.WarningsMe += 1
                    NewCommand.Type = CommandType.Warning
                    NewCommand.Description = "Warn " & Edit.Page.Name.Substring(10)

                Case EditType.Revert
                    Stats.RevertsMe += 1
                    NewCommand.Type = CommandType.Revert
                    NewCommand.Description = "Revert on " & Edit.Page.Name

                Case EditType.Report
                    NewCommand.Type = CommandType.Report
                    NewCommand.Description = "Report " & TrimSummary(Edit.Summary).Substring(10)

                Case Else
                    NewCommand.Type = CommandType.Edit
                    NewCommand.Description = "Edit " & Edit.Page.Name
            End Select

            If Main IsNot Nothing Then Main.AddToUndoList(NewCommand)
        End If

        'Check for new messages
        If Edit.Page Is GetPage("User talk:" & MyUser.Name) Then
            Main.SystemShowNewMessages.Enabled = Not (Edit.User Is MyUser)
            If Main.SystemShowNewMessages.Enabled AndAlso Config.TrayIcon Then Main.TrayIcon.ShowBalloonTip(10000)
        End If

        'Add to the queue
        If WhitelistLoaded _
            AndAlso Edit.User.Level <> UserL.Ignore _
            AndAlso (Config.ShowNewPages OrElse Not Edit.NewPage) _
            AndAlso Edit.Page.Level <> PageL.Ignore _
            AndAlso Not OwnUserspace(Edit) _
            AndAlso Edit.Type >= EditType.None _
            AndAlso Not Math.Abs(Edit.Size) > 100000 Then

            If (Edit.Page.Namespace = "" AndAlso Config.NamespacesChecked.Contains("article")) _
                OrElse (Config.NamespacesChecked.Contains(Edit.Page.Namespace.ToLower)) Then

                EditQueue.Add(Edit)
                Edit.Added = True
                Main.DiffNextB.Enabled = True
                If CurrentEdit Is Nothing Then DisplayEdit(Edit)
                If EditQueue.Count > 5000 Then EditQueue.RemoveAt(5000)
                Redraw = True
            End If
        End If

        'Warnings
        If Edit.Page.Namespace = "User talk" AndAlso Not Edit.Page.Name.Contains("/") Then
            Dim PageOwner As User = GetUser(Edit.Page.Name.Substring(10))
            Dim WarningLevel As UserL = GetUserLevelFromSummary(Edit)

            If PageOwner IsNot Nothing Then
                If Edit.User Is MyUser AndAlso PageOwner.WarningsCurrent AndAlso WarningLevel >= UserL.Warning Then

                    'Add our own warnings straight to the list
                    Dim NewWarning As New Warning

                    NewWarning.Level = WarningLevel
                    NewWarning.Time = Edit.Time
                    NewWarning.Type = "huggle"
                    NewWarning.User = MyUser

                    If PageOwner.Warnings Is Nothing Then PageOwner.Warnings = New List(Of Warning)
                    PageOwner.Warnings.Add(NewWarning)
                    PageOwner.Warnings.Sort(AddressOf SortWarningsByDate)
                Else
                    'Even if we can get the level of others, we can rarely even guess at the type
                    PageOwner.WarningsCurrent = False
                End If

                'Refresh any open user info form
                For Each Item As Form In Application.OpenForms
                    If TypeOf Item Is UserInfoForm AndAlso CType(Item, UserInfoForm).ThisUser Is PageOwner _
                        Then CType(Item, UserInfoForm).RefreshWarnings()
                Next Item
            End If
        End If

        'Get edit counts for non-whitelisted registered users, in batches of 50, and whitelist if appropriate
        If Config.AutoWhitelist AndAlso Not Edit.User.Anonymous _
            AndAlso (Edit.User.Level = UserL.None OrElse Edit.User.Level = UserL.Message) _
            AndAlso Edit.User.EditCount = 0 AndAlso NextCount.Count < 50 Then

            NextCount.Add(Edit.User)

            If NextCount.Count = 50 Then
                Dim NewCountRequest As New CountRequest
                NewCountRequest.Users.AddRange(NextCount)
                NewCountRequest.Start()
                NextCount.Clear()
            End If
        End If

        'Sort the queue
        EditQueue.Sort(AddressOf CompareEdits)

        'Preload diffs
        For l As Integer = 0 To Math.Min(EditQueue.Count, Config.Preloads) - 1
            If EditQueue(l).CacheState = CacheState.Uncached Then
                Dim NewDiffRequest As New DiffRequest
                NewDiffRequest.Edit = EditQueue(l)
                NewDiffRequest.Start()
            End If
        Next l

        'Refresh the interface
        If Main IsNot Nothing Then
            If CurrentEdit IsNot Nothing Then
                If CurrentEdit.Page Is Edit.Page Then Main.DrawHistory()
                If CurrentEdit.User Is Edit.User Then Main.DrawContribs()
            End If

            If Config.ShowQueue AndAlso Redraw Then Main.DrawQueue()

            For Each Item As TabPage In Main.Tabs.TabPages
                Dim ThisTab As BrowserTab = CType(Item.Controls(0), BrowserTab)

                If ThisTab.Edit IsNot Nothing Then
                    If ThisTab.Edit.Page Is Edit.Page AndAlso ThisTab.ShowNewEdits Then
                        'Show new edits to page
                        DisplayEdit(Edit, False, ThisTab, Not (Edit.User Is MyUser))

                        If ThisTab Is CurrentTab Then
                            Main.DiffRevertB.Enabled = False
                            Main.RevertWarnB.Enabled = False
                            Main.Reverting = False
                            Main.RevertTimer.Stop()
                            Main.RevertTimer.Interval = 3000
                            Main.RevertTimer.Start()
                        Else
                            ThisTab.Highlight = True
                        End If

                    ElseIf ThisTab.Edit.User Is Edit.User AndAlso ThisTab.ShowNewContribs Then
                        'Show new contribs by user
                        DisplayEdit(Edit, False, ThisTab)

                        If ThisTab Is CurrentTab Then
                            Main.DiffRevertB.Enabled = False
                            Main.RevertWarnB.Enabled = False
                            Main.RevertTimer.Start()
                        Else
                            ThisTab.Highlight = True
                        End If
                    End If
                End If
            Next Item

            Main.RefreshInterface()
        End If
    End Sub

    Sub UserDeleteRequest(ByVal Page As Page)
        If Administrator Then
            Dim NewDeleteForm As New DeleteForm
            NewDeleteForm.ThisPage = Page
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

    Function DoRevert(ByVal Edit As Edit, Optional ByVal Rollback As Boolean = True, _
        Optional ByVal Summary As String = Nothing, Optional ByVal Undoing As Boolean = False) As Boolean

        If Edit Is Nothing Then Return False

        'Confirm self-reversion
        If Config.ConfirmSelfRevert AndAlso Not Undoing AndAlso Edit.User Is MyUser _
            AndAlso (Edit.Page.FirstEdit Is Nothing OrElse Edit.Id <> Edit.Page.FirstEdit.Id) _
            AndAlso MsgBox("This will revert your own edit. Continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No _
            Then Return False

        'Confirm reversion of whitelisted user
        If Config.ConfirmIgnored AndAlso Edit.User IsNot MyUser AndAlso Edit.User.Level = UserL.Ignore _
            AndAlso MsgBox("Revert edit by whitelisted user '" & _
            Edit.User.Name & "'?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return False

        'Confirm reversion of multiple edits
        If Config.ConfirmMultiple AndAlso Edit.Prev IsNot Nothing AndAlso Edit.User IsNot Nothing _
            AndAlso Edit.User Is Edit.Prev.User AndAlso MsgBox("This will revert multiple edits by '" & _
            Edit.User.Name & "'.", MsgBoxStyle.OkCancel, "Revert") <> MsgBoxResult.Ok Then Return False

        If Not Undoing AndAlso Edit.User.Level = UserL.None Then Edit.User.Level = UserL.Reverted
        If Not Undoing AndAlso Edit.Page.Level = PageL.None Then Edit.Page.Level = PageL.Watch

        'If reverting first edit to user talk page, blank it
        If Edit.Page.FirstEdit IsNot Nothing AndAlso Edit.Id = Edit.Page.FirstEdit.Id _
            AndAlso Edit.Page.Namespace = "User talk" Then

            Dim NewEditRequest As New EditRequest
            NewEditRequest.Text = ""
            NewEditRequest.Page = Edit.Page
            NewEditRequest.Minor = Config.MinorReverts
            If Undoing Then NewEditRequest.Summary = Config.UndoSummary Else NewEditRequest.Summary = _
                "Revert edit by [[Special:Contributions/" & Config.Username & "|" & Config.Username & "]]"
            NewEditRequest.Start()
            Return False
        End If

        'If reverting first edit to page, offer to tag for speedy deletion
        If Edit.Page.FirstEdit IsNot Nothing AndAlso Edit.Id = Edit.Page.FirstEdit.Id Then
            If Not Config.Speedy Then
                MsgBox("Edit is the first edit to the page; unable to revert.")
                Return False
            End If

            Dim Prompt As String = "Edit is the only edit to the page. "

            If Administrator Then Prompt &= "Delete page instead?" Else Prompt &= "Tag for speedy deletion instead?"
            If MsgBox(Prompt, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then UserDeleteRequest(Edit.Page)

            Return False
        End If

        'If reverting page creator, offer to tag for speedy deletion
        If Edit.Page.FirstEdit IsNot Nothing AndAlso Config.Speedy AndAlso Edit.User Is Edit.Page.FirstEdit.User Then
            Dim Prompt As String = "Last edit was made by page creator, '" & Edit.User.Name & "'. "
            If Administrator Then Prompt &= "Delete page instead?" Else Prompt &= "Tag for speedy deletion instead?"

            Select Case MsgBox(Prompt, MsgBoxStyle.YesNoCancel)

                Case MsgBoxResult.Yes
                    UserDeleteRequest(Edit.Page)
                    Return False

                Case MsgBoxResult.Cancel
                    Return False
            End Select
        End If

        'Use rollback if possible
        If Rollback AndAlso RollbackAvailable AndAlso Config.UseRollback _
            AndAlso (Edit Is Edit.Page.LastEdit) AndAlso (Edit.RollbackUrl IsNot Nothing) Then

            If Edit Is CurrentEdit Then
                Main.DiffRevertB.Enabled = False
                Main.RevertWarnB.Enabled = False
                Main.Reverting = True
                Main.RevertTimer.Interval = 5000
                Main.RevertTimer.Start()
            End If

            Dim NewRollbackRequest As New RollbackRequest
            NewRollbackRequest.Edit = Edit
            NewRollbackRequest.Summary = Summary
            NewRollbackRequest.Start()
            Return True
        End If

        'Revert all edits by the last editor of the page, if possible
        If Edit Is Edit.Page.LastEdit AndAlso Not Undoing Then
            If Edit Is CurrentEdit Then
                Main.DiffRevertB.Enabled = False
                Main.RevertWarnB.Enabled = False
                Main.Reverting = True
                Main.RevertTimer.Interval = 5000
                Main.RevertTimer.Start()
            End If

            Dim NewFakeRollbackRequest As New FakeRollbackRequest
            NewFakeRollbackRequest.Page = Edit.Page
            NewFakeRollbackRequest.ExcludeUser = Edit.User
            NewFakeRollbackRequest.Summary = Summary
            NewFakeRollbackRequest.Start()
            Return True
        End If

        'Confirm revert to another revision by the same user
        If Not Undoing AndAlso Edit.Prev IsNot Nothing AndAlso Edit.Prev.User Is Edit.User _
            AndAlso Config.ConfirmSame AndAlso _
            MsgBox("This will revert to another revision by the user that is being reverted, " _
            & Edit.User.Name & "." & vbCrLf & "Continue with this?", MsgBoxStyle.OkCancel, _
            "huggle") = MsgBoxResult.Cancel Then Return False

        If Edit Is CurrentEdit Then
            Main.DiffRevertB.Enabled = False
            Main.RevertWarnB.Enabled = False
            Main.Reverting = True
            Main.RevertTimer.Interval = 5000
            Main.RevertTimer.Start()
        End If

        'Plain old reversion
        Dim NewRequest As New RevertRequest
        NewRequest.Edit = Edit.Prev
        NewRequest.Summary = Summary
        NewRequest.Start()
        Return True

    End Function

    Sub ProcessBlock(ByVal BlockObject As Object)
        Dim ThisBlock As Block = CType(BlockObject, Block)

        If ThisBlock IsNot Nothing AndAlso ThisBlock.User IsNot Nothing Then
            Stats.Blocks += 1
            If ThisBlock.Admin Is MyUser Then Stats.BlocksMe += 1

            If ThisBlock.User.BlocksCurrent Then
                If ThisBlock.User.Blocks Is Nothing Then ThisBlock.User.Blocks = New List(Of Block)
                ThisBlock.User.Blocks.Insert(0, ThisBlock)
            End If

            If ThisBlock.Action = "block" Then
                If ThisBlock.User.Level > UserL.Ignore Then ThisBlock.User.Level = UserL.Blocked
            Else
                ThisBlock.User.Level = UserL.None
            End If

            'Refresh any open user info form
            For Each Item As Form In Application.OpenForms
                If TypeOf Item Is UserInfoForm AndAlso CType(Item, UserInfoForm).ThisUser Is ThisBlock.User _
                    Then CType(Item, UserInfoForm).RefreshBlocks()
            Next Item
        End If
    End Sub

    Sub ProcessDelete(ByVal DeleteObject As Object)
        Dim ThisDelete As Delete = CType(DeleteObject, Delete)

        If ThisDelete IsNot Nothing AndAlso ThisDelete.Page IsNot Nothing Then
            Dim ThisPage As Page = ThisDelete.Page
            Dim ThisEdit As Edit = ThisPage.LastEdit

            If ThisDelete.Page.DeletesCurrent Then
                If ThisDelete.Page.Deletes Is Nothing Then ThisDelete.Page.Deletes = New List(Of Delete)
                ThisDelete.Page.Deletes.Insert(0, ThisDelete)
            End If

            While ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
                If ThisEdit.Prev IsNot Nothing Then ThisEdit.Prev.Next = ThisEdit.Next
                If ThisEdit.Next IsNot Nothing Then ThisEdit.Next.Prev = ThisEdit.Prev
                If ThisEdit.PrevByUser IsNot Nothing Then ThisEdit.PrevByUser.NextByUser = ThisEdit.NextByUser
                If ThisEdit.NextByUser IsNot Nothing Then ThisEdit.NextByUser.PrevByUser = ThisEdit.PrevByUser
                If EditQueue.Contains(ThisEdit) Then EditQueue.Remove(ThisEdit)

                ThisEdit.Deleted = True
                ThisEdit = ThisEdit.Prev
            End While

            For Each Item As TabPage In Main.Tabs.TabPages
                Dim ThisTab As BrowserTab = CType(Item.Controls(0), BrowserTab)

                If ThisTab.Edit IsNot Nothing AndAlso ThisTab.Edit.Page Is ThisPage _
                    Then DisplayEdit(ThisTab.Edit, False, ThisTab)
            Next Item
        End If
    End Sub

    Sub ProcessRestore(ByVal DeleteObject As Object)
        Dim ThisRestore As Delete = CType(DeleteObject, Delete)

        If ThisRestore IsNot Nothing AndAlso ThisRestore.Page IsNot Nothing Then
            If ThisRestore.Page.DeletesCurrent Then
                If ThisRestore.Page.Deletes Is Nothing Then ThisRestore.Page.Deletes = New List(Of Delete)
                ThisRestore.Page.Deletes.Insert(0, ThisRestore)
            End If
        End If
    End Sub

    Sub ProcessPageMove(ByVal PageMoveObject As Object)
        Dim ThisPageMove As PageMove = CType(PageMoveObject, PageMove)

        GetPage(ThisPageMove.Source).Name = ThisPageMove.Destination

        Dim RedirectEdit As New Edit

        RedirectEdit.Type = EditType.Redirect
        RedirectEdit.User = ThisPageMove.User
        RedirectEdit.Prev = NullEdit

        GetPage(ThisPageMove.Source).FirstEdit = RedirectEdit
        GetPage(ThisPageMove.Source).LastEdit = RedirectEdit
    End Sub

    Sub ProcessProtection(ByVal ProtectionObject As Object)
        Dim ThisProtection As Protection = CType(ProtectionObject, Protection)

        If ThisProtection IsNot Nothing AndAlso ThisProtection.Page IsNot Nothing Then
            Dim ThisPage As Page = ThisProtection.Page

            If ThisProtection.Page.ProtectionsCurrent Then
                If ThisProtection.Page.Protections Is Nothing _
                    Then ThisProtection.Page.Protections = New List(Of Protection)
                ThisProtection.Page.Protections.Insert(0, ThisProtection)
            End If

            ThisPage.EditLevel = ThisProtection.EditLevel
            ThisPage.MoveLevel = ThisProtection.MoveLevel
        End If
    End Sub

    Sub ProcessUpload(ByVal UploadObject As Object)
    End Sub

    Sub ProcessDiff(ByVal PreloadDataObject As Object, ByVal Tab As BrowserTab)

        If PreloadDataObject Is Nothing Then
            Exit Sub
        End If

        Dim PDO As CacheData = CType(PreloadDataObject, CacheData)

        Dim DiffText As String = PDO.Text
        Dim ThisEdit As Edit = PDO.Edit

        If Not ThisEdit.Multiple Then
            If DiffText.Contains("<span class=""mw-rollback-link"">") Then
                Dim RollbackUrl As String = DiffText.Substring(DiffText.IndexOf("<span class=""mw-rollback-link"">"))
                RollbackUrl = RollbackUrl.Substring(RollbackUrl.IndexOf("<a href=""") + 9)
                RollbackUrl = RollbackUrl.Substring(0, RollbackUrl.IndexOf(""""))
                RollbackUrl = HtmlDecode(RollbackUrl)
                ThisEdit.RollbackUrl = RollbackUrl
            Else
                ThisEdit.RollbackUrl = Nothing
            End If

            If ThisEdit.Id = "next" OrElse ThisEdit.Id = "cur" AndAlso DiffText.Contains("'>undo</a>)") Then
                Dim Diff As String = DiffText.Substring(DiffText.IndexOf("'>undo</a>)") - 20)
                Diff = Diff.Substring(Diff.IndexOf("undo=") + 5)
                Diff = Diff.Substring(0, Diff.IndexOf("'"))
                ThisEdit.Id = Diff
            End If

            If AllEditsById.ContainsKey(ThisEdit.Id) Then ThisEdit = AllEditsById(ThisEdit.Id)

            If ThisEdit.Oldid = "prev" AndAlso DiffText.Contains("<div id=""mw-diff-otitle1""><strong><a") Then
                Dim Oldid As String = DiffText.Substring(DiffText.IndexOf("<div id=""mw-diff-otitle1""><strong><a") _
                    + "<div id=""mw-diff-otitle1""><strong><a".Length)
                Oldid = Oldid.Substring(Oldid.IndexOf("oldid=") + 6)
                Oldid = Oldid.Substring(0, Oldid.IndexOf("'"))
                ThisEdit.Oldid = Oldid
            End If

            If ThisEdit.User Is Nothing AndAlso DiffText.Contains("<div id=""mw-diff-ntitle2"">") Then
                Dim Username As String = DiffText.Substring(DiffText.IndexOf("<div id=""mw-diff-ntitle2"">") _
                    + "<div id=""mw-diff-ntitle2"">".Length)
                Username = Username.Substring(Username.IndexOf(">") + 1)
                Username = Username.Substring(0, Username.IndexOf("<"))
                Username = HtmlDecode(Username.Replace(" (page does not exist)", ""))

                ThisEdit.User = GetUser(Username)
            End If

            If ThisEdit.Prev Is Nothing Then
                ThisEdit.Prev = New Edit
                ThisEdit.Prev.Page = ThisEdit.Page
                ThisEdit.Prev.Next = ThisEdit
                ThisEdit.Prev.Id = ThisEdit.Oldid
                ThisEdit.Prev.Oldid = "prev"
            End If

            If ThisEdit.Time = Date.MinValue AndAlso DiffText.Contains("<div id=""mw-diff-ntitle1"">") Then
                Dim Time As String = DiffText.Substring(DiffText.IndexOf("<div id=""mw-diff-ntitle1"">"))
                Time = Time.Substring(0, Time.IndexOf("</div>"))

                If Time.Contains("Revision as of ") Then
                    Time = Time.Substring(Time.IndexOf("Revision as of ") + 15)
                    Time = Time.Substring(0, Time.IndexOf("<"))
                    ThisEdit.Time = Date.SpecifyKind(CDate(Time), DateTimeKind.Local).ToUniversalTime

                ElseIf Time.Contains("Current revision") Then
                    Time = Time.Substring(Time.IndexOf("Current revision</a> (") + 22)
                    Time = Time.Substring(0, Time.IndexOf(")"))
                    ThisEdit.Time = Date.SpecifyKind(CDate(Time), DateTimeKind.Local).ToUniversalTime
                End If
            End If

            If ThisEdit.Prev.Time = Date.MinValue AndAlso DiffText.Contains("<div id=""mw-diff-otitle1"">") Then
                Dim Time As String = DiffText.Substring(DiffText.IndexOf("<div id=""mw-diff-otitle1"">"))
                Time = Time.Substring(Time.IndexOf("<a href="))
                Time = Time.Substring(Time.IndexOf(">") + 1)
                Time = Time.Substring(0, Time.IndexOf("<"))
                Time = Time.Substring(Time.IndexOf(":") - 2)
                If Date.TryParse(Time, ThisEdit.Prev.Time) Then ThisEdit.Prev.Time = Date.SpecifyKind(CDate(Time), DateTimeKind.Local).ToUniversalTime
            End If

            If ThisEdit.Prev.User Is Nothing AndAlso DiffText.Contains("<div id=""mw-diff-otitle2"">") Then
                Dim Username As String = DiffText.Substring _
                    (DiffText.IndexOf(("<div id=""mw-diff-otitle2"">")))
                Username = Username.Substring(0, Username.IndexOf("</a>"))

                If Username.IndexOf("title=""User:") > -1 Then _
                    Username = Username.Substring(Username.IndexOf("title=""User:") + 12) _
                    Else Username = Username.Substring(Username.IndexOf("title=""Special:Contributions/") + 29)

                Username = Username.Substring(0, Username.IndexOf(""""))
                Username = HtmlDecode(Username.Replace(" (page does not exist)", ""))

                ThisEdit.Prev.User = GetUser(Username)
            End If

            If ThisEdit.Prev.Summary Is Nothing Then
                If DiffText.Contains("<div id=""mw-diff-otitle3"">") Then
                    Dim Summary As String = DiffText.Substring _
                        (DiffText.IndexOf("<div id=""mw-diff-otitle3"">") + "<div id=""mw-diff-otitle3"">".Length)

                    If Summary.Contains("<div id=""mw-diff-ntitle3"">") Then _
                        Summary = Summary.Substring(0, Summary.IndexOf("<div id=""mw-diff-ntitle3"">"))

                    If Summary.Contains("<span class=""comment"">") Then
                        Summary = Summary.Substring(Summary.IndexOf("<span class=""comment"">") _
                            + "<span class=""comment"">".Length)
                        Summary = Summary.Substring(0, Summary.IndexOf("</span></div>"))
                        Summary = StripHTML(Summary)
                        If Summary.StartsWith("(") AndAlso Summary.EndsWith(")") _
                            Then Summary = Summary.Substring(1, Summary.Length - 2)

                        ThisEdit.Prev.Summary = Summary
                    Else
                        ThisEdit.Prev.Summary = ""
                    End If
                Else
                    ThisEdit.Prev.Summary = ""
                End If
            End If

            If ThisEdit.Prev.Summary.StartsWith("←Created page with '") _
                AndAlso ThisEdit.Prev.Prev Is Nothing Then
                ThisEdit.Prev.Prev = NullEdit
                ThisEdit.Page.FirstEdit = ThisEdit.Prev
            End If

            If DiffText.Contains("<div id=""mw-diff-ntitle4"">&nbsp;</div>") _
                Then ThisEdit.Page.LastEdit = ThisEdit

            If Not ThisEdit.Prev.Processed Then ProcessEdit(ThisEdit.Prev)
        End If

        If Not DiffCache.ContainsKey(ThisEdit.Id & " " & ThisEdit.Oldid) _
            Then DiffCache.Add(ThisEdit.Id & " " & ThisEdit.Oldid, DiffText)

        ThisEdit.CacheState = CacheState.Cached

        If Tab.Edit Is ThisEdit OrElse (Tab.Edit.Next Is ThisEdit AndAlso HidingEdit) _
            Then DisplayEdit(ThisEdit, False, Tab, ThisEdit.User IsNot MyUser)
    End Sub

    Sub ProcessHistory(ByVal Result As String, ByVal ThisPage As Page)

        Dim History As MatchCollection = New Regex("<rev revid=""([^""]+)"" user=""([^""]+)"" (anon="""" )" & _
            "?timestamp=""([^""]+)"" (comment=""([^""]+)"" )?/>", RegexOptions.Compiled).Matches(Result)

        If History.Count = 0 Then
            If ThisPage.LastEdit Is Nothing Then ThisPage.LastEdit = NullEdit
            Exit Sub
        End If

        Dim NextEdit As Edit = Nothing

        For i As Integer = 0 To History.Count - 1
            Dim ThisEdit As Edit
            Dim Diff As String = History(i).Groups(1).Value

            If AllEditsById.ContainsKey(Diff) Then ThisEdit = AllEditsById(Diff) Else ThisEdit = New Edit

            ThisEdit.Id = Diff
            If ThisEdit.Oldid Is Nothing Then ThisEdit.Oldid = "prev"
            ThisEdit.Page = ThisPage

            ThisEdit.User = GetUser(HtmlDecode(History(i).Groups(2).Value))
            If ThisEdit.Summary Is Nothing Then ThisEdit.Summary = HtmlDecode(History(i).Groups(6).Value)
            If ThisEdit.Time = Date.MinValue Then ThisEdit.Time = CDate(History(i).Groups(4).Value)

            If ThisPage.LastEdit Is Nothing Then
                ThisPage.LastEdit = ThisEdit
            ElseIf NextEdit IsNot Nothing Then
                ThisEdit.Next = NextEdit
                ThisEdit.Next.Oldid = ThisEdit.Id
                NextEdit.Prev = ThisEdit
            End If

            NextEdit = ThisEdit
            ProcessEdit(ThisEdit)
        Next i

        If Result.Contains("<revisions rvstartid=""") Then
            ThisPage.HistoryOffset = NextEdit.Id
        Else
            NextEdit.Prev = NullEdit
            ThisPage.FirstEdit = NextEdit
        End If

        Main.RefreshInterface()
    End Sub

    Sub ProcessContribs(ByVal Result As String, ByVal ThisUser As User)

        Dim Contribs As MatchCollection = New Regex("<item user=""[^""]+"" pageid=""[^""]+"" revid=""([^""]+)"" " & _
            "ns=""([^""]+)"" title=""([^""]+)"" timestamp=""([^""]+)"" (new="""" )?(minor="""" )?(comment=""([^" & _
            """]+)"" )?/>", RegexOptions.Compiled).Matches(Result)

        If Contribs.Count = 0 Then
            If ThisUser.LastEdit Is Nothing Then ThisUser.LastEdit = NullEdit
            Exit Sub
        End If

        Dim NextEditByUser As Edit = Nothing

        For i As Integer = 0 To Contribs.Count - 1
            Dim ThisEdit As Edit
            Dim Diff As String = Contribs(i).Groups(1).Value

            If AllEditsById.ContainsKey(Diff) Then ThisEdit = AllEditsById(Diff) Else ThisEdit = New Edit

            ThisEdit.Id = Diff
            If ThisEdit.Oldid Is Nothing Then ThisEdit.Oldid = "prev"
            ThisEdit.User = ThisUser

            ThisEdit.Page = GetPage(HtmlDecode(Contribs(i).Groups(3).Value))
            If ThisEdit.Summary Is Nothing Then ThisEdit.Summary = HtmlDecode(Contribs(i).Groups(8).Value)
            If ThisEdit.Time = Date.MinValue Then ThisEdit.Time = CDate(Contribs(i).Groups(4).Value)

            If ThisUser.LastEdit Is Nothing Then
                ThisUser.LastEdit = ThisEdit
            ElseIf NextEditByUser IsNot Nothing Then
                ThisEdit.NextByUser = NextEditByUser
                NextEditByUser.PrevByUser = ThisEdit
            End If

            NextEditByUser = ThisEdit
            ProcessEdit(ThisEdit)
        Next i

        If Result.Contains("<usercontribs ucstart=""") Then
            Dim D As Date = NextEditByUser.Time
            ThisUser.ContribsOffset = CStr(D.Year) & CStr(D.Month).PadLeft(2, "0"c) & CStr(D.Day).PadLeft(2, "0"c) _
                & CStr(D.Hour).PadLeft(2, "0"c) & CStr(D.Minute).PadLeft(2, "0"c) & CStr(D.Second).PadLeft(2, "0"c)
        Else
            NextEditByUser.PrevByUser = NullEdit
            ThisUser.FirstEdit = NextEditByUser

            'Count edits
            If ThisUser.EditCount = -1 Then
                Dim ThisEdit As Edit = ThisUser.LastEdit, Count As Integer

                While ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
                    Count += 1
                    ThisEdit = ThisEdit.PrevByUser
                End While

                ThisUser.EditCount = Count
            End If
        End If

        Main.RefreshInterface()
    End Sub

    Sub ProcessRcApi(ByVal Result As String)
        Result = Result.Substring(Result.IndexOf("<recentchanges>") + 15)
        Result = Result.Substring(0, Result.IndexOf("</recentchanges>"))

        If Result IsNot Nothing Then
            Dim RcEntries As String() = Result.Split(New String() {"<rc "}, StringSplitOptions.None)

            Dim RcLineRegex As New Regex _
                ("type=""[^""]*"" ns=""[^""]*"" title=""([^""]*)"" rcid=""[^""]*"" pageid=""[^""]*"" " _
                    & "revid=""([^""]*)"" old_revid=""([^""]*)"" user=""([^""]*)""( bot="""")?( anon=" _
                    & """"")?( new="""")?( minor="""")? oldlen=""([^""]*)"" newlen=""([^""]*)"" times" _
                    & "tamp=""([^""]*)""( comment=""([^""]*)"")? />", RegexOptions.Compiled)

            For i As Integer = RcEntries.GetUpperBound(0) To 0 Step -1
                Dim Item As String = RcEntries(i)
                Dim RcLineMatch As Match = RcLineRegex.Match(Item)

                If RcLineMatch.Success Then
                    Dim PageName As String = HtmlDecode(RcLineMatch.Groups(1).Value)

                    If PageName.StartsWith("Special:") Then
                        If PageName = "Special:Log/block" Then
                            Dim BlockSummary As String = HtmlDecode(RcLineMatch.Groups(13).Value)

                            If BlockSummary.Length > 14 AndAlso BlockSummary.Contains("""") Then
                                BlockSummary = BlockSummary.Substring(14)
                                BlockSummary = BlockSummary.Substring(0, BlockSummary.IndexOf(""""))

                                Dim BlockedUser As User = GetUser(BlockSummary)
                                If BlockedUser.Level < UserL.Blocked Then BlockedUser.Level = UserL.Blocked
                            End If
                        End If
                    Else
                        Dim NewEdit As New Edit

                        NewEdit.Page = GetPage(PageName)
                        NewEdit.Id = RcLineMatch.Groups(2).Value
                        NewEdit.Oldid = RcLineMatch.Groups(3).Value

                        If NewEdit.Oldid = "0" Then
                            NewEdit.Prev = NullEdit
                            NewEdit.Page.FirstEdit = NewEdit
                            NewEdit.Oldid = "-1"
                        End If

                        NewEdit.User = GetUser(HtmlDecode(RcLineMatch.Groups(4).Value))
                        NewEdit.Size = (CInt(RcLineMatch.Groups(10).Value) - CInt(RcLineMatch.Groups(9).Value))
                        NewEdit.Time = Date.SpecifyKind(CDate(RcLineMatch.Groups(11).Value).ToUniversalTime, DateTimeKind.Utc)
                        NewEdit.Summary = RcLineMatch.Groups(13).Value

                        If Not AllEditsById.ContainsKey(NewEdit.Id) Then
                            ProcessEdit(NewEdit)
                            ProcessNewEdit(NewEdit)
                        End If
                    End If
                End If
            Next i
        End If

        For j As Integer = 0 To Math.Min(EditQueue.Count - 1, Config.Preloads - 1)
            If EditQueue(j).CacheState = CacheState.Uncached Then
                Dim NewGetDiffRequest As New DiffRequest
                NewGetDiffRequest.Edit = EditQueue(j)
                NewGetDiffRequest.Start()
            End If
        Next j

        Main.RcReqTimer.Start()
    End Sub

    Function CompareEdits(ByVal X As Edit, ByVal Y As Edit) As Integer
        If X.Type > Y.Type AndAlso X.Type >= EditType.ReplacedWith Then Return -1
        If Y.Type > X.Type AndAlso Y.Type >= EditType.ReplacedWith Then Return 1

        If X.User.Level > Y.User.Level AndAlso X.User.Level >= UserL.Reverted Then Return -1
        If Y.User.Level > X.User.Level AndAlso Y.User.Level >= UserL.Reverted Then Return 1

        If X.Page.Level > Y.Page.Level Then Return -1
        If Y.Page.Level > X.Page.Level Then Return 1

        If X.NewPage AndAlso Not Y.NewPage Then Return -1
        If Y.NewPage AndAlso Not X.NewPage Then Return 1

        If X.User.Anonymous AndAlso Not Y.User.Anonymous Then Return -1
        If Y.User.Anonymous AndAlso Not X.User.Anonymous Then Return 1

        If X.Page.Namespace = "" AndAlso Not Y.Page.Namespace = "" Then Return -1
        If Y.Page.Namespace = "" AndAlso Not X.Page.Namespace = "" Then Return 1

        If X.Random <> Y.Random Then Return Math.Sign(Y.Random - X.Random)

        Return String.Compare(X.Page.Name, Y.Page.Name)
    End Function

    Sub DisplayEdit(ByVal Edit As Edit, Optional ByVal InBrowsingHistory As Boolean = False, _
        Optional ByVal Tab As BrowserTab = Nothing, Optional ByVal ChangeCurrentEdit As Boolean = True)

        If Tab Is Nothing Then Tab = CurrentTab

        If Edit IsNot Nothing AndAlso Edit.Page IsNot Nothing AndAlso Edit.User IsNot Nothing Then

            'Add edit to browsing history
            If Not InBrowsingHistory AndAlso (Tab.History.Count = 0 OrElse (Not Tab.History(0).Edit Is Edit)) _
                Then Tab.AddHistoryItem(New HistoryItem(Edit))

            'Remove edit from queue
            If CurrentQueue IsNot Nothing AndAlso CurrentQueue.Contains(Edit) Then
                CurrentQueue.Remove(Edit)
                Main.DrawQueue()
            End If

            If Tab Is CurrentTab AndAlso ChangeCurrentEdit Then
                Tab.Edit = Edit
                Main.SetCurrentPage(Edit.Page, False)
                Main.SetCurrentUser(Edit.User, False)
            End If

            If Edit.Deleted Then
                Tab.Browser.DocumentText = "<div style=""font-family: Arial"">This revision has been deleted.</div>"
                Edit.CacheState = CacheState.Viewed

            ElseIf Edit.Prev Is NullEdit Then
                'For the first revision to the page, show the revision
                Dim NewRequest As New BrowserRequest
                NewRequest.Tab = CurrentTab
                NewRequest.Url = SitePath & "w/index.php?title=" & UrlEncode(Edit.Page.Name) & "&oldid=" & Edit.Id
                NewRequest.Start()

            Else
                If Edit.CacheState = CacheState.Cached OrElse Edit.CacheState = CacheState.Viewed Then

                    If DiffCache.ContainsKey(Edit.Id & " " & Edit.Oldid) Then
                        Dim DocumentText, DiffText As String

                        DiffText = DiffCache(Edit.Id & " " & Edit.Oldid)

                        'Notify user of new messages
                        If Main.SystemShowNewMessages.Enabled _
                            AndAlso (Not Edit.Page.Name = "User talk:" & MyUser.Name) _
                            Then DiffText = "<div class=""usermessage"">You have new messages; select " & _
                            "System -> Show new messages or press M to view them.</div>" & DiffText

                        'Replace relative URLs with absolute ones
                        DiffText = DiffText.Replace("href=""/wiki/", "href=""" & Config.SitePath & "wiki/")
                        DiffText = DiffText.Replace("href='/wiki/", "href='" & Config.SitePath & "wiki/")
                        DiffText = DiffText.Replace("href=""/w/", "href=""" & Config.SitePath & "w/")
                        DiffText = DiffText.Replace("href='/w/", "href='" & Config.SitePath & "w/")

                        DocumentText = "<html><head><title>" & Edit.Page.Name & "</title></head><body>" & _
                            DiffText & "</body></html>"

                        Tab.CurrentUrl = SitePath & "w/index.php?title=" & UrlEncode(Edit.Page.Name.Replace(" ", "_")) & _
                            "&diff=" & Edit.Id & "&oldid=" & Edit.Oldid
                        Tab.Browser.DocumentText = DocumentText
                    End If

                    Edit.CacheState = CacheState.Viewed

                    Main.PageB.ForeColor = Color.Black
                    Main.RevertTimer.Stop()
                    Main.Reverting = False
                    HidingEdit = False

                ElseIf Edit.CacheState = CacheState.Uncached Then
                    If Tab Is CurrentTab Then
                        For Each Item As ToolStripItem In New ToolStripItem() _
                            {Main.RevertWarnB, Main.DiffRevertB, Main.WarnB, _
                            Main.UserReportB, Main.PageDeleteB, Main.PageTagB, Main.ContribsPrevB, _
                            Main.ContribsNextB, Main.ContribsLastB, Main.HistoryPrevB, Main.HistoryNextB, _
                            Main.HistoryLastB, Main.HistoryDiffToCurB, Main.PageWatchB}

                            Item.Enabled = False
                        Next Item
                    End If

                    If Not ChangeCurrentEdit Then HidingEdit = True
                    LatestDiffRequest = New DiffRequest
                    LatestDiffRequest.Edit = Edit
                    LatestDiffRequest.Tab = Tab
                    LatestDiffRequest.Start()
                End If
            End If

            Main.RefreshInterface()

        ElseIf Edit IsNot Nothing AndAlso Edit.Page IsNot Nothing Then
            If CurrentQueue IsNot Nothing AndAlso CurrentQueue.Contains(Edit) Then
                CurrentQueue.Remove(Edit)
                Main.DrawQueue()
            End If

            Dim NewHistoryRequest As New HistoryRequest
            NewHistoryRequest.DisplayWhenDone = True
            NewHistoryRequest.Page = Edit.Page
            NewHistoryRequest.Start()
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
        If CurrentQueue.Count > 0 Then
            Dim ThisEdit As Edit = CurrentQueue(0)
            DisplayEdit(ThisEdit, , CurrentTab)

            If EditQueue.Contains(ThisEdit) Then
                EditQueue.Remove(ThisEdit)
                Main.DrawQueue()
            End If

            If CurrentQueue.Count = 0 Then Main.DiffNextB.Enabled = False
        End If
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
            NewEdit.[Next] = CurrentEdit.Page.LastEdit.[Next]
            NewEdit.PrevByUser = CurrentEdit.PrevByUser
            NewEdit.NextByUser = CurrentEdit.NextByUser
            NewEdit.Multiple = True

            DisplayEdit(NewEdit)
        End If
    End Sub

    Sub DisplayContribsItem(ByVal Index As Integer)
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.User IsNot Nothing _
            AndAlso CurrentEdit.User.LastEdit IsNot Nothing Then

            Dim ThisEdit As Edit = CurrentEdit.User.LastEdit

            For i As Integer = 0 To Index - 1
                If ThisEdit.PrevByUser Is Nothing OrElse ThisEdit.PrevByUser Is NullEdit Then Exit Sub

                ThisEdit = ThisEdit.PrevByUser
            Next i

            DisplayEdit(ThisEdit)
        End If
    End Sub

    Function IsTagFromSummary(ByVal ThisEdit As Edit) As Boolean
        Dim Summary As String = ThisEdit.Summary.ToLower

        'Try to interpret as many styles of summary as possible without too many mistakes
        If Summary = "" Then Return False

        If Summary = "prod" OrElse Summary.Contains(" prod ") _
            OrElse Summary.Contains("{prod") OrElse Summary.Contains("prod}") _
            OrElse Summary.Contains(":prod") OrElse Summary.Contains("prod2") _
            OrElse Summary.Contains("prod-") OrElse Summary.Contains("prod:") _
            OrElse (Summary.Contains("prodding for deletion") OrElse Summary.Contains("proposed deletion") _
            OrElse Summary.Contains("proposed for deletion")) AndAlso ThisEdit.Page.Namespace = "" Then Return True

        If Summary.Contains("db-") OrElse Summary = "db" Then Return True

        If Summary.Contains("requesting speedy deletion") _
            OrElse Summary.Contains("speedy deletion request") _
            OrElse Summary.Contains("marked for speedy deletion") _
            OrElse (Summary.Contains("tagging for") AndAlso (Summary.Contains("speedy deletion") _
            OrElse Summary.Contains("proposed deletion"))) Then Return True

        If Summary.StartsWith("nominated for deletion") Then Return True

        If (Summary = "afd" OrElse Summary.Contains(":afd") OrElse Summary.Contains("{afd") _
            OrElse Summary.Contains("afd}") OrElse Summary.StartsWith("afd ")) _
            AndAlso ThisEdit.Page.Namespace = "" AndAlso ThisEdit.Size > 0 Then Return True

        Return False
    End Function

    Function GetUserLevelFromSummary(ByVal ThisEdit As Edit) As UserL
        'Try to interpret as many styles of summary as possible without too many mistakes

        Dim Summary As String = ThisEdit.Summary.ToLower
        If Summary = "" OrElse Summary Is Nothing Then Return UserL.None
        If ThisEdit.User.Name = ThisEdit.Page.Name.Substring(10) Then Return UserL.None

        If ThisEdit.User.Level = UserL.Ignore Then
            If Summary.EndsWith("v5") Then Return UserL.Blocked
            If Summary.EndsWith("v6") Then Return UserL.Blocked
            If Summary.EndsWith("v7") Then Return UserL.Blocked
            If Summary.EndsWith("t5") Then Return UserL.Blocked
            If Summary.EndsWith("t6") Then Return UserL.Blocked
            If Summary.EndsWith("t7") Then Return UserL.Blocked
            If Summary.EndsWith("d5") Then Return UserL.Blocked
            If Summary.EndsWith("d6") Then Return UserL.Blocked
            If Summary.EndsWith("d7") Then Return UserL.Blocked

            If Summary.Contains("test 5") OrElse Summary.Contains("test5") Then Return UserL.Blocked
            If Summary.Contains("test 6") OrElse Summary.Contains("test6") Then Return UserL.Blocked
            If Summary.Contains("test 7") OrElse Summary.Contains("test7") Then Return UserL.Blocked

            If Summary.StartsWith("notification: blocked") Then Return UserL.Blocked

            If Summary.Contains("you have been temporarily blocked") OrElse Summary.Contains("you have been blocked") _
                OrElse Summary.Contains("temporary block") OrElse Summary.Contains("vandalblock") _
                OrElse Summary.Contains("+block") OrElse Summary.Contains("+indefblock") _
                OrElse Summary.Contains("+anonblock") OrElse Summary = "blocked" OrElse Summary = "block" _
                OrElse Summary.Contains("indef blocked") Then Return UserL.Blocked
        End If

        'Guest9999
        If TrimSummary(Summary).Contains("informing of speedy delete nomination") Then Return UserL.Notification

        'BJBot
        If Summary.Contains("orphaned fair use image tagging") Then Return UserL.Notification

        'Mecu
        If Summary.Contains("warning: image missing fair use rationale") Then Return UserL.Notification

        'ClueBot
        If Summary.StartsWith("warning ") Then
            If Summary.EndsWith("#1") Then Return UserL.Warn1
            If Summary.EndsWith("#2") Then Return UserL.Warn2
            If Summary.EndsWith("#3") Then Return UserL.Warn3
            If Summary.EndsWith("#4") Then Return UserL.WarnFinal
        End If

        '{{uw-vandalism1}}, {{uw-vandalism1|Foo}}, ...
        If Summary.Contains("1}}") OrElse Summary.Contains("1|") Then Return UserL.Warn1
        If Summary.Contains("2}}") OrElse Summary.Contains("2|") Then Return UserL.Warn2
        If Summary.Contains("3}}") OrElse Summary.Contains("3|") Then Return UserL.Warn3
        If Summary.Contains("4}}") OrElse Summary.Contains("4|") Then Return UserL.WarnFinal

        '{{test1-n}}
        If Summary.Contains("1-n}}") Then Return UserL.Warn1
        If Summary.Contains("2-n}}") Then Return UserL.Warn2
        If Summary.Contains("3-n}}") Then Return UserL.Warn3
        If Summary.Contains("4-n}}") Then Return UserL.WarnFinal

        If Summary.Contains("warn1") Then Return UserL.Warn1
        If Summary.Contains("warn2") Then Return UserL.Warn2
        If Summary.Contains("warn3") Then Return UserL.Warn3
        If Summary.Contains("warn4") Then Return UserL.WarnFinal

        If Summary.Contains("warn 1") Then Return UserL.Warn1
        If Summary.Contains("warn 2") Then Return UserL.Warn2
        If Summary.Contains("warn 3") Then Return UserL.Warn3
        If Summary.Contains("warn 4") Then Return UserL.WarnFinal

        If Summary.Contains("test1") Then Return UserL.Warn1
        If Summary.Contains("test2") Then Return UserL.Warn2
        If Summary.Contains("test3") Then Return UserL.Warn3
        If Summary.Contains("test4") Then Return UserL.WarnFinal

        If Summary.Contains("test 1") Then Return UserL.Warn1
        If Summary.Contains("test 2") Then Return UserL.Warn2
        If Summary.Contains("test 3") Then Return UserL.Warn3
        If Summary.Contains("test 4") Then Return UserL.WarnFinal

        If Summary.Contains("vand1") Then Return UserL.Warn1
        If Summary.Contains("vand2") Then Return UserL.Warn2
        If Summary.Contains("vand3") Then Return UserL.Warn3
        If Summary.Contains("vand4") Then Return UserL.WarnFinal

        If Summary.Contains("vand 1") Then Return UserL.Warn1
        If Summary.Contains("vand 2") Then Return UserL.Warn2
        If Summary.Contains("vand 3") Then Return UserL.Warn3
        If Summary.Contains("vand 4") Then Return UserL.WarnFinal

        'VirginiaProp
        If Summary.Contains("vandal1") Then Return UserL.Warn1
        If Summary.Contains("vandal2") Then Return UserL.Warn2
        If Summary.Contains("vandal3") Then Return UserL.Warn3
        If Summary.Contains("vandal4") Then Return UserL.WarnFinal

        'Oda Mari
        If Summary.Contains("vandal 1") Then Return UserL.Warn1
        If Summary.Contains("vandal 2") Then Return UserL.Warn2
        If Summary.Contains("vandal 3") Then Return UserL.Warn3
        If Summary.Contains("vandal 4") Then Return UserL.WarnFinal

        If Summary.Contains("delete 1") Then Return UserL.Warn1
        If Summary.Contains("delete 2") Then Return UserL.Warn2
        If Summary.Contains("delete 3") Then Return UserL.Warn3
        If Summary.Contains("delete 4") Then Return UserL.WarnFinal

        If Summary.Contains("warning 1") Then Return UserL.Warn1
        If Summary.Contains("warning 2") Then Return UserL.Warn2
        If Summary.Contains("warning 3") Then Return UserL.Warn3
        If Summary.Contains("warning 4") Then Return UserL.WarnFinal

        If Summary.Contains("vandalism 1") Then Return UserL.Warn1
        If Summary.Contains("vandalism 2") Then Return UserL.Warn2
        If Summary.Contains("vandalism 3") Then Return UserL.Warn3
        If Summary.Contains("vandalism 4") Then Return UserL.WarnFinal

        If Summary.Contains("vandalism1") Then Return UserL.Warn1
        If Summary.Contains("vandalism2") Then Return UserL.Warn2
        If Summary.Contains("vandalism3") Then Return UserL.Warn3
        If Summary.Contains("vandalism4") Then Return UserL.WarnFinal

        'Lradrama
        If Summary.Contains("level 1") Then Return UserL.Warn1
        If Summary.Contains("level 2") Then Return UserL.Warn2
        If Summary.Contains("level 3") Then Return UserL.Warn3
        If Summary.Contains("level 4") Then Return UserL.WarnFinal

        'Searchme
        If Summary.Contains("test0") Then Return UserL.Warn1
        If Summary.Contains("test 0") Then Return UserL.Warn1

        ' Kbh3rd
        If Summary.StartsWith("vandalizing in") Then
            Select Case Right(Summary, 3)
                Case "/4/" : Return UserL.WarnFinal
                Case "/3/" : Return UserL.Warn3
                Case "/2/" : Return UserL.Warn2
                Case "/1/" : Return UserL.Warn1
                Case Else : Return UserL.Warn1
            End Select
        End If

        'ArielGold
        If Summary.StartsWith("1st blanking notice") Then Return UserL.Warn1
        If Summary.StartsWith("1st notice") Then Return UserL.Warn1
        If Summary.StartsWith("2nd warn") Then Return UserL.Warn2
        If Summary.StartsWith("3rd warn") Then Return UserL.Warn3
        If Summary.StartsWith("4th warn") Then Return UserL.WarnFinal

        'Katr67
        If Summary.StartsWith("first warning") Then Return UserL.Warn1
        If Summary.StartsWith("second warning") Then Return UserL.Warn2
        If Summary.StartsWith("third warning") Then Return UserL.Warn3
        If Summary.StartsWith("fourth warning") Then Return UserL.WarnFinal
        If Summary.StartsWith("final warning") Then Return UserL.WarnFinal

        If Summary.Contains("uw-selfrevert") Then Return UserL.Notification
        If Summary.Contains("uw-unsourced") Then Return UserL.Notification

        If Summary.Contains("uw-") Then
            Select Case Right(Summary, 1)
                Case "4" : Return UserL.WarnFinal
                Case "3" : Return UserL.Warn3
                Case "2" : Return UserL.Warn2
                Case "1" : Return UserL.Warn1
                Case Else : Return UserL.Warn1
            End Select
        End If

        If Summary.Contains("{{uw-") Then Return UserL.Warn1

        'Alison
        If Summary.EndsWith("w1") Then Return UserL.Warn1
        If Summary.EndsWith("w2") Then Return UserL.Warn2
        If Summary.EndsWith("w3") Then Return UserL.Warn3
        If Summary.EndsWith("w4") Then Return UserL.WarnFinal

        If Summary.EndsWith("t1") Then Return UserL.Warn1
        If Summary.EndsWith("t2") Then Return UserL.Warn2
        If Summary.EndsWith("t3") Then Return UserL.Warn3
        If Summary.EndsWith("t4") Then Return UserL.WarnFinal

        If Summary.EndsWith("v1") Then Return UserL.Warn1
        If Summary.EndsWith("v2") Then Return UserL.Warn2
        If Summary.EndsWith("v3") Then Return UserL.Warn3
        If Summary.EndsWith("v4") Then Return UserL.WarnFinal

        If Summary.EndsWith("d1") Then Return UserL.Warn1
        If Summary.EndsWith("d2") Then Return UserL.Warn2
        If Summary.EndsWith("d3") Then Return UserL.Warn3
        If Summary.EndsWith("d4") Then Return UserL.WarnFinal

        If Summary.Contains("blank1") Then Return UserL.Warn1
        If Summary.Contains("blank2") Then Return UserL.Warn2
        If Summary.Contains("blank3") Then Return UserL.Warn3
        If Summary.Contains("blank4") Then Return UserL.WarnFinal

        If Summary = "+bv" Then Return UserL.WarnFinal
        If Summary = "bv" Then Return UserL.WarnFinal
        If Summary = "bv warning" Then Return UserL.WarnFinal

        'D. Recorder
        If Summary.EndsWith(" bv") Then Return UserL.WarnFinal

        'Excirial
        If Summary.Contains("4im") Then Return UserL.WarnFinal

        '"warnings" that shouldn't be
        If Summary.StartsWith("general note: adding useless trivia") Then Return UserL.Notification
        If Summary.StartsWith("warning: adding useless trivia") Then Return UserL.Notification
        If Summary.StartsWith("general note: adding spam links") Then Return UserL.Notification

        If Summary.StartsWith("message re.") Then Return UserL.Warn1
        If Summary.StartsWith("general note: ") Then Return UserL.Warn1
        If Summary.StartsWith("caution: ") Then Return UserL.Warn2
        If Summary.StartsWith("warning: ") Then Return UserL.Warn3

        If Summary.Contains("final warn") Then Return UserL.WarnFinal
        If Summary.Contains("only warn") Then Return UserL.WarnFinal
        If Summary.Contains("first warn") Then Return UserL.Warn1
        If Summary.Contains("second warn") Then Return UserL.Warn2
        If Summary.Contains("third warn") Then Return UserL.Warn3

        'MWT
        If Summary.Contains("message regarding") AndAlso Summary.Contains("article using") Then Return UserL.Warning

        'EWS23
        If Summary.StartsWith("experimenting in") Then Return UserL.Warning

        'Hgilbert
        If Summary.Contains("avoid vandalising") Then Return UserL.Warning

        'Derumi
        If Summary.Contains("welcome/warn") OrElse Summary.Contains("welcome/minor warn") Then Return UserL.Warning

        '(someone whose name I forgot...)
        If Summary.Contains("repeated vandalism") Then Return UserL.Warning

        'NawlinWiki
        If Summary = "v" Then Return UserL.Warning
        If Summary = "t" Then Return UserL.Warning

        'VoABot II
        If Summary.StartsWith("bot - notifying user of reverted changes") Then Return UserL.Warn1

        'CounterVandalismBot
        If Summary.StartsWith("automatic warning regarding") Then Return UserL.Warn1

        If Summary.StartsWith("your recent edit") Then Return UserL.Warning
        If Summary.StartsWith("your edit") Then Return UserL.Warning
        If Summary.StartsWith("regarding your change to") Then Return UserL.Warning

        If Summary.Contains("vandalism to") Then Return UserL.Warning
        If Summary.Contains("vandalism in") Then Return UserL.Warning

        'AlexiusHoratius
        If Summary = GetMonthName(My.Computer.Clock.GmtTime.Month) & " " & CStr(My.Computer.Clock.GmtTime.Year) _
            Then Return UserL.Warning

        If Summary.Contains(GetMonthName(My.Computer.Clock.GmtTime.Month) & " " & CStr(My.Computer.Clock.GmtTime.Year)) _
            AndAlso Summary.Contains("new section") Then Return UserL.Warning

        If Summary.StartsWith("welcome") Then Return UserL.Notification
        If Summary.StartsWith("prod nomination of") Then Return UserL.Notification
        If Summary.StartsWith("notification: ") Then Return UserL.Notification
        If Summary.Contains("nn-warn") Then Return UserL.Notification

        If Summary.StartsWith("message from antispambot") Then Return UserL.Notification
        If Summary.StartsWith("proposed deletion of article you created") Then Return UserL.Notification
        If Summary.Contains("nonsensepage") Then Return UserL.Notification
        If Summary.Contains("prodwarning") Then Return UserL.Notification
        If Summary.Contains("nn") Then Return UserL.Notification
        If Summary.Contains("notice of possible deletion") Then Return UserL.Notification

        If Summary.Contains("warning") Then Return UserL.Warning
        If Summary.StartsWith("warn") Then Return UserL.Warning

        Return UserL.None
    End Function

    Function ProcessUserTalk(ByVal Text As String, ByVal ThisUser As User) As List(Of Warning)
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
                Case "1" : NewWarning.Level = UserL.Warn1
                Case "2" : NewWarning.Level = UserL.Warn2
                Case "3" : NewWarning.Level = UserL.Warn3
                Case "4" : NewWarning.Level = UserL.WarnFinal
                Case Else : If Item.Groups(1).Value = "bv" Then NewWarning.Level = UserL.WarnFinal _
                    Else NewWarning.Level = UserL.Warning
            End Select

            If Item.Groups(1).Value.Contains("block") Then NewWarning.Level = UserL.Blocked

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
                Case "1" : NewWarning.Level = UserL.Warn1
                Case "2" : NewWarning.Level = UserL.Warn2
                Case "3" : NewWarning.Level = UserL.Warn3
                Case "4" : NewWarning.Level = UserL.WarnFinal
                Case Else : If Item.Groups(1).Value = "bv" Then NewWarning.Level = UserL.WarnFinal _
                    Else NewWarning.Level = UserL.Warning
            End Select

            If NewWarning.Type.Contains("block") Then NewWarning.Level = UserL.Blocked

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
                Case "1" : NewWarning.Level = UserL.Warn1
                Case "2" : NewWarning.Level = UserL.Warn2
                Case "3" : NewWarning.Level = UserL.Warn3
                Case "4" : NewWarning.Level = UserL.WarnFinal
                Case Else : If Item.Groups(1).Value = "bv" Then NewWarning.Level = UserL.WarnFinal _
                    Else NewWarning.Level = UserL.Warning
            End Select

            Warnings.Add(NewWarning)
        Next Item

        'Find old warning templates
        For Each Item As Match In Regex.Matches(Text, _
            "<!-- Template:(.block[^>]*|[Ss]pam[^>]*|[Vv]w[^>]*|[Tt]est[^>]*|[Aa]non vandal[^>]*|" & _
            "[Bb]latantvandal[^>]*|[Aa]ttack[^>]*) -->" & _
            ".*\[\[User( talk)?:([^|]*).*(\d{2}:\d{2}, \d+ [a-zA-Z]+ \d{4}) \(UTC\)", RegexOptions.Compiled)

            Dim NewWarning As New Warning
            NewWarning.Time = Date.SpecifyKind(CDate(Item.Groups(4).Value), DateTimeKind.Utc)
            If Item.Groups(3).Value <> "" Then NewWarning.User = GetUser(Item.Groups(3).Value)

            NewWarning.Type = Item.Groups(1).Value.ToLower
            If NewWarning.Type.Contains(" ") _
                Then NewWarning.Type = NewWarning.Type.Substring(0, NewWarning.Type.IndexOf(" "))
            If NewWarning.Type.Contains("-") _
                Then NewWarning.Type = NewWarning.Type.Substring(0, NewWarning.Type.IndexOf("-"))

            If NewWarning.Type.StartsWith("test2") Then NewWarning.Level = UserL.Warn2 _
                Else If NewWarning.Type.StartsWith("test3") Then NewWarning.Level = UserL.Warn3 _
                Else If NewWarning.Type.StartsWith("test4") Then NewWarning.Level = UserL.WarnFinal _
                Else If NewWarning.Type.StartsWith("test5") Then NewWarning.Level = UserL.Blocked _
                Else If NewWarning.Type.StartsWith("blatantvandal") Then NewWarning.Level = UserL.Warn4im _
                Else If NewWarning.Type.Contains("block") Then NewWarning.Level = UserL.Blocked _
                Else NewWarning.Level = UserL.Warn1

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
                Case "1" : NewWarning.Level = UserL.Warn1
                Case "2" : NewWarning.Level = UserL.Warn2
                Case "3" : NewWarning.Level = UserL.Warn3
                Case "4" : NewWarning.Level = UserL.WarnFinal
                Case Else : NewWarning.Level = UserL.Warn1
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
            NewWarning.Level = UserL.Warn1

            If NewWarning.Time.AddHours(Config.WarningAge) > Date.UtcNow Then RecentWarnings += 1

            NewWarning.Level = UserL.Warn1
            Warnings.Add(NewWarning)
        Next Item

        'Find shared IP template tags
        If ThisUser.Anonymous Then
            For Each Item As String In SharedIPTemplates
                If Text.ToLower.Contains(Item.ToLower) Then
                    Callback(AddressOf SetSharedIP, CObj(ThisUser))
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