Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Web.HttpUtility

Class Main

    Public Reverting As Boolean
    Private LoggingOut As Boolean

    Public Sub Initialize()
        Icon = My.Resources.icon_red_button
        InitialTab.Parent = Tabs.TabPages(0)
        CurrentTab = InitialTab
        CurrentQueue = EditQueue

        Location = New Point(Math.Max(32, Config.WindowPosition.X), Math.Max(32, Config.WindowPosition.Y))
        Size = New Size(Math.Max(Config.WindowSize.Width, MinimumSize.Width), _
            Math.Max(Config.WindowSize.Height, MinimumSize.Height))
        If Config.WindowMaximize Then WindowState = FormWindowState.Maximized _
            Else WindowState = FormWindowState.Normal

        If Administrator Then UserReport.ShortcutKeyDisplayString = ""

        SystemReconnectIRC.Enabled = Config.IrcMode
        Splitter.SplitterDistance = Splitter.Height - 100
        StartTime = Date.UtcNow

        HistoryStrip.AutoSize = False
        MainStrip.Location = New Point(3, 24)
        HistoryStrip.Location = New Point(406, 24)
        NavigationStrip.Location = New Point(3, 79)
        ActionsStrip.Location = New Point(434, 79)

        If Not Config.IrcMode Then
            RcReqTimer.Start()
            BlockReqTimer.Start()
        End If

        Configure()
    End Sub

    Sub DrawHistory()
        If CurrentPage Is Nothing Then Exit Sub

        History.Image = Drawing.History(CurrentPage)
        HistoryScrollRB.Enabled = (HistoryOffset > 0)

        Dim ThisEdit As Edit = CurrentPage.LastEdit
        Dim X As Integer = History.Width - 18 + (HistoryOffset * 17)
        Dim EnableScroll As Boolean

        While ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
            If X < 0 Then
                EnableScroll = True
                Exit While
            End If

            X -= 17
            ThisEdit = ThisEdit.Prev
        End While

        HistoryScrollLB.Enabled = EnableScroll
        If CurrentEdit.Page.FirstEdit Is Nothing Then HistoryB.Enabled = True
    End Sub

    Sub DrawContribs()
        If CurrentUser Is Nothing Then Exit Sub

        Contribs.Image = Drawing.Contribs(CurrentUser)
        ContribsScrollRB.Enabled = (ContribsOffset > 0)

        Dim ThisEdit As Edit = CurrentUser.LastEdit
        Dim X As Integer = Contribs.Width - 18 + (ContribsOffset * 17)
        Dim EnableScroll As Boolean

        While ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
            If X < 0 Then
                EnableScroll = True
                Exit While
            End If

            X -= 17
            ThisEdit = ThisEdit.PrevByUser
        End While

        ContribsScrollLB.Enabled = EnableScroll
        If CurrentEdit.User.FirstEdit Is Nothing Then ContribsB.Enabled = True
    End Sub

    Sub DrawQueue()
        Dim QueueHeight As Integer = (Queue.Height \ 20) - 2

        If QueueHeight < CurrentQueue.Count Then
            QueueScroll.Enabled = True
            QueueScroll.Maximum = CurrentQueue.Count - 2
            QueueScroll.SmallChange = 1
            QueueScroll.LargeChange = QueueHeight
        Else
            QueueScroll.Enabled = False
            QueueScroll.Value = 0
        End If

        Queue.Draw(CurrentQueue)
    End Sub

    Private Sub Main_ResizeShown() Handles Me.Resize, Me.Shown
        HistoryStrip.Size = New Size(Math.Max(300, Width - 371), 55)
        History.Width = Math.Max(1, Width - 726)
        Contribs.Width = Math.Max(1, Width - 726)
        Status.Columns(1).Width = Width - 30

        If Visible Then
            DrawHistory()
            DrawContribs()
        End If
    End Sub

    Private Sub Main_FormClosing() Handles Me.FormClosing
        TrayIcon.Visible = False
        Visible = False
        EditQueue.Clear()

        If Not LoggingOut Then ClosingForm.ShowDialog()
    End Sub

    Private Sub Revert_Click() Handles DiffRevertB.ButtonClick
        DoRevert(CurrentEdit)
    End Sub

    Sub DiffNextB_Click() Handles QueueNext.Click, DiffNextB.Click
        ShowNextEdit()
    End Sub

    Private Sub UserIgnore_Click() Handles UserIgnore.Click, UserIgnoreB.Click
        If CurrentUser IsNot Nothing _
            Then If CurrentUser.Level = UserL.Ignore Then UnignoreUser(CurrentUser) Else IgnoreUser(CurrentUser)
    End Sub

    Public Sub IgnoreUser(ByVal User As User)
        User.Level = UserL.Ignore
        WhitelistChanged = True
        Log("Ignored user '" & User.Name & "'")

        For Each Item As Command In Undo
            If Item.Type = CommandType.Unignore AndAlso Item.User Is User Then
                RemoveFromUndoList(Item)
                Exit For
            End If
        Next Item

        Dim NewCommand As New Command
        NewCommand.User = User
        NewCommand.Description = "Ignore " & User.Name
        NewCommand.Type = CommandType.Ignore
        AddToUndoList(NewCommand)

        Dim i As Integer = 0, Redraw As Boolean

        Do While i < EditQueue.Count - 1
            If EditQueue(i).User Is User Then
                EditQueue.RemoveAt(i)
                Redraw = True
            Else
                i += 1
            End If
        Loop

        If Redraw AndAlso Config.ShowQueue Then DrawQueue()

        If User Is CurrentUser Then
            RefreshInterface()
            DrawHistory()
        End If
    End Sub

    Public Sub UnignoreUser(ByVal User As User)
        User.Level = UserL.None
        Log("Unignored user '" & User.Name & "'")

        For Each Item As Command In Undo
            If Item.Type = CommandType.Ignore AndAlso Item.User Is User Then
                RemoveFromUndoList(Item)
                Exit For
            End If
        Next Item

        Dim NewCommand As New Command
        NewCommand.User = User
        NewCommand.Description = "Unignore " & User.Name
        NewCommand.Type = CommandType.Unignore
        AddToUndoList(NewCommand)

        If User Is User Then
            RefreshInterface()
            DrawHistory()
        End If
    End Sub

    Private Sub HistoryPrev_Click() Handles HistoryPrevB.Click
        If CurrentEdit IsNot Nothing Then
            If CurrentEdit.Prev Is Nothing Then
                Dim PreviousEdit As New Edit

                PreviousEdit.Page = CurrentEdit.Page
                PreviousEdit.Oldid = "prev"
                PreviousEdit.Id = CurrentEdit.Oldid
                PreviousEdit.Next = CurrentEdit
                CurrentEdit.Prev = PreviousEdit
            End If

            DisplayEdit(CurrentEdit.Prev)
        End If
    End Sub

    Private Sub HistoryNext_Click() Handles HistoryNextB.Click
        If CurrentEdit IsNot Nothing Then
            If CurrentEdit.Multiple Then
                DisplayEdit(CurrentEdit.Prev.Next.Next)
            Else
                If CurrentEdit.Next Is Nothing Then
                    Dim NextEdit As New Edit

                    NextEdit.Page = CurrentEdit.Page
                    NextEdit.Oldid = CurrentEdit.Id
                    NextEdit.Id = "next"
                    NextEdit.Prev = CurrentEdit
                    CurrentEdit.Next = NextEdit
                End If

                DisplayEdit(CurrentEdit.Next)
            End If
        End If
    End Sub

    Private Sub HistoryLast_Click() Handles HistoryLastB.Click
        If CurrentPage IsNot Nothing Then
            If CurrentPage.LastEdit IsNot Nothing Then
                DisplayEdit(CurrentPage.LastEdit)
            Else
                Dim NewEdit As New Edit
                NewEdit.Page = CurrentPage
                NewEdit.Id = "cur"
                NewEdit.Oldid = "prev"
                DisplayEdit(NewEdit)
            End If
        End If
    End Sub

    Private Sub ContribsPrev_Click() Handles ContribsPrevB.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.PrevByUser IsNot Nothing _
            AndAlso CurrentEdit.PrevByUser IsNot NullEdit Then DisplayEdit(CurrentEdit.PrevByUser)
    End Sub

    Private Sub ContribsNext_Click() Handles ContribsNextB.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.NextByUser IsNot Nothing _
            Then DisplayEdit(CurrentEdit.NextByUser)
    End Sub

    Private Sub ContribsLast_Click() Handles ContribsLastB.Click
        If CurrentUser IsNot Nothing AndAlso CurrentEdit IsNot CurrentUser.LastEdit _
            Then DisplayEdit(CurrentUser.LastEdit)
    End Sub

    Private Sub ContribsFirst()
        If CurrentUser IsNot Nothing AndAlso CurrentUser.FirstEdit IsNot Nothing _
            Then DisplayEdit(CurrentUser.FirstEdit)
    End Sub

    Private Sub HistoryFirst()
        If CurrentPage IsNot Nothing AndAlso CurrentPage.FirstEdit IsNot Nothing _
        Then DisplayEdit(CurrentPage.FirstEdit)
    End Sub

    Private Sub History_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles History.MouseDown
        Dim Position As Integer = CInt((History.Width - e.X - 10) / 17) + HistoryOffset

        If e.Button = MouseButtons.Left Then
            DisplayHistoryItem(Position)

        ElseIf e.Button = MouseButtons.Right Then
            Dim ThisEdit As Edit = CurrentEdit.Page.LastEdit

            For i As Integer = 0 To Position - 1
                If ThisEdit.Prev Is Nothing OrElse ThisEdit.Prev Is NullEdit Then Exit Sub
                ThisEdit = ThisEdit.Prev
            Next i

            ShowDiffToCur(ThisEdit)
        End If

        DrawHistory()
    End Sub

    Private Sub History_MouseMove(ByVal s As Object, ByVal e As MouseEventArgs) Handles History.MouseMove
        If CurrentPage IsNot Nothing AndAlso CurrentPage.LastEdit IsNot Nothing Then

            Dim Position As Integer = CInt((History.Width - e.X - 10) / 17) + HistoryOffset
            Dim ThisEdit As Edit = CurrentPage.LastEdit

            For i As Integer = 0 To Position - 1
                If ThisEdit.Prev Is Nothing OrElse ThisEdit.Prev Is NullEdit Then
                    EditInfo.Visible = False
                    Exit Sub
                End If

                ThisEdit = ThisEdit.Prev
            Next i

            EditInfo.SetEdit(ThisEdit, EditInfoPanel.DisplayMode.PageName)
            EditInfo.Left = Width - EditInfo.Width - 12
            EditInfo.Top = Tabs.Top + 2
            EditInfo.Visible = True
        End If
    End Sub

    Private Sub HistoryContribs_MouseLeave() _
        Handles History.MouseLeave, Contribs.MouseLeave

        EditInfo.Visible = False
    End Sub

    Private Sub Contribs_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles Contribs.MouseDown
        DisplayContribsItem(CInt((Contribs.Width - e.X - 10) / 17) + ContribsOffset)
        DrawContribs()
    End Sub

    Private Sub Contribs_MouseMove(ByVal s As Object, ByVal e As MouseEventArgs) Handles Contribs.MouseMove
        If CurrentUser IsNot Nothing AndAlso CurrentUser.LastEdit IsNot Nothing Then

            Dim Position As Integer = CInt((Contribs.Width - e.X - 10) / 17) + ContribsOffset
            Dim ThisEdit As Edit = CurrentUser.LastEdit

            For i As Integer = 0 To Position - 1
                If ThisEdit.PrevByUser Is Nothing OrElse ThisEdit.PrevByUser Is NullEdit Then
                    EditInfo.Visible = False
                    Exit Sub
                End If

                ThisEdit = ThisEdit.PrevByUser
            Next i

            EditInfo.SetEdit(ThisEdit, EditInfoPanel.DisplayMode.UserName)
            EditInfo.Left = Width - EditInfo.Width - 12
            EditInfo.Top = Tabs.Top + 2
            EditInfo.Visible = True
        End If
    End Sub

    Private Sub ViewHistory_Click() Handles PageHistory.Click, HistoryB.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Page IsNot Nothing Then
            Dim NewHistoryRequest As New HistoryRequest
            NewHistoryRequest.Page = CurrentEdit.Page
            NewHistoryRequest.Start()
        End If
    End Sub

    Private Sub UserContribs_Click() Handles UserContribs.Click, ContribsB.Click
        If CurrentEdit IsNot Nothing Then
            Dim NewContribsRequest As New ContribsRequest
            NewContribsRequest.User = CurrentEdit.User
            NewContribsRequest.Start()
        End If
    End Sub

    Private Sub HistoryScrollTimer_Tick() Handles ScrollTimer.Tick
        If ScrollTimer.Tag Is HistoryScrollLB Then HistoryLeft()
        If ScrollTimer.Tag Is HistoryScrollRB Then HistoryRight()
        If ScrollTimer.Tag Is ContribsScrollLB Then ContribsLeft()
        If ScrollTimer.Tag Is ContribsScrollRB Then ContribsRight()
    End Sub

    Private Sub HistoryScroll_MouseDown(ByVal Sender As Object, ByVal e As MouseEventArgs) _
        Handles HistoryScrollLB.MouseDown, HistoryScrollRB.MouseDown

        ScrollTimer.Tag = Sender
        ScrollTimer.Start()
    End Sub

    Private Sub HistoryScroll_MouseUp(ByVal Sender As Object, ByVal e As MouseEventArgs) _
        Handles HistoryScrollLB.MouseUp, HistoryScrollRB.MouseUp

        If Sender Is HistoryScrollLB Then HistoryLeft() Else HistoryRight()
        ScrollTimer.Stop()
    End Sub

    Private Sub ContribsScroll_MouseDown(ByVal Sender As Object, ByVal e As MouseEventArgs) _
        Handles ContribsScrollLB.MouseDown, ContribsScrollRB.MouseDown

        ScrollTimer.Tag = Sender
        ScrollTimer.Start()
    End Sub

    Private Sub ContribsScroll_MouseUp(ByVal Sender As Object, ByVal e As MouseEventArgs) _
        Handles ContribsScrollLB.MouseUp, ContribsScrollRB.MouseUp

        If Sender Is ContribsScrollLB Then ContribsLeft() Else ContribsRight()
        ScrollTimer.Stop()
    End Sub

    Sub HistoryLeft()
        If HistoryScrollLB.Enabled Then
            HistoryOffset += 1
            DrawHistory()
            If Not HistoryScrollLB.Enabled Then ScrollTimer.Stop()
        End If
    End Sub

    Sub HistoryRight()
        If HistoryScrollRB.Enabled Then
            If HistoryOffset > 0 Then
                HistoryOffset -= 1
                DrawHistory()
            End If

            If Not HistoryScrollRB.Enabled Then ScrollTimer.Stop()
        End If
    End Sub

    Sub ContribsLeft()
        If ContribsScrollLB.Enabled Then
            ContribsOffset += 1
            DrawContribs()
            If Not ContribsScrollLB.Enabled Then ScrollTimer.Stop()
        End If
    End Sub

    Sub ContribsRight()
        If ContribsScrollRB.Enabled Then
            If ContribsOffset > 0 Then
                ContribsOffset -= 1
                DrawContribs()
            End If

            If Not ContribsScrollRB.Enabled Then ScrollTimer.Stop()
        End If
    End Sub

    Sub PageViewLatest_Click() Handles PageViewLatest.Click
        If CurrentPage IsNot Nothing Then DisplayUrl(SitePath & "w/index.php?title=" & _
            UrlEncode(CurrentPage.Name.Replace(" ", "_")))
    End Sub

    Sub PageView_Click() Handles PageView.Click, PageViewB.Click
        If CurrentEdit IsNot Nothing Then DisplayUrl(SitePath & "w/index.php?title=" & _
            UrlEncode(CurrentPage.Name) & "&oldid=" & CurrentEdit.Id)
    End Sub

    Private Sub DiffRevertSummary_Click() Handles DiffRevertSummary.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Prev IsNot Nothing Then
            Dim NewRevertForm As New RevertForm

            NewRevertForm.ThisPage = CurrentPage

            If NewRevertForm.ShowDialog = DialogResult.OK Then
                DiffRevertB.Enabled = False
                RevertWarnB.Enabled = False
                Reverting = True
                RevertTimer.Interval = 5000
                RevertTimer.Start()
            End If
        End If
    End Sub

    Private Sub RevertTimer_Tick() Handles RevertTimer.Tick
        RevertTimer.Stop()
        Reverting = False

        If CurrentEdit IsNot Nothing Then
            DiffRevertB.Enabled = True
            If CurrentEdit.User IsNot Nothing AndAlso CurrentEdit.User.Level > UserL.Ignore _
                Then RevertWarnB.Enabled = True
        End If
    End Sub

    Private Sub SystemExit_Click() Handles SystemExit.Click
        Close()
    End Sub

    Private Sub UserTalk_Click() Handles UserTalk.Click, UserTalkB.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.User IsNot Nothing Then
            Dim NewBrowserRequest As New BrowserRequest
            NewBrowserRequest.Tab = CurrentTab
            NewBrowserRequest.Url = SitePath & "w/index.php?title=User_talk:" & UrlEncode(CurrentEdit.User.Name)
            NewBrowserRequest.Start()

            Dim NewWarnLevelRequest As New WarningLogRequest
            NewWarnLevelRequest.ThisUser = CurrentEdit.User
            NewWarnLevelRequest.Start()
        End If
    End Sub

    Private Sub UserReport_Click() _
        Handles UserReport.Click, UserBlock.Click, UserReportB.Click

        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.User IsNot Nothing _
            AndAlso CurrentEdit.User.Level >= UserL.None Then

            If Administrator Then BlockUser(CurrentEdit.User) Else ReportUser(CurrentEdit.User, CurrentEdit)
        End If
    End Sub

    Private Sub HistoryScrollLast_Click()
        If HistoryOffset > 0 Then
            HistoryOffset = 0
            DrawHistory()
        End If
    End Sub

    Private Sub ContribsScrollLast_Click()
        If ContribsOffset > 0 Then
            ContribsOffset = 0
            DrawContribs()
        End If
    End Sub

    Private Sub RcReqTimer_Tick() Handles RcReqTimer.Tick
        RcReqTimer.Stop()

        Dim NewRcApiRequest As New RcApiRequest
        NewRcApiRequest.Start()
    End Sub

    Private Sub UserMessage_Click() _
        Handles UserMessage.Click, UserMessageB.Click, UserMessageOther.Click

        If CurrentUser IsNot Nothing Then
            Dim NewMessageForm As New MessageForm
            NewMessageForm.User = CurrentUser
            NewMessageForm.ShowDialog()
        End If
    End Sub

    Private Sub Warn_Click() Handles UserWarn.Click, WarnAdvanced.Click
        If CurrentUser IsNot Nothing Then
            Dim NewWarningForm As New WarningForm
            NewWarningForm.ThisUser = CurrentUser

            If NewWarningForm.ShowDialog() = DialogResult.OK Then WarnB.Enabled = False
        End If
    End Sub

    Private Sub RevertWarnB_ButtonClick() Handles RevertWarnB.ButtonClick
        RevertAndWarn()
    End Sub

    Public Sub RevertAndWarn(Optional ByVal WarnType As String = "warning", _
        Optional ByVal Level As UserL = UserL.None, Optional ByVal Summary As String = Nothing)

        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Prev IsNot Nothing Then

            'Get confirmation if needed
            If Config.ConfirmMultiple AndAlso CurrentEdit.User IsNot Nothing _
                AndAlso CurrentEdit.User Is CurrentEdit.Prev.User _
                AndAlso MsgBox("This will revert multiple edits by '" & CurrentEdit.User.Name & "'.", _
                MsgBoxStyle.OkCancel, "Revert") <> MsgBoxResult.Ok Then Exit Sub

            If DoRevert(CurrentEdit, True, Summary) Then
                'Be sure not to warn twice for the same edit
                Dim i As Integer = 0

                While i < PendingWarnings.Count - 1
                    If PendingWarnings(i).Page Is CurrentEdit.Page Then PendingWarnings.RemoveAt(i) Else i += 1
                End While

                If Level <> UserL.None Then CurrentEdit.LevelToWarn = Level
                CurrentEdit.TypeToWarn = WarnType

                PendingWarnings.Add(CurrentEdit)

                If Config.AutoAdvance Then ShowNextEdit()
            End If
        End If
    End Sub

    Private Sub EditPage_Click() Handles PageEdit.Click, PageEditB.Click
        If CurrentPage IsNot Nothing Then
            Dim NewEditForm As New EditForm
            NewEditForm.Page = CurrentPage
            NewEditForm.Show()
        End If
    End Sub

    Private Sub TagPage_Click() Handles PageTagB.Click
        If CurrentEdit.Page IsNot Nothing Then
            Dim NewTagForm As New TagForm

            NewTagForm.ThisPage = CurrentEdit.Page
            NewTagForm.ToSpeedy.Enabled = PageTagSpeedy.Enabled
            NewTagForm.ToProd.Enabled = PageProd.Enabled
            NewTagForm.ShowDialog()
        End If
    End Sub

    Sub TagSpeedy_Click() Handles PageTagSpeedy.Click
        If CurrentPage IsNot Nothing Then
            Dim NewSpeedyTagForm As New SpeedyForm
            NewSpeedyTagForm.ThisPage = CurrentPage
            NewSpeedyTagForm.ShowDialog()
        End If
    End Sub

    Sub PageDelete_Click() Handles PageDelete.Click, PageDeleteB.Click
        If CurrentPage IsNot Nothing Then
            Dim NewDeleteForm As New DeleteForm
            NewDeleteForm.ThisPage = CurrentPage

            If NewDeleteForm.ShowDialog = DialogResult.OK Then
                PageDelete.Enabled = False
                PageDeleteB.Enabled = False
            End If
        End If
    End Sub

    Sub PageTagProd_Click() Handles PageProd.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Page IsNot Nothing Then

            Dim NewProdTagForm As New ProdForm
            NewProdTagForm.ThisPage = CurrentEdit.Page

            If NewProdTagForm.ShowDialog = DialogResult.OK Then
                Dim NewTagRequest As New TagRequest

                NewTagRequest.Page = CurrentEdit.Page
                NewTagRequest.Tag = "{{subst:prod|" & NewProdTagForm.Reason.Text & "}}"
                NewTagRequest.Summary = Config.ProdSummary.Replace("$1", NewProdTagForm.Reason.Text)
                NewTagRequest.AvoidText = "{{dated prod|"

                If CurrentEdit.Page.FirstEdit IsNot Nothing Then
                    NewTagRequest.NotifyRequest = New UserMessageRequest
                    NewTagRequest.NotifyRequest.Message = Config.ProdMessage.Replace("$1", CurrentEdit.Page.Name) _
                        .Replace("$2", NewProdTagForm.Reason.Text)
                    NewTagRequest.NotifyRequest.Title = Config.ProdMessageTitle.Replace("$1", CurrentEdit.Page.Name)
                    NewTagRequest.NotifyRequest.AvoidText = CurrentEdit.Page.Name
                    NewTagRequest.NotifyRequest.Summary = Config.ProdMessageSummary.Replace("$1", CurrentEdit.Page.Name)
                    NewTagRequest.NotifyRequest.User = CurrentEdit.Page.FirstEdit.User
                End If

                NewTagRequest.Start()
            End If
        End If
    End Sub

    Private Sub PageMove_Click() Handles PageMove.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Page IsNot Nothing Then

            Dim NewMovePageForm As New MoveForm
            NewMovePageForm.ThisPage = CurrentEdit.Page

            If NewMovePageForm.ShowDialog = DialogResult.OK Then
                Dim NewMoveRequest As New MoveRequest
                NewMoveRequest.Page = CurrentEdit.Page
                NewMoveRequest.Target = NewMovePageForm.Target.Text
                NewMoveRequest.Reason = NewMovePageForm.Reason.Text
                NewMoveRequest.Start()
            End If
        End If
    End Sub

    Sub Log(ByVal Message As String, Optional ByVal Tag As Object = Nothing, Optional ByVal InProgress As Boolean = False)
        Dim NewListViewItem As New ListViewItem
        If InProgress Then NewListViewItem.ForeColor = Color.Red
        If Tag IsNot Nothing Then NewListViewItem.Tag = Tag
        NewListViewItem.SubItems.Add(Date.Now.Year.ToString & "-" & Date.Now.Month.ToString.PadLeft(2, "0"c) _
            & "-" & Date.Now.Day.ToString.PadLeft(2, "0"c) & " " & Date.Now.ToLongTimeString _
            & UtcOffset() & " -- " & Message)

        Status.BeginUpdate()

        If InProgress Then
            Status.Items.Insert(0, NewListViewItem)
        Else
            Dim i As Integer = 0

            While i < Status.Items.Count
                If Status.Items(i).ForeColor <> Color.Red Then
                    Status.Items.Insert(i, NewListViewItem)
                    Exit Sub
                End If

                i += 1
            End While

            Status.Items.Insert(0, NewListViewItem)
        End If

        Status.EndUpdate()
        UpdateCancelButton()
    End Sub

    Sub Delog(ByVal Request As Request)
        Dim i As Integer = 0

        Status.BeginUpdate()

        While i < Status.Items.Count
            If Status.Items(i).ForeColor = Color.Red AndAlso Status.Items(i).Tag Is Request _
                Then Status.Items.RemoveAt(i) Else i += 1
        End While

        Status.EndUpdate()
        UpdateCancelButton()
    End Sub

    Private Sub UpdateCancelButton()
        Dim CancellableRequests As Boolean

        For Each Item As ListViewItem In Status.Items
            If Item.ForeColor = Color.Red Then
                CancellableRequests = True
                Exit For
            End If
        Next Item

        CancelB.Enabled = CancellableRequests
    End Sub

    Function UtcOffset() As String
        If TimeZone.CurrentTimeZone.GetUtcOffset(Date.Now).TotalMinutes = 0 Then Return "" _
            Else Return " (" & CStr(IIf(TimeZone.CurrentTimeZone.GetUtcOffset(Date.Now).Minutes < 0, "-", "+")) _
            & CStr(TimeZone.CurrentTimeZone.GetUtcOffset(Date.Now).Hours).PadLeft(2, "0"c) & ":" _
            & CStr(TimeZone.CurrentTimeZone.GetUtcOffset(Date.Now).Minutes).PadLeft(2, "0"c) & ")"
    End Function

    Private Sub GoBack_Click() _
        Handles BrowserBack.Click, BrowserBackB.ButtonClick

        CurrentTab.HistoryBack()
    End Sub

    Private Sub GoForward_Click() _
        Handles BrowserForward.Click, BrowserForwardB.ButtonClick

        CurrentTab.HistoryForward()
    End Sub

    Private Sub SystemShowNewMessages_Click() _
        Handles SystemShowNewMessages.Click

        DisplayEdit(GetPage("User talk:" & MyUser.Name).LastEdit)
        SystemShowNewMessages.Enabled = False
    End Sub

    Private Sub BlockRequestTimer_Tick() Handles BlockReqTimer.Tick
        BlockReqTimer.Stop()

        Dim NewBlockApiRequest As New BlockApiRequest
        NewBlockApiRequest.Start()
    End Sub

    Sub BlockUser(ByVal ThisUser As User)
        Dim NewBlockForm As New BlockForm

        NewBlockForm.ThisUser = ThisUser
        NewBlockForm.Reason.Text = Config.BlockReason

        If ThisUser.Anonymous Then
            NewBlockForm.BlockEmail.Enabled = False
            NewBlockForm.EnableAutoblock.Enabled = False
            NewBlockForm.AnonOnly.Checked = True
            NewBlockForm.BlockCreation.Checked = True
            NewBlockForm.Expiry.Text = Config.BlockTimeAnon
        Else
            NewBlockForm.AnonOnly.Enabled = False
            NewBlockForm.EnableAutoblock.Checked = True
            NewBlockForm.BlockCreation.Checked = True
            NewBlockForm.Expiry.Text = Config.BlockTime
        End If

        If NewBlockForm.ShowDialog = DialogResult.OK Then
            UserReportB.Enabled = False

            Dim NewBlockRequest As New BlockRequest
            NewBlockRequest.User = NewBlockForm.ThisUser
            NewBlockRequest.Reason = NewBlockForm.Reason.Text
            NewBlockRequest.AnonOnly = NewBlockForm.AnonOnly.Checked
            NewBlockRequest.BlockCreation = NewBlockForm.BlockCreation.Checked
            NewBlockRequest.BlockEmail = NewBlockForm.BlockEmail.Checked
            NewBlockRequest.Autoblock = NewBlockForm.EnableAutoblock.Checked
            NewBlockRequest.Notify = (NewBlockForm.BlockMessage.Text <> "" _
                AndAlso NewBlockForm.BlockMessage.Text <> "(none)")
            NewBlockRequest.Expiry = NewBlockForm.Expiry.Text
            If NewBlockForm.BlockMessage.Text <> "(standard block message)" _
                Then NewBlockRequest.NotifyTemplate = NewBlockForm.BlockMessage.Text
            NewBlockRequest.Start()
        End If
    End Sub

    Sub ReportUser(ByVal ThisUser As User, Optional ByVal ThisEdit As Edit = Nothing)
        Dim NewUserReport As New ReportForm
        NewUserReport.ThisUser = ThisUser
        NewUserReport.Message.Text = Config.ReportReason

        If NewUserReport.ShowDialog = DialogResult.OK Then
            If NewUserReport.ReportTo.SelectedIndex = 0 Then

                Dim NewRequest As New AIVReportRequest
                NewRequest.User = ThisUser
                NewRequest.Edit = ThisEdit
                NewRequest.Reason = NewUserReport.Message.Text
                NewRequest.Start()

            Else
                Dim NewRequest As New UAAReportRequest
                NewRequest.User = ThisUser
                NewRequest.Reason = NewUserReport.Message.Text
                NewRequest.Start()
            End If
        End If
    End Sub

    Private Sub HistoryDiffToCur_Click() Handles HistoryDiffToCurB.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Prev IsNot Nothing Then ShowDiffToCur(CurrentEdit.Prev)
    End Sub

    Private Sub WatchPage_Click() Handles PageWatch.Click, PageWatchB.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Page IsNot Nothing Then
            If Watchlist.Contains(SubjectPage(CurrentEdit.Page)) Then
                Dim NewRequest As New UnwatchRequest
                NewRequest.Page = SubjectPage(CurrentEdit.Page)
                NewRequest.Manual = True
                NewRequest.Start()
            Else
                Dim NewRequest As New WatchRequest
                NewRequest.Page = SubjectPage(CurrentEdit.Page)
                NewRequest.Manual = True
                NewRequest.Start()
            End If
        End If
    End Sub

    Private Sub LogContextCopy_Click() Handles LogContextCopy.Click
        If Status.SelectedIndices.Count > 0 _
            Then Clipboard.SetText(Status.Items(Status.SelectedIndices(0)).SubItems(1).Text)
    End Sub

    Private Sub TrayRestore_Click() Handles TrayRestore.Click
        Visible = Not Visible
        If Visible Then TrayRestore.Text = "Minimize" Else TrayRestore.Text = "Restore"
    End Sub

    Private Sub TrayExit_Click() Handles TrayExit.Click
        Close()
    End Sub

    Private Sub TrayIcon_DoubleClick() Handles TrayIcon.DoubleClick
        TrayRestore_Click()
    End Sub

    Private Sub TrayIcon_BalloonTipClicked() Handles TrayIcon.BalloonTipClicked
        If SystemShowNewMessages.Enabled Then SystemShowNewMessages_Click()
        If Not Visible Then TrayRestore_Click()
    End Sub

    Private Sub Tabs_SelectedIndexChanged() Handles Tabs.SelectedIndexChanged
        If Tabs.SelectedTab IsNot Nothing Then
            CType(Tabs.SelectedTab.Controls(0), BrowserTab).Highlight = False
            CurrentTab = CType(Tabs.SelectedTab.Controls(0), BrowserTab)
            Tabs.SelectedTab.Font = Me.Font
            Tabs.SelectedTab.ForeColor = Color.Black
            PageB.Text = CurrentEdit.Page.Name
            UserB.Text = CurrentEdit.User.Name
            DrawHistory()
            DrawContribs()
            BrowserBackB.DropDown = CurrentTab.BackMenu
            BrowserForwardB.DropDown = CurrentTab.ForwardMenu
            RefreshInterface()
        End If
    End Sub

    Private Sub NewTab_Click() _
        Handles BrowserNewTab.Click, BrowserNewTabB.Click

        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Page IsNot Nothing Then
            Dim NewTabPage As New TabPage
            Dim NewBrowserTab As New BrowserTab

            NewBrowserTab.Parent = NewTabPage
            NewBrowserTab.ShowNewEdits = Config.ShowNewEdits
            NewBrowserTab.ShowNewContribs = False
            NewBrowserTab.Edit = CurrentEdit

            NewTabPage.Controls.Add(NewBrowserTab)
            NewTabPage.Controls(0).Dock = DockStyle.Fill
            NewTabPage.Text = CurrentEdit.Page.Name

            Tabs.ItemSize = New Size(150, 22)
            Tabs.TabPages.Add(NewTabPage)
            Tabs.SelectedTab = NewTabPage
            BrowserCloseTabB.Enabled = True
            BrowserCloseOthers.Enabled = True
        End If
    End Sub

    Private Sub CloseTab_Click() _
        Handles BrowserCloseTab.Click, BrowserCloseTabB.Click

        Dim SelectedIndex As Integer = Tabs.SelectedIndex
        If SelectedIndex > 0 Then Tabs.SelectedIndex = SelectedIndex - 1

        If Tabs.TabPages.Count > 2 Then
            Tabs.TabPages.RemoveAt(Tabs.SelectedIndex + 1)
        ElseIf Tabs.TabPages.Count = 2 Then
            Tabs.TabPages.RemoveAt(Tabs.SelectedIndex)
        End If

        If Tabs.TabPages.Count = 1 Then
            Tabs.ItemSize = New Size(1, 1)
            BrowserCloseTabB.Enabled = False
            BrowserCloseOthers.Enabled = False
        End If
    End Sub

    Private Sub HelpDocs_Click() Handles HelpDocs.Click
        Process.Start(Config.DocsLocation)
    End Sub

    Private Sub QueueTrim_Click() Handles QueueTrim.Click
        Dim NewQueueTrimForm As New QueueTrimForm

        If NewQueueTrimForm.ShowDialog = DialogResult.OK AndAlso NewQueueTrimForm.DiscardTime > 0 Then
            Dim i As Integer, Redraw As Boolean

            While i < CurrentQueue.Count
                If CurrentQueue(i).Time.AddMinutes(NewQueueTrimForm.DiscardTime) < Date.UtcNow Then
                    CurrentQueue.RemoveAt(i)
                    Redraw = True
                Else
                    i += 1
                End If
            End While

            If Redraw AndAlso Config.ShowQueue Then DrawQueue()
        End If
    End Sub

    Private Sub QueueClear_Click() Handles QueueClear.Click
        CurrentQueue.Clear()
        DrawQueue()
        DiffNextB.Enabled = False
    End Sub

    Private Sub BrowserCloseOtherTabs_Click() Handles BrowserCloseOthers.Click
        If CurrentTab IsNot Nothing Then
            For Each Item As TabPage In Tabs.TabPages
                If Item IsNot CurrentTab.Parent Then Tabs.TabPages.Remove(Item)
            Next Item
        End If
    End Sub

    Private Sub PageRequestProtection_Click() _
        Handles PageRequestProtection.Click

        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Page IsNot Nothing Then

            Dim NewReqProtectionForm As New ProtectionForm
            NewReqProtectionForm.ThisPage = CurrentEdit.Page

            If NewReqProtectionForm.ShowDialog = DialogResult.OK Then
                Dim NewRequest As New ReqProtectionRequest
                NewRequest.Page = CurrentEdit.Page
                NewRequest.Reason = NewReqProtectionForm.Reason.Text
                NewRequest.Type = NewReqProtectionForm.Type
                NewRequest.Start()
            End If
        End If
    End Sub

    Private Sub ShowNewEdits_Click() Handles BrowserNewEdits.Click
        CurrentTab.ShowNewEdits = BrowserNewEdits.Checked

        If CurrentTab.ShowNewEdits Then
            CurrentTab.ShowNewContribs = False
            BrowserNewContribs.Checked = False
        End If
    End Sub

    Private Sub ShowNewContribs_Click() Handles BrowserNewContribs.Click
        CurrentTab.ShowNewContribs = BrowserNewContribs.Checked

        If CurrentTab.ShowNewContribs Then
            CurrentTab.ShowNewEdits = False
            BrowserNewEdits.Checked = False
        End If
    End Sub

    Private Sub HelpAbout_Click() Handles HelpAbout.Click
        AboutForm.ShowDialog()
    End Sub

    Private Sub Tabs_DrawItem(ByVal s As Object, ByVal e As DrawItemEventArgs) Handles Tabs.DrawItem
        If e.Bounds.Width > 100 Then
            If CBool(e.State And DrawItemState.Selected) _
                Then e.Graphics.FillRectangle(New Pen(Color.FromKnownColor(KnownColor.ControlLightLight), 1).Brush, _
                e.Bounds) Else e.Graphics.DrawImage(My.Resources.gradient, e.Bounds.Left, e.Bounds.Top + 2, _
                e.Bounds.Width, e.Bounds.Height)

            Dim ThisTab As BrowserTab = CType(Tabs.TabPages(e.Index).Controls(0), BrowserTab)
            Dim DrawFont As Font = New Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Point)
            Dim DrawBrush As Brush = Brushes.Black
            Dim LayoutArea As New SizeF(10000, 20)
            Dim Text As String = Tabs.TabPages(e.Index).Text
            Dim MaxWidth As Integer = e.Bounds.Width - 6

            If ThisTab.Highlight Then
                DrawFont = New Font(DrawFont, FontStyle.Bold)
                If ThisTab.ShowNewEdits Then DrawBrush = Brushes.Red Else DrawBrush = Brushes.Blue
            End If

            If CBool(e.State And DrawItemState.Selected) Then
                DrawFont = New Font(DrawFont, FontStyle.Bold)
                MaxWidth -= 4
            End If

            Dim Width As Single = e.Graphics.MeasureString(Text, DrawFont, LayoutArea).Width

            If Not Width < MaxWidth Then

                While e.Graphics.MeasureString(Text & "...", DrawFont, LayoutArea).Width > MaxWidth
                    Text = Text.Substring(0, Text.Length - 1)
                End While

                Text &= "…"
            End If

            If CBool(e.State And DrawItemState.Selected) _
                Then e.Graphics.DrawString(Text, DrawFont, DrawBrush, e.Bounds.Left + 5, e.Bounds.Top + 6) _
                Else e.Graphics.DrawString(Text, DrawFont, DrawBrush, e.Bounds.Left + 1, e.Bounds.Top + 4)
        End If
    End Sub

    Private Sub BrowserOpen_Click() _
        Handles BrowserOpen.Click, BrowserOpenB.Click

        If CurrentTab IsNot Nothing AndAlso CurrentTab.CurrentUrl IsNot Nothing _
            Then Process.Start(CurrentTab.CurrentUrl)
    End Sub

    Private Sub SystemReloadConfig_Click()
        Dim NewGetConfigRequest As New ConfigRequest
        NewGetConfigRequest.GetUserConfigInThread()
    End Sub

    Private Sub ShowLog_Click() Handles SystemShowLog.Click
        Splitter.Panel2Collapsed = Not SystemShowLog.Checked
        Config.ShowLog = SystemShowLog.Checked
    End Sub

    Private Sub ShowQueue_Click() Handles SystemShowQueue.Click
        Config.ShowQueue = SystemShowQueue.Checked
        RefreshInterface()
    End Sub

    Private Sub RateUpdateTimer_Tick() Handles RateUpdateTimer.Tick
        Dim FirstTime As Date = Date.UtcNow, Edits, Reverts As Integer

        For Each Item As Edit In AllEditsById.Values
            If Item.Time > StartTime AndAlso Item.Time.AddMinutes(10) > Date.UtcNow Then
                Edits += 1
                If Item.Time < FirstTime Then FirstTime = Item.Time
                If Item.Type = EditType.Revert Then Reverts += 1
            End If
        Next Item

        If Edits > 0 AndAlso (Date.UtcNow - FirstTime).TotalMinutes > 0 _
            Then Stats.Text = CStr(CInt(Edits / (Date.UtcNow - FirstTime).TotalMinutes)) & " edits per minute, " _
            & CStr(CInt(Reverts / (Date.UtcNow - FirstTime).TotalMinutes)) & " reversions per minute"
    End Sub

    Private Sub Queue_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles Queue.MouseDown
        Dim Index As Integer = CInt((e.Y - 26) / 20) + QueueScroll.Value

        If Index > -1 AndAlso Index < CurrentQueue.Count Then
            DisplayEdit(CurrentQueue(Index))
            DrawQueue()
        End If
    End Sub

    Private Sub UserToPage_Click()
        If UserB.Text <> "" Then SetCurrentPage(GetPage("User talk:" & UserB.Text), True)
    End Sub

    Private Sub PageToUser_Click()
        If PageB.Text.StartsWith("User:") Then SetCurrentUser(GetUser(PageB.Text.Substring(5)), True) _
            Else If PageB.Text.StartsWith("User talk:") Then SetCurrentUser(GetUser(PageB.Text.Substring(10)), True)
    End Sub

    Private Sub UserInfo_Click() Handles UserInfo.Click, UserInfoB.Click
        If CurrentUser IsNot Nothing Then
            Dim NewUserInfoForm As New UserInfoForm
            NewUserInfoForm.ThisUser = CurrentUser
            NewUserInfoForm.Show()
        End If
    End Sub

    Private Sub PageProtect_Click() Handles PageProtect.Click
        If CurrentPage IsNot Nothing Then
            Dim NewProtectForm As New ProtectForm
            NewProtectForm.ThisPage = CurrentPage
            NewProtectForm.ShowDialog()
        End If
    End Sub

    Private Sub DrawTimer_Tick() Handles DrawTimer.Tick
        DrawHistory()
        DrawContribs()
        DrawQueue()
    End Sub

    Public Sub SetCurrentUser(ByVal User As User, ByVal DisplayLast As Boolean)
        If User IsNot Nothing Then
            If DisplayLast AndAlso User IsNot CurrentUser Then
                If User.LastEdit Is Nothing OrElse User.LastEdit.User Is Nothing Then
                    CurrentEdit = New Edit
                    CurrentEdit.Page = GetPage(PageB.Text)
                    CurrentEdit.User = User

                    Dim NewContribsRequest As New ContribsRequest
                    NewContribsRequest.User = User
                    NewContribsRequest.DisplayWhenDone = True
                    NewContribsRequest.Start()
                Else
                    CurrentEdit = User.LastEdit
                    DisplayEdit(CurrentEdit)
                End If

                If User.LastEdit Is NullEdit Then UserB.ForeColor = Color.Red
                RefreshInterface()
            End If

            If User IsNot CurrentUser Then ContribsOffset = 0
            If Not UserB.Items.Contains(User.Name) Then UserB.Items.Add(User.Name)
            UserB.Text = User.Name
            DrawContribs()
        End If
    End Sub

    Public Sub SetCurrentPage(ByVal Page As Page, ByVal DisplayLast As Boolean)
        If Page IsNot Nothing Then
            If DisplayLast AndAlso Page IsNot CurrentPage Then
                If Page.LastEdit Is Nothing Then
                    CurrentEdit = New Edit
                    CurrentEdit.Page = Page
                    CurrentEdit.User = GetUser(UserB.Text)

                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.Page = Page
                    NewHistoryRequest.DisplayWhenDone = True
                    NewHistoryRequest.Start()
                Else
                    CurrentEdit = Page.LastEdit
                    DisplayEdit(CurrentEdit)
                End If

                If Page.LastEdit Is NullEdit Then PageB.ForeColor = Color.Red
                RefreshInterface()
            End If

            If Page IsNot CurrentPage Then HistoryOffset = 0
            If Not PageB.Items.Contains(Page.Name) Then PageB.Items.Add(Page.Name)
            PageB.Text = Page.Name
            If CurrentEdit.Page IsNot Nothing Then Tabs.SelectedTab.Text = CurrentEdit.Page.Name
            DrawHistory()
        End If
    End Sub

    Private Sub PageB_ForeColorChanged() Handles PageB.ForeColorChanged
        If PageB.ForeColor = Color.Red AndAlso PageB.Items.Contains(PageB.Text) Then
            Dim Text As String = PageB.Text
            PageB.Items.Remove(PageB.Text)
            PageB.Text = Text
        End If
    End Sub

    Private Sub UserB_ForeColorChanged() Handles UserB.ForeColorChanged
        If UserB.ForeColor = Color.Red AndAlso UserB.Items.Contains(UserB.Text) Then
            Dim Text As String = UserB.Text
            UserB.Items.Remove(UserB.Text)
            UserB.Text = Text
        End If
    End Sub

    Private Sub PageB_Leave() Handles PageB.Leave
        If (PageB.Text = "" AndAlso CurrentPage IsNot Nothing) _
            Then PageB.Text = CurrentPage.Name _
            Else If (CurrentPage Is Nothing OrElse PageB.Text <> CurrentPage.Name) _
            Then SetCurrentPage(GetPage(PageB.Text), True)
    End Sub

    Private Sub UserB_Leave() Handles UserB.Leave
        If (UserB.Text = "" AndAlso CurrentUser IsNot Nothing) _
            Then UserB.Text = CurrentUser.Name _
            Else If (CurrentUser Is Nothing OrElse UserB.Text <> CurrentUser.Name) _
            Then SetCurrentUser(GetUser(UserB.Text), True)
    End Sub

    Private Sub PageB_SelectedIndexChanged() Handles PageB.SelectedIndexChanged
        If CurrentPage Is Nothing OrElse PageB.Text <> CurrentPage.Name Then SetCurrentPage(GetPage(PageB.Text), True)
    End Sub

    Private Sub UserB_SelectedIndexChanged() Handles UserB.SelectedIndexChanged
        If CurrentUser Is Nothing OrElse UserB.Text <> CurrentUser.Name Then SetCurrentUser(GetUser(UserB.Text), True)
    End Sub

    Public Sub AddToUndoList(ByVal ThisCommand As Command)
        Undo.Add(ThisCommand)

        Dim NewItem As New ToolStripMenuItem
        NewItem.Text = ThisCommand.Description
        NewItem.Tag = CObj(ThisCommand)
        AddHandler NewItem.Click, AddressOf UndoItemClicked
        UndoMenu.Items.Insert(0, NewItem)
        If UndoMenu.Items.Count > 10 Then UndoMenu.Items.RemoveAt(10)
        UndoB.Enabled = True
    End Sub

    Public Sub RemoveFromUndoList(ByVal ThisCommand As Command)
        Undo.Remove(ThisCommand)

        For Each Item As ToolStripItem In UndoMenu.Items
            If CType(Item.Tag, Command) Is ThisCommand Then
                UndoMenu.Items.Remove(Item)
                If UndoMenu.Items.Count = 0 Then UndoMenu.Enabled = False
                Exit For
            End If
        Next Item
    End Sub

    Private Sub UndoItemClicked(ByVal Sender As Object, ByVal e As EventArgs)
        Dim MenuItem As ToolStripItem = CType(Sender, ToolStripItem)

        For Each Item As Command In Undo
            If CType(MenuItem.Tag, Command) Is Item Then
                Undo.Remove(Item)
                UndoMenu.Items.Remove(MenuItem)
                If UndoMenu.Items.Count = 0 Then UndoMenu.Enabled = False

                Select Case Item.Type
                    Case CommandType.Edit, CommandType.Revert, CommandType.Warning, CommandType.Report
                        DoRevert(Item.Edit, False, Config.UndoSummary, True)

                    Case CommandType.Ignore
                        UnignoreUser(Item.User)

                    Case CommandType.Unignore
                        IgnoreUser(Item.User)
                End Select
                Exit For
            End If
        Next Item
    End Sub

    Private Sub WarnVandalism_Click() Handles WarnVandalism.Click
        Dim NewRequest As New WarningRequest
        NewRequest.Level = 0
        NewRequest.Edit = CurrentEdit
        NewRequest.Type = "warning"
        NewRequest.Start()
    End Sub

    Private Sub WarnSpam_Click() Handles WarnSpam.Click
        Dim NewRequest As New WarningRequest
        NewRequest.Level = 0
        NewRequest.Edit = CurrentEdit
        NewRequest.Type = "spam"
        NewRequest.Start()
    End Sub

    Private Sub WarnTest_Click() Handles WarnTest.Click
        Dim NewRequest As New WarningRequest
        NewRequest.Level = 0
        NewRequest.Edit = CurrentEdit
        NewRequest.Type = "test"
        NewRequest.Start()
    End Sub

    Private Sub WarnDelete_Click() Handles WarnDelete.Click
        Dim NewRequest As New WarningRequest
        NewRequest.Level = 0
        NewRequest.Edit = CurrentEdit
        NewRequest.Type = "delete"
        NewRequest.Start()
    End Sub

    Private Sub WarnAttacks_Click() Handles WarnAttack.Click
        Dim NewRequest As New WarningRequest
        NewRequest.Level = 0
        NewRequest.Edit = CurrentEdit
        NewRequest.Type = "attack"
        NewRequest.Start()
    End Sub

    Private Sub UserMessageWelcome_Click() Handles UserMessageWelcome.Click
        If CurrentUser IsNot Nothing Then
            Dim NewRequest As New UserMessageRequest
            NewRequest.User = CurrentUser
            If CurrentUser.Anonymous Then NewRequest.Message = Config.WelcomeAnon _
                Else NewRequest.Message = Config.Welcome
            NewRequest.AutoSign = True
            NewRequest.Summary = Config.WelcomeSummary
            NewRequest.Minor = Config.MinorNotifications
            NewRequest.AvoidText = "<!-- Template:Welcome"
            NewRequest.Start()
        End If
    End Sub

    Private Sub RevertItem_Click(ByVal Sender As Object, ByVal e As EventArgs)
        If CurrentEdit IsNot Nothing Then
            Dim MenuItem As ToolStripItem = CType(Sender, ToolStripItem)
            DoRevert(CurrentEdit, True, CStr(MenuItem.Tag))
        End If
    End Sub

    Private Sub TemplateItem_Click(ByVal Sender As Object, ByVal e As EventArgs)
        If CurrentUser IsNot Nothing Then
            Dim MenuItem As ToolStripItem = CType(Sender, ToolStripItem)
            Dim NewRequest As New UserMessageRequest
            NewRequest.User = CurrentUser
            NewRequest.Message = "{{subst:" & CStr(MenuItem.Tag) & "}}"
            NewRequest.AutoSign = True
            NewRequest.Summary = "Notification: {{[[Template:" & CStr(MenuItem.Tag) & "|" & CStr(MenuItem.Tag) & "]]}}"
            NewRequest.Minor = Config.MinorNotifications
            NewRequest.AvoidText = "<!-- Template:" & CStr(MenuItem.Tag)
            NewRequest.Start()
        End If
    End Sub

    Private Sub GoToItem_Click(ByVal Sender As Object, ByVal e As EventArgs)
        If CurrentUser IsNot Nothing Then
            Dim MenuItem As ToolStripItem = CType(Sender, ToolStripItem)
            SetCurrentPage(GetPage(CStr(MenuItem.Tag)), True)
        End If
    End Sub

    Private Sub Status_ItemActivate() Handles Status.ItemActivate
        If Status.SelectedItems.Count > 0 AndAlso Status.SelectedItems(0).Tag IsNot Nothing _
            AndAlso TypeOf Status.SelectedItems(0).Tag Is Edit _
            Then DisplayEdit(CType(Status.SelectedItems(0).Tag, Edit))
    End Sub

    Private Sub SystemReconnectIRC_Click() Handles SystemReconnectIRC.Click
        EditQueue.Clear()
        DrawQueue()
        DiffNextB.Enabled = False
        Irc.Reconnect = True
    End Sub

    Private Sub HelpFeedback_Click() Handles HelpFeedback.Click
        Process.Start(Config.FeedbackLocation)
    End Sub

    Private Sub PageB_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles PageB.KeyDown
        If e.KeyCode = Keys.Enter AndAlso PageB.Text <> "" _
            AndAlso (CurrentPage Is Nothing OrElse CurrentPage.Name <> PageB.Text) Then

            SetCurrentPage(GetPage(PageB.Text), True)
            Queue.Focus()
        End If

        If e.KeyCode = Keys.Escape Then
            PageB.Text = CurrentPage.Name
            Queue.Focus()
        End If

        If e.KeyCode = Keys.OemOpenBrackets OrElse e.KeyCode = Keys.OemCloseBrackets _
            OrElse (e.Shift AndAlso (e.KeyCode = Keys.Oemcomma OrElse e.KeyCode = Keys.OemPeriod)) _
            Then e.SuppressKeyPress = True
    End Sub

    Private Sub UserB_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles UserB.KeyDown
        If e.KeyCode = Keys.Enter AndAlso UserB.Text <> "" _
            AndAlso (CurrentUser Is Nothing OrElse CurrentUser.Name <> UserB.Text) Then

            SetCurrentUser(GetUser(UserB.Text), True)
            Queue.Focus()
        End If

        If e.KeyCode = Keys.Escape Then
            UserB.Text = CurrentUser.Name
            Queue.Focus()
        End If

        If e.KeyCode = Keys.OemOpenBrackets OrElse e.KeyCode = Keys.OemCloseBrackets _
            OrElse (e.Shift AndAlso (e.KeyCode = Keys.Oemcomma OrElse e.KeyCode = Keys.OemPeriod)) _
            Then e.SuppressKeyPress = True
    End Sub

    Private Sub SystemOptions_Click() Handles SystemOptions.Click
        Dim NewConfigForm As New ConfigForm
        NewConfigForm.ShowDialog()
    End Sub

    Private Sub Stats_Click() Handles Stats.Click, SystemStats.Click
        StatsForm.Show()
    End Sub

    Private Sub PageB_TextChanged() Handles PageB.TextChanged
        PageB.ForeColor = Color.Black
        HistoryOffset = 0
    End Sub

    Private Sub UserB_TextChanged() Handles UserB.TextChanged
        UserB.ForeColor = Color.Black
        ContribsOffset = 0
    End Sub

    Private Sub PageMarkPatrolled_Click() Handles PageMarkPatrolled.Click
        If CurrentPage IsNot Nothing Then
            Dim NewPatrolRequest As New PatrolRequest
            NewPatrolRequest.Page = CurrentPage()
            NewPatrolRequest.Start()
        End If
    End Sub

    Private Sub CancelB_Click() Handles CancelB.Click
        Dim i, CancelledRequests As Integer

        While i < Status.Items.Count
            If Status.Items(i).ForeColor = Color.Red AndAlso TypeOf Status.Items(i).Tag Is Request Then
                CType(Status.Items(i).Tag, Request).Cancel()
                CancelledRequests += 1
            Else
                i += 1
            End If
        End While

        If CancelledRequests = 1 Then Log("Cancelled 1 request") _
            Else Log("Cancelled " & CStr(CancelledRequests) & " requests")

        PendingWarnings.Clear()
    End Sub

    Private Sub Speedy_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim NewSpeedyRequest As New SpeedyRequest
        NewSpeedyRequest.AutoNotify = True
        NewSpeedyRequest.Criterion = SpeedyCriteria(CType(sender, ToolStripMenuItem).Name.Substring(6))
        NewSpeedyRequest.Page = CurrentPage
        NewSpeedyRequest.Start()
    End Sub

    Private Sub PageNominate_Click() Handles PageNominate.Click
        If CurrentPage IsNot Nothing Then
            Dim NewXfdForm As New XfdForm
            NewXfdForm.Page = CurrentPage
            NewXfdForm.ShowDialog()
        End If
    End Sub

    Private Sub SystemSaveLog_Click() Handles SystemSaveLog.Click
        Dim Dialog As New SaveFileDialog, LogItems As New List(Of String)

        For Each Item As ListViewItem In Status.Items
            If Item.ForeColor <> Color.Red Then LogItems.Add(Item.SubItems(1).Text)
        Next Item

        Dialog.Title = "Save log"
        Dialog.FileName = "huggle-log.txt"
        Dialog.Filter = "Text file|*.txt"

        If Dialog.ShowDialog = DialogResult.OK Then
            File.WriteAllLines(Dialog.FileName, LogItems.ToArray)
            Log("Saved log")
        End If
    End Sub

    Private Sub PageShowHistoryPage_Click() Handles PageShowHistoryPage.Click
        If CurrentPage IsNot Nothing Then DisplayUrl(Config.SitePath & "w/index.php?title=" & _
            UrlEncode(CurrentPage.Name.Replace(" ", "_")) & "&action=history")
    End Sub

    Private Sub WarnError_Click() Handles WarnError.Click
        Dim NewRequest As New WarningRequest
        NewRequest.Level = 0
        NewRequest.Edit = CurrentEdit
        NewRequest.Type = "error"
        NewRequest.Start()
    End Sub

    Private Sub WarnUnsourced_Click() Handles WarnUnsourced.Click
        Dim NewRequest As New WarningRequest
        NewRequest.Level = 0
        NewRequest.Edit = CurrentEdit
        NewRequest.Type = "unsourced"
        NewRequest.Start()
    End Sub

    Private Sub WarnNpov_Click() Handles WarnNpov.Click
        Dim NewRequest As New WarningRequest
        NewRequest.Level = 0
        NewRequest.Edit = CurrentEdit
        NewRequest.Type = "npov"
        NewRequest.Start()
    End Sub

    Private Sub CreateContribsQueue(ByVal Result As Boolean)
        If CurrentUser IsNot Nothing Then
            Dim ThisEdit As Edit = CurrentUser.LastEdit, NewQueue As New List(Of Edit), i As Integer

            While i < Config.ContribsBlockSize AndAlso ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
                NewQueue.Add(ThisEdit)
                ThisEdit = ThisEdit.PrevByUser
                i += 1
            End While

            CurrentQueue = NewQueue
        End If
    End Sub

    Private Sub QueueSource_SelectedIndexChanged() _
        Handles QueueSource.SelectedIndexChanged

        Select Case QueueSource.SelectedItem.ToString
            Case "Filtered changes" : CurrentQueue = EditQueue
            Case "New pages" : CurrentQueue = NewPageQueue
            Case "All changes" : CurrentQueue = AllEditsByTime

            Case "Recent user contribs"
                If CurrentUser IsNot Nothing Then
                    Dim i As Integer = 0, ThisEdit As Edit = CurrentUser.LastEdit

                    While i < Config.ContribsBlockSize AndAlso ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
                        ThisEdit = ThisEdit.PrevByUser
                        i += 1
                    End While

                    If i < Config.ContribsBlockSize Then
                        Dim NewContribsRequest As New ContribsRequest
                        NewContribsRequest.User = CurrentUser
                        NewContribsRequest.Start(AddressOf CreateContribsQueue)
                    Else
                        CreateContribsQueue(True)
                    End If
                End If

            Case "Add..."
                Dim NewQueueForm As New QueueForm
                NewQueueForm.ShowDialog()

            Case Else
                Dim NewQueue As New List(Of Edit)

                For Each Item As String In QueueSources(QueueSource.SelectedItem.ToString)
                    Dim Page As Page = GetPage(Item)

                    If Page.LastEdit IsNot Nothing Then
                        NewQueue.Add(Page.LastEdit)
                    Else
                        Dim NewEdit As New Edit
                        NewEdit.Page = Page
                        NewEdit.Id = "cur"
                        NewEdit.Oldid = "prev"
                        NewQueue.Add(NewEdit)
                    End If
                Next Item

                CurrentQueue = NewQueue
        End Select

        DrawQueue()
    End Sub

    Private Sub QueueEditSources_Click() Handles QueueEditSources.Click
        Dim NewQueueForm As New QueueForm
        NewQueueForm.ShowDialog()
    End Sub

    Private Sub RevertWarnVandalism_Click() Handles RevertWarnVandalism.Click
        RevertAndWarn("warning")
    End Sub

    Private Sub RevertWarnSpam_Click() Handles RevertWarnSpam.Click
        RevertAndWarn("spam")
    End Sub

    Private Sub RevertWarnTest_Click() Handles RevertWarnTest.Click
        RevertAndWarn("test")
    End Sub

    Private Sub RevertWarnDelete_Click() Handles RevertWarnDelete.Click
        RevertAndWarn("delete")
    End Sub

    Private Sub RevertWarnAttack_Click() Handles RevertWarnAttack.Click
        RevertAndWarn("attack")
    End Sub

    Private Sub RevertWarnError_Click() Handles RevertWarnError.Click
        RevertAndWarn("error")
    End Sub

    Private Sub RevertWarnNpov_Click() Handles RevertWarnNpov.Click
        RevertAndWarn("npov")
    End Sub

    Private Sub RevertWarnUnsourced_Click() Handles RevertWarnUnsourced.Click
        RevertAndWarn("unsourced")
    End Sub

    Private Sub RevertAndWarnAdvanced_Click() Handles RevertWarnAdvanced.Click
        Dim NewRevertAndWarnForm As New RevertAndWarnForm
        NewRevertAndWarnForm.User = CurrentUser
        NewRevertAndWarnForm.ShowDialog()
    End Sub

    Private Sub QueueScroll_Scroll() Handles QueueScroll.Scroll
        DrawQueue()
    End Sub

    Private Sub PagePurge_Click() Handles PagePurge.Click
        If CurrentPage IsNot Nothing Then
            Dim NewRequest As New PurgeRequest
            NewRequest.Page = CurrentPage
            NewRequest.Start()
        End If
    End Sub

    Private Sub GoMyTalk_Click() Handles GoMyTalk.Click
        SetCurrentPage(GetPage("User talk:" & Config.Username), True)
    End Sub

    Private Sub GoMyContribs_Click() Handles GoMyContribs.Click
        SetCurrentUser(GetUser(Config.Username), True)
    End Sub

    Private Sub SystemRequests_Click() Handles SystemRequests.Click
        RequestsForm.Show()
    End Sub

    Private Sub SystemLogOut_Click() Handles SystemLogOut.Click
        LoggingOut = True
        Irc.Disconnect()

        Dim NewLoginForm As New LoginForm
        NewLoginForm.Show()
        Close()
    End Sub

End Class