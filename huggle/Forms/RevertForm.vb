'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for revert form
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
Class RevertForm

    Public Edit As Edit
    Private Shared LastSummary As String

    Private Sub RevertForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("revert-title", Edit.Page.Name)
        Localize(Me, "revert")

        Summary.Items.AddRange(Config.RevertSummaries.ToArray)
        Summary.Text = LastSummary
        Summary.Focus()
        Summary.SelectAll()
    End Sub

    Private Sub RevertForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub RevertForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        DialogResult = DialogResult.OK

        If Not Config.RevertSummaries.Contains(Summary.Text) Then Config.RevertSummaries.Add(Summary.Text)
        LastSummary = Summary.Text
        DoRevert(CurrentEdit, Summary.Text, CurrentOnly:=CurrentOnly.Checked)

        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Summary_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Summary.KeyDown
        If e.KeyCode = Keys.Enter Then OK_Click()
    End Sub

End Class