Class ExceptionForm

    Public Exception As Exception

    Private Sub ExceptionForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = "Bug report"
        ContinueButton.Text = Msg("continue")
        ExitButton.Text = Msg("exit")

        Details.Text = Exception.GetType.Name & ": " & Exception.Message & CRLF & Exception.StackTrace
        ContinueButton.Focus()
    End Sub

    Private Sub ContinueButton_Click() Handles ContinueButton.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub ExitButton_Click() Handles ExitButton.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ExceptionForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class