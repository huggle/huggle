Imports System.Net
Imports System.Text.Encoding
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class RevertRequest : Inherits Request

        'Revert to something

        Public Edit As Edit, Summary As String

        Public Sub Start()
            If Edit IsNot Nothing Then
                LogProgress("Reverting edit to '" & Edit.Page.Name & "'...")

                Dim RequestThread As New Thread(AddressOf Process)
                RequestThread.IsBackground = True
                RequestThread.Start()
            End If
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(Edit.Page, Edit.Id)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf NoPage)
                Exit Sub
            End If

            If Summary IsNot Nothing Then Data.Summary = Summary Else Data.Summary = GetReversionSummary(Edit)
            Data.Minor = Config.MinorReverts
            Data.Watch = Config.WatchReverts

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then
                Callback(AddressOf Failed)

            ElseIf Data.Result.Contains("<div id=""mw-spamprotectiontext"">") Then
                Callback(AddressOf SpamBlacklist)

            Else
                Callback(AddressOf Done)
            End If
        End Sub

        Private Sub Done(ByVal O As Object)
            If Config.WatchReverts Then
                If Not Watchlist.Contains(SubjectPage(Edit.Page)) Then Watchlist.Add(SubjectPage(Edit.Page))
                Main.UpdateWatchButton()
            End If

            If Cancelled Then UndoEdit(Edit.Page)
            Complete()
        End Sub

        Private Sub NoPage(ByVal O As Object)
            Log("Did not revert edit to '" & Edit.Page.Name & "' because the page does not exist")
            Fail()
        End Sub

        Private Sub SpamBlacklist(ByVal O As Object)
            If MsgBox("Edit to '" & Edit.Page.Name & "' was blocked by the spam blacklist." & vbCrLf & _
                "Edit page manually?", MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then

                Dim NewEditForm As New EditForm
                NewEditForm.Page = Edit.Page
                NewEditForm.Show()
            Else
                Log("Did not revert edit to '" & Edit.Page.Name & "' because the spam blacklist would not allow it")
            End If

            Fail()
        End Sub

        Private Sub Failed(ByVal O As Object)
            Log("Failed to revert '" & Edit.Page.Name & "'")
            Fail()
        End Sub

        Private Function GetReversionSummary(ByVal Edit As Edit) As String
            Dim RevertedUsers As New List(Of String)
            Dim ReversionLength As Integer = 0
            Dim RevertingEdit As Edit = Edit.Page.LastEdit
            Dim EndPart, Summary As String

            While RevertingEdit IsNot Edit AndAlso RevertingEdit IsNot Nothing
                ReversionLength += 1
                If Not RevertedUsers.Contains(RevertingEdit.User.Name) Then RevertedUsers.Add(RevertingEdit.User.Name)
                RevertingEdit = RevertingEdit.Prev
            End While

            If RevertedUsers.Contains(Edit.User.Name) _
                Then EndPart = " to an older version by " & Edit.User.Name _
                Else EndPart = " to last version by " & Edit.User.Name

            Dim MaxLength As Integer = 250 - EndPart.Length

            If ReversionLength = 0 Then Return Nothing
            If ReversionLength = 1 Then Summary = "Reverted 1 edit by " _
                Else Summary = "Reverted " & CStr(ReversionLength) & " edits by "

            If RevertedUsers.Count = 1 Then
                If (Summary & "[[Special:Contributions/" & RevertedUsers(0) & "|" & RevertedUsers(0) & "]]").Length <= MaxLength _
                    Then Summary &= "[[Special:Contributions/" & RevertedUsers(0) & "|" & RevertedUsers(0) & "]]" _
                    Else Summary &= RevertedUsers(0)

                If Summary.Length > MaxLength Then Return Summary
            Else
                For i As Integer = 0 To RevertedUsers.Count - 3
                    Summary &= "[[Special:Contributions/" & RevertedUsers(i) & "|" & RevertedUsers(i) & "]], "
                Next i

                Summary &= "[[Special:Contributions/" & RevertedUsers(RevertedUsers.Count - 2) & "|" _
                    & RevertedUsers(RevertedUsers.Count - 2) & "]] and " & "[[Special:Contributions/" _
                    & RevertedUsers(RevertedUsers.Count - 1) & "|" & RevertedUsers(RevertedUsers.Count - 1) & "]]"

                If Summary.Length > MaxLength Then
                    Summary = "Reverted " & CStr(ReversionLength) & " edits by "

                    Dim Done As Boolean
                    Dim i As Integer

                    For i = 0 To RevertedUsers.Count - 3
                        If (Summary & RevertedUsers(i) & ", " & RevertedUsers(i + 1) _
                            & " and " & RevertedUsers(i + 2)).Length <= MaxLength Then

                            Summary &= RevertedUsers(i) & ", "

                        ElseIf (Summary & RevertedUsers(i) & " and " _
                            & CStr(RevertedUsers.Count - i - 3) & " other users").Length <= MaxLength Then

                            Summary &= RevertedUsers(i) & " and " & CStr(RevertedUsers.Count - i - 3) & " other users"
                            Done = True
                            Exit For
                        Else
                            Done = True
                            Exit For
                        End If
                    Next i

                    If Not Done Then Summary &= RevertedUsers(i) & " and " & RevertedUsers(i + 1)
                End If

                If Summary.Length > MaxLength _
                    Then Summary = Summary.Substring(0, Summary.IndexOf(" by ") + 4) & CStr(RevertedUsers.Count) & " users"

                If Summary.Length > MaxLength Then Return Summary
            End If

            Summary &= EndPart

            Return Summary
        End Function

    End Class

    Class RollbackRequest : Inherits Request

        'Much easier than reverting

        Public Edit As Edit, Summary As String, Minor As Boolean = Config.MinorReverts

        Sub Start()
            LogProgress("Reverting edit to '" & Edit.Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Sub Process()
            'Set automatic summary
            Dim NextEdit As Edit = Edit

            If Summary Is Nothing Then
                While True
                    If NextEdit.User IsNot Edit.User Then
                        If Config.RollbackSummary IsNot Nothing Then Summary = Config.RollbackSummary.Replace _
                            ("$1", Edit.User.Name).Replace("$2", NextEdit.User.Name)
                        Exit While
                    End If

                    If NextEdit.Prev Is Nothing OrElse NextEdit.Prev Is NullEdit Then
                        If Config.RollbackSummaryUnknown IsNot Nothing Then _
                            Summary = Config.RollbackSummaryUnknown.Replace("$1", Edit.User.Name)
                        Exit While
                    End If

                    NextEdit = NextEdit.Prev
                End While
            End If

            Dim Client As New WebClient, Retries As Integer = 3, Result As String = ""

            Do
                Dim LoggingIn As Boolean = False

                Do
                    Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                    Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                    Client.Proxy = Login.Proxy

                    Retries -= 1

                    Dim GetString As String = SitePath & Edit.RollbackUrl.Substring(1)
                    If Summary IsNot Nothing AndAlso Summary <> "" Then GetString &= "&summary=" & UrlEncode(Summary)
                    If Config.Summary IsNot Nothing Then GetString &= UrlEncode(" " & Config.Summary)

                    Try
                        Result = UTF8.GetString(Client.DownloadData(GetString))
                    Catch ex As Exception
                        Callback(AddressOf Exc)
                        Exit Sub
                    End Try

                    If Result.Contains("<li id=""pt-login"">") Then
                        If Retries = 0 Then Exit Do
                        Callback(AddressOf LoginNeeded)

                        Dim NewLoginRequest As New LoginRequest

                        Select Case NewLoginRequest.DoLogin
                            Case LoginResult.Success
                                Callback(AddressOf LoginDone)

                            Case Else
                                Callback(AddressOf LoginFailed)
                                Exit Sub
                        End Select

                        LoggingIn = True
                    Else
                        LoggingIn = False
                    End If

                Loop Until Not LoggingIn

            Loop Until IsWikiPage(result) OrElse retries = 0

            If result.Contains("<h1 class=""firstHeading"">Action throttled</h1>") Then
                Callback(AddressOf Throttled)

            ElseIf result.Contains("<h1 class=""firstHeading"">Error: unable to proceed</h1") _
                AndAlso result.Contains("contributor is the only author of this page") Then
                Callback(AddressOf NoOtherEditors)

            ElseIf result.Contains("<h1 class=""firstHeading"">Error: unable to proceed</h1>") _
                AndAlso result.Contains("because someone else has edited the page") Then
                Callback(AddressOf Beaten)

            ElseIf result.Contains("<h1 class=""firstHeading"">Error: unable to proceed</h1>") _
                AndAlso result.Contains("There seems to be a problem with your login session") Then
                Callback(AddressOf WrongData)

            ElseIf result.Contains("<h1 class=""firstHeading"">Error: unable to proceed</h1>") Then
                Callback(AddressOf Unauthorized)

            ElseIf Retries = 0 Then
                Callback(AddressOf ServerUnavailable)

            Else
                Callback(AddressOf Done)
            End If
        End Sub

        Sub Done(ByVal O As Object)
            If Config.WatchReverts AndAlso Not Watchlist.Contains(SubjectPage(Edit.Page)) Then
                Dim NewWatchPageRequest As New WatchRequest
                NewWatchPageRequest.Page = Edit.Page
                NewWatchPageRequest.Start()
            End If

            If Cancelled Then UndoEdit(Edit.Page)
            Complete()
        End Sub

        Private Sub WrongData(ByVal O As Object)
            Log("Did not rollback '" & Edit.Page.Name & "' – token or other data incorrect")
            Fail()
        End Sub

        Private Sub Throttled(ByVal O As Object)
            Log("Did not rollback '" & Edit.Page.Name & "' – rate limit exceeded")
            Fail()
        End Sub

        Private Sub Unauthorized(ByVal O As Object)
            Log("Did not rollback '" & Edit.Page.Name & "' – returned ""unauthorized""")
            Fail()
        End Sub

        Private Sub Beaten(ByVal O As Object)
            Log("Did not rollback '" & Edit.Page.Name & "' - page was edited first")
            Fail()
        End Sub

        Private Sub NoOtherEditors(ByVal O As Object)
            Log("Did not rollback '" & Edit.Page.Name & "' - only one user has edited the page")
            Fail()
        End Sub

        Private Sub ServerUnavailable(ByVal O As Object)
            Log("Did not rollback '" & Edit.Page.Name & "' - server unavailable")
            Fail()
        End Sub

        Private Sub Exc(ByVal O As Object)
            DoRevert(Edit, False)
            Complete()
        End Sub

        Private Sub LoginNeeded(ByVal O As Object)
            LogProgress("User has been logged out; logging in...")
        End Sub

        Private Sub LoginDone(ByVal O As Object)
            Log("Logged in")
            MsgBox("Rollback failed as user was logged out; you may wish to retry.", MsgBoxStyle.Exclamation, "huggle")
            Fail()
        End Sub

        Private Sub LoginFailed(ByVal O As Object)
            Log("Failed to log in")
            MsgBox("Failed to log in. You may need to restart Huggle in order to edit.", MsgBoxStyle.Critical, "huggle")
            Fail()
        End Sub

    End Class

    Class FakeRollbackRequest : Inherits Request

        'Finds the last revision not by the same user as the most recent revision, using the API,
        'then reverts to that revision. Like rollback, but much slower. Used when rollback is not available.

        Public Page As Page, ExcludeUser As User, Summary As String

        Public Sub Start()
            LogProgress("Reverting edit to '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()

            Dim Result As String = GetApi("action=query&format=xml&prop=revisions" & _
                "&titles=" & UrlEncode(Page.Name) & "&rvlimit=1&rvprop=user|content&rvexcludeuser=" & _
                UrlEncode(ExcludeUser.Name))

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Not Result.Contains("<revisions>") Then
                Callback(AddressOf NoOtherUser)
                Exit Sub

            ElseIf Result.Contains("missing=""""") Then
                Callback(AddressOf PageMissing)
                Exit Sub

            ElseIf Not Result.Contains("<rev user=""") Then
                Callback(AddressOf NoOtherUser)
                Exit Sub
            End If

            Dim Data As New EditData

            Dim OldUser As String = Result.Substring(Result.IndexOf("<rev user=""") + 11)
            OldUser = HtmlDecode(OldUser.Substring(0, OldUser.IndexOf("""")))

            Data.Page = Page

            If Summary IsNot Nothing AndAlso Summary <> "" Then Data.Summary = Summary _
                Else Data.Summary = Config.ManualRevertSummary.Replace("$1", ExcludeUser.Name).Replace("$2", OldUser)

            Data.Text = Result.Substring(Result.IndexOf("<rev user=""") + 11)
            Data.Text = Data.Text.Substring(Data.Text.IndexOf(">") + 1)
            Data.Text = HtmlDecode(Data.Text.Substring(0, Data.Text.IndexOf("</rev>")))

            Result = GetApi("action=query&format=xml&prop=info&intoken=edit&titles=" & _
                UrlEncode(Page.Name))

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Result.Contains("missing=""""") Then
                Callback(AddressOf PageMissing)
                Exit Sub

            ElseIf Not ((Result.Contains("touched=""") AndAlso Result.Contains("edittoken="""))) Then
                Callback(AddressOf Failed)
                Exit Sub

            End If

            Data.StartTime = CStr(Date.UtcNow.Year) & CStr(Date.UtcNow.Month).PadLeft(2, "0"c) & _
                CStr(Date.UtcNow.Day).PadLeft(2, "0"c) & CStr(Date.UtcNow.Hour).PadLeft(2, "0"c) & _
                CStr(Date.UtcNow.Minute).PadLeft(2, "0"c) & CStr(Date.UtcNow.Second).PadLeft(2, "0"c)

            Data.EditTime = Result.Substring(Result.IndexOf("touched=""") + 9)
            Data.EditTime = Data.EditTime.Substring(0, Data.EditTime.IndexOf(""""))
            Data.EditTime = Data.EditTime.Replace(":", "").Replace("Z", "").Replace("-", "").Replace("T", "")
            Data.EditTime = HtmlDecode(Data.EditTime)

            Data.Token = Result.Substring(Result.IndexOf("edittoken=""") + 11)
            Data.Token = HtmlDecode(Data.Token.Substring(0, Data.Token.IndexOf("""")))

            Data.Minor = Config.MinorReverts
            Data.Watch = Config.WatchReverts

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Cancelled Then UndoEdit(Page)
            Complete()
        End Sub

        Private Sub NoOtherUser(ByVal O As Object)
            Log("Did not revert edits to '" & Page.Name & "', because only one user has edited the page")
            Fail()
        End Sub

        Private Sub PageMissing(ByVal O As Object)
            Log("Did not revert edits to '" & Page.Name & "', because the page does not exist")
            Fail()
        End Sub

        Private Sub Failed(ByVal O As Object)
            Log("Failed to revert edits to '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

End Namespace
