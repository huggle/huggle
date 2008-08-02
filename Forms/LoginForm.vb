Imports System.Net
Imports System.Threading

Class LoginForm

    Public LoggingIn As Boolean
    Private Request As LoginRequest

    Private Sub LoginForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button

        GetLocalConfig()
        Version.Text = "Version " & Config.Version.Major & "." & Config.Version.Minor & "." & Config.Version.Build

#If DEBUG Then
        If Not Config.Projects.Contains("localhost;localhost") Then Config.Projects.Add("localhost;localhost")
#End If

        For Each Item As String In Config.Projects
            If Item.Contains(";") Then Project.Items.Add(Item.Substring(0, Item.IndexOf(";")))
        Next Item

        If Project.Items.Count > 0 Then Project.SelectedIndex = 0

        ProxyEnabled.Checked = Config.ProxyEnabled
        ProxyPort.Text = Config.ProxyPort
        If ProxyPort.Text.Length = 0 Then ProxyPort.Text = "80"
        ProxyAddress.Text = Config.ProxyServer
        ProxyDomain.Text = Config.ProxyUserDomain
        ProxyUsername.Text = Config.ProxyUsername

        If RememberMe Then Username.Text = Config.Username
        If Username.Text = "" Then Username.Focus() Else Password.Focus()
    End Sub

    Private Sub LoginForm_Shown() Handles Me.Shown
        If Username.Text = "" Then Username.Focus() Else Password.Focus()
    End Sub

    Private Sub LoginForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then End
    End Sub

    Private Sub Username_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Username.KeyDown
        If e.KeyCode = Keys.Enter Then Password.Focus()
    End Sub

    Private Sub Password_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Password.KeyDown
        If e.KeyCode = Keys.Enter Then OK_Click()
    End Sub

    Private Sub Password_TextChanged() Handles Password.TextChanged
        OK.Enabled = (Username.Text <> "" AndAlso Password.Text <> "")
    End Sub

    Private Sub Username_TextChanged() Handles Username.TextChanged
        OK.Enabled = (Username.Text <> "" AndAlso Password.Text <> "")
    End Sub

    Private Sub Credit_LinkClicked() Handles Credit.LinkClicked
        OpenUrlInBrowser("http://en.wikipedia.org/wiki/User:Gurch")
    End Sub

    Private Sub OK_Click() Handles OK.Click
        LoggingIn = True

        For Each Item As String In Config.Projects
            If Item.StartsWith(Project.Text & ";") Then
                Config.Project = Item.Substring(Item.IndexOf(";") + 1)
                Exit For
            End If
        Next Item

        Config.SitePath = "http://" & Config.Project & "/"
        Config.IrcMode = (Config.Project <> "localhost")
        If Config.Project.Contains(".org") Then Config.IrcChannel = "#" & _
            Config.Project.Substring(0, Config.Project.IndexOf(".org"))

        Options.Enabled = False
        OK.Enabled = False
        ShowProxySettings.Enabled = False
        HideProxySettings.Enabled = False
        Progress.Enabled = True
        Cancel.Text = "Cancel"

        Login.Password = Password.Text
        Config.ProxyEnabled = ProxyEnabled.Checked
        Config.ProxyPort = ProxyPort.Text
        ProxyAddress.Text = ProxyAddress.Text.Replace("http://", "")
        Config.ProxyServer = ProxyAddress.Text
        Config.ProxyUserDomain = ProxyDomain.Text
        Config.ProxyUsername = ProxyUsername.Text
        Config.Username = Username.Text.Substring(0, 1).ToUpper & Username.Text.Substring(1)

        Try
            Login.ConfigureProxy(ProxyEnabled.Checked, ProxyAddress.Text, ProxyPort.Text, ProxyUsername.Text, _
                ProxyPassword.Text, ProxyDomain.Text)

        Catch ex As Exception
            Abort(ex.Message)
        End Try

        Request = New LoginRequest
        Request.LoginForm = Me
        Request.Start()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        If LoggingIn Then
            Irc.Disconnect()
            Request.Cancel()
            Abort("Cancelled.")
            OK.Focus()
        Else
            End
        End If
    End Sub

    Private Sub ShowProxySettings_Click() Handles ShowProxySettings.Click
        Me.Height += 165
        HideProxySettings.Visible = True
        ShowProxySettings.Visible = False
    End Sub

    Private Sub HideProxySettings_Click() Handles HideProxySettings.Click
        Me.Height -= 165
        HideProxySettings.Visible = False
        ShowProxySettings.Visible = True
    End Sub

    Sub Done(ByVal O As Object)
        MainForm = New Main
        MainForm.Show()
        MainForm.Initialize()
        Close()
    End Sub

    Sub UpdateStatus(ByVal MessageObject As Object)
        Status.Text = CStr(MessageObject)
        If Progress.Value <= Progress.Maximum - 1 Then Progress.Value += 1
    End Sub

    Sub Abort(ByVal MessageObject As Object)
        LoggingIn = False
        Status.Text = CStr(MessageObject)
        Options.Enabled = True
        OK.Enabled = True
        ShowProxySettings.Enabled = True
        HideProxySettings.Enabled = True
        Cancel.Text = "Exit"
        Progress.Enabled = False
        Progress.Value = 0
    End Sub
End Class
