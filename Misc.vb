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
Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Module Misc

    'Globals

    Public AllLists As New Dictionary(Of String, List(Of String))
    Public AllRequests As New List(Of Request)
    Public Config As Configuration
    Public CustomReverts As New Dictionary(Of Page, String)
    Public CurrentQueue As Queue
    Public CurrentTab As BrowserTab
    Public EditToken As String
    Public HidingEdit As Boolean = True
    Public LastRcTime As Date
    Public LastTagText As String = ""
    Public LatestDiffRequest As DiffRequest
    Public LogBuffer As New List(Of String)
    Public MainForm As Main
    Public NextCount As New List(Of User)
    Public NullEdit As New Edit
    Public PatrolToken As String
    Public PendingRequests As New List(Of Request)
    Public PendingWarnings As New List(Of Edit)
    Public QueueNames As New Dictionary(Of String, List(Of String))
    Public SecondQueue As Queue
    Public StartTime As Date
    Public SyncContext As Threading.SynchronizationContext
    Public Undo As New List(Of Command)
    Public Watchlist As New List(Of Page)
    Public Whitelist As New List(Of String)
    Public WhitelistAutoChanges As New List(Of String)
    Public WhitelistLoaded As Boolean
    Public WhitelistManualChanges As New List(Of String)
    Public GlExcess As Integer = 20000
    Public Errors As String

    Public Delegate Sub Action()
    Public Delegate Sub CallbackDelegate(ByVal Success As Boolean)

    Public ReadOnly Tab As Char = Convert.ToChar(9)
    Public ReadOnly LF As Char = Convert.ToChar(10)
    Public ReadOnly CR As Char = Convert.ToChar(13)
    Public ReadOnly CRLF As String = Convert.ToChar(13) & Convert.ToChar(10)
    Public monthname As String() = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}

    Public Property CurrentEdit() As Edit
        'pointer to current
        Get
            If CurrentTab Is Nothing Then CurrentTab = CType(MainForm.Tabs.TabPages(0).Controls(0), BrowserTab)
            Return CurrentTab.Edit
        End Get

        Set(ByVal value As Edit)
            If CurrentTab IsNot Nothing Then CurrentTab.Edit = value
        End Set
    End Property

    Public ReadOnly Property CurrentUser() As User
        'Pointer to current user object
        Get
            If CurrentEdit Is Nothing Then Return Nothing Else Return CurrentEdit.User
        End Get
    End Property

    Public Sub ErrorLog(ByVal value As String)
        Errors = Errors & " >> " & System.DateTime.Now.ToString & " " & value
    End Sub

    Public ReadOnly Property CurrentPage() As Page
        'Pointer current page
        Get
            If CurrentEdit Is Nothing Then Return Nothing Else Return CurrentEdit.Page
        End Get
    End Property

    Class Block
        Public Time As Date
        Public User As User
        Public Action As String
        Public Duration As String
        Public Options As String
        Public Admin As User
        Public Comment As String
    End Class

    Class CacheData
        Public Edit As Edit
        Public Text As String
    End Class

    Class Command
        'commentme
        Public Description As String
        Public Type As CommandType
        Public Edit As Edit
        Public Page As Page
        Public User As User
    End Class

    Public Enum CommandType As Integer
        'Type of command
        Revert
        Report
        Warning
        Edit
        Ignore
        Unignore
        Accept
    End Enum

    Class Delete
        'Delete
        Public Time As Date
        Public Page As Page
        Public Action As String
        Public Admin As User
        Public Comment As String
    End Class

    Class EditData
        Public Edit As Edit, Page As Page
        Public Text, Summary, StartTime, EditTime, Token, Section As String
        Public Result As String
        Public CaptchaId, CaptchaWord As String
        Public Minor, Watch, Creating, CannotUndo As Boolean
        Public [Error] As Boolean
        Public NoAutoSummary As Boolean
    End Class

    Sub CleanSettings()
        'Clean
        'This function only clean main variables for shutdown

        Misc.monthname = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}
        Config.AutoReport = False
        Config.AutoWhitelist = False
        Config.Block = False
        Config.BlockMessage = "Blocked"
        Config.BlockMessageDefault = False
        Config.BlockMessageIndef = "Blocked"
        Config.BlockReason = ""
        Config.BlockSummary = ""
        Config.BlockTime = "1h"
        Config.BlockTimeAnon = ""
        Config.ConfigSummary = "Updating config."
        Config.ConfirmIgnored = False
        Config.ConfirmWarned = False
        Config.RequireConfig = True
        Config.RequireEdits = 0
        Config.RequireRev = False
        Config.RequireRollback = False
        Config.RequireTime = 0
        Config.RevertSummary = "."
        Config.RevisionAccess = False
        Config.RevisionR = False
        Config.SingleRevertSummary = ""
        Config.AIV = False
        Config.AIVBotLocation = ""
        Config.Approval = False
        Config.AIVLocation = "Wikipedia:AIV"
        Config.AivSingleNote = ""
        Config.AutoWarn = False
        Config.CfdLocation = "Wikipedia"
        Config.ConfirmPage = False
        Config.ConfirmSelfRevert = True
        Config.ConfirmMultiple = False
        Config.ConfirmRange = True
        Config.ConfirmWarned = True
        Config.CountBatchSize = 100
        Config.DefaultSummary = ""
        Config.Delete = False
        Config.DocsLocation = "en:Project:Huggle"
        Config.Email = True
        Config.EmailSubject = "Email from huggle"
        Config.Enabled = True
        Config.ExtendReports = False
        Config.FeedbackLocation = "Project:Huggle"
        Config.IfdLocation = "Wikipedia:IFD"
        Config.IrcChannel = ""
        Config.IrcServer = "irc.pmtpta.wikimedia.org"
        Config.MfdLocation = "Wikipedia:MFD"
        Config.MonthHeadings = True
        Config.OpenInBrowser = True
        Config.Patrol = True
        Config.Preloads = 0
        Config.PatrolSpeedy = False
        Config.Prod = False
        Config.ProdMessage = ""
        Config.ProdMessageSummary = ""
        Config.ProdMessageTitle = ""
        Config.PromptForBlock = False
        Config.ReportSummary = "reporting user"
        Config.RequireAdmin = False
        Config.RequireAutoconfirmed = False
        Config.RollbackSummary = ""




        Config.RevertSummary = ""
        Config.RequireRollback = False
        Config.RevertSummaries.Clear()
        Config.RequireTime = 0
        Config.RequireRev = False
        Config.RevisionR = False
        Config.BlockMessage = ""
        Config.RevisionAccess = False
        Config.RightAlignQueue = False
        Config.ShowNewEdits = False
        Config.ShowNewMessages = False
        Config.ShowQueue = False
        Config.ShowToolTips = False
        Config.ShowTwoQueues = False
        Config.Sight = False
        Config.SingleRevertSummary = ""
        Config.SlowIrc = False
        Config.SockReportLocation = ""
        Config.SockReports = False
        Config.RightPending = False
        Config.RequireConfig = False
        Config.UseAdminFunctions = False
        Config.UsePending = False
        Config.Project = ""
        Config.AutoAdvance = False
        Config.AfdLocation = ""
        Config.UAA = False
        Config.UAALocation = ""
        Config.AutoWhitelist = False
        Config.UserConfigLocation = ""
        Config.ConfirmRange = False
        Config.IrcMode = False
        Config.UseIrc = False
        Config.ExtendReports = False

    End Sub

    Class HistoryItem

        Public Edit As Edit
        Public Url As String
        Public Text As String

        Sub New(ByVal Edit As Edit)
            Me.Edit = Edit
        End Sub

        Sub New(ByVal Url As String)
            Me.Url = Url
        End Sub

    End Class

    Class PageMove
        Public Time As Date
        Public User As User
        Public Source As String
        Public Destination As String
        Public Summary As String
    End Class

    Class Protection
        'Informations about page protection
        Public Time As Date
        Public Admin As User
        Public Action As String
        Public Page As Page
        Public EditLevel As String
        Public MoveLevel As String
        Public CreateLevel As String
        Public Cascading As Boolean
        Public EditExpiry As Date
        Public MoveExpiry As Date
        Public CreateExpiry As Date
        Public Summary As String
        Public Pending As Boolean
    End Class

    Public Enum ProtectionType As Integer
        'Types
        Semi
        Full
        Move
    End Enum

    Class SpeedyCriterion
        'Info for speedy del data
        Public Code As String
        Public DisplayCode As String
        Public Description As String
        Public Template As String
        Public Parameter As String
        Public Message As String
        Public Notify As Boolean
    End Class

    Class Upload
        Public Time As Date
        Public User As User
        Public File As Page
        Public Summary As String
    End Class

    Class Warning
        'Defines warning type
        Public Level As UserLevel
        Public Time As Date
        Public Type As String
        Public User As User
    End Class

    Function ApiLimit() As Integer
        'Defines limits for api wikimedia blocks user who excess it
        If Config.Rights.Contains("apihighlimits") Then Return 5000 Else Return 500
    End Function

    Function ArrayContains(Of T)(ByVal Array As T(), ByVal Value As T) As Boolean
        'contain
        For Each Item As T In Array
            If Item.Equals(Value) Then Return True
        Next Item

        Return False
    End Function

    Sub Callback(ByVal Target As Threading.SendOrPostCallback, Optional ByVal PostData As Object = Nothing)
        'Invoke a method on the main thread
        SyncContext.Post(Target, PostData)
    End Sub

    Sub Callback(ByVal Target As Action)
        'As above, but invoked method does not need to take a parameter
        SyncContext.Post(AddressOf CallbackInvoke, CObj(Target))
    End Sub

    Sub CallbackInvoke(ByVal TargetObject As Object)
        'We're back on the main thread, now invoke the method
        CType(TargetObject, Action)()
    End Sub

    Function CompareUsernames(ByVal a As String, ByVal b As String) As Integer
        Return String.Compare(a, b, StringComparison.OrdinalIgnoreCase)
    End Function

    Sub ControlInvoke(ByVal Control As Control, ByVal Target As Action, ByVal ParamArray Args() As Object)
        'Works around a syntax oddity whereby Control.Invoke does not accept an AddressOf expression as a 
        'parameter directly, making expressions unnecessarily complex
        Control.Invoke(Target, Args)
    End Sub

    Sub ControlInvoke(Of T)(ByVal Control As Control, ByVal Target As Action(Of T), ByVal Param As T)
        'As above, with strongly-typed parameter
        Control.Invoke(Target, Param)
    End Sub

    Function FormatPageHtml(ByVal Page As Page, ByVal Text As String) As String
        'We're only interested in the page here, not the site interface that comes with it.
        'Extract the actual page content and then put some headers back so that it renders properly.
        'No, you can't have your non-monobook skin. So what? It doesn't show the site interface anyway.

        ' DVdm - 04/02/2016 - Sometimes Text is Nothing. If it is, then trap it and just return Nothing
        ' =============================================================================================
        If Text IsNot Nothing Then
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
            Text = MakeHtmlWikiPage(Page.Name, Text)
            Return Text
        Else
            Return Nothing
        End If
    End Function

    Function GetParameter(ByVal Source As String, ByVal Parameter As String) As String
        'Return a parameter from source
        If Source Is Nothing Then Return Nothing
        If Not Source.Contains(Parameter & "=""") Then Return Nothing
        Source = Source.Substring(Source.IndexOf(Parameter & "=""") + (Parameter & "=""").Length)
        If Not Source.Contains("""") Then Return Nothing
        Source = Source.Substring(0, Source.IndexOf(""""))
        Return HtmlDecode(Source)
    End Function

    Function FindString(ByVal Source As String, ByVal [From] As String) As String
        'comment needed
        If [From] Is Nothing Or Source Is Nothing Then
            Return ""
        End If
        If Not Source.Contains([From]) Then Return Nothing
        Return Source.Substring(Source.IndexOf([From]) + [From].Length)
    End Function

    Function FindString(ByVal Source As String, ByVal [From] As String, ByVal [To] As String) As String
        If [From] IsNot Nothing And Source IsNot Nothing Then
            If Not Source.Contains([From]) Then Return Nothing
            Source = Source.Substring(Source.IndexOf([From]) + [From].Length)
            If Not Source.Contains([To]) Then Return Nothing
            Return Source.Substring(0, Source.IndexOf([To]))
        Else
            Return ""
        End If
    End Function

    Function FindString(ByVal Source As String, ByVal From1 As String, ByVal From2 As String,
        ByVal [To] As String) As String

        If Not Source.Contains(From1) Then Return Nothing
        Source = Source.Substring(Source.IndexOf(From1) + From1.Length)
        If Not Source.Contains(From2) Then Return Nothing
        Source = Source.Substring(Source.IndexOf(From2) + From2.Length)
        If Not Source.Contains([To]) Then Return Source
        Return Source.Substring(0, Source.IndexOf([To]))
    End Function

    Function FindString(ByVal Source As String, ByVal From1 As String, ByVal From2 As String,
        ByVal From3 As String, ByVal [To] As String) As String

        If Not Source.Contains(From1) Then Return Nothing
        Source = Source.Substring(Source.IndexOf(From1) + From1.Length)
        If Not Source.Contains(From2) Then Return Nothing
        Source = Source.Substring(Source.IndexOf(From2) + From2.Length)
        If Not Source.Contains(From3) Then Return Nothing
        Source = Source.Substring(Source.IndexOf(From3) + From3.Length)
        If Not Source.Contains([To]) Then Return Source
        Return Source.Substring(0, Source.IndexOf([To]))
    End Function

    Function GetMonthName(ByVal Number As Integer) As String
        'return a localized name of month
        If Number <= 12 And Number > 0 Then
            Return Misc.monthname(Number - 1)
        Else
            Return ""
        End If

    End Function

    Function GetPage(ByVal Name As String) As Page
        'Get pointer to page
        Return Page.GetPage(Name)
    End Function

    Function GetUser(ByVal Name As String) As User
        'Get user object data
        Return User.GetUser(Name)
    End Function

    Function HtmlToWikiText(ByVal Text As String) As String
        'Convert edit summary in HTML form to its equivalent wikitext

        If Text.StartsWith("(") AndAlso Text.EndsWith(")") Then Text = Text.Substring(1, Text.Length - 2)

        Dim Break As Integer = 0

        ' 5/June/2013 Addshore - Add a limit of 10 to this loop after massive memory leak
        ' https://en.wikipedia.org/w/index.php?title=Wikipedia:Huggle/Feedback&diff=558338713&oldid=556694544
        While Text.Contains("<a href=") AndAlso Text.Contains("</a>") And Break < Misc.GlExcess And Break < 10
            Dim LinkTarget As String = HtmlDecode(FindString(Text, "<a href=", "title=""", """"))
            Dim LinkText As String = HtmlDecode(FindString(Text, "<a href=", ">", "</a>"))

            If LinkTarget = LinkText Then
                Text = Text.Substring(0, Text.IndexOf("<a href=")) & "[[" & LinkText & "]]" &
                FindString(Text, "</a>")
            Else
                Text = Text.Substring(0, Text.IndexOf("<a href=")) & "[[" & LinkTarget & "|" & LinkText & "]]" &
                FindString(Text, "</a>")
            End If
            Break = Break + 1
        End While

        Text = StripHTML(Text)

        Return Text
    End Function

    Function IsWikiPage(ByVal Text As String) As Boolean
        'Unfortunately there is no one element common to all skins
        If Text Is Nothing Then Return False
        Return Regex.Match(Text, "<body class=.mediawiki").Success Or Regex.Match(Text, "<div id=['""](mw[-_])?content['""] *>").Success
    End Function

    Function LocalConfigPath() As String
        'Get path to local drive config
        Return MakePath(Application.StartupPath, "Config")
    End Function

    Sub Localize(ByVal Control As Control, ByVal Prefix As String)
        'Use localized string for a control's text where necessary; recurse through all child controls
        For Each Item As Control In Control.Controls
            If TypeOf Item Is Label OrElse TypeOf Item Is CheckBox OrElse TypeOf Item Is RadioButton OrElse
                TypeOf Item Is Button OrElse TypeOf Item Is GroupBox OrElse TypeOf Item Is Huggle.TriState Then

                If Config.Language IsNot Nothing And Config.Messages(Config.Language).ContainsKey _
                    (Prefix & "-" & Item.Name.Replace("Label", "").ToLower) Then
                    Item.Text = Msg(Prefix & "-" & Item.Name.Replace("Label", "").ToLower)
                ElseIf Config.Messages(Config.Language).ContainsKey(Item.Name.ToLower) Then
                    Item.Text = Msg(Item.Name.ToLower)
                End If
            End If

            Localize(Item, Prefix)
        Next Item
    End Sub

    Sub Log(ByVal Message As String, Optional ByVal Tag As Object = Nothing)

        Callback(AddressOf LogCallback, New LogArgs With {.Message = Message, .Tag = Tag})
        'TO-DO huggle log
    End Sub

    Sub LogCallback(ByVal ArgsObject As Object)
        'Add log
        Dim Args As LogArgs = CType(ArgsObject, LogArgs)

        If MainForm Is Nothing Then LogBuffer.Add(Args.Message) Else MainForm.Log(Args.Message, Args.Tag)
    End Sub

    Structure LogArgs
        '
        Public Message As String
        Public Tag As Object
    End Structure

    Function MakeHtmlWikiPage(ByVal Page As String, ByVal Text As String) As String
        'Convert to html
        Return My.Resources.WikiPageHtml.Replace("$PATH", Config.Projects(Config.Project)).Replace("$PAGE", Page) _
            .Replace("$USER", Config.Username).Replace("$FONTSIZE", CStr(CInt((CInt(Config.DiffFontSize) * 1.2)))) &
            "<body>" & Text & "</body></html>"
    End Function

    Function MakePath(ByVal ParamArray Items As String()) As String
        For Each Item As String In Items
            If Item.StartsWith(Path.DirectorySeparatorChar) OrElse Item.StartsWith(Path.AltDirectorySeparatorChar) _
                Then Item = Item.Substring(1)
            If Item.EndsWith(Path.DirectorySeparatorChar) OrElse Item.EndsWith(Path.AltDirectorySeparatorChar) _
                Then Item = Item.Substring(0, Item.Length - 1)
        Next Item

        Return String.Join(Path.DirectorySeparatorChar, Items)
    End Function

    'Detect whether Mono is being used
    Function Mono() As Boolean
        Static UsingMono As Boolean = Nothing
        If UsingMono = Nothing Then UsingMono = (Type.GetType("Mono.Runtime") IsNot Nothing)
        Return UsingMono
    End Function

    Function Msg(ByVal Name As String, ByVal ParamArray Params() As String) As String
        'Returns a formatted message string localized to the user's language,
        'or in the default language if no localized message is available
        Try
            If Config.Language IsNot Nothing AndAlso Config.Messages.ContainsKey(Config.Language) _
                AndAlso Config.Messages(Config.Language).ContainsKey(Name.ToLower) Then
                Return String.Format(Config.Messages(Config.Language)(Name.ToLower), Params)

            ElseIf Config.Messages.ContainsKey(Config.DefaultLanguage) _
                AndAlso Config.Messages(Config.DefaultLanguage).ContainsKey(Name.ToLower) Then
                Return String.Format(Config.Messages(Config.DefaultLanguage)(Name.ToLower), Params)

            Else
                'Message does not exist in localized or default form. Output the message name instead.
                Return "[" & Name.ToLower & "]"
            End If

        Catch ex As FormatException
            'Message string didn't provide correct formatting placeholders. Output the message name instead.
            Return "[" & Name.ToLower & "]"
        End Try
    End Function

    Function MsgExists(ByVal Name As String) As Boolean
        Return Config.Messages(Config.Language).ContainsKey(Name.ToLower)
    End Function

    Sub OpenUrlInBrowser(ByVal Url As String)
        'Open
        Try
            Process.Start(Url)
        Catch
            Debug.WriteLine("OpenURL")
        End Try
    End Sub

    Function Ordinal(ByVal N As Integer) As String
        If (N Mod 100) \ 10 = 1 Then Return CStr(N) & "th"

        Select Case N Mod 10
            Case 1 : Return CStr(N) & "st"
            Case 2 : Return CStr(N) & "nd"
            Case 3 : Return CStr(N) & "rd"
            Case Else : Return CStr(N) & "th"
        End Select
    End Function

    Function PageNames(ByVal Pages As List(Of Page)) As List(Of String)
        'Convert a list of pages to a list of their names
        Dim Names As New List(Of String)

        For Each Item As Page In Pages
            If Not Names.Contains(Item.Name) Then Names.Add(Item.Name)
        Next Item

        Return Names
    End Function

    Function ParseUrl(ByVal Url As String) As Dictionary(Of String, String)
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

        ElseIf Url.Contains("w/wiki.phtml?") Then
            Url = Url.Substring(Url.IndexOf("w/wiki.phtml?") + 13)
        End If

        For Each Item As String In Url.Split("&"c)
            If Item.Contains("=") Then
                Dim ParamKey As String = Item.Substring(0, Item.IndexOf("="))

                'Only add keys if they do not already exist
                'This should avoid unexpected "ArgumentException: An item with the same key has already been added."
                If Not Params.ContainsKey(ParamKey) Then
                    Params.Add(
                        ParamKey,
                        UrlDecode(Item.Substring(Item.IndexOf("=") + 1))
                        )
                End If

            End If
        Next Item

        Return Params
    End Function

    Function SitePath() As String
        Return Config.Projects(Config.Project) & Config.WikiPath
    End Function

    Function ShortSitePath() As String
        Return Config.Projects(Config.Project) & Config.ShortWikiPath
    End Function

    Function SortWarningsByDate(ByVal X As Warning, ByVal Y As Warning) As Integer
        Return Date.Compare(Y.Time, X.Time)
    End Function

    Function Split(ByVal Source As String, ByVal Delimiter As String) As String()
        Return Source.Split(New String() {Delimiter}, StringSplitOptions.RemoveEmptyEntries)
    End Function

    Function StripHTML(ByVal Text As String) As String
        'Removes anything with < > around it
        If Text Is Nothing Then Return Nothing

        While Text.Contains("<") AndAlso Text.Contains(">")
            Text = Text.Substring(0, Text.IndexOf("<")) & Text.Substring(Text.IndexOf(">") + 1)
        End While

        Return Text
    End Function

    Function Timestamp(ByVal Time As Date) As String
        Return Time.Year & CStr(Time.Month).PadLeft(2, "0"c) & CStr(Time.Day).PadLeft(2, "0"c) &
            CStr(Time.Hour).PadLeft(2, "0"c) & CStr(Time.Minute).PadLeft(2, "0"c) & CStr(Time.Second).PadLeft(2, "0"c)
    End Function

    Function TrimSummary(ByVal Summary As String) As String
        Summary = Summary.Replace(" (page does not exist)", "")

        Dim Break As Integer = 0
        While Summary.IndexOf("[[") > -1 AndAlso Summary.IndexOf("[[") < Summary.IndexOf("]]") And Break < Misc.GlExcess
            Dim Title As String = Summary.Substring(Summary.IndexOf("[[") + 2)

            If Title.Contains("|") AndAlso (Not Title.Contains("]]") _
                OrElse Title.IndexOf("|") < Title.IndexOf("]]")) _
                Then Title = Title.Substring(Title.IndexOf("|") + 1)

            If Title.Contains("]]") Then Title = Title.Substring(0, Title.IndexOf("]]"))

            Summary = Summary.Substring(0, Summary.IndexOf("[[")) & Title & Summary.Substring(Summary.IndexOf("]]") + 2)
            Break = Break + 1
        End While

        Return Summary
    End Function

    Function Truncate(ByVal Str As String, ByVal Length As Integer) As String
        If Length < Str.Length Then Return Str.Substring(0, Length) & "..." Else Return Str
    End Function

    Function VersionString(ByVal Version As Version) As String
        Return Version.Major & "." & Version.Minor & "." & Version.Build
    End Function

    Function WikiTimestamp(ByVal Time As Date) As String
        Return CStr(Time.Hour).PadLeft(2, "0"c) & ":" & CStr(Time.Minute).PadLeft(2, "0"c) & ", " & CStr(Time.Day) &
            " " & GetMonthName(Time.Month) & " " & CStr(Time.Year) & " (UTC)"
    End Function

    Function WikiUrl(ByVal Url As String) As Boolean
        Return (Url.StartsWith(SitePath() & "index.php?") OrElse Url.StartsWith(ShortSitePath()))
    End Function

End Module