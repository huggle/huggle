Imports System.Threading

Class StartupForm

    Private Sub StartupForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        SyncContext = SynchronizationContext.Current
    End Sub

    Private Sub OK_Click() Handles OK.Click
        LoginForm.Show()
        Close()
    End Sub

    Private Sub DocsLink_LinkClicked() Handles DocsLink.LinkClicked
        Tools.OpenUrlInBrowser(Config.DocsLocation)
    End Sub

    Private Sub ConfigLink_LinkClicked() Handles ConfigLink.LinkClicked
        Tools.OpenUrlInBrowser(SitePath & "w/index.php?title=" & Config.UserConfigLocation)
    End Sub

    Private Sub StartupForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape : Close()
            Case Keys.Enter : OK_Click()
        End Select
    End Sub

End Class