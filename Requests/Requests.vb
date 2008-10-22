Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    MustInherit Class Request

        'Base class of all Web requests

        Private _Query As String, _StartTime As Date, _Mode As Modes, _State As States, _Done As RequestCallback
        Protected _Result As New RequestResult

        Public Delegate Sub RequestCallback(ByVal Result As RequestResult)

        Public Sub New()
            _StartTime = Date.Now
            PendingRequests.Add(Me)
            AllRequests.Add(Me)
            Callback(AddressOf UpdateForms)
        End Sub

        Public ReadOnly Property StartTime() As Date
            Get
                Return _StartTime
            End Get
        End Property

        Public Property Mode() As Modes
            Get
                Return _Mode
            End Get
            Private Set(ByVal value As Modes)
                _Mode = value
                Callback(AddressOf UpdateForms)
            End Set
        End Property

        Public Property Query() As String
            Get
                Return _Query
            End Get
            Private Set(ByVal value As String)
                _Query = UrlDecode(value)
                Callback(AddressOf UpdateForms)
            End Set
        End Property

        Public Property State() As States
            Get
                Return _State
            End Get
            Private Set(ByVal value As States)
                _State = value
                Callback(AddressOf UpdateForms)
            End Set
        End Property

        Protected Enum LoginResult As Integer
            : None : WrongPassword : NoUser : InvalidUsername : CaptchaNeeded : Failed : Cancelled : Success
        End Enum

        Public Enum Modes As Integer
            : None : [Get] : Post
        End Enum

        Public Enum States As Integer
            : InProgress : Complete : Failed : Cancelled : SpamFilter
        End Enum

        'Complete the request, optionally returning text
        Protected Sub Complete(Optional ByVal Message As String = Nothing, Optional ByVal Text As String = Nothing)
            State = States.Complete
            If Message IsNot Nothing Then Log(Message)
            If Text IsNot Nothing Then _Result = New RequestResult(Text, Nothing)
            Callback(AddressOf EndRequest)
        End Sub

        'Cancel the request
        Public Sub Cancel()
            State = States.Cancelled
            Callback(AddressOf EndRequest)
        End Sub

        'Fail the request, optionally with specific error message
        Protected Sub Fail(Optional ByVal Details As String = Nothing, Optional ByVal Reason As String = Nothing)
            If Details Is Nothing Then Details = Msg("error-fail", Truncate(Query, 50))
            If Reason Is Nothing Then Reason = Msg("error-unknown")

            State = States.Failed
            Log(Details & ": " & Reason)
            _Result = New RequestResult(Nothing, Details & ": " & Reason)
            Callback(AddressOf EndRequest)
        End Sub

        'Sort out various things when the request has finished
        Private Sub EndRequest()
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If MainForm IsNot Nothing Then MainForm.Delog(Me)
        End Sub

        'Update details on any open RequestsForm
        Private Sub UpdateForms()
            For Each Item As Form In Application.OpenForms
                If TypeOf Item Is RequestsForm Then CType(Item, RequestsForm).UpdateList(Me)
            Next Item
        End Sub

        'Carry out the request on the same thread and return its result
        Public Function Invoke() As RequestResult
            Process()
            Done()
            Return _Result
        End Function

        'Start the request in a separate thread, optionally calling a specified subroutine when done
        Public Sub Start(Optional ByVal Done As RequestCallback = Nothing)
            _Done = Done
            Dim RequestThread As New Thread(AddressOf ProcessThread)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        'Helper function for above
        Private Sub ProcessThread()
            Process()
            Callback(AddressOf ThreadDone)
        End Sub

        'Helper function for above
        Private Sub ThreadDone()
            Done()
            If _Done IsNot Nothing Then _Done.Invoke(_Result)
        End Sub

        'The main body of the request
        Protected MustOverride Sub Process()

        'Any processing that must always happen on the main thread (usually because it needs UI access)
        Protected Overridable Sub Done()
        End Sub

        'Add a normal entry to the log
        Protected Sub Log(ByVal Message As String)
            Misc.Log(Message, Me)
        End Sub

        'Add or replace an in-progress entry for this request in the log
        Protected Sub LogProgress(ByVal Message As String)
            Callback(AddressOf LogProgressCallback, CObj(Message))
        End Sub

        'Helper function for above
        Private Sub LogProgressCallback(ByVal MessageObject As Object)
            If MainForm IsNot Nothing Then
                MainForm.Delog(Me)
                MainForm.Log(CStr(MessageObject), Me, True)
            End If
        End Sub

        'Remove any in-progress log entry for this request
        Protected Sub DelogProgress()
            Callback(AddressOf DelogProgressCallback)
        End Sub

        'Helper function for above
        Private Sub DelogProgressCallback()
            If MainForm IsNot Nothing Then MainForm.Delog(Me)
        End Sub

        'Undo an edit that had already saved when the request was cancelled
        Protected Sub UndoEdit(ByVal Page As Page)
            If Page.LastEdit IsNot Nothing AndAlso Page.LastEdit.User.IsMe _
                Then DoRevert(Page.LastEdit, Config.UndoSummary, Undoing:=True)
        End Sub

        Protected Sub UndoEdit(ByVal Page As String)
            UndoEdit(GetPage(Page))
        End Sub

        Protected Function DoLogin() As LoginResult
            Dim Client As New WebClient2, Result As String = "", Retries As Integer = 3

            Mode = Modes.Get
            Query = "title=Special:Userlogin"

            If Login.CaptchaId Is Nothing Then
                'Get login form, to check whether captcha is needed
                Do
                    Client.Headers.Add(HttpRequestHeader.UserAgent, Config.UserAgent)
                    Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                    Client.Proxy = Proxy

                    Retries -= 1

                    Try
                        Result = UTF8.GetString(Client.DownloadData(Config.SitePath & _
                            "w/index.php?title=Special:Userlogin"))
                        If State = States.Cancelled Then Return LoginResult.Cancelled

                    Catch ex As WebException
                        If State = States.Cancelled Then Return LoginResult.Cancelled Else Throw
                    End Try

                Loop Until IsWikiPage(Result) OrElse Retries = 0

                If Retries = 0 Then Return LoginResult.Failed

                If Client.ResponseHeaders(HttpResponseHeader.SetCookie) IsNot Nothing Then
                    Cookie = Client.ResponseHeaders(HttpResponseHeader.SetCookie)
                    Cookie = Cookie.Substring(0, Cookie.IndexOf(";") + 1)
                End If

                If Result.Contains("<div class='captcha'>") Then
                    Login.CaptchaId = Result.Substring(Result.IndexOf("id=""wpCaptchaId"" value=""") + 24)
                    Login.CaptchaId = Login.CaptchaId.Substring(0, Login.CaptchaId.IndexOf(""""))

                    Return LoginResult.CaptchaNeeded
                End If
            End If

            Mode = Modes.Post
            Query = "title=Special:Userlogin&action=submitlogin&type=login"

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, Config.UserAgent)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.Proxy

                Retries -= 1

                Dim PostString As String = "wpName=" & UrlEncode(Config.Username) & "&wpRemember=1" & _
                    "&wpPassword=" & UrlEncode(Config.Password) & "&wpCaptchaId=" & Login.CaptchaId & _
                    "&wpCaptchaWord=" & Login.CaptchaWord

                Try
                    Result = UTF8.GetString(Client.UploadData(Config.SitePath & _
                        "w/index.php?title=Special:Userlogin&action=submitlogin", UTF8.GetBytes(PostString)))

                    If State = States.Cancelled Then Return LoginResult.Cancelled

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

            'Set cookie
            Cookie = ""

            For Each Item As Cookie In Client.Cookies.GetCookies(New Uri(Config.SitePath))
                Cookie &= Item.ToString & "; "
            Next Item

            Cookie = Cookie.Trim(" "c)

            Return LoginResult.Success
        End Function

        Protected Function GetUrl(ByVal Url As String) As String

            If Url.Contains("?") Then Query = Url.Substring(Url.IndexOf("?") + 1) Else Query = Url
            Mode = Modes.Get

            Dim Client As New WebClient, Retries As Integer = Config.RequestAttempts, Result As String = Nothing

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, Config.UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.Proxy

                If Retries < Config.RequestAttempts Then Thread.Sleep(Config.RequestRetryInterval)
                Retries -= 1

                Try
                    Result = UTF8.GetString(Client.DownloadData(Url))
                Catch ex As WebException
                    If Retries > 0 Then Log(Msg("error-exception", Truncate(Query, 50)) & ": " & _
                        ex.Message & " " & Msg("retrying"))
                End Try

                If State = States.Cancelled Then Thread.CurrentThread.Abort()

            Loop Until Retries = 0 OrElse Result IsNot Nothing

            If Retries = 0 Then Return Nothing Else Return Result
        End Function

        Protected Function PostUrl(ByVal Url As String, ByVal PostString As String) As String

            If Url.Contains("?") Then Query = Url.Substring(Url.IndexOf("?") + 1) Else Query = Url
            Mode = Modes.Get

            Dim Client As New WebClient, Retries As Integer = Config.RequestAttempts, Result As String = Nothing

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, Config.UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Proxy = Login.Proxy

                If Retries < Config.RequestAttempts Then Thread.Sleep(Config.RequestRetryInterval)
                Retries -= 1

                Try
                    Result = UTF8.GetString(Client.UploadData(Url, UTF8.GetBytes(PostString)))
                Catch ex As WebException
                    If Retries > 0 Then Log(Msg("error-exception", Truncate(Query, 50)) & ": " & _
                        ex.Message & " " & Msg("retrying"))
                End Try

                If State = States.Cancelled Then Thread.CurrentThread.Abort()

            Loop Until Retries = 0 OrElse Result IsNot Nothing

            If Retries = 0 Then Return Nothing Else Return Result
        End Function

        'Retrieve data through the MediaWiki API
        Protected Function GetApi(ByVal QueryString As String) As ApiResult
            Return GetApi(Config.Project, QueryString)
        End Function

        Protected Function GetApi(ByVal Project As String, ByVal QueryString As String) As ApiResult

            If Project Is Nothing Then Project = Config.Project

            Query = QueryString
            Mode = Modes.Get

            Dim Client As New WebClient, Retries As Integer = Config.RequestAttempts, Result As String = ""

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, Config.UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.Proxy

                If Retries < Config.RequestAttempts Then Thread.Sleep(Config.RequestRetryInterval)
                Retries -= 1

                Try
                    Result = UTF8.GetString(Client.DownloadData _
                        (Config.SitePath(project) & "w/api.php?format=xml&" & querystring))
                Catch ex As WebException
                    If Retries > 0 Then Log(Msg("error-exception", Truncate(Query, 50)) & ": " & _
                        ex.Message & " " & Msg("retrying"))
                End Try

            Loop Until Result <> "" OrElse Retries = 0

            If Result.StartsWith("MediaWiki API is not enabled for this site") _
                Then Return New ApiResult(Nothing, "error-apidisabled", Msg("error-apidisabled"))

            If Result = "" Then Return New ApiResult(Nothing, "null", Msg("error-noreponse"))

            Return New ApiResult(Result, HtmlDecode(FindString(Result, "<error", "code=""", """")), _
                HtmlDecode(FindString(Result, "<error", "info=""", """")))
        End Function

        'Submit data through the MediaWiki API
        Protected Function PostApi(ByVal QueryString As String, ByVal PostString As String) As ApiResult
            Query = QueryString
            Mode = Modes.Post

            Dim Client As New WebClient, Retries As Integer = Config.RequestAttempts, Result As String = ""

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, Config.UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Proxy = Login.Proxy

                If Retries < Config.RequestAttempts Then Thread.Sleep(Config.RequestRetryInterval)
                Retries -= 1

                Try
                    Result = UTF8.GetString(Client.UploadData _
                        (Config.SitePath & "w/api.php?format=xml&" & QueryString, UTF8.GetBytes(PostString)))

                Catch ex As WebException
                    If Retries > 0 Then Log(Msg("error-exception", Truncate(Query, 50)) & ": " & _
                        ex.Message & " " & Msg("retrying"))
                End Try

            Loop Until Result <> "" OrElse Retries = 0

            If Result.StartsWith("MediaWiki API is not enabled for this site") _
                Then Return New ApiResult(Nothing, "error-apidisabled", Msg("error-apidisabled"))

            If Result = "" Then Return New ApiResult(Nothing, "null", Msg("error-noresponse"))

            Return New ApiResult(Result, HtmlDecode(FindString(Result, "<error", "code=""", """")), _
                HtmlDecode(FindString(Result, "<error", "info=""", """")))
        End Function

        'Get the text of a page
        Protected Function GetText(ByVal PageName As String, Optional ByVal Revision As String = Nothing, _
            Optional ByVal Section As String = Nothing) As ApiResult

            Dim QueryString As String = "action=query&prop=revisions&rvlimit=1&rvprop=content&titles=" & _
                UrlEncode(PageName)

            If Section IsNot Nothing Then QueryString &= "&rvsection=" & UrlEncode(Section)
            If Revision IsNot Nothing Then QueryString &= "&startid=" & UrlEncode(Revision)

            Return GetApi(QueryString)
        End Function

        Protected Function GetText(ByVal Page As Page, Optional ByVal Revision As String = Nothing, _
            Optional ByVal Section As String = Nothing) As ApiResult

            Return GetText(Page.Name, Revision, Section)
        End Function

        'Make an edit through the MediaWiki API
        Protected Function PostEdit(ByVal Page As Page, ByVal Text As String, ByVal Summary As String, _
            Optional ByVal Section As String = Nothing, Optional ByVal Minor As Boolean = False, _
            Optional ByVal Watch As Boolean = False, Optional ByVal SuppressAutoSummary As Boolean = False) _
            As ApiResult

            Dim Result As ApiResult

            'Get edit token
            If EditToken Is Nothing Then
                Result = GetApi("action=query&prop=info&intoken=edit&titles=" & UrlEncode(Page.Name))
                If Result.Error Then Return Result
                EditToken = GetParameter(Result.Text, "edittoken")
            End If

            'Edit page
            Dim QueryString As String = "title=" & UrlEncode(Page.Name) & "&text=" & UrlEncode(Text) _
                & "&summary=" & UrlEncode(Summary)

            If Config.Summary IsNot Nothing AndAlso Not SuppressAutoSummary _
                Then QueryString &= UrlEncode(" " & Config.Summary)

            QueryString &= "&token=" & UrlEncode(EditToken)

            If Section IsNot Nothing Then QueryString &= "&section=" & UrlEncode(Section)
            If Minor Then QueryString &= "&minor"
            If Watch Then QueryString &= "&watch"

            Result = PostApi("action=edit", QueryString)
            If Not Result.Error AndAlso Not Watchlist.Contains(Page.SubjectPage) Then Watchlist.Add(Page.SubjectPage)

            Return Result
        End Function

        Protected Function PostEdit(ByVal PageName As String, ByVal Text As String, ByVal Summary As String, _
            Optional ByVal Section As String = Nothing, Optional ByVal Minor As Boolean = False, _
            Optional ByVal Watch As Boolean = False, Optional ByVal SuppressAutoSummary As Boolean = False) _
            As ApiResult

            Return PostEdit(GetPage(PageName), Text, Summary, Section, Minor, Watch, SuppressAutoSummary)
        End Function

    End Class

    Class ApiResult

        'Represents the result of a MediaWiki API request

        Private _Text, _ErrorCode, _ErrorMessage As String

        Public Sub New(ByVal Text As String, ByVal ErrorCode As String, ByVal ErrorMessage As String)
            _Text = Text
            _ErrorCode = ErrorCode
            _ErrorMessage = ErrorMessage
        End Sub

        Public ReadOnly Property [Error]() As Boolean
            Get
                Return (_ErrorCode IsNot Nothing)
            End Get
        End Property

        Public ReadOnly Property Text() As String
            Get
                Return _Text
            End Get
        End Property

        Public ReadOnly Property ErrorCode() As String
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMessage() As String
            Get
                'Return a localized error message where possible
                If _ErrorCode Is Nothing Then Return _ErrorMessage
                If MsgExists("api-" & _ErrorCode) Then Return Msg("api-" & _ErrorCode)
                Return _ErrorMessage
            End Get
        End Property

    End Class

    Class RequestResult

        'Represents the result of a Request

        Private _Text, _ErrorMessage As String

        Public Sub New(Optional ByVal Text As String = Nothing, Optional ByVal ErrorMessage As String = Nothing)
            _Text = Text
            _ErrorMessage = ErrorMessage
        End Sub

        Public ReadOnly Property [Error]() As Boolean
            Get
                Return (_ErrorMessage IsNot Nothing)
            End Get
        End Property

        Public Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal value As String)
                _Text = value
            End Set
        End Property

        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _ErrorMessage
            End Get
        End Property

    End Class

End Namespace
