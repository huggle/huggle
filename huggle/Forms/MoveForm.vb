'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for move form
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
Imports System.Text.RegularExpressions

Class MoveForm

    Public Page As Page

    Private Sub MoveForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("move-title", Page.Name)
        Localize(Me, "move")
        Target.Text = Page.Name
        MoveTalk.Visible = Page.IsTalkPage
    End Sub

    Private Sub OK_Click() Handles OK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        'If cancel is clicked close the form
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub MovePageForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Reason_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Reason.KeyDown
        If e.KeyCode = Keys.Enter AndAlso OK.Enabled Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub MovePageForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Target_TextChanged() Handles Target.TextChanged
        OK.Enabled = (Target.Text <> "" AndAlso Reason.Text <> "")
    End Sub

    Private Sub Reason_TextChanged() Handles Reason.TextChanged
        OK.Enabled = (Target.Text <> "" AndAlso Reason.Text <> "")
    End Sub

End Class