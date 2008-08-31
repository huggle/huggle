Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class BlockLogRequest : Inherits Request

        Public ThisUser As User, Target As ListView

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetApi("format=xml&action=query&list=logevents&letype=block&letitle=User:" & _
                UrlEncode(ThisUser.Name) & "&lelimit=50").Replace(LF, "")

            Dim LogMatches As MatchCollection = _
                New Regex("<item logid=""[0-9]+"" pageid=""[0-9]+"" ns=""2"" title=""User:[^""]+"" " & _
                "type=""block"" action=""(block|unblock)"" user=""([^""]+)"" timestamp=""([^""]+)"" " & _
                "comment=""([^""]+)""[^<]*(<block flags=""([^""]*)"" duration=""([^""]*)"")?", _
                RegexOptions.Compiled).Matches(Result)

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If ThisUser.Blocks IsNot Nothing Then ThisUser.Blocks.Clear()

            If LogMatches.Count > 0 Then
                If ThisUser.Blocks Is Nothing Then ThisUser.Blocks = New List(Of Block)

                For Each Item As Match In LogMatches
                    Dim NewBlock As New Block

                    NewBlock.Time = CDate(Item.Groups(3).Value)
                    NewBlock.Action = HtmlDecode(Item.Groups(1).Value)
                    NewBlock.Duration = HtmlDecode(Item.Groups(7).Value)
                    NewBlock.Options = HtmlDecode(Item.Groups(6).Value)
                    NewBlock.Admin = GetUser(HtmlDecode(Item.Groups(2).Value))
                    NewBlock.Comment = HtmlDecode(Item.Groups(4).Value)

                    If ThisUser.Blocks Is Nothing Then ThisUser.Blocks = New List(Of Block)
                    ThisUser.Blocks.Add(NewBlock)
                Next Item
            End If

            Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Target IsNot Nothing Then
                If ThisUser.Blocks Is Nothing OrElse ThisUser.Blocks.Count = 0 Then
                    If Target.Items.Count > 0 Then Target.Items(0).Text = "No block log for this user."
                Else
                    Target.Items.Clear()
                    Target.Columns.Clear()
                    Target.Columns.Add("Date", 105)
                    Target.Columns.Add("Action", 50)
                    Target.Columns.Add("Duration", 60)
                    Target.Columns.Add("Options", 70)
                    Target.Columns.Add("Admin", 90)
                    Target.Columns.Add("Comment", 100)

                    For Each Item As Block In ThisUser.Blocks
                        Dim NewItem As New ListViewItem
                        NewItem.Text = Item.Time.ToShortDateString & " " & Item.Time.ToShortTimeString
                        NewItem.SubItems.Add(Item.Action)
                        NewItem.SubItems.Add(Item.Duration)
                        NewItem.SubItems.Add(Item.Options)
                        NewItem.SubItems.Add(Item.Admin.Name)
                        NewItem.SubItems.Add(TrimSummary(Item.Comment))

                        Target.Items.Add(NewItem)
                    Next Item
                End If
            End If

            Complete()
        End Sub

        Private Sub Failed()
            If Target IsNot Nothing AndAlso Target.Items.Count > 0 _
                Then Target.Items(0).Text = "Failed to retrieve block log."
            Fail()
        End Sub

    End Class

    Class DiffRequest : Inherits Request

        Public Edit As Edit, Tab As BrowserTab

        Public Sub Start()
            If Tab Is Nothing Then Tab = CurrentTab
            Edit.Cached = Edit.CacheState.Caching

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Oldid As String = If(Edit.Oldid = "-1", "prev", Edit.Oldid)

            'Using &action=render here would make things far simpler; unfortunately, that was broken for
            'image diff pages in 2007 and it would appear that nobody cares.

            Result = GetText("title=" & UrlEncode(Edit.Page.Name) _
                & "&diff=" & Edit.Id & "&oldid=" & Oldid & "&diffonly=1&uselang=en")

            If Result Is Nothing Then
                Callback(AddressOf Failed)
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
                If Edit.Size <> 0 Then
                    Dim EditChange As String = CStr(Edit.Size)
                    If Edit.Size > 0 Then EditChange = "+" & EditChange

                    Result = "<div style=""float: right; font-size: 140%; font-family: Arial"">" & _
                        EditChange & "</div>" & Result
                End If

                Callback(AddressOf Done)

            ElseIf Result.Contains("<div class=""firstrevisionheader""") Then
                'This is the first revision to the page... so no diff
                Callback(AddressOf ReachedEnd)

            Else
                Callback(AddressOf Failed)
            End If
        End Sub

        Private Sub Done()
            ProcessDiff(Edit, Result, Tab)
            Complete()
        End Sub

        Private Sub ReachedEnd()
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
            If LatestDiffRequest Is Me Then CurrentTab.Browser.DocumentText = _
                "<div style=""font-family: Arial"">" & _
                "This revision has been deleted, never existed, or exists " & _
                "but the server handling the request does not know it yet.</div>"

            Complete()
        End Sub

        Private Sub Failed()
            Edit.Cached = Edit.CacheState.Uncached
            Fail()
        End Sub

    End Class

    Class HistoryRequest : Inherits Request

        'Fetch page history

        Public Page As Page, BlockSize As Integer, DisplayWhenDone, GetContent As Boolean

        Public Sub Start(Optional ByVal Done As RequestCallback = Nothing)
            _Done = Done
            BlockSize = HistoryBlockSize
            If GetContent Then BlockSize = Math.Min(ApiLimit() \ 10, BlockSize)
            If Page.HistoryOffset Is Nothing Then Page.HistoryOffset = ""

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim QueryString As String = "format=xml&action=query&prop=revisions&titles=" & _
                UrlEncode(Page.Name) & "&rvlimit=" & CStr(BlockSize) & "&rvprop=ids|timestamp|user|comment"

            If GetContent Then QueryString &= "|content"
            If Page.HistoryOffset <> "" Then QueryString &= "&rvstartid=" & Page.HistoryOffset

            Result = GetApi(QueryString)

            If Result Is Nothing Then
                Callback(AddressOf Failed)
            ElseIf Result.Contains("<revisions") Then
                Callback(AddressOf Done)
            ElseIf Result.Contains("missing="""" />") Then
                Callback(AddressOf NoPage)
            ElseIf Result.Contains("invalid="""" />") Then
                Callback(AddressOf BadTitle)
            Else
                Callback(AddressOf Failed)
            End If
        End Sub

        Private Sub Done()
            ProcessHistory(Result, Page)
            MainForm.DrawHistory()

            If DisplayWhenDone AndAlso Page.LastEdit IsNot Nothing Then
                DisplayEdit(Page.LastEdit)

                If Page.LastEdit.User.LastEdit Is Nothing Then
                    Dim NewContribsRequest As New ContribsRequest
                    NewContribsRequest.User = Page.LastEdit.User
                    NewContribsRequest.Start()
                End If
            End If

            Complete()
        End Sub

        Private Sub BadTitle()
            MainForm.PageB.ForeColor = Color.Red
            Fail()
        End Sub

        Private Sub NoPage()
            Page.LastEdit = NullEdit
            MainForm.PageB.ForeColor = Color.Red
            Fail()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class ContribsRequest : Inherits Request

        'Fetch user contributions

        Public User As User
        Public DisplayWhenDone As Boolean, ReportWhenDone As AIVReportRequest
        Public BlockSize As Integer = ContribsBlockSize

        Public Sub Start(Optional ByVal Done As RequestCallback = Nothing)
            _Done = Done
            If User.ContribsOffset Is Nothing Then User.ContribsOffset = ""

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetApi("action=query&format=xml&list=usercontribs&ucuser=" & _
                UrlEncode(User.Name) & "&uclimit=" & CStr(BlockSize) & "&ucstart=" & User.ContribsOffset)

            If Result.Contains("<usercontribs />") Then
                Callback(AddressOf NoContribs)
            ElseIf Result.Contains("<usercontribs") Then
                Callback(AddressOf Done)
            Else
                Callback(AddressOf Failed)
            End If
        End Sub

        Private Sub Done()
            ProcessContribs(Result, User)
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

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class BrowserRequest : Inherits Request

        Public Url As String, Tab As BrowserTab, HistoryItem As HistoryItem

        Public Sub Start()
            Tab.LastBrowserRequest = Me
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetUrl(Url)
            If Result IsNot Nothing Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            If MainForm.Visible AndAlso Tab.LastBrowserRequest Is Me Then
                Dim PageName As String = ParseUrl(Url)("title")
                MainForm.PageB.Text = PageName
                Result = FormatPageHtml(GetPage(PageName), Result)
                Tab.Browser.DocumentText = Result
                Tab.CurrentUrl = Url
                If HistoryItem IsNot Nothing Then HistoryItem.Text = Result
            End If

            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class RcApiRequest : Inherits Request

        'Get recent changes through the API, if IRC mode not enabled
        'Bearable, but IRC is better

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetApi("action=query&format=xml&list=recentchanges" & _
                "&rclimit=" & CStr(Config.RcBlockSize) & "&rcprop=user|comment|flags|timestamp|title|ids|sizes")

            If Result IsNot Nothing Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            ProcessRcApi(Result)
            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class BlockApiRequest : Inherits Request

        'Get blocks through the API, if IRC mode not enabled

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetApi("action=query&format=xml&list=blocks&bklimit=50&bkstart=" & Timestamp(Date.UtcNow))

            If Result IsNot Nothing Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            Dim BlockMatches As MatchCollection = Regex.Matches(Result, "<block id=""[0-9]+"" user=""([^""]+)""", _
                RegexOptions.Compiled)

            For Each Item As Match In BlockMatches
                Dim BlockedUser As User = GetUser(Item.Groups(1).Value)
                BlockedUser.BlocksCurrent = False
                If BlockedUser.Level <> UserLevel.Blocked Then BlockedUser.Level = UserLevel.Blocked
            Next Item

            MainForm.BlockReqTimer.Start()
            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class CountRequest : Inherits Request

        'Accepts a list of users, gets their edit counts
        'whitelists any with more than WhitelistEditCount edits

        Public Users As New List(Of User)

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Query As String = ""

            For Each Item As User In Users
                Query &= UrlEncode(Item.Name) & "|"
            Next Item

            Query = Query.Substring(0, Query.Length - 1)

            Result = GetApi("format=xml&action=query&list=users&usprop=editcount&ususers=" & Query)

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Dim EditCounts As New Dictionary(Of User, Integer)
            Dim Items As String() = Result.Split(New String() {"<user "}, StringSplitOptions.RemoveEmptyEntries)

            For i As Integer = 0 To Items.Length - 1
                If Items(i).Contains("name=""") AndAlso Items(i).Contains("editcount=""") Then
                    Dim Username As String = Items(i).Substring(Items(i).IndexOf("name=""") + 6)
                    Username = Username.Substring(0, Username.IndexOf(""""))
                    Username = HtmlDecode(Username)

                    Dim Count As String = Items(i).Substring(Items(i).IndexOf("editcount=""") + 11)
                    Count = Count.Substring(0, Count.IndexOf(""""))

                    EditCounts.Add(GetUser(Username), CInt(Count))
                End If
            Next i

            Callback(AddressOf Done, CObj(EditCounts))
        End Sub

        Private Sub Done(ByVal ResultObject As Object)
            Dim EditCounts As Dictionary(Of User, Integer) = CType(ResultObject, Dictionary(Of User, Integer))

            For Each Item As KeyValuePair(Of User, Integer) In EditCounts
                Item.Key.EditCount = Item.Value

                If Item.Key.EditCount > Config.WhitelistEditCount Then
                    'Ignore user
                    Item.Key.Ignored = True
                    WhitelistAutoChanges.Add(Item.Key.Name)
                End If
            Next Item

            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class WatchRequest : Inherits Request

        'Adds page to your watchlist

        Public Page As Page, Manual As Boolean

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetText("title=" & UrlEncode(Page.Name) & "&action=watch")
            If Result IsNot Nothing Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            If Manual Then Log("Added '" & Page.Name & "' to watchlist")
            If Not Watchlist.Contains(Page.SubjectPage) Then Watchlist.Add(Page.SubjectPage)
            If CurrentEdit IsNot Nothing AndAlso Page Is CurrentEdit.Page Then MainForm.UpdateWatchButton()

            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class UnwatchRequest : Inherits Request

        'Removes page from your watchlist

        Public Page As Page, Manual As Boolean

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetText("title=" & UrlEncode(Page.Name) & "&action=unwatch")
            If Result IsNot Nothing Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            If Manual Then Log("Removed '" & Page.Name & "' from watchlist")
            If Watchlist.Contains(Page.SubjectPage) Then Watchlist.Remove(Page.SubjectPage)
            If CurrentEdit IsNot Nothing AndAlso Page Is CurrentEdit.Page Then MainForm.UpdateWatchButton()

            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class WarningLogRequest : Inherits Request

        Public ThisUser As User, Target As ListView

        Public Sub Start()

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetApi("action=query&format=xml&prop=revisions&rvprop=content&titles=User talk:" & _
                UrlEncode(ThisUser.Name))

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If Result.Contains("<revisions><rev>") Then
                Result = Result.Substring(Result.IndexOf("<revisions><rev>") + 16)
                Result = Result.Substring(0, Result.IndexOf("</rev>"))
                Result = HtmlDecode(Result)

                ThisUser.Warnings = ProcessUserTalk(Result, ThisUser)
                If ThisUser.Warnings IsNot Nothing Then ThisUser.Warnings.Sort(AddressOf SortWarningsByDate)
            End If

            Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Target IsNot Nothing Then
                If ThisUser.Warnings Is Nothing OrElse ThisUser.Warnings.Count = 0 Then
                    If Target.Items.Count > 0 Then Target.Items(0).Text = "No warnings for this user."
                Else
                    For Each Item As Warning In ThisUser.Warnings
                        If Item.Time.AddHours(Config.WarningAge) > My.Computer.Clock.GmtTime Then
                            If Item.Level > ThisUser.Level Then ThisUser.Level = Item.Level
                            If ThisUser.WarnTime < Item.Time Then ThisUser.WarnTime = Item.Time
                        End If
                    Next Item

                    Target.Items.Clear()
                    Target.Columns.Clear()
                    Target.Columns.Add("Date", 100)
                    Target.Columns.Add("Level", 60)
                    Target.Columns.Add("Type", 80)
                    Target.Columns.Add("User", 120)

                    For Each Warning As Warning In ThisUser.Warnings
                        Dim NewItem As New ListViewItem

                        NewItem.Text = Warning.Time.ToShortDateString & " " & Warning.Time.ToShortTimeString
                        If Warning.Time.AddHours(36) > Date.UtcNow Then NewItem.ForeColor = Color.Blue

                        Select Case Warning.Level
                            Case UserLevel.Notification : NewItem.SubItems.Add("--")
                            Case UserLevel.Warn1 : NewItem.SubItems.Add("Level 1")
                            Case UserLevel.Warn2 : NewItem.SubItems.Add("Level 2")
                            Case UserLevel.Warn3 : NewItem.SubItems.Add("Level 3")
                            Case UserLevel.Warn4im : NewItem.SubItems.Add("Level 4im")
                            Case UserLevel.WarnFinal : NewItem.SubItems.Add("Level 4")
                            Case UserLevel.Blocked : NewItem.SubItems.Add("Blocked")
                            Case Else : NewItem.SubItems.Add("--")
                        End Select

                        NewItem.SubItems.Add(Warning.Type)

                        If Warning.User IsNot Nothing Then NewItem.SubItems.Add(Warning.User.Name) _
                            Else NewItem.SubItems.Add("?")

                        Target.Items.Add(NewItem)
                    Next Warning
                End If
            End If

            Complete()
        End Sub

        Private Sub Failed()
            If Target IsNot Nothing AndAlso Target.Items.Count > 0 Then
                Target.Items(0).Text = "No warnings for this user."
            End If

            Fail()
        End Sub

    End Class

    Class PageTextRequest : Inherits Request

        'Get text of page

        Public Page As Page

        Public Sub Start(Optional ByVal Done As RequestCallback = Nothing)
            _Done = Done

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetPageText(Page.Name)
            If Result IsNot Nothing Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            Page.Text = Result
            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class PreviewRequest : Inherits Request

        'Get preview of unsaved changes

        Public Page As Page, Text As String

        Public Sub Start(Optional ByVal Done As RequestCallback = Nothing)
            _Done = Done

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = PostData("title=" & UrlEncode(Page.Name) & "&action=submit", _
                "wpTextbox1=" & UrlEncode(Text) & "&wpPreview=1&live=1")

            If Result IsNot Nothing Then
                Result = Result.Substring(Result.IndexOf("<preview>") + 9)
                Result = Result.Substring(0, Result.IndexOf("</preview>"))
                Result = HtmlDecode(Result)
                Result = Result.Substring(Result.IndexOf("</div>") + 6)
                Callback(AddressOf Done)
            Else
                Callback(AddressOf Failed)
            End If
        End Sub

        Private Sub Done()
            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class ChangesRequest : Inherits Request

        'Get diff of unsaved changes

        Public Page As Page, Text As String

        Public Sub Start(Optional ByVal Done As RequestCallback = Nothing)
            _Done = Done

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = PostData("title=" & UrlEncode(Page.Name) & "&action=submit", _
                "&wpDiff=0&wpStarttime=" & Timestamp(Date.UtcNow) & "&wpEdittime=&wpTextbox1=" & UrlEncode(Text))

            If Result Is Nothing OrElse Not IsWikiPage(Result) Then
                Callback(AddressOf Failed)
            Else
                Result = Result.Substring(Result.IndexOf("<div id=""wikiDiff"">"))
                Result = Result.Substring(0, Result.IndexOf("<form"))
                Callback(AddressOf Done)
            End If
        End Sub

        Private Sub Done()
            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class ProtectionLogRequest : Inherits Request

        Public Page As Page, Target As ListView

        Public Sub Start()
            If Page.ProtectionsCurrent Then
                Done()
                Exit Sub
            End If

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetApi("format=xml&action=query&list=logevents" & _
                "&letype=protect&lelimit=100&letitle=" & UrlEncode(Page.Name))

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Result = Result.Replace(LF, "")

            If Page.Protections IsNot Nothing Then Page.Protections.Clear()
            Page.ProtectionsCurrent = True

            Dim LogMatches As MatchCollection = _
                New Regex("<item logid=""[0-9]+"" pageid=""[0-9]+"" ns=""[0-9]+"" title=""[^""]+"" " & _
                "type=""protect"" action=""(protect|unprotect|modify)"" user=""([^""]+)"" timestamp=""([^""]+)"" " & _
                "comment=""([^""\[]+)?(?:\[(?:edit=(sysop|autoconfirmed))?:?(?:move=(sysop|autoconfirmed))?\])?(?: " & _
                "\[(cascading)\])?(?: \(expires ([^\(]+)\(UTC\)\))?"" />", RegexOptions.Compiled).Matches(Result)

            If LogMatches.Count > 0 Then
                If Page.Protections Is Nothing Then Page.Protections = New List(Of Protection)

                For Each Item As Match In LogMatches
                    Dim NewProtection As New Protection

                    NewProtection.Time = CDate(Item.Groups(3).Value)
                    NewProtection.Action = HtmlDecode(Item.Groups(1).Value)
                    NewProtection.Admin = GetUser(HtmlDecode(Item.Groups(2).Value))
                    NewProtection.Summary = HtmlDecode(Item.Groups(4).Value).Trim(" "c)
                    NewProtection.EditLevel = Item.Groups(5).Value
                    NewProtection.MoveLevel = Item.Groups(6).Value
                    NewProtection.Cascading = (Item.Groups(7).Value = "cascading")
                    Date.TryParse(Item.Groups(8).Value, NewProtection.Expiry)

                    If Page.Protections Is Nothing Then Page.Protections = New List(Of Protection)
                    Page.Protections.Add(NewProtection)
                Next Item
            End If

            If Page.Protections IsNot Nothing AndAlso (Page.Protections(0).Expiry = Date.MinValue _
                OrElse Page.Protections(0).Expiry > Date.UtcNow) Then

                Page.EditLevel = Page.Protections(0).EditLevel
                Page.MoveLevel = Page.Protections(0).MoveLevel
            End If

            Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Target IsNot Nothing Then
                If Page.Protections Is Nothing OrElse Page.Protections.Count = 0 Then
                    Target.Items(0).Text = "No protection log for this page."
                Else
                    Target.Items.Clear()
                    Target.Columns.Clear()
                    Target.Columns.Add("Date", 110)
                    Target.Columns.Add("Admin", 100)
                    Target.Columns.Add("Edit", 50)
                    Target.Columns.Add("Move", 50)
                    Target.Columns.Add("Expiry", 110)
                    Target.Columns.Add("Comment", 120)

                    For Each Item As Protection In Page.Protections
                        Dim NewListViewItem As New ListViewItem

                        NewListViewItem.Text = Item.Time.ToShortDateString & " " & Item.Time.ToShortTimeString
                        NewListViewItem.SubItems.Add(Item.Admin.Name)
                        NewListViewItem.SubItems.Add(Item.EditLevel.Replace("autoconfirmed", "autoc."))
                        NewListViewItem.SubItems.Add(Item.MoveLevel.Replace("autoconfirmed", "autoc."))
                        If Item.Expiry = Date.MinValue Then NewListViewItem.SubItems.Add("-") _
                            Else NewListViewItem.SubItems.Add(Item.Expiry.ToShortDateString & " " & _
                            Item.Expiry.ToShortTimeString)
                        NewListViewItem.SubItems.Add(TrimSummary(Item.Summary))

                        Target.Items.Add(NewListViewItem)
                    Next Item
                End If
            End If

            Target.Enabled = False
            Target.Enabled = True
            Complete()
        End Sub

        Private Sub Failed()
            If Target IsNot Nothing AndAlso Target.Items.Count > 0 _
                Then Target.Items(0).Text = "Failed to retrieve protection log."
            Fail()
        End Sub

    End Class

    Class DeleteLogRequest : Inherits Request

        Public Page As Page, Target As ListView

        Public Sub Start()
            If Page.DeletesCurrent Then
                Done()
                Exit Sub
            End If

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetApi("format=xml&action=query&list=logevents" & _
                "&letype=delete&lelimit=50&letitle=" & UrlEncode(Page.Name))

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Result = Result.Replace(LF, "")

            If Page.Deletes IsNot Nothing Then Page.Deletes.Clear()
            Page.DeletesCurrent = True

            Dim LogMatches As MatchCollection = _
                New Regex("<item logid=""0"" pageid=""[0-9]+"" ns=""[0-9]+"" title=""[^""]+"" " & _
                "type=""delete"" action=""(delete|undelete)"" user=""([^""]+)"" timestamp=""([^""]+)"" " & _
                "comment=""([^""]+)"" />", RegexOptions.Compiled).Matches(Result)

            If LogMatches.Count > 0 Then
                If Page.Deletes Is Nothing Then Page.Deletes = New List(Of Delete)

                For Each Item As Match In LogMatches
                    Dim NewDelete As New Delete

                    NewDelete.Time = CDate(Item.Groups(3).Value)
                    NewDelete.Action = HtmlDecode(Item.Groups(1).Value)
                    NewDelete.Admin = GetUser(HtmlDecode(Item.Groups(2).Value))
                    NewDelete.Comment = HtmlDecode(Item.Groups(4).Value)

                    If Page.Deletes Is Nothing Then Page.Deletes = New List(Of Delete)
                    Page.Deletes.Add(NewDelete)
                Next Item
            End If

            Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Target IsNot Nothing Then
                If Page.Deletes Is Nothing OrElse Page.Deletes.Count = 0 Then
                    Target.Items(0).Text = "No deletion log for this page."
                Else
                    Target.Items.Clear()
                    Target.Columns.Clear()
                    Target.Columns.Add("Date", 110)
                    Target.Columns.Add("Action", 50)
                    Target.Columns.Add("Admin", 100)
                    Target.Columns.Add("Comment", 200)

                    For Each Item As Delete In Page.Deletes
                        Dim NewListViewItem As New ListViewItem

                        NewListViewItem.Text = Item.Time.ToShortDateString & " " & Item.Time.ToShortTimeString
                        NewListViewItem.SubItems.Add(Item.Action)
                        NewListViewItem.SubItems.Add(Item.Admin.Name)
                        NewListViewItem.SubItems.Add(TrimSummary(Item.Comment))

                        Target.Items.Add(NewListViewItem)
                    Next Item
                End If
            End If

            Complete()
        End Sub

        Private Sub Failed()
            If Target IsNot Nothing AndAlso Target.Items.Count > 0 _
                Then Target.Items(0).Text = "Failed to retrieve deletion log."
            Fail()
        End Sub

    End Class

    Class PurgeRequest : Inherits Request

        'Purge a page

        Public Page As Page

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetText("title=" & UrlEncode(Page.Name) & "&action=purge")
            If IsWikiPage(Result) Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            Log("Purged '" & Page.Name & "'")
            Complete()
        End Sub

        Private Sub Failed()
            Log("Failed to purge '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

    Class StartupMessageRequest : Inherits Request

        'Get message shown to user when logging in

        Public Sub Start(Optional ByVal Done As RequestCallback = Nothing)
            _Done = Done

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetApi("action=parse&format=xml&text={{" & Config.StartupMessageLocation & "}}")

            If Result Is Nothing OrElse Not Result.Contains("<text>") Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Result = Result.Substring(Result.IndexOf("<text>") + 6)
            Result = Result.Substring(0, Result.IndexOf("</text>"))
            Result = HtmlDecode(Result)

            Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class ThreeRevertRuleCheckRequest : Inherits Request

        'Check for three-revert rule violation by specified user

        Private Shadows _Done As ThreeRevertRuleCheckCallback
        Private VersionId As String, RevertIds As New List(Of String)

        Public Delegate Sub ThreeRevertRuleCheckCallback(ByVal Result As Request.Output, _
            ByVal BaseEdit As Edit, ByVal Reverts As List(Of Edit))

        Public User As User

        Public Sub Start(ByVal Done As ThreeRevertRuleCheckCallback)
            _Done = Done

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim QueryString As String = "action=query&format=xml&prop=revisions&rvprop=ids|content&revids="

            Dim ThisEdit As Edit = User.LastEdit, i As Integer = ApiLimit() \ 10

            While ThisEdit IsNot NullEdit AndAlso ThisEdit IsNot Nothing AndAlso i > 0
                QueryString &= ThisEdit.Id & "|"
                ThisEdit = ThisEdit.PrevByUser
                i -= 1
            End While

            QueryString = QueryString.Trim("|"c)

            Result = GetApi(QueryString)

            If Not Result.Contains("<pages>") Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            'Search for revisions with the same content
            Result = Result.Substring(Result.IndexOf("<pages>") + 7)
            Result = Result.Substring(0, Result.IndexOf("</pages>"))

            Dim Pages As New List(Of String)(Result.Split(New String() {"<page "}, StringSplitOptions.RemoveEmptyEntries))

            For Each Page As String In Pages
                Dim PageName As String = Page.Substring(Page.IndexOf("title=""") + 7)
                PageName = PageName.Substring(0, PageName.IndexOf(""""))

                Dim PageResult As String = Page.Substring(Page.IndexOf("<revisions>") + 11)
                PageResult = PageResult.Substring(0, PageResult.IndexOf("</revisions>"))

                Dim Revisions As New List(Of String)(PageResult.Split(New String() {"<rev "}, _
                    StringSplitOptions.RemoveEmptyEntries))
                Dim RevisionMatches As New Dictionary(Of String, List(Of String))

                For Each Revision As String In Revisions
                    Dim Revid As String = Revision.Substring(Revision.IndexOf("revid=""") + 7)
                    Revid = Revid.Substring(0, Revid.IndexOf(""""))

                    Revision = Revision.Substring(Revision.IndexOf(">") + 1)
                    Revision = Revision.Substring(0, Revision.IndexOf("</rev>"))
                    Revision = HtmlDecode(Revision)

                    If Edit.All.ContainsKey(Revid) Then Edit.All(Revid).Text = Revision

                    If RevisionMatches.ContainsKey(Revision) Then RevisionMatches(Revision).Add(Revid) _
                        Else RevisionMatches.Add(Revision, New List(Of String)(New String() {Revid}))
                Next Revision

                For Each Match As List(Of String) In RevisionMatches.Values
                    If Match.Count = 3 Then
                        'We have found three identical revisions to the same page by the same user within 24 hours
                        'Now we need to ensure the earliest is a reversion to a previous revision,
                        'otherwise only two reversions have been made. This means looking further back in the 
                        'page history for an identical revision, possibly by another user
                        RevertIds = Match
                        Callback(AddressOf HistoryNeeded)
                        Exit Sub

                    ElseIf Match.Count > 3 Then
                        'If there are more than three identical revisions, we know they are all reversions
                        VersionId = Match(Match.Count - 1)
                        Match.RemoveAt(Match.Count - 1)
                        RevertIds = Match
                        Callback(AddressOf Done)
                        Exit Sub
                    End If
                Next Match
            Next Page

            Callback(AddressOf Done)
        End Sub

        Private Sub HistoryNeeded()
            Dim NewRequest As New HistoryRequest
            NewRequest.Page = Edit.All(RevertIds(0)).Page
            NewRequest.GetContent = True
            NewRequest.Start(AddressOf HistoryDone)
        End Sub

        Private Sub HistoryDone(ByVal Result As Request.Output)
            If Not Result.Success Then
                Failed()
                Exit Sub
            End If

            Dim ThisEdit As Edit = Edit.All(RevertIds(0)).Page.LastEdit
            Dim TextToFind As String = Edit.All(RevertIds(0)).Text

            While ThisEdit IsNot NullEdit AndAlso ThisEdit IsNot Nothing
                If Not RevertIds.Contains(ThisEdit.Id) AndAlso ThisEdit.Text = TextToFind _
                    AndAlso ThisEdit.Time < Edit.All(RevertIds(0)).Time Then

                    'Found an older revision identical to the three already found, meaning they were all reversions
                    VersionId = ThisEdit.Id
                    Done()
                    Exit Sub
                End If

                ThisEdit = ThisEdit.Prev
            End While

            Done()
        End Sub

        Private Sub Done()
            Complete()

            If VersionId Is Nothing Then
                _Done(New Output(States.Complete, Nothing), Nothing, Nothing)
            Else
                Dim Reverts As New List(Of Edit)

                For Each Item As String In RevertIds
                    Reverts.Add(Edit.All(Item))
                Next Item

                _Done(New Output(States.Complete, Nothing), Edit.All(VersionId), Reverts)
            End If
        End Sub

        Private Sub Failed()
            Fail()
            _Done(New Output(States.Failed, Nothing), Nothing, Nothing)
        End Sub

    End Class

End Namespace