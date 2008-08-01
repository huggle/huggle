Imports System.IO
Imports System.Web.HttpUtility

Class QueueForm

    Private Mode As String, CurrentRequest As ListRequest

    Private ReadOnly Property CurrentQueueSource() As QueueSource
        Get
            If Queues.SelectedIndex = -1 Then Return Nothing Else Return QueueSources(Queues.SelectedItem.ToString)
        End Get
    End Property

    Private Sub QueueForm_Load() Handles MyBase.Load
        Icon = My.Resources.icon_red_button
        Tip.Active = Config.ShowToolTips

        Limit.Maximum = ApiLimit() * QueueBuilderLimit
        Limit.Value = Math.Min(1000, Limit.Maximum)

        SourceType.Items.AddRange(New String() {"Backlinks", "Category", "Category (recursive)", _
            "Existing queue", "External link uses", "File", "Image uses", "Images on page", "Links on page", _
            "Search", "Templates on page", "Transclusions", "User contributions", "Watchlist"})

        SourceType.SelectedIndex = 0

        Queues.BeginUpdate()

        For Each Item As String In QueueSources.Keys
            Queues.Items.Add(Item)
        Next Item

        Queues.EndUpdate()
    End Sub

    Private Sub QueueForm_FormClosing() Handles Me.FormClosing
        MainForm.SetQueueSources()
    End Sub

    Private Sub QueueForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub AddItem_Click() Handles AddItem.Click
        Dim Input As String = InputBox("Enter page name:", "huggle")

        If Input <> StripIllegalCharacters(Input) Then
            MsgBox("'" & Input & "' is not a valid page title.", MsgBoxStyle.Critical, "huggle")
            Exit Sub
        End If

        If Input.Length > 0 AndAlso Not Queue.Items.Contains(Input) Then
            If Input.Length = 1 Then Input = Input.ToUpper
            If Input.Length >= 1 Then Input = Input.Substring(0, 1).ToUpper & Input.Substring(1)

            If Queue.Items.Contains(Input) Then
                MsgBox("This queue already contains the page '" & Input & "'.", MsgBoxStyle.Exclamation, "huggle")
            Else
                Queue.Items.Add(Input)
                CurrentQueueSource.Items.Add(Input)
                Queue.SelectedIndex = Queue.Items.Count - 1
                RefreshInterface()
            End If
        End If
    End Sub

    Private Sub AddQueue_Click() Handles AddQueue.Click
        Dim i As Integer = QueueSources.Count + 1

        While QueueSources.ContainsKey("Queue" & CStr(i))
            i += 1
        End While

        Dim Name As String = "Queue" & CStr(i)

        QueueSources.Add(Name, New QueueSource)
        Queues.Items.Add(Name)
        Queues.SelectedItem = Name
    End Sub

    Private Sub ArticlesOnly_CheckedChanged() Handles ArticlesOnly.CheckedChanged
        If ArticlesOnly.Checked Then
            Dim i As Integer = 0

            While i < Queue.Items.Count
                Dim Item As String = Queue.Items(i).ToString

                If Item.Contains(":") AndAlso ArrayContains(Namespaces, Item.Substring(0, Item.IndexOf(":"))) Then
                    Queue.Items.RemoveAt(i)
                    CurrentQueueSource.Items.RemoveAt(i)
                Else
                    i += 1
                End If
            End While

            RefreshInterface()
        End If
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
        CurrentQueueSource.Items.Clear()
        Queue.Items.Clear()
        Progress.Text = ""
        RefreshInterface()
    End Sub

    Private Sub Close_Click() Handles OK.Click
        Close()
    End Sub

    Private Sub Combine_Click() Handles Combine.Click
        Mode = "combine"
        Cancel.Location = Combine.Location
        SelectList()
    End Sub

    Private Sub Copy_Click() Handles Copy.Click
        Dim NewName As String = InputBox("Copy to:", "huggle", "Queue" & CStr(QueueSources.Count + 1))

        If NewName.Length > 0 Then
            If QueueSources.ContainsKey(NewName) Then
                MsgBox("A queue with the name '" & NewName & "' already exists. Choose another name.", _
                    MsgBoxStyle.Exclamation, "huggle")
            Else
                Dim NewQueueSource As New QueueSource
                NewQueueSource.Items.AddRange(CurrentQueueSource.Items)
                QueueSources.Add(NewName, NewQueueSource)
                Queues.Items.Add(NewName)
                Queues.SelectedItem = NewName
            End If
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

    Private Sub Queue_DoubleClick() Handles Queue.DoubleClick
        If Queue.SelectedIndex > -1 Then
            OpenUrlInBrowser(SitePath & "w/index.php?title=" & UrlEncode(Queue.SelectedItem.ToString))
        End If
    End Sub

    Private Sub Queue_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Queue.KeyDown
        If e.KeyCode = Keys.Delete AndAlso Queue.SelectedIndex > -1 Then
            CurrentQueueSource.Items.RemoveAt(Queue.SelectedIndex)
            Queue.Items.RemoveAt(Queue.SelectedIndex)
            RefreshInterface()
        End If
    End Sub

    Private Sub Queue_SelectedIndexChanged() Handles Queue.SelectedIndexChanged
        RemoveItem.Enabled = (Queue.SelectedIndex > -1)
    End Sub

    Private Sub Queues_SelectedIndexChanged() Handles Queues.SelectedIndexChanged
        Queue.Items.Clear()

        If Queues.SelectedIndex > -1 Then
            Queue.BeginUpdate()

            For Each Item As String In CurrentQueueSource.Items
                Queue.Items.Add(Item)
            Next Item

            Queue.EndUpdate()
        End If

        Progress.Text = ""
        RefreshInterface()
    End Sub

    Private Sub RemoveItem_Click() Handles RemoveItem.Click
        If Queue.SelectedIndex > -1 Then
            CurrentQueueSource.Items.Remove(Queue.SelectedItem.ToString)
            Queue.Items.Remove(Queue.SelectedItem)
            Queue.SelectedIndex = Queue.Items.Count - 1
            RefreshInterface()
        End If
    End Sub

    Private Sub RemoveQueue_Click() Handles RemoveQueue.Click
        If Queues.SelectedIndex > -1 Then
            QueueSources.Remove(Queues.SelectedItem.ToString)
            Queues.Items.Remove(Queues.SelectedItem)
            Queues.SelectedIndex = Queues.Items.Count - 1
        End If
    End Sub

    Private Sub Rename_Click() Handles Rename.Click
        Dim NewName As String = InputBox("Enter new name:", "huggle", Queues.SelectedItem.ToString)

        If NewName.Length > 0 Then
            If QueueSources.ContainsKey(NewName) Then
                MsgBox("A queue with the name '" & NewName & "' already exists. Choose another name.", _
                    MsgBoxStyle.Exclamation, "huggle")
            Else
                Dim NewQueueSource As New QueueSource
                NewQueueSource.Items.AddRange(CurrentQueueSource.Items)
                QueueSources.Remove(Queues.SelectedItem.ToString)
                Queues.Items(Queues.SelectedIndex) = NewName
            End If
        End If
    End Sub

    Private Sub Save_Click() Handles Save.Click
        Dim Dialog As New SaveFileDialog
        Dialog.FileName = Queues.SelectedItem.ToString & ".txt"
        Dialog.Filter = "Text file|*.txt"
        Dialog.Title = "Save queue"

        If Dialog.ShowDialog = DialogResult.OK Then
            Dim Items As New List(Of String)

            For Each Item As String In Queue.Items
                Items.Add("*[[" & Item & "]]")
            Next Item

            File.WriteAllLines(Dialog.FileName, Items.ToArray)
        End If
    End Sub

    Private Sub Sort_Click() Handles Sort.Click
        Queue.BeginUpdate()
        Queue.Sorted = True
        Queue.Sorted = False
        CurrentQueueSource.Items.Sort()
        Queue.EndUpdate()
    End Sub

    Private Sub Source_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Source.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Source.Text <> "" Then Combine_Click()
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

    Private Sub RefreshInterface()
        If Queue.Items.Count = 0 Then Combine.Text = "Add" Else Combine.Text = "Combine"
        Count.Text = CStr(Queue.Items.Count) & " items"
        If SourceType.Text = "File" Then Source.Width = 145 Else Source.Width = 221

        AddItem.Enabled = (Queues.SelectedIndex > -1)
        ArticlesOnly.Enabled = (Queues.SelectedIndex > -1)
        Browse.Enabled = (Queues.SelectedIndex > -1)
        Browse.Visible = (SourceType.Text = "File")
        Cancel.Visible = Throbber.Active
        Clear.Enabled = (Queues.SelectedIndex > -1 AndAlso Queue.Items.Count > 0)
        Copy.Enabled = (Queues.SelectedIndex > -1)
        Limit.Enabled = (Not Throbber.Active AndAlso Queues.SelectedIndex > -1)
        Queue.Enabled = (Queues.SelectedIndex > -1)
        QueueSelector.Enabled = (Queues.SelectedIndex > -1)
        QueueSelector.Visible = (SourceType.Text = "Existing queue")
        RemoveItem.Enabled = (Queue.SelectedIndex > -1)
        RemoveQueue.Enabled = (Queues.SelectedIndex > -1)
        Rename.Enabled = (Queues.SelectedIndex > -1)
        Save.Enabled = (Queue.Items.Count > 0)
        Sort.Enabled = (Queue.Items.Count > 1)
        Source.Enabled = Limit.Enabled
        Source.Visible = (SourceType.Text <> "Watchlist" AndAlso SourceType.Text <> "Existing queue")
        SourceType.Enabled = Limit.Enabled

        If QueueSelector.Visible Then
            QueueSelector.Items.Clear()

            For Each Item As Object In Queues.Items
                If Item IsNot Queues.SelectedItem Then QueueSelector.Items.Add(Item.ToString)
            Next Item

            If QueueSelector.Items.Count = 0 Then QueueSelector.Text = "" Else QueueSelector.SelectedIndex = 0

            Combine.Enabled = Not Throbber.Active AndAlso Queues.SelectedIndex > -1 _
                AndAlso (QueueSelector.SelectedIndex > -1)
            Exclude.Enabled = (Combine.Enabled AndAlso Queue.Items.Count > 0)
            Intersect.Enabled = Exclude.Enabled
        Else
            Combine.Enabled = Not Throbber.Active AndAlso Queues.SelectedIndex > -1 _
                AndAlso (Source.Text.Length > 0 OrElse SourceType.Text = "Watchlist")
            Exclude.Enabled = (Combine.Enabled AndAlso Queue.Items.Count > 0)
            Intersect.Enabled = Exclude.Enabled
        End If
    End Sub

    Private Sub SelectList()
        Select Case SourceType.Text
            Case "Backlinks" : GetList(New BacklinksRequest(Source.Text))
            Case "Category" : GetList(New CategoryRequest(Source.Text.Replace("Category:", "")))
            Case "Category (recursive)" : GetList(New RecursiveCategoryRequest(Source.Text.Replace("Category:", "")))
            Case "Existing queue" : GotList(QueueSources(QueueSelector.Text).Items)
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
        CurrentRequest.ArticlesOnly = ArticlesOnly.Checked
        CurrentRequest.Start(AddressOf GotList, AddressOf ListProgress)
        Throbber.Start()
        Progress.Text = "Running query..."
        RefreshInterface()
        Cancel.Focus()
    End Sub

    Private Sub ListProgress(ByVal State As String, ByVal PartialResult As List(Of String))
        Progress.Text = State

        If PartialResult IsNot Nothing AndAlso PartialResult.Count > 0 Then
            If Mode = "combine" Then
                Cancel.Text = "Stop"
                CombineItems(PartialResult)
            End If
        End If

        RefreshInterface()
    End Sub

    Private Sub GotList(ByVal Items As List(Of String))
        If Items Is Nothing Then
            Progress.Text = "Query failed."
        ElseIf Items.Count = 0 Then
            Progress.Text = "Query returned no results."
        Else
            Progress.Text = Items.Count & " results returned."

            Select Case Mode
                Case "combine" : CombineItems(Items)
                Case "intersect" : IntersectItems(Items)
                Case "exclude" : ExcludeItems(Items)
            End Select
        End If

        Throbber.Stop()
        RefreshInterface()
        Source.Focus()
    End Sub

    Private Sub CombineItems(ByVal Items As List(Of String))
        Queue.BeginUpdate()

        For Each Item As String In Items
            If Not CurrentQueueSource.Items.Contains(Item) Then
                CurrentQueueSource.Items.Add(Item)
                Queue.Items.Add(Item)
            End If
        Next Item

        Queue.EndUpdate()
    End Sub

    Private Sub IntersectItems(ByVal Items As List(Of String))
        Queue.BeginUpdate()

        Dim i As Integer = 0

        While i < CurrentQueueSource.Items.Count
            If Not Items.Contains(CurrentQueueSource.Items(i)) Then
                CurrentQueueSource.Items.RemoveAt(i)
                Queue.Items.RemoveAt(i)
            Else
                i += 1
            End If
        End While

        Queue.EndUpdate()
    End Sub

    Private Sub ExcludeItems(ByVal Items As List(Of String))
        Queue.BeginUpdate()

        Dim i As Integer = 0

        While i < CurrentQueueSource.Items.Count
            If Items.Contains(CurrentQueueSource.Items(i)) Then
                CurrentQueueSource.Items.RemoveAt(i)
                Queue.Items.RemoveAt(i)
            Else
                i += 1
            End If
        End While

        Queue.EndUpdate()
    End Sub

    Private Function GetFile(ByVal FileName As String) As List(Of String)
        Dim Items As New List(Of String)

        If File.Exists(FileName) Then
            For Each Item As String In File.ReadAllLines(FileName)
                If Item.StartsWith("*[[") OrElse Item.StartsWith("* [[") Then Item = Item.Substring(1)
                Item = StripIllegalCharacters(Item)
                If Item.Length = 1 Then Item = Item.ToUpper
                If Item.Length >= 1 Then Item = Item.Substring(0, 1).ToUpper & Item.Substring(1)
                If Item.Length > 0 Then Items.Add(Item)
            Next Item

            Return Items
        Else
            Return Nothing
        End If
    End Function

    Private Function StripIllegalCharacters(ByVal Text As String) As String
        Return Text.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("|", "") _
            .Replace("<", "").Replace(">", "").Replace("#", "").Replace(CChar(vbTab), "").Replace("_", " ").Trim(" "c)
    End Function

End Class