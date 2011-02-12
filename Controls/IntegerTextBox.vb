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
Class IntegerTextBox

    Inherits TextBox

    'Text box which restricts input to integer values

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)

        Dim Position As Integer = SelectionStart, i As Integer

        While i < Text.Length
            If Not Char.IsDigit(Text(i)) Then
                Text = Text.Remove(i, 1)
                Position -= 1
            Else
                i += 1
            End If
        End While

        SelectionStart = Position
    End Sub

    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    Public Property Value() As Integer
        Get
            If Text = "" Then Return 0 Else Return CInt(Text)
        End Get
        Set(ByVal value As Integer)
            MyBase.Text = CStr(value)
        End Set
    End Property

End Class
