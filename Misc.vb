Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Module Misc

    'Globals

    Public Administrator As Boolean
    Public AllEditsById As New Dictionary(Of String, Edit)
    Public AllEditsByTime As New List(Of Edit)
    Public AllPages As New Dictionary(Of String, Page)
    Public AllRequests As New List(Of Request)
    Public AllUsers As New Dictionary(Of String, User)
    Public ContribsOffset As Integer
    Public Cookie As String
    Public CurrentQueue As New List(Of Edit)
    Public CurrentTab As BrowserTab
    Public DiffCache As New Dictionary(Of String, String)
    Public EditQueue As New List(Of Edit), NewPageQueue As New List(Of Edit)
    Public HidingEdit As Boolean = True
    Public HistoryOffset As Integer
    Public LastTagText As String = ""
    Public LatestDiffRequest As DiffRequest
    Public ManualRevertSummaries As New List(Of String)
    Public MyUser As User
    Public NextCount As New List(Of User)
    Public NullEdit As New Edit
    Public PendingRequests As New List(Of Request)
    Public PendingWarnings As New List(Of Edit)
    Public QueueSources As New Dictionary(Of String, List(Of String))
    Public RollbackAvailable As Boolean
    Public SpeedyCriteria As New Dictionary(Of String, SpeedyCriterion)
    Public StartTime As Date
    Public SyncContext As Threading.SynchronizationContext
    Public Undo As New List(Of Command)
    Public VersionOK As Boolean
    Public WarningMessages As New Dictionary(Of String, String)
    Public Watchlist As New List(Of Page)
    Public Whitelist As New List(Of String)
    Public WhitelistChanged As Boolean
    Public WhitelistLoaded As Boolean

    Public Delegate Sub CallbackDelegate(ByVal Success As Boolean)

    Class Command
        Public Description As String
        Public Type As CommandType
        Public Edit As Edit
        Public Page As Page
        Public User As User
    End Class

    Public Enum CommandType As Integer
        Revert
        Report
        Warning
        Edit
        Ignore
        Unignore
    End Enum

    Class SpeedyCriterion
        Public Code As String
        Public DisplayCode As String
        Public Description As String
        Public Template As String
        Public Parameter As String
        Public Message As String
        Public Notify As Boolean
    End Class

    Public Property CurrentEdit() As Edit
        <DebuggerStepThrough()> Get
            If CurrentTab Is Nothing Then CurrentTab = CType(Main.Tabs.TabPages(0).Controls(0), BrowserTab)
            Return CurrentTab.Edit
        End Get

        Set(ByVal value As Edit)
            If CurrentTab IsNot Nothing Then CurrentTab.Edit = value
        End Set
    End Property

    Public ReadOnly Property CurrentUser() As User
        <DebuggerStepThrough()> Get
            If CurrentEdit Is Nothing Then Return Nothing Else Return CurrentEdit.User
        End Get
    End Property

    Public ReadOnly Property CurrentPage() As Page
        <DebuggerStepThrough()> Get
            If CurrentEdit Is Nothing Then Return Nothing Else Return CurrentEdit.Page
        End Get
    End Property

    Public RevertSummaries() As String = _
    { _
        "[[wp:undo|undid]]", _
        "undid", _
        "bot - rv", _
        "bot - revert", _
        "bot--revert", _
        "revert", _
        "rv", _
        "js: revert", _
        "automatically reverting" _
    }

    Public SharedIPTemplates() As String = _
    { _
        "sharedip", _
        "shared ip", _
        "publicip", _
        "ipowner", _
        "vandalip", _
        "sharedipedu", _
        "schoolip", _
        "school ip", _
        "sharedunknownedu", _
        "sharedipcert", _
        "sharedippublic", _
        "sharedip us military", _
        "ipedu", _
        "aberwebcacheipaddress" _
    }

    Public Namespaces() As String = _
    { _
        "Talk", _
        "Wikipedia", _
        "Wikipedia talk", _
        "Category", _
        "Category talk", _
        "Template", _
        "Template talk", _
        "Image", _
        "Image talk", _
        "User", _
        "User talk", _
        "Portal", _
        "Portal talk", _
        "Help", _
        "Help talk", _
        "MediaWiki", _
        "MediaWiki talk" _
    }

    Public ConfigNamespaces() As String = _
    { _
        "article", _
        "talk", _
        "wikipedia", _
        "wikipedia talk", _
        "category", _
        "category talk", _
        "template", _
        "template talk", _
        "image", _
        "image talk", _
        "user", _
        "user talk", _
        "portal", _
        "portal talk", _
        "help", _
        "help talk", _
        "mediawiki", _
        "mediawiki talk" _
    }

    <DebuggerDisplay("{Description}")> _
    Class Edit
        Public Added As Boolean
        Public CacheState As CacheState
        Public Deleted As Boolean
        Public Id As String
        Public LevelToWarn As UserL
        Public Multiple As Boolean
        Public NewPage As Boolean
        Public [Next] As Edit
        Public NextByUser As Edit
        Public Oldid As String
        Public Page As Page
        Public Prev As Edit
        Public PrevByUser As Edit
        Public Processed As Boolean
        Public Random As Double
        Public RollbackUrl As String
        Public Size As Integer
        Public Summary As String
        Public Time As Date
        Public Type As EditType
        Public TypeToWarn As String
        Public User As User
        Public WarningLevel As UserL

        Public ReadOnly Property Description() As String
            Get
                Return Time.ToLongTimeString & " -- " & Page.Name & " -- " & User.Name
            End Get
        End Property
    End Class

    <DebuggerDisplay("{Name}")> _
    Class Page
        Public Name As String
        Public FirstEdit As Edit
        Public LastEdit As Edit
        Public Level As PageL
        Public [Namespace] As String
        Public Deletes As List(Of Delete)
        Public DeletesCurrent As Boolean
        Public Protections As List(Of Protection)
        Public ProtectionsCurrent As Boolean
        Public HistoryOffset As String
        Public Patrolled As Boolean
        Public Rcid As String
        Public EditLevel As String
        Public MoveLevel As String

        Public ReadOnly Property IsMovable() As Boolean
            Get
                If ArrayContains(Config.UnmovableNamespaces, [Namespace]) Then Return False
                If MoveLevel = "sysop" AndAlso Not Administrator Then Return False
                If ArrayContains(Config.ProtectedNamespaces, [Namespace]) AndAlso Not Administrator Then Return False

                Return True
            End Get
        End Property

    End Class

    <DebuggerDisplay("{Name}")> _
    Class User
        Private _Level As UserL
        Private _SharedIP As Boolean
        Private _EditCount As Integer = -1
        Private _SessionEditCount As Integer = 0

        Public AutoRevert As Boolean
        Public Name As String
        Public FirstEdit As Edit
        Public LastEdit As Edit
        Public Anonymous As Boolean
        Public WarnTime As Date
        Public Warnings As List(Of Warning)
        Public WarningsCurrent As Boolean
        Public Blocks As List(Of Block)
        Public BlocksCurrent As Boolean
        Public Bot As Boolean
        Public ContribsOffset As String

        Public Property Level() As UserL
            <DebuggerStepThrough()> Get
                Return _Level
            End Get
            <DebuggerStepThrough()> Set(ByVal value As UserL)
                _Level = value

                Dim Redraw, Sort As Boolean

                If value > UserL.Ignore Then
                    Dim ThisEdit As Edit = LastEdit

                    While ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
                        If ThisEdit Is ThisEdit.Page.LastEdit AndAlso Not ThisEdit.Added _
                            AndAlso Not EditQueue.Contains(ThisEdit) Then

                            If ThisEdit.User.Level <> UserL.Ignore _
                                AndAlso (Config.ShowNewPages OrElse Not ThisEdit.NewPage) _
                                AndAlso ThisEdit.Page.Level <> PageL.Ignore _
                                AndAlso Not OwnUserspace(ThisEdit) _
                                AndAlso ThisEdit.Type >= EditType.None _
                                AndAlso Not Math.Abs(ThisEdit.Size) > 100000 Then

                                Dim LCSpace As String = ThisEdit.Page.Namespace.ToLower
                                If LCSpace = "" Then LCSpace = "article"

                                If Config.NamespacesChecked.Contains(LCSpace) Then
                                    EditQueue.Add(ThisEdit)
                                    ThisEdit.Added = True
                                    Main.DiffNextB.Enabled = True
                                    If EditQueue.Count > 5000 Then EditQueue.RemoveAt(5000)
                                    Redraw = True
                                    Sort = True
                                End If
                            End If
                        End If

                        ThisEdit = ThisEdit.PrevByUser
                    End While
                End If

                For Each Item As Form In Application.OpenForms
                    If TypeOf Item Is UserInfoForm AndAlso CType(Item, UserInfoForm).ThisUser Is Me _
                        Then If _Level = UserL.Ignore Then CType(Item, UserInfoForm).Whitelisted.Text = "Yes" _
                        Else CType(Item, UserInfoForm).Whitelisted.Text = "No"
                Next Item

                'Redraw contribs and queue if necessary
                If Main IsNot Nothing Then
                    If CurrentEdit IsNot Nothing AndAlso CurrentEdit.User Is Me Then Main.DrawContribs()

                    If Config.ShowQueue Then
                        For i As Integer = 0 To Math.Min(EditQueue.Count - 1, (Main.Queue.Height \ 20) - 2)
                            If EditQueue(i).User Is Me Then
                                Redraw = True
                                Exit For
                            End If
                        Next i
                    End If

                    If Redraw Then Main.DrawQueue()
                End If

                If Sort Then EditQueue.Sort(AddressOf CompareEdits)
            End Set
        End Property

        Public Property SharedIP() As Boolean
            Get
                Return _SharedIP
            End Get
            Set(ByVal value As Boolean)
                _SharedIP = value

                For Each Item As Form In Application.OpenForms
                    If TypeOf Item Is UserInfoForm AndAlso CType(Item, UserInfoForm).ThisUser Is Me _
                        Then If _SharedIP Then CType(Item, UserInfoForm).SharedIP.Text = "Yes" _
                        Else CType(Item, UserInfoForm).SharedIP.Text = "No"
                Next Item
            End Set
        End Property

        Public Property EditCount() As Integer
            Get
                Return _EditCount
            End Get
            Set(ByVal value As Integer)
                _EditCount = value

                For Each Item As Form In Application.OpenForms
                    If TypeOf Item Is UserInfoForm AndAlso CType(Item, UserInfoForm).ThisUser Is Me _
                        Then CType(Item, UserInfoForm).EditCount.Text = CStr(_EditCount)
                Next Item
            End Set
        End Property

        Public Property SessionEditCount() As Integer
            Get
                Return _SessionEditCount
            End Get
            Set(ByVal value As Integer)
                _SessionEditCount = value

                For Each Item As Form In Application.OpenForms
                    If TypeOf Item Is UserInfoForm AndAlso CType(Item, UserInfoForm).ThisUser Is Me _
                        Then CType(Item, UserInfoForm).SessionEditCount.Text = CStr(_SessionEditCount)
                Next Item
            End Set
        End Property

    End Class

    Class CacheData
        Public Edit As Edit
        Public Text As String
    End Class

    Class EditData
        Public Edit As Edit, Page As Page
        Public Text, Summary, StartTime, EditTime, Token, Section As String
        Public Result As String
        Public CaptchaId, CaptchaWord As String
        Public Minor, Watch, Creating As Boolean
        Public [Error] As Boolean
    End Class

    Class PageMove
        Public Time As Date
        Public User As User
        Public Source As String
        Public Destination As String
        Public Summary As String
    End Class

    Class Protection
        Public Time As Date
        Public Admin As User
        Public Action As String
        Public Page As Page
        Public EditLevel As String
        Public MoveLevel As String
        Public Cascading As Boolean
        Public Expiry As Date
        Public Summary As String
    End Class

    Class Upload
        Public Time As Date
        Public User As User
        Public File As Page
        Public Summary As String
    End Class

    Class Warning
        Public Level As UserL
        Public Time As Date
        Public Type As String
        Public User As User
    End Class

    Class Block
        Public Time As Date
        Public User As User
        Public Action As String
        Public Duration As String
        Public Options As String
        Public Admin As User
        Public Comment As String
    End Class

    Class Delete
        Public Time As Date
        Public Page As Page
        Public Action As String
        Public Admin As User
        Public Comment As String
    End Class

    Class HistoryItem

        Sub New(ByVal Edit As Edit)
            Me.Edit = Edit
        End Sub

        Sub New(ByVal Url As String)
            Me.Url = Url
        End Sub

        Public Edit As Edit
        Public Url As String
        Public Text As String

    End Class

    Enum PageL As Integer
        None = 0
        Ignore = -1
        Watch = 1
        Bad = 2
    End Enum

    Enum UserL As Integer
        None = 0
        Ignore = -1
        Notification = 1
        Reverted = 2
        ReportedUAA = 3
        Message = 4
        Warning = 5
        Warn1 = 6
        Warn2 = 7
        Warn3 = 8
        Warn4im = 9
        WarnFinal = 10
        ReportedAIV = 11
        Blocked = 12
    End Enum

    Enum EditType As Integer
        Blanked = 2
        ReplacedWith = 1
        None = 0
        Revert = -1
        Message = -2
        Tag = -3
        Warning = -4
        Report = -5
        Redirect = -6
    End Enum

    Enum CacheState As Integer
        Uncached
        Caching
        Cached
        Viewed
    End Enum

    Enum RemoveState As Integer
        None
        Removing
        Removed
    End Enum

    Enum ProtectionType As Integer
        Semi
        Full
        Move
    End Enum

    <DebuggerStepThrough()> Function GetPage(ByVal PageName As String) As Page
        If PageName = "" Then Return Nothing
        PageName = PageName.Replace("_", " ").Trim(" "c)
        If PageName.Contains("#") Then PageName = PageName.Substring(0, PageName.IndexOf("#"))
        If PageName.Length > 1 Then PageName = PageName.Substring(0, 1).ToUpper & PageName.Substring(1) _
            Else PageName = PageName.ToUpper

        If AllPages.ContainsKey(PageName) Then
            Return AllPages(PageName)
        Else
            Dim NewPage As New Page
            NewPage.Name = PageName

            For Each Item As String In Config.IgnoredPages
                If Item = PageName OrElse Item = TalkPageName(PageName) Then NewPage.Level = PageL.Ignore
            Next Item

            NewPage.Namespace = ""

            For Each Item As String In Namespaces
                If NewPage.Name.StartsWith(Item & ":") Then NewPage.Namespace = Item
            Next Item

            AllPages.Add(PageName, NewPage)
            Return NewPage
        End If
    End Function

    <DebuggerStepThrough()> Function GetUser(ByVal UserName As String) As User
        If UserName = "" Then Return Nothing
        UserName = UserName.Replace("_", " ").Trim(" "c)
        If UserName.Contains("#") Then UserName = UserName.Substring(0, UserName.IndexOf("#"))
        If UserName.Contains("/") Then UserName = UserName.Substring(0, UserName.IndexOf("/"))
        If UserName.Length > 1 Then UserName = UserName.Substring(0, 1).ToUpper & UserName.Substring(1) _
            Else UserName = UserName.ToUpper

        If AllUsers.ContainsKey(UserName) Then
            Return AllUsers(UserName)
        Else
            Dim NewUser As New User
            NewUser.Name = UserName

            'That regex there matches any IP address. Or so I'm told.

            If Regex.IsMatch(NewUser.Name, "((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}" & _
                "(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)", RegexOptions.Compiled) Then
                NewUser.Anonymous = True

            ElseIf Whitelist.Contains(NewUser.Name) Then
                NewUser.Level = UserL.Ignore
            End If

            AllUsers.Add(UserName, NewUser)

            Return NewUser
        End If
    End Function

    <DebuggerStepThrough()> Function OwnUserspace(ByVal NewEdit As Edit) As Boolean
        Dim Pagename As String = NewEdit.Page.Name

        If Pagename.Contains("/") Then Pagename = Pagename.Substring(0, Pagename.IndexOf("/"))

        Return (Pagename.StartsWith("User:") OrElse Pagename.StartsWith("User talk:") _
            AndAlso NewEdit.User.Name = Pagename.Substring(Pagename.IndexOf(":") + 1))
    End Function

    <DebuggerStepThrough()> Function StripHTML(ByVal Str As String) As String
        'Removes anything with < > around it
        While Str.Contains("<") AndAlso Str.Contains(">")
            Str = Str.Substring(0, Str.IndexOf("<")) & Str.Substring(Str.IndexOf(">") + 1)
        End While

        Return Str
    End Function

    <DebuggerStepThrough()> Function IsWikiPage(ByVal Text As String) As Boolean
        'Unfortunately there is no one element common to all skins
        If Text Is Nothing Then Return False
        Return Regex.Match(Text, "<div id=['""](mw[-_])?content['""]>").Success
    End Function

    <DebuggerStepThrough()> Function TalkPageName(ByVal PageName As String) As String
        Dim PageNamespace As String = ""

        For Each Item As String In Namespaces
            If PageName.StartsWith(Item & ":") Then PageNamespace = Item
        Next Item

        If PageNamespace = "" Then
            Return "Talk:" & PageName

        ElseIf Not PageNamespace.Contains(" talk") AndAlso Not PageNamespace = "Talk" Then
            Return PageNamespace & " talk" & PageName.Substring(PageName.IndexOf(":"))

        Else
            Return PageName
        End If
    End Function

    <DebuggerStepThrough()> Function SubjectPage(ByVal Page As Page) As Page
        If Page.Namespace = "Talk" AndAlso Page.Name.Contains(":") Then
            Return GetPage(Page.Name.Substring(Page.Name.IndexOf(":") + 1))

        ElseIf Page.Namespace.Contains(" talk") AndAlso Page.Name.Contains(":") Then
            Return GetPage(Page.Namespace.Substring(0, Page.Namespace.Length - 5) & _
                Page.Name.Substring(Page.Name.IndexOf(":")))

        Else
            Return Page
        End If
    End Function

    <DebuggerStepThrough()> Function GetMonthName(ByVal Number As Integer) As String
        'Yes, I know about MonthName(). But I have to localize.

        Select Case Config.Project
            Case "es.wikipedia"
                Select Case Number
                    Case 1 : Return "enero"
                    Case 2 : Return "febrero"
                    Case 3 : Return "marzo"
                    Case 4 : Return "abril"
                    Case 5 : Return "mayo"
                    Case 6 : Return "junio"
                    Case 7 : Return "julio"
                    Case 8 : Return "agosto"
                    Case 9 : Return "septiembre"
                    Case 10 : Return "octubre"
                    Case 11 : Return "noviembre"
                    Case 12 : Return "diciembre"
                    Case Else : Return ""
                End Select

            Case Else
                Select Case Number
                    Case 1 : Return "January"
                    Case 2 : Return "February"
                    Case 3 : Return "March"
                    Case 4 : Return "April"
                    Case 5 : Return "May"
                    Case 6 : Return "June"
                    Case 7 : Return "July"
                    Case 8 : Return "August"
                    Case 9 : Return "September"
                    Case 10 : Return "October"
                    Case 11 : Return "November"
                    Case 12 : Return "December"
                    Case Else : Return ""
                End Select
        End Select
    End Function

    <DebuggerStepThrough()> Function Ordinal(ByVal N As Integer) As String
        If (N Mod 100) \ 10 = 1 Then Return CStr(N) & "th"

        Select Case N Mod 10
            Case 1 : Return CStr(N) & "st"
            Case 2 : Return CStr(N) & "nd"
            Case 3 : Return CStr(N) & "rd"
            Case Else : Return CStr(N) & "th"
        End Select
    End Function

    <DebuggerStepThrough()> Function WikiUrl(ByVal Url As String) As Boolean
        Return (Url.StartsWith(SitePath & "w/index.php?") OrElse Url.StartsWith("wiki/"))
    End Function

    <DebuggerStepThrough()> Function ParseUrl(ByVal Url As String) As Dictionary(Of String, String)
        Dim Params As New Dictionary(Of String, String)

        If Url.Contains("wiki/") OrElse Url.Contains("w/index.php/") Then

            If Url.Contains("wiki/") Then Url = Url.Substring(Url.IndexOf("wiki/") + 5) _
                Else Url = Url.Substring(Url.IndexOf("w/index.php/") + 12)

            If Url.Contains("?") Then
                Dim Title As String = Url.Substring(0, Url.IndexOf("?"))

                If Title.Contains("#") Then Title = Title.Substring(0, Title.IndexOf("#"))
                Params.Add("title", UrlDecode(Title.Replace("_", " ")))
                Url = Url.Substring(Url.IndexOf("?") + 1)
            Else
                If Url.Contains("#") Then Url = Url.Substring(0, Url.IndexOf("#"))
                Params.Add("title", UrlDecode(Url.Replace("_", " ")))
                Url = ""
            End If

        ElseIf Url.Contains("w/index.php?") Then
            Url = Url.Substring(Url.IndexOf("w/index.php?") + 12)
        End If

        For Each Item As String In Url.Split("&"c)
            If Item.Contains("=") Then Params.Add(Item.Substring(0, Item.IndexOf("=")), _
                UrlDecode(Item.Substring(Item.IndexOf("=") + 1)))
        Next Item

        Return Params
    End Function

    <DebuggerStepThrough()> Function FormatPageHtml(ByVal Page As Page, ByVal Text As String) As String
        'We're only interested in the page here, not the site interface that comes with it.
        'Extract the actual page content and then put some headers back so that it renders properly.
        'No, you can't have your non-monobook skin. So what? It doesn't show the site interface anyway.

        If Text.Contains("<!-- start content -->") AndAlso Text.Contains("<!-- end content -->") Then
            Text = Text.Substring(Text.IndexOf("<!-- start content -->"))
            Text = Text.Substring(0, Text.IndexOf("<!-- end content -->"))

        ElseIf Text.Contains("<!-- content -->") AndAlso Text.Contains("<!-- mw_content -->") Then
            Text = Text.Substring(Text.IndexOf("<!-- content -->"))
            Text = Text.Substring(0, Text.IndexOf("<!-- mw_content -->"))

        ElseIf Text.Contains("</h1>") AndAlso Text.Contains("<div class=""printfooter"">") Then
            Text = Text.Substring(Text.IndexOf("</h1>") + 5)
            Text = Text.Substring(0, Text.IndexOf("<div class=""printfooter"">"))
        End If

        'Modern skin puts scripts in the middle of the page for some reason
        If Text.Contains("<script ") AndAlso Text.Contains("</script>") _
            Then Text = Text.Substring(0, Text.IndexOf("<script ")) & Text.Substring(Text.IndexOf("</script>") + 9)

        Text = "<h1>" & Page.Name & "</h1>" & Text

        Text = "<style type=""text/css""> * {background-image: none !important} " & _
            ".historysubmit {display: none}</style>" & vbCrLf & Text

        'Yes, the style version is out of date. It doesn't matter; nothing is cached between huggle sessions
        Text = "<style type=""text/css"">/*<![CDATA[*/" & vbCrLf & _
            "@import ""/w/index.php?title=MediaWiki:Common.css&usemsgcache=yes&action=raw&ctype=text/css " & _
            "&smaxage=2678400"";" & vbCrLf & "@import ""/w/index.php?title=MediaWiki:Monobook.css&usemsgcache=yes" & _
            "&action=raw&ctype=text/css&smaxage=2678400"";" & vbCrLf & "@import ""/w/index.php?title=-&action=raw" & _
            "&gen=css&maxage=2678400&smaxage=0&ts=20071017064247"";" & vbCrLf & _
            "@import ""/skins-1.5/common/shared.css?100"";" & vbCrLf & _
            "@import ""/skins-1.5/monobook/main.css?100"";" & vbCrLf & "/*]]>*/</style>" & Text

        Text = "<html><head><title>" & Page.Name & "</title></head><body>" & Text & "</body></html>"

        Text = Text.Replace("@import ""/skins-1.5/", "@import """ & SitePath & "skins-1.5/")
        Text = Text.Replace("@import ""/w/index.php?", "@import """ & SitePath & "w/index.php?")
        Text = Text.Replace("src=""/skins-1.5/", "src=""" & SitePath & "skins-1.5/")
        Return Text
    End Function

    <DebuggerStepThrough()> Sub Callback(ByVal Target As Threading.SendOrPostCallback, Optional ByVal PostData As Object = Nothing)
        SyncContext.Post(Target, PostData)
    End Sub

    <DebuggerStepThrough()> Sub Log(ByVal Message As String, Optional ByVal Tag As Object = Nothing)
        If Main IsNot Nothing Then Main.Log(Message, Tag)
    End Sub

    <DebuggerStepThrough()> Function TrimSummary(ByVal Summary As String) As String
        Summary = Summary.Replace(" (page does not exist)", "")

        While Summary.Contains("[[") AndAlso Summary.Contains("]]")
            Dim Title As String = Summary.Substring(Summary.IndexOf("[[") + 2)

            If Title.Contains("|") AndAlso (Not Title.Contains("]]") _
                OrElse Title.IndexOf("|") < Title.IndexOf("]]")) _
                Then Title = Title.Substring(Title.IndexOf("|") + 1)

            If Title.Contains("]]") Then Title = Title.Substring(0, Title.IndexOf("]]"))

            If Not Summary.Contains("]]") Then Exit While
            Summary = Summary.Substring(0, Summary.IndexOf("[[")) & Title & Summary.Substring(Summary.IndexOf("]]") + 2)
        End While

        Return Summary
    End Function

    <DebuggerStepThrough()> Function SortWarningsByDate(ByVal X As Warning, ByVal Y As Warning) As Integer
        Return Date.Compare(Y.Time, X.Time)
    End Function

    <DebuggerStepThrough()> Function ApiLimit() As Integer
        If Administrator Then Return 5000 Else Return 500
    End Function

    <DebuggerStepThrough()> Function ArrayContains(Of T)(ByVal Array As T(), ByVal Value As T) As Boolean
        For Each Item As T In Array
            If Item.Equals(Value) Then Return True
        Next Item

        Return False
    End Function

    Class Stats

        Public Shared Edits, EditsMe, Reverts, RevertsMe, Warnings, WarningsMe, Blocks, BlocksMe As Integer

    End Class

End Module

Class ListView2

    Inherits ListView

    Sub New()
        DoubleBuffered = True
    End Sub

End Class