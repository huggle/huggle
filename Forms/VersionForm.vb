Class VersionForm

    Private Sub VersionForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Close()
    End Sub

    Private Sub VersionForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape OrElse e.KeyCode = Keys.Enter Then Close()
    End Sub

End Class