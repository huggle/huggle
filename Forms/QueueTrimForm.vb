Class QueueTrimForm

    Private Sub QueueTrimForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("queuetrim-title")
        Localize(Me, "queuetrim")
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
        CurrentQueue.RemoveOldEdits(CInt(Age.Value))
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

End Class