Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Web.HttpUtility

Class Main

    Public Reverting, DisplayingEdit As Boolean
    Private LoggingOut As Boolean

    'Timer is enabled when a key is pressed and prevents both MainForm.Main.KeyDown and
    'BrowserTab.Browser.PreviewKeyDown being raised at the same time, which sometimes happens depending on 
    'what is currently focused in the browser and what was last focused outside it
    Private WithEvents KeyDelayTimer As New Windows.Forms.Timer

    Public Sub Initialize()
        Icon = My.Resources.huggle_icon
        TrayIcon.Icon = My.Resources.huggle_icon
        KeyDelayTimer.Interval = 10
        ScrollTimer.Interval = 1000 \ Config.HistoryScrollSpeed

        'Temporary bugfix
        If Config.RollbackSummary IsNot Nothing Then _
            Config.RollbackSummary = Config.RollbackSummary.Replace("$1", "$3").Replace("$2", "$1").Replace("$3", "$2")

        InitialTab.Parent = Tabs.TabPages(0)
        CurrentTab = InitialTab

        CurrentQueue = Queue.Default
        SecondQueue = Queue.SecondDefault

        Location = New Point(Math.Max(32, Config.WindowPosition.X), Math.Max(32, Config.WindowPosition.Y))
        Size = New Size(Math.Max(Config.WindowSize.Width, MinimumSize.Width), _
            Math.Max(Config.WindowSize.Height, MinimumSize.Height))
        If Config.WindowMaximize Then WindowState = FormWindowState.Maximized Else WindowState = FormWindowState.Normal

        SystemReconnectIRC.Enabled = Config.IrcMode

        StartTime = Date.UtcNow

        For Each Item As String In LogBuffer
            Log(Item)
        Next Item

        Configure()
    End Sub

    Sub DrawHistory()
        If CurrentPage Is Nothing Then Exit Sub

        History.Page = CurrentPage
        HistoryStrip.Refresh()
        HistoryScrollRB.Enabled = (History.Offset > 0)

        Dim Edit As Edit = CurrentPage.LastEdit
        Dim X As Integer = History.Width - 18 + (History.Offset * 17)
        Dim EnableScroll As Boolean

        While Edit IsNot Nothing AndAlso Edit IsNot NullEdit
            If X < 0 Then
                EnableScroll = True
                Exit While
            End If

            X -= 17
            Edit = Edit.Prev
        End While

        HistoryScrollLB.Enabled = EnableScroll
        If CurrentPage.FirstEdit Is Nothing Then HistoryB.Enabled = True
    End Sub

    Sub DrawContribs()
        If CurrentUser Is Nothing Then Exit Sub

        Contribs.User = CurrentUser
        ContribsStrip.Refresh()
        ContribsScrollRB.Enabled = (Contribs.Offset > 0)

        Dim Edit As Edit = CurrentUser.LastEdit
        Dim X As Integer = Contribs.Width - 18 + (Contribs.Offset * 17)
        Dim EnableScroll As Boolean

        While Edit IsNot Nothing AndAlso Edit IsNot NullEdit
            If X < 0 Then
                EnableScroll = True
                Exit While
            End If

            X -= 17
            Edit = Edit.PrevByUser
        End While

        ContribsScrollLB.Enabled = EnableScroll
        If CurrentEdit.User.FirstEdit Is Nothing Then ContribsB.Enabled = True
    End Sub

    Sub DrawQueues()
        If CurrentQueue IsNot Nothing AndAlso CurrentQueue.Edits IsNot Nothing AndAlso QueueContainer.Visible Then
            Dim QueueHeight As Integer = (QueueArea.Height \ 20) - 2

            If QueueHeight < CurrentQueue.Edits.Count Then
                QueueScroll.Enabled = True
                QueueScroll.Maximum = CurrentQueue.Edits.Count - 2
                QueueScroll.Minimum = 0
                QueueScroll.SmallChange = 1
                QueueScroll.LargeChange = Math.Max(1, QueueHeight)
            Else
                QueueScroll.Enabled = False
                QueueScroll.Value = 0
            End If

            QueueArea.Draw(CurrentQueue, QueueScroll.Value)
        End If

        If SecondQueue IsNot Nothing AndAlso SecondQueue.Edits IsNot Nothing AndAlso QueueContainer.Visible _
            AndAlso Config.ShowTwoQueues Then

            Dim QueueHeight As Integer = (QueueArea2.Height \ 20) - 2

            If QueueHeight < SecondQueue.Edits.Count Then
                QueueScroll2.Enabled = True
                QueueScroll2.Maximum = SecondQueue.Edits.Count - 2
                QueueScroll2.Minimum = 0
                QueueScroll2.SmallChange = 1
                QueueScroll2.LargeChange = Math.Max(1, QueueHeight)
            Else
                QueueScroll2.Enabled = False
                QueueScroll2.Value = 0
            End If

            QueueArea2.Draw(SecondQueue, QueueScroll2.Value)
        End If
    End Sub

    Private Sub Main_ResizeShown() Handles Me.Resize, Me.Shown
        Status.Columns(1).Width = Width - 30
        HistoryStrip.Left = MainStrip.Right
        ContribsStrip.Left = MainStrip.Right
        HistoryStrip.Width = Width - MainStrip.Right - 3
        ContribsStrip.Width = Width - MainStrip.Right - 3
        History.Width = Width - MainStrip.Right - 330
        Contribs.Width = Width - MainStrip.Right - 330

        If Visible Then
            DrawHistory()
            DrawContribs()
        End If
    End Sub

    Private Sub ShowStartupPage() Handles Me.Shown
        If Config.StartupPage IsNot Nothing Then
            Dim NewRequest As New BrowserRequest
            NewRequest.NoFormatting = True
            NewRequest.Url = SitePath() & "index.php?title=" & UrlEncode(Config.StartupPage) & "&action=render"
            NewRequest.Start()
        End If
    End Sub

    Private Sub Main_FormClosing() Handles Me.FormClosing
        TrayIcon.Visible = False
        Visible = False
        If Not LoggingOut Then ClosingForm.ShowDialog()
    End Sub

    Private Sub Revert_Click() Handles RevertB.ButtonClick
        DoRevert(CurrentEdit)
    End Sub

    Sub DiffNextB_Click() Handles QueueNext.Click, NextDiffB.Click
        ShowNextEdit()
    End Sub

    Private Sub UserIgnore_Click() Handles UserIgnore.Click, UserIgnoreB.Click
        If CurrentUser IsNot Nothing Then
            If CurrentUser.Ignored Then UnignoreUser(CurrentUser) Else IgnoreUser(CurrentUser)
        End If
    End Sub

    Public Sub IgnoreUser(ByVal User As User)
        'Sets the username to be ignored
        User.Ignored = True
        If Not WhitelistManualChanges.Contains(User.Name) Then WhitelistManualChanges.Add(User.Name)
        'Says that the user will be ignored (in log)
        Log("Ignored user '" & User.Name & "'")

        'Add undo menu item
        For Each Item As Command In Undo
            If Item.Type = CommandType.Unignore AndAlso Item.User Is User Then
                RemoveFromUndoList(Item)
                Exit For
            End If
        Next Item

        Dim NewCommand As New Command
        NewCommand.User = User
        NewCommand.Description = Msg("main-user-ignore") & " " & User.Name
        NewCommand.Type = CommandType.Ignore
        AddToUndoList(NewCommand)

        If User Is CurrentUser Then
            RefreshInterface()
            DrawHistory()
        End If
    End Sub

    Public Sub UnignoreUser(ByVal User As User)
        User.Ignored = False
        If WhitelistManualChanges.Contains(User.Name) Then WhitelistManualChanges.Remove(User.Name)
        If WhitelistAutoChanges.Contains(User.Name) Then WhitelistAutoChanges.Remove(User.Name)
        Log("Unignored user '" & User.Name & "'")

        'Add undo menu item
        For Each Item As Command In Undo
            If Item.Type = CommandType.Ignore AndAlso Item.User Is User Then
                RemoveFromUndoList(Item)
                Exit For
            End If
        Next Item

        Dim NewCommand As New Command
        NewCommand.User = User
        NewCommand.Description = Msg("main-user-unignore") & " " & User.Name
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

    Public HistorySelectionStart As Integer

    Private Sub History_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles History.MouseDown
        If CurrentEdit IsNot Nothing Then
            HistorySelectionStart = CInt((History.Width - e.X - 10) / 17) + History.Offset

            If e.Button = MouseButtons.Left Then
                History.NewerEdit = CurrentEdit
                History.OlderEdit = CurrentEdit.Prev

                DisplayHistoryItem(HistorySelectionStart)

            ElseIf e.Button = MouseButtons.Right Then
                History.NewerEdit = CurrentPage.LastEdit

                Dim ThisEdit As Edit = CurrentPage.LastEdit

                For i As Integer = 0 To HistorySelectionStart - 1
                    If ThisEdit.Prev Is Nothing OrElse ThisEdit.Prev Is NullEdit Then Exit Sub
                    ThisEdit = ThisEdit.Prev
                Next i

                History.OlderEdit = ThisEdit

                ShowDiffToCur(ThisEdit)
            End If

            DrawHistory()
        End If
    End Sub

    Private Sub History_MouseUp(ByVal s As Object, ByVal e As MouseEventArgs) Handles History.MouseUp
        If CurrentEdit IsNot Nothing Then
            If HistorySelectionStart > 0 AndAlso e.Button = MouseButtons.Left Then
                Dim Position As Integer = CInt((History.Width - e.X - 10) / 17) + History.Offset

                If Position <> HistorySelectionStart Then
                    Dim OlderEdit As Edit = CurrentPage.LastEdit

                    For i As Integer = 0 To Math.Max(Position, HistorySelectionStart) - 1
                        If OlderEdit.Prev Is Nothing OrElse OlderEdit.Prev Is NullEdit Then Exit Sub
                        OlderEdit = OlderEdit.Prev
                    Next i

                    History.OlderEdit = OlderEdit

                    Dim NewerEdit As Edit = CurrentPage.LastEdit

                    For i As Integer = 0 To Math.Min(Position, HistorySelectionStart) - 1
                        If NewerEdit.Prev Is Nothing OrElse NewerEdit.Prev Is NullEdit Then Exit Sub
                        NewerEdit = NewerEdit.Prev
                    Next i

                    History.NewerEdit = NewerEdit

                    ShowDiffBetween(OlderEdit, NewerEdit)
                End If

                HistorySelectionStart = 0
            End If
        End If
    End Sub

    Private Sub History_MouseMove(ByVal s As Object, ByVal e As MouseEventArgs) Handles History.MouseMove
        If CurrentPage IsNot Nothing AndAlso CurrentPage.LastEdit IsNot Nothing Then

            Dim Position As Integer = CInt((History.Width - e.X - 10) / 17) + History.Offset
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
            EditInfo.Top = 78
            EditInfo.Visible = True
        End If
    End Sub

    Private Sub HistoryContribs_MouseLeave() Handles History.MouseLeave, Contribs.MouseLeave
        EditInfo.Visible = False
    End Sub

    Private Sub Contribs_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles Contribs.MouseDown

        If CurrentUser IsNot Nothing AndAlso CurrentUser.LastEdit IsNot Nothing Then
            Dim Position As Integer = CInt((Contribs.Width - e.X - 10) / 17) + Contribs.Offset
            Dim Edit As Edit = CurrentUser.LastEdit

            For i As Integer = 0 To Position - 1
                If Edit.PrevByUser Is Nothing OrElse Edit.PrevByUser Is NullEdit Then Exit Sub
                Edit = Edit.PrevByUser
            Next i

            Contribs.SelectedEdit = Edit
            DisplayEdit(Edit)
        End If

        DrawContribs()
    End Sub

    Private Sub Contribs_MouseMove(ByVal s As Object, ByVal e As MouseEventArgs) Handles Contribs.MouseMove

        If CurrentUser IsNot Nothing AndAlso CurrentUser.LastEdit IsNot Nothing Then
            Dim Position As Integer = CInt((Contribs.Width - e.X - 10) / 17) + Contribs.Offset
            Dim Edit As Edit = CurrentUser.LastEdit

            For i As Integer = 0 To Position - 1
                If Edit.PrevByUser Is Nothing OrElse Edit.PrevByUser Is NullEdit Then
                    EditInfo.Visible = False
                    Exit Sub
                End If

                Edit = Edit.PrevByUser
            Next i

            EditInfo.SetEdit(Edit, EditInfoPanel.DisplayMode.UserName)
            EditInfo.Left = Width - EditInfo.Width - 12
            EditInfo.Top = 78
            EditInfo.Visible = True
        End If
    End Sub

    Private Sub ViewHistory_Click() Handles PageHistory.Click, HistoryB.Click
        If CurrentPage IsNot Nothing Then
            Dim NewHistoryRequest As New HistoryRequest
            NewHistoryRequest.Page = CurrentPage
            NewHistoryRequest.Start(AddressOf GotHistory)
        End If
    End Sub

    Private Sub GotHistory(ByVal Result As RequestResult)
        If Not Result.Error Then DrawHistory()
    End Sub

    Private Sub UserContribs_Click() Handles UserContribs.Click, ContribsB.Click
        If CurrentUser IsNot Nothing Then
            Dim NewRequest As New ContribsRequest
            NewRequest.User = CurrentUser
            NewRequest.Start(AddressOf GotContribs)
        End If
    End Sub

    Private Sub GotContribs(ByVal Result As RequestResult)
        If Not Result.Error Then
            DrawHistory()
            DrawContribs()
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
            History.Offset += 1
            DrawHistory()
            If Not HistoryScrollLB.Enabled Then ScrollTimer.Stop()
        End If
    End Sub

    Sub HistoryRight()
        If HistoryScrollRB.Enabled Then
            If History.Offset > 0 Then
                History.Offset -= 1
                DrawHistory()
            End If

            If Not HistoryScrollRB.Enabled Then ScrollTimer.Stop()
        End If
    End Sub

    Sub ContribsLeft()
        If ContribsScrollLB.Enabled Then
            Contribs.Offset += 1
            DrawContribs()
            If Not ContribsScrollLB.Enabled Then ScrollTimer.Stop()
        End If
    End Sub

    Sub ContribsRight()
        If ContribsScrollRB.Enabled Then
            If Contribs.Offset > 0 Then
                Contribs.Offset -= 1
                DrawContribs()
            End If

            If Not ContribsScrollRB.Enabled Then ScrollTimer.Stop()
        End If
    End Sub

    Sub PageViewLatest_Click() Handles PageViewLatest.Click
        If CurrentPage IsNot Nothing Then DisplayUrl _
            (SitePath() & "index.php?title=" & UrlEncode(CurrentPage.Name.Replace(" ", "_")))
    End Sub

    Sub PageView_Click() Handles PageViewB.Click, RevisionView.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentPage IsNot Nothing Then DisplayUrl _
            (SitePath() & "index.php?title=" & UrlEncode(CurrentPage.Name) & "&oldid=" & CurrentEdit.Id)
    End Sub

    Private Sub DiffRevertSummary_Click() Handles RevertAdvanced.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Prev IsNot Nothing Then
            Dim NewRevertForm As New RevertForm

            NewRevertForm.Edit = CurrentEdit

            If NewRevertForm.ShowDialog = DialogResult.OK Then
                RevertB.Enabled = False
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
            RevertB.Enabled = True
            If CurrentEdit.User IsNot Nothing AndAlso Not CurrentEdit.User.Ignored Then RevertWarnB.Enabled = True
        End If
    End Sub

    Private Sub SystemExit_Click() Handles SystemExit.Click
        Close()
    End Sub

    Private Sub UserTalk_Click() Handles UserTalk.Click, UserTalkB.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.User IsNot Nothing Then
            Dim NewBrowserRequest As New BrowserRequest
            NewBrowserRequest.Tab = CurrentTab
            NewBrowserRequest.Url = SitePath() & "index.php?title=" & UrlEncode(CurrentUser.TalkPage.Name)
            NewBrowserRequest.Start()

            Dim NewWarnLevelRequest As New WarningLogRequest
            NewWarnLevelRequest.User = CurrentEdit.User
            NewWarnLevelRequest.Start()
        End If
    End Sub

    Private Sub HistoryScrollLast_Click()
        If History.Offset > 0 Then
            History.Offset = 0
            DrawHistory()
        End If
    End Sub

    Private Sub ContribsScrollLast_Click()
        If Contribs.Offset > 0 Then
            Contribs.Offset = 0
            DrawContribs()
        End If
    End Sub

    Private Sub RcReqTimer_Tick() Handles RcReqTimer.Tick
        If Not Config.IrcMode Then
            RcReqTimer.Stop()

            Dim NewRcApiRequest As New RcApiRequest
            NewRcApiRequest.Start()
        End If
    End Sub

    Private Sub UserMessage_Click() _
        Handles UserMessage.Click, UserMessageB.Click, UserMessageOther.Click

        If CurrentUser IsNot Nothing Then
            Dim NewMessageForm As New MessageForm
            NewMessageForm.User = CurrentUser
            NewMessageForm.ShowDialog()
        End If
    End Sub

    Private Sub Warn_Click() Handles WarnAdvanced.Click
        If CurrentUser IsNot Nothing Then
            Dim NewWarningForm As New WarningForm
            NewWarningForm.User = CurrentUser

            If NewWarningForm.ShowDialog() = DialogResult.OK Then WarnB.Enabled = False
        End If
    End Sub

    Private Sub RevertWarnB_ButtonClick() Handles RevertWarnB.ButtonClick
        RevertAndWarn()
    End Sub

    Public Sub RevertAndWarn(Optional ByVal WarnType As String = "warning", _
        Optional ByVal Level As UserLevel = UserLevel.None, Optional ByVal Summary As String = Nothing, _
        Optional ByVal CurrentOnly As Boolean = False)

        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Prev IsNot Nothing Then

            'Get confirmation if needed
            If Config.ConfirmMultiple AndAlso CurrentEdit.User IsNot Nothing _
                AndAlso CurrentEdit.User Is CurrentEdit.Prev.User _
                AndAlso MessageBox.Show("This will revert multiple edits by '" & CurrentEdit.User.Name & "'. Continue?", _
                "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub

            If DoRevert(CurrentEdit, Summary, CurrentOnly:=CurrentOnly) Then
                'Be sure not to warn twice for the same edit
                Dim i As Integer = 0

                While i < PendingWarnings.Count - 1
                    If PendingWarnings(i).Page Is CurrentEdit.Page Then PendingWarnings.RemoveAt(i) Else i += 1
                End While

                If Level <> UserLevel.None Then CurrentEdit.LevelToWarn = Level
                CurrentEdit.TypeToWarn = WarnType

                PendingWarnings.Add(CurrentEdit)

                'If AutoAdvance is turned on in the config then Show the next edit
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

    Private Sub TagPage_Click() Handles PageTagB.Click, PageTag.Click
        If CurrentEdit.Page IsNot Nothing Then
            Dim NewTagForm As New TagForm

            NewTagForm.Page = CurrentEdit.Page
            NewTagForm.ToSpeedy.Enabled = PageTagSpeedy.Enabled
            NewTagForm.ToProd.Enabled = PageTagProd.Enabled
            NewTagForm.ShowDialog()
        End If
    End Sub

    Sub TagSpeedy_Click() Handles PageTagSpeedy.Click
        If CurrentPage IsNot Nothing Then
            Dim NewSpeedyTagForm As New SpeedyForm
            NewSpeedyTagForm.Page = CurrentPage
            NewSpeedyTagForm.ShowDialog()
        End If
    End Sub

    Sub PageDelete_Click() Handles PageDelete.Click, PageDeleteB.Click
        If CurrentPage IsNot Nothing Then
            Dim NewDeleteForm As New DeleteForm
            NewDeleteForm.Page = CurrentPage
            NewDeleteForm.Show()
        End If
    End Sub

    Sub PageTagProd_Click() Handles PageTagProd.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentPage IsNot Nothing Then
            Dim NewForm As New ProdForm
            NewForm.Page = CurrentPage
            NewForm.ShowDialog()
        End If
    End Sub

    Private Sub PageMove_Click() Handles PageMove.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Page IsNot Nothing Then

            Dim NewMovePageForm As New MoveForm
            NewMovePageForm.Page = CurrentEdit.Page

            If NewMovePageForm.ShowDialog = DialogResult.OK Then
                Dim NewMoveRequest As New MoveRequest
                NewMoveRequest.Page = CurrentEdit.Page
                NewMoveRequest.Target = NewMovePageForm.Target.Text
                NewMoveRequest.Summary = NewMovePageForm.Reason.Text
                NewMoveRequest.Start()
            End If
        End If
    End Sub

    Sub Log(ByVal Message As String, Optional ByVal Tag As Object = Nothing, _
        Optional ByVal InProgress As Boolean = False)

        Dim NewItem As New ListViewItem
        If InProgress Then NewItem.ForeColor = Color.Red
        If Tag IsNot Nothing Then NewItem.Tag = Tag
        NewItem.SubItems.Add(Date.Now.Year.ToString & "-" & Date.Now.Month.ToString.PadLeft(2, "0"c) _
            & "-" & Date.Now.Day.ToString.PadLeft(2, "0"c) & " " & Date.Now.ToLongTimeString _
            & UtcOffset() & " -- " & Message)

        If InProgress Then
            Status.Items.Insert(0, NewItem)
        Else
            Dim i As Integer = 0

            While i < Status.Items.Count
                If Status.Items(i).ForeColor <> Color.Red Then
                    Status.Items.Insert(i, NewItem)
                    Exit Sub
                End If

                i += 1
            End While

            Status.Items.Insert(0, NewItem)
        End If

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
            Else Return " (" & CStr(If(TimeZone.CurrentTimeZone.GetUtcOffset(Date.Now).Minutes < 0, "-", "+")) _
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

    Private Sub SystemShowNewMessages_Click() Handles SystemMessages.Click
        DisplayEdit(User.Me.TalkPage.LastEdit)
        SystemMessages.Enabled = False
    End Sub

    Sub BlockUser(ByVal ThisUser As User)
        Dim NewBlockForm As New BlockForm

        NewBlockForm.User = ThisUser
        NewBlockForm.Show()
    End Sub

    Sub ReportUser(ByVal User As User, Optional ByVal Edit As Edit = Nothing)
        Dim NewUserReport As New ReportForm
        NewUserReport.User = User
        NewUserReport.Edit = Edit
        NewUserReport.Show()
    End Sub

    Private Sub HistoryDiffToCur_Click() Handles HistoryDiffToCurB.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Prev IsNot Nothing Then ShowDiffToCur(CurrentEdit.Prev)
    End Sub

    Private Sub WatchPage_Click() Handles PageWatch.Click, PageWatchB.Click
        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Page IsNot Nothing Then
            If Watchlist.Contains(CurrentEdit.Page.SubjectPage) Then
                Dim NewRequest As New UnwatchRequest
                NewRequest.Page = CurrentEdit.Page.SubjectPage
                NewRequest.Manual = True
                NewRequest.Start()
            Else
                Dim NewRequest As New WatchRequest
                NewRequest.Page = CurrentEdit.Page.SubjectPage
                NewRequest.Manual = True
                NewRequest.Start()
            End If
        End If
    End Sub

    Private Sub LogContextCopy_Click() Handles LogCopy.Click
        If Status.SelectedIndices.Count > 0 _
            Then Clipboard.SetText(Status.Items(Status.SelectedIndices(0)).SubItems(1).Text)
    End Sub

    Private Sub TrayRestore_Click() Handles TrayRestore.Click
        Visible = Not Visible
        If Visible Then TrayRestore.Text = Msg("minimizewindow") Else TrayRestore.Text = Msg("restorewindow")
    End Sub

    Private Sub TrayExit_Click() Handles TrayExit.Click
        Close()
    End Sub

    Private Sub TrayIcon_DoubleClick() Handles TrayIcon.DoubleClick
        TrayRestore_Click()
    End Sub

    Private Sub TrayIcon_BalloonTipClicked() Handles TrayIcon.BalloonTipClicked
        If SystemMessages.Enabled Then SystemShowNewMessages_Click()
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
            BrowserCloseTab.Enabled = True
        End If
    End Sub

    Private Sub CloseTab_Click() _
        Handles BrowserCloseTab.Click, BrowserCloseTabB.Click

        Dim SelectedIndex As Integer = Tabs.SelectedIndex
        If SelectedIndex > 0 Then Tabs.SelectedIndex = (SelectedIndex - 1)

        Tabs.TabPages.RemoveAt(SelectedIndex)

        If Tabs.TabPages.Count = 1 Then
            Tabs.ItemSize = New Size(1, 1)
            BrowserCloseTabB.Enabled = False
            BrowserCloseOthers.Enabled = False
            BrowserCloseTab.Enabled = False
        End If
    End Sub

    Private Sub HelpDocs_Click() Handles HelpDocumentation.Click
        OpenUrlInBrowser(Config.DocsLocation)
    End Sub

    Private Sub QueueTrim_Click() Handles QueueTrim.Click
        Dim NewQueueTrimForm As New QueueTrimForm
        NewQueueTrimForm.ShowDialog()
    End Sub

    Private Sub QueueClear_Click() Handles QueueClear.Click
        CurrentQueue.Reset()
        DrawQueues()
        RefreshInterface()
    End Sub

    Private Sub QueueClearAll_Click() Handles QueueClearAll.Click
        For Each Item As Queue In Queue.All.Values
            Item.Reset()
        Next Item

        DrawQueues()
        RefreshInterface()
    End Sub

    Private Sub BrowserCloseOtherTabs_Click() Handles BrowserCloseOthers.Click
        If CurrentTab IsNot Nothing Then
            For Each Item As TabPage In Tabs.TabPages
                If Item IsNot CurrentTab.Parent Then Tabs.TabPages.Remove(Item)
            Next Item
        End If
    End Sub

    Private Sub PageRequestProtection_Click() _
        Handles PageReqProtection.Click

        If CurrentEdit IsNot Nothing AndAlso CurrentEdit.Page IsNot Nothing Then

            Dim NewReqProtectionForm As New ProtectionForm
            NewReqProtectionForm.Page = CurrentEdit.Page

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
            Then OpenUrlInBrowser(CurrentTab.CurrentUrl)
    End Sub

    Private Sub SystemReloadConfig_Click()
        Dim NewRequest As New ConfigRequest
        NewRequest.Start()
    End Sub

    Private Sub ShowLog_Click() Handles SystemShowLog.Click
        Config.ShowLog = SystemShowLog.Checked
        RefreshInterface()
    End Sub

    Private Sub ShowQueue_Click() Handles SystemShowQueue.Click
        Config.ShowQueue = SystemShowQueue.Checked
        RefreshInterface()
    End Sub

    Private Sub RateUpdateTimer_Tick() Handles RateUpdateTimer.Tick
        Dim FirstTime As Date = Date.UtcNow, Edits, Reverts As Integer

        For Each Item As Edit In Edit.All.Values
            If Item.Time > StartTime AndAlso Item.Time.AddMinutes(10) > Date.UtcNow Then
                Edits += 1
                If Item.Time < FirstTime Then FirstTime = Item.Time
                If Item.Type = EditType.Revert Then Reverts += 1
            End If
        Next Item

        If Edits > 0 AndAlso (Date.UtcNow - FirstTime).TotalMinutes > 0 _
            Then MenuStats.Text = Msg("main-stats", CStr(CInt(Edits / (Date.UtcNow - FirstTime).TotalMinutes)), _
            CStr(CInt(Reverts / (Date.UtcNow - FirstTime).TotalMinutes)))
    End Sub

    Private Sub QueueArea_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles QueueArea.MouseDown
        If CurrentQueue IsNot Nothing Then
            Dim Index As Integer = CInt((e.Y - 26) / 20) + QueueScroll.Value

            If Index > -1 AndAlso Index < CurrentQueue.Edits.Count Then
                DisplayEdit(CurrentQueue.Edits(Index))
                DrawQueues()
            End If
        End If
    End Sub

    Private Sub QueueArea2_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles QueueArea2.MouseDown
        If SecondQueue IsNot Nothing Then
            Dim Index As Integer = CInt((e.Y - 26) / 20) + QueueScroll2.Value

            If Index > -1 AndAlso Index < SecondQueue.Edits.Count Then
                DisplayEdit(SecondQueue.Edits(Index))
                DrawQueues()
            End If
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
            NewUserInfoForm.User = CurrentUser
            NewUserInfoForm.Show()
        End If
    End Sub

    Private Sub PageProtect_Click() Handles PageProtect.Click
        If CurrentPage IsNot Nothing Then
            Dim NewProtectForm As New ProtectForm
            NewProtectForm.Page = CurrentPage
            NewProtectForm.ShowDialog()
        End If
    End Sub

    Public Sub SetCurrentUser(ByVal User As User, Optional ByVal DisplayLast As Boolean = False)
        If User IsNot Nothing AndAlso MainForm IsNot Nothing AndAlso MainForm.Visible Then
            If DisplayLast AndAlso User IsNot CurrentUser Then
                If User.LastEdit Is Nothing OrElse User.LastEdit.User Is Nothing Then
                    CurrentEdit = New Edit
                    CurrentEdit.Page = GetPage(PageB.Text)
                    CurrentEdit.User = User

                    Dim NewContribsRequest As New ContribsRequest
                    NewContribsRequest.User = User
                    NewContribsRequest.DisplayWhenDone = True
                    NewContribsRequest.Start(AddressOf GotContribs)
                Else
                    CurrentEdit = User.LastEdit
                    DisplayEdit(CurrentEdit)
                End If

                If User.LastEdit Is NullEdit Then UserB.ForeColor = Color.Red
                RefreshInterface()
            End If

            If CurrentUser Is User Then Contribs.SelectedEdit = CurrentEdit

            DisplayingEdit = True
            If User IsNot CurrentUser Then Contribs.Offset = 0
            If Not UserB.Items.Contains(User.Name) Then UserB.Items.Add(User.Name)
            UserB.Text = User.Name
            DrawContribs()
            DisplayingEdit = False
        End If
    End Sub

    Public Sub SetCurrentPage(ByVal Page As Page, Optional ByVal DisplayLast As Boolean = False)
        If Page IsNot Nothing AndAlso MainForm IsNot Nothing AndAlso MainForm.Visible Then
            If DisplayLast AndAlso Page IsNot CurrentPage Then
                If Page.LastEdit Is Nothing Then
                    CurrentEdit = New Edit
                    CurrentEdit.Page = Page
                    CurrentEdit.User = GetUser(UserB.Text)

                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.Page = Page
                    NewHistoryRequest.Start(AddressOf GotPageContent)
                Else
                    CurrentEdit = Page.LastEdit
                    DisplayEdit(CurrentEdit)
                End If

                If Page.LastEdit Is NullEdit Then PageB.ForeColor = Color.Red
                RefreshInterface()
            End If

            If Page Is CurrentPage AndAlso CurrentEdit.Id IsNot Nothing Then
                If Edit.All.ContainsKey(CurrentEdit.Id) Then History.NewerEdit = Edit.All(CurrentEdit.Id)
                If Edit.All.ContainsKey(CurrentEdit.Oldid) Then History.OlderEdit = Edit.All(CurrentEdit.Oldid) _
                    Else History.OlderEdit = CurrentEdit.Prev
            End If

            DisplayingEdit = True
            If Page IsNot CurrentPage Then History.Offset = 0
            If Not PageB.Items.Contains(Page.Name) Then PageB.Items.Add(Page.Name)
            PageB.Text = Page.Name
            If CurrentPage IsNot Nothing Then Tabs.SelectedTab.Text = CurrentPage.Name
            DisplayingEdit = False
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
        If Not DisplayingEdit AndAlso _
            (CurrentPage Is Nothing OrElse PageB.Text <> CurrentPage.Name) Then SetCurrentPage(GetPage(PageB.Text), True)
    End Sub

    Private Sub UserB_SelectedIndexChanged() Handles UserB.SelectedIndexChanged
        If Not DisplayingEdit AndAlso _
            (CurrentUser Is Nothing OrElse UserB.Text <> CurrentUser.Name) Then SetCurrentUser(GetUser(UserB.Text), True)
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
                If UndoMenu.Items.Count = 0 Then UndoB.Enabled = False
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
                If UndoMenu.Items.Count = 0 Then UndoB.Enabled = False

                Select Case Item.Type
                    Case CommandType.Edit, CommandType.Revert, CommandType.Warning, CommandType.Report
                        DoRevert(Item.Edit, Config.UndoSummary, Undoing:=True)

                    Case CommandType.Ignore
                        UnignoreUser(Item.User)

                    Case CommandType.Unignore
                        IgnoreUser(Item.User)
                End Select
                Exit For
            End If
        Next Item
    End Sub

    Private Sub UserMessageWelcome_Click() Handles UserMessageWelcome.Click
        If CurrentUser IsNot Nothing Then
            Dim NewRequest As New UserMessageRequest
            NewRequest.User = CurrentUser
            If CurrentUser.Anonymous Then NewRequest.Message = Config.WelcomeAnon _
                Else NewRequest.Message = Config.Welcome
            NewRequest.AutoSign = True
            NewRequest.Summary = Config.WelcomeSummary
            NewRequest.Minor = Config.Minor("note")
            NewRequest.Watch = Config.Watch("note")
            NewRequest.AvoidText = "<!-- Template:Welcome"
            NewRequest.Start()
        End If
    End Sub

    Private Sub RevertItem_Click(ByVal Sender As Object, ByVal e As EventArgs)
        If CurrentEdit IsNot Nothing Then
            Dim MenuItem As ToolStripItem = CType(Sender, ToolStripItem)
            DoRevert(CurrentEdit, CStr(MenuItem.Tag))
        End If
    End Sub

    Private Sub TemplateItem_Click(ByVal Sender As Object, ByVal e As EventArgs)
        If CurrentUser IsNot Nothing Then
            Dim MenuItem As ToolStripItem = CType(Sender, ToolStripItem)
            Dim NewRequest As New UserMessageRequest
            NewRequest.User = CurrentUser
            NewRequest.Message = "{{subst:" & CStr(MenuItem.Tag) & "}}"
            NewRequest.AutoSign = True
            NewRequest.Summary = Config.TemplateMessageSummary.Replace("$1", MenuItem.Text)
            NewRequest.Minor = Config.Minor("note")
            NewRequest.Watch = Config.Watch("note")
            NewRequest.AvoidText = "<!-- Template:" & CStr(MenuItem.Tag)
            NewRequest.Start()
        End If
    End Sub

    Private Sub GoToItem_Click(ByVal Sender As Object, ByVal e As EventArgs)
        Dim MenuItem As ToolStripItem = CType(Sender, ToolStripItem)
        SetCurrentPage(GetPage(CStr(MenuItem.Tag)), True)
    End Sub

    Private Sub Status_ItemActivate() Handles Status.ItemActivate
        If Status.SelectedItems.Count > 0 AndAlso Status.SelectedItems(0).Tag IsNot Nothing _
            AndAlso TypeOf Status.SelectedItems(0).Tag Is Edit _
            Then DisplayEdit(CType(Status.SelectedItems(0).Tag, Edit))
    End Sub

    Private Sub SystemReconnectIRC_Click() Handles SystemReconnectIRC.Click
        For Each Item As Queue In Queue.All.Values
            Item.Reset()
        Next Item

        DrawQueues()
        NextDiffB.Enabled = False
        Log("Reconnecting to IRC recent changes feed")
        Irc.Reconnect()
    End Sub

    Private Sub HelpFeedback_Click() Handles HelpFeedback.Click
        OpenUrlInBrowser(Config.FeedbackLocation)
    End Sub

    Private Sub PageB_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles PageB.KeyDown
        If e.KeyCode = Keys.Enter AndAlso PageB.Text <> "" _
            AndAlso (CurrentPage Is Nothing OrElse CurrentPage.Name <> PageB.Text) Then

            SetCurrentPage(GetPage(PageB.Text), True)
            QueueArea.Focus()
        End If

        If e.KeyCode = Keys.Escape Then
            PageB.Text = CurrentPage.Name
            QueueArea.Focus()
        End If

        If e.KeyCode = Keys.OemOpenBrackets OrElse e.KeyCode = Keys.OemCloseBrackets _
            OrElse (e.Shift AndAlso (e.KeyCode = Keys.Oemcomma OrElse e.KeyCode = Keys.OemPeriod)) _
            Then e.SuppressKeyPress = True
    End Sub

    Private Sub UserB_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles UserB.KeyDown
        If e.KeyCode = Keys.Enter AndAlso UserB.Text <> "" _
            AndAlso (CurrentUser Is Nothing OrElse CurrentUser.Name <> UserB.Text) Then

            SetCurrentUser(GetUser(UserB.Text), True)
            QueueArea.Focus()
        End If

        If e.KeyCode = Keys.Escape Then
            UserB.Text = CurrentUser.Name
            QueueArea.Focus()
        End If

        If e.KeyCode = Keys.OemOpenBrackets OrElse e.KeyCode = Keys.OemCloseBrackets _
            OrElse (e.Shift AndAlso (e.KeyCode = Keys.Oemcomma OrElse e.KeyCode = Keys.OemPeriod)) _
            Then e.SuppressKeyPress = True
    End Sub

    Private Sub SystemOptions_Click() Handles SystemOptions.Click
        Dim NewConfigForm As New ConfigForm
        NewConfigForm.ShowDialog()
    End Sub

    Private Sub Stats_Click() Handles MenuStats.Click, SystemStatistics.Click
        StatsForm.Show()
    End Sub

    Private Sub PageB_TextChanged() Handles PageB.TextChanged
        PageB.ForeColor = Color.Black
        History.Offset = 0
    End Sub

    Private Sub UserB_TextChanged() Handles UserB.TextChanged
        UserB.ForeColor = Color.Black
        Contribs.Offset = 0
    End Sub

    Private Sub PageMarkPatrolled_Click() Handles PagePatrol.Click
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
        NewSpeedyRequest.Criterion = Config.SpeedyCriteria(CType(sender, ToolStripMenuItem).Name.Substring(6))
        NewSpeedyRequest.Page = CurrentPage
        NewSpeedyRequest.Start()
    End Sub

    Private Sub PageNominate_Click() Handles PageXfd.Click
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

        Dialog.Title = Msg("main-savelogtitle")
        Dialog.FileName = "huggle-log.txt"
        Dialog.Filter = "Text file|*.txt"

        If Dialog.ShowDialog = DialogResult.OK Then
            File.WriteAllLines(Dialog.FileName, LogItems.ToArray)
            Log("Saved log")
        End If
    End Sub

    Private Sub PageShowHistoryPage_Click() Handles PageHistoryPage.Click
        If CurrentPage IsNot Nothing Then DisplayUrl _
            (SitePath() & "index.php?title=" & UrlEncode(CurrentPage.Name.Replace(" ", "_")) & "&action=history")
    End Sub

    Private Sub QueueSelector_SelectedIndexChanged() Handles QueueSelector.SelectedIndexChanged
        If QueueSelector.SelectedItem.ToString = Msg("main-addqueue") Then
            Dim NewQueueForm As New QueueForm
            NewQueueForm.ShowDialog()
        ElseIf Queue.All.ContainsKey(QueueSelector.SelectedItem.ToString) Then
            CurrentQueue = Queue.All(QueueSelector.SelectedItem.ToString)
            If CurrentQueue.Type = QueueType.Dynamic AndAlso Not CurrentQueue.RefreshAlways Then CurrentQueue.ForceUpdate()
        Else
            CurrentQueue = Nothing
        End If

        DrawQueues()
        RefreshInterface()
    End Sub

    Private Sub QueueSelector2_SelectedIndexChanged() Handles QueueSelector2.SelectedIndexChanged
        If QueueSelector2.SelectedItem.ToString = Msg("main-addqueue") Then
            Dim NewQueueForm As New QueueForm
            NewQueueForm.ShowDialog()
        ElseIf Queue.All.ContainsKey(QueueSelector2.SelectedItem.ToString) Then
            SecondQueue = Queue.All(QueueSelector2.SelectedItem.ToString)
            If SecondQueue.Type = QueueType.Dynamic AndAlso Not SecondQueue.RefreshAlways Then SecondQueue.ForceUpdate()
        Else
            SecondQueue = Nothing
        End If

        DrawQueues()
        RefreshInterface()
    End Sub

    Private Sub QueueEditSources_Click() Handles QueueOptions.Click
        Dim NewQueueForm As New QueueForm
        NewQueueForm.Show()
    End Sub

    Private Sub RevertAndWarnAdvanced_Click() Handles RevertWarnAdvanced.Click
        Dim NewRevertAndWarnForm As New RevertAndWarnForm
        NewRevertAndWarnForm.User = CurrentUser
        NewRevertAndWarnForm.ShowDialog()
    End Sub

    Private Sub QueueScroll_Scroll() Handles QueueScroll.Scroll, QueueScroll2.Scroll
        DrawQueues()
    End Sub

    Private Sub PagePurge_Click() Handles PagePurge.Click
        If CurrentPage IsNot Nothing Then
            Dim NewRequest As New PurgeRequest
            NewRequest.Page = CurrentPage
            NewRequest.Start()
        End If
    End Sub

    Private Sub GoMyTalk_Click() Handles GotoMyTalk.Click
        SetCurrentPage(User.Me.TalkPage, True)
    End Sub

    Private Sub GoMyContribs_Click() Handles GotoMyContribs.Click
        SetCurrentUser(User.Me, True)
    End Sub

    Private Sub SystemRequests_Click() Handles SystemRequests.Click
        RequestsForm.Show()
    End Sub

    Private Sub SystemLogOut_Click() Handles SystemLogOut.Click
        LoggingOut = True
        Irc.Disconnect()

        'Save everything
        SaveLocalConfig()
        SaveLists()
        SaveQueues()
        SaveWhitelist()

        'Clear various things
        Edit.All.Clear()
        Page.ClearAll()
        Queue.All.Clear()
        AllRequests.Clear()
        User.ClearAll()
        PendingRequests.Clear()
        PendingWarnings.Clear()
        Undo.Clear()
        Watchlist.Clear()
        Whitelist.Clear()
        Request.ClearCookies()

        Dim NewLoginForm As New LoginForm
        NewLoginForm.Show()
        Close()
    End Sub

    Private Sub UserEmail_Click() Handles UserEmail.Click
        Dim NewForm As New EmailForm
        NewForm.User = CurrentUser
        NewForm.Show()
    End Sub

    Private Sub PageSwitchTalk_Click() Handles PageSwitchTalk.Click
        If CurrentPage IsNot Nothing Then If CurrentPage.IsTalkPage _
            Then SetCurrentPage(CurrentPage.SubjectPage, True) _
            Else SetCurrentPage(CurrentPage.TalkPage, True)
    End Sub

    Private Sub LogClear_Click() Handles LogClear.Click
        Status.Items.Clear()
    End Sub

    Private Sub SystemListBuilder_Click() Handles SystemLists.Click
        Dim NewForm As New ListForm
        NewForm.Show()
    End Sub

    Private Sub UserReportVandalism_Click() Handles UserReportVandalism.Click
        If CurrentUser IsNot Nothing Then ReportUser(CurrentUser, CurrentEdit)
    End Sub

    Private Sub UserReportUsername_Click() Handles UserReportUsername.Click
        Dim NewForm As New ReportForm
        NewForm.User = CurrentUser
        NewForm.Edit = CurrentEdit
        NewForm.Reason.Text = "Inappropriate username"
        NewForm.Message.Text = "inappropriate username"
        NewForm.Show()
    End Sub

    Private Sub UserReport3rr_Click() Handles UserReport3rr.Click
        Dim NewForm As New Report3rrForm
        NewForm.User = CurrentUser
        NewForm.Show()
    End Sub

    Private Sub UserBlockB_Click() Handles UserBlockB.Click
        If CurrentUser IsNot Nothing Then BlockUser(CurrentUser)
    End Sub

    Public Sub StartRevert()
        RevertB.Enabled = False
        RevertWarnB.Enabled = False
        Reverting = True
        RevertTimer.Interval = 5000
        RevertTimer.Start()
    End Sub

    Private Sub DiffRevertCurrentOnly_Click() Handles RevertCurrentOnly.Click
        DoRevert(CurrentEdit, CurrentOnly:=True)
    End Sub

    Private Sub WarnItem_Click(ByVal Sender As Object, ByVal e As EventArgs)
        Dim NewRequest As New WarningRequest
        NewRequest.Level = 0
        NewRequest.Edit = CurrentEdit
        NewRequest.Type = CType(Sender, ToolStripMenuItem).Tag.ToString
        NewRequest.Start()
    End Sub

    Private Sub RevertWarnItem_Click(ByVal Sender As Object, ByVal e As EventArgs)
        RevertAndWarn(CType(Sender, ToolStripMenuItem).Tag.ToString)
    End Sub

    Private Sub RevisionSight_Click() Handles RevisionSight.Click
        If CurrentEdit IsNot Nothing Then
            Dim NewRequest As New SightRequest
            NewRequest.Edit = CurrentEdit
            NewRequest.Start()
        End If
    End Sub

    Private Sub SightAndNext_Click() Handles SightAndNext.Click
        RevisionSight_Click()
        ShowNextEdit()
    End Sub

    Private Sub UserReportSock_Click() Handles UserReportSock.Click
        If CurrentUser IsNot Nothing Then
            Dim NewForm As New SockReportForm
            NewForm.User = CurrentUser
            NewForm.Show()
        End If
    End Sub

    Private Sub QueueArea_Paint() Handles QueueArea.Paint
        DrawQueues()
    End Sub

    Private Sub SystemShowTwoQueues_Click() Handles SystemShowTwoQueues.Click
        Config.ShowTwoQueues = SystemShowTwoQueues.Checked
        QueueContainer.Panel2Collapsed = (Not Config.ShowTwoQueues)
    End Sub

End Class
