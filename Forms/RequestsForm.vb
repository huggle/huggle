Class RequestsForm

    Private WithEvents Timer As New Timer

    Private Sub RequestsForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Timer.Interval = 200
        Timer.Start()
    End Sub

    Private Sub RefreshList() Handles Timer.Tick
        List.SelectedItems.Clear()
        List.BeginUpdate()
        List.Items.Clear()

        For i As Integer = AllRequests.Count - 1 To 0 Step -1
            Dim NewListViewItem As New ListViewItem

            Select Case AllRequests(i).State
                Case Request.RequestState.Cancelled
                    NewListViewItem.BackColor = Color.DarkGray

                Case Request.RequestState.Failed
                    NewListViewItem.BackColor = Color.LightCoral

                Case Request.RequestState.InProgress
                    NewListViewItem.BackColor = Color.LightSteelBlue
            End Select

            NewListViewItem.Text = AllRequests(i).StartTime.ToLongTimeString
            NewListViewItem.SubItems.Add(AllRequests(i).GetType.Name.Replace("Request", ""))

            Select Case AllRequests(i).Mode
                Case Request.RequestMode.Get : NewListViewItem.SubItems.Add("Get")
                Case Request.RequestMode.Post : NewListViewItem.SubItems.Add("Post")
                Case Else : Continue For
            End Select

            NewListViewItem.SubItems.Add(AllRequests(i).Query)

            List.Items.Add(NewListViewItem)
        Next i

        List.EndUpdate()
    End Sub

End Class