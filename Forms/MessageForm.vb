'This is a source code or part of Huggle project

'Copyright (C) 2011 Huggle team

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.
Class MessageForm

    Public User As User

    Private Sub MessageForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("message-title", User.Name)
        Localize(Me, "message")
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Message.Text <> "" Then
            DialogResult = DialogResult.OK

            Dim NewRequest As New UserMessageRequest
            NewRequest.User = User
            NewRequest.Message = Message.Text
            NewRequest.Title = Subject.Text
            If Summary.Text = "" Then NewRequest.Summary = "/* " & Subject.Text & " */ - new section" _
                Else NewRequest.Summary = Summary.Text
            NewRequest.Watch = Config.Watch("message")
            NewRequest.Minor = Config.Minor("message")
            NewRequest.AutoSign = AutoSign.Checked
            NewRequest.SuppressAutoSummary = True
            NewRequest.Start()

            Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub NewMessageForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Message_TextChanged() _
        Handles Message.TextChanged, Summary.TextChanged, Subject.TextChanged

        OK.Enabled = (Message.Text.Length > 0) AndAlso (Subject.Text <> "" OrElse Summary.Text <> "")
    End Sub

    Private Sub NewMessageForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

End Class