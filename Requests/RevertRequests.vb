Imports System.Net
Imports System.Text.Encoding
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class RevertRequest : Inherits Request

        'Revert to something

        Public Edit As Edit, Summary As String, LastUser As User

        Protected Overrides Sub Process()
            LogProgress(Msg("revert-progress", Edit.Page.Name))

            Dim Result As ApiResult = GetText(Edit.Page, Edit.Id)

            If Result.Error Then
                Fail(Msg("revert-fail", Edit.Page.Name), Result.ErrorMessage)
                Exit Sub

            ElseIf Result.Text.Contains("missing=""") Then
                Fail(Msg("revert-fail", Edit.Page.Name), Msg("revert-missing"))
                Exit Sub
            End If

            Dim Text As String = GetTextFromRev(Result.Text)

            If Summary IsNot Nothing _
                Then Summary = Summary.Replace("$1", Edit.Page.LastEdit.User.Name).Replace("$2", Edit.User.Name) _
                Else Summary = GetReversionSummary(Edit)

            If CustomReverts.ContainsKey(Edit.Page) Then CustomReverts.Remove(Edit.Page)

            Dim FullSummary As String = Summary
            If Config.Summary IsNot Nothing Then FullSummary &= " " & Config.Summary
            CustomReverts.Add(Edit.Page, FullSummary)

            If Edit.Page.LastEdit.User IsNot LastUser AndAlso Edit.Page.LastEdit.User.Ignored Then
                Fail(Msg("revert-fail", Edit.Page.Name), Msg("revert-conflict", Edit.Page.LastEdit.User.Name))
                Exit Sub
            End If

            Result = PostEdit(Edit.Page, Text, Summary, Minor:=Config.Minor("revert"), Watch:=Config.Watch("revert"))

            If Result.Error Then
                Fail(Msg("revert-fail", Edit.Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            If FindString(Result.Text, "<edit", ">").Contains("nochange=""""") Then
                Fail(Msg("revert-fail", Edit.Page.Name), Msg("revert-nochange"))
                Exit Sub
            End If

            Complete()
            If State = States.Cancelled Then UndoEdit(Edit.Page)
        End Sub

        Private Function GetReversionSummary(ByVal Edit As Edit) As String
            Dim RevertedUsers As New List(Of String)
            Dim ReversionLength As Integer = 0
            Dim RevertingEdit As Edit = Edit.Page.LastEdit
            Dim EndPart, Summary As String

            Dim WReverted As String = Config.MultipleRevertSummaryParts(0)
            Dim WEditBy As String = Config.MultipleRevertSummaryParts(1)
            Dim WEditsBy As String = Config.MultipleRevertSummaryParts(2)
            Dim WAnd As String = Config.MultipleRevertSummaryParts(3)
            Dim WOtherUsers As String = Config.MultipleRevertSummaryParts(4)
            Dim WToLastVersionBy As String = Config.MultipleRevertSummaryParts(5)
            Dim WToAnOlderVersionBy As String = Config.MultipleRevertSummaryParts(6)

            While RevertingEdit IsNot Edit AndAlso RevertingEdit IsNot Nothing
                ReversionLength += 1
                If Not RevertedUsers.Contains(RevertingEdit.User.Name) Then RevertedUsers.Add(RevertingEdit.User.Name)
                RevertingEdit = RevertingEdit.Prev
            End While

            If RevertedUsers.Contains(Edit.User.Name) _
                Then EndPart = " " & WToAnOlderVersionBy & " " & Edit.User.Name _
                Else EndPart = " " & WToLastVersionBy & " " & Edit.User.Name

            Dim MaxLength As Integer = 250 - EndPart.Length

            If ReversionLength = 0 Then Return Nothing
            If ReversionLength = 1 Then Summary = WReverted & " 1 " & WEditBy & " " _
                Else Summary = WReverted & " " & CStr(ReversionLength) & " " & WEditsBy & " "

            If RevertedUsers.Count = 1 Then
                If (Summary & "[[Special:Contributions/" & RevertedUsers(0) & "|" & RevertedUsers(0) & "]]").Length _
                    <= MaxLength Then Summary &= "[[Special:Contributions/" & RevertedUsers(0) & "|" & _
                    RevertedUsers(0) & "]]" Else Summary &= RevertedUsers(0)

                If Summary.Length > MaxLength Then Return Summary
            Else
                For i As Integer = 0 To RevertedUsers.Count - 3
                    Summary &= "[[Special:Contributions/" & RevertedUsers(i) & "|" & RevertedUsers(i) & "]], "
                Next i

                Summary &= "[[Special:Contributions/" & RevertedUsers(RevertedUsers.Count - 2) & "|" _
                    & RevertedUsers(RevertedUsers.Count - 2) & "]] " & WAnd & " " & "[[Special:Contributions/" _
                    & RevertedUsers(RevertedUsers.Count - 1) & "|" & RevertedUsers(RevertedUsers.Count - 1) & "]]"

                If Summary.Length > MaxLength Then
                    Summary = WReverted & " " & CStr(ReversionLength) & " " & WEditsBy & " "

                    Dim Done As Boolean
                    Dim i As Integer

                    For i = 0 To RevertedUsers.Count - 3
                        If (Summary & RevertedUsers(i) & ", " & RevertedUsers(i + 1) _
                            & " and " & RevertedUsers(i + 2)).Length <= MaxLength Then

                            Summary &= RevertedUsers(i) & ", "

                        ElseIf (Summary & RevertedUsers(i) & " " & WAnd & " " _
                            & CStr(RevertedUsers.Count - i - 3) & " " & WOtherUsers).Length <= MaxLength Then

                            Summary &= RevertedUsers(i) & " " & WAnd & " " & CStr(RevertedUsers.Count - i - 3) & _
                                " " & WOtherUsers
                            Done = True
                            Exit For
                        Else
                            Done = True
                            Exit For
                        End If
                    Next i

                    If Not Done Then Summary &= RevertedUsers(i) & " " & WAnd & " " & RevertedUsers(i + 1)
                End If

                If Summary.Length > MaxLength Then Return Summary
            End If

            Summary &= EndPart

            Return Summary
        End Function

    End Class

    Class RollbackRequest : Inherits Request

        'Reverts revisions using rollback function
        Public Edit As Edit, Summary As String

        Protected Overrides Sub Process()
            LogProgress(Msg("revert-progress", Edit.Page.Name))

            If Summary Is Nothing Then Summary = Config.RollbackSummary
            If Config.Summary IsNot Nothing Then Summary &= " " & Config.Summary

            Dim QueryString As String = "action=rollback&title=" & UrlEncode(Edit.Page.Name) & _
                "&from=" & UrlEncode(Edit.User.Name) & "&token=" & UrlEncode(Edit.RollbackToken) & _
                "&summary=" & UrlEncode(Summary)

            Dim Result As String = DoWebRequest(SitePath() & "index.php?" & QueryString)

            'If Result.Error Then Fail(Msg("revert-fail", Edit.Page.Name), Result.ErrorMessage) Else Complete()

            Complete()
        End Sub

        Protected Overrides Sub Done()
            If Not _Result.Error Then
                If Config.Watch("revert") AndAlso Not Watchlist.Contains(Edit.Page.SubjectPage) Then
                    Dim NewRequest As New WatchRequest
                    NewRequest.Page = Edit.Page
                    NewRequest.Start()
                End If

                If State = States.Cancelled Then UndoEdit(Edit.Page)
            End If
        End Sub

    End Class

    Class FakeRollbackRequest : Inherits Request

        'Finds the last revision not by the same user as the most recent revision, using the API,
        'then reverts to that revision. Like rollback, but much slower. Used when rollback is not available.

        Public Page As Page, ExcludeUser As User, LastUser As User, Summary As String

        Protected Overrides Sub Process()
            LogProgress(Msg("revert-progress", Page.Name))

            Dim Result As ApiResult = DoApiRequest("action=query&prop=revisions&rvlimit=1&rvprop=user|content" & _
                "&titles=" & UrlEncode(Page.Name) & "&rvexcludeuser=" & UrlEncode(ExcludeUser.Name))

            If Result.Error Then
                Fail(Msg("revert-fail", Page.Name), Result.ErrorMessage)
                Exit Sub

            ElseIf Not Result.Text.Contains("<revisions>") Then
                Fail(Msg("revert-fail", Page.Name), Msg("revert-nootheruser"))
                Exit Sub

            ElseIf Result.Text.Contains("missing=""""") Then
                Fail(Msg("revert-fail", Page.Name), Msg("error-pagemissing"))
                Exit Sub

            ElseIf Not Result.Text.Contains("<rev user=""") Then
                Fail(Msg("revert-fail", Page.Name), Msg("revert-nootheruser"))
                Exit Sub
            End If

            Dim OldUser As String = GetParameter(GetTextFromRev(Result.Text), "user")

            If Summary Is Nothing Then Summary = Config.RevertSummary
            Summary = Summary.Replace("$1", ExcludeUser.Name).Replace("$2", OldUser)

            Dim Text As String = GetTextFromRev(Result.Text)

            If Page.LastEdit.User IsNot LastUser AndAlso Page.LastEdit.User.Ignored Then
                Fail(Msg("revert-fail", Page.Name), Msg("revert-conflict", Page.LastEdit.User.Name))
                Exit Sub
            End If

            Result = PostEdit(Page, Text, Summary, Minor:=Config.Minor("revert"), Watch:=Config.Watch("revert"))

            If Result.Error Then Fail(Msg("revert-fail", Page.Name), Result.ErrorMessage) Else Complete()

            If State = States.Cancelled Then UndoEdit(Page)
        End Sub

    End Class

    Class UndoRequest : Inherits Request

        'Revert only a single revision

        Public Edit As Edit, Summary As String

        Protected Overrides Sub Process()
            LogProgress(Msg("revert-progress", Edit.Page.Name))

            '"Undo" function is not available through the API

            Dim Result As String = Nothing

            Try
                Result = DoUrlRequest(SitePath() & "index.php?title=" & _
                    UrlEncode(Edit.Page.Name) & "&action=edit&undo=" & Edit.Id)

            Catch ex As WebException
                Fail(Msg("revert-fail", Edit.Page.Name), ex.Message)
                Exit Sub
            End Try

            If Result Is Nothing Then
                Fail(Msg("revert-fail", Edit.Page.Name))
                Exit Sub

            ElseIf Result.Contains("<span id=""mw-undo-failure"">") Then
                Fail(Msg("revert-fail", Edit.Page.Name), Msg("revert-cannotundo"))

                If MessageBox.Show("Cannot revert edit to " & Edit.Page.Name & " by " & Edit.User.Name & " as other " & _
                    "edits which affect this edit have since been made." & CRLF & "Edit the page manually instead?", _
                    "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    Dim NewForm As New EditForm
                    NewForm.Page = Edit.Page
                    NewForm.Show()
                End If

                Exit Sub
            End If

            Dim Text As String = HtmlDecode(FindString(Result, "<textarea", ">", "</textarea"))

            If Summary Is Nothing Then Summary = Config.SingleRevertSummary
            Summary = Summary.Replace("$1", Edit.User.Name)

            Dim FullSummary As String = Summary
            If Config.Summary IsNot Nothing Then FullSummary &= " " & Config.Summary
            CustomReverts.Add(Edit.Page, FullSummary)

            If State = States.Cancelled Then Thread.CurrentThread.Abort()

            Dim Result2 As ApiResult = PostEdit(Edit.Page, Text, Summary, _
                Minor:=Config.Minor("revert"), Watch:=Config.Watch("revert"))

            If Result2.Error Then Fail(Msg("revert-fail", Edit.Page.Name), Result2.ErrorMessage) Else Complete()

            If State = States.Cancelled Then UndoEdit(Edit.Page)
        End Sub

    End Class

End Namespace
