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

Imports System.Drawing

Class WikiTextBox

    Inherits RichTextBox

    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hWnd As IntPtr) As Integer

    Private _Highlight As Boolean = True, SettingText As Boolean, Request As HighlightRequest, Loading As Boolean = True
    Private WithEvents KeystrokeTimer As Timer

    Public Sub SetHighlighting(ByVal Value As Boolean)
        _Highlight = Value

        If _Highlight Then
            DoHighlight()
        Else
            SettingText = True

            Dim Source As String = Text
            Source = Highlight.RtfEscape(Source)
            Source = Highlight.RtfHeader & Source
            Source &= Highlight.RtfFooter
            Rtf = Source

            SettingText = False
        End If
    End Sub

    Private Sub Load()
        Loading = False
        DetectUrls = False

        For Each Item As FontFamily In (New Text.InstalledFontCollection).Families
            If Item.Name = "Consolas" Then
                Font = New Font(Item, Font.Size)
                Highlight.FontName = Highlight.FontName.Replace("Courier New", "Consolas")
                Exit For
            End If
        Next Item

        SettingText = True
        Rtf = Highlight.RtfHeader & Highlight.RtfFooter
        SettingText = False

        KeystrokeTimer = New Timer
        KeystrokeTimer.Interval = 1000
    End Sub

    Private Sub DoHighlight()
        'This throws an exception if called while in design mode.
        'Which, since it's on a separate thread, brings down the whole IDE. Fun.
        If Not DesignMode Then
            Request = New HighlightRequest
            Request.Start(Text, AddressOf HighlightDone)
        End If
    End Sub

    Private Sub HighlightDone(ByVal Result As String)
        LockWindowUpdate(Handle)

        Dim Start As Integer = SelectionStart, Length As Integer = SelectionLength
        Dim StartOfLowestLine As Integer = GetFirstCharIndexFromLine(GetLineFromCharIndex _
            (Math.Max(0, GetCharIndexFromPosition(New Point(0, Height))) - 1))

        SettingText = True
        Rtf = Result
        SettingText = False

        SelectionStart = StartOfLowestLine
        SelectionLength = 1
        SelectionStart = Start
        SelectionLength = Length

        LockWindowUpdate(IntPtr.Zero)

        MyBase.Refresh()
    End Sub

    Public Sub SetText(ByVal NewText As String)
        SettingText = True
        Text = NewText
        SettingText = False
        If _Highlight Then DoHighlight()
    End Sub

    Private Sub WikiTextBox_TextChanged() Handles Me.TextChanged
        If Loading Then Load()
        If SettingText Then Exit Sub
        If KeystrokeTimer.Enabled Then KeystrokeTimer.Stop()
        If _Highlight Then KeystrokeTimer.Start()
    End Sub

    Private Sub KeystrokeTimer_Tick() Handles KeystrokeTimer.Tick
        KeystrokeTimer.Stop()
        If _Highlight Then DoHighlight()
    End Sub

End Class
