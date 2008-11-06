Imports System.IO

Class ClosingForm

    Private Sub ClosingForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon

        'Save everything
        SaveLocalConfig()
        SaveLists()
        SaveQueues()
        SaveWhitelist()

        If Mono() Then WebBrowser.ClearTempFiles()
        If Config.IrcMode Then Irc.Disconnect()
        If Config.ConfigVersion <> Config.Version Then Config.ConfigChanged = True

        If Config.LogFile IsNot Nothing AndAlso Config.LogFile.Length > 0 Then
            Dim LogItems As New List(Of String)

            For Each Item As ListViewItem In MainForm.Status.Items
                If Item.ForeColor <> Color.Red Then LogItems.Insert(0, Item.SubItems(1).Text)
            Next Item

            Try
                File.AppendAllText(Config.LogFile, CRLF & String.Join(CRLF, LogItems.ToArray))
            Catch ex As IOException
                MessageBox.Show("Unable to update log file: " & ex.Message, "Huggle", _
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        If Config.UpdateWhitelist AndAlso (WhitelistAutoChanges.Count > 0 _
            OrElse (Config.UpdateWhitelistManual AndAlso WhitelistManualChanges.Count > 0)) Then
            UpdateWhitelist()
        ElseIf Config.ConfigChanged Then
            UpdateUserConfig()
        Else
            End
        End If
    End Sub

    Private Sub UpdateWhitelist()
        Status.Text = Msg("closing-whitelist")
        Progress.Value = 1

        Dim NewRequest As New UpdateWhitelistRequest
        NewRequest.Start(AddressOf WhitelistDone)
    End Sub

    Public Sub WhitelistDone(Optional ByVal Result As RequestResult = Nothing)
        If Config.ConfigChanged Then
            UpdateUserConfig()
        Else
            End
        End If
    End Sub

    Private Sub UpdateUserConfig()
        Status.Text = Msg("closing-config")
        Progress.Value = 2

        Dim NewRequest As New SaveUserConfigRequest
        NewRequest.Start(AddressOf UpdateUserConfigDone)
    End Sub

    Public Sub UpdateUserConfigDone(ByVal Result As RequestResult)
        End
    End Sub

End Class