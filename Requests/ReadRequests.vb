Imports System.IO
Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class BlockLogRequest : Inherits Request

        'Retrieve block log for a user

        Public User As User

        Protected Overrides Sub Process()
            Dim Result As ApiResult = DoApiRequest("action=query&list=logevents&letype=block&lelimit=50&letitle=" & _
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
        End Sub

    End Class

    Class DiffRequest : Inherits Request

        Public Edit As Edit, Tab As BrowserTab
        Private Shared _PreloadCount As Integer

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
            If Tab Is Nothing Then Tab = CurrentTab
            Edit.Cached = Edit.CacheState.Caching

            Dim Oldid As String = If(Edit.Oldid = "-1", "prev", Edit.Oldid)

            'Using &action=render here would make things far simpler; unfortunately, that was broken for
            'image diff pages in 2007 and it would appear that nobody cares.

            Result = DoUrlRequest(SitePath() & "index.php?title=" & UrlEncode(Edit.Page.Name) _
                & "&diff=" & Edit.Id & "&oldid=" & Oldid & "&diffonly=1&uselang=en")

            If Result Is Nothing Then
                Edit.Cached = Edit.CacheState.Uncached
                Fail(Msg("diff-fail", Edit.Page.Name), Msg("error-noresponse"))
                Exit Sub

            ElseIf Result.Contains("<div id=""mw-missing-article"">") _
                OrElse Result.Contains("The database did not find the text of a page that it should have found") Then
                Callback(AddressOf Deleted)
                Exit Sub
            End If

            If Result.Contains(">undo</a>)") Then
                Dim Result2 As String = Result.Substring(0, Result.IndexOf(">undo</a>)"))
                Result2 = Result2.Substring(0, Result2.LastIndexOf("(<a") - 1)
                Result = Result2 & Result.Substring(Result.IndexOf(">undo</a>") + 10)
            End If

            If Result.Contains("<table class='diff'>") Then
                Result = Result.Substring(Result.IndexOf("<table class='diff'>"))
                Result = Result.Substring(0, Result.IndexOf("</table>") + 8)
                Result = "<div style=""font-size: 160%; font-family: Arial"">" & Edit.Page.Name & "</div>" & Result

                'Add the change size in the top-right corner
                If Edit.Change <> 0 Then
                    Dim EditChange As String = CStr(Edit.Change)
                    If Edit.Change > 0 Then EditChange = "+" & EditChange

                    Result = "<div style=""float: right; font-size: 140%; font-family: Arial"">" & _
                        EditChange & "</div>" & Result
                End If

                Complete()

            ElseIf Result.Contains("<div class=""firstrevisionheader""") Then
                'This is the first revision to the page... so no diff
                Callback(AddressOf ReachedEnd)

            Else
                Edit.Cached = Edit.CacheState.Uncached
                Fail()
            End If
        End Sub

        Protected Overrides Sub Done()
            PreloadCount -= 1
            If Result IsNot Nothing Then ProcessDiff(Edit, Result, Tab)
        End Sub

        Private Sub ReachedEnd()
            PreloadCount -= 1

            'Mark this as the start of the history
            Edit.Prev = NullEdit
            Edit.Oldid = "-1"
            Edit.Cached = Edit.CacheState.Cached

            If LatestDiffRequest Is Me Then
                MainForm.HistoryPrevB.Enabled = False
                If Edit.Next IsNot Nothing Then Tab.Edit = Edit.Next
                DisplayEdit(CurrentEdit, False, Tab)
            End If

            Complete()
        End Sub

        Private Sub Deleted()
            PreloadCount -= 1
            If LatestDiffRequest Is Me Then CurrentTab.Browser.DocumentText = _
                "<div style=""font-family: Arial"">" & _
                "This revision has been deleted, never existed, or exists " & _
                "but the server handling the request does not know it yet.</div>"
            Complete()
        End Sub

    End Class

    Class HistoryRequest : Inherits Request

        'Fetch page history

        Public Page As Page, BlockSize As Integer, GetContent As Boolean

        Protected Overrides Sub Process()

            BlockSize = Config.HistoryBlockSize
            If GetContent Then BlockSize = Math.Min(ApiLimit() \ 10, BlockSize)

            Dim QueryString As String = "action=query&prop=revisions&titles=" & UrlEncode(Page.Name) & _
                "&rvlimit=" & CStr(BlockSize) & "&rvprop=ids|timestamp|user|comment"

            If GetContent Then QueryString &= "|content"
            If Page.HistoryOffset IsNot Nothing Then QueryString &= "&rvstartid=" & Page.HistoryOffset

            Dim Result As ApiResult = DoApiRequest(QueryString)

            If Result.Error Then
                Fail(Msg("history-fail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Complete(, Result.Text)
        End Sub

        Protected Overrides Sub Done()
            ProcessHistory(_Result.Text, Page)
        End Sub

    End Class

    Class ContribsRequest : Inherits Request

        'Fetch user contributions
        Private Result As ApiResult

        Public User As User, DisplayWhenDone As Boolean, ReportWhenDone As VandalReportRequest
        Public BlockSize As Integer = Config.ContribsBlockSize

        Protected Overrides Sub Process()
            Result = DoApiRequest("action=query&format=xml&list=usercontribs&ucuser=" & _
                UrlEncode(User.Name) & "&uclimit=" & CStr(BlockSize) & "&ucstart=" & User.ContribsOffset)

            If Result.Error Then
                Fail()
            ElseIf Result.Text.Contains("<usercontribs />") Then
                Callback(AddressOf NoContribs)
            ElseIf Result.Text.Contains("<usercontribs") Then
                Complete()
            Else
                Fail()
            End If
        End Sub

        Protected Overrides Sub Done()
            ProcessContribs(Result.Text, User)
            MainForm.DrawContribs()

            If DisplayWhenDone AndAlso User.LastEdit IsNot Nothing Then
                DisplayEdit(User.LastEdit)

                If User.LastEdit.Page.LastEdit Is Nothing Then
                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.Page = User.LastEdit.Page
                    NewHistoryRequest.Start()
                End If
            End If

            If ReportWhenDone IsNot Nothing Then ReportWhenDone.Start()
            Complete()
        End Sub

        Private Sub NoContribs()
            User.LastEdit = NullEdit
            MainForm.UserB.ForeColor = Color.Red
            Complete()
        End Sub

    End Class

    Class BrowserRequest : Inherits Request

        Public Url As String, Tab As BrowserTab, HistoryItem As HistoryItem

        Protected Overrides Sub Process()
            Tab.LastBrowserRequest = Me

            Dim Result As String = DoUrlRequest(Url)

            If Result Is Nothing Then Fail() Else Complete(, Result)
        End Sub

        Protected Overrides Sub Done()
            If MainForm.Visible AndAlso Tab.LastBrowserRequest Is Me Then
                Dim PageName As String = ParseUrl(Url)("title")
                MainForm.PageB.Text = PageName
                _Result.Text = FormatPageHtml(GetPage(PageName), _Result.Text)
                Tab.Browser.DocumentText = _Result.Text
                Tab.CurrentUrl = Url
                If HistoryItem IsNot Nothing Then HistoryItem.Text = _Result.Text
            End If
        End Sub

    End Class

    Class RcApiRequest : Inherits Request

        'Get recent changes through the API, if IRC mode not enabled
        'Bearable, but IRC is better

        Protected Overrides Sub Process()
            Dim Result As ApiResult = DoApiRequest("action=query&list=recentchanges" & _
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

                    If Count > Config.WhitelistEditCount Then User.Ignored = True
                    WhitelistAutoChanges.Add(User.Name)
                End If
            Next Item

            Complete()
        End Sub

    End Class

    Class WarningLogRequest : Inherits Request

        'Retrieve user's talk page and extract a log of warnings

        Public User As User

        Protected Overrides Sub Process()
            Dim Result As ApiResult = DoApiRequest("action=query&format=xml&prop=revisions&rvprop=content&titles=" & _
                UrlEncode(User.TalkPage.Name))

            If Result.Error Then
                Fail(Msg("warninglog-fail", User.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Dim Revisions As String = HtmlDecode(FindString(Result.Text, "<revisions><rev>", "</rev>"))

            If Revisions IsNot Nothing Then
                User.Warnings = ProcessUserTalk(Revisions, User)
                User.Warnings.Sort(AddressOf SortWarningsByDate)
            End If

            Complete()
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

            Page.Text = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>"))
            Complete(, Page.Text)
        End Sub

    End Class

    Class PreviewRequest : Inherits Request

        'Get preview of unsaved changes

        Public Page As Page, Text As String

        Protected Overrides Sub Process()
            Dim Result As ApiResult = _
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
            Dim Result As String = DoUrlRequest(SitePath() & "index.php?title=" & _
                UrlEncode(Page.Name) & "&action=submit", "&wpDiff=0&wpStarttime=" & Timestamp(Date.UtcNow) & _
                "&wpEdittime=&wpTextbox1=" & UrlEncode(Text))

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

            Dim Result As ApiResult = DoApiRequest("action=query&list=logevents&letype=protect&lelimit=100" & _
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
                        NewProtection.EditExpiry = CDate(FindString(FindString(Param, "[edit=", "["), "(expires ", " (UTC))"))
                        NewProtection.MoveExpiry = CDate(FindString(FindString(Param, "[move="), "(expires ", " (UTC))"))
                        NewProtection.Cascading = (Param.Contains("[cascading]"))
                    End If

                    NewProtection.Summary = Comment

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
            Dim Result As ApiResult = DoApiRequest("format=xml&action=query&list=logevents&letype=delete&lelimit=50" & _
                "&letitle=" & UrlEncode(Page.Name))

            If Result.Error Then
                Fail(, Result.ErrorMessage)
                Exit Sub
            End If

            If Page.Deletes IsNot Nothing Then Page.Deletes.Clear()
            If Page.Deletes Is Nothing Then Page.Deletes = New List(Of Delete)
            Page.DeletesCurrent = True

            For Each Item As String In Split(Result.Text, LF)
                Dim NewDelete As New Delete

                NewDelete.Page = Page
                NewDelete.Time = CDate(GetParameter(Item, "timestamp"))
                NewDelete.Action = GetParameter(Item, "action")
                NewDelete.Admin = GetUser(GetParameter(Item, "user"))
                NewDelete.Comment = GetParameter(Item, "comment")

                Page.Deletes.Add(NewDelete)
            Next Item

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
            Dim QueryString As String = "action=query&prop=revisions&rvprop=ids|content&revids="

            Dim ThisEdit As Edit = User.LastEdit, i As Integer = ApiLimit() \ 10

            While ThisEdit IsNot NullEdit AndAlso ThisEdit IsNot Nothing AndAlso i > 0
                QueryString &= ThisEdit.Id & "|"
                ThisEdit = ThisEdit.PrevByUser
                i -= 1
            End While

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
                Dim Revisions As New List(Of String)(PageResult.Split(New String() {"<rev "}, _
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

                        While ThisEdit IsNot NullEdit AndAlso ThisEdit IsNot Nothing
                            If Not RevertIds.Contains(ThisEdit.Id) AndAlso ThisEdit.Text = TextToFind _
                                AndAlso ThisEdit.Time < Edit.All(RevertIds(0)).Time Then

                                'Found an older revision identical to the four already found
                                'meaning they were all reversions
                                VersionId = ThisEdit.Id
                                Exit While
                            End If

                            ThisEdit = ThisEdit.Prev
                        End While

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

            Dim Result As ApiResult = DoApiRequest("action=query&prop=info&generator=allpages&gapnamespace=" & _
                CStr(PathPage.Space.Number) & "&gapprefix=" & PathPage.BaseName, Project:=Config.Projects("meta"))

            If Result.Error Then
                Fail(Msg("login-error-language"), Result.ErrorMessage)
                Exit Sub
            End If

            Dim ToUpdate As New List(Of String)

            For Each Page As String In Split(Result.Text, "<page ")
                Dim Lang As String = GetParameter(Page, "title")
                If Lang Is Nothing Then Continue For
                Lang = Lang.Substring(Lang.LastIndexOf("/") + 1)

                Dim PageTimestamp As Date = CDate(GetParameter(Page, "touched"))
                Dim FileTimestamp As Date

                If File.Exists(ConfigIO.L10nLocation & Path.DirectorySeparatorChar & Lang & ".txt") Then _
                    FileTimestamp = File.GetLastWriteTime(ConfigIO.L10nLocation & Path.DirectorySeparatorChar & Lang & ".txt")

                'If local copy of localization does not exist or is out of date
                If PageTimestamp > FileTimestamp Then ToUpdate.Add(Config.LocalizatonPath & Lang)
            Next Page

            If ToUpdate.Count = 0 Then
                Complete()
                Exit Sub
            End If

            Result = DoApiRequest("action=query&prop=revisions&rvprop=content&titles=" & _
                String.Join("|", ToUpdate.ToArray), Project:=Config.Projects("meta"))

            If Result.Error Then
                Fail(Msg("login-error-language"), Result.ErrorMessage)
                Exit Sub
            End If

            'Store messages locally

            For Each Page As String In Split(Result.Text, "<page ")
                Dim Language As String = GetParameter(Page, "title")
                If Language Is Nothing Then Continue For
                Language = Language.Substring(Language.LastIndexOf("/") + 1)

                Dim Text As String = HtmlDecode(FindString(Page, "<rev>", "</rev>")).Replace(LF, CRLF)

                If Directory.Exists(ConfigIO.L10nLocation) _
                    OrElse Directory.CreateDirectory(ConfigIO.L10nLocation).Exists _
                    Then File.WriteAllText(ConfigIO.L10nLocation & "\" & Language & ".txt", Text)
            Next Page

            If Config.Languages.Count = 0 Then Fail("No localization files found.") Else Complete()
        End Sub

    End Class

End Namespace