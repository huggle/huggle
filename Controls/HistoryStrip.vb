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

Class HistoryStrip

    Inherits ToolStripItem

    Public Page As Page, Offset As Integer, OlderEdit, NewerEdit As Edit

    Public Sub Draw(ByVal s As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
        Dim Gfx As Graphics = e.Graphics

        Gfx.Clear(Color.FromKnownColor(KnownColor.Control))
        Gfx.DrawImage(My.Resources.gradient, 2, 2, Width - 4, Height - 4)
        Gfx.DrawRectangle(Pens.DarkGray, 1, 1, Width - 3, Height - 3)

        If Page IsNot Nothing Then
            Dim OlderPosition As Integer = -1, NewerPosition As Integer = -1
            Dim Edit As Edit = Page.LastEdit
            Dim X As Integer = Width - 18 + (Offset * 17)

            While Edit IsNot Nothing AndAlso Edit IsNot NullEdit
                If X < 0 Then Exit While

                If X < Width Then
                    Try
                        Gfx.DrawImage(Edit.Icon, X, 2)
                    Catch ex As InvalidOperationException
                    End Try

                    Select Case Edit.WarningLevel
                        Case UserLevel.Warn1 : Gfx.DrawString("!1", Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn2 : Gfx.DrawString("!2", Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn3 : Gfx.DrawString("!3", Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn4im, UserLevel.WarnFinal : Gfx.DrawString("!4", Font, Brushes.Black, X + 2, 3)
                    End Select

                    If Edit.Assisted AndAlso Edit.Type = EditType.None AndAlso Edit.WarningLevel = UserLevel.None _
                        AndAlso Not Edit.Bot Then Gfx.DrawString("*", New Font(FontFamily.GenericSansSerif, _
                        14, FontStyle.Regular), Brushes.Black, X + 2, 3)

                    If OlderEdit IsNot Nothing AndAlso Edit.Id = OlderEdit.Id Then OlderPosition = X - 1
                    If NewerEdit IsNot Nothing AndAlso Edit.Id = NewerEdit.Id Then NewerPosition = X - 1

                    If Edit.Page IsNot Nothing AndAlso Edit Is Edit.Page.LastEdit _
                        Then Gfx.DrawRectangle(Pens.DarkBlue, X, 2, 15, 15)

                    Gfx.DrawLine(Pens.DarkGray, X + 16, 2, X + 16, 17)
                End If

                X -= 17
                Edit = Edit.Prev
            End While

            If X < 0 AndAlso Edit IsNot Nothing AndAlso (Edit Is CurrentEdit _
                OrElse Edit.Id = CurrentEdit.Oldid) AndAlso Not (Edit Is Page.LastEdit _
                OrElse Edit Is Page.LastEdit.Prev) Then

                Offset += 1
                Draw(s, e)
            End If

            Gfx.DrawLine(Pens.DarkGray, X + 16, 2, X + 16, 17)

            'Draw selection box
            If NewerPosition > -1 AndAlso OlderPosition > -1 Then
                If OlderEdit IsNot Nothing AndAlso NewerEdit IsNot Nothing AndAlso NewerEdit.Prev IsNot Nothing _
                    AndAlso OlderEdit.Id = NewerEdit.Prev.Id Then

                    Gfx.DrawRectangle(New Pen(Color.Red, 2), OlderPosition, 1, 35, 18)
                Else
                    Gfx.DrawRectangle(New Pen(Color.Red, 2), OlderPosition, 1, 18, 18)
                    Gfx.DrawRectangle(New Pen(Color.Red, 2), NewerPosition, 1, 18, 18)
                End If
            End If

            'Draw end-of-history box
            If Edit Is NullEdit Then Gfx.FillRectangle(Brushes.Black, X, 2, 16, 16)
        End If
    End Sub

End Class
