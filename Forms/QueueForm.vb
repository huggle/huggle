Imports System.Text.RegularExpressions

Class QueueForm

    Private ReadOnly Property CurrentQueue() As Queue
        Get
            If QueueList.SelectedIndex = -1 Then Return Nothing Else Return Queue.All(QueueList.SelectedItem.ToString)
        End Get
    End Property

    Private Sub QueueForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Tip.Active = Config.ShowToolTips

        QueueList.BeginUpdate()

        For Each Item As String In Queue.All.Keys
            QueueList.Items.Add(Item)
        Next Item

        QueueList.EndUpdate()

        For Each Item As Space In Space.All
            Namespaces.Items.Add(Item)
        Next Item

        If QueueList.Items.Count > 0 Then QueueList.SelectedIndex = 0
        RefreshInterface()
    End Sub

    Private Sub QueueForm_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        Misc.CurrentQueue = Me.CurrentQueue
        MainForm.SetQueueSelector()
    End Sub

    Private Sub QueueForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub AddQueue_Click() Handles AddQueue.Click
        Dim i As Integer = Queue.All.Count + 1

        While Queue.All.ContainsKey("Queue" & CStr(i))
            i += 1
        End While

        Dim Name As String = "Queue" & CStr(i)

        Dim NewQueue As New Queue(Name)
        QueueList.Items.Add(Name)
        QueueList.SelectedItem = Name
    End Sub

    Private Sub ApplyFilters_Click() Handles ApplyFilters.Click
        Dim i As Integer

        While i < CurrentQueue.Pages.Count
            If CurrentQueue.MatchesFilter(CurrentQueue.Pages(i)) Then i += 1 Else CurrentQueue.Pages.RemoveAt(i)
        End While
    End Sub

    Private Sub Copy_Click() Handles CopyQueue.Click
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

    Private Sub DeleteQueue_Click() Handles DeleteQueue.Click
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

    Private Sub FilterIgnored_CheckStateChanged() Handles FilterIgnored.CheckStateChanged
        CurrentQueue.FilterIgnored = CType(CInt(FilterIgnored.State), QueueFilter)
    End Sub

    Private Sub FilterNewPage_CheckStateChanged() Handles FilterNewPage.CheckStateChanged
        CurrentQueue.FilterNewPage = CType(CInt(FilterNewPage.State), QueueFilter)
    End Sub

    Private Sub FilterOwnUserspace_CheckStateChanged() Handles FilterOwnUserspace.CheckStateChanged
        CurrentQueue.FilterOwnUserspace = CType(CInt(FilterOwnUserspace.State), QueueFilter)
    End Sub

    Private Sub FilterReverts_CheckStateChanged() Handles FilterReverts.CheckStateChanged
        CurrentQueue.FilterReverts = CType(CInt(FilterReverts.State), QueueFilter)
    End Sub

    Private Sub ListBuilder_Click() Handles ListBuilder.Click
        Dim NewForm As New ListForm
        NewForm.Show()
    End Sub

    Private Sub ListSelector_SelectedIndexChanged() Handles ListSelector.SelectedIndexChanged
        QueuePages.BeginUpdate()
        QueuePages.Items.Clear()

        For Each Item As String In AllLists(ListSelector.Text)
            QueuePages.Items.Add(Item)
        Next Item

        QueuePages.EndUpdate()

        CurrentQueue.Pages.AddRange(AllLists(ListSelector.Text))
        CurrentQueue.NeedsReset = True
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

    Private Sub PageRegex_Leave() Handles PageRegex.Leave
        Try
            If PageRegex.Text = "" Then CurrentQueue.PageRegex = Nothing _
                Else CurrentQueue.PageRegex = New Regex(PageRegex.Text, RegexOptions.Compiled)

        Catch ex As ArgumentException
            MessageBox.Show("Value entered for page filter is not a valid regular expression.", _
                "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            PageRegex.Text = ""
            CurrentQueue.PageRegex = Nothing
        End Try
    End Sub

    Private Sub QueueList_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles QueueList.KeyDown
        If e.KeyCode = Keys.Delete AndAlso QueueList.SelectedIndex > -1 Then DeleteQueue_Click()
    End Sub

    Private Sub QueueList_SelectedIndexChanged() Handles QueueList.SelectedIndexChanged
        QueuePages.Items.Clear()

        If CurrentQueue IsNot Nothing Then
            QueuePages.BeginUpdate()

            For Each Item As String In CurrentQueue.Pages
                QueuePages.Items.Add(Item)
            Next Item

            QueuePages.EndUpdate()

            Select Case CurrentQueue.Type
                Case QueueType.FixedList : FixedList.Checked = True
                Case QueueType.LiveList : LiveList.Checked = True
                Case QueueType.Live : Live.Checked = True
            End Select

            If CurrentQueue.SortOrder = QueueSortOrder.Time Then SortOrder.SelectedIndex = 0 _
                Else SortOrder.SelectedIndex = 1

            FilterAnonymous.State = CType(CInt(CurrentQueue.FilterAnonymous), CheckState)
            FilterIgnored.State = CType(CInt(CurrentQueue.FilterIgnored), CheckState)
            FilterNewPage.State = CType(CInt(CurrentQueue.FilterNewPage), CheckState)
            FilterOwnUserspace.State = CType(CInt(CurrentQueue.FilterOwnUserspace), CheckState)
            FilterReverts.State = CType(CInt(CurrentQueue.FilterReverts), CheckState)

            RemoveAfter.Checked = (CurrentQueue.RemoveAfter > 0)
            If CurrentQueue.RemoveAfter > RemoveAfterTime.Minimum Then RemoveAfterTime.Value = CurrentQueue.RemoveAfter _
                Else RemoveAfterTime.Value = RemoveAfterTime.Minimum
            RemoveOld.Checked = CurrentQueue.RemoveOld

            For i As Integer = 0 To Namespaces.Items.Count - 1
                Namespaces.SetItemChecked(i, CurrentQueue.Spaces.Contains(CType(Namespaces.Items(i), Space)))
            Next i
        End If

        RefreshInterface()
    End Sub

    Private Sub RemoveAfterChanged() Handles RemoveAfter.CheckedChanged, RemoveAfterTime.ValueChanged
        If CurrentQueue IsNot Nothing Then If RemoveAfter.Checked _
            Then CurrentQueue.RemoveAfter = CInt(RemoveAfterTime.Value) Else CurrentQueue.RemoveAfter = 0
    End Sub

    Private Sub RenameQueue_Click() Handles RenameQueue.Click
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
            If SortOrder.SelectedIndex = 0 Then CurrentQueue.SortOrder = QueueSortOrder.Time _
                Else CurrentQueue.SortOrder = QueueSortOrder.Quality
        End If
    End Sub

    Private Sub TypeGroup_CheckedChanged() _
        Handles FixedList.CheckedChanged, LiveList.CheckedChanged, Live.CheckedChanged

        If CurrentQueue IsNot Nothing Then
            If FixedList.Checked Then : CurrentQueue.Type = QueueType.FixedList
            ElseIf LiveList.Checked Then : CurrentQueue.Type = QueueType.LiveList
            ElseIf Live.Checked Then : CurrentQueue.Type = QueueType.Live
            End If
        End If

        RefreshInterface()
    End Sub

    Private Sub UserRegex_Leave() Handles UserRegex.Leave
        Try
            If UserRegex.Text = "" Then CurrentQueue.UserRegex = Nothing _
                Else CurrentQueue.UserRegex = New Regex(UserRegex.Text, RegexOptions.Compiled)

        Catch ex As ArgumentException
            MessageBox.Show("Value entered for user filter is not a valid regular expression.", _
                "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            UserRegex.Text = ""
            CurrentQueue.UserRegex = Nothing
        End Try
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

            If CurrentQueue.Type = QueueType.Live Then
                If Tabs.TabPages.Contains(PagesTab) Then Tabs.TabPages.Remove(PagesTab)
                If Not Tabs.TabPages.Contains(TitleFiltersTab) Then Tabs.TabPages.Insert(1, TitleFiltersTab)
            Else
                If Not Tabs.TabPages.Contains(PagesTab) Then Tabs.TabPages.Insert(2, PagesTab)
                If Tabs.TabPages.Contains(TitleFiltersTab) Then Tabs.TabPages.Remove(TitleFiltersTab)
            End If

            If CurrentQueue.Type = QueueType.FixedList Then
                If Tabs.TabPages.Contains(EditFiltersTab) Then Tabs.TabPages.Remove(EditFiltersTab)
                If Tabs.TabPages.Contains(OptionsTab) Then Tabs.TabPages.Remove(OptionsTab)
            Else
                If Not Tabs.TabPages.Contains(EditFiltersTab) Then Tabs.TabPages.Add(EditFiltersTab)
                If Not Tabs.TabPages.Contains(OptionsTab) Then Tabs.TabPages.Insert(0, OptionsTab)
            End If

            ApplyFilters.Visible = (CurrentQueue.Type <> QueueType.Live)
            ApplyFiltersLabel.Visible = (CurrentQueue.Type <> QueueType.Live)
        End If

        QueuesEmpty.Visible = (QueueList.Items.Count = 0)
        Tabs.Enabled = (QueueList.SelectedIndex > -1)
        TypeGroup.Enabled = (QueueList.SelectedIndex > -1)
    End Sub

End Class