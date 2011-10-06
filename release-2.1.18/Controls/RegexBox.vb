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

Imports System.Text.RegularExpressions

Class RegexBox

    'Control for user input of regular expressions

    Private _Regex As Regex

    Public ReadOnly Property Regex() As Regex
        Get
            Return _Regex
        End Get
    End Property

    Public Property Multiline() As Boolean
        Get
            Return Pattern.Multiline
        End Get
        Set(ByVal value As Boolean)
            Pattern.Multiline = value
        End Set
    End Property

    Public Overrides Property Text() As String
        Get
            Return Pattern.Text
        End Get
        Set(ByVal value As String)
            Pattern.Text = value
        End Set
    End Property

    Private Sub Pattern_TextChanged() Handles Pattern.TextChanged
        Try
            _Regex = New Regex(Pattern.Text)
            Status.Text = Nothing
            Pattern.BackColor = Color.FromKnownColor(KnownColor.Window)
            Pattern.ForeColor = Color.FromKnownColor(KnownColor.WindowText)

        Catch ex As ArgumentException
            _Regex = Nothing
            Status.Text = ex.Message.Substring(ex.Message.IndexOf(" - ") + 3)
            Pattern.BackColor = Color.LightCoral
            Pattern.ForeColor = Color.White
        End Try
    End Sub

End Class
