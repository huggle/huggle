'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for addtemplate form
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

Class AddTemplateForm

    Public ReadOnly Property DisplayText() As String
        Get
            Return DisplayTextBox.Text
        End Get
    End Property

    Public ReadOnly Property Template() As String
        Get
            Return TemplateBox.Text
        End Get
    End Property

    Private Sub AddTemplateForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("config-addtemplate")

        DisplayTextLabel.Text = Msg("config-templatetext")
        TemplateLabel.Text = Msg("config-template")
    End Sub

    Private Sub AddTemplateForm_FormClosing() Handles MyBase.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub AddTemplateForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ItemTextChanged() Handles DisplayTextBox.TextChanged, TemplateBox.TextChanged
        OK.Enabled = (DisplayTextBox.Text <> "" AndAlso TemplateBox.Text <> "")
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If DisplayTextBox.Text <> "" AndAlso TemplateBox.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Template_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles TemplateBox.KeyDown
        If e.KeyCode = Keys.Enter AndAlso DisplayTextBox.Text <> "" AndAlso TemplateBox.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

End Class