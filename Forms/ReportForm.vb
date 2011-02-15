'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for report form
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
Class ReportForm

    Public User As User, Edit As Edit

    Private Sub ReportForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("report-title", User.Name)
        Localize(Me, "report")

        Message.Text = Config.VandalReportReason
        Message.Focus()

        If Config.AIV Then Reason.Items.Add("Vandalism after final warning")
        If Config.UAA Then Reason.Items.Add("Inappropriate username")

        Reason.SelectedIndex = 0

        WarnLog.User = User
    End Sub

    Private Sub ReportForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ReportForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click

        Select Case Reason.Text
            Case "Vandalism after final warning"
                Dim NewRequest As New VandalReportRequest
                NewRequest.User = User
                NewRequest.Edit = Edit
                NewRequest.Reason = Message.Text
                NewRequest.Start()

            Case "Inappropriate username"
                Dim NewRequest As New UsernameReportRequest
                NewRequest.User = User
                NewRequest.Reason = Message.Text
                NewRequest.Start()
        End Select

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Message_TextChanged() Handles Message.TextChanged
        OK.Enabled = (Message.Text <> "")
    End Sub

    Private Sub Reason_SelectedIndexChanged() Handles Reason.SelectedIndexChanged
        Select Case Reason.Text
            Case "Vandalism after final warning"
                If Message.Text = "" OrElse Message.Text = "inappropriate username" Then Message.Text = Config.VandalReportReason
                Height = 311

            Case "Inappropriate username"
                If Message.Text = "" OrElse Message.Text = Config.VandalReportReason Then Message.Text = "inappropriate username"
                Height = 311
        End Select
    End Sub

End Class