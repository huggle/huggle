Class ExceptionForm

    Public Exception As Exception

    Private Sub ExceptionForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("error")
        ContinueButton.Text = Msg("continue")
        ExitButton.Text = Msg("exit")

        Details.Text = Exception.GetType.Name & ": " & Exception.Message & LF & Exception.StackTrace
    End Sub

    Private Sub ContinueButton_Click() Handles ContinueButton.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub ExitButton_Click() Handles ExitButton.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

End Class