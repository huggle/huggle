Imports System.Net
Imports System.Text.Encoding
Imports System.Threading
Imports System.Web.HttpUtility

Module RevertRequests

    Class RevertRequest : Inherits Request

        'Revert to something

        Public ThisEdit As Edit, Summary As String

        Public Sub Start()
            If ThisEdit IsNot Nothing Then
                Log("Reverting edit to '" & ThisEdit.Page.Name & "'...", ThisEdit.Page, True)
                Dim RequestThread As New Thread(AddressOf Process)
                RequestThread.IsBackground = True
                RequestThread.Start()
            End If
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEdit(ThisEdit.Page, ThisEdit.Id)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf NoPage)
                Exit Sub
            End If

            If Summary IsNot Nothing Then Data.Summary = Summary Else Data.Summary = GetReversionSummary(ThisEdit)
            Data.Minor = Config.MinorReverts
            Data.Watch = Config.WatchReverts

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then
                Callback(AddressOf Failed)

            ElseIf Data.Result.Contains("<div id=""mw-spamprotectiontext"">") Then
                Callback(AddressOf SpamFilter)

            Else
                Callback(AddressOf Done)
            End If
        End Sub

        Private Sub Done(ByVal O As Object)
            If Config.WatchReverts Then
                If Not Watchlist.Contains(SubjectPage(ThisEdit.Page)) Then Watchlist.Add(SubjectPage(ThisEdit.Page))
                Main.UpdateWatchButton()
            End If

            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(ThisEdit.Page)
        End Sub

        Private Sub NoPage(ByVal O As Object)
            Log("Did not revert edit to '" & ThisEdit.Page.Name & "' because the page does not exist")
            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub SpamFilter(ByVal O As Object)
            If MsgBox("Edit to " & ThisEdit.Page.Name & " was blocked by spam filter." & vbCrLf & _
                "Edit page manually?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Diagnostics.Process.Start _
                    (SitePath & "w/index.php?title=" & UrlEncode(ThisEdit.Page.Name) _
                    & "&diff=cur&oldid=prev")

            Log("Did not revert edit to '" & ThisEdit.Page.Name & "' because the spam blacklist would not allow it")
            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Log("Failed to revert '" & ThisEdit.Page.Name & "'")
            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
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

            'For some reason, this guy doesn't like his name appearing in edit summaries when someone reverts past him.
            If RevertedUsers.Count > 1 AndAlso RevertedUsers.Contains("Philip Trueman") _
                Then RevertedUsers.Remove("Philip Trueman")

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

        Public ThisEdit As Edit
        Public Minor As Boolean = Config.MinorReverts
        Public Summary As String

        Sub Start()
            Log("Reverting edit to '" & ThisEdit.Page.Name & "'...", ThisEdit.Page, True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Sub Process()
            Dim Client As New WebClient, Retries As Integer = 3, Result As String = ""

            Dim SomeEdit As Edit = ThisEdit

            While True
                If SomeEdit.User IsNot ThisEdit.User Then
                    If Config.RollbackSummary IsNot Nothing Then Summary = Config.RollbackSummary.Replace _
                        ("$1", ThisEdit.User.Name).Replace("$2", SomeEdit.User.Name)
                    If Config.Summary IsNot Nothing Then Summary &= " " & Config.Summary
                    Exit While
                End If

                If SomeEdit.Prev Is Nothing OrElse SomeEdit.Prev Is NullEdit Then
                    If Config.RollbackSummaryUnknown IsNot Nothing Then _
                        Summary = Config.RollbackSummaryUnknown.Replace("$1", ThisEdit.User.Name)
                    If Config.Summary IsNot Nothing Then Summary &= " " & Config.Summary
                    Exit While
                End If

                SomeEdit = SomeEdit.Prev
            End While

            Do
                Dim LoggingIn As Boolean = False

                Do
                    Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                    Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)

                    Retries -= 1

                    Dim GetString As String = SitePath & ThisEdit.RollbackUrl.Substring(1)
                    If Summary IsNot Nothing AndAlso Summary <> "" Then GetString &= "&summary=" & UrlEncode(Summary)

                    Try
                        Result = UTF8.GetString(Client.DownloadData(GetString))
                    Catch ex As Exception
                        Callback(AddressOf Exc)
                        Exit Sub
                    End Try

                    If Result.Contains("<li id=""pt-login"">") Then
                        If Retries = 0 Then Exit Do
                        Callback(AddressOf LoginNeeded)

                        Select Case DoLogin()
                            Case "failed", "captcha-needed", "wrong-password"
                                Callback(AddressOf LoginFailed)
                                Exit Sub

                            Case Else
                                Callback(AddressOf LoginDone)
                                Exit Sub
                        End Select

                        LoggingIn = True
                    Else
                        LoggingIn = False
                    End If

                Loop Until Not LoggingIn

            Loop Until IsWikiPage(Result) OrElse Retries = 0

            If Result.Contains("<h1 class=""firstHeading"">Action throttled</h1>") Then
                Callback(AddressOf Throttled)

            ElseIf Result.Contains("<h1 class=""firstHeading"">Error: unable to proceed</h1") _
                AndAlso Result.Contains("contributor is the only author of this page") Then
                Callback(AddressOf NoOtherEditors)

            ElseIf Result.Contains("<h1 class=""firstHeading"">Error: unable to proceed</h1>") _
                AndAlso Result.Contains("because someone else has edited the page") Then
                Callback(AddressOf Beaten)

            ElseIf Result.Contains("<h1 class=""firstHeading"">Error: unable to proceed</h1>") _
                AndAlso Result.Contains("There seems to be a problem with your login session") Then
                Callback(AddressOf WrongData)

            ElseIf Result.Contains("<h1 class=""firstHeading"">Error: unable to proceed</h1>") Then
                Callback(AddressOf Unauthorized)

            ElseIf Retries = 0 Then
                Callback(AddressOf ServerUnavailable)

            Else
                Callback(AddressOf Done)
            End If
        End Sub

        Sub Done(ByVal O As Object)
            If Config.WatchReverts AndAlso Not Watchlist.Contains(SubjectPage(ThisEdit.Page)) Then
                Dim NewWatchPageRequest As New WatchRequest
                NewWatchPageRequest.ThisPage = ThisEdit.Page
                NewWatchPageRequest.Start()
            End If

            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(ThisEdit.Page)
        End Sub

        Private Sub WrongData(ByVal O As Object)
            Log("Did not rollback '" & ThisEdit.Page.Name & "' – token or other data incorrect")
            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Throttled(ByVal O As Object)
            Log("Did not rollback '" & ThisEdit.Page.Name & "' – rate limit exceeded")
            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Unauthorized(ByVal O As Object)
            Log("Did not rollback '" & ThisEdit.Page.Name & "' – returned ""unauthorized""")
            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Beaten(ByVal O As Object)
            Log("Did not rollback '" & ThisEdit.Page.Name & "' - page was edited first")
            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub NoOtherEditors(ByVal O As Object)
            Log("Did not rollback '" & ThisEdit.Page.Name & "' - only one user has edited the page")
            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub ServerUnavailable(ByVal O As Object)
            Log("Did not rollback '" & ThisEdit.Page.Name & "' - server unavailable")
            Delog(ThisEdit.Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Exc(ByVal O As Object)
            Delog(ThisEdit.Page)
            DoRevert(ThisEdit, False)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub LoginNeeded(ByVal O As Object)
            Log("User has been logged out; logging in...", MyUser, True)
        End Sub

        Private Sub LoginDone(ByVal O As Object)
            Delog(MyUser)
            Delog(ThisEdit.Page)
            Log("Logged in")
            MsgBox("Rollback failed as user was logged out; you may wish to retry.", MsgBoxStyle.Exclamation, "huggle")
        End Sub

        Private Sub LoginFailed(ByVal O As Object)
            Delog(MyUser)
            Delog(ThisEdit.Page)
            Log("Failed to log in")
            MsgBox("Failed to log in. You may need to restart Huggle in order to edit.", MsgBoxStyle.Critical, "huggle")
        End Sub

    End Class

    Class FakeRollbackRequest : Inherits Request

        'Finds the last revision not by the same user as the most recent revision, using the API,
        'then reverts to that revision. Like rollback, but much slower. Used when rollback is not available.

        Public ThisPage As Page, ExcludeUser As User
        Public Summary As String

        Public Sub Start()
            Log("Reverting edit to '" & ThisPage.Name & "'...", ThisPage, True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()

            Dim Result As String = GetText(SitePath & "w/api.php?action=query&format=xml&prop=revisions" & _
                "&titles=" & UrlEncode(ThisPage.Name) & "&rvlimit=1&rvprop=user|content&rvexcludeuser=" & _
                UrlEncode(ExcludeUser.Name), "<pages>")

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

            Data.Page = ThisPage

            If Summary IsNot Nothing AndAlso Summary <> "" Then Data.Summary = Summary _
                Else Data.Summary = Config.ManualRevertSummary.Replace("$1", ExcludeUser.Name).Replace("$2", OldUser)

            Data.Text = Result.Substring(Result.IndexOf("<rev user=""") + 11)
            Data.Text = Data.Text.Substring(Data.Text.IndexOf(">") + 1)
            Data.Text = HtmlDecode(Data.Text.Substring(0, Data.Text.IndexOf("</rev>")))

            Result = GetText(SitePath & "w/api.php?action=query&format=xml&prop=info&intoken=edit&titles=" & _
                UrlEncode(ThisPage.Name), "<pages>")

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
            Delog(ThisPage)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(ThisPage)
        End Sub

        Private Sub NoOtherUser(ByVal O As Object)
            Delog(ThisPage)
            Log("Did not revert edits to '" & ThisPage.Name & "', because only one user has edited the page")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub PageMissing(ByVal O As Object)
            Delog(ThisPage)
            Log("Did not revert edits to '" & ThisPage.Name & "', because the page does not exist")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(ThisPage)
            Log("Failed to revert edits to '" & ThisPage.Name & "'")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

End Module
