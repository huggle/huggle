'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for Edit info form
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

Class EditInfoPanel

    Private Buffer As BufferedGraphics, CurrentEdit As Edit

    Public Enum DisplayMode
        : PageName : UserName
    End Enum

    Private Sub EditInfo_Load() Handles Me.Load
        Buffer = BufferedGraphicsManager.Current.Allocate(CreateGraphics, DisplayRectangle)
    End Sub

    Public Sub SetEdit(ByVal Edit As Edit, ByVal Mode As DisplayMode)
        If Edit IsNot Nothing AndAlso Edit.Page IsNot Nothing AndAlso Edit.User IsNot Nothing Then
            If Edit IsNot CurrentEdit Then
                If Mode = DisplayMode.PageName Then PageUser.Text = Edit.User.Name Else PageUser.Text = Edit.Page.Name
                Change.Text = CStr(If(Edit.Change > 0, "+", "")) & CStr(Edit.Change)
                Change.Visible = (Not Edit.Change = 0)
                If Edit.Summary Is Nothing Then Summary.Text = "" Else Summary.Text = TrimSummary(Edit.Summary)
                Time.Text = WikiTimestamp(Edit.Time.ToLocalTime)
                CurrentEdit = Edit
            End If
        Else
            Change.Text = ""
            Summary.Text = ""
            Time.Text = ""
            PageUser.Text = ""
            CurrentEdit = Nothing
        End If
    End Sub

    Private Sub EditInfo_Paint() Handles Me.Paint
        Buffer.Graphics.Clear(Color.FromKnownColor(KnownColor.Control))
        Buffer.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, Width - 1, Height - 2)
        Buffer.Render()
    End Sub

End Class
