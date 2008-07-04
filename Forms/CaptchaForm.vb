Imports System.Net

Class CaptchaForm

    Public CaptchaId As String

    Private Sub CaptchaForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Dim Client As New WebClient, TempFileName As String = System.IO.Path.GetTempFileName

        Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
        Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
        Client.Proxy = Login.Proxy

        Client.DownloadFile(SitePath & "w/index.php?title=Special:Captcha/image&wpCaptchaId=" & CaptchaId, TempFileName)
        Captcha.Image = New Bitmap(TempFileName)

        Width += (Captcha.Image.Width - Captcha.Width)
        Height += (Captcha.Image.Height - Captcha.Height)
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        If Answer.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub CaptchaForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
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

    Private Sub Answer_TextChanged(ByVal s As Object, ByVal e As EventArgs) Handles Answer.TextChanged
        OK.Enabled = (Answer.Text <> "")
    End Sub

End Class