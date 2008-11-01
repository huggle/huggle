Imports System.Net

Class CaptchaForm

    Public CaptchaId As String

    Private Sub CaptchaForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("login-captchatitle")
        OK.Text = Msg("accept")

        Dim Client As New WebClient, TempFileName As String = System.IO.Path.GetTempFileName

        Client.Proxy = Login.Proxy

        'Get the captcha
        Client.DownloadFile(SitePath() & "index.php?title=Special:Captcha/image&wpCaptchaId=" & CaptchaId, _
            TempFileName)
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