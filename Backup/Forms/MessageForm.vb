Class MessageForm

    Public User As User

    Private Sub MessageForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Text = "Message " & User.Name
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        If Message.Text <> "" Then
            DialogResult = DialogResult.OK

            Dim NewRequest As New UserMessageRequest
            NewRequest.ThisUser = User
            NewRequest.Message = Message.Text
            NewRequest.Title = Subject.Text
            If Summary.Text = "" Then NewRequest.Summary = "/* " & Subject.Text & " */ - new section" _
                Else NewRequest.Summary = Summary.Text
            NewRequest.Watch = Config.WatchNotifications
            NewRequest.Minor = Config.MinorNotifications
            NewRequest.AutoSign = AutoSign.Checked
            NewRequest.Start()

            Close()
        End If
    End Sub

    Private Sub Cancel_Click(ByVal s As Object, ByVal e As EventArgs) Handles Cancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub NewMessageForm_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Message_TextChanged(ByVal s As Object, ByVal e As EventArgs) _
        Handles Message.TextChanged, Summary.TextChanged, Subject.TextChanged

        OK.Enabled = (Message.Text.Length > 0) AndAlso (Subject.Text <> "" OrElse Summary.Text <> "")
    End Sub

    Private Sub NewMessageForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class