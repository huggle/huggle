'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for exception form
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
Class ExceptionForm

    Public Exception As Exception

    Private Sub ExceptionForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = "Bug report"
        ContinueButton.Text = Msg("continue")
        ExitButton.Text = Msg("exit")
        If Exception IsNot Nothing Then
            Details.Text = Exception.GetType.Name & ": " & Exception.Message & CRLF & Exception.StackTrace
        End If
        ContinueButton.Focus()
    End Sub

    Private Sub ContinueButton_Click() Handles ContinueButton.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub ExitButton_Click() Handles ExitButton.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ExceptionForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class