Imports System.IO
Imports System.Threading

Module Config

    'Configuration

    Public Version As New Version(Application.ProductVersion)

    Public ConfigChanged As Boolean = False
    Public ConfigVersion As New Version(0, 0, 0)
    Public ContribsBlockSize As Integer = 100
    Public CreditUrl As String = "http://en.wikipedia.org/wiki/User:Gurch"
    Public DiffCss As String = "* {text-decoration: none;} :hover {text-decoration: underline;} " & _
        ".new {color: red;} table.diff {table-layout: fixed} table.diff col.diff-content {width: 50%;} " & _
        ".diff-otitle, .diff-ntitle {font-size: 120%;} td.diff-marker {font-size: 0px; color: white;} " & _
        "#differences-nextlink, #differences-prevlink, .diff-lineno, #mw-diff-otitle4, #mw-diff-ntitle4 " & _
        "{display: none;} .diff td div {overflow: scroll;} .diff {width: 98%; font-family: arial; font-size: 8pt;} " & _
        ".diff-addedline {background: #CCFFCC;} .diff-deletedline {background: #FFFFCC;} .diffchange {color: red; " & _
        "font-weight: bold;} .diff-multi {font-size: 120%; font-weight: bold;} .mw-rollback-link {display: none;} " & _
        ".usermessage {font-family: arial; background-color: #ffce7b; border: 1px solid #ffa500; color: black; " & _
        "font-weight: bold; margin: 2em 0 1em; padding: 0.5em 1em; vertical-align: middle;}"
    Public GlobalConfigLocation As String = "http://meta.wikimedia.org/w/index.php?title=Huggle/GlobalConfig&action=raw"
    Public HistoryBlockSize As Integer = 100
    Public LocalConfigLocation As String = "\config.txt"
    Public ProtectedNamespaces As String() = {"MediaWiki"}
    Public QueueWidth As Integer = 160
    Public RememberMe As Boolean = True
    Public SitePath As String = "http://en.wikipedia.org/"
    Public UnmovableNamespaces As String() = {"Category", "Image"}

    'Values stored in local config file

    Public IrcMode As Boolean = True
    Public Project As String
    Public ProxyUsername As String
    Public ProxyUserDomain As String
    Public ProxyServer As String
    Public ProxyPort As String
    Public Username As String
    Public WindowMaximize As Boolean = True
    Public WindowPosition As New Point
    Public WindowSize As New Size

    'Values changeable through global / project / user config pages

    Public AfdLocation As String
    Public AIV As Boolean = False
    Public AIVBotLocation As String
    Public AIVLocation As String
    Public AivSingleNote As String
    Public Approval As Boolean = False
    Public AutoAdvance As Boolean = False
    Public AutoReport As Boolean = True
    Public AutoWarn As Boolean = True
    Public AutoWhitelist As Boolean = True
    Public Block As Boolean = False
    Public BlockExpiryOptions As New List(Of String)
    Public BlockMessage As String
    Public BlockMessageDefault As Boolean = True
    Public BlockMessageIndef As String
    Public BlockReason As String
    Public BlockSummary As String
    Public BlockTime As String = "indefinite"
    Public BlockTimeAnon As String = "24 hours"
    Public CfdLocation As String
    Public ConfigSummary As String
    Public ConfirmIgnored As Boolean = True
    Public ConfirmMultiple As Boolean = False
    Public ConfirmSame As Boolean = True
    Public ConfirmSelfRevert As Boolean = True
    Public CustomRevertSummaries As New List(Of String)
    Public DefaultSummary As String = ""
    Public Delete As Boolean = False
    Public DiffFontSize As String = "8"
    Public DocsLocation As String = "http://en.wikipedia.org/wiki/Wikipedia:Huggle"
    Public Enabled As Boolean = False
    Public EnabledForAll As Boolean = False
    Public ExtendReports As Boolean = True
    Public FeedbackLocation As String
    Public GoToPages As New List(Of String)
    Public IconsLocation As String = "http://en.wikipedia.org/wiki/Wikipedia:Huggle/Icons"
    Public IfdLocation As String
    Public IgnoredPages As New List(Of String)
    Public Initialised As Boolean = False
    Public IrcChannel As String
    Public IrcPort As Integer = 6667
    Public IrcServer As String
    Public IrcUsername As String
    Public LogFile As String
    Public ManualRevertSummary As String
    Public MaxAIVDiffs As Integer = 8
    Public MfdLocation As String
    Public MinorNotifications As Boolean = False
    Public MinorOther As Boolean = False
    Public MinorReports As Boolean = False
    Public MinorReverts As Boolean = True
    Public MinorTags As Boolean = False
    Public MinorWarnings As Boolean = False
    Public MinVersion As String
    Public MinWarningWait As Integer = 10
    Public MonthHeadings As Boolean = False
    Public NamespacesChecked As New List(Of String)(New String() {"article", "talk", "user", "user talk", "help", _
        "help talk", "portal", "portal talk", "template", "template talk", "mediawiki", "mediawiki talk", "image", _
        "image talk", "category", "category talk", "wikipedia", "wikipedia talk"})
    Public OpenInBrowser As Boolean = False
    Public Patrol As Boolean = False
    Public PatrolSpeedy As Boolean = False
    Public Preloads As Integer = 2
    Public Prod As Boolean = False
    Public ProdMessage As String
    Public ProdMessageSummary As String
    Public ProdMessageTitle As String
    Public ProdSummary As String
    Public ProjectConfigLocation As String
    Public Projects As New List(Of String)(New String() { _
        "en.wikipedia;en.wikipedia.org", _
        "bg.wikipedia;bg.wikipedia.org", _
        "es.wikipedia;es.wikipedia.org", _
        "no.wikipedia;no.wikipedia.org", _
        "pt.wikipedia;pt.wikipedia.org", _
        "ru.wikipedia;ru.wikipedia.org", _
        "commons;commons.wikimedia.org", _
        "meta;meta.wikimedia.org", _
        "test wiki;test.wikipedia.org" _
        })
    Public PromptForBlock As Boolean = True
    Public PromptForReport As Boolean = False
    Public Protect As Boolean = False
    Public ProtectionReason As String
    Public ProtectionRequests As Boolean = False
    Public ProtectionRequestPage As String
    Public ProtectionRequestReason As String
    Public ProtectionRequestSummary As String
    Public QueueBuilderLimit As Integer = 2
    Public RcBlockSize As Integer = 100
    Public ReportExtendSummary As String
    Public ReportLinkDiffs As Boolean = False
    Public ReportReason As String = "vandalism"
    Public ReportSummary As String
    Public RequireAdmin As Boolean = False
    Public RequireConfig As Boolean = False
    Public RequireEdits As Integer
    Public RequireRollback As Boolean = False
    Public RequireTime As Integer
    Public RfdLocation As String
    Public RollbackSummary As String
    Public RollbackSummaryUnknown As String
    Public SensitiveAddresses As New List(Of String)
    Public ShowAnonymous As Boolean = True
    Public ShowRegistered As Boolean = True
    Public ShowNewEdits As Boolean = True
    Public ShowNewPages As Boolean = False
    Public ShowLog As Boolean = True
    Public ShowQueue As Boolean = True
    Public ShowToolTips As Boolean = True
    Public Speedy As Boolean = False
    Public SpeedyDeleteSummary As String
    Public SpeedyMessageSummary As String
    Public SpeedyMessageTitle As String
    Public SpeedySummary As String
    Public Summary As String
    Public Tags As New List(Of String)
    Public TemplateMessages As New List(Of String)
    Public TemplateMessagesGlobal As New List(Of String)
    Public TfdLocation As String
    Public TrayIcon As Boolean = False
    Public UAALocation As String
    Public UAABotLocation As String
    Public UndoSummary As String
    Public UpdateWhitelist As Boolean = False
    Public UseAdminFunctions As Boolean = True
    Public UserAgent As String = "Huggle/" & Version.Major.ToString & "." & Version.Minor.ToString & "." & _
        Version.Build & " http://en.wikipedia.org/wiki/Huggle"
    Public UserConfigLocation As String = "Special:Mypage/huggle.css"
    Public UserListLocation As String
    Public UserListUpdateSummary As String
    Public UseRollback As Boolean = True
    Public WarningAge As Integer = 36
    Public WarningImLevel As Boolean = False
    Public WarningMode As String
    Public WarningSeries As New List(Of String)
    Public WatchNotifications As Boolean = False
    Public WatchOther As Boolean = False
    Public WatchReports As Boolean = False
    Public WatchReverts As Boolean = False
    Public WatchTags As Boolean = False
    Public WatchWarnings As Boolean = False
    Public WhitelistEnabled As Boolean = False
    Public WhitelistEditCount As Integer = 500
    Public WhitelistLocation As String
    Public WhitelistUpdateSummary As String
    Public Xfd As Boolean = False
    Public XfdDiscussionSummary As String
    Public XfdLogSummary As String
    Public XfdMessage As String
    Public XfdMessageSummary As String
    Public XfdMessageTitle As String
    Public XfdSummary As String

    Public WarnSummary As String
    Public WarnSummary2 As String
    Public WarnSummary3 As String
    Public WarnSummary4 As String

    Public Welcome As String
    Public WelcomeAnon As String
    Public WelcomeSummary As String

    Public Sub GetLocalConfig()
        'Read from local configuration file

        InitialiseShortcuts()

        If File.Exists(LocalConfigPath() & LocalConfigLocation) Then
            For Each Item As String In New List(Of String)(File.ReadAllLines(LocalConfigPath() & LocalConfigLocation))
                If Item.Contains(":") Then
                    Dim OptionName As String = Item.Substring(0, Item.IndexOf(":")), _
                        OptionValue As String = Item.Substring(Item.IndexOf(":") + 1)

                    Select Case OptionName
                        Case "irc" : Config.IrcMode = CBool(OptionValue)
                        Case "log-file" : Config.LogFile = OptionValue
                        Case "project" : Config.Project = OptionValue
                        Case "proxy-port" : Config.ProxyPort = OptionValue
                        Case "proxy-server" : Config.ProxyServer = OptionValue
                        Case "proxy-userdomain" : Config.ProxyUserDomain = OptionValue
                        Case "proxy-username" : Config.ProxyUsername = OptionValue
                        Case "username" : Config.Username = OptionValue
                        Case "window-height" : Config.WindowSize.Height = CInt(OptionValue)
                        Case "window-left" : Config.WindowPosition.X = CInt(OptionValue)
                        Case "window-maximize" : Config.WindowMaximize = CBool(OptionValue)
                        Case "window-top" : Config.WindowPosition.Y = CInt(OptionValue)
                        Case "window-width" : Config.WindowSize.Width = CInt(OptionValue)
                        Case "shortcuts" : SetShortcutsFromConfig(OptionValue)
                        Case "revert-summaries" : SetRevertSummaries(OptionValue)
                    End Select
                End If
            Next Item
        End If
    End Sub

    Public Sub WriteLocalConfig()
        'Write to local configuration file
        If Main IsNot Nothing Then
            Dim LocalConfigItems As New List(Of String)

            LocalConfigItems.Add("irc:" & CStr(Config.IrcMode).ToLower)
            LocalConfigItems.Add("log-file:" & Config.LogFile)
            LocalConfigItems.Add("project:" & Config.Project)
            LocalConfigItems.Add("proxy-port:" & Config.ProxyPort)
            LocalConfigItems.Add("proxy-server:" & Config.ProxyServer)
            LocalConfigItems.Add("proxy-userdomain:" & Config.ProxyUserDomain)
            LocalConfigItems.Add("proxy-username:" & Config.ProxyUsername)
            LocalConfigItems.Add("username:" & Config.Username)
            LocalConfigItems.Add("window-height:" & CStr(Main.Height))
            LocalConfigItems.Add("window-left:" & CStr(Main.Left))
            LocalConfigItems.Add("window-maximize:" & CStr(Main.WindowState = FormWindowState.Maximized).ToLower)
            LocalConfigItems.Add("window-top:" & CStr(Main.Top))
            LocalConfigItems.Add("window-width:" & CStr(Main.Width))

            Dim Shortcuts As New List(Of String)

            For Each Item As KeyValuePair(Of String, Shortcut) In ShortcutKeys
                Shortcuts.Add(Item.Key & ";" & CInt(Item.Value.Key).ToString & ";" & CInt(Item.Value.Control) _
                    .ToString & ";" & CInt(Item.Value.Alt).ToString & ";" & CInt(Item.Value.Shift).ToString)
            Next Item

            LocalConfigItems.Add("shortcuts:" & Strings.Join(Shortcuts.ToArray, ","))

            Dim Summaries As New List(Of String)

            For Each Item As String In ManualRevertSummaries
                Summaries.Add(Item.Replace(",", "\,"))
            Next Item

            LocalConfigItems.Add("revert-summaries:" & Strings.Join(Summaries.ToArray, ","))

            File.WriteAllLines(LocalConfigPath() & LocalConfigLocation, LocalConfigItems.ToArray)
        End If
    End Sub

    Private Function LocalConfigPath() As String
        Dim Path As String = Application.UserAppDataPath
        Path = Path.Substring(0, Path.LastIndexOf("\"))
        Return Path.Substring(0, Path.LastIndexOf("\"))
    End Function

    Private Sub SetShortcutsFromConfig(ByVal Value As String)
        For Each Item As String In Value.Replace(vbLf, "").Replace(vbCr, "").Replace("\,", Chr(1)).Split _
            (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

            If Item.Contains(";") Then
                Dim ItemKey As String = Item.Substring(0, Item.IndexOf(";")).Trim(" "c).Replace(Chr(1), ",")
                Dim ItemValue As String() = Item.Substring(Item.IndexOf(";") + 1).Split(";"c)

                If ShortcutKeys.ContainsKey(ItemKey) Then ShortcutKeys(ItemKey) = New Shortcut _
                    (CType(CInt(ItemValue(0)), Keys), ItemValue(1) <> "0", ItemValue(2) <> "0", ItemValue(3) <> "0")
            End If
        Next Item
    End Sub

    Private Sub SetRevertSummaries(ByVal Value As String)
        For Each Item As String In Value.Replace(vbLf, "").Replace(vbCr, "").Replace("\,", Chr(1)).Split _
            (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

            ManualRevertSummaries.Add(Item.Replace(Chr(1), ","))
        Next Item
    End Sub

End Module
