Imports System.Threading

Class StartupForm

    Private Sub StartupForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button

        Dim NewRequest As New StartupMessageRequest
        NewRequest.Start(AddressOf RequestDone)
    End Sub

    Private Sub StartupForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape : Close()
            Case Keys.Enter : OK_Click()
        End Select
    End Sub

    Private Sub StartupForm_Shown() Handles Me.Shown
        Browser.DocumentText = MakeHtmlWikiPage(Config.StartupMessageLocation, "Getting messages...")
    End Sub

    Private Sub Browser_Navigating(ByVal s As Object, ByVal e As WebBrowserNavigatingEventArgs) _
    Handles Browser.Navigating

        Dim Url As String = e.Url.ToString
        If Url.StartsWith("about:/") Then Url = Config.SitePath & Url.Substring(7)
        If Url = "about:blank" Then Exit Sub Else e.Cancel = True

        OpenUrlInBrowser(Url)
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Config.StartupMessage = ShowStartupMessage.Checked
        MainForm = New Main
        MainForm.Show()
        MainForm.Initialize()
        Close()
    End Sub

    Private Sub RequestDone(ByVal Result As RequestResult)
        If Me IsNot Nothing AndAlso Me.Visible Then
            If Result.Error Then
                Browser.DocumentText = MakeHtmlWikiPage(Config.StartupMessageLocation, Result.ErrorMessage)
            Else
                Browser.DocumentText = MakeHtmlWikiPage(Config.StartupMessageLocation, Result.Text)
            End If
        End If
    End Sub

End Class