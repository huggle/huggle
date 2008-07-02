Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Module ReadRequests

    Class BlockLogRequest

        Public ThisUser As User, Target As ListView

        Public Sub Start()
            If ThisUser.BlocksCurrent Then
                Done(Nothing)
                Exit Sub
            End If

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim LogMatches As MatchCollection
            Dim Result As String = GetText(SitePath & _
                "w/api.php?format=xml&action=query&list=logevents&letype=block&letitle=User:" & _
                UrlEncode(ThisUser.Name) & "&lelimit=50", "<logevents").Replace(vbLf, "")

            LogMatches = New Regex("<item logid=""[0-9]+"" pageid=""[0-9]+"" ns=""2"" title=""User:[^""]+"" " & _
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

            ThisUser.BlocksCurrent = True
            Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
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
        End Sub

        Private Sub Failed(ByVal O As Object)
            If Target IsNot Nothing AndAlso Target.Items.Count > 0 _
                Then Target.Items(0).Text = "Failed to retrieve block log."
        End Sub

    End Class

    Class DiffRequest

        Public ThisEdit As Edit, Tab As BrowserTab

        Public Sub Start()
            If Tab Is Nothing Then Tab = CurrentTab
            ThisEdit.CacheState = CacheState.Caching
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Client As New WebClient
            Dim Retries As Integer = 3, Result As String = ""

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.GetProxy

                If Retries < 3 Then Thread.Sleep(1000)
                Retries -= 1

                Dim Oldid As String = ThisEdit.Oldid
                If Oldid = "-1" Then Oldid = "prev"

                'Using &action=render here would make things far simpler; unfortunately, that was broken for
                'image diff pages in 2007 and it would appear that nobody cares.

                Try
                    Result = UTF8.GetString(Client.DownloadData _
                        (SitePath & "w/index.php?title=" & UrlEncode(ThisEdit.Page.Name) _
                        & "&diff=" & ThisEdit.Id & "&oldid=" & Oldid & "&diffonly=1&uselang=en"))
                Catch ex As Exception
                End Try

            Loop Until ((Result.Contains("<table ") OrElse Result.Contains("<div class=""firstrevisionheader""") _
                OrElse Result.Contains("<div id=""mw-missingarticle"">")) _
                AndAlso Not Result.Contains("<div id=""mw-missingarticle"">")) OrElse Retries = 0

            If Result.Contains("<div id=""mw-missingarticle"">") Then
                Callback(AddressOf Deleted)
                Exit Sub
            ElseIf Retries = 0 Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Dim CacheData As New CacheData
            CacheData.Edit = ThisEdit

            If Result.Contains(">undo</a>)") Then
                Dim Result2 As String = Result.Substring(0, Result.IndexOf(">undo</a>)"))
                Result2 = Result2.Substring(0, Result2.LastIndexOf("(<a") - 1)
                Result = Result2 & Result.Substring(Result.IndexOf(">undo</a>") + 10)
            End If

            If Result.Contains("<table class='diff'>") Then
                Result = Result.Substring(Result.IndexOf("<table class='diff'>"))
                Result = Result.Substring(0, Result.IndexOf("</table>") + 8)

                'Style the diffs; by default, as done in monobook
                Result = "<style type=""text/css"">" & " table.diff {font-size: " & Config.DiffFontSize & "pt} " & _
                    DiffCss & "</style>" & Result

                Result = "<div style=""font-size: 160%; font-family: Arial"">" & ThisEdit.Page.Name _
                    & "</div>" & Result

                'Add the change size in the top-right corner
                If ThisEdit.Size <> 0 Then
                    Dim EditChange As String = CStr(ThisEdit.Size)
                    If ThisEdit.Size > 0 Then EditChange = "+" & EditChange

                    Result = "<div style=""float: right; font-size: 140%; font-family: Arial"">" & EditChange _
                        & "</div>" & Result
                End If

                CacheData.Text = Result
                Callback(AddressOf Done, CObj(CacheData))

            ElseIf Result.Contains("<div class=""firstrevisionheader""") Then
                'This is the first revision to the page... so no diff
                Callback(AddressOf ReachedEnd)

            Else
                Callback(AddressOf Failed)
            End If
        End Sub

        Private Sub Done(ByVal CacheDataObject As Object)
            ProcessDiff(CacheDataObject, Tab)
        End Sub

        Private Sub ReachedEnd(ByVal O As Object)
            'Mark this as the start of the history
            ThisEdit.Prev = NullEdit
            ThisEdit.Oldid = "-1"
            ThisEdit.CacheState = CacheState.Cached

            If LatestDiffRequest Is Me Then
                Main.HistoryPrevB.Enabled = False
                If ThisEdit.Next IsNot Nothing Then Tab.Edit = ThisEdit.Next
                DisplayEdit(CurrentEdit, False, Tab)
            End If
        End Sub

        Private Sub Deleted(ByVal O As Object)
            If LatestDiffRequest Is Me Then CurrentTab.Browser.DocumentText = _
                "<div style=""font-family: Arial"">" & _
                "This revision has been deleted, never existed, or exists " & _
                "but the server handling the request does not know it yet.</div>"
        End Sub

        Private Sub Failed(ByVal O As Object)
            ThisEdit.CacheState = CacheState.Uncached
        End Sub

    End Class

    Class HistoryRequest

        'Fetch page history

        Public ThisPage As Page, BlockSize As Integer
        Public DisplayWhenDone As Boolean
        Private _Done As CallbackDelegate

        Public Sub Start(Optional ByVal Done As CallbackDelegate = Nothing)
            _Done = Done
            BlockSize = HistoryBlockSize
            If ThisPage.HistoryOffset Is Nothing Then ThisPage.HistoryOffset = ""

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Client As New WebClient
            Dim Retries As Integer = 3, Result As String = ""

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.GetProxy

                Retries -= 1

                Dim QueryString As String = SitePath & "w/api.php?format=xml&action=query" & _
                    "&prop=revisions&titles=" & UrlEncode(ThisPage.Name) & "&rvlimit=" & _
                    CStr(HistoryBlockSize) & "&rvprop=ids|timestamp|user|comment"

                If ThisPage.HistoryOffset <> "" Then QueryString &= "&rvstartid=" & ThisPage.HistoryOffset

                Result = UTF8.GetString(Client.DownloadData(QueryString))

            Loop Until Result.Contains("<revisions") OrElse Result.Contains("missing="""" />") _
                OrElse Result.Contains("invalid="""" />") OrElse Retries = 0

            If Result.Contains("<revisions") Then
                Callback(AddressOf Done, CObj(Result))
            ElseIf Result.Contains("missing="""" />") Then
                Callback(AddressOf NoPage)
            ElseIf Result.Contains("invalid="""" />") Then
                Callback(AddressOf BadTitle)
            Else
                Callback(AddressOf Failed)
            End If
        End Sub

        Private Sub Done(ByVal ResultObject As Object)
            ProcessHistory(CStr(ResultObject), ThisPage)
            Main.DrawHistory()

            If DisplayWhenDone AndAlso ThisPage.LastEdit IsNot Nothing Then
                DisplayEdit(ThisPage.LastEdit)

                If ThisPage.LastEdit.User.LastEdit Is Nothing Then
                    Dim NewContribsRequest As New ContribsRequest
                    NewContribsRequest.ThisUser = ThisPage.LastEdit.User
                    NewContribsRequest.Start()
                End If
            End If

            If _Done IsNot Nothing Then _Done(True)
        End Sub

        Private Sub BadTitle(ByVal O As Object)
            Main.PageB.ForeColor = Color.Red
            If _Done IsNot Nothing Then _Done(False)
        End Sub

        Private Sub NoPage(ByVal O As Object)
            ThisPage.LastEdit = NullEdit
            Main.PageB.ForeColor = Color.Red
            If _Done IsNot Nothing Then _Done(False)
        End Sub

        Private Sub Failed(ByVal O As Object)
            If _Done IsNot Nothing Then _Done(False)
        End Sub

    End Class

    Class ContribsRequest

        'Fetch user contributions

        Public ThisUser As User
        Public DisplayWhenDone As Boolean
        Public ReportWhenDone As AIVReportRequest
        Public BlockSize As Integer = ContribsBlockSize
        Private _Done As CallbackDelegate

        Public Sub Start(Optional ByVal Done As CallbackDelegate = Nothing)
            If ThisUser IsNot Nothing Then
                If Done IsNot Nothing Then _Done = Done
                If ThisUser.ContribsOffset Is Nothing Then ThisUser.ContribsOffset = ""

                Dim RequestThread As New Thread(AddressOf Process)
                RequestThread.IsBackground = True
                RequestThread.Start()
            End If
        End Sub

        Private Sub Process()
            Dim Client As New WebClient

            Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
            Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
            Client.Proxy = Login.GetProxy

            Dim Retries As Integer = 3, Result As String = ""

            Do
                Retries -= 1

                Result = UTF8.GetString(Client.DownloadData(SitePath & _
                    "w/api.php?format=xml&action=query&list=usercontribs&ucuser=" & UrlEncode(ThisUser.Name) & _
                    "&uclimit=" & CStr(BlockSize) & "&ucstart=" & ThisUser.ContribsOffset))

            Loop Until Result.Contains("<usercontribs") OrElse Retries = 0

            If Result.Contains("<usercontribs />") Then
                Callback(AddressOf NoContribs)
            ElseIf Result.Contains("<usercontribs") Then
                Callback(AddressOf Done, CObj(Result))
            Else
                Callback(AddressOf Failed)
            End If
        End Sub

        Private Sub Done(ByVal ResultObject As Object)
            ProcessContribs(CStr(ResultObject), ThisUser)
            Main.DrawContribs()

            If DisplayWhenDone AndAlso ThisUser.LastEdit IsNot Nothing Then
                DisplayEdit(ThisUser.LastEdit)

                If ThisUser.LastEdit.Page.LastEdit Is Nothing Then
                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.ThisPage = ThisUser.LastEdit.Page
                    NewHistoryRequest.Start()
                End If
            End If

            If ReportWhenDone IsNot Nothing Then ReportWhenDone.Start()
            If _Done IsNot Nothing Then _Done(True)
        End Sub

        Private Sub NoContribs(ByVal O As Object)
            ThisUser.LastEdit = NullEdit
            Main.UserB.ForeColor = Color.Red
            If _Done IsNot Nothing Then _Done(True)
        End Sub

        Private Sub Failed(ByVal O As Object)
            If _Done IsNot Nothing Then _Done(False)
        End Sub

    End Class

    Class BrowserRequest

        Public Url As String, Tab As BrowserTab, HistoryItem As HistoryItem

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Client As New WebClient, Retries As Integer = 3, Result As String = Nothing

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Proxy = Login.GetProxy

                Retries -= 1

                Try
                    Result = UTF8.GetString(Client.DownloadData(Url))
                Catch ex As Exception
                End Try

            Loop Until Result IsNot Nothing OrElse Retries = 0

            If Retries > 0 Then Callback(AddressOf Done, CObj(Result))
        End Sub

        Private Sub Done(ByVal ResultObject As Object)
            Dim Result As String = CStr(ResultObject)

            Dim PageName As String = ParseUrl(Url)("title")
            Main.PageB.Text = PageName
            Result = FormatPageHtml(GetPage(PageName), Result)
            Tab.Browser.DocumentText = Result
            Tab.CurrentUrl = Url
            If HistoryItem IsNot Nothing Then HistoryItem.Text = Result
        End Sub

    End Class

    Class RcApiRequest

        'Get recent changes through the API, if IRC mode not enabled
        'Bearable, but IRC is better

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Result As String = GetText(SitePath & "w/api.php?action=query&format=xml&list=recentchanges" & _
                "&rclimit=" & CStr(Config.RcBlockSize) & "&rcprop=user|comment|flags|timestamp|title|ids|sizes", _
                "<recentchanges>")
            
            If Result IsNot Nothing Then Callback(AddressOf Done, CObj(Result))
        End Sub

        Private Sub Done(ByVal ResultObject As Object)
            Dim Result As String = CStr(ResultObject)
            ProcessRcApi(Result)
        End Sub

    End Class

    Class BlockApiRequest

        'Get blocks through the API, if IRC mode not enabled

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Result As String = GetText(SitePath & "w/api.php?action=query&format=xml&list=blocks&bklimit=50&bkstart=" & Date.UtcNow.Year & _
                    CStr(Date.UtcNow.Month).PadLeft(2, "0"c) & CStr(Date.UtcNow.Day).PadLeft(2, "0"c) & _
                    CStr(Date.UtcNow.Hour).PadLeft(2, "0"c) & CStr(Date.UtcNow.Minute).PadLeft(2, "0"c) & _
                    CStr(Date.UtcNow.Second).PadLeft(2, "0"c), "<blocks")

            If Result IsNot Nothing Then Callback(AddressOf Done, CObj(Result))
        End Sub

        Private Sub Done(ByVal ResultObject As Object)
            Dim Result As String = CStr(ResultObject)

            Dim BlockMatches As MatchCollection = Regex.Matches(Result, "<block id=""[0-9]+"" user=""([^""]+)""", _
                RegexOptions.Compiled)

            For Each Item As Match In BlockMatches
                Dim BlockedUser As User = GetUser(Item.Groups(1).Value)
                BlockedUser.BlocksCurrent = False
                If BlockedUser.Level <> UserL.Blocked Then BlockedUser.Level = UserL.Blocked
            Next Item

            Main.BlockReqTimer.Start()
        End Sub

    End Class

    Class CountRequest

        'Accepts a list of users, gets their edit counts
        'whitelists any with more than WhitelistEditCount edits

        Public Users As New List(Of User)

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim GetString As String = ""

            For Each Item As User In Users
                GetString &= UrlEncode(Item.Name) & "|"
            Next Item

            GetString = GetString.Substring(0, GetString.Length - 1)

            Dim Result As String = GetText(SitePath & _
                "w/api.php?format=xml&action=query&list=users&usprop=editcount&ususers=" & GetString, "<users>")

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
                    Item.Key.Level = UserL.Ignore
                    WhitelistChanged = True

                    If CurrentEdit IsNot Nothing AndAlso Item.Key Is CurrentEdit.User Then
                        Main.UserIgnoreB.Image = My.Resources.user_unwhitelist
                    End If

                    Dim i As Integer = 0

                    While i < EditQueue.Count - 1
                        If EditQueue(i).User Is Item.Key Then EditQueue.RemoveAt(i) Else i += 1
                    End While
                End If
            Next Item
        End Sub

        Private Sub Failed(ByVal O As Object)
        End Sub

    End Class

    Class WatchRequest

        'Adds page to your watchlist

        Public ThisPage As New Page, Manual As Boolean

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Result As String = GetText(SitePath & "w/index.php?title=" & _
                UrlEncode(ThisPage.Name) & "&action=watch", "<h1 class=""firstHeading"">Added to watchlist</h1>")

            If Result IsNot Nothing Then Callback(AddressOf Done, Nothing) Else Callback(AddressOf Failed, Nothing)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Manual Then Log("Added '" & ThisPage.Name & "' to watchlist")
            If Not Watchlist.Contains(SubjectPage(ThisPage)) Then Watchlist.Add(SubjectPage(ThisPage))
            If CurrentEdit IsNot Nothing AndAlso ThisPage Is CurrentEdit.Page Then Main.UpdateWatchButton()
        End Sub

        Private Sub Failed(ByVal O As Object)
        End Sub

    End Class

    Class UnwatchRequest

        'Removes page from your watchlist

        Public ThisPage As New Page, Manual As Boolean

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Result As String = GetText(SitePath & "w/index.php?title=" & _
                UrlEncode(ThisPage.Name) & "&action=unwatch", "<h1 class=""firstHeading"">Removed from watchlist</h1>")

            If Result IsNot Nothing Then Callback(AddressOf Done, Nothing) Else Callback(AddressOf Failed, Nothing)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Manual Then Log("Removed '" & ThisPage.Name & "' from watchlist")
            If Watchlist.Contains(SubjectPage(ThisPage)) Then Watchlist.Remove(SubjectPage(ThisPage))
            If CurrentEdit IsNot Nothing AndAlso ThisPage Is CurrentEdit.Page Then Main.UpdateWatchButton()
        End Sub

        Private Sub Failed(ByVal O As Object)
        End Sub

    End Class

    Class WarningLogRequest

        Public ThisUser As User, Target As ListView
        Private Result As String

        Public Sub Start()
            If ThisUser.WarningsCurrent Then
                Done(Nothing)
                Exit Sub
            End If

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Result = GetText(SitePath & _
                "w/api.php?action=query&format=xml&prop=revisions&rvprop=content&titles=User talk:" & _
                UrlEncode(ThisUser.Name), "<revisions><rev>")

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Result = Result.Substring(Result.IndexOf("<revisions><rev>") + 16)
            Result = Result.Substring(0, Result.IndexOf("</rev>"))
            Result = HtmlDecode(Result)

            ThisUser.Warnings = ProcessUserTalk(Result, ThisUser)
            If ThisUser.Warnings IsNot Nothing Then ThisUser.Warnings.Sort(AddressOf SortWarningsByDate)
            ThisUser.WarningsCurrent = True

            Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
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
                            Case UserL.Notification : NewItem.SubItems.Add("--")
                            Case UserL.Warn1 : NewItem.SubItems.Add("Level 1")
                            Case UserL.Warn2 : NewItem.SubItems.Add("Level 2")
                            Case UserL.Warn3 : NewItem.SubItems.Add("Level 3")
                            Case UserL.Warn4im : NewItem.SubItems.Add("Level 4im")
                            Case UserL.WarnFinal : NewItem.SubItems.Add("Level 4")
                            Case UserL.Blocked : NewItem.SubItems.Add("Blocked")
                            Case Else : NewItem.SubItems.Add("--")
                        End Select

                        NewItem.SubItems.Add(Warning.Type)

                        If Warning.User IsNot Nothing Then NewItem.SubItems.Add(Warning.User.Name) _
                            Else NewItem.SubItems.Add("?")

                        Target.Items.Add(NewItem)
                    Next Warning
                End If
            End If
        End Sub

        Private Sub Failed(ByVal O As Object)
            If Target IsNot Nothing AndAlso Target.Items.Count > 0 Then
                Target.Items(0).Text = "No warnings for this user."
                ThisUser.WarningsCurrent = True
            End If
        End Sub

    End Class

    Class GetTextRequest

        'Get text of page

        Public Page As Page
        Public Delegate Sub GetTextCallback(ByVal Result As Boolean, ByVal Text As String)
        Private _Done As GetTextCallback

        Public Sub Start(Optional ByVal Done As GetTextCallback = Nothing)
            _Done = Done

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Result As String = GetText(Page)
            If Result IsNot Nothing Then Result = Result.Replace(vbLf, vbCrLf)
            Callback(AddressOf Done, CObj(Result))
        End Sub

        Private Sub Done(ByVal ResultObject As Object)
            If _Done IsNot Nothing Then _Done((ResultObject IsNot Nothing), CStr(ResultObject))
        End Sub

    End Class

    Class PreviewRequest

        'Get preview
        Public Page As Page
        Public Text As String
        Public Delegate Sub PreviewCallback(ByVal Result As Boolean, ByVal Text As String)
        Public _Done As PreviewCallback

        Public Sub Start(Optional ByVal Done As PreviewCallback = Nothing)
            _Done = Done

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Result As String = PostData(SitePath & "w/index.php?title=" & UrlEncode(Page.Name.Replace(" ", "_")) & _
                "&action=submit", "wpTextbox1=" & UrlEncode(Text) & "&wpPreview=1&live=1")

            If Result IsNot Nothing Then
                Result = Result.Substring(Result.IndexOf("<preview>") + 9)
                Result = Result.Substring(0, Result.IndexOf("</preview>"))
                Result = HtmlDecode(Result)
                Result = Result.Substring(Result.IndexOf("</div>") + 6)
            End If

            Callback(AddressOf Done, CObj(Result))
        End Sub

        Private Sub Done(ByVal ResultObject As Object)
            If _Done IsNot Nothing Then _Done((ResultObject IsNot Nothing), CStr(ResultObject))
        End Sub

    End Class

    Class PreviewDiffRequest

        'Get preview of changes
        Public Page As Page
        Public Text As String
        Public Delegate Sub PreviewDiffCallback(ByVal Result As Boolean, ByVal Text As String)
        Public _Done As PreviewDiffCallback

        Public Sub Start(Optional ByVal Done As PreviewDiffCallback = Nothing)
            _Done = Done

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Result As String = PostData(SitePath & "w/index.php?title=" & UrlEncode(Page.Name.Replace(" ", "_")) & _
                "&action=submit", "wpTextbox1=" & UrlEncode(Text) & "&wpDiff=1")
            Callback(AddressOf Done, CObj(Result))
        End Sub

        Private Sub Done(ByVal ResultObject As Object)
            If _Done IsNot Nothing Then _Done((ResultObject IsNot Nothing), CStr(ResultObject))
        End Sub

    End Class

    Class ProtectionLogRequest

        Public ThisPage As Page, Target As ListView

        Public Sub Start()
            If ThisPage.ProtectionsCurrent Then
                Done(Nothing)
                Exit Sub
            End If

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim LogMatches As MatchCollection
            Dim Result As String = GetText(SitePath & "w/api.php?format=xml&action=query&list=logevents" & _
                "&letype=protect&lelimit=100&letitle=" & UrlEncode(ThisPage.Name), "<logevents").Replace(vbLf, "")

            LogMatches = New Regex("<item logid=""[0-9]+"" pageid=""[0-9]+"" ns=""[0-9]+"" title=""[^""]+"" " & _
                "type=""protect"" action=""(protect|unprotect|modify)"" user=""([^""]+)"" timestamp=""([^""]+)"" " & _
                "comment=""([^""\[]+)?(?:\[(?:edit=(sysop|autoconfirmed))?:?(?:move=(sysop|autoconfirmed))?\])?(?: " & _
                "\[(cascading)\])?(?: \(expires ([^\(]+)\(UTC\)\))?"" />", RegexOptions.Compiled).Matches(Result)

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If ThisPage.Protections IsNot Nothing Then ThisPage.Protections.Clear()
            ThisPage.ProtectionsCurrent = True

            If LogMatches.Count > 0 Then
                If ThisPage.Protections Is Nothing Then ThisPage.Protections = New List(Of Protection)

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

                    If ThisPage.Protections Is Nothing Then ThisPage.Protections = New List(Of Protection)
                    ThisPage.Protections.Add(NewProtection)
                Next Item
            End If

            If ThisPage.Protections IsNot Nothing AndAlso (ThisPage.Protections(0).Expiry = Date.MinValue _
                OrElse ThisPage.Protections(0).Expiry > Date.UtcNow) Then

                ThisPage.EditLevel = ThisPage.Protections(0).EditLevel
                ThisPage.MoveLevel = ThisPage.Protections(0).MoveLevel
            End If

            Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Target IsNot Nothing Then
                If ThisPage.Protections Is Nothing OrElse ThisPage.Protections.Count = 0 Then
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

                    For Each Item As Protection In ThisPage.Protections
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
        End Sub

        Private Sub Failed(ByVal O As Object)
            If Target IsNot Nothing AndAlso Target.Items.Count > 0 _
                Then Target.Items(0).Text = "Failed to retrieve protection log."
        End Sub

    End Class

    Class DeleteLogRequest

        Public ThisPage As Page, Target As ListView

        Public Sub Start()
            If ThisPage.DeletesCurrent Then
                Done(Nothing)
                Exit Sub
            End If

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim LogMatches As MatchCollection
            Dim Result As String = GetText(SitePath & "w/api.php?format=xml&action=query&list=logevents" & _
                "&letype=delete&lelimit=50&letitle=" & UrlEncode(ThisPage.Name), "<logevents").Replace(vbLf, "")

            LogMatches = New Regex("<item logid=""0"" pageid=""[0-9]+"" ns=""[0-9]+"" title=""[^""]+"" " & _
                "type=""delete"" action=""(delete|undelete)"" user=""([^""]+)"" timestamp=""([^""]+)"" " & _
                "comment=""([^""]+)"" />", RegexOptions.Compiled).Matches(Result)

            If Result Is Nothing Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If ThisPage.Deletes IsNot Nothing Then ThisPage.Deletes.Clear()
            ThisPage.DeletesCurrent = True

            If LogMatches.Count > 0 Then
                If ThisPage.Deletes Is Nothing Then ThisPage.Deletes = New List(Of Delete)

                For Each Item As Match In LogMatches
                    Dim NewDelete As New Delete

                    NewDelete.Time = CDate(Item.Groups(3).Value)
                    NewDelete.Action = HtmlDecode(Item.Groups(1).Value)
                    NewDelete.Admin = GetUser(HtmlDecode(Item.Groups(2).Value))
                    NewDelete.Comment = HtmlDecode(Item.Groups(4).Value)

                    If ThisPage.Deletes Is Nothing Then ThisPage.Deletes = New List(Of Delete)
                    ThisPage.Deletes.Add(NewDelete)
                Next Item
            End If

            Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Target IsNot Nothing Then
                If ThisPage.Deletes Is Nothing OrElse ThisPage.Deletes.Count = 0 Then
                    Target.Items(0).Text = "No deletion log for this page."
                Else
                    Target.Items.Clear()
                    Target.Columns.Clear()
                    Target.Columns.Add("Date", 110)
                    Target.Columns.Add("Action", 50)
                    Target.Columns.Add("Admin", 100)
                    Target.Columns.Add("Comment", 200)

                    For Each Item As Delete In ThisPage.Deletes
                        Dim NewListViewItem As New ListViewItem

                        NewListViewItem.Text = Item.Time.ToShortDateString & " " & Item.Time.ToShortTimeString
                        NewListViewItem.SubItems.Add(Item.Action)
                        NewListViewItem.SubItems.Add(Item.Admin.Name)
                        NewListViewItem.SubItems.Add(TrimSummary(Item.Comment))

                        Target.Items.Add(NewListViewItem)
                    Next Item
                End If
            End If
        End Sub

        Private Sub Failed(ByVal O As Object)
            If Target IsNot Nothing AndAlso Target.Items.Count > 0 _
                Then Target.Items(0).Text = "Failed to retrieve deletion log."
        End Sub

    End Class

    Class CategoryRequest

        'Get the contents of a category

        Public Delegate Sub CategoryRequestCallback(ByVal Result As List(Of String))
        Public Category As String
        Private _Done As CategoryRequestCallback, Items As New List(Of String)

        Public Sub Start(ByVal Done As CategoryRequestCallback)
            _Done = Done

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim ContinueFrom As String = Nothing, Calls As Integer = 0

            Do
                Calls += 1

                Dim RequestString As String = Config.SitePath & "w/api.php?action=query&format=xml" & _
                    "&list=categorymembers&cmlimit=" & CStr(ApiLimit()) & "&cmprop=title&cmtitle=" & _
                    UrlEncode("Category:" & Category)
                If ContinueFrom IsNot Nothing Then RequestString &= "&cmcontinue=" & ContinueFrom

                Dim Result As String = GetText(RequestString, "<query>")

                If Result Is Nothing Then
                    Callback(AddressOf Failed)
                    Exit Sub

                ElseIf Result.Contains("<categorymembers />") Then
                    Callback(AddressOf Done)
                    Exit Sub
                End If

                If Result.Contains("<query-continue>") Then
                    ContinueFrom = Result.Substring(Result.IndexOf("cmcontinue=""") + 12)
                    ContinueFrom = ContinueFrom.Substring(0, ContinueFrom.IndexOf(""""))
                    ContinueFrom = HtmlDecode(ContinueFrom)
                Else
                    ContinueFrom = Nothing
                End If

                Result = Result.Substring(Result.IndexOf("<categorymembers>") + 17)
                Result = Result.Substring(0, Result.IndexOf("</categorymembers>"))

                For Each Item As String In Result.Split(New String() {"<cm"}, StringSplitOptions.RemoveEmptyEntries)
                    If Item.Contains("title=""") Then
                        Item = Item.Substring(Item.IndexOf("title=""") + 7)
                        Item = Item.Substring(0, Item.IndexOf(""""))
                        Item = HtmlDecode(Item)
                        Items.Add(Item)
                    End If
                Next Item
            Loop Until ContinueFrom Is Nothing OrElse Calls >= Config.QueueBuilderLimit

            Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            _Done(Items)
        End Sub

        Private Sub Failed(ByVal O As Object)
            _Done(Nothing)
        End Sub

    End Class

    Class PurgeRequest

        'Purge a page

        Public Page As Page

        Public Sub Start()
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Result As String = GetText(Config.SitePath & "w/index.php?title=" & UrlEncode(Page.Name) & _
                "&action=purge")

            If Not IsWikiPage(Result) Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            Log("Purged '" & Page.Name & "'")
        End Sub

        Private Sub Failed(ByVal O As Object)
            Log("Failed to purge '" & Page.Name & "'")
        End Sub

    End Class

End Module