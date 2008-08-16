Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Class QueueForm

    Private Mode As String, CurrentRequest As ListRequest

    Private ReadOnly Property CurrentQueue() As Queue
        Get
            If QueueList.SelectedIndex = -1 Then Return Nothing Else Return Queue.All(QueueList.SelectedItem.ToString)
        End Get
    End Property

    Private Sub QueueForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Tip.Active = Config.ShowToolTips

        SourceType.Items.AddRange(New String() {"Manually add pages", "Backlinks", "Category", "Category (recursive)", _
            "Existing queue", "External link uses", "File", "Image uses", "Images on page", "Links on page", _
            "Search", "Templates on page", "Transclusions", "User contributions", "Watchlist"})

        QueueList.BeginUpdate()

        For Each Item As String In Queue.All.Keys
            QueueList.Items.Add(Item)
        Next Item

        QueueList.EndUpdate()

        For Each Item As Space In Space.All
            Namespaces.Items.Add(Item)
        Next Item

        Limit.Maximum = ApiLimit() * QueueBuilderLimit
        Limit.Value = Math.Min(1000, Limit.Maximum)
        SourceType.SelectedIndex = 0
        If QueueList.Items.Count > 0 Then QueueList.SelectedIndex = 0
        RefreshInterface()
    End Sub

    Private Sub QueueForm_FormClosing() Handles Me.FormClosing
        If CurrentRequest IsNot Nothing AndAlso CurrentRequest.State <> Request.States.Cancelled _
            Then CurrentRequest.Cancel()
        If QueueList.SelectedIndex > -1 Then MainForm.QueueSelector.SelectedItem = QueueList.SelectedItem.ToString
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
        NewQueue.Spaces.Add(Space.Article)
        QueueList.Items.Add(Name)
        QueueList.SelectedItem = Name
    End Sub

    Private Sub ApplyFilters_Click() Handles ApplyFilters.Click
        Dim i As Integer

        While i < CurrentQueue.Pages.Count
            If CurrentQueue.MatchesFilter(CurrentQueue.Pages(i)) Then i += 1 Else CurrentQueue.Pages.RemoveAt(i)
        End While
    End Sub

    Private Sub Browse_Click() Handles Browse.Click
        Dim Dialog As New OpenFileDialog
        Dialog.Title = "Add text file to queue"
        Dialog.Filter = "All files|*.*"

        If Dialog.ShowDialog() = DialogResult.OK Then Source.Text = Dialog.FileName
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        Throbber.Stop()
        CurrentRequest.Cancel()
        Cancel.Text = "Cancel"
        Progress.Text = "Cancelled."
        RefreshInterface()
    End Sub

    Private Sub Clear_Click() Handles Clear.Click
        CurrentQueue.Pages.Clear()
        QueuePages.Items.Clear()
        Progress.Text = ""
        RefreshInterface()
    End Sub

    Private Sub Combine_Click() Handles Combine.Click
        Mode = "combine"
        Cancel.Location = Combine.Location
        SelectList()
    End Sub

    Private Sub Copy_Click() Handles Copy.Click
        Dim NewName As String = InputBox("Copy to:", "huggle", "Queue" & CStr(Queue.All.Count + 1))

        If NewName.Length > 0 Then
            If Queue.All.ContainsKey(NewName) Then
                MsgBox("A queue with the name '" & NewName & "' already exists. Choose another name.", _
                    MsgBoxStyle.Exclamation, "huggle")
            Else
                Dim NewQueue As New Queue(NewName)
                NewQueue.Pages.AddRange(CurrentQueue.Pages)
                QueueList.Items.Add(NewName)
                QueueList.SelectedItem = NewName
                MainForm.SetQueueSelector()
            End If
        End If
    End Sub

    Private Sub EditPage() Handles QueueMenuEdit.Click
        If QueuePages.SelectedIndex > -1 Then
            Dim NewForm As New EditForm
            NewForm.Page = GetPage(QueuePages.SelectedItem.ToString)
            NewForm.Show()
        End If
    End Sub

    Private Sub Exclude_Click() Handles Exclude.Click
        Mode = "exclude"
        Cancel.Location = Exclude.Location
        SelectList()
    End Sub

    Private Sub FilterAnonymous_CheckStateChanged() Handles FilterAnonymous.CheckStateChanged
        CurrentQueue.FilterAnonymous = CType(CInt(FilterNewPage.State), QueueFilter)
    End Sub

    Private Sub FilterIgnoredUser_CheckStateChanged() Handles FilterIgnoredUser.CheckStateChanged
        CurrentQueue.FilterNewPage = CType(CInt(FilterNewPage.State), QueueFilter)
    End Sub

    Private Sub FilterNewPage_CheckStateChanged() Handles FilterNewPage.CheckStateChanged
        CurrentQueue.FilterNewPage = CType(CInt(FilterNewPage.State), QueueFilter)
    End Sub

    Private Sub Intersect_Click() Handles Intersect.Click
        Mode = "intersect"
        Cancel.Location = Intersect.Location
        SelectList()
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

    Private Sub OpenPageInBrowser() Handles QueuePages.DoubleClick, QueueMenuView.Click
        If QueuePages.SelectedIndex > -1 _
            Then OpenUrlInBrowser(SitePath & "w/index.php?title=" & UrlEncode(QueuePages.SelectedItem.ToString))
    End Sub

    Private Sub PageRegex_Leave() Handles PageRegex.Leave
        Try
            If PageRegex.Text = "" Then CurrentQueue.PageRegex = Nothing _
                Else CurrentQueue.PageRegex = New Regex(PageRegex.Text, RegexOptions.Compiled)

        Catch ex As ArgumentException
            MsgBox("Value entered for page filter is not a valid regular expression.", MsgBoxStyle.Exclamation, "huggle")
            PageRegex.Text = ""
            CurrentQueue.PageRegex = Nothing
        End Try
    End Sub

    Private Sub QueuePages_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles QueuePages.KeyDown
        If e.KeyCode = Keys.Delete AndAlso QueuePages.SelectedIndex > -1 Then RemovePage()
    End Sub

    Private Sub QueuePages_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles QueuePages.MouseDown
        QueuePages.SelectedIndex = QueuePages.IndexFromPoint(e.Location)
    End Sub

    Private Sub QueueList_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles QueueList.KeyDown
        If e.KeyCode = Keys.Delete AndAlso QueueList.SelectedIndex > -1 Then RemoveQueue_Click()
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

            FilterAnonymous.State = CType(CInt(CurrentQueue.FilterAnonymous), CheckState)
            FilterIgnoredUser.State = CType(CInt(CurrentQueue.FilterIgnored), CheckState)
            FilterNewPage.State = CType(CInt(CurrentQueue.FilterNewPage), CheckState)

            RemoveAfter.Checked = (CurrentQueue.RemoveAfter > 0)
            RemoveAfterTime.Value = CurrentQueue.RemoveAfter
            RemoveOld.Checked = CurrentQueue.RemoveOld

            For i As Integer = 0 To Namespaces.Items.Count - 1
                Namespaces.SetItemChecked(i, CurrentQueue.Spaces.Count = 0 _
                    OrElse CurrentQueue.Spaces.Contains(CType(Namespaces.Items(i), Space)))
            Next i
        End If

        Progress.Text = ""
        RefreshInterface()
    End Sub

    Private Sub RemoveAfterChanged() Handles RemoveAfter.CheckedChanged, RemoveAfterTime.ValueChanged
        If CurrentQueue IsNot Nothing Then If RemoveAfter.Checked _
            Then CurrentQueue.RemoveAfter = CInt(RemoveAfterTime.Value) Else CurrentQueue.RemoveAfter = 0
    End Sub

    Private Sub RemovePage() Handles QueueMenuRemove.Click
        If QueuePages.SelectedIndex > -1 Then
            Dim Index As Integer = QueuePages.SelectedIndex
            If Index > 0 Then QueuePages.SelectedIndex -= 1

            CurrentQueue.Pages.RemoveAt(Index)
            QueuePages.Items.RemoveAt(Index)
            QueuePages.SelectedIndex = QueuePages.Items.Count - 1
            CurrentQueue.NeedsReset = True
            RefreshInterface()
        End If
    End Sub

    Private Sub RemoveQueue_Click() Handles RemoveQueue.Click
        If QueueList.SelectedIndex > -1 Then
            Dim Index As Integer = QueueList.SelectedIndex
            Queue.All.Remove(QueueList.SelectedItem.ToString)
            If Index > 0 Then QueueList.SelectedIndex -= 1

            QueueList.Items.RemoveAt(Index)
            QueueList.SelectedIndex = QueueList.Items.Count - 1
            MainForm.SetQueueSelector()
        End If
    End Sub

    Private Sub Rename_Click() Handles Rename.Click
        Dim OldName As String = QueueList.SelectedItem.ToString
        Dim NewName As String = InputBox("Enter new name:", "huggle", OldName)

        If NewName.Length > 0 AndAlso NewName <> OldName Then
            If Queue.All.ContainsKey(NewName) Then
                MsgBox("A queue with the name '" & NewName & "' already exists. Choose another name.", _
                    MsgBoxStyle.Exclamation, "huggle")
            Else
                CurrentQueue.Name = NewName
                QueueList.Items(QueueList.SelectedIndex) = NewName
            End If
        End If
    End Sub

    Private Sub Save_Click() Handles Save.Click
        Dim Dialog As New SaveFileDialog
        Dialog.FileName = QueueList.SelectedItem.ToString & ".txt"
        Dialog.Filter = "Text file|*.txt"
        Dialog.Title = "Save queue"

        If Dialog.ShowDialog = DialogResult.OK Then
            Dim Items As New List(Of String)

            For Each Item As String In QueuePages.Items
                Items.Add("*[[" & Item & "]]")
            Next Item

            File.WriteAllLines(Dialog.FileName, Items.ToArray)
        End If
    End Sub

    Private Sub SortOrder_SelectedIndexChanged() Handles SortOrder.SelectedIndexChanged
        If CurrentQueue IsNot Nothing Then
            If SortOrder.SelectedIndex = 0 Then CurrentQueue.SortOrder = QueueSortOrder.Time _
                Else CurrentQueue.SortOrder = QueueSortOrder.Quality
        End If
    End Sub

    Private Sub Source_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Source.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Source.Text <> "" Then
            Combine_Click()
            If SourceType.Text = "Manually add pages" Then Source.Clear()
        End If
    End Sub

    Private Sub Source_TextChanged() Handles Source.TextChanged
        RefreshInterface()
    End Sub

    Private Sub SourceType_SelectedIndexChanged() Handles SourceType.SelectedIndexChanged
        Select Case SourceType.Text
            Case "Category", "Category (recursive)" : SourceLabel.Text = "Category:"
            Case "Existing queue" : SourceLabel.Text = "Queue:"
            Case "External link uses" : SourceLabel.Text = "URL:"
            Case "File" : SourceLabel.Text = "File:"
            Case "Image uses" : SourceLabel.Text = "Image:"
            Case "Search" : SourceLabel.Text = "Search terms:"
            Case "User contributions" : SourceLabel.Text = "User:"
            Case "Watchlist" : SourceLabel.Text = ""
            Case Else : SourceLabel.Text = "Page:"
        End Select

        RefreshInterface()
        If SourceType.Text = "File" Then Source.Clear()

        If SourceType.Text = "Existing queue" Then
            QueueSelector.Focus()
        Else
            Source.Focus()
            Source.SelectAll()
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
            MsgBox("Value entered for user filter is not a valid regular expression.", MsgBoxStyle.Exclamation, "huggle")
            UserRegex.Text = ""
            CurrentQueue.UserRegex = Nothing
        End Try
    End Sub

    Private Sub RefreshInterface()
        Combine.Text = If(QueuePages.Items.Count = 0, "Add", "Combine")
        Count.Text = CStr(QueuePages.Items.Count) & " items"
        QueuePages.ContextMenuStrip = If(QueuePages.Items.Count > 0, QueueMenu, Nothing)
        Source.Width = If(SourceType.Text = "File", 137, 210)

        For Each Item As Control In New Control() _
            {Copy, FromLabel, Limit, LimitLabel, RemoveQueue, Rename, SourceLabel, SourceTypeLabel}
            Item.Enabled = Not Throbber.Active
        Next Item

        If CurrentQueue IsNot Nothing Then

            If CurrentQueue.Type = QueueType.Live Then
                If Tabs.TabPages.Contains(PagesTab) Then Tabs.TabPages.Remove(PagesTab)
            Else
                If Not Tabs.TabPages.Contains(PagesTab) Then Tabs.TabPages.Insert(1, PagesTab)
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

        AddQueue.Enabled = (Not Throbber.Active)
        Browse.Visible = (SourceType.Text = "File")
        Cancel.Visible = Throbber.Active
        Clear.Enabled = (Not Throbber.Active AndAlso QueuePages.Items.Count > 0)
        Count.Visible = (QueueList.SelectedIndex > -1)
        From.Enabled = (Not Throbber.Active AndAlso SourceType.Text <> "Manually add pages")
        QueueEmpty.Visible = (QueueList.SelectedIndex > -1 AndAlso QueuePages.Items.Count = 0)
        QueueList.Enabled = (Not Throbber.Active)
        QueuesEmpty.Visible = (QueueList.Items.Count = 0)
        QueueSelector.Visible = (SourceType.Text = "Existing queue")
        Save.Enabled = (QueuePages.Items.Count > 0)
        Sort.Enabled = (Not Throbber.Active AndAlso QueuePages.Items.Count > 1)
        Source.Enabled = Limit.Enabled
        Source.Visible = (SourceType.Text <> "Watchlist" AndAlso SourceType.Text <> "Existing queue")
        SourceType.Enabled = Limit.Enabled
        Tabs.Enabled = (QueueList.SelectedIndex > -1)
        TypeGroup.Enabled = (QueueList.SelectedIndex > -1)

        If QueueSelector.Visible Then
            QueueSelector.Items.Clear()

            For Each Item As Object In QueueList.Items
                If Item IsNot QueueList.SelectedItem Then QueueSelector.Items.Add(Item.ToString)
            Next Item

            If QueueSelector.Items.Count = 0 Then QueueSelector.Text = "" Else QueueSelector.SelectedIndex = 0

            Combine.Enabled = Not Throbber.Active AndAlso QueueList.SelectedIndex > -1 _
                AndAlso (QueueSelector.SelectedIndex > -1)
            Exclude.Enabled = (Combine.Enabled AndAlso QueuePages.Items.Count > 0)
            Intersect.Enabled = Exclude.Enabled
        Else
            Combine.Enabled = Not Throbber.Active AndAlso QueueList.SelectedIndex > -1 _
                AndAlso (Source.Text.Length > 0 OrElse SourceType.Text = "Watchlist")
            Exclude.Enabled = (Combine.Enabled AndAlso QueuePages.Items.Count > 0)
            Intersect.Enabled = Exclude.Enabled
        End If
    End Sub

    Private Sub SelectList()
        Select Case SourceType.Text
            Case "Manually add pages" : GotList(New List(Of String)(Source.Text.Split("|"c)))
            Case "Backlinks" : GetList(New BacklinksRequest(Source.Text))
            Case "Category" : GetList(New CategoryRequest(Source.Text.Replace("Category:", "")))
            Case "Category (recursive)" : GetList(New RecursiveCategoryRequest(Source.Text.Replace("Category:", "")))
            Case "Existing queue" : GotList(Queue.All(QueueSelector.Text).Pages)
            Case "External link uses" : GetList(New ExternalLinkUsageRequest(Source.Text.Replace("http://", "")))
            Case "File" : GotList(GetFile(Source.Text))
            Case "Image uses" : GetList(New ImageUsageRequest(Source.Text.Replace("Image:", "")))
            Case "Images on page" : GetList(New ImagesRequest(Source.Text))
            Case "Links on page" : GetList(New LinksRequest(Source.Text))
            Case "Search" : GetList(New SearchRequest(Source.Text))
            Case "Templates on page" : GetList(New TemplatesRequest(Source.Text))
            Case "Transclusions" : GetList(New TransclusionsRequest(Source.Text))
            Case "User contributions" : GetList(New ContribsListRequest(Source.Text))
            Case "Watchlist" : GotList(PageNames(Watchlist))
        End Select
    End Sub

    Private Sub GetList(ByVal Request As ListRequest)
        CurrentRequest = Request
        CurrentRequest.Limit = CInt(Limit.Value)
        CurrentRequest.Queue = CurrentQueue
        CurrentRequest.From = From.Text
        CurrentRequest.Start(AddressOf GotList, AddressOf ListProgress)

        Progress.Text = "Running query..."
        Throbber.Start()
        RefreshInterface()
        Cancel.Focus()
    End Sub

    Private Sub ListProgress(ByVal State As String, ByVal PartialResult As List(Of String))
        Progress.Text = State

        If PartialResult IsNot Nothing AndAlso PartialResult.Count > 0 Then
            If Mode = "combine" Then
                Cancel.Text = "Stop"
                CombineItems(PartialResult)
                CurrentQueue.NeedsReset = False
            End If
        End If

        RefreshInterface()
    End Sub

    Private Sub GotList(ByVal Titles As List(Of String))
        If Titles Is Nothing Then
            Progress.Text = "Query failed."
            Exit Sub
        End If

        Dim ValidItems As New List(Of String)

        For Each Title As String In Titles
            Title = Page.SanitizeTitle(Title)
            If Title IsNot Nothing Then ValidItems.Add(Title)
        Next Title

        If ValidItems.Count = 0 Then
            Progress.Text = "Query returned no results."
        Else
            Progress.Text = ValidItems.Count & " results returned."

            Select Case Mode
                Case "combine" : CombineItems(ValidItems)
                Case "intersect" : IntersectItems(ValidItems)
                Case "exclude" : ExcludeItems(ValidItems)
            End Select

            CurrentQueue.NeedsReset = False
        End If

        Throbber.Stop()
        RefreshInterface()
        Source.Focus()
    End Sub

    Private Sub CombineItems(ByVal Items As List(Of String))
        QueuePages.BeginUpdate()

        For Each Item As String In Items
            If Not CurrentQueue.Pages.Contains(Item) Then
                CurrentQueue.Pages.Add(Item)
                QueuePages.Items.Add(Item)
            End If
        Next Item

        QueuePages.EndUpdate()
    End Sub

    Private Sub IntersectItems(ByVal Items As List(Of String))
        QueuePages.BeginUpdate()

        Dim i As Integer = 0

        While i < CurrentQueue.Pages.Count
            If Not Items.Contains(CurrentQueue.Pages(i)) Then
                CurrentQueue.Pages.RemoveAt(i)
                QueuePages.Items.RemoveAt(i)
            Else
                i += 1
            End If
        End While

        QueuePages.EndUpdate()
    End Sub

    Private Sub ExcludeItems(ByVal Items As List(Of String))
        QueuePages.BeginUpdate()

        Dim i As Integer = 0

        While i < CurrentQueue.Pages.Count
            If Items.Contains(CurrentQueue.Pages(i)) Then
                CurrentQueue.Pages.RemoveAt(i)
                QueuePages.Items.RemoveAt(i)
            Else
                i += 1
            End If
        End While

        QueuePages.EndUpdate()
    End Sub

    Private Function GetFile(ByVal FileName As String) As List(Of String)
        Dim Titles As New List(Of String)

        If File.Exists(FileName) Then
            For Each Title As String In File.ReadAllLines(FileName)
                If Title.StartsWith("*[[") OrElse Title.StartsWith("* [[") Then Title = Title.Substring(1)
                Title = Page.SanitizeTitle(Title)
                If Title.Length > 0 Then Titles.Add(Title)
            Next Title

            Return Titles
        Else
            Return Nothing
        End If
    End Function

End Class