Class ExceptionForm

    Public Exception As Exception

    Private Sub ExceptionForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button

        Dim StackTrace As String = Exception.StackTrace

        'Extract only the useful bit of the stack trace
        If StackTrace.Contains("   at huggle.") _
            Then StackTrace = StackTrace.Substring(StackTrace.IndexOf("   at huggle."))

        Details.Text = Exception.GetType.Name & ": " & Exception.Message & vbCrLf & StackTrace
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