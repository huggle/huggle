Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Module Requests

    Class Request

        Protected Cancelled As Boolean

        Public Sub Cancel()
            Cancelled = True
        End Sub

        Protected Sub New()
            PendingRequests.Add(Me)
            If Main IsNot Nothing Then Main.CancelB.Enabled = True
        End Sub

    End Class

    Public Sub UndoEdit(ByVal Page As Page)
        If Page.LastEdit IsNot Nothing AndAlso Page.LastEdit.User Is MyUser _
            Then DoRevert(Page.LastEdit, False, Config.UndoSummary, True)
    End Sub

    Public Function GetText(ByVal Page As Page) As String
        Return GetText(SitePath & "w/index.php?title=" & UrlEncode(Page.Name) & "&action=raw")
    End Function

    Public Function GetText(ByVal Url As String, Optional ByVal TextNeeded As String = Nothing) As String

        Dim Client As New WebClient, Retries As Integer = 3, Result As String = Nothing

        Do
            Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
            Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)

            If Retries < 3 Then Thread.Sleep(1000)
            Retries -= 1

            Try
                Result = UTF8.GetString(Client.DownloadData(Url))
            Catch ex As Exception
                Callback(AddressOf GetTextException, CObj(Url))
            End Try

        Loop Until (TextNeeded Is Nothing OrElse (Result IsNot Nothing AndAlso Result.Contains(TextNeeded))) _
            OrElse Retries = 0

        If Retries = 0 Then Return Nothing Else Return Result
    End Function

    Private Sub GetTextException(ByVal UrlObject As Object)
        Dim Url As String = CStr(UrlObject)
        Log("Service unavailable when requesting " & Url & ", retrying in 3 seconds.")
    End Sub

    Public Function GetEdit(ByVal Page As Page, Optional ByVal Rev As String = Nothing, _
        Optional ByVal Section As String = Nothing) As EditData

        Dim Retries As Integer = 3, Result As String = Nothing
        Dim TimeMatch, TokenMatch As Match, Data As New EditData

        Data.Page = Page
        Data.Section = Section

        Do
            Dim LoggingIn As Boolean

            Do
                Dim Client As New WebClient
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)

                If Retries < 3 Then Thread.Sleep(1000)
                Retries -= 1

                Dim GetString As String = SitePath & "w/index.php?title=" & _
                    UrlEncode(Data.Page.Name.Replace(" ", "_")) & "&action=edit"
                If Rev IsNot Nothing Then GetString &= "&oldid=" & Rev
                If Section IsNot Nothing Then GetString &= "&section=" & Section

                Try
                    Result = UTF8.GetString(Client.DownloadData(GetString))
                Catch ex As Exception
                    Callback(AddressOf GetEditException, CObj(Data))
                End Try

                If Result.Contains("<li id=""pt-login"">") Then
                    If Retries = 0 Then Exit Do
                    Callback(AddressOf LoginNeeded)

                    Select Case DoLogin()
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

    Private Sub GetEditException(ByVal DataObject As Object)
        Dim Data As EditData = CType(DataObject, EditData)
        Log("Service unavailable when editing '" & Data.Page.Name & "', retrying in 3 seconds.")
    End Sub

    Private Sub LoginNeeded(ByVal O As Object)
        Log("User has been logged out, logging in...", MyUser, True)
    End Sub

    Private Sub LoginDone(ByVal O As Object)
        Delog(MyUser)
        Log("Logged in")
    End Sub

    Private Sub LoginFailed(ByVal O As Object)
        Delog(MyUser)
        Log("Failed to log in")
        MsgBox("Failed to log in. You may need to restart Huggle in order to edit.", MsgBoxStyle.Critical, "huggle")
    End Sub

    Private Sub Blocked(ByVal O As Object)
        Log("User is blocked")
        MsgBox("Your user account has been blocked from editing.", MsgBoxStyle.Critical, "huggle")
    End Sub

    Private Sub LoggedOut(ByVal O As Object)
        Log("Failed to save page - user is not logged in.")
        MsgBox("Your user account has been logged out. You may need to restart Huggle in order to edit.", _
            MsgBoxStyle.Critical, "huggle")
    End Sub

    Public Function PostEdit(ByVal Data As EditData) As EditData
        Dim Client As New WebClient, Retries As Integer = 3, Result As String = ""

        Do
            Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
            Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
            Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")

            If Retries < 3 Then Thread.Sleep(1000)
            Retries -= 1

            If Config.Summary IsNot Nothing Then Data.Summary &= " " & Config.Summary

            Dim PostString As String = "wpTextbox1=" & UrlEncode(Data.Text) _
                & "&wpSummary=" & UrlEncode(Data.Summary) & "&wpStarttime=" & UrlEncode(Data.StartTime) _
                & "&wpEdittime=" & UrlEncode(Data.EditTime) & "&wpEditToken=" & UrlEncode(Data.Token)

            If Data.Section IsNot Nothing Then PostString &= "&section=" & UrlEncode(Data.Section)
            If Data.Minor Then PostString &= "&wpMinoredit=0"
            If Data.Watch OrElse Watchlist.Contains(SubjectPage(Data.Page)) Then PostString &= "&wpWatchthis=0"

            If Data.CaptchaId IsNot Nothing Then PostString &= "&wpCaptchaId=" & UrlEncode(Data.CaptchaId)
            If Data.CaptchaWord IsNot Nothing Then PostString &= "&wpCaptchaWord=" & UrlEncode(Data.CaptchaWord)

            Try
                'Special:Mypage doesnt work in postbacks
                Result = UTF8.GetString(Client.UploadData(SitePath & _
                    "w/index.php?title=" & UrlEncode(Data.Page.Name.Replace("Special:Mypage", "User:" + Username).Replace(" ", "_")) & _
                    "&action=submit", "POST", UTF8.GetBytes(PostString)))
            Catch ex As Exception
                Callback(AddressOf PostEditException, CObj(Data))
            End Try
        Loop Until IsWikiPage(Result) OrElse Retries = 0

        Data.Result = Result

        If Retries = 0 Then
            Data.Error = True

        ElseIf Result.Contains("<div class=""permissions-errors"">") Then
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

    Public Function PostData(ByVal Url As String, ByVal Data As String) As String

        Dim Client As New WebClient, Retries As Integer = 3, Result As String = ""

        Do
            Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
            Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
            Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")

            If Retries < 3 Then Thread.Sleep(1000)
            Retries -= 1

            Try
                Result = UTF8.GetString(Client.UploadData(Url, UTF8.GetBytes(Data)))
            Catch ex As Exception
                Callback(AddressOf PostDataException, CObj(Url))
            End Try

        Loop Until Result <> "" OrElse Retries = 0

        Return Result
    End Function

    Private Sub PostDataException(ByVal UrlObject As Object)
        Log("Service unavailable when requesting '" & CStr(UrlObject) & "', retrying in 3 seconds.")
    End Sub

End Module
