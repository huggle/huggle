'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for Closin
'last modified by Petrb

'Copyright (C) 2011 Huggle team

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.
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

        If Config.ConfigChanged AndAlso Config.SaveConfig Then
            UpdateUserConfig()

        ElseIf Config.WhitelistLocation IsNot Nothing AndAlso Config.UpdateWhitelist AndAlso _
            (WhitelistAutoChanges.Count > 0 OrElse (Config.UpdateWhitelistManual AndAlso _
            WhitelistManualChanges.Count > 0)) Then

            UpdateWhitelist()
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
        End
    End Sub

    Private Sub UpdateUserConfig()
        Status.Text = Msg("closing-config")
        Progress.Value = 2

        Dim NewRequest As New SaveUserConfigRequest
        NewRequest.Start(AddressOf UpdateUserConfigDone)
    End Sub

    Public Sub UpdateUserConfigDone(ByVal Result As RequestResult)
        If Config.WhitelistLocation IsNot Nothing AndAlso Config.UpdateWhitelist AndAlso _
            (WhitelistAutoChanges.Count > 0 OrElse (Config.UpdateWhitelistManual AndAlso _
            WhitelistManualChanges.Count > 0)) Then

            UpdateWhitelist()
        Else
            End
        End If
    End Sub

End Class