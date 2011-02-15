'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code
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
        Progress.Visible = True
        Status.Text = Msg("update-progress")

        Request = New UpdateRequest
        Request.Filename = FileName
        Request.ProgressBar = Progress
        Request.Start(AddressOf UpdateDone)
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