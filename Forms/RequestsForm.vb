Class RequestsForm

    Private Sub RequestsForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button

        For Each Item As Request In AllRequests
            AddRequest(Item)
        Next Item
    End Sub

    Private Sub CancelAll_Click() Handles CancelAll.Click
        While PendingRequests.Count > 0
            PendingRequests(0).Cancel()
        End While
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