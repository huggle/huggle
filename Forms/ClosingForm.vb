Imports System.IO

Class ClosingForm

    Private Sub ClosingForm_FormClosing() Handles Me.FormClosing
        End
    End Sub

    Private Sub ClosingForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon

        'Save everything
        SaveLocalConfig()
        SaveLists()
        SaveQueues()

        If Config.IrcMode Then Irc.Disconnect()

        If Config.LogFile IsNot Nothing AndAlso Config.LogFile.Length > 0 Then
            Dim LogItems As New List(Of String)

            For Each Item As ListViewItem In MainForm.Status.Items
                If Item.ForeColor <> Color.Red Then LogItems.Insert(0, Item.SubItems(1).Text)
            Next Item

            File.AppendAllText(Config.LogFile, CRLF & String.Join(CRLF, LogItems.ToArray))
        End If

        'Doing a final user edit count on users to be counted before close
        Dim NewCountRequest As New CountRequest
        NewCountRequest.Users.AddRange(NextCount)
        NewCountRequest.Start()
        NextCount.Clear()

        If Config.UpdateWhitelist AndAlso (WhitelistAutoChanges.Count > 0 _
            OrElse (Config.UpdateWhitelistManual AndAlso WhitelistManualChanges.Count > 0)) Then

            Status.Text = Msg("closing-whitelist")
            Progress.Value = 1
            Dim NewUpdateWhitelistRequest As New UpdateWhitelistRequest
            'Call the whitelist updating process
            NewUpdateWhitelistRequest.Start(AddressOf WhitelistDone)
        Else
            'Is the whitelist doesnt need to be updated go straight to the next task
            WhitelistDone()
        End If
    End Sub

    Public Sub WhitelistDone(Optional ByVal Result As RequestResult = Nothing)
        'If the config has been changed when running huggle or the version number is incorrect update the config
        If Config.ConfigChanged OrElse (Config.ConfigVersion <> Config.Version) Then
            Status.Text = Msg("closing-config")
            Progress.Value = 2

            Dim NewWriteConfigRequest As New SaveUserConfigRequest
            NewWriteConfigRequest.Invoke()
        End If
    End Sub

End Class