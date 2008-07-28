Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    MustInherit Class Request

        'Base class of all Web requests

        Public Query As String, Mode As RequestMode, StartTime As Date
        Public Completed, Cancelled As Boolean, Success As Boolean = True

        Public Enum RequestMode As Integer
            : None : [Get] : Post
        End Enum

        Public Sub New()
            StartTime = Date.Now
            PendingRequests.Add(Me)
            AllRequests.Add(Me)
        End Sub

        Protected Sub Complete()
            Completed = True
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Main IsNot Nothing Then Main.Delog(Me)
        End Sub

        Public Sub Cancel()
            Cancelled = True
            Success = False
            Complete()
        End Sub

        Protected Sub Fail()
            Success = False
            Complete()
        End Sub

        Protected Sub LogProgress(ByVal Message As String)
            If Main IsNot Nothing Then
                Main.Delog(Me)
                Main.Log(Message, Me, True)
            End If
        End Sub

        Protected Sub DelogProgress()
            If Main IsNot Nothing Then
                Main.Delog(Me)
            End If
        End Sub

        Protected Sub UndoEdit(ByVal Page As Page)
            If Page.LastEdit IsNot Nothing AndAlso Page.LastEdit.User Is MyUser _
                Then DoRevert(Page.LastEdit, False, Config.UndoSummary, True)
        End Sub

        Protected Sub UndoEdit(ByVal Page As String)
            UndoEdit(GetPage(Page))
        End Sub

        Protected Function GetPageText(ByVal Page As String) As String
            Return GetUrl(SitePath & "w/index.php?title=" & UrlEncode(Page) & "&action=raw", "page '" & Page & "'")
        End Function

        Protected Function GetPageText(ByVal Page As Page) As String
            Return GetUrl(SitePath & "w/index.php?title=" & UrlEncode(Page.Name) & "&action=raw", _
                "page '" & Page.Name & "'")
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
                Catch ex As Exception
                    Callback(AddressOf GetUrlException, CObj(QueryDescription))
                End Try

                If Cancelled Then Thread.CurrentThread.Abort()

            Loop Until Retries = 0 OrElse Result IsNot Nothing

            If Retries = 0 Then Return Nothing Else Return Result
        End Function

        Private Sub GetUrlException(ByVal QueryDescription As Object)
            Log("Error when requesting " & CStr(QueryDescription) & ", retrying in 3 seconds.")
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
                    Catch ex As Exception
                        Callback(AddressOf GetEditDataException, CObj(Data))
                    End Try

                    If Result.Contains("<li id=""pt-login"">") Then
                        If Retries = 0 Then Exit Do
                        Callback(AddressOf LoginNeeded)

                        Dim NewLoginRequest As New LoginRequest

                        Select Case NewLoginRequest.DoLogin
                            Case "failed", "captcha-needed", "wrong-password"
                                Callback(AddressOf LoginFailed)
                                Data.Error = True
                                Return Data

                            Case Else
                                Callback(AddressOf LoginDone)
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

        Private Sub GetEditDataException(ByVal EditDataObject As Object)
            Dim Data As EditData = CType(EditDataObject, EditData)
            Log("Service unavailable when editing '" & Data.Page.Name & "', retrying in 3 seconds.")
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
                Catch ex As Exception
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
            Log("Service unavailable when saving '" & Data.Page.Name & "', retrying in 3 seconds.")
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
                Catch ex As Exception
                    Callback(AddressOf PostDataException, CObj(QueryString))
                End Try

            Loop Until Result <> "" OrElse Retries = 0

            Return Result
        End Function

        Private Sub PostDataException(ByVal RequestedItem As Object)
            Log("Service unavailable when posting '" & CStr(RequestedItem) & "', retrying in 3 seconds.")
        End Sub

    End Class

End Namespace
