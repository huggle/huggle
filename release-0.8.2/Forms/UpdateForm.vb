Imports System.Diagnostics

Class UpdateForm

    Private FileName As String = "huggle " & VersionString(Config.LatestVersion) & ".exe"
    Private Request As UpdateRequest

    Private Sub VersionForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("update-title")

        Message.Text = Msg("update-notification") & CRLF

        If Config.Version >= Config.MinVersion _
            Then Message.Text &= Msg("update-optional", VersionString(Config.LatestVersion)) _
            Else Message.Text &= Msg("update-required", VersionString(Config.LatestVersion))

        Message.Text &= CRLF & Msg("update-prompt")
    End Sub

    Private Sub VersionForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape OrElse e.KeyCode = Keys.Enter Then Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        If Request IsNot Nothing Then Request.Cancel()
        Close()
    End Sub

    Private Sub Update_Click() Handles Download.Click
        Download.Enabled = False
        Progress.Visible = True
        Status.Text = Msg("update-progress")

        Request = New UpdateRequest
        Request.FileName = FileName
        Request.ProgressBar = Progress
        Request.Start(AddressOf UpdateDone)
    End Sub

    Private Sub UpdateDone(ByVal Result As Request.Output)
        If Result.Success Then
            Process.Start(FileName)
            End
        Else
            Progress.Value = 0
            Progress.Visible = False
            Download.Enabled = True
            Status.Text = Msg("update-error")
        End If
    End Sub

End Class