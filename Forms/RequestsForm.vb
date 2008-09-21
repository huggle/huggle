Class RequestsForm

    Private Sub RequestsForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("requests-title")

        List.Columns(0).Text = Msg("requests-time")
        List.Columns(1).Text = Msg("requests-type")
        List.Columns(3).Text = Msg("requests-query")

        Localize(Me, "requests")

        For Each Item As Request In AllRequests
            AddRequest(Item)
        Next Item
    End Sub

    Private Sub RequestsForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub CancelAll_Click() Handles CancelAll.Click
        While PendingRequests.Count > 0
            PendingRequests(0).Cancel()
        End While
    End Sub

    Private Sub CancelItem_Click() Handles CancelItem.Click
        CType(List.SelectedItems(0).Tag, Request).Cancel()
    End Sub

    Private Sub Clear_Click() Handles Clear.Click
        List.Items.Clear()
    End Sub

    Private Sub CloseButton_Click() Handles OK.Click
        Close()
    End Sub

    Private Sub CopyListItem_Click() Handles CopyListItem.Click
        If List.SelectedItems.Count > 0 Then Clipboard.SetText(List.SelectedItems(0).SubItems(3).Text)
    End Sub

    Private Sub List_SelectedIndexChanged() Handles List.SelectedIndexChanged
        CopyListItem.Enabled = (List.SelectedItems.Count > 0)
        CancelItem.Enabled = (List.SelectedItems.Count > 0 _
            AndAlso CType(List.SelectedItems(0).Tag, Request).State = Request.States.InProgress)
    End Sub

    Private Sub AddRequest(ByVal Request As Request)
        Dim NewItem As New ListViewItem

        Select Case Request.State
            Case Request.States.Cancelled : NewItem.BackColor = Color.DarkGray
            Case Request.States.Failed : NewItem.BackColor = Color.LightCoral
            Case Request.States.InProgress : NewItem.BackColor = Color.LightSteelBlue
        End Select

        NewItem.Text = Request.StartTime.ToLongTimeString
        NewItem.SubItems.Add(Request.GetType.Name.Replace("Request", ""))

        Select Case Request.Mode
            Case Request.Modes.Get : NewItem.SubItems.Add("Get")
            Case Request.Modes.Post : NewItem.SubItems.Add("Post")
            Case Else : NewItem.SubItems.Add("")
        End Select

        NewItem.Tag = Request
        NewItem.SubItems.Add(Request.Query)

        List.Items.Insert(0, NewItem)
    End Sub

    Public Sub UpdateList(ByVal Request As Request)
        List.BeginUpdate()

        Dim Done As Boolean = False

        For Each Item As ListViewItem In List.Items
            If Item.Tag Is Request Then
                Select Case Request.State
                    Case Request.States.Cancelled : Item.BackColor = Color.DarkGray
                    Case Request.States.Failed : Item.BackColor = Color.LightCoral
                    Case Request.States.InProgress : Item.BackColor = Color.LightSteelBlue
                    Case Else : Item.BackColor = Color.White
                End Select

                Item.Text = Request.StartTime.ToLongTimeString
                Item.SubItems(1).Text = Request.GetType.Name.Replace("Request", "")

                Select Case Request.Mode
                    Case Request.Modes.Get : Item.SubItems(2).Text = "Get"
                    Case Request.Modes.Post : Item.SubItems(2).Text = "Post"
                    Case Else : Item.SubItems(2).Text = ""
                End Select

                Item.SubItems(3).Text = Request.Query

                Done = True
                Exit For
            End If
        Next Item

        If Not Done Then AddRequest(Request)

        List.EndUpdate()
    End Sub

End Class