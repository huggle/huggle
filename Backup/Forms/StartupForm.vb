Imports System.Threading

Class StartupForm

    Private Sub StartupForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        SyncContext = SynchronizationContext.Current

        Status.Text = "Checking global configuration page..."
        OK.Enabled = False
        Progress.Value = 1
        Dim NewRequest As New GlobalConfigRequest
        NewRequest.Start()
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        If OK.Text = "Retry" Then
            StartupForm_Load(Nothing, Nothing)

        ElseIf OK.Text = "Close" Then
            End

        Else
            LoginForm.Show()
            Close()
        End If
    End Sub

    Private Sub DocsLink_LinkClicked(ByVal s As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles DocsLink.LinkClicked

        Process.Start(Config.DocsLocation)
    End Sub

    Private Sub ConfigLink_LinkClicked(ByVal s As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles ConfigLink.LinkClicked

        Process.Start(SitePath & "w/index.php?title=" & Config.UserConfigLocation)
    End Sub

    Public Sub GlobalConfigDone()
        Progress.Value = 2
        Status.Text = "Click ""Continue"" to log in."
        OK.Enabled = True
        OK.Text = "Continue >"
        OK.Focus()
    End Sub

    Public Sub GlobalConfigFailed()
        Status.Text = "Failed to retrieve global configuration page."
        OK.Enabled = True
        OK.Text = "Retry"
    End Sub

    Public Sub GlobalConfigDisabled()
        Status.Text = "Huggle is disabled for all projects."
        OK.Enabled = True
        OK.Text = "Close"
    End Sub

    Private Sub StartupForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class