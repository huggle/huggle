Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Module Login

    Public CaptchaId, CaptchaWord As String, Proxy As IWebProxy
    '----PROXY SCRIPT----
    'Origionaly made by schallot here http://schallot.googlepages.com/huggle
    'Developed by the huggle team for use in huggle
    '--------------------
    Public Sub ConfigureProxy(ByVal Enabled As Boolean, ByVal Address As String, ByVal Port As Integer, _
        ByVal Username As String, ByVal Password As String, ByVal Domain As String)

        Dim Wp As WebProxy

        If (Address = "") Then
            'If the addrsss is nothing then port = 80
            Port = 80
            'Also import the Internet explorer settings from the registry
            Dim ProxyString As String = CStr(Microsoft.Win32.Registry.GetValue _
                ("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyServer", ""))
            'And import them again from the registry
            Dim ProxyEnabled As Boolean = CBool(Microsoft.Win32.Registry.GetValue _
                ("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyEnable", False))
            'If the proxy isnt enabled or there is no string then...
            If (Not ProxyEnabled Or ProxyString = "") Then
                Wp = New WebProxy
            Else
                Wp = New WebProxy("http://" & ProxyString & "/", True)
            End If
        ElseIf Enabled Then
            'If enabled then, If the port is out of range of the normal port then set the port to 80 (standard)
            If Port <= 0 Or Port >= 65536 Then Port = 80

            Wp = New WebProxy("http://" & Address & ":" & CStr(Port) & "/", True)
        Else
            Wp = New WebProxy
        End If

        Wp.Credentials = New NetworkCredential(Username, Password, Domain)
        Wp.UseDefaultCredentials = True
        Proxy = Wp
    End Sub

End Module

