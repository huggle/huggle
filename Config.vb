Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading

Module Config

    'Configuration

    Public Version As New Version(Application.ProductVersion)

    Public ConfigChanged As Boolean
    Public ConfigVersion As New Version(0, 0, 0)
    Public ContribsBlockSize As Integer = 100
    Public GlobalConfigLocation As String = "http://meta.wikimedia.org/w/index.php?title=Huggle/GlobalConfig&action=raw"
    Public HistoryBlockSize As Integer = 100
    Public IrcMode As Boolean = True
    Public LatestVersion As New Version(0, 0, 0)
    Public LocalConfigLocation As String = "\config.txt"
    Public QueueSize As Integer = 5000
    Public QueueWidth As Integer = 160
    Public RememberMe As Boolean = True
    Public SitePath As String = "http://en.wikipedia.org/"

    'Values stored in local config file

    Public Project As String
    Public ProxyUsername As String
    Public ProxyUserDomain As String
    Public ProxyServer As String
    Public ProxyPort As String
    Public ProxyEnabled As Boolean
    Public Username As String
    Public WindowMaximize As Boolean = True
    Public WindowPosition As New Point
    Public WindowSize As New Size

    'Values changeable through global / project / user config pages

    Public AfdLocation As String
    Public AIV As Boolean
    Public AIVBotLocation As String
    Public AIVLocation As String
    Public AivSingleNote As String
    Public Approval As Boolean
    Public AutoAdvance As Boolean
    Public AutoReport As Boolean = True
    Public AutoWarn As Boolean = True
    Public AutoWhitelist As Boolean = True
    Public Block As Boolean
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
    Public ConfirmMultiple As Boolean
    Public ConfirmSame As Boolean = True
    Public ConfirmSelfRevert As Boolean = True
    Public CountBatchSize As Integer = 20
    Public CustomRevertSummaries As New List(Of String)
    Public DefaultSummary As String = ""
    Public Delete As Boolean
    Public DiffFontSize As String = "8"
    Public DocsLocation As String = "http://en.wikipedia.org/wiki/Wikipedia:Huggle"
    Public DownloadLocation As String = "http://huggle.googlecode.com/files/huggle $1.exe"
    Public Email As Boolean
    Public EmailSubject As String
    Public Enabled As Boolean
    Public EnabledForAll As Boolean
    Public ExtendReports As Boolean = True
    Public FeedbackLocation As String
    Public GoToPages As New List(Of String)
    Public IconsLocation As String = "http://en.wikipedia.org/wiki/Wikipedia:Huggle/Icons"
    Public IfdLocation As String
    Public IgnoredPages As New List(Of String)
    Public Initialised As Boolean
    Public IrcChannel As String
    Public IrcPort As Integer = 6667
    Public IrcServer As String
    Public IrcUsername As String
    Public LogFile As String
    Public ManualRevertSummary As String
    Public MaxAIVDiffs As Integer = 8
    Public MfdLocation As String
    Public MinorNotifications As Boolean
    Public MinorOther As Boolean
    Public MinorReports As Boolean
    Public MinorReverts As Boolean = True
    Public MinorTags As Boolean
    Public MinorWarnings As Boolean
    Public MinVersion As Version
    Public MinWarningWait As Integer = 10
    Public MonthHeadings As Boolean
    Public OpenInBrowser As Boolean
    Public Patrol As Boolean
    Public PatrolSpeedy As Boolean
    Public Preloads As Integer = 2
    Public Prod As Boolean
    Public ProdMessage As String
    Public ProdMessageSummary As String
    Public ProdMessageTitle As String
    Public ProdSummary As String
    Public ProjectConfigLocation As String
    Public Projects As New List(Of String)(New String() { _
        "en.wikipedia;en.wikipedia.org", _
        "bg.wikipedia;bg.wikipedia.org", _
        "de.wikipedia;de.wikipedia.org", _
        "es.wikipedia;es.wikipedia.org", _
        "no.wikipedia;no.wikipedia.org", _
        "pt.wikipedia;pt.wikipedia.org", _
        "ru.wikipedia;ru.wikipedia.org", _
        "commons;commons.wikimedia.org", _
        "meta;meta.wikimedia.org", _
        "test wiki;test.wikipedia.org" _
        })
    Public PromptForBlock As Boolean = True
    Public PromptForReport As Boolean
    Public Protect As Boolean
    Public ProtectionReason As String
    Public ProtectionRequests As Boolean
    Public ProtectionRequestPage As String
    Public ProtectionRequestReason As String
    Public ProtectionRequestSummary As String
    Public QueueMaxAge As Integer
    Public QueueBuilderLimit As Integer = 10
    Public RcBlockSize As Integer = 100
    Public ReportExtendSummary As String
    Public ReportLinkDiffs As Boolean
    Public ReportReason As String = "vandalism"
    Public ReportSummary As String
    Public RequireAdmin As Boolean
    Public RequireConfig As Boolean
    Public RequireEdits As Integer
    Public RequireRollback As Boolean
    Public RequireTime As Integer
    Public RfdLocation As String
    Public RollbackSummary As String
    Public RollbackSummaryUnknown As String
    Public SensitiveAddresses As New List(Of String)
    Public SharedIPTemplates As New List(Of String)
    Public ShowAnonymous As Boolean = True
    Public ShowRegistered As Boolean = True
    Public ShowNewEdits As Boolean = True
    Public ShowNewPages As Boolean
    Public ShowLog As Boolean = True
    Public ShowQueue As Boolean = True
    Public ShowToolTips As Boolean = True
    Public Speedy As Boolean
    Public SpeedyDeleteSummary As String
    Public SpeedyMessageSummary As String
    Public SpeedyMessageTitle As String
    Public SpeedySummary As String
    Public StartupMessage As Boolean = True
    Public StartupMessageLocation As String
    Public Summary As String
    Public Tags As New List(Of String)
    Public TemplateMessages As New List(Of String)
    Public TemplateMessagesGlobal As New List(Of String)
    Public TfdLocation As String
    Public TrayIcon As Boolean
    Public UAALocation As String
    Public UAABotLocation As String
    Public UndoSummary As String
    Public UpdateWhitelist As Boolean
    Public UpdateWhitelistManual As Boolean
    Public UseAdminFunctions As Boolean = True
    Public UserAgent As String = "Huggle/" & Version.Major.ToString & "." & Version.Minor.ToString & "." & _
        Version.Build & " http://en.wikipedia.org/wiki/Huggle"
    Public UserConfigLocation As String = "Special:Mypage/huggle.css"
    Public UserListLocation As String
    Public UserListUpdateSummary As String
    Public UseRollback As Boolean = True
    Public WarningAge As Integer = 36
    Public WarningImLevel As Boolean
    Public WarningMode As String
    Public WarningSeries As New List(Of String)
    Public WatchNotifications As Boolean
    Public WatchOther As Boolean
    Public WatchReports As Boolean
    Public WatchReverts As Boolean
    Public WatchTags As Boolean
    Public WatchWarnings As Boolean
    Public WhitelistEditCount As Integer = 500
    Public WhitelistLocation As String
    Public WhitelistUpdateSummary As String
    Public Xfd As Boolean
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

    Public Sub SetGlobalConfigOption(ByVal Name As String, ByVal Value As String)
        'Global config only
        Select Case Name
            Case "enable-all" : Config.EnabledForAll = CBool(Value)
            Case "config" : Config.ProjectConfigLocation = Value
            Case "documentation" : Config.DocsLocation = Value
            Case "feedback" : Config.FeedbackLocation = Value
            Case "irc-server" : Config.IrcServer = Value
            Case "sensitive-addresses" : SetSensitiveAddresses(Value)
            Case "user-agent" : Config.UserAgent = Value.Replace("$1", Config.Version.ToString)
            Case "user-config" : Config.UserConfigLocation = Value
        End Select
    End Sub

    Private Sub SetSensitiveAddresses(ByVal Value As String)
        Config.SensitiveAddresses.Clear()

        For Each Item As String In Value.Replace(LF, "").Replace("\,", Convert.ToChar(1)).Split _
            (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

            If Item.Contains(";") Then Config.SensitiveAddresses.Add(Item.Trim(" "c).Replace(Convert.ToChar(1), ","))
        Next Item
    End Sub

    Public Sub SetSharedConfigOption(ByVal Name As String, ByVal Value As String)
        'Project and user config
        Select Case Name
            Case "enable" : Config.Enabled = CBool(Value)
            Case "admin" : Config.UseAdminFunctions = CBool(Value)
            Case "anonymous" : SetAnonymous(Value)
            Case "auto-advance" : Config.AutoAdvance = CBool(Value)
            Case "auto-whitelist" : Config.AutoWhitelist = CBool(Value)
            Case "blocktime" : Config.BlockTime = Value
            Case "blocktime-anon" : Config.BlockTimeAnon = Value
            Case "block-message" : Config.BlockMessage = Value
            Case "block-message-default" : Config.BlockMessageDefault = CBool(Value)
            Case "block-message-indef" : Config.BlockMessageIndef = Value
            Case "block-prompt" : Config.PromptForBlock = CBool(Value)
            Case "block-reason" : Config.BlockReason = Value
            Case "block-summary" : Config.BlockSummary = "Notification: Blocked"
            Case "confirm-ignored" : Config.ConfirmIgnored = CBool(Value)
            Case "confirm-multiple" : Config.ConfirmMultiple = CBool(Value)
            Case "confirm-same" : Config.ConfirmSame = CBool(Value)
            Case "confirm-self-revert" : Config.ConfirmSelfRevert = CBool(Value)
            Case "default-summary" : Config.DefaultSummary = Value
            Case "diff-font-size" : Config.DiffFontSize = Value
            Case "extend-reports" : Config.ExtendReports = CBool(Value)
            Case "irc-port" : Config.IrcPort = CInt(Value)
            Case "minor" : SetMinor(Value)
            Case "new-pages" : Config.ShowNewPages = CBool(Value)
            Case "open-in-browser" : Config.OpenInBrowser = CBool(Value)
            Case "patrol-speedy" : Config.PatrolSpeedy = CBool(Value)
            Case "preload" : Config.Preloads = CInt(Value)
            Case "prod" : Config.Prod = CBool(Value)
            Case "prod-message" : Config.ProdMessage = Value
            Case "prod-message-summary" : Config.ProdMessageSummary = Value
            Case "prod-message-title" : Config.ProdMessageTitle = Value
            Case "prod-summary" : Config.ProdSummary = Value
            Case "protection-reason" : Config.ProtectionReason = Value
            Case "protection-requests" : Config.ProtectionRequests = CBool(Value)
            Case "queue-max-age" : Config.QueueMaxAge = CInt(Value)
            Case "report" : SetReport(Value)
            Case "report-extend-summary" : Config.ReportExtendSummary = Value
            Case "report-summary" : Config.ReportSummary = Value
            Case "revert-summaries" : Config.CustomRevertSummaries = GetList(Value)
            Case "rollback" : Config.UseRollback = CBool(Value)
            Case "show-log" : Config.ShowLog = CBool(Value)
            Case "show-new-edits" : Config.ShowNewEdits = CBool(Value)
            Case "show-queue" : Config.ShowQueue = CBool(Value)
            Case "show-tool-tips" : Config.ShowToolTips = CBool(Value)
            Case "speedy" : Config.Speedy = CBool(Value)
            Case "speedy-message-summary" : Config.SpeedyMessageSummary = Value
            Case "speedy-message-title" : Config.SpeedyMessageTitle = Value
            Case "speedy-summary" : Config.SpeedySummary = Value
            Case "tags" : SetTags(Value)
            Case "tray-icon" : Config.TrayIcon = CBool(Value)
            Case "undo-summary" : Config.UndoSummary = Value
            Case "update-whitelist" : Config.UpdateWhitelist = CBool(Value)
            Case "watchlist" : SetWatch(Value)
            Case "welcome" : Config.Welcome = Value
            Case "welcome-anon" : Config.WelcomeAnon = Value
        End Select
    End Sub

    Public Sub SetProjectConfigOption(ByVal Name As String, ByVal Value As String)
        'Project config only
        Select Case Name
            Case "afd" : Config.AfdLocation = Value
            Case "aiv" : Config.AIVLocation = Value
            Case "aivbot" : Config.AIVBotLocation = Value
            Case "aiv-reports" : Config.AIV = CBool(Value)
            Case "aiv-single-note" : Config.AivSingleNote = Value
            Case "approval" : Config.Approval = CBool(Value)
            Case "block" : Config.Block = CBool(Value)
            Case "block-expiry-options" : Config.BlockExpiryOptions = GetList(Value)
            Case "cfd" : Config.CfdLocation = Value
            Case "config-summary" : Config.ConfigSummary = Value
            Case "count-batch-size" : Config.CountBatchSize = CInt(Value)
            Case "delete" : Config.Delete = CBool(Value)
            Case "email" : Config.Email = CBool(Value)
            Case "email-subject" : Config.EmailSubject = Value
            Case "enable-all" : Config.EnabledForAll = CBool(Value)
            Case "go" : Config.GoToPages = GetList(Value)
            Case "ifd" : Config.IfdLocation = Value
            Case "ignore" : Config.IgnoredPages = GetList(Value)
            Case "manual-revert-summary" : Config.ManualRevertSummary = Value
            Case "mfd" : Config.MfdLocation = Value
            Case "min-version" : SetMinVersion(Value)
            Case "namespace-aliases" : SetNamespaceAliases(GetList(Value))
            Case "namespace-names" : SetNamespaceNames(GetList(Value))
            Case "patrol" : Config.Patrol = CBool(Value)
            Case "protect" : Config.Protect = CBool(Value)
            Case "protection-request-page" : Config.ProtectionRequestPage = Value
            Case "protection-request-reason" : Config.ProtectionRequestReason = Value
            Case "protection-request-summary" : Config.ProtectionRequestSummary = Value
            Case "rc-block-size" : Config.RcBlockSize = CInt(Value)
            Case "report-link-diffs" : Config.ReportLinkDiffs = CBool(Value)
            Case "require-admin" : Config.RequireAdmin = CBool(Value)
            Case "require-config" : Config.RequireConfig = CBool(Value)
            Case "require-edits" : Config.RequireEdits = CInt(Value)
            Case "require-rollback" : Config.RequireRollback = CBool(Value)
            Case "require-time" : Config.RequireTime = CInt(Value)
            Case "rollback-summary" : Config.RollbackSummary = Value
            Case "rollback-summary-unknown" : Config.RollbackSummaryUnknown = Value
            Case "rfd" : Config.RfdLocation = Value
            Case "shared-ip-templates" : Config.SharedIPTemplates = GetList(Value)
            Case "speedy-delete-summary" : Config.SpeedyDeleteSummary = Value
            Case "speedy-options" : SetSpeedyOptions(Value)
            Case "startup-message-location" : Config.StartupMessageLocation = Value
            Case "summary" : Config.Summary = Value
            Case "templates" : Config.TemplateMessagesGlobal = GetList(Value)
            Case "tfd" : Config.TfdLocation = Value
            Case "uaa" : Config.UAALocation = Value
            Case "uaabot" : Config.UAABotLocation = Value
            Case "update-whitelist-manual" : Config.UpdateWhitelistManual = CBool(Value)
            Case "userlist" : Config.UserListLocation = Value
            Case "userlist-update-summary" : Config.UserListUpdateSummary = Value
            Case "version" : SetLatestVersion(Value)
            Case "warning-im-level" : Config.WarningImLevel = CBool(Value)
            Case "warning-mode" : Config.WarningMode = Value
            Case "warning-month-headings" : Config.MonthHeadings = CBool(Value)
            Case "warning-series" : Config.WarningSeries = GetList(Value)
            Case "welcome-summary" : Config.WelcomeSummary = Value
            Case "whitelist" : Config.WhitelistLocation = Value
            Case "whitelist-edit-count" : Config.WhitelistEditCount = CInt(Value)
            Case "whitelist-update-summary" : Config.WhitelistUpdateSummary = Value
            Case "xfd" : Config.Xfd = CBool(Value)
            Case "xfd-discussion-summary" : Config.XfdDiscussionSummary = Value
            Case "xfd-log-summary" : Config.XfdLogSummary = Value
            Case "xfd-message" : Config.XfdMessage = Value
            Case "xfd-message-summary" : Config.XfdMessageSummary = Value
            Case "xfd-message-title" : Config.XfdMessageTitle = Value
            Case "xfd-summary" : Config.XfdSummary = Value

            Case "warn-summary" : Config.WarnSummary = Value
            Case "warn-summary-2" : Config.WarnSummary2 = Value
            Case "warn-summary-3" : Config.WarnSummary3 = Value
            Case "warn-summary-4" : Config.WarnSummary4 = Value

            Case Else
                For Each Item2 As String In Config.WarningSeries
                    If Name.StartsWith(Item2) Then
                        Select Case Name.Substring(Item2.Length)
                            Case "1", "2", "3", "4", "4im"
                                WarningMessages.Add(Name, Value)
                        End Select

                        Exit For
                    End If
                Next Item2
        End Select
    End Sub

    Public Sub SetUserConfigOption(ByVal Name As String, ByVal Value As String)
        'User config only
        Select Case Name
            Case "templates" : Config.TemplateMessages = GetList(Value)
            Case "version" : Config.ConfigVersion = New Version(CInt(Value.Substring(0, 1)), _
                CInt(Value.Substring(2, 1)), CInt(Value.Substring(4)), 0)
        End Select
    End Sub

    Private Sub SetAnonymous(ByVal Value As String)
        Config.ShowRegistered = (Value <> "only")
        Config.ShowAnonymous = (Value <> "no")
    End Sub

    Private Sub SetLatestVersion(ByVal VersionString As String)
        Config.LatestVersion = New Version(CInt(VersionString.Substring(0, 1)), _
            CInt(VersionString.Substring(2, 1)), CInt(VersionString.Substring(4)), 0)
    End Sub

    Private Sub SetMinor(ByVal Value As String)
        Config.MinorNotifications = False
        Config.MinorOther = False
        Config.MinorReports = False
        Config.MinorReverts = False
        Config.MinorTags = False
        Config.MinorWarnings = False

        For Each Item As String In Value.Split(","c)
            Item = Item.Trim(","c, " "c, LF).ToLower

            Select Case Item
                Case "reverts" : Config.MinorReverts = True
                Case "other" : Config.MinorOther = True
                Case "notifications" : Config.MinorNotifications = True
                Case "reports" : Config.MinorReports = True
                Case "tags" : Config.MinorTags = True
                Case "warnings" : Config.MinorWarnings = True
            End Select
        Next Item
    End Sub

    Private Sub SetMinVersion(ByVal VersionString As String)
        Config.MinVersion = New Version(CInt(VersionString.Substring(0, 1)), _
            CInt(VersionString.Substring(2, 1)), CInt(VersionString.Substring(4)), 0)
    End Sub

    Private Sub SetNamespaceNames(ByVal Items As List(Of String))
        For Each Item As String In Items
            If Item.Contains(";") Then Space.SetName(CInt(Item.Split(";"c)(0)), Item.Split(";"c)(1))
        Next Item
    End Sub

    Private Sub SetNamespaceAliases(ByVal Items As List(Of String))
        For Each Item As String In Items
            If Item.Contains(";") Then
                Dim Key As String = Item.Split(";"c)(0), Value As Integer = CInt(Item.Split(";"c)(1))
                If Not Space.Aliases.ContainsKey(Key) Then Space.Aliases.Add(Key, Value)
            End If
        Next Item
    End Sub

    Private Sub SetReport(ByVal Value As String)
        Config.AutoReport = (Value = "auto")
        Config.PromptForReport = (Value = "prompt")
    End Sub

    Private Sub SetSpeedyOptions(ByVal Value As String)
        SpeedyCriteria.Clear()

        For Each Item As String In Value.Replace(LF, "").Replace("\;", Convert.ToChar(2)) _
            .Replace("\,", Convert.ToChar(1)).Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)

            Dim Subitems As New List(Of String)(Item.Trim(" "c).Replace(Convert.ToChar(1), ",") _
                .Split(New String() {";"}, StringSplitOptions.RemoveEmptyEntries))

            Item = Item.Trim(" "c).Replace(Convert.ToChar(2), ";")

            If Subitems.Count >= 4 Then
                Dim NewOption As New SpeedyCriterion

                NewOption.Code = Subitems(0)
                NewOption.Description = Subitems(1)
                NewOption.DisplayCode = NewOption.Code

                If NewOption.DisplayCode.Contains("-") Then NewOption.DisplayCode = _
                    NewOption.DisplayCode.Substring(0, NewOption.DisplayCode.IndexOf("-"))

                If Subitems(2).Contains("|") Then
                    NewOption.Template = Subitems(2).Substring(0, Subitems(2).IndexOf("|"))
                    NewOption.Parameter = Subitems(2).Substring(Subitems(2).IndexOf("|") + 1)
                Else
                    NewOption.Template = Subitems(2)
                End If

                NewOption.Message = Subitems(3)
                NewOption.Notify = (Subitems.Count >= 5 AndAlso Subitems(4).ToLower = "notify")
                SpeedyCriteria.Add(NewOption.Code, NewOption)
            End If
        Next Item
    End Sub

    Private Sub SetTags(ByVal Value As String)
        Config.Tags.Clear()

        For Each Item As String In Value.Replace(LF, "").Replace("\,", Convert.ToChar(1)).Split _
            (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

            Config.Tags.Add("{{" & Item.Trim(" "c).Replace(Convert.ToChar(1), ",") & "}}")
        Next Item
    End Sub

    Private Sub SetWatch(ByVal Value As String)
        Config.WatchNotifications = False
        Config.WatchOther = False
        Config.WatchReports = False
        Config.WatchReverts = False
        Config.WatchTags = False
        Config.WatchWarnings = False

        For Each Item As String In Value.Split(","c)
            Item = Item.Trim(","c, " "c, LF).ToLower

            Select Case Item
                Case "reverts" : Config.WatchReverts = True
                Case "other" : Config.WatchOther = True
                Case "notifications" : Config.WatchNotifications = True
                Case "reports" : Config.WatchReports = True
                Case "tags" : Config.WatchTags = True
                Case "warnings" : Config.WatchWarnings = True
            End Select
        Next Item
    End Sub

    Public Sub LoadLocalConfig()
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
                        Case "proxy-enabled" : Config.ProxyEnabled = CBool(OptionValue)
                        Case "proxy-port" : Config.ProxyPort = OptionValue
                        Case "proxy-server" : Config.ProxyServer = OptionValue
                        Case "proxy-userdomain" : Config.ProxyUserDomain = OptionValue
                        Case "proxy-username" : Config.ProxyUsername = OptionValue
                        Case "startup-message" : Config.StartupMessage = CBool(OptionValue)
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

    Private Sub SetShortcutsFromConfig(ByVal Value As String)
        For Each Item As String In Value.Replace(LF, "").Replace("\,", Convert.ToChar(1)).Split _
            (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

            If Item.Contains(";") Then
                Dim ItemKey As String = Item.Substring(0, Item.IndexOf(";")).Trim(" "c).Replace(Convert.ToChar(1), ",")
                Dim ItemValue As String() = Item.Substring(Item.IndexOf(";") + 1).Split(";"c)

                If ShortcutKeys.ContainsKey(ItemKey) Then ShortcutKeys(ItemKey) = New Shortcut _
                    (CType(CInt(ItemValue(0)), Keys), ItemValue(1) <> "0", ItemValue(2) <> "0", ItemValue(3) <> "0")
            End If
        Next Item
    End Sub

    Public Sub SaveLocalConfig()
        'Write to local configuration file
        If MainForm IsNot Nothing Then
            Dim Items As New List(Of String)

            Items.Add("irc:" & CStr(Config.IrcMode).ToLower)
            Items.Add("log-file:" & Config.LogFile)
            Items.Add("project:" & Config.Project)
            Items.Add("proxy-enabled:" & CStr(Config.ProxyEnabled).ToLower)
            Items.Add("proxy-port:" & Config.ProxyPort)
            Items.Add("proxy-server:" & Config.ProxyServer)
            Items.Add("proxy-userdomain:" & Config.ProxyUserDomain)
            Items.Add("proxy-username:" & Config.ProxyUsername)
            Items.Add("startup-message:" & CStr(Config.StartupMessage).ToLower)
            Items.Add("username:" & Config.Username)
            Items.Add("window-height:" & CStr(MainForm.Height))
            Items.Add("window-left:" & CStr(MainForm.Left))
            Items.Add("window-maximize:" & CStr(MainForm.WindowState = FormWindowState.Maximized).ToLower)
            Items.Add("window-top:" & CStr(MainForm.Top))
            Items.Add("window-width:" & CStr(MainForm.Width))

            Dim Shortcuts As New List(Of String)

            For Each Item As KeyValuePair(Of String, Shortcut) In ShortcutKeys
                Shortcuts.Add(Item.Key & ";" & CInt(Item.Value.Key).ToString & ";" & CInt(Item.Value.Control) _
                    .ToString & ";" & CInt(Item.Value.Alt).ToString & ";" & CInt(Item.Value.Shift).ToString)
            Next Item

            Items.Add("shortcuts:" & String.Join(",", Shortcuts.ToArray))

            Dim Summaries As New List(Of String)

            For Each Item As String In ManualRevertSummaries
                Summaries.Add(Item.Replace(",", "\,"))
            Next Item

            Items.Add("revert-summaries:" & String.Join(",", Summaries.ToArray))

            File.WriteAllLines(LocalConfigPath() & LocalConfigLocation, Items.ToArray)
        End If
    End Sub

    Private Sub SetRevertSummaries(ByVal Value As String)
        For Each Item As String In Value.Replace(LF, "").Replace("\,", Convert.ToChar(1)).Split _
            (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

            ManualRevertSummaries.Add(Item.Replace(Convert.ToChar(1), ","))
        Next Item
    End Sub

    Public Sub LoadLists()
        AllLists.Clear()

        'Load lists from application data subfolder
        If Directory.Exists(ListsLocation) Then
            For Each Path As String In Directory.GetFiles(ListsLocation)
                AllLists.Add(Path.Substring(Path.LastIndexOf("\") + 1, Path.Length - Path.LastIndexOf("\") - 5), _
                    New List(Of String)(File.ReadAllLines(Path)))
            Next Path
        End If
    End Sub

    Public Sub SaveLists()
        'Create subfolder if it does not exist
        If Not Directory.Exists(ListsLocation) AndAlso Not Directory.CreateDirectory(ListsLocation).Exists Then
            MessageBox.Show("Unable to save page lists; could not create sub-folder.", "Huggle", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        For Each Path As String In Directory.GetFiles(ListsLocation)
            File.Delete(Path)
        Next Path

        'Write lists to application data subfolder
        For Each List As KeyValuePair(Of String, List(Of String)) In AllLists
            File.WriteAllLines(ListsLocation() & "\" & List.Key & ".txt", List.Value.ToArray)
        Next List
    End Sub

    Private Function ListsLocation() As String
        Return LocalConfigPath() & "\Lists\" & Config.Project
    End Function

    Public Sub LoadQueues()
        Queue.All.Clear()

        'Load queues from application data subfolder
        If Directory.Exists(QueuesLocation) Then
            For Each Path As String In Directory.GetFiles(QueuesLocation)
                Dim Items As New List(Of String)(File.ReadAllLines(Path))
                Dim Queue As Queue = Nothing

                For Each Item As String In Items
                    If Item.Contains(":") Then
                        Dim OptionName As String = Item.Substring(0, Item.IndexOf(":")), _
                            OptionValue As String = Item.Substring(Item.IndexOf(":") + 1)

                        Try
                            Select Case OptionName
                                Case "name" : Queue = New Queue(OptionValue)
                                Case "filter-anonymous" : Queue.FilterAnonymous = CType(OptionValue, QueueFilter)
                                Case "filter-huggle" : Queue.FilterHuggle = CType(OptionValue, QueueFilter)
                                Case "filter-ignored" : Queue.FilterIgnored = CType(OptionValue, QueueFilter)
                                Case "filter-new-pages" : Queue.FilterNewPage = CType(OptionValue, QueueFilter)
                                Case "filter-notifications" : Queue.FilterNotifications = CType(OptionValue, QueueFilter)
                                Case "filter-own-userspace" : Queue.FilterOwnUserspace = CType(OptionValue, QueueFilter)
                                Case "filter-reverts" : Queue.FilterReverts = CType(OptionValue, QueueFilter)
                                Case "list" : If AllLists.ContainsKey(OptionValue) Then Queue.ListName = OptionValue
                                Case "page-regex" : Queue.PageRegex = New Regex(OptionValue)
                                Case "remove-after" : Queue.RemoveAfter = CInt(OptionValue)
                                Case "remove-old" : Queue.RemoveOld = CBool(OptionValue)
                                Case "remove-viewed" : Queue.RemoveViewed = CBool(OptionValue)
                                Case "sort-order" : Queue.SortOrder = CType(OptionValue, QueueSortOrder)
                                Case "spaces" : Queue.Spaces.AddRange(SetQueueSpaces(OptionValue))
                                Case "type" : Queue.Type = SetQueueType(OptionValue)
                                Case "user-regex" : Queue.UserRegex = New Regex(OptionValue)
                            End Select

                        Catch ex As Exception
                            'Ignore malformed config entries
                        End Try
                    End If
                Next Item

                Queue.Reset()
            Next Path
        End If

        If Queue.All.ContainsKey("Filtered edits") Then Queue.Default = Queue.All("Filtered edits")
        SetDefaultQueues()
    End Sub

    Private Sub SetDefaultQueues()
        'Create the default queues if they do not exist
        If Not Queue.All.ContainsKey("Filtered edits") Then
            Queue.Default = New Queue("Filtered edits")
            Queue.Default.Type = QueueType.Live
            Queue.Default.SortOrder = QueueSortOrder.Quality
            Queue.Default.Spaces = New List(Of Space)(New Space() {Space.Article})
            Queue.Default.FilterIgnored = QueueFilter.Exclude
            Queue.Default.FilterOwnUserspace = QueueFilter.Exclude
            Queue.Default.FilterReverts = QueueFilter.Exclude
            Queue.Default.FilterNotifications = QueueFilter.Exclude
            Queue.Default.RemoveOld = True
            Queue.Default.Reset()
        End If

        If Not Queue.All.ContainsKey("New pages") Then
            Dim NewQueue As New Queue("New pages")
            NewQueue.Type = QueueType.Live
            NewQueue.SortOrder = QueueSortOrder.Time
            NewQueue.FilterNewPage = QueueFilter.Require
            NewQueue.Reset()
        End If

        If Not Queue.All.ContainsKey("All edits") Then
            Dim NewQueue As New Queue("All edits")
            NewQueue.Type = QueueType.Live
            NewQueue.SortOrder = QueueSortOrder.Time
            NewQueue.Reset()
        End If
    End Sub

    Private Function SetQueueSpaces(ByVal Value As String) As List(Of Space)
        'Helper function for above
        Dim Spaces As New List(Of Space)

        For Each Item As String In Value.Split(","c)
            If Not Spaces.Contains(Space.GetSpace(CInt(Item))) Then Spaces.Add(Space.GetSpace(CInt(Item)))
        Next Item

        Return Spaces
    End Function

    Private Function SetQueueType(ByVal Value As String) As QueueType
        'Helper function for above
        Select Case Value
            Case "FixedList" : Return QueueType.FixedList
            Case "LiveList" : Return QueueType.LiveList
            Case Else : Return QueueType.Live
        End Select
    End Function

    Public Sub SaveQueues()
        'Create subfolder if it does not exist
        If Not Directory.Exists(QueuesLocation) AndAlso Not Directory.CreateDirectory(QueuesLocation).Exists Then
            MessageBox.Show("Unable to save edit queues; could not create sub-folder.", "Huggle", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        For Each Path As String In Directory.GetFiles(QueuesLocation)
            File.Delete(Path)
        Next Path

        'Write queues to application data subfolder
        For Each Queue As Queue In Queue.All.Values
            Dim Items As New List(Of String)

            Items.Add("name:" & Queue.Name)
            Items.Add("type:" & Queue.Type.ToString)
            Items.Add("filter-anonymous:" & CStr(CInt(Queue.FilterAnonymous)))
            Items.Add("filter-huggle:" & CStr(CInt(Queue.FilterHuggle)))
            Items.Add("filter-ignored:" & CStr(CInt(Queue.FilterIgnored)))
            Items.Add("filter-new-pages:" & CStr(CInt(Queue.FilterNewPage)))
            Items.Add("filter-notifications:" & CStr(CInt(Queue.FilterNotifications)))
            Items.Add("filter-own-userspace:" & CStr(CInt(Queue.FilterOwnUserspace)))
            Items.Add("filter-reverts:" & CStr(CInt(Queue.FilterReverts)))
            Items.Add("remove-after:" & CStr(Queue.RemoveAfter))
            Items.Add("remove-old:" & CStr(Queue.RemoveOld))
            Items.Add("remove-viewed:" & CStr(Queue.RemoveViewed))
            Items.Add("sort-order:" & CStr(CInt(Queue.SortOrder)))

            If Queue.Spaces.Count > 0 Then
                Dim Spaces As New List(Of String)

                For Each Space As Space In Queue.Spaces
                    If Not Spaces.Contains(CStr(Space.Number)) Then Spaces.Add(CStr(Space.Number))
                Next Space

                Items.Add("spaces:" & String.Join(",", Spaces.ToArray))
            End If

            If Queue.ListName IsNot Nothing Then Items.Add("list:" & Queue.ListName)
            If Queue.PageRegex IsNot Nothing Then Items.Add("page-regex:" & Queue.PageRegex.ToString)
            If Queue.UserRegex IsNot Nothing Then Items.Add("user-regex:" & Queue.UserRegex.ToString)

            File.WriteAllLines(QueuesLocation() & "\" & Queue.Name & ".txt", Items.ToArray)
        Next Queue
    End Sub

    Private Function QueuesLocation() As String
        Return LocalConfigPath() & "\Queues\" & Config.Project
    End Function

End Module
