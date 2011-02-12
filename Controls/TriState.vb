'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for
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

Class TriState

    Private _State As CheckState = CheckState.Indeterminate
    Public Event CheckStateChanged()

    Public Property State() As CheckState
        Get
            Return _State
        End Get
        Set(ByVal value As CheckState)
            _State = value
            Refresh()
        End Set
    End Property

    Public Overrides Property Text() As String
        Get
            Return _Text.Text
        End Get
        Set(ByVal value As String)
            _Text.Text = value
            Width = _Text.Right
        End Set
    End Property

    Private Sub TriState_MouseDown(ByVal s As Object, ByVal e As MouseEventArgs) Handles Me.MouseDown, _Text.MouseDown
        Select Case _State
            Case CheckState.Indeterminate : State = CheckState.Checked
            Case CheckState.Checked : State = CheckState.Unchecked
            Case CheckState.Unchecked : State = CheckState.Indeterminate
        End Select

        RaiseEvent CheckStateChanged()
    End Sub

    Private Sub TriState_Paint(ByVal s As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        Select Case State
            Case CheckState.Indeterminate : e.Graphics.DrawImage(My.Resources.tri_none, 0, 0)
            Case CheckState.Checked : e.Graphics.DrawImage(My.Resources.tri_yes, 0, 0)
            Case CheckState.Unchecked : e.Graphics.DrawImage(My.Resources.tri_no, 0, 0)
        End Select
    End Sub

End Class
