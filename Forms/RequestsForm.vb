Class RequestsForm

    Private Sub RequestsForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button

        For Each Item As Request In AllRequests
            AddRequest(Item)
        Next Item
    End Sub

    Private Sub AddRequest(ByVal Request As Request)
        Dim NewItem As New ListViewItem

        Select Case Request.State
            Case Request.RequestState.Cancelled : NewItem.BackColor = Color.DarkGray
            Case Request.RequestState.Failed : NewItem.BackColor = Color.LightCoral
            Case Request.RequestState.InProgress : NewItem.BackColor = Color.LightSteelBlue
        End Select

        NewItem.Text = Request.StartTime.ToLongTimeString
        NewItem.SubItems.Add(Request.GetType.Name.Replace("Request", ""))

        Select Case Request.Mode
            Case Request.RequestMode.Get : NewItem.SubItems.Add("Get")
            Case Request.RequestMode.Post : NewItem.SubItems.Add("Post")
            Case Else : NewItem.SubItems.Add("")
        End Select

        NewItem.SubItems.Add(Request.Query)

        List.Items.Add(NewItem)
    End Sub

    Public Sub UpdateList(ByVal Request As Request)
        List.BeginUpdate()

        Dim Done As Boolean = False

        For Each Item As ListViewItem In List.Items
            If Item.Tag Is Request Then
                Select Case Request.State
                    Case Request.RequestState.Cancelled : Item.BackColor = Color.DarkGray
                    Case Request.RequestState.Failed : Item.BackColor = Color.LightCoral
                    Case Request.RequestState.InProgress : Item.BackColor = Color.LightSteelBlue
                End Select

                Item.Text = Request.StartTime.ToLongTimeString
                Item.SubItems(0).Text = Request.GetType.Name.Replace("Request", "")

                Select Case Request.Mode
                    Case Request.RequestMode.Get : Item.SubItems(1).Text = "Get"
                    Case Request.RequestMode.Post : Item.SubItems(1).Text = "Post"
                    Case Else : Item.SubItems(1).Text = ""
                End Select

                Item.SubItems(2).Text = Request.Query

                Done = True
                Exit For
            End If
        Next Item

        If Not Done Then AddRequest(Request)

        List.EndUpdate()
    End Sub

End Class