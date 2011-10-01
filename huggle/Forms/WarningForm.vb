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


Class WarningForm

    Public User As User

    Private Sub WarningForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("warning-title", User.Name)
        Localize(Me, "warning")

        For Each Item As String In Config.WarningTypes.Values
            WarnType.Items.Add(Item)
        Next Item

        WarnLog.User = User
        If WarnType.Items.Count > 0 Then WarnType.SelectedIndex = 0

        Level2.Visible = (Config.WarningMode <> "1")
        Level3.Visible = (Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2")
        LevelFinal.Visible = (Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2" _
            AndAlso Config.WarningMode <> "3")
    End Sub

    Private Sub WarningForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub WarningForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape : Close_Click()
            Case Keys.Enter : OK_Click()
            Case Keys.D1 : Level1.Checked = True
            Case Keys.D2 : Level2.Checked = True
            Case Keys.D3 : Level3.Checked = True
            Case Keys.D4 : LevelFinal.Checked = True
            Case Keys.A : LevelAuto.Checked = True
        End Select
    End Sub

    Private Sub Close_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        DialogResult = DialogResult.OK

        Dim NewWarningRequest As New WarningRequest
        NewWarningRequest.Edit = CurrentEdit

        If LevelAuto.Checked Then NewWarningRequest.Level = 0
        If Level1.Checked Then NewWarningRequest.Level = 1
        If Level2.Checked Then NewWarningRequest.Level = 2
        If Level3.Checked Then NewWarningRequest.Level = 3
        If LevelFinal.Checked Then NewWarningRequest.Level = 4

        For Each Item As KeyValuePair(Of String, String) In Config.WarningTypes
            If Item.Value = WarnType.Text Then
                NewWarningRequest.Type = Item.Key
                Exit For
            End If
        Next Item

        NewWarningRequest.Start()

        Close()
    End Sub

End Class