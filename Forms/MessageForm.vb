Class MessageForm

    Public User As User

    Private Sub MessageForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("message-title", User.Name)
        Localize(Me, "message")
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Message.Text <> "" Then
            DialogResult = DialogResult.OK

            Dim NewRequest As New UserMessageRequest
            NewRequest.User = User
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

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub NewMessageForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Message_TextChanged() _
        Handles Message.TextChanged, Summary.TextChanged, Subject.TextChanged

        OK.Enabled = (Message.Text.Length > 0) AndAlso (Subject.Text <> "" OrElse Summary.Text <> "")
    End Sub

    Private Sub NewMessageForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class