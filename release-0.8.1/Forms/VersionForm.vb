Imports System.Diagnostics

Class VersionForm

    Private FileName As String = "huggle " & VersionString(Config.LatestVersion) & ".exe"
    Private Request As UpdateRequest

    Private Sub VersionForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button

        Message.Text = "This version of Huggle is out of date." & LF

        If Config.Version >= Config.MinVersion Then Message.Text &= "Updating to the latest version, " & _
            VersionString(Config.LatestVersion) & ", is recommended, and may be required in future."

        If Config.Version < Config.MinVersion Then Message.Text &= "You must update to the latest version, " & _
            VersionString(Config.LatestVersion) & "."

        Message.Text &= LF & "Download and run the latest version now?"
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
        Status.Text = "Downloading new version..."

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
            Status.Text = "Failed to download new version."
        End If
    End Sub

End Class