Namespace Requests

    Class LoginRequest : Inherits Request

        'Log in to wiki, retrieve configuration settings and other state

        Public LoginForm As LoginForm

        Protected Overrides Sub Process()
            LogBuffer.Clear()
            LoadLists()
            LoadQueues()

            'Log in... can't use the API here because it locks you out after a wrong password
            UpdateStatus(Msg("login-progress-start"))

            Try
                Dim LoginResult As LoginResult = DoLogin()

                Select Case LoginResult
                    'Cases for various login problems
                    '---------------------------------
                    'in order, the cases are:
                    '* captcha needed
                    '* unknown error
                    '* invalid username
                    '* no such user
                    '* wrong password
                    '----------------------------------
                    Case LoginResult.CaptchaNeeded : Callback(AddressOf CaptchaNeeded)

                    Case LoginResult.Failed : Abort(Msg("login-error-unknown"))

                    Case LoginResult.InvalidUsername : Abort(Msg("login-error-invalid"))

                    Case LoginResult.NoUser : Abort(Msg("login-error-nouser"))

                    Case LoginResult.WrongPassword
                        If CaptchaId Is Nothing Then Abort(Msg("login-error-password")) _
                            Else Abort(Msg("login-error-captcha"))
                        CaptchaId = Nothing
                End Select

                If LoginResult <> LoginResult.Success Then Exit Sub

            Catch ex As WebException
                'Trap errors resulting from failed login
                Abort(ex.Message)
                Exit Sub
            End Try

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            'Update language files
            UpdateStatus(Msg("login-progress-language"))

            Dim LanguageResult As RequestResult = (New UpdateLanguagesRequest).Invoke

            If LanguageResult.Error Then
                Abort(LanguageResult.ErrorMessage)
                Exit Sub
            End If

            'Get global configuration
            UpdateStatus(Msg("login-progress-global"))

            Dim GlobalConfigResult As RequestResult = (New GlobalConfigRequest).Invoke

            'If the global config has an error then show relevant error
            If GlobalConfigResult.Error Then
                Abort(GlobalConfigResult.ErrorMessage)
                Exit Sub
            End If

            'If the global config is set to "not enabled for all" then show relevant error
            If Not Config.EnabledForAll Then
                Abort(Msg("login-error-alldisabled"))
                Exit Sub
            End If

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            'Notify user of new version
            If Config.LatestVersion > Config.Version Then
                Dim UpdateForm As New UpdateForm

                'If the current version is smaller than the min version abort with error
                If Config.MinVersion > Config.Version Then
                    Abort(Msg("login-error-version"))
                    UpdateForm.ShowDialog()
                    Exit Sub
                End If

                UpdateForm.ShowDialog()
            End If

            'Get project and user configuration
            UpdateStatus(Msg("login-progress-config"))

            Dim ConfigResult As RequestResult = (New ConfigRequest).Invoke

            If ConfigResult.Error Then
                Abort(ConfigResult.ErrorMessage)
                Exit Sub
            End If

            'If the project config is set to not enabled then show relevant error
            If Not Config.EnabledForAll Then
                Abort(Msg("login-error-projdisabled"))
                Exit Sub
            End If

            'If a config is required and the user config is not set to "enable:true" error
            If Config.RequireConfig AndAlso Not Config.Enabled Then
                Config.ConfigChanged = True
                Abort(Msg("login-error-disabled"))
                Exit Sub
            End If

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            'Connect to IRC, if required (on separate thread)
            If Config.UseIrc Then IrcConnect()

            'userlist checks
            '-------------------------------------------
            'Check user list. If approval required, deny access to users not on the list, *otherwise*, add them
            If Config.UserListLocation IsNot Nothing AndAlso (Config.Approval OrElse Not Config.UsernameListed) Then
                UpdateStatus(Msg("login-progress-userlist"))

                'get userlist
                Dim UserlistResult As ApiResult = GetText(Config.UserListLocation)

                If UserlistResult.Error Then
                    Abort(Msg("login-error-userlist"), UserlistResult.ErrorMessage)
                    Exit Sub
                End If

                'if user is not on the list...
                If Not UserlistResult.Text.Contains _
                    ("[[Special:Contributions/" & Config.Username & "|" & Config.Username & "]]") Then
                    'abort them if config setting "approval" is set to true
                    If Config.Approval Then
                        Abort(Msg("login-error-approval"))
                        Exit Sub
                    End If
                    'else, add them
                    '
                    ' ---------------------
                    ' See [issue 149] -
                    ' this code may have a bug
                    ' concerning HTML entities
                    ' in the username
                    ' ---------------------
                    Dim Matches As MatchCollection = _
                        New Regex("\* \[\[Special:Contributions/([^\|]+)\|[^\|]+\]\]").Matches(UserlistResult.Text)
                    Dim ListedUsers As New List(Of String)

                    For Each Item As Match In Matches
                        If Not ListedUsers.Contains(Item.Groups(1).Value) Then ListedUsers.Add(Item.Groups(1).Value)
                    Next Item

                    ListedUsers.Add(Config.Username)
                    ListedUsers.Sort(AddressOf CompareUsernames)

                    Dim Text As String = "{{/Header}}" & LF

                    For Each Item As String In ListedUsers
                        Text &= "* [[Special:Contributions/" & Item & "|" & Item & "]]" & LF
                    Next Item

                    PostEdit(Config.UserListLocation, Text, _
                        Config.UserListUpdateSummary.Replace("$1", Config.Username), Minor:=True)
                End If

                If Config.UsernameListed = False Then
                    Config.UsernameListed = True
                    Config.ConfigChanged = True
                End If
            End If

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            If Config.WhitelistLocation IsNot Nothing Then
                'Get whitelist
                UpdateStatus(Msg("login-progress-whitelist"))

                Dim WhitelistResult As RequestResult = (New WhitelistRequest).Invoke

                'If something went wrong with the getting of the whitelist say so
                If WhitelistResult.Error Then
                    Abort(Msg("login-error-whitelist"), WhitelistResult.ErrorMessage)
                    Exit Sub
                End If

                WhitelistLoaded = True
            End If

            'In case user is not already on the whitelist (usually will be)
            If Not Whitelist.Contains(User.Me.Name) Then WhitelistAutoChanges.Add(User.Me.Name)
            'Ignore self (Add self to whitelist)
            User.Me.Ignored = True

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            Callback(AddressOf LoginForm.Done)
            Complete()
        End Sub

        Private Sub CaptchaNeeded()
            Dim NewCaptchaForm As New CaptchaForm
            NewCaptchaForm.CaptchaId = CaptchaId

            If NewCaptchaForm.ShowDialog = DialogResult.OK Then
                CaptchaWord = NewCaptchaForm.Answer.Text
                Start()
            Else
                CaptchaId = Nothing
                CaptchaWord = Nothing
                Abort(Msg("login-error-nocaptcha"))
            End If
        End Sub

        Private Sub UpdateStatus(ByVal Message As String)
            If Not LoginForm.LoggingIn Then Thread.CurrentThread.Abort()
            Callback(AddressOf LoginForm.UpdateStatus, CObj(Message))
        End Sub

        Private Sub Abort(ByVal Message As String, Optional ByVal Reason As String = Nothing)
            If Reason IsNot Nothing Then Message &= ": " & Reason
            Callback(AddressOf LoginForm.Abort, CObj(Message))
            Fail(Message, Reason)
        End Sub

    End Class

End Namespace