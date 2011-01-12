Class SockReportForm

    Public User As User

    Private Sub SockReportForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = "Reporting '" & User.Name & "'"
        Details.SetHighlighting(True)
    End Sub

    Private Sub Accounts_SelectedIndexChanged() Handles Accounts.SelectedIndexChanged
        Remove.Enabled = (Accounts.SelectedIndex > -1)
    End Sub

    Private Sub Add_Click() Handles Add.Click
        Dim Username As String = InputBox.Show("Enter username:")
        If Username Is Nothing Then Exit Sub
        Username = User.SanitizeName(Username)
        If Username IsNot Nothing AndAlso Not Accounts.Items.Contains(Username) Then Accounts.Items.Add(Username)
        Clear.Enabled = (Accounts.Items.Count > 0)
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        Close()
    End Sub

    Private Sub Clear_Click() Handles Clear.Click
        Accounts.Items.Clear()
        Remove.Enabled = (Accounts.SelectedIndex > -1)
        Clear.Enabled = False
    End Sub

    Private Sub Details_TextChanged() Handles Details.TextChanged
        OK.Enabled = (Details.Text <> "")
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Dim NewRequest As New SockReportRequest
        NewRequest.User = User
        NewRequest.Details = Details.Text
        NewRequest.Accounts = New List(Of String)

        For Each Item As String In Accounts.Items
            NewRequest.Accounts.Add(Item)
        Next Item

        NewRequest.Start()
        Close()
    End Sub

    Private Sub Remove_Click() Handles Remove.Click
        If Accounts.SelectedIndex > -1 Then Accounts.Items.Remove(Accounts.SelectedItem)
        Remove.Enabled = (Accounts.SelectedIndex > -1)
        Clear.Enabled = (Accounts.Items.Count > 0)
    End Sub

End Class