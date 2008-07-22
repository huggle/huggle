Imports System.IO

Class QueueForm

    Public Mode As String

    Private Sub QueueForm_Load() Handles MyBase.Load
        Icon = My.Resources.icon_red_button
        SourceType.SelectedIndex = 0

        QueueSourcesList.BeginUpdate()

        For Each Item As String In QueueSources.Keys
            QueueSourcesList.Items.Add(Item)
        Next Item

        QueueSourcesList.EndUpdate()
    End Sub

    Private Sub QueueForm_FormClosing() Handles Me.FormClosing
        Main.SetQueueSources()
    End Sub

    Private Sub QueueSourcesList_SelectedIndexChanged() _
        Handles QueueSourcesList.SelectedIndexChanged

        RemoveQueue.Enabled = (QueueSourcesList.SelectedIndex > -1)
        Rename.Enabled = (QueueSourcesList.SelectedIndex > -1)
        Copy.Enabled = (QueueSourcesList.SelectedIndex > -1)
        Source.Enabled = (QueueSourcesList.SelectedIndex > -1)
        SourceType.Enabled = (QueueSourcesList.SelectedIndex > -1)
        Browse.Enabled = (QueueSourcesList.SelectedIndex > -1)
        AddItem.Enabled = (QueueSourcesList.SelectedIndex > -1)
        QueueItems.Enabled = (QueueSourcesList.SelectedIndex > -1)

        QueueItems.Items.Clear()

        If QueueSourcesList.SelectedIndex > -1 Then
            QueueItems.BeginUpdate()

            For Each Item As String In QueueSources(QueueSourcesList.SelectedItem.ToString)
                QueueItems.Items.Add(Item)
            Next Item

            QueueItems.EndUpdate()
        End If

        RefreshInterface()
    End Sub

    Private Sub Close_Click() Handles OK.Click
        Close()
    End Sub

    Private Sub Browse_Click() Handles Browse.Click
        Dim Dialog As New OpenFileDialog
        Dialog.Title = "Add text file to queue"
        Dialog.Filter = "All files|*.*"

        If Dialog.ShowDialog() = DialogResult.OK Then Source.Text = Dialog.FileName
    End Sub

    Private Sub SourceType_SelectedIndexChanged() Handles SourceType.SelectedIndexChanged
        Browse.Visible = (SourceType.Text = "File")

        Select Case SourceType.Text
            Case "File" : SourceLabel.Text = "File:"
            Case "Category" : SourceLabel.Text = "Category:"
        End Select

        Source.Clear()
    End Sub

    Private Sub Sort_Click() Handles Sort.Click
        QueueItems.BeginUpdate()
        QueueItems.Sorted = True
        QueueItems.Sorted = False
        QueueSources(QueueSourcesList.SelectedItem.ToString).Sort()
        QueueItems.EndUpdate()
    End Sub

    Private Sub AddQueue_Click() Handles AddQueue.Click
        Dim Input As String = InputBox("Enter queue name:", "huggle", "Queue" & CStr(QueueSources.Count + 1))

        If Input.Length > 0 Then
            For Each Item As String In QueueSources.Keys
                If Input = Item Then
                    MsgBox("A queue with the name '" & Item & "' already exists. Choose another name.", _
                        MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            Next Item

            QueueSources.Add(Input, New List(Of String))
            QueueSourcesList.Items.Add(Input)
            QueueSourcesList.SelectedItem = Input
        End If
    End Sub

    Private Sub RemoveQueue_Click() Handles RemoveQueue.Click
        If QueueSourcesList.SelectedIndex > -1 Then
            QueueSources.Remove(QueueSourcesList.SelectedItem.ToString)
            QueueSourcesList.Items.Remove(QueueSourcesList.SelectedItem)
            QueueSourcesList.SelectedIndex = QueueSourcesList.Items.Count - 1
        End If
    End Sub

    Private Sub Source_TextChanged() Handles Source.TextChanged
        RefreshInterface()
    End Sub

    Private Sub RefreshInterface()
        If QueueItems.Items.Count = 0 Then Combine.Text = "Add" Else Combine.Text = "Combine"
        Clear.Enabled = (QueueSourcesList.SelectedIndex > -1 AndAlso QueueItems.Items.Count > 0)
        Combine.Enabled = (Source.Text.Length > 0)
        Intersect.Enabled = (Source.Text.Length > 0 AndAlso QueueItems.Items.Count > 0)
        Replace.Enabled = (Source.Text.Length > 0 AndAlso QueueItems.Items.Count > 0)
        RemoveItem.Enabled = (QueueItems.Items.Count > 0)
        Sort.Enabled = (QueueItems.Items.Count > 0)
        ArticlesOnly.Enabled = (QueueItems.Items.Count > 0)
        Save.Enabled = (QueueItems.Items.Count > 0)
        Count.Text = CStr(QueueItems.Items.Count) & " items"
    End Sub

    Private Sub Combine_Click() Handles Combine.Click
        Mode = "combine"

        Select Case SourceType.Text
            Case "File" : CombineItems(GetFile(Source.Text))
            Case "Category" : GetCategory(Source.Text)
        End Select
    End Sub

    Private Sub Intersect_Click() Handles Intersect.Click
        Mode = "intersect"

        Select Case SourceType.Text
            Case "File" : IntersectItems(GetFile(Source.Text))
            Case "Category" : GetCategory(Source.Text)
        End Select
    End Sub

    Private Sub Replace_Click() Handles Replace.Click
        Mode = "replace"

        Select Case SourceType.Text
            Case "File" : ReplaceItems(GetFile(Source.Text))
            Case "Category" : GetCategory(Source.Text)
        End Select
    End Sub

    Private Sub CombineItems(ByVal Items As List(Of String))
        QueueItems.BeginUpdate()

        For Each Item As String In Items
            If Not QueueSources(QueueSourcesList.SelectedItem.ToString).Contains(Item) Then
                QueueSources(QueueSourcesList.SelectedItem.ToString).Add(Item)
                QueueItems.Items.Add(Item)
            End If
        Next Item

        QueueItems.EndUpdate()
        RefreshInterface()
    End Sub

    Private Sub IntersectItems(ByVal Items As List(Of String))
        QueueItems.BeginUpdate()

        Dim i As Integer = 0

        While i < QueueSources(QueueSourcesList.SelectedItem.ToString).Count
            If Not Items.Contains(QueueSources(QueueSourcesList.SelectedItem.ToString)(i)) Then
                QueueSources(QueueSourcesList.SelectedItem.ToString).RemoveAt(i)
                QueueItems.Items.RemoveAt(i)
            Else
                i += 1
            End If
        End While

        QueueItems.EndUpdate()
        RefreshInterface()
    End Sub

    Private Sub ReplaceItems(ByVal Items As List(Of String))
        QueueItems.Items.Clear()
        QueueSources(QueueSourcesList.SelectedItem.ToString).Clear()
        QueueItems.BeginUpdate()

        For Each Item As String In Items
            QueueSources(QueueSourcesList.SelectedItem.ToString).Add(Item)
            QueueItems.Items.Add(Item)
        Next Item

        QueueItems.EndUpdate()
        RefreshInterface()
    End Sub

    Private Function GetFile(ByVal FileName As String) As List(Of String)
        Dim Items As New List(Of String)

        For Each Item As String In File.ReadAllLines(FileName)
            If Item.StartsWith("*[[") OrElse Item.StartsWith("#[[") OrElse Item.StartsWith("* [[") _
                OrElse Item.StartsWith("# [[") Then Item = Item.Substring(1)
            Item = Item.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Trim(" "c)
            If Item.Length = 1 Then Item = Item.ToUpper
            If Item.Length >= 1 Then Item = Item.Substring(0, 1).ToUpper & Item.Substring(1)
            If Item.Length > 0 Then Items.Add(Item)
        Next Item

        Return Items
    End Function

    Private Sub GetCategory(ByVal CategoryName As String)
        Source.Enabled = False

        Dim NewCategoryRequest As New CategoryRequest
        NewCategoryRequest.Category = Source.Text
        NewCategoryRequest.Start(AddressOf GetCategoryDone)
    End Sub

    Private Sub GetCategoryDone(ByVal Items As List(Of String))
        If Items Is Nothing Then
            MsgBox("Failed to retrieve contents of Category:" & Source.Text & ".", MsgBoxStyle.Critical, "huggle")
        ElseIf Items.Count = 0 Then
            MsgBox("Category:" & Source.Text & " is empty or does not exist", MsgBoxStyle.Critical, "huggle")
        Else
            Select Case Mode
                Case "combine" : CombineItems(Items)
                Case "intersect" : IntersectItems(Items)
                Case "replace" : ReplaceItems(Items)
            End Select
        End If

        Source.Enabled = True
    End Sub

    Private Sub QueueItems_SelectedIndexChanged() _
        Handles QueueItems.SelectedIndexChanged

        RemoveItem.Enabled = (QueueItems.SelectedIndex > -1)
    End Sub

    Private Sub AddItem_Click() Handles AddItem.Click
        Dim Input As String = InputBox("Enter page name:", "huggle")

        If Input.Length > 0 AndAlso Not QueueItems.Items.Contains(Input) Then
            If Input.Length = 1 Then Input = Input.ToUpper
            If Input.Length >= 1 Then Input = Input.Substring(0, 1).ToUpper & Input.Substring(1)

            If QueueItems.Items.Contains(Input) Then
                MsgBox("This queue already contains the page '" & Input & "'.", MsgBoxStyle.Exclamation, "huggle")
            Else
                QueueItems.Items.Add(Input)
                QueueSources(QueueSourcesList.SelectedItem.ToString).Add(Input)
                QueueItems.SelectedIndex = QueueItems.Items.Count - 1
                RefreshInterface()
            End If
        End If
    End Sub

    Private Sub RemoveItem_Click() Handles RemoveItem.Click
        If QueueItems.SelectedIndex > -1 Then
            QueueSources(QueueSourcesList.SelectedItem.ToString).Remove(QueueItems.SelectedItem.ToString)
            QueueItems.Items.Remove(QueueItems.SelectedItem)
            QueueItems.SelectedIndex = QueueItems.Items.Count - 1
            RefreshInterface()
        End If
    End Sub

    Private Sub Clear_Click() Handles Clear.Click
        QueueSources(QueueSourcesList.SelectedItem.ToString).Clear()
        QueueItems.Items.Clear()
        RefreshInterface()
    End Sub

    Private Sub QueueItems_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles QueueItems.KeyDown
        If e.KeyCode = Keys.Delete AndAlso QueueItems.SelectedIndex > -1 Then
            QueueSources(QueueSourcesList.SelectedItem.ToString).RemoveAt(QueueItems.SelectedIndex)
            QueueItems.Items.RemoveAt(QueueItems.SelectedIndex)
            RefreshInterface()
        End If
    End Sub

    Private Sub ArticlesOnly_Click() Handles ArticlesOnly.Click
        Dim i As Integer = 0

        While i < QueueItems.Items.Count
            Dim Removed As Boolean = False

            For Each Item As String In Namespaces
                If QueueItems.Items(i).ToString.StartsWith(Item) Then
                    QueueItems.Items.RemoveAt(i)
                    QueueSources(QueueSourcesList.SelectedItem.ToString).RemoveAt(i)
                    Removed = True
                    Exit For
                End If
            Next Item

            If Not Removed Then i += 1
        End While

        RefreshInterface()
    End Sub

    Private Sub Rename_Click() Handles Rename.Click
        Dim NewName As String = InputBox("Enter new name:", "huggle", QueueSourcesList.SelectedItem.ToString)

        If NewName.Length > -1 Then
            Dim List As List(Of String) = QueueSources(QueueSourcesList.SelectedItem.ToString)
            QueueSources.Remove(QueueSourcesList.SelectedItem.ToString)
            QueueSources.Add(NewName, List)
            QueueSourcesList.Items(QueueSourcesList.SelectedIndex) = NewName
        End If
    End Sub

    Private Sub Save_Click() Handles Save.Click
        Dim Dialog As New SaveFileDialog
        Dialog.FileName = QueueSourcesList.SelectedItem.ToString & ".txt"
        Dialog.Filter = "Text file|*.txt"
        Dialog.Title = "Save queue"

        If Dialog.ShowDialog = DialogResult.OK Then
            Dim Items As New List(Of String)

            For Each Item As String In QueueItems.Items
                Items.Add("*[[" & Item & "]]")
            Next Item

            File.WriteAllLines(Dialog.FileName, Items.ToArray)
        End If
    End Sub

    Private Sub Copy_Click() Handles Copy.Click
        Dim NewName As String = InputBox("Copy to:", "huggle", "Queue" & CStr(QueueSources.Count + 1))

        If NewName.Length > 0 Then
            For Each Item As String In QueueSources.Keys
                If NewName = Item Then
                    MsgBox("A queue with the name '" & Item & "' already exists. Choose another name.", _
                        MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            Next Item

            QueueSources.Add(NewName, New List(Of String)(QueueSources(QueueSourcesList.SelectedItem.ToString)))
            QueueSourcesList.Items.Add(NewName)
            QueueSourcesList.SelectedItem = NewName
        End If
    End Sub

End Class