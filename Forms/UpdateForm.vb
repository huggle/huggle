Imports System.Diagnostics

Class UpdateForm

    Private FileName As String = "huggle " & VersionString(Config.LatestVersion) & ".exe"
    Private Request As UpdateRequest, _Result As RequestResult

    Private Sub VersionForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
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
        OpenUrlInBrowser(CStr(Config.DownloadLocation))
        Close()
    End Sub

    Private Sub UpdateDone(ByVal Result As RequestResult)
        If Result.Error Then
            _Result = Result
            Invoke(New Action(AddressOf UpdateResult))
        Else
            Process.Start(FileName)
            End
        End If
    End Sub

    Private Sub UpdateResult()
        Progress.Value = 0
        Progress.Visible = False
        Download.Enabled = True
        Status.Text = _Result.ErrorMessage
    End Sub

End Class