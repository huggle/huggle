'This is a source code or part of Huggle project

'Copyright (C) 2011 Huggle team

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.



Imports System.IO
Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class ApiRequest : Inherits Request

        'Make an arbitrary API query

        Public ApiQuery As String

        Protected Overrides Sub Process()
            Dim Result As ApiResult = DoApiRequest(ApiQuery)

            If Result.Error Then Fail(, Result.ErrorMessage) Else Complete(, Result.Text)
        End Sub

    End Class

    Class BlockLogRequest : Inherits Request

        'Retrieve block log for a user

        Public User As User

        Protected Overrides Sub Process()
            Try
                Dim Result As ApiResult = DoApiRequest("action=query&list=logevents&letype=block&lelimit=50&letitle=" &
                    UrlEncode(User.Userpage.Name))

                If Result.Error Then
                    Fail(Msg("blocklog-fail", User.Name), Result.ErrorMessage)
                    Exit Sub
                End If

                If User.Blocks IsNot Nothing Then User.Blocks.Clear()

                Dim LogText As String = FindString(Result.Text, "<logevents>", "</logevents>")

                If LogText IsNot Nothing Then
                    If User.Blocks Is Nothing Then User.Blocks = New List(Of Block)

                    For Each Item As String In Split(LogText, "<item ")
                        Dim NewBlock As New Block

                        NewBlock.User = User
                        NewBlock.Time = CDate(GetParameter(Item, "timestamp"))
                        NewBlock.Action = GetParameter(Item, "action")
                        NewBlock.Duration = GetParameter(Item, "duration")
                        NewBlock.Options = GetParameter(Item, "flags")
                        NewBlock.Admin = GetUser(GetParameter(Item, "user"))
                        NewBlock.Comment = GetParameter(Item, "comment")

                        User.Blocks.Add(NewBlock)
                    Next Item
                End If

                Complete()
            Catch except As Exception
                Debug.WriteLine("BlockLog")
            End Try
        End Sub

    End Class

    Class DiffRequest : Inherits Request

        Public Edit As Edit, Tab As BrowserTab
        Private Shared _PreloadCount As Integer, _RequestCount As Integer
        Private Shared MaxSimultaneousRequests As Integer = 10

        Private Result As String

        Public Shared Property PreloadCount() As Integer
            Get
                Return _PreloadCount
            End Get
            Set(ByVal value As Integer)
                _PreloadCount = value
            End Set
        End Property

        Protected Overrides Sub Process()
            _RequestCount += 1

            If _RequestCount >= MaxSimultaneousRequests Then
                Done()
                Exit Sub
            End If

            If Tab Is Nothing Then Tab = CurrentTab
            Edit.DiffCacheState = Edit.CacheState.Caching

            Dim Oldid As String = If(Edit.Oldid = "-1", "prev", Edit.Oldid)

            'Using &action=render here would make things far simpler; unfortunately, that was broken for
            'image diff pages in 2007 and it would appear that nobody cares.

            Dim QueryString As String = SitePath() & "index.php?title=" & UrlEncode(Edit.Page.Name) _
                        & "&diff=" & Edit.Id & "&oldid=" & Oldid & "&uselang=en"

            If Not Config.QuickSight OrElse Edit.Sighted Then Result &= "diffonly=true"

            Try
                Result = DoUrlRequest(QueryString)

                Dim Api2 As String

                If (Config.RightPending = True And Config.UsePending = True) Then
                    QueryString = "api.php?action=query&prop=info|flagged&titles=" & UrlEncode(Edit.Page.Name)
                    Api2 = DoUrlRequest(QueryString)
                End If

            Catch ex As WebException
                Fail(Msg("diff-fail", Edit.Page.Name), ex.Message)
                Exit Sub
            End Try

            If Result Is Nothing Then
                Edit.DiffCacheState = Edit.CacheState.Uncached
                Fail(Msg("diff-fail", Edit.Page.Name), Msg("error-noresponse"))
                Exit Sub

            ElseIf Result.Contains("<div id=""mw-missing-article"">") _
                OrElse Result.Contains("The database did not find the text of a page that it should have found") Then
                Callback(AddressOf Deleted)
                Fail()
                Exit Sub
            End If

            If Config.QuickSight AndAlso Not Edit.Sighted Then
                Dim ReviewForm As String = FindString(Result, "<fieldset class=""flaggedrevs_reviewform", "</fieldset>")

                If ReviewForm IsNot Nothing Then
                    Edit.SightPostData = ""

                    For Each Item As String In Split(ReviewForm, "<input")
                        If GetParameter(Item, "name") IsNot Nothing AndAlso GetParameter(Item, "value") IsNot Nothing _
                            Then Edit.SightPostData &= GetParameter(Item, "name") & "=" &
                            UrlEncode(GetParameter(Item, "value")) & "&"
                    Next Item
                End If
            End If

            If Result.Contains(">undo</a>)") Then
                Dim Result2 As String = Result.Substring(0, Result.IndexOf(">undo</a>)"))
                Result2 = Result2.Substring(0, Result2.LastIndexOf("(<a") - 1)
                Result = Result2 & Result.Substring(Result.IndexOf(">undo</a>") + 10)
            End If

            ' DVdm - 25/02/2016 - Strip the table: Bug: there are no single quotes here anymore. Format has changed
            ' =====================================================================================================

            'If Result.Contains("<table class='diff") Then
            '    If Result.Contains("<table class='diff diff") Then
            '        If Result.Contains("talign-left'>") Then
            '            Result = Result.Substring(Result.IndexOf("<table class='diff diff-contentalign-left'>"))
            '        ElseIf Result.Contains("talign-right'>") Then
            '            Result = Result.Substring(Result.IndexOf("<table class='diff diff-contentalign-right'>"))
            '        End If
            '    End If
            '    If Result.Contains("<table class='diff'") Then
            '        Result = Result.Substring(Result.IndexOf("<table class='diff'>"))
            '    End If
            '    Result = Result.Substring(0, Result.IndexOf("</table>") + 8)
            '    Result = "<div style=""font-size: 160%; font-family: Arial"">" & Edit.Page.Name & "</div>" & Result

            '    'Add the change size in the top-right corner
            '    If Edit.Change <> 0 Then
            '        Dim EditChange As String = CStr(Edit.Change)
            '        If Edit.Change > 0 Then EditChange = "+" & EditChange

            '        Result = "<div style=""float: right; font-size: 140%; font-family: Arial"">" &
            '                EditChange & "</div>" & Result
            '    End If

            '    Complete()
            'ElseIf Result.Contains("<div class=""firstrevisionheader""") Then
            '    'This is the first revision to the page... so no diff
            '    Callback(AddressOf ReachedEnd)

            'Else
            '    Edit.DiffCacheState = Edit.CacheState.Uncached
            '    Fail()
            'End If

            If Result.Contains("<table class=""diff") Then
                If Result.Contains("<table class=""diff diff") Then
                    If Result.Contains("talign-left""") Then
                        Result = Result.Substring(Result.IndexOf("<table class=""diff diff-contentalign-left"""))
                    ElseIf Result.Contains("talign-right""") Then
                        Result = Result.Substring(Result.IndexOf("<table class=""diff diff-contentalign-right"""))
                    End If
                End If
                If Result.Contains("<table class=""diff""") Then
                    Result = Result.Substring(Result.IndexOf("<table class=""diff"">"))
                End If
                Result = Result.Substring(0, Result.IndexOf("</table>") + 8)
                Result = "<div style=""font-size: 160%; font-family: Arial"">" & Edit.Page.Name & "</div>" & Result

                'Add the change size in the top-right corner
                If Edit.Change <> 0 Then
                    Dim EditChange As String = CStr(Edit.Change)
                    If Edit.Change > 0 Then EditChange = "+" & EditChange

                    Result = "<div style=""float: right; font-size: 140%; font-family: Arial"">" &
                            EditChange & "</div>" & Result
                End If

                Complete()
            ElseIf Result.Contains("<div class=""firstrevisionheader""") Then
                'This is the first revision to the page... so no diff
                Callback(AddressOf ReachedEnd)

            Else
                Edit.DiffCacheState = Edit.CacheState.Uncached
                Fail()
            End If
            ' =====================================================================================================

        End Sub

        Protected Overrides Sub Done()
            PreloadCount -= 1
            _RequestCount -= 1
            If Result IsNot Nothing Then ProcessDiff(Edit, Result, Tab)
        End Sub

        Private Sub ReachedEnd()
            PreloadCount -= 1

            'Mark this as the start of the history
            Edit.Prev = NullEdit
            Edit.Oldid = "-1"
            Edit.DiffCacheState = Edit.CacheState.Cached

            If LatestDiffRequest Is Me Then
                MainForm.HistoryPrevB.Enabled = False
                If Edit.Next IsNot Nothing Then Tab.Edit = Edit.Next
                DisplayEdit(CurrentEdit, False, Tab)
            End If

            Complete()
        End Sub

        Private Sub Deleted()
            PreloadCount -= 1
            If LatestDiffRequest Is Me Then CurrentTab.Browser.DocumentText =
                "<div style=""font-family: Arial"">" &
                "This revision has been deleted, never existed, or exists " &
                "but the server handling the request does not know it yet.</div>"
            Complete()
        End Sub

    End Class

    Class HistoryRequest : Inherits Request

        'Fetch page history

        Public Page As Page, BlockSize As Integer = Config.HistoryBlockSize, Full, GetContent As Boolean
        Private FullTotal As Integer, FullLimit As Integer = 5000

        Protected Overrides Sub Process()
            Dim Result As ApiResult, Offset As String = Page.HistoryOffset, BREAK As Integer = 0

            If Full Then LogProgress(Msg("history-progress", Page.Name, CStr(FullTotal)))

            Do
                If GetContent Then BlockSize = Math.Min(ApiLimit() \ 10, BlockSize)

                Dim QueryString As String = "action=query&prop=revisions&titles=" & UrlEncode(Page.Name) &
                        "&rvlimit=" & CStr(BlockSize) & "&rvprop=ids|timestamp|user|comment"

                If GetContent Then QueryString &= "|content"
                If Offset IsNot Nothing Then QueryString &= "&rvstartid=" & Offset

                Result = DoApiRequest(QueryString)

                If Result.Error Then
                    Fail(Msg("history-fail", Page.Name), Result.ErrorMessage)
                    Exit Sub
                End If

                If Full Then
                    FullTotal += BlockSize
                    _Result = New RequestResult(Result.Text)
                    LogProgress(Msg("history-progress", Page.Name, CStr(FullTotal)))
                End If
                BREAK = BREAK + 1
                Callback(AddressOf ProcessHistoryPart)
                Offset = GetParameter(Result.Text, "rvstartid")
            Loop Until (Not Full OrElse FullTotal >= FullLimit OrElse Offset Is Nothing) And BREAK < Misc.GlExcess
            If BREAK >= Misc.GlExcess Then Log("Debug interrupted HistoryRequest.Process")

            Complete(, Result.Text)
        End Sub

        Private Sub ProcessHistoryPart()
            ProcessHistory(_Result.Text, Page)
        End Sub

        Protected Overrides Sub Done()
            ProcessHistory(_Result.Text, Page)
            MainForm.Delog(Me)
        End Sub

    End Class

    Class ContribsRequest : Inherits Request

        'Fetch user contributions
        Private Result As ApiResult

        Public User As User, DisplayWhenDone As Boolean
        Public BlockSize As Integer = Config.ContribsBlockSize

        Protected Overrides Sub Process()
            ' DVdm - 11/02/2016 - deprecated parameter &ucstart= with nothing
            ' ===============================================================
            'Result = DoApiRequest("action=query&list=usercontribs&ucuser=" &
            '        UrlEncode(User.Name) & "&uclimit=" & CStr(BlockSize) & "&ucstart=" & User.ContribsOffset)
            Result = DoApiRequest("action=query&list=usercontribs&ucuser=" &
                    UrlEncode(User.Name) & "&uclimit=" & CStr(BlockSize))
            ' ===============================================================

            If Result.Error Then
                Fail("Failed to retrieve user contributions", Result.ErrorMessage)
            ElseIf Result.Text.Contains("<usercontribs />") Then
                Callback(AddressOf NoContribs)
            ElseIf Result.Text.Contains("<usercontribs") Then
                Complete()
            Else
                Fail("Failed to retrieve user contributions", Result.ErrorMessage)
            End If
        End Sub

        Protected Overrides Sub Done()
            ProcessContribs(Result.Text, User)

            If DisplayWhenDone AndAlso User.LastEdit IsNot Nothing Then
                DisplayEdit(User.LastEdit)

                If User.LastEdit.Page IsNot Nothing AndAlso User.LastEdit.Page.LastEdit Is Nothing Then
                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.Page = User.LastEdit.Page
                    NewHistoryRequest.Start()
                End If
            End If

            Complete()
        End Sub

        Private Sub NoContribs()
            User.LastEdit = NullEdit
            MainForm.UserB.ForeColor = Color.Red
            Complete()
        End Sub

    End Class

    Class BrowserRequest : Inherits Request

        Public Url As String, Tab As BrowserTab, HistoryItem As HistoryItem, NoFormatting As Boolean

        Protected Overrides Sub Process()
            If Tab Is Nothing Then Tab = CurrentTab
            Tab.LastBrowserRequest = Me
            Dim Result As String

            Try
                Result = DoUrlRequest(Url)
            Catch ex As WebException
                Fail(ex.Message)
                Exit Sub
            End Try

            If Result Is Nothing Then Fail() Else Complete(, Result)
        End Sub

        Protected Overrides Sub Done()
            If MainForm.Visible AndAlso Tab.LastBrowserRequest Is Me Then
                ' catch NullReferenceException, JUST in case
                Try
                    Dim PageName As String = ParseUrl(Url)("title")
                    MainForm.PageB.Text = PageName
                    '--------------------------------------------------------
                    ' extra checks since NullReferenceExceptions are happening
                    ' here en masse. It's ugly, but should prevent some problems.
                    If _Result.Text Is Nothing Then Fail()
                    Dim Page As Huggle.Page = GetPage(PageName)
                    If Page Is Nothing Then Fail()
                    '---------------------------------------------------------
                    Debug.WriteLine("__")
                    If Not NoFormatting Then _Result.Text = FormatPageHtml(Page, _Result.Text)
                    Tab.Browser.DocumentText = _Result.Text
                    Tab.CurrentUrl = Url
                    If HistoryItem IsNot Nothing Then HistoryItem.Text = _Result.Text
                Catch ex As NullReferenceException
                    Fail(ex.Message)
                    Exit Sub
                End Try
            End If
        End Sub

    End Class

    Class RcApiRequest : Inherits Request

        'Get recent changes through the API, if IRC mode not enabled
        'Bearable, but IRC is better

        Protected Overrides Sub Process()
            Dim Result As ApiResult = DoApiRequest("action=query&list=recentchanges" &
                "&rclimit=" & CStr(Config.RcBlockSize) & "&rcprop=user|comment|flags|timestamp|title|ids|sizes")

            If Result.Error Then Fail(, Result.ErrorMessage) Else Complete(, Result.Text)
        End Sub

        Protected Overrides Sub Done()
            ProcessRc(_Result.Text)
        End Sub

    End Class

    Class CountRequest : Inherits Request

        'Accepts a list of users, gets their edit counts
        'whitelists any with more than WhitelistEditCount edits

        Public Users As New List(Of User)

        Protected Overrides Sub Process()
            Dim QueryString As String = "action=query&list=users&usprop=editcount&ususers="

            For Each Item As User In Users
                QueryString &= UrlEncode(Item.Name) & "|"
            Next Item

            QueryString = QueryString.Substring(0, QueryString.Length - 1)

            Dim Result As ApiResult = DoApiRequest(QueryString)

            If Result.Error Then
                Fail(, Result.ErrorMessage)
                Exit Sub
            End If

            Dim EditCounts As New Dictionary(Of User, Integer)

            For Each Item As String In Split(Result.Text, "<user ")
                If Item.Contains("name=""") AndAlso Item.Contains("editcount=""") Then
                    Dim User As User = GetUser(GetParameter(Item, "name"))
                    Dim Count As Integer = CInt(GetParameter(Item, "editcount"))

                    User.EditCount = Count
                    If Count > Config.WhitelistEditCount Then
                        User.Ignored = True
                        WhitelistAutoChanges.Add(User.Name)
                    End If
                End If
            Next Item

            Complete()
        End Sub

    End Class

    Class WarningLogRequest : Inherits Request

        'Retrieve user's talk page and extract a log of warnings

        Public User As User

        Protected Overrides Sub Process()
            Dim Result As ApiResult = DoApiRequest("action=query&prop=revisions&rvprop=content&titles=" &
                UrlEncode(User.TalkPage.Name))

            If Result.Error Then
                Fail(Msg("warninglog-fail", User.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Dim Text As String = GetTextFromRev(Result.Text)

            If Text IsNot Nothing Then
                User.Warnings = ProcessUserTalk(Text, User)
                User.Warnings.Sort(AddressOf SortWarningsByDate)
            End If

            Complete(, Text)
        End Sub

    End Class

    Class PageRevRequest : Inherits Request

        Public OldPage As Page
        Public Rev As String
        Protected Overrides Sub Process()
            If Rev Is Nothing Then
                Fail(, "No revision id")
            Else
                Dim result As ApiResult = GetText(OldPage, Rev)
                If result.Error Then
                    Fail(, result.ErrorMessage)
                    Exit Sub
                End If

                OldPage.Text = GetTextFromRev(result.Text)
                Complete(, OldPage.Text)
            End If
        End Sub
    End Class

    Class PageTextRequest : Inherits Request

        'Get text of page

        Public Page As Page

        Protected Overrides Sub Process()
            Dim Result As ApiResult = GetText(Page)

            If Result.Error Then
                Fail(, Result.ErrorMessage)
                Exit Sub
            End If

            Page.Text = GetTextFromRev(Result.Text)
            Complete(, Page.Text)
        End Sub

    End Class

    Class PreviewRequest : Inherits Request

        'Get preview of unsaved changes

        Public Page As Page, Text As String

        Protected Overrides Sub Process()
            Dim Result As ApiResult =
                DoApiRequest("action=parse", "prop=text" & "&title=" & UrlEncode(Page.Name) & "&text=" & UrlEncode(Text))

            If Result.Error Then
                Fail(, Result.ErrorMessage)
                Exit Sub
            End If

            Dim Html As String = MakeHtmlWikiPage(Page.Name, HtmlDecode(FindString(Result.Text, "<text>", "</text>")))

            Complete(, Html)
        End Sub

    End Class

    Class ChangesRequest : Inherits Request

        'Get diff of unsaved changes

        Public Page As Page, Text As String

        Protected Overrides Sub Process()
            Dim Result As String = Nothing

            Try
                Result = DoUrlRequest(SitePath() & "index.php?title=" & UrlEncode(Page.Name) & "&action=submit",
                    "&wpDiff=0&wpStarttime=" & Timestamp(Date.UtcNow) & "&wpEdittime=&wpTextbox1=" & UrlEncode(Text))

            Catch ex As WebException
                Fail(ex.Message)
                Exit Sub
            End Try

            If Result Is Nothing OrElse Not IsWikiPage(Result) Then
                Fail()
            Else
                Result = Result.Substring(Result.IndexOf("<div id=""wikiDiff"">"))
                Result = Result.Substring(0, Result.IndexOf("<form"))
                Complete(, Result)
            End If
        End Sub

    End Class

    Class ProtectionLogRequest : Inherits Request

        'Retrieve protection log

        Public Page As Page, Target As ListView

        Protected Overrides Sub Process()
            If Page.ProtectionsCurrent Then
                Complete()
                Exit Sub
            End If

            Dim Result As ApiResult = DoApiRequest("action=query&list=logevents&letype=protect&lelimit=100" &
                "&letitle=" & UrlEncode(Page.Name))

            If Result.Error Then
                Fail(Msg("protectlog-fail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            If Page.Protections IsNot Nothing Then Page.Protections.Clear()
            If Page.Protections Is Nothing Then Page.Protections = New List(Of Protection)
            Page.ProtectionsCurrent = True

            If Result.Text.Contains("<logevents>") Then
                For Each Item As String In Split(FindString(Result.Text, "<logevents>", "</logevents>"), "<item ")
                    Dim NewProtection As New Protection

                    NewProtection.Time = CDate(GetParameter(Item, "timestamp"))
                    NewProtection.Action = GetParameter(Item, "action")
                    NewProtection.Admin = GetUser(GetParameter(Item, "user"))

                    'The API returns protection log entries in one of two different formats 
                    'depending on how old they are, so we have to interpret both

                    Dim Comment As String = GetParameter(Item, "comment")
                    Dim Param As String = FindString(Item, "<param>", "</param>")
                    Try
                        If Param Is Nothing Then
                            'Old format
                            NewProtection.EditLevel = FindString(Comment, "[edit=", ":")
                            NewProtection.MoveLevel = FindString(Comment, "move=", "]")
                            NewProtection.EditExpiry = CDate(FindString(Comment, "(expires ", " (UTC))"))
                            NewProtection.MoveExpiry = NewProtection.EditExpiry
                            NewProtection.Cascading = (Comment.Contains("[cascading]"))

                            If Comment.Contains(" [edit=") Then Comment = Comment.Substring(0, Comment.IndexOf(" [edit="))
                            If Comment.Contains(" [move=") Then Comment = Comment.Substring(0, Comment.IndexOf(" [move="))

                        Else
                            'New format
                            NewProtection.EditLevel = FindString(Param, "[edit=", "]")
                            NewProtection.MoveLevel = FindString(Param, "[move=", "]")
                            'NewProtection.EditExpiry = CDate(FindString(FindString(Param, "[edit=", "["), "(expires ", " (UTC))"))
                            NewProtection.MoveExpiry = CDate(FindString(FindString(Param, "[move="), "(expires ", " (UTC))"))
                            NewProtection.Cascading = (Param.Contains("[cascading]"))
                        End If

                        NewProtection.Summary = Comment
                    Catch ex As Exception
                        Debug.WriteLine("Error at Request.Process")
                    End Try
                    Page.Protections.Add(NewProtection)
                Next Item
            End If

            If Page.Protections.Count > 0 AndAlso (Page.Protections(0).EditExpiry = Date.MinValue _
                OrElse Page.Protections(0).EditExpiry > Date.UtcNow) Then

                Page.EditLevel = Page.Protections(0).EditLevel
                Page.MoveLevel = Page.Protections(0).MoveLevel
            End If

            Complete()
        End Sub

    End Class

    Class DeleteLogRequest : Inherits Request

        'Retrieve deletion log

        Public Page As Page, Target As ListView

        Protected Overrides Sub Process()
            Dim Result As ApiResult = DoApiRequest("action=query&list=logevents&letype=delete&lelimit=50" &
                "&letitle=" & UrlEncode(Page.Name))

            If Result.Error Then
                Fail(, Result.ErrorMessage)
                Exit Sub
            End If

            If Page.Deletes IsNot Nothing Then Page.Deletes.Clear()
            If Page.Deletes Is Nothing Then Page.Deletes = New List(Of Delete)
            Page.DeletesCurrent = True

            Dim Results As String = FindString(Result.Text, "<logevents>", "</logevents>")

            If Results IsNot Nothing Then
                For Each Item As String In Split(Result.Text, LF)
                    Dim NewDelete As New Delete

                    NewDelete.Page = Page
                    NewDelete.Time = CDate(GetParameter(Item, "timestamp"))
                    NewDelete.Action = GetParameter(Item, "action")
                    NewDelete.Admin = GetUser(GetParameter(Item, "user"))
                    NewDelete.Comment = GetParameter(Item, "comment")

                    Page.Deletes.Add(NewDelete)
                Next Item
            End If

            Complete()
        End Sub

    End Class

    Class PurgeRequest : Inherits Request

        'Purge a page

        Public Page As Page

        Protected Overrides Sub Process()
            LogProgress(Msg("purge-progress", Page.Name))

            Dim Result As ApiResult = DoApiRequest("action=purge&titles=" & UrlEncode(Page.Name))

            If Result.Error Then
                Fail(Msg("purge-fail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Complete(Msg("purge-done", Page.Name))
        End Sub

    End Class

    Class ThreeRevertRuleCheckRequest : Inherits Request

        'Check for three-revert rule violation by specified user

        Private VersionId As String, RevertIds As New List(Of String)
        Private Shadows _Result As ThreeRevertRuleCheckResult

        Public User As User

        Class ThreeRevertRuleCheckResult

            Inherits RequestResult

            Public BaseEdit As Edit
            Public Reverts As List(Of Edit)

        End Class

        Protected Overrides Sub Process()
            Dim Break As Integer = 0
            Dim QueryString As String = "action=query&prop=revisions&rvprop=ids|content&revids="

            Dim ThisEdit As Edit = User.LastEdit, i As Integer = ApiLimit() \ 10

            While (ThisEdit IsNot NullEdit AndAlso ThisEdit IsNot Nothing AndAlso i > 0) And Break < Misc.GlExcess
                Break = Break + 1
                QueryString &= ThisEdit.Id & "|"
                ThisEdit = ThisEdit.PrevByUser
                i -= 1
            End While

            Break = 0

            QueryString = QueryString.Trim("|"c)

            Dim Result As ApiResult = DoApiRequest(QueryString)

            If Result.Error Then
                Fail(, Result.ErrorMessage)
                Exit Sub
            End If

            'Search for revisions with the same content
            Dim Results As String = FindString(Result.Text, "<pages>", "</pages")

            For Each Page As String In Split(Results, "<page ")
                Dim PageResult As String = FindString(Page, "<revisions>", "</revisions>")
                Dim Revisions As New List(Of String)(PageResult.Split(New String() {"<rev "},
                    StringSplitOptions.RemoveEmptyEntries))
                Dim RevisionMatches As New Dictionary(Of String, List(Of String))

                For Each Revision As String In Revisions
                    Dim Revid As String = GetParameter(Revision, "revid")

                    Revision = HtmlDecode(FindString(Revision, ">", "</rev>"))

                    If Edit.All.ContainsKey(Revid) Then Edit.All(Revid).Text = Revision

                    If RevisionMatches.ContainsKey(Revision) Then RevisionMatches(Revision).Add(Revid) _
                        Else RevisionMatches.Add(Revision, New List(Of String)(New String() {Revid}))
                Next Revision

                For Each Match As List(Of String) In RevisionMatches.Values
                    If Match.Count = 4 Then
                        'We have found four identical revisions to the same page by the same user within 24 hours
                        'Now we need to ensure the earliest is a reversion to a previous revision,
                        'otherwise only three reversions have been made. This means looking further back in the 
                        'page history for an identical revision, possibly by another user
                        RevertIds = Match

                        Dim NewRequest As New HistoryRequest
                        NewRequest.Page = Edit.All(RevertIds(0)).Page
                        NewRequest.GetContent = True
                        MyBase._Result = NewRequest.Invoke

                        If MyBase._Result.Error Then
                            Fail(, Result.ErrorMessage)
                            Exit Sub
                        End If

                        ThisEdit = Edit.All(RevertIds(0)).Page.LastEdit
                        Dim TextToFind As String = Edit.All(RevertIds(0)).Text

                        While ThisEdit IsNot NullEdit AndAlso ThisEdit IsNot Nothing And Break < Misc.GlExcess
                            Break = Break + 1
                            If Not RevertIds.Contains(ThisEdit.Id) AndAlso ThisEdit.Text = TextToFind _
                                AndAlso ThisEdit.Time < Edit.All(RevertIds(0)).Time Then

                                'Found an older revision identical to the four already found
                                'meaning they were all reversions
                                VersionId = ThisEdit.Id
                                Exit While
                            End If

                            ThisEdit = ThisEdit.Prev
                        End While
                        If Break >= Misc.GlExcess Then Log("Debug interrupted ThreeRevertRuleCheck.Process()")

                        SetResult()
                        Complete()
                        Exit Sub

                    ElseIf Match.Count > 4 Then
                        'If there are more than four identical revisions, we know they are all reversions
                        VersionId = Match(Match.Count - 1)
                        Match.RemoveAt(Match.Count - 1)
                        RevertIds = Match

                        SetResult()
                        Complete()
                        Exit Sub
                    End If
                Next Match
            Next Page

            Complete()
        End Sub

        Private Sub SetResult()
            _Result = New ThreeRevertRuleCheckResult

            If VersionId Is Nothing Then
                _Result.BaseEdit = Nothing
                _Result.Reverts = New List(Of Edit)
            Else
                Dim Reverts As New List(Of Edit)

                For Each Item As String In RevertIds
                    Reverts.Add(Edit.All(Item))
                Next Item

                _Result.BaseEdit = Edit.All(VersionId)
                _Result.Reverts = Reverts
            End If

            MyBase._Result = _Result
        End Sub

    End Class

    Class UpdateLanguagesRequest : Inherits Request

        'Check for localization updates, retrieve appropriate pages

        Protected Overrides Sub Process()
            Dim PathPage As Page = GetPage(Config.LocalizatonPath)

            Dim Result As ApiResult = DoApiRequest("action=query&prop=info&generator=allpages&gapnamespace=" &
                CStr(PathPage.Space.Number) & "&gapprefix=" & PathPage.BaseName, Project:="meta")
            'this is buggy so it has been disabled for a while

            Exit Sub

            If Result.Error Then
                Fail(Msg("login-error-language"), Result.ErrorMessage)
                Exit Sub
            End If

            Dim ToUpdate As New List(Of String)
            Dim UpList As New List(Of String)

            For Each Page As String In Split(Result.Text, "<page ")
                Dim Lang As String = GetParameter(Page, "title")
                If Lang Is Nothing Then

                Else

                    Lang = Lang.Substring(Lang.LastIndexOf("/") + 1)

                    Dim PageTimestamp As Date = CDate(GetParameter(Page, "touched"))
                    Dim FileTimestamp As Date = Date.MinValue

                    If File.Exists(ConfigIO.L10nLocation & Path.DirectorySeparatorChar & Lang & ".txt") Then _
                        FileTimestamp = File.GetLastWriteTime(MakePath(ConfigIO.L10nLocation, Lang & ".txt"))

                    'If local copy of localization does not exist or is out of date
                    If PageTimestamp > FileTimestamp Or Not File.Exists(ConfigIO.L10nLocation & Path.DirectorySeparatorChar & Lang & ".txt") Then ToUpdate.Add(Config.LocalizatonPath & Lang)
                End If
            Next Page

            If ToUpdate.Count = 0 Then
                Complete()
                Exit Sub
            End If

            While ToUpdate.Count <> 0

                UpList.Add(ToUpdate(0))
                ToUpdate.RemoveAt(0)

                Result = DoApiRequest("action=query&prop=revisions&rvprop=content&titles=" &
                    String.Join("|", UpList.ToArray), Project:="meta")
                UpList.Clear()
                If Result.Error Then
                    Fail(Msg("login-error-language"), Result.ErrorMessage)
                    Exit Sub
                End If

                'Store messages locally
                For Each Page As String In Split(Result.Text, "<page ")
                    Dim Language As String = GetParameter(Page, "title")
                    If Language Is Nothing Then
                        Continue For
                    End If

                    Language = Language.Substring(Language.LastIndexOf("/") + 1)

                    Dim Text As String = GetTextFromRev(Page).Replace(LF, CRLF)

                    'Ignore incomplete localizations
                    If Text.Contains("'''INCOMPLETE'''" & CRLF) Then Continue For

                    If (Directory.Exists(ConfigIO.L10nLocation) _
                        OrElse Directory.CreateDirectory(ConfigIO.L10nLocation).Exists) _
                        Then File.WriteAllText(MakePath(ConfigIO.L10nLocation, Language & ".txt"), Text)
                Next Page
            End While
            If Config.Languages.Count = 0 Then Fail("No localization files found.") Else Complete()
        End Sub

    End Class

    Class WhitelistRequest : Inherits Request

        'Get the whitelist

        Protected Overrides Sub Process()
            Dim Result As String
            Dim WhitelistPath As String = MakePath(WhitelistsLocation(), Config.Project & ".txt")
            Dim Question As DialogResult

            Result = DoWebRequest(Config.WhitelistUrl, "action=read&wp=" & UrlEncode(Config.Project))

            If Result Is Nothing Then
                Question = MessageBox.Show("Failed to download the whitelist, continue?", "Whitelist error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If Question = DialogResult.Yes Then
                    If File.Exists(WhitelistPath) Then
                        Whitelist = New List(Of String)(File.ReadAllLines(WhitelistPath))
                    Else
                        Config.WhitelistUsed = False
                        Exit Sub
                    End If

                    Complete()
                    Exit Sub
                Else
                    Fail("Unable to download the whitelist")
                    Exit Sub
                    Complete()
                End If
            End If

            If Result.Contains("<!-- failed") Or Result.Contains("<!-- list -->") = False Then
                Question = MessageBox.Show("Failed to download the whitelist, continue?", "Whitelist error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If Question = DialogResult.Yes Then
                    If File.Exists(WhitelistPath) Then
                        Whitelist = New List(Of String)(File.ReadAllLines(WhitelistPath))
                    Else
                        Config.WhitelistUsed = False
                        Exit Sub
                    End If

                    Complete()
                    Exit Sub
                Else
                    Fail("Unable to download the whitelist")
                    Exit Sub
                    Complete()
                End If
            End If

            Result = Result.Replace("<!-- list -->", "")

            'Check for subpages
            'Dim PathPage As Page = GetPage(Config.WhitelistLocation)

            'If PathPage Is Nothing Then Exit Sub

            'Result = DoApiRequest("action=query&prop=info&generator=allpages&gapnamespace=" & _
            '   CStr(PathPage.Space.Number) & "&gapprefix=" & PathPage.BaseName)

            'If Result.Error Then
            'Fail(Result.ErrorMessage)
            'Exit Sub
            'End If

            'Dim Pages As New List(Of String)
            'Dim NeedsUpdate As Boolean

            'For Each Page As String In Split(Result.Text, "<page ")
            'Dim Name As String = GetParameter(Page, "title")
            'If Name Is Nothing Then Continue For
            'If Name.EndsWith("/Header") Then Continue For

            'Dim PageTimestamp As Date = CDate(GetParameter(Page, "touched"))
            'Dim LocalTimestamp As Date = Date.MinValue

            'If Config.WhitelistTimestamps.ContainsKey(Config.Project & "::" & Name) _
            '    Then Date.TryParse(Config.WhitelistTimestamps(Config.Project & "::" & Name), LocalTimestamp)

            'If local copy of whitelist does not exist or is out of date
            'If PageTimestamp > LocalTimestamp Then NeedsUpdate = True
            'Pages.Add(Config.Project)
            'Next Page

            'If Not NeedsUpdate Then


            'Complete()
            'Exit Sub
            'End If

            'Result = DoApiRequest("action=query&prop=revisions&rvprop=content&titles=" & String.Join("|", Pages.ToArray))

            'If Result.Error Then
            'Fail(Msg("login-error-language"), Result.ErrorMessage)
            'Exit Sub
            'End If

            'For Each Page As String In Split(Result.Text, "<page ")
            'Dim Name As String = GetParameter(Page, "title")
            'If Name Is Nothing Then Continue For
            'Name = Config.Project & "::" & Name

            'If Config.WhitelistTimestamps.ContainsKey(Name) _
            '    Then Config.WhitelistTimestamps(Name) = Date.UtcNow.ToString _
            '    Else Config.WhitelistTimestamps.Add(Name, Date.UtcNow.ToString)
            Whitelist.AddRange(Split(Result, "|"))
            If (Whitelist.Contains("")) Then
                Whitelist.Remove("")
            End If
            'Whitelist.AddRange(Split(GetTextFromRev(Page), LF))
            'Next Page

            Complete()
            Config.WhitelistUsed = True
        End Sub

    End Class

End Namespace