Class EmailForm

    Public User As User, Request As EmailRequest

    Private Sub EmailForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = "E-mail " & User.Name
    End Sub

    Private Sub EmailForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub EmailForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Request.Message = Message.Text
        Request.Subject = Subject.Text
        Request.CcMe = CcMe.Checked
        Request.PostForm()

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        Request.Cancel()
        Close()
    End Sub

    Private Sub Message_TextChanged() Handles Subject.TextChanged, Message.TextChanged
        OK.Enabled = (Message.Text <> "" AndAlso Subject.Text <> "")
    End Sub

End Class