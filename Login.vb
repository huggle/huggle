Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Module Login

    Public CaptchaId, CaptchaWord As String, Proxy As IWebProxy

    Public Sub ConfigureProxy(ByVal Enabled As Boolean, ByVal Address As String, ByVal Port As Integer, _
        ByVal Username As String, ByVal Password As String, ByVal Domain As String)

        Dim Wp As WebProxy

        If (Address = "") Then
            Port = 80

            Dim ProxyString As String = CStr(Microsoft.Win32.Registry.GetValue _
                ("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyServer", ""))

            Dim ProxyEnabled As Boolean = CBool(Microsoft.Win32.Registry.GetValue _
                ("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings", "ProxyEnable", False))

            If (Not ProxyEnabled Or ProxyString = "") Then
                Wp = New WebProxy
            Else
                Wp = New WebProxy("http://" & ProxyString & "/", True)
            End If
        ElseIf Enabled Then
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
            'Log in... can't use the API here because it locks you out after a wrong password
            UpdateStatus(Msg("login-progress-start"))

            Try
                Dim LoginResult As LoginResult = DoLogin()

                Select Case LoginResult
                    'Outcomes for what posibly could go wrong when logging in
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

            If GlobalConfigResult.Error Then
                Abort(GlobalConfigResult.ErrorMessage)
                Exit Sub
            End If

            If Not Config.EnabledForAll Then
                Abort(Msg("login-error-alldisabled"))
                Exit Sub
            End If

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            'Notify user of new version
            If Config.LatestVersion > Config.Version Then
                Dim UpdateForm As New UpdateForm

                If Config.MinVersion > Config.Version Then
                    Abort(Msg("login-error-version"))
                    UpdateForm.ShowDialog()
                    Exit Sub
                End If

                UpdateForm.ShowDialog()
            End If

            'Get project configuration
            UpdateStatus(Msg("login-progress-project"))

            Dim ProjectConfigResult As RequestResult = (New ProjectConfigRequest).Invoke

            If ProjectConfigResult.Error Then
                Abort(ProjectConfigResult.ErrorMessage)
                Exit Sub
            End If

            If Not Config.EnabledForAll Then
                Abort(Msg("login-error-projdisabled"))
                Exit Sub
            End If

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            'Connect to IRC, if required (on separate thread)
            If Config.IrcMode Then IrcConnect()

            'Get user configuration
            UpdateStatus(Msg("login-progress-user"))

            Dim UserConfigResult As RequestResult = (New UserConfigRequest).Invoke

            If UserConfigResult.Error Then
                Abort(Msg("login-error-user"))
                Exit Sub
            End If

            If Config.RequireConfig AndAlso Not Config.Enabled Then
                Config.ConfigChanged = True
                Abort(Msg("login-error-disabled"))
                Exit Sub
            End If

            If Config.TemplateMessages.Count = 0 Then Config.TemplateMessages = Config.TemplateMessagesGlobal

            If Config.WarnSummary2 Is Nothing Then Config.WarnSummary2 = Config.WarnSummary
            If Config.WarnSummary3 Is Nothing Then Config.WarnSummary3 = Config.WarnSummary
            If Config.WarnSummary4 Is Nothing Then Config.WarnSummary4 = Config.WarnSummary

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            'Get user information, groups and account creation date
            UpdateStatus(Msg("login-progress-rights"))

            Dim Result As ApiResult = DoApiRequest _
                ("action=query&meta=userinfo&uiprop=rights|editcount&list=logevents&letype=newusers&letitle=" & _
                 UrlEncode(User.Me.Userpage.Name))

            If Result.Error Then Abort(Msg("login-error-rights"), Result.ErrorMessage)

            If Result.Text.Contains("<userinfo ") AndAlso Result.Text.Contains("<rights>") Then

                If FindString(Result.Text, "<userinfo", "</userinfo>").Contains("anon=""""") Then
                    'If we get here, somehow the user is not logged in
                    Abort(Msg("login-error-rights"), Msg("login-error-unknown"))
                    Exit Sub
                End If

                Dim EditCount As Integer = CInt(GetParameter(Result.Text, "editcount"))

                If EditCount < Config.RequireEdits Then
                    Abort(Msg("login-error-count", CStr(Config.RequireEdits)))
                    Exit Sub
                End If

                Dim Rights As New List(Of String)(FindString(Result.Text, "<rights>", "</rights>").Replace("</r>", "") _
                    .Split(New String() {"<r>"}, StringSplitOptions.RemoveEmptyEntries))
                Dim Autoconfirmed, AdminAvailable As Boolean

                If Rights.Contains("rollback") Then RollbackAvailable = True
                If Rights.Contains("autoconfirmed") Then Autoconfirmed = True
                If Rights.Contains("block") Then AdminAvailable = True

                Administrator = AdminAvailable AndAlso Config.UseAdminFunctions

                If Config.RequireAdmin AndAlso Not AdminAvailable Then
                    Abort(Msg("login-error-admin"))
                    Exit Sub
                End If

                If Config.RequireAutoconfirmed AndAlso Not Autoconfirmed Then
                    Abort(Msg("login-error-autoconfirmed"))
                    Exit Sub
                End If

                If Config.RequireRollback AndAlso Not RollbackAvailable Then
                    Abort(Msg("login-error-rollback"))
                    Exit Sub
                End If

            Else
                Abort(Msg("login-error-rights"))
                Exit Sub
            End If

            If Config.RequireTime > 0 Then
                'We know the user exists, so if we get an empty result the user must have been 
                'created in 2005 or earlier, before the log existed
                If Result.Text.Contains("<logevents>") Then
                    Dim CreationDate As Date = Date.MinValue
                    Date.TryParse(GetParameter(Result.Text, "timestamp"), CreationDate)

                    If CreationDate = Date.MinValue OrElse CreationDate.AddDays(Config.RequireTime) > Date.UtcNow Then
                        Abort(Msg("login-error-age", CStr(Config.RequireTime)))
                        Exit Sub
                    End If
                End If
            End If

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            'Check user list. If approval required, deny access to users not on the list, otherwise add them
            If Config.UserListLocation IsNot Nothing AndAlso (Config.Approval OrElse Not Config.UsernameListed) Then
                UpdateStatus(Msg("login-progress-userlist"))

                Result = GetText(Config.UserListLocation)

                If Result.Error Then
                    Abort(Msg("login-error-userlist"), Result.ErrorMessage)
                    Exit Sub
                End If

                If Not Result.Text.Contains _
                    ("[[Special:Contributions/" & Config.Username & "|" & Config.Username & "]]") Then

                    If Config.Approval Then
                        Abort(Msg("login-error-approval"))
                        Exit Sub
                    End If

                    Dim Matches As MatchCollection = _
                        New Regex("\* \[\[Special:Contributions/([^\|]+)\|[^\|]+\]\]").Matches(Result.Text)
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

                    PostEdit(Config.UserListLocation, Text, Config.UserListUpdateSummary, Minor:=True)
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

                If WhitelistResult.Error Then
                    Abort(Msg("login-error-whitelist"), Result.ErrorMessage)
                    Exit Sub
                End If

                WhitelistLoaded = True
            End If

            'In case user is not already on the whitelist (usually will be)
            If Not Whitelist.Contains(User.Me.Name) Then WhitelistAutoChanges.Add(User.Me.Name)
            User.Me.Ignored = True

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            'Get watchlist
            UpdateStatus(Msg("login-progress-watchlist"))

            Dim WatchlistResult As RequestResult = (New WatchlistRequest).Invoke

            If WatchlistResult.Error Then
                Abort(Msg("login-error-watchlist"), WatchlistResult.ErrorMessage)
                Exit Sub
            End If

            For Each Item As String In Split(WatchlistResult.Text, CRLF)
                Dim Page As Page = GetPage(Item)
                If Not Watchlist.Contains(Page) Then Watchlist.Add(Page)
            Next Item

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