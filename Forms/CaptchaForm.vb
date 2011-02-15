'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for captcha form
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

Imports System.Net

Class CaptchaForm

    Public CaptchaId As String

    Private Sub CaptchaForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("login-captchatitle")
        OK.Text = Msg("accept")

        Dim Client As New WebClient, TempFileName As String = System.IO.Path.GetTempFileName

        Client.Proxy = Login.Proxy

        'Get the captcha
        Client.DownloadFile(SitePath() & "index.php?title=Special:Captcha/image&wpCaptchaId=" & _
            CaptchaId, TempFileName)
        Captcha.Image = New Bitmap(TempFileName)

        'Size the captcha in form correctly
        Width += (Captcha.Image.Width - Captcha.Width)
        Height += (Captcha.Image.Height - Captcha.Height)
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Answer.Text <> "" Then
            DialogResult = DialogResult.OK
            'If captcha is done and correct then close huggle
            Close()
        End If
    End Sub

    Private Sub CaptchaForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            'If captcha canceled then close
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub Answer_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Answer.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Answer.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Answer_TextChanged() Handles Answer.TextChanged
        'When captcha text is changed enable the ok button
        OK.Enabled = (Answer.Text <> "")
    End Sub

End Class