Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    MustInherit Class Request

        'Base class of all Web requests

        Public Query As String, Mode As RequestMode, State As RequestState, StartTime As Date

        Public Delegate Sub CallbackDelegate(ByVal Success As Boolean)

        Public Enum RequestState As Integer
            : InProgress : Complete : Failed : Cancelled
        End Enum

        Public Enum RequestMode As Integer
            : None : [Get] : Post
        End Enum

        Protected Enum LoginResult As Integer
            : None : WrongPassword : NoUser : InvalidUsername : CaptchaNeeded : Failed : Cancelled : Success
        End Enum

        Public Sub New()
            StartTime = Date.Now
            PendingRequests.Add(Me)
            AllRequests.Add(Me)
            UpdateForm()
        End Sub

        Protected Sub Complete()
            State = RequestState.Complete
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If MainForm IsNot Nothing Then MainForm.Delog(Me)
            UpdateForm()
        End Sub

        Public Sub Cancel()
            State = RequestState.Cancelled
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If MainForm IsNot Nothing Then MainForm.Delog(Me)
            UpdateForm()
        End Sub

        Protected Sub Fail()
            State = RequestState.Failed
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If MainForm IsNot Nothing Then MainForm.Delog(Me)
            UpdateForm()
        End Sub

        Private Sub UpdateForm()
            For Each Item As Form In Application.OpenForms
                If TypeOf Item Is RequestsForm Then CType(Item, RequestsForm).UpdateList(Me)
            Next Item
        End Sub

        Protected Sub LogProgress(ByVal Message As String)
            UpdateForm()

            If MainForm IsNot Nothing Then
                MainForm.Delog(Me)
                MainForm.Log(Message, Me, True)
            End If
        End Sub

        Protected Sub DelogProgress()
            If MainForm IsNot Nothing Then MainForm.Delog(Me)
        End Sub

        Protected Sub UndoEdit(ByVal Page As Page)
            If Page.LastEdit IsNot Nothing AndAlso Page.LastEdit.User Is MyUser _
                Then DoRevert(Page.LastEdit, False, Config.UndoSummary, True)
        End Sub

        Protected Sub UndoEdit(ByVal Page As String)
            UndoEdit(GetPage(Page))
        End Sub

        Protected Function DoLogin() As LoginResult
            Dim Client As New WebClient, Result As String = "", Retries As Integer = 3

            Mode = RequestMode.Get
            Query = "title=Special:Userlogin"

            If Login.CaptchaId Is Nothing Then
                'Get login form, to check whether captcha is needed
                Do
                    Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                    Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                    Client.Proxy = Proxy

                    Retries -= 1

                    Try
                        Result = UTF8.GetString(Client.DownloadData(SitePath & "w/index.php?title=Special:Userlogin"))
                        If State = RequestState.Cancelled Then Return LoginResult.Cancelled

                    Catch ex As WebException
                        If State = RequestState.Cancelled Then Return LoginResult.Cancelled Else Throw
                    End Try

                Loop Until IsWikiPage(Result) OrElse Retries = 0

                If Retries = 0 Then Return LoginResult.Failed

                If Client.ResponseHeaders(HttpResponseHeader.SetCookie) IsNot Nothing Then
                    SessionCookie = Client.ResponseHeaders(HttpResponseHeader.SetCookie)
                    SessionCookie = SessionCookie.Substring(0, SessionCookie.IndexOf(";") + 1)
                End If

                If Result.Contains("<div class='captcha'>") Then
                    Login.CaptchaId = Result.Substring(Result.IndexOf("id=""wpCaptchaId"" value=""") + 24)
                    Login.CaptchaId = Login.CaptchaId.Substring(0, Login.CaptchaId.IndexOf(""""))

                    Return LoginResult.CaptchaNeeded
                End If
            End If

            Mode = RequestMode.Post
            Query = "title=Special:Userlogin&action=submitlogin&type=login"

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Headers.Add(HttpRequestHeader.Cookie, SessionCookie)
                Client.Proxy = Login.Proxy

                Retries -= 1

                Dim PostString As String = "wpName=" & UrlEncode(Config.Username) & "&wpRemember=1" & _
                    "&wpPassword=" & UrlEncode(Login.Password) & "&wpCaptchaId=" & Login.CaptchaId & _
                    "&wpCaptchaWord=" & Login.CaptchaWord

                Try
                    'Pass username/password in post data
                    Result = UTF8.GetString(Client.UploadData(Config.SitePath & _
                        "w/index.php?title=Special:Userlogin&action=submitlogin&type=login", UTF8.GetBytes(PostString)))

                    If State = RequestState.Cancelled Then Return LoginResult.Cancelled

                Catch ex As WebException
                    Thread.Sleep(1000)
                End Try

            Loop Until IsWikiPage(Result) OrElse Retries = 0

            If Retries = 0 Then Return LoginResult.Failed

            If Result.Contains("<span id=""mw-noname"">") Then Return LoginResult.InvalidUsername
            If Result.Contains("<span id=""mw-nosuchuser"">") Then Return LoginResult.NoUser
            If Result.Contains("<span id=""mw-wrongpasswordempty"">") Then Return LoginResult.WrongPassword
            If Result.Contains("<span id=""mw-wrongpassword"">") Then Return LoginResult.WrongPassword
            If Result.Contains("<div id=""userloginForm"">") Then Return LoginResult.Failed

            Dim CookiePrefix As String, LoginCookie As String = Client.ResponseHeaders(HttpResponseHeader.SetCookie)

            If Config.Project = "localhost" Then CookiePrefix = "wikidb" _
                Else CookiePrefix = Config.Project.Substring(0, Config.Project.IndexOf(".")) & "wiki"

            Dim Userid As String = LoginCookie.Substring(LoginCookie.IndexOf(CookiePrefix & "UserID=") _
                + CookiePrefix.Length + 7)
            Userid = Userid.Substring(0, Userid.IndexOf(";"))

            'SUL-enabled accounts work differently
            If LoginCookie.Contains("centralauth_User=") Then

                Dim CaUserName As String = LoginCookie.Substring(LoginCookie.IndexOf("centralauth_User=") + 17)
                CaUserName = CaUserName.Substring(0, CaUserName.IndexOf(";"))

                Dim CaToken As String = LoginCookie.Substring(LoginCookie.IndexOf("centralauth_Token=") + 18)
                CaToken = CaToken.Substring(0, CaToken.IndexOf(";"))

                Cookie = CookiePrefix & "UserID=" & UrlEncode(Userid) & "; " & CookiePrefix & "UserName=" & _
                    UrlEncode(Config.Username) & "; " & "centralauth_User=" & UrlEncode(Config.Username) & "; " & _
                    "centralauth_Token=" & UrlEncode(CaToken) & ";"

                If SessionCookie IsNot Nothing Then Cookie &= " " & SessionCookie

                If LoginCookie.Contains("centralauth_Session=") Then
                    Dim CaSession As String = LoginCookie.Substring(LoginCookie.IndexOf("centralauth_Session=") + 20)
                    CaSession = CaSession.Substring(0, CaSession.IndexOf(";"))
                    Cookie &= " centralauth_Session=" & UrlEncode(CaSession) & ";"
                End If

            Else
                Dim Token As String = LoginCookie.Substring(LoginCookie.IndexOf(CookiePrefix & "Token=") _
                    + CookiePrefix.Length + 6)
                Token = Token.Substring(0, Token.IndexOf(";"))

                Cookie = CookiePrefix & "UserID=" & UrlEncode(Userid) & "; " & CookiePrefix & "UserName=" & _
                UrlEncode(Config.Username) & "; " & CookiePrefix & "Token=" & UrlEncode(Token) & "; " & SessionCookie
            End If

            MyUser = GetUser(Config.Username)

            Return LoginResult.Success
        End Function

        Protected Function GetPageText(ByVal Page As String) As String
            Dim Result As String = GetUrl(SitePath & _
                "w/api.php?action=query&format=xml&prop=revisions&rvprop=content&titles=" & UrlEncode(Page), _
                "page '" & Page & "'")

            If Result Is Nothing Then
                Return Nothing

            ElseIf Result.Contains("<rev>") Then
                Result = Result.Substring(Result.IndexOf("<rev>") + 5)
                Result = Result.Substring(0, Result.IndexOf("</rev>"))
                Result = HtmlDecode(Result)
                Return Result

            ElseIf Result.Contains("missing=""""") Then
                Return ""
            End If
            
            Return Nothing
        End Function

        Protected Function GetText(ByVal QueryString As String) As String
            Return GetUrl(SitePath & "w/index.php?" & QueryString, "'" & QueryString & "'")
        End Function

        Protected Function GetApi(ByVal QueryString As String) As String
            Return GetUrl(SitePath & "w/api.php?" & QueryString, "API query '" & QueryString & "'")
        End Function

        Protected Function GetUrl(ByVal Url As String, Optional ByVal QueryDescription As String = Nothing) As String

            If Url.Contains("?") Then Query = Url.Substring(Url.IndexOf("?") + 1) Else Query = Url
            Mode = RequestMode.Get

            Dim Client As New WebClient, Retries As Integer = 3, Result As String = Nothing

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.Proxy

                If Retries < 3 Then Thread.Sleep(1000)
                Retries -= 1

                Try
                    Result = UTF8.GetString(Client.DownloadData(Url))
                Catch ex As WebException
                    Callback(AddressOf GetUrlException, QueryDescription)
                End Try

                If State = RequestState.Cancelled Then Thread.CurrentThread.Abort()

            Loop Until Retries = 0 OrElse Result IsNot Nothing

            If Retries = 0 Then Return Nothing Else Return Result
        End Function

        Private Sub GetUrlException(ByVal QueryDescription As Object)
            Log("Error when requesting " & CStr(QueryDescription) & ", retrying in 1 second.")
        End Sub

        Protected Function GetEditData(ByVal Page As String, Optional ByVal Rev As String = Nothing, _
            Optional ByVal Section As Integer = -1) As EditData

            Return GetEditData(GetPage(Page), Rev, Section)
        End Function

        Protected Function GetEditData(ByVal Page As Page, Optional ByVal Rev As String = Nothing, _
            Optional ByVal Section As Integer = -1) As EditData

            Dim Retries As Integer = 3, Result As String = Nothing
            Dim TimeMatch, TokenMatch As Match, Data As New EditData

            Data.Page = Page
            If Section > -1 Then Data.Section = CStr(Section)

            Dim QueryString As String = SitePath & "w/index.php?title=" & UrlEncode(Data.Page.Name) & "&action=edit"
            If Rev IsNot Nothing Then QueryString &= "&oldid=" & Rev
            If Data.Section IsNot Nothing Then QueryString &= "&section=" & Data.Section

            Mode = RequestMode.Get
            Query = QueryString.Substring(QueryString.IndexOf("?") + 1)

            Do
                Dim LoggingIn As Boolean

                Do
                    Dim Client As New WebClient
                    Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                    Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                    Client.Proxy = Login.Proxy

                    If Retries < 3 Then Thread.Sleep(1000)
                    Retries -= 1

                    Try
                        Result = UTF8.GetString(Client.DownloadData(QueryString))
                    Catch ex As WebException
                        Callback(AddressOf GetEditDataException, CObj(Data))
                    End Try

                    If Result.Contains("<li id=""pt-login"">") Then
                        If Retries = 0 Then Exit Do
                        Callback(AddressOf LoginNeeded)

                        Select Case DoLogin()
                            Case LoginResult.Success
                                Callback(AddressOf LoginDone)

                            Case Else
                                Callback(AddressOf LoginFailed)
                                Data.Error = True
                                Return Data
                        End Select

                        LoggingIn = True
                    Else
                        LoggingIn = False
                    End If
                Loop Until Not LoggingIn

                If Result.Contains("<div class=""permissions-errors"">") Then
                    Callback(AddressOf Blocked)
                    Data.Error = True
                    Return Data
                End If

                TimeMatch = Regex.Match(Result, "<input type='hidden' value=""(.*?)"" name=""wpEdittime"" />")
                TokenMatch = Regex.Match(Result, "<input type='hidden' value=""(.*?)"" name=""wpEditToken"" />")

            Loop Until (TimeMatch.Success AndAlso TokenMatch.Success) OrElse Retries = 0

            If Retries = 0 Then
                Data.Error = True
                Return Data
            End If

            If Result.Contains("class=""selected new""><a ") Then Data.Creating = True

            If Not Result.Contains("<textarea") Then
                Data.Error = True
                Return Data
            End If

            Result = Result.Substring(Result.IndexOf("<textarea"))
            Result = Result.Substring(Result.IndexOf(">") + 1)
            Result = HtmlDecode(Result.Substring(0, Result.IndexOf("</textarea>")))

            Data.StartTime = CStr(Date.UtcNow.Year) & CStr(Date.UtcNow.Month).PadLeft(2, "0"c) & _
                CStr(Date.UtcNow.Day).PadLeft(2, "0"c) & CStr(Date.UtcNow.Hour).PadLeft(2, "0"c) & _
                CStr(Date.UtcNow.Minute).PadLeft(2, "0"c) & CStr(Date.UtcNow.Second).PadLeft(2, "0"c)
            Data.EditTime = TimeMatch.Groups(1).Value
            Data.Text = Result
            Data.Token = TokenMatch.Groups(1).Value

            Return Data
        End Function

        Private Sub GetEditDataException(ByVal DataObject As Object)
            Log("Error when editing '" & CType(DataObject, EditData).Page.Name & "', retrying in 1 second.")
        End Sub

        Private Sub LoginNeeded(ByVal O As Object)
            LogProgress("User has been logged out, logging in...")
        End Sub

        Private Sub LoginDone(ByVal O As Object)
            Log("Logged in")
            Complete()
        End Sub

        Private Sub LoginFailed(ByVal O As Object)
            Log("Failed to log in")
            MsgBox("Failed to log in. You may need to restart Huggle in order to edit.", MsgBoxStyle.Critical, "huggle")
            Complete()
        End Sub

        Private Sub Blocked(ByVal O As Object)
            Log("User is blocked")
            MsgBox("Your user account has been blocked from editing.", MsgBoxStyle.Critical, "huggle")
            Complete()
        End Sub

        Private Sub LoggedOut(ByVal O As Object)
            Log("Failed to save page - user is not logged in.")
            MsgBox("Your user account has been logged out. You may need to restart Huggle in order to edit.", _
                MsgBoxStyle.Critical, "huggle")
            Complete()
        End Sub

        Protected Function PostEdit(ByVal Data As EditData) As EditData

            'Special pages don't work in post requests
            Data.Page.Name = Data.Page.Name.Replace("Special:Mypage", "User:" & Config.Username) _
                .Replace("Special:Mytalk", "User talk:" & Config.Username)

            Dim Client As New WebClient, Retries As Integer = 3, Result As String = ""

            Dim PostString As String = "wpTextbox1=" & UrlEncode(Data.Text) _
                & "&wpEditToken=" & UrlEncode(Data.Token) & "&wpStarttime=" & UrlEncode(Data.StartTime) _
                & "&wpEdittime=" & UrlEncode(Data.EditTime) & "&wpSummary=" & UrlEncode(Data.Summary)

            If Config.Summary IsNot Nothing Then PostString &= UrlEncode(" " & Config.Summary)
            If Data.Section IsNot Nothing Then PostString &= "&section=" & UrlEncode(Data.Section)
            If Data.Minor Then PostString &= "&wpMinoredit=0"
            If Data.Watch OrElse Watchlist.Contains(SubjectPage(Data.Page)) Then PostString &= "&wpWatchthis=0"

            If Data.CaptchaId IsNot Nothing Then PostString &= "&wpCaptchaId=" & UrlEncode(Data.CaptchaId)
            If Data.CaptchaWord IsNot Nothing Then PostString &= "&wpCaptchaWord=" & UrlEncode(Data.CaptchaWord)

            Query = "title=" & UrlEncode(Data.Page.Name) & "&action=submit"
            Mode = RequestMode.Post

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Proxy = Login.Proxy

                If Retries < 3 Then Thread.Sleep(1000)
                Retries -= 1

                Try
                    Result = UTF8.GetString(Client.UploadData(SitePath & "w/index.php?title=" & _
                        UrlEncode(Data.Page.Name) & "&action=submit", UTF8.GetBytes(PostString)))
                Catch ex As WebException
                    Callback(AddressOf PostEditException, CObj(Data))
                End Try

            Loop Until IsWikiPage(Result) OrElse Retries = 0

            Data.Result = Result

            If Retries = 0 Then
                Data.Error = True

            ElseIf Result.Contains("<div id=""mw-blocked-text"">") Then
                Callback(AddressOf Blocked)
                Data.Error = True

            ElseIf Result.Contains("<div class='previewnote'>") Then
                Callback(AddressOf LoggedOut)
                Data.Error = True
            End If

            Return Data
        End Function

        Private Sub PostEditException(ByVal DataObject As Object)
            Dim Data As EditData = CType(DataObject, EditData)
            Log("Error saving '" & Data.Page.Name & "', retrying in 1 second.")
        End Sub

        Protected Function PostData(ByVal QueryString As String, ByVal Data As String) As String

            Dim Url As String = SitePath & "w/index.php?" & QueryString

            Query = QueryString
            Mode = RequestMode.Post

            Dim Client As New WebClient, Retries As Integer = 3, Result As String = ""

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Proxy = Login.Proxy

                If Retries < 3 Then Thread.Sleep(1000)
                Retries -= 1

                Try
                    Result = UTF8.GetString(Client.UploadData(Url, UTF8.GetBytes(Data)))
                Catch ex As WebException
                    Callback(AddressOf PostDataException, CObj(QueryString))
                End Try

            Loop Until Result <> "" OrElse Retries = 0

            Return Result
        End Function

        Private Sub PostDataException(ByVal RequestedItem As Object)
            Log("Error posting '" & CStr(RequestedItem) & "', retrying in 1 second.")
        End Sub

    End Class

End Namespace
