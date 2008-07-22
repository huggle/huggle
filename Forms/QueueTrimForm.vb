Class QueueTrimForm

    Public DiscardTime As Double

    Private Sub QueueTrimForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
    End Sub

    Private Sub QueueTrimForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub QueueTrimForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Val(DiscardTimeInput.Text) > 0 Then
            DiscardTime = Val(DiscardTimeInput.Text)
            DialogResult = DialogResult.OK
            Close()
        Else
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

End Class