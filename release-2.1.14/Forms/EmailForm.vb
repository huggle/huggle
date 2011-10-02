'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for email form
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
Class EmailForm

    Public User As User

    Private Sub EmailForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("email-title", User.Name)
        Localize(Me, "email")
    End Sub

    Private Sub EmailForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub EmailForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub OK_Click() Handles Send.Click
        Dim NewRequest As New EmailRequest
        NewRequest.Subject = Subject.Text
        NewRequest.Message = Message.Text
        NewRequest.CcMe = CcMe.Checked
        NewRequest.Start()

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        Close()
    End Sub

    Private Sub Message_TextChanged() Handles Subject.TextChanged, Message.TextChanged
        'When there is something in the message and subject enable the ok button
        Send.Enabled = (Message.Text <> "" AndAlso Subject.Text <> "")
    End Sub

End Class