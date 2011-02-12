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

Imports System.Drawing.Drawing2D

Class Throbber

    Public Active As Boolean

    Private WithEvents Timer As New Timer
    Private Gfx As BufferedGraphics, Value As Integer, CanRender As Boolean
    Private Brush1 As Brush = New Pen(Color.SteelBlue).Brush, Brush2 As Brush = New Pen(Color.LightSteelBlue).Brush

    Private Sub Throbber_HandleCreated() Handles Me.HandleCreated
        CanRender = True
    End Sub

    Private Sub Throbber_HandleDestroyed() Handles Me.HandleDestroyed
        CanRender = False
    End Sub

    Private Sub Throbber_Load() Handles Me.Load
        Gfx = BufferedGraphicsManager.Current.Allocate(CreateGraphics, DisplayRectangle)
        Gfx.Graphics.SmoothingMode = SmoothingMode.HighQuality

        Timer.Interval = 20
        Timer.Start()
    End Sub

    Private Sub Timer_Tick() Handles Timer.Tick
        If Active Then
            Value = (Value + 1) Mod ((Height - 1) * 2)

            For i As Integer = -(Width \ (Height - 1)) To (Width \ (Height - 1))
                Gfx.Graphics.FillRectangle(If(i Mod 2 = 0, Brush1, Brush2), _
                    Value + (i * (Height - 1)), 1, Height - 1, Height - 1)
            Next i
        Else
            Gfx.Graphics.Clear(BackColor)
        End If

        Gfx.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, Width - 1, Height - 1)
        If CanRender Then Gfx.Render()
    End Sub

    Public Sub Start()
        Active = True
    End Sub

    Public Sub [Stop]()
        Active = False
    End Sub

End Class
