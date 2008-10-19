Class EmailForm

    Public User As User

    Private Sub EmailForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("email-title", User.Name)
        Localize(Me, "email")
    End Sub

    Private Sub EmailForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub EmailForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub OK_Click() Handles Send.Click
        Dim NewRequest As New EmailRequest
        NewRequest.Subject = Subject.Text
        NewRequest.Message = Message.Text
        NewRequest.CcMe = CcMe.Checked
        NewRequest.Start()

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        Close()
    End Sub

    Private Sub Message_TextChanged() Handles Subject.TextChanged, Message.TextChanged
        'When there is something in the message and subject enable the ok button
        Send.Enabled = (Message.Text <> "" AndAlso Subject.Text <> "")
    End Sub

End Class