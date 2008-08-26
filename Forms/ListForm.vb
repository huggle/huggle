Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Class ListForm

    Private Mode As String, CurrentRequest As ListRequest
    Public Spaces As New List(Of Space)(Space.All), TitleRegex As Regex

    Private ReadOnly Property CurrentList() As List(Of String)
        Get
            If Lists.SelectedIndex = -1 Then Return Nothing Else Return AllLists(Lists.SelectedItem.ToString)
        End Get
    End Property

    Private Sub ListForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Tip.Active = Config.ShowToolTips

        SourceType.Items.AddRange(New String() {"Manually add pages", "Backlinks", "Category", "Category (recursive)", _
            "Existing list", "External link uses", "File", "Image uses", "Images on page", "Links on page", _
            "Search", "Templates on page", "Transclusions", "User contributions", "Watchlist"})

        Lists.BeginUpdate()

        For Each Item As String In AllLists.Keys
            Lists.Items.Add(Item)
        Next Item

        Lists.EndUpdate()

        Limit.Maximum = ApiLimit() * QueueBuilderLimit
        Limit.Value = Math.Min(1000, Limit.Maximum)
        SourceType.SelectedIndex = 0
        If Lists.Items.Count > 0 Then Lists.SelectedIndex = 0
        RefreshInterface()
    End Sub

    Private Sub ListForm_FormClosing() Handles Me.FormClosing
        If CurrentRequest IsNot Nothing AndAlso CurrentRequest.State <> Request.States.Cancelled _
            Then CurrentRequest.Cancel()
    End Sub

    Private Sub ListForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Actions_Click() Handles Actions.Click
        Dim NewForm As New ListActionsForm
        NewForm.Form = Me
        NewForm.List = CurrentList
        NewForm.ShowDialog()
    End Sub

    Private Sub AddList_Click() Handles AddList.Click
        Dim Name As String = NextName()
        Dim NewList As New List(Of String)
        AllLists.Add(Name, NewList)
        Lists.Items.Add(Name)
        Lists.SelectedItem = Name
        RefreshListSelectors()
    End Sub

    Private Sub Browse_Click() Handles Browse.Click
        Dim Dialog As New OpenFileDialog
        Dialog.Title = "Add text file to list"
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
        CurrentList.Clear()
        ListPages.Items.Clear()
        Progress.Text = ""
        RefreshInterface()
    End Sub

    Private Sub Combine_Click() Handles Combine.Click
        Mode = "combine"
        Cancel.Location = Combine.Location
        SelectList()
    End Sub

    Private Sub CloseButton_Click() Handles CloseButton.Click
        Close()
    End Sub

    Private Sub CopyList_Click() Handles CopyList.Click
        Dim Name As String = InputBox.Show("Copy to:", NextName)

        If Name.Length > 0 Then
            If AllLists.ContainsKey(Name) Then
                MessageBox.Show("A list with the name '" & Name & "' already exists. Choose another name.", _
                    "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Dim NewList As New List(Of String)(CurrentList)
                AllLists.Add(Name, NewList)
                Lists.Items.Add(Name)
                Lists.SelectedItem = Name
                RefreshListSelectors()
            End If
        End If
    End Sub

    Private Sub DeleteList_Click() Handles DeleteList.Click
        If Lists.SelectedIndex > -1 AndAlso MessageBox.Show("Delete list '" & Lists.SelectedItem.ToString & "'?", _
            "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) _
            = DialogResult.Yes Then

            Dim Index As Integer = Lists.SelectedIndex
            AllLists.Remove(Lists.SelectedItem.ToString)
            If Index > 0 Then Lists.SelectedIndex -= 1

            Lists.Items.RemoveAt(Index)
            Lists.SelectedIndex = Lists.Items.Count - 1
            RefreshListSelectors()
        End If
    End Sub

    Private Sub EditPage() Handles ListMenuEdit.Click
        If ListPages.SelectedIndex > -1 Then
            Dim NewForm As New EditForm
            NewForm.Page = GetPage(ListPages.SelectedItem.ToString)
            NewForm.Show()
        End If
    End Sub

    Private Sub Exclude_Click() Handles Exclude.Click
        Mode = "exclude"
        Cancel.Location = Exclude.Location
        SelectList()
    End Sub

    Private Sub Intersect_Click() Handles Intersect.Click
        Mode = "intersect"
        Cancel.Location = Intersect.Location
        SelectList()
    End Sub

    Private Sub ListPages_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles ListPages.KeyDown
        If e.KeyCode = Keys.Delete AndAlso ListPages.SelectedIndex > -1 Then RemovePage()
    End Sub

    Private Sub ListPages_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles ListPages.MouseDown
        ListPages.SelectedIndex = ListPages.IndexFromPoint(e.Location)
    End Sub

    Private Sub Lists_SelectedIndexChanged() Handles Lists.SelectedIndexChanged
        Progress.Text = ""
        RefreshInterface()
    End Sub

    Private Sub RenameList_Click() Handles RenameList.Click
        Dim OldName As String = Lists.SelectedItem.ToString
        Dim NewName As String = InputBox.Show("Enter new name:", OldName)

        If NewName IsNot Nothing AndAlso NewName.Length > 0 AndAlso NewName <> OldName Then
            If AllLists.ContainsKey(NewName) Then
                MessageBox.Show("A list with the name '" & NewName & "' already exists. Choose another name.", _
                    "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Dim ThisList As List(Of String) = CurrentList
                AllLists.Remove(OldName)
                AllLists.Add(NewName, ThisList)
                Lists.Items(Lists.SelectedIndex) = NewName
                RefreshListSelectors()
            End If
        End If
    End Sub

    Private Sub RemovePage() Handles ListMenuRemove.Click
        If ListPages.SelectedIndex > -1 Then
            Dim Index As Integer = ListPages.SelectedIndex
            If Index > 0 Then ListPages.SelectedIndex -= 1

            CurrentList.RemoveAt(Index)
            ListPages.Items.RemoveAt(Index)
            ListPages.SelectedIndex = ListPages.Items.Count - 1
            RefreshInterface()
        End If
    End Sub

    Private Sub Save_Click() Handles Save.Click
        Dim Dialog As New SaveFileDialog
        Dialog.FileName = Lists.SelectedItem.ToString & ".txt"
        Dialog.Filter = "Text file|*.txt"
        Dialog.Title = "Save queue"

        If Dialog.ShowDialog = DialogResult.OK Then
            Dim Items As New List(Of String)

            For Each Item As String In Lists.Items
                Items.Add("*[[" & Item & "]]")
            Next Item

            File.WriteAllLines(Dialog.FileName, Items.ToArray)
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
            Case "Existing list" : SourceLabel.Text = "List:"
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
            ListSelector.Focus()
        Else
            Source.Focus()
            Source.SelectAll()
        End If
    End Sub

    Private Sub CombineItems(ByVal Items As List(Of String))
        ListPages.BeginUpdate()

        For Each Item As String In Items
            If Not CurrentList.Contains(Item) Then
                CurrentList.Add(Item)
                ListPages.Items.Add(Item)
            End If
        Next Item

        ListPages.EndUpdate()
    End Sub

    Private Sub ExcludeItems(ByVal Items As List(Of String))
        ListPages.BeginUpdate()

        Dim i As Integer = 0

        While i < CurrentList.Count
            If Items.Contains(CurrentList(i)) Then
                CurrentList.RemoveAt(i)
                ListPages.Items.RemoveAt(i)
            Else
                i += 1
            End If
        End While

        ListPages.EndUpdate()
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

    Private Sub GetList(ByVal Request As ListRequest)
        CurrentRequest = Request
        CurrentRequest.Limit = CInt(Limit.Value)
        CurrentRequest.List = CurrentList
        CurrentRequest.From = From.Text
        CurrentRequest.TitleRegex = TitleRegex
        CurrentRequest.Spaces = Spaces
        CurrentRequest.Start(AddressOf GotList, AddressOf ListProgress)

        Progress.Text = "Running query..."
        Throbber.Start()
        RefreshInterface()
        Cancel.Focus()
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

    Private Sub IntersectItems(ByVal Items As List(Of String))
        ListPages.BeginUpdate()

        Dim i As Integer = 0

        While i < CurrentList.Count
            If Not Items.Contains(CurrentList(i)) Then
                CurrentList.RemoveAt(i)
                ListPages.Items.RemoveAt(i)
            Else
                i += 1
            End If
        End While

        ListPages.EndUpdate()
    End Sub

    Private Function NextName() As String
        Dim i As Integer = AllLists.Count + 1

        While AllLists.ContainsKey("List" & CStr(i))
            i += 1
        End While

        Return "List" & CStr(i)
    End Function

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

    Private Sub OpenPageInBrowser() Handles ListPages.DoubleClick, ListMenuView.Click
        If ListPages.SelectedIndex > -1 _
            Then OpenUrlInBrowser(SitePath & "w/index.php?title=" & UrlEncode(ListPages.SelectedItem.ToString))
    End Sub

    Private Sub RefreshInterface()
        Combine.Text = If(ListPages.Items.Count = 0, "Add", "Combine")
        Count.Text = CStr(ListPages.Items.Count) & " items"
        ListPages.ContextMenuStrip = If(ListPages.Items.Count > 0, ListMenu, Nothing)
        Source.Width = If(SourceType.Text = "File", 137, 210)

        For Each Item As Control In New Control() _
            {CopyList, FromLabel, Limit, LimitLabel, DeleteList, RenameList, SourceLabel, SourceTypeLabel}
            Item.Enabled = Not Throbber.Active
        Next Item

        Actions.Enabled = (Lists.SelectedIndex > -1)
        AddList.Enabled = (Not Throbber.Active)
        Browse.Visible = (SourceType.Text = "File")
        Cancel.Visible = Throbber.Active
        Clear.Enabled = (Not Throbber.Active AndAlso ListPages.Items.Count > 0)
        Count.Visible = (Lists.SelectedIndex > -1)
        From.Enabled = (Not Throbber.Active AndAlso SourceType.Text <> "Manually add pages")
        ListEmpty.Visible = (Lists.SelectedIndex > -1 AndAlso ListPages.Items.Count = 0)
        ListPages.Enabled = (Not Throbber.Active)
        ListsEmpty.Visible = (Lists.Items.Count = 0)
        ListSelector.Visible = (SourceType.Text = "Existing list")
        Save.Enabled = (ListPages.Items.Count > 0)
        Source.Enabled = Limit.Enabled
        Source.Visible = (SourceType.Text <> "Watchlist" AndAlso SourceType.Text <> "Existing list")
        SourceType.Enabled = Limit.Enabled

        If ListSelector.Visible Then
            ListSelector.Items.Clear()

            For Each Item As Object In Lists.Items
                If Item IsNot Lists.SelectedItem Then Lists.Items.Add(Item.ToString)
            Next Item

            If ListSelector.Items.Count = 0 Then ListSelector.Text = "" Else ListSelector.SelectedIndex = 0

            Combine.Enabled = Not Throbber.Active AndAlso Lists.SelectedIndex > -1 _
                AndAlso (ListSelector.SelectedIndex > -1)
            Exclude.Enabled = (Combine.Enabled AndAlso ListPages.Items.Count > 0)
            Intersect.Enabled = Exclude.Enabled
        Else
            Combine.Enabled = Not Throbber.Active AndAlso Lists.SelectedIndex > -1 _
                AndAlso (Source.Text.Length > 0 OrElse SourceType.Text = "Watchlist")
            Exclude.Enabled = (Combine.Enabled AndAlso ListPages.Items.Count > 0)
            Intersect.Enabled = Exclude.Enabled
        End If

        For Each Item As Queue In Queue.All.Values
            If Item.ListName = Lists.SelectedItem.ToString Then Item.NeedsReset = True
        Next Item
    End Sub

    Private Sub RefreshListSelectors()
        For Each Form As Form In Application.OpenForms
            If TypeOf Form Is QueueForm Then CType(Form, QueueForm).SetListSelector()
        Next Form
    End Sub

    Private Sub SelectList()
        Select Case SourceType.Text
            Case "Manually add pages" : GotList(New List(Of String)(Source.Text.Split("|"c)))
            Case "Backlinks" : GetList(New BacklinksRequest(Source.Text))
            Case "Category" : GetList(New CategoryRequest(Source.Text.Replace("Category:", "")))
            Case "Category (recursive)" : GetList(New RecursiveCategoryRequest(Source.Text.Replace("Category:", "")))
            Case "Existing queue" : GotList(Queue.All(ListSelector.Text).Pages)
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

End Class