Imports System.Threading

Class StartupForm

    Private Sub StartupForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        SyncContext = SynchronizationContext.Current
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        LoginForm.Show()
        Close()
    End Sub

    Private Sub DocsLink_LinkClicked(ByVal s As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles DocsLink.LinkClicked

        Process.Start(Config.DocsLocation)
    End Sub

    Private Sub ConfigLink_LinkClicked(ByVal s As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles ConfigLink.LinkClicked

        Process.Start(SitePath & "w/index.php?title=" & Config.UserConfigLocation)
    End Sub

    Private Sub StartupForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape : Close()
            Case Keys.Enter : OK_Click(Nothing, Nothing)
        End Select
    End Sub

End Class