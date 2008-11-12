Imports System.Text.RegularExpressions

Class QueueForm

    Private ReadOnly Property CurrentQueue() As Queue
        Get
            If QueueList.SelectedIndex = -1 Then Return Nothing Else Return Queue.All(QueueList.SelectedItem.ToString)
        End Get
    End Property

    Private Sub QueueForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("queue-title")
        Localize(Me, "queue")
        Tip.Active = Config.ShowToolTips

        QueueList.BeginUpdate()

        For Each Item As String In Queue.All.Keys
            QueueList.Items.Add(Item)
        Next Item

        QueueList.EndUpdate()

        For Each Item As Space In Space.All
            Namespaces.Items.Add(Item)
        Next Item

        SetListSelector()
        If QueueList.Items.Count > 0 AndAlso QueueList.Items.Count > MainForm.QueueSelector.SelectedIndex _
            Then QueueList.SelectedIndex = MainForm.QueueSelector.SelectedIndex
        RefreshInterface()
    End Sub

    Private Sub QueueForm_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        Misc.CurrentQueue = Me.CurrentQueue
        QueueNames(Config.Project).Clear()

        For Each Item As String In QueueList.Items
            QueueNames(Config.Project).Add(Item)
        Next Item

        MainForm.SetQueueSelector()
    End Sub

    Private Sub QueueForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub AddQueue_Click() Handles Add.Click
        Dim i As Integer = Queue.All.Count + 1

        While Queue.All.ContainsKey("Queue" & CStr(i))
            i += 1
        End While

        Dim Name As String = "Queue" & CStr(i)

        Dim NewQueue As New Queue(Name)
        QueueList.Items.Add(Name)
        QueueList.SelectedItem = Name
    End Sub

    Private Sub ApplyFilters_Click() Handles Apply.Click
        If CurrentQueue.Pages IsNot Nothing Then
            Dim i As Integer

            While i < CurrentQueue.Pages.Count
                If CurrentQueue.MatchesFilter(CurrentQueue.Pages(i)) Then i += 1 Else CurrentQueue.Pages.RemoveAt(i)
            End While
        End If
    End Sub

    Private Sub Copy_Click() Handles Copy.Click
        Dim NewName As String = InputBox.Show("Copy to:", "Queue" & CStr(Queue.All.Count + 1))

        If NewName.Length > 0 Then
            If Queue.All.ContainsKey(NewName) Then
                MessageBox.Show("A queue with the name '" & NewName & "' already exists. Choose another name.", _
                    "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Dim NewQueue As New Queue(NewName)
                NewQueue.Pages.AddRange(CurrentQueue.Pages)
                QueueList.Items.Add(NewName)
                QueueList.SelectedItem = NewName
                MainForm.SetQueueSelector()
            End If
        End If
    End Sub

    Private Sub DeleteQueue_Click() Handles Delete.Click
        If QueueList.SelectedIndex > -1 AndAlso MessageBox.Show("Delete queue '" & CurrentQueue.Name & "'?", _
            "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) _
            = DialogResult.Yes Then

            Dim Index As Integer = QueueList.SelectedIndex
            Queue.All.Remove(QueueList.SelectedItem.ToString)
            If Index > 0 Then QueueList.SelectedIndex -= 1

            QueueList.Items.RemoveAt(Index)
            QueueList.SelectedIndex = QueueList.Items.Count - 1
            MainForm.SetQueueSelector()
        End If
    End Sub

    Private Sub FilterAnonymous_CheckStateChanged() Handles FilterAnonymous.CheckStateChanged
        CurrentQueue.FilterAnonymous = CType(CInt(FilterAnonymous.State), QueueFilter)
    End Sub

    Private Sub FilterAssisted_CheckStateChanged() Handles FilterAssisted.CheckStateChanged
        CurrentQueue.FilterAssisted = CType(CInt(FilterAssisted.State), QueueFilter)
    End Sub

    Private Sub FilterBot_CheckStateChanged() Handles FilterBot.CheckStateChanged
        CurrentQueue.FilterBot = CType(CInt(FilterBot.State), QueueFilter)
    End Sub

    Private Sub FilterHuggle_CheckStateChanged() Handles FilterHuggle.CheckStateChanged
        CurrentQueue.FilterHuggle = CType(CInt(FilterHuggle.State), QueueFilter)
    End Sub

    Private Sub FilterIgnored_CheckStateChanged() Handles FilterIgnored.CheckStateChanged
        CurrentQueue.FilterIgnored = CType(CInt(FilterIgnored.State), QueueFilter)
    End Sub

    Private Sub FilterMe_CheckStateChanged() Handles FilterMe.CheckStateChanged
        CurrentQueue.FilterMe = CType(CInt(FilterMe.State), QueueFilter)
    End Sub

    Private Sub FilterNewPage_CheckStateChanged() Handles FilterNewPage.CheckStateChanged
        CurrentQueue.FilterNewPage = CType(CInt(FilterNewPage.State), QueueFilter)
    End Sub

    Private Sub FilterNotifications_CheckStateChanged() Handles FilterNotifications.CheckStateChanged
        CurrentQueue.FilterNotifications = CType(CInt(FilterNotifications.State), QueueFilter)
    End Sub

    Private Sub FilterOwnUserspace_CheckStateChanged() Handles FilterOwnUserspace.CheckStateChanged
        CurrentQueue.FilterOwnUserspace = CType(CInt(FilterOwnUserspace.State), QueueFilter)
    End Sub

    Private Sub FilterReverts_CheckStateChanged() Handles FilterReverts.CheckStateChanged
        CurrentQueue.FilterReverts = CType(CInt(FilterReverts.State), QueueFilter)
    End Sub

    Private Sub FilterTags_CheckStateChanged() Handles FilterTags.CheckStateChanged
        CurrentQueue.FilterTags = CType(CInt(FilterTags.State), QueueFilter)
    End Sub

    Private Sub FilterWarnings_CheckStateChanged() Handles FilterWarnings.CheckStateChanged
        CurrentQueue.FilterWarnings = CType(CInt(FilterWarnings.State), QueueFilter)
    End Sub

    Private Sub IgnorePages_CheckedChanged() Handles IgnorePages.CheckedChanged
        CurrentQueue.IgnorePages = IgnorePages.Checked
    End Sub

    Private Sub ListBuilder_Click() Handles ListBuilder.Click
        Dim NewForm As New ListForm
        NewForm.Show()
    End Sub

    Private Sub ListSelector_SelectedIndexChanged() Handles ListSelector.SelectedIndexChanged
        If CurrentQueue IsNot Nothing Then
            If ListSelector.SelectedIndex = 0 Then CurrentQueue.ListName = Nothing _
                Else CurrentQueue.ListName = ListSelector.Text

            QueuePages.BeginUpdate()
            QueuePages.Items.Clear()

            If CurrentQueue.Pages IsNot Nothing Then
                For Each Item As String In CurrentQueue.Pages
                    QueuePages.Items.Add(Item)
                Next Item
            End If

            QueuePages.EndUpdate()
        End If
    End Sub

    Private Sub Namespaces_ItemCheck(ByVal s As Object, ByVal e As ItemCheckEventArgs) Handles Namespaces.ItemCheck
        If e.NewValue = CheckState.Checked Then
            If Not CurrentQueue.Spaces.Contains(CType(Namespaces.Items(e.Index), Space)) _
                Then CurrentQueue.Spaces.Add(CType(Namespaces.Items(e.Index), Space))
        Else
            If CurrentQueue.Spaces.Contains(CType(Namespaces.Items(e.Index), Space)) _
                Then CurrentQueue.Spaces.Remove(CType(Namespaces.Items(e.Index), Space))
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Close()
    End Sub

    Private Sub PageRegex_Leave() Handles PageRegex.Leave
        CurrentQueue.PageRegex = PageRegex.Regex
    End Sub

    Private Sub Preload_CheckedChanged() Handles Preload.CheckedChanged
        If CurrentQueue IsNot Nothing Then CurrentQueue.Preload = Preload.Checked
    End Sub

    Private Sub QueueList_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles QueueList.KeyDown
        If e.KeyCode = Keys.Delete AndAlso QueueList.SelectedIndex > -1 Then DeleteQueue_Click()
    End Sub

    Private Sub QueueList_SelectedIndexChanged() Handles QueueList.SelectedIndexChanged
        QueuePages.Items.Clear()

        If CurrentQueue IsNot Nothing Then
            If CurrentQueue.Pages IsNot Nothing Then
                QueuePages.BeginUpdate()

                For Each Item As String In CurrentQueue.Pages
                    QueuePages.Items.Add(Item)
                Next Item
            End If

            QueuePages.EndUpdate()

            Select Case CurrentQueue.Type
                Case QueueType.FixedList : FixedList.Checked = True
                Case QueueType.LiveList : LiveList.Checked = True
                Case QueueType.Live : Live.Checked = True
                Case QueueType.Dynamic : DynamicList.Checked = True
            End Select

            Select Case CurrentQueue.SortOrder
                Case QueueSortOrder.Time : SortOrder.SelectedIndex = 0
                Case QueueSortOrder.TimeReverse : SortOrder.SelectedIndex = 1
                Case QueueSortOrder.Quality : SortOrder.SelectedIndex = 2
            End Select

            FilterAnonymous.State = CType(CInt(CurrentQueue.FilterAnonymous), CheckState)
            FilterAssisted.State = CType(CInt(CurrentQueue.FilterAssisted), CheckState)
            FilterBot.State = CType(CInt(CurrentQueue.FilterBot), CheckState)
            FilterHuggle.State = CType(CInt(CurrentQueue.FilterHuggle), CheckState)
            FilterIgnored.State = CType(CInt(CurrentQueue.FilterIgnored), CheckState)
            FilterMe.State = CType(CInt(CurrentQueue.FilterMe), CheckState)
            FilterNewPage.State = CType(CInt(CurrentQueue.FilterNewPage), CheckState)
            FilterNotifications.State = CType(CInt(CurrentQueue.FilterNotifications), CheckState)
            FilterOwnUserspace.State = CType(CInt(CurrentQueue.FilterOwnUserspace), CheckState)
            FilterReverts.State = CType(CInt(CurrentQueue.FilterReverts), CheckState)
            FilterTags.State = CType(CInt(CurrentQueue.FilterTags), CheckState)
            FilterWarnings.State = CType(CInt(CurrentQueue.FilterWarnings), CheckState)

            If CurrentQueue.PageRegex Is Nothing Then PageRegex.Text = Nothing _
                Else PageRegex.Text = CurrentQueue.PageRegex.ToString
            If CurrentQueue.SummaryRegex Is Nothing Then SummaryRegex.Text = Nothing _
                Else SummaryRegex.Text = CurrentQueue.SummaryRegex.ToString
            If CurrentQueue.UserRegex Is Nothing Then UserRegex.Text = Nothing _
                Else UserRegex.Text = CurrentQueue.UserRegex.ToString

            DynamicSourceType.Text = CurrentQueue.DynamicSourceType
            DynamicSource.Text = CurrentQueue.DynamicSource
            RefreshAlways.Checked = CurrentQueue.RefreshAlways
            RefreshInterval.Value = CurrentQueue.RefreshInterval
            RefreshReAdd.Checked = CurrentQueue.RefreshReAdd

            IgnorePages.Checked = CurrentQueue.IgnorePages
            RemoveAfter.Checked = (CurrentQueue.RemoveAfter > 0)
            If CurrentQueue.RemoveAfter > RemoveAfterTime.Minimum Then RemoveAfterTime.Value = _
                CurrentQueue.RemoveAfter Else RemoveAfterTime.Value = RemoveAfterTime.Minimum
            RemoveOld.Checked = CurrentQueue.RemoveOld
            RemoveViewed.Checked = CurrentQueue.RemoveViewed
            Preload.Checked = CurrentQueue.Preload

            Users.Items.AddRange(CurrentQueue.Users.ToArray)

            If CurrentQueue.Pages Is Nothing Then ListSelector.SelectedIndex = 0 _
                Else ListSelector.SelectedItem = CurrentQueue.ListName

            If DynamicSourceType.SelectedIndex = -1 Then DynamicSourceType.SelectedIndex = 0

            For i As Integer = 0 To Namespaces.Items.Count - 1
                Namespaces.SetItemChecked(i, CurrentQueue.Spaces.Contains(CType(Namespaces.Items(i), Space)))
            Next i
        End If

        RefreshInterface()
    End Sub

    Private Sub RemoveAfterChanged() Handles RemoveAfter.CheckedChanged, RemoveAfterTime.ValueChanged
        If CurrentQueue IsNot Nothing Then If RemoveAfter.Checked _
            Then CurrentQueue.RemoveAfter = CInt(RemoveAfterTime.Value) Else CurrentQueue.RemoveAfter = 0
        RemoveAfterTime.Enabled = RemoveAfter.Checked
    End Sub

    Private Sub RemoveOld_CheckedChanged() Handles RemoveOld.CheckedChanged
        If CurrentQueue IsNot Nothing Then CurrentQueue.RemoveOld = RemoveOld.Checked
    End Sub

    Private Sub RemoveViewed_CheckedChanged() Handles RemoveViewed.CheckedChanged
        If CurrentQueue IsNot Nothing Then CurrentQueue.RemoveViewed = RemoveViewed.Checked
    End Sub

    Private Sub RenameQueue_Click() Handles Rename.Click
        Dim OldName As String = QueueList.SelectedItem.ToString
        Dim NewName As String = InputBox.Show("Enter new name:", OldName)

        If NewName IsNot Nothing AndAlso NewName.Length > 0 AndAlso NewName <> OldName Then
            If Queue.All.ContainsKey(NewName) Then
                MessageBox.Show("A queue with the name '" & NewName & "' already exists. Choose another name.", _
                    "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                CurrentQueue.Name = NewName
                QueueList.Items(QueueList.SelectedIndex) = NewName
            End If
        End If
    End Sub

    Private Sub SortOrder_SelectedIndexChanged() Handles SortOrder.SelectedIndexChanged
        If CurrentQueue IsNot Nothing Then
            Select Case SortOrder.Text
                Case "Newest edits first" : CurrentQueue.SortOrder = QueueSortOrder.Time
                Case "Oldest edits first" : CurrentQueue.SortOrder = QueueSortOrder.TimeReverse
                Case "Most suspicious edits first" : CurrentQueue.SortOrder = QueueSortOrder.Quality
            End Select
        End If
    End Sub

    Private Sub SummaryRegex_Leave() Handles SummaryRegex.Leave
        CurrentQueue.SummaryRegex = SummaryRegex.Regex
    End Sub

    Private Sub TypeGroup_CheckedChanged() _
        Handles FixedList.CheckedChanged, LiveList.CheckedChanged, Live.CheckedChanged, DynamicList.CheckedChanged

        If CurrentQueue IsNot Nothing Then
            If FixedList.Checked Then : CurrentQueue.Type = QueueType.FixedList
            ElseIf LiveList.Checked Then : CurrentQueue.Type = QueueType.LiveList
            ElseIf Live.Checked Then : CurrentQueue.Type = QueueType.Live
            ElseIf DynamicList.Checked Then : CurrentQueue.Type = QueueType.Dynamic
            End If
        End If

        RefreshInterface()
    End Sub

    Private Sub UserRegex_Leave() Handles UserRegex.Leave
        CurrentQueue.UserRegex = UserRegex.Regex
    End Sub

    Private Function NextName() As String
        Dim i As Integer = Queue.All.Count + 1

        While Queue.All.ContainsKey("Queue" & CStr(i))
            i += 1
        End While

        Return "Queue" & CStr(i)
    End Function

    Private Sub RefreshInterface()
        If CurrentQueue IsNot Nothing Then
            Select Case CurrentQueue.Type
                Case QueueType.Live
                    HideTab(PagesTab)
                    ShowTab(UsersTab)
                    HideTab(DynamicListTab)
                    ShowTab(OptionsTab)
                    ShowTab(TitleFiltersTab)
                    ShowTab(EditFiltersTab)

                Case QueueType.LiveList
                    HideTab(DynamicListTab)
                    ShowTab(OptionsTab)
                    ShowTab(PagesTab)
                    ShowTab(UsersTab)
                    ShowTab(TitleFiltersTab)
                    ShowTab(EditFiltersTab)

                Case QueueType.FixedList
                    HideTab(UsersTab)
                    HideTab(DynamicListTab)
                    HideTab(OptionsTab)
                    HideTab(TitleFiltersTab)
                    HideTab(EditFiltersTab)
                    ShowTab(PagesTab)

                Case QueueType.Dynamic
                    HideTab(PagesTab)
                    HideTab(UsersTab)
                    HideTab(OptionsTab)
                    HideTab(EditFiltersTab)
                    ShowTab(DynamicListTab)
                    ShowTab(TitleFiltersTab)
            End Select

            Apply.Visible = (CurrentQueue.Type <> QueueType.Live)
            ApplyFiltersLabel.Visible = (CurrentQueue.Type <> QueueType.Live)
        End If

        RemoveUser.Enabled = (Users.SelectedIndex > -1)
        ClearUsers.Enabled = (Users.Items.Count > 0)
        Delete.Enabled = (QueueList.SelectedIndex > -1)
        MoveUp.Enabled = (QueueList.SelectedIndex > 0)
        MoveDown.Enabled = (QueueList.SelectedIndex > -1 AndAlso QueueList.SelectedIndex < QueueList.Items.Count - 1)
        Rename.Enabled = (QueueList.SelectedIndex > -1)
        Copy.Enabled = (QueueList.SelectedIndex > -1)
        QueuesEmpty.Visible = (QueueList.Items.Count = 0)
        Tabs.Enabled = (QueueList.SelectedIndex > -1)
        TypeGroup.Enabled = (QueueList.SelectedIndex > -1)
    End Sub

    Private Sub ShowTab(ByVal Tab As TabPage)
        If Not Tabs.TabPages.Contains(Tab) Then Tabs.TabPages.Add(Tab)
    End Sub

    Private Sub HideTab(ByVal Tab As TabPage)
        If Tabs.TabPages.Contains(Tab) Then Tabs.TabPages.Remove(Tab)
    End Sub

    Public Sub SetListSelector()
        ListSelector.BeginUpdate()
        ListSelector.Items.Clear()
        ListSelector.Items.Add("(none)")

        For Each Item As String In AllLists.Keys
            ListSelector.Items.Add(Item)
        Next Item

        ListSelector.EndUpdate()

        If CurrentQueue Is Nothing OrElse CurrentQueue.ListName Is Nothing Then ListSelector.SelectedIndex = 0 _
            Else ListSelector.SelectedItem = CurrentQueue.ListName
    End Sub

    Private Sub MoveUp_Click() Handles MoveUp.Click
        Dim Index As Integer = QueueList.SelectedIndex
        Dim Queue As Queue = CurrentQueue
        QueueList.Items.RemoveAt(Index)
        QueueList.Items.Insert(Index - 1, Queue.Name)
        QueueList.SelectedIndex = Index - 1
    End Sub

    Private Sub MoveDown_Click() Handles MoveDown.Click
        Dim Index As Integer = QueueList.SelectedIndex
        Dim Queue As Queue = CurrentQueue
        QueueList.Items.RemoveAt(Index)
        QueueList.Items.Insert(Index + 1, Queue.Name)
        QueueList.SelectedIndex = Index + 1
    End Sub

    Private Sub DynamicSourceType_SelectedIndexChanged() Handles DynamicSourceType.SelectedIndexChanged
        If CurrentQueue IsNot Nothing Then CurrentQueue.DynamicSourceType = DynamicSourceType.Text

        Select Case DynamicSourceType.Text
            Case "Category" : DynamicSourceLabel.Text = "Category:"
        End Select
    End Sub

    Private Sub DynamicSource_TextChanged() Handles DynamicSource.TextChanged
        If CurrentQueue IsNot Nothing Then CurrentQueue.DynamicSource = DynamicSource.Text
    End Sub

    Private Sub RefreshInterval_ValueChanged() Handles RefreshInterval.ValueChanged
        If CurrentQueue IsNot Nothing Then CurrentQueue.RefreshInterval = CInt(RefreshInterval.Value)
    End Sub

    Private Sub RefreshAlways_CheckedChanged() Handles RefreshAlways.CheckedChanged
        If CurrentQueue IsNot Nothing Then CurrentQueue.RefreshAlways = RefreshAlways.Checked
    End Sub

    Private Sub RefreshReAdd_CheckedChanged() Handles RefreshReAdd.CheckedChanged
        If CurrentQueue IsNot Nothing Then CurrentQueue.RefreshReAdd = RefreshReAdd.Checked
    End Sub

    Private Sub CheckAllSpaces_Click() Handles CheckAllSpaces.Click
        If CurrentQueue IsNot Nothing Then
            If CurrentQueue.Spaces.Count = 0 _
                Then CurrentQueue.Spaces.AddRange(Space.All) _
                Else CurrentQueue.Spaces.Clear()

            For i As Integer = 0 To Namespaces.Items.Count - 1
                Namespaces.SetItemChecked(i, CurrentQueue.Spaces.Contains(CType(Namespaces.Items(i), Space)))
            Next i
        End If
    End Sub

    Private Sub AddUser_Click(ByVal s As Object, ByVal e As EventArgs) Handles AddUser.Click
        Dim UserName As String = InputBox.Show("Enter user name:")

        If Not String.IsNullOrEmpty(UserName) Then
            UserName = User.SanitizeName(UserName)
            If Not Users.Items.Contains(UserName) Then Users.Items.Add(UserName)
            If Not CurrentQueue.Users.Contains(UserName) Then CurrentQueue.Users.Add(UserName)
            RefreshInterface()
        End If
    End Sub

    Private Sub RemoveUser_Click(ByVal s As Object, ByVal e As EventArgs) Handles RemoveUser.Click
        If Users.SelectedIndex > -1 Then
            If CurrentQueue.Users.Contains(Users.SelectedItem.ToString) Then CurrentQueue.Users.Remove(Users.SelectedItem.ToString)
            Users.Items.RemoveAt(Users.SelectedIndex)
            RefreshInterface()
        End If
    End Sub

    Private Sub ClearUsers_Click(ByVal s As Object, ByVal e As EventArgs) Handles ClearUsers.Click
        Users.Items.Clear()
        CurrentQueue.Users.Clear()
        RefreshInterface()
    End Sub

    Private Sub Users_SelectedIndexChanged(ByVal s As Object, ByVal e As EventArgs) Handles Users.SelectedIndexChanged
        RemoveUser.Enabled = (Users.SelectedIndex > -1)
    End Sub

End Class