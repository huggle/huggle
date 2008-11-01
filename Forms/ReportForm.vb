Class ReportForm

    Public User As User, Edit As Edit

    Private Sub ReportForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = "Report " & User.Name
        Message.Focus()

        If Config.AIV Then Reason.Items.Add("Vandalism after final warning")
        If Config.UAA Then Reason.Items.Add("Inappropriate username")

        Reason.SelectedIndex = 0

        WarnLog.User = User
    End Sub

    Private Sub ReportForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ReportForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click

        Select Case Reason.Text
            Case "Vandalism after final warning"
                Dim NewRequest As New VandalReportRequest
                NewRequest.User = User
                NewRequest.Edit = Edit
                NewRequest.Reason = Message.Text
                NewRequest.Start()

            Case "Inappropriate username"
                Dim NewRequest As New UsernameReportRequest
                NewRequest.User = User
                NewRequest.Reason = Message.Text
                NewRequest.Start()
        End Select

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Message_TextChanged() Handles Message.TextChanged
        OK.Enabled = (Message.Text <> "")
    End Sub

    Private Sub Reason_SelectedIndexChanged() Handles Reason.SelectedIndexChanged
        Select Case Reason.Text
            Case "Vandalism after final warning"
                If Message.Text = "" OrElse Message.Text = "inappropriate username" Then Message.Text = "vandalism"
                Height = 311

            Case "Inappropriate username"
                If Message.Text = "" OrElse Message.Text = "vandalism" Then Message.Text = "inappropriate username"
                Height = 311
        End Select
    End Sub

End Class