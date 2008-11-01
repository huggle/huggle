Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading

Class Configuration

    'Configuration

    Public Sub New()
#If DEBUG Then
        'If the app is in debug mode add a localhost wiki to the project list
        If Not Projects.ContainsKey("localhost") Then
            Dim NewProject As New Project
            NewProject.Name = "localhost"
            NewProject.Path = "http://localhost/w/"
            Projects.Add("localhost", NewProject)
        End If
#End If
    End Sub

    'Constants

    Public ReadOnly ContribsBlockSize As Integer = 100
    Public ReadOnly HistoryBlockSize As Integer = 100
    Public ReadOnly HistoryScrollSpeed As Integer = 25
    Public ReadOnly IrcConnectionTimeout As Integer = 60000
    Public ReadOnly RequestTimeout As Integer = 30000
    Public ReadOnly QueueSize As Integer = 5000
    Public ReadOnly QueueWidth As Integer = 160
    Public ReadOnly RequestAttempts As Integer = 3
    Public ReadOnly RequestRetryInterval As Integer = 1000
    Public ReadOnly LocalConfigLocation As String = "\config.txt"
    Public ReadOnly GlobalConfigLocation As String = "Huggle/Config"

    'Values only used at runtime

    Public ConfigChanged As Boolean
    Public ConfigVersion As New Version(0, 0, 0)
    Public DefaultLanguage As String = "en"
    Public DefaultProject As Project
    Public Languages As New List(Of String)
    Public LatestVersion As New Version(0, 0, 0)
    Public Messages As New Dictionary(Of String, Dictionary(Of String, String))
    Public Password As String
    Public Projects As New Dictionary(Of String, Project)
    Public Version As New Version(Application.ProductVersion)
    Public WarningMessages As New Dictionary(Of String, String)

    'Values stored in local config file

    Public Language As String
    Public Project As Project
    Public ProxyUsername As String
    Public ProxyUserDomain As String
    Public ProxyServer As String
    Public ProxyPort As String
    Public ProxyEnabled As Boolean
    Public RememberMe As Boolean = True
    Public RememberPassword As Boolean
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
    Public AssistedSummaries As New List(Of String)
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
    Public ConfirmRange As Boolean = True
    Public ConfirmSame As Boolean = True
    Public ConfirmSelfRevert As Boolean = True
    Public ConfirmWarned As Boolean = True
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
    Public IrcMode As Boolean
    Public IrcPort As Integer = 6667
    Public IrcServer As String
    Public IrcUsername As String
    Public LocalizatonPath As String = "Huggle/Localization/"
    Public LogFile As String
    Public MaxAIVDiffs As Integer = 8
    Public MfdLocation As String
    Public MinorManual As Boolean
    Public MinorNotifications As Boolean
    Public MinorOther As Boolean
    Public MinorReports As Boolean
    Public MinorReverts As Boolean = True
    Public MinorTags As Boolean
    Public MinorWarnings As Boolean
    Public MinVersion As Version
    Public MinWarningWait As Integer = 10
    Public MonthHeadings As Boolean
    Public MultipleRevertSummaryParts As List(Of String)
    Public OpenInBrowser As Boolean
    Public PageBlankedPattern As Regex
    Public PageCreatedPattern As Regex
    Public PageRedirectedPattern As Regex
    Public PageReplacedPattern As Regex
    Public Patrol As Boolean
    Public PatrolSpeedy As Boolean
    Public Preloads As Integer = 2
    Public Prod As Boolean
    Public ProdMessage As String
    Public ProdMessageSummary As String
    Public ProdMessageTitle As String
    Public ProdSummary As String
    Public ProjectConfigLocation As String
    Public PromptForBlock As Boolean = True
    Public PromptForReport As Boolean
    Public Protect As Boolean
    Public ProtectionReason As String
    Public ProtectionRequests As Boolean
    Public ProtectionRequestPage As String
    Public ProtectionRequestReason As String
    Public ProtectionRequestSummary As String
    Public QueueBuilderLimit As Integer = 10
    Public RcBlockSize As Integer = 100
    Public ReportExtendSummary As String
    Public ReportLinkDiffs As Boolean
    Public ReportReason As String = "vandalism"
    Public ReportSummary As String
    Public RequireAdmin As Boolean
    Public RequireAutoconfirmed As Boolean
    Public RequireConfig As Boolean
    Public RequireEdits As Integer
    Public RequireRollback As Boolean
    Public RequireTime As Integer
    Public RevertPatterns As New List(Of Regex)
    Public RevertSummary As String
    Public RevertSummaries As New List(Of String)
    Public RfdLocation As String
    Public RightAlignQueue As Boolean
    Public RollbackSummary As String
    Public SensitiveAddresses As New List(Of String)
    Public SharedIPTemplates As New List(Of String)
    Public ShowNewEdits As Boolean = True
    Public ShowLog As Boolean = True
    Public ShowQueue As Boolean = True
    Public ShowToolTips As Boolean = True
    Public SingleRevertSummary As String
    Public Speedy As Boolean
    Public SpeedyDeleteSummary As String
    Public SpeedyMessageSummary As String
    Public SpeedyMessageTitle As String
    Public SpeedySummary As String
    Public Summary As String
    Public Tags As New List(Of String)
    Public TemplateMessages As New List(Of String)
    Public TemplateMessagesGlobal As New List(Of String)
    Public TfdLocation As String
    Public TranslateLocation As String = "http://meta.wikimedia.org/wiki/Huggle/Localization"
    Public TrayIcon As Boolean
    Public TRR As Boolean
    Public TRRLocation As String
    Public UAA As Boolean
    Public UAALocation As String
    Public UAABotLocation As String
    Public UndoSummary As String
    Public UpdateWhitelist As Boolean
    Public UpdateWhitelistManual As Boolean
    Public UseAdminFunctions As Boolean = True
    Public UserAgent As String = "Huggle/" & Version.Major.ToString & "." & Version.Minor.ToString & "." & _
        Version.Build.ToString & " http://en.wikipedia.org/wiki/Wikipedia:Huggle"
    Public UserConfigLocation As String = "Special:Mypage/huggle.css"
    Public UserListLocation As String
    Public UserListUpdateSummary As String
    Public UseRollback As Boolean = True
    Public WarningAge As Integer = 36
    Public WarningImLevel As Boolean
    Public WarningMode As String
    Public WarningSeries As New List(Of String)
    Public WatchManual As Boolean
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

End Class

Module ConfigIO

    Public Function ProcessConfigFile(ByVal File As String) As Dictionary(Of String, String)
        'Remove comments and lines without ':', combine multi-line options, replace \n with line break,
        'strip leading/trailing whitespace and split into key/value pairs

        Dim Items As New List(Of String)(File.Replace(CR, "").Replace(Tab, "    ").Split(LF))
        Dim Result As New Dictionary(Of String, String)

        Dim i As Integer = 0

        While i < Items.Count
            If i > 0 AndAlso Items(i).StartsWith(" ") Then
                Items(i - 1) &= Items(i).Trim(" "c)
                Items.RemoveAt(i)
            ElseIf Items(i).StartsWith("#") OrElse Items(i).StartsWith("<pre") OrElse Not Items(i).Contains(":") Then
                Items.RemoveAt(i)
            Else
                Items(i) = Items(i).Replace("\n", LF).Trim(" "c)
                i += 1
            End If
        End While

        i = 0

        While i < Items.Count
            Dim ItemName As String = Items(i).Substring(0, Items(i).IndexOf(":")).ToLower
            Dim ItemValue As String = Items(i).Substring(Items(i).IndexOf(":") + 1)

            If Result.ContainsKey(ItemName) _
                Then Log("Warning: Duplicate definition for option '" & ItemName & "' in configuration file") _
                Else Result.Add(ItemName, ItemValue)

            i += 1
        End While

        Return Result
    End Function

    Public Sub SetGlobalConfigOption(ByVal Name As String, ByVal Value As String)
        'Global config only
        Select Case Name
            Case "enable-all" : Config.EnabledForAll = CBool(Value)
            Case "config" : Config.ProjectConfigLocation = Value
            Case "documentation" : Config.DocsLocation = Value
            Case "feedback" : Config.FeedbackLocation = Value
            Case "irc-server" : Config.IrcServer = Value
            Case "min-version" : Config.MinVersion = ParseVersion(Value)
            Case "sensitive-addresses" : SetSensitiveAddresses(Value)
            Case "user-agent" : Config.UserAgent = Value.Replace("$1", Config.Version.ToString)
            Case "user-config" : Config.UserConfigLocation = Value
            Case "version" : Config.LatestVersion = ParseVersion(Value)
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
            Case "aiv-extend-reports" : Config.ExtendReports = CBool(Value)
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
            Case "confirm-range" : Config.ConfirmRange = CBool(Value)
            Case "confirm-same" : Config.ConfirmSame = CBool(Value)
            Case "confirm-self-revert" : Config.ConfirmSelfRevert = CBool(Value)
            Case "confirm-warned" : Config.ConfirmWarned = CBool(Value)
            Case "default-summary" : Config.DefaultSummary = Value
            Case "diff-font-size" : Config.DiffFontSize = Value
            Case "irc-port" : Config.IrcPort = CInt(Value)
            Case "minor" : SetMinor(Value)
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
            Case "report" : SetReport(Value)
            Case "report-summary" : Config.ReportSummary = Value
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

            Case "warn-summary" : Config.WarnSummary = Value
            Case "warn-summary-2" : Config.WarnSummary2 = Value
            Case "warn-summary-3" : Config.WarnSummary3 = Value
            Case "warn-summary-4" : Config.WarnSummary4 = Value

            Case Else
                For Each Item2 As String In Config.WarningSeries
                    If Name.StartsWith(Item2) Then
                        Select Case Name.Substring(Item2.Length)
                            Case "1", "2", "3", "4", "4im"
                                Config.WarningMessages.Add(Name, Value)
                        End Select

                        Exit For
                    End If
                Next Item2
        End Select
    End Sub

    Public Sub SetProjectConfigOption(ByVal Name As String, ByVal Value As String)
        'Project config only
        Select Case Name
            Case "3rr" : Config.TRRLocation = Value
            Case "afd" : Config.AfdLocation = Value
            Case "aiv" : Config.AIVLocation = Value
            Case "aivbot" : Config.AIVBotLocation = Value
            Case "aiv-extend" : Config.ExtendReports = CBool(Value)
            Case "aiv-extend-summary" : Config.ReportExtendSummary = Value
            Case "aiv-link-diffs" : Config.ReportLinkDiffs = CBool(Value)
            Case "aiv-single-note" : Config.AivSingleNote = Value
            Case "approval" : Config.Approval = CBool(Value)
            Case "assisted-summaries" : Config.AssistedSummaries = GetList(Value)
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
            Case "manual-revert-summary" : Config.RevertSummary = Value
            Case "multiple-revert-summary-parts" : Config.MultipleRevertSummaryParts = GetList(Value)
            Case "mfd" : Config.MfdLocation = Value
            Case "namespace-aliases" : SetNamespaceAliases(GetList(Value))
            Case "namespace-names" : SetNamespaceNames(GetList(Value))
            Case "page-blanked-pattern" : Config.PageBlankedPattern = New Regex(Value, RegexOptions.Compiled)
            Case "page-created-pattern" : Config.PageCreatedPattern = New Regex(Value, RegexOptions.Compiled)
            Case "page-redirected-pattern" : Config.PageRedirectedPattern = New Regex(Value, RegexOptions.Compiled)
            Case "page-replaced-pattern" : Config.PageReplacedPattern = New Regex(Value, RegexOptions.Compiled)
            Case "patrol" : Config.Patrol = CBool(Value)
            Case "protect" : Config.Protect = CBool(Value)
            Case "protection-request-page" : Config.ProtectionRequestPage = Value
            Case "protection-request-reason" : Config.ProtectionRequestReason = Value
            Case "protection-request-summary" : Config.ProtectionRequestSummary = Value
            Case "rc-block-size" : Config.RcBlockSize = CInt(Value)
            Case "require-admin" : Config.RequireAdmin = CBool(Value)
            Case "require-config" : Config.RequireConfig = CBool(Value)
            Case "require-edits" : Config.RequireEdits = CInt(Value)
            Case "require-rollback" : Config.RequireRollback = CBool(Value)
            Case "require-time" : Config.RequireTime = CInt(Value)
            Case "revert-patterns" : SetRevertPatterns(GetList(Value))
            Case "revert-summaries" : Config.CustomRevertSummaries = GetList(Value)
            Case "rollback-summary" : Config.RollbackSummary = Value
            Case "rfd" : Config.RfdLocation = Value
            Case "shared-ip-templates" : Config.SharedIPTemplates = GetList(Value)
            Case "single-revert-summary" : Config.SingleRevertSummary = Value
            Case "speedy-delete-summary" : Config.SpeedyDeleteSummary = Value
            Case "speedy-options" : SetSpeedyOptions(Value)
            Case "summary" : Config.Summary = Value
            Case "templates" : Config.TemplateMessagesGlobal = GetList(Value)
            Case "tfd" : Config.TfdLocation = Value
            Case "uaa" : Config.UAALocation = Value
            Case "uaabot" : Config.UAABotLocation = Value
            Case "update-whitelist-manual" : Config.UpdateWhitelistManual = CBool(Value)
            Case "userlist" : Config.UserListLocation = Value
            Case "userlist-update-summary" : Config.UserListUpdateSummary = Value
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
        End Select
    End Sub

    Public Sub SetUserConfigOption(ByVal Name As String, ByVal Value As String)
        'User config only
        Select Case Name
            Case "templates" : Config.TemplateMessages = GetList(Value)
            Case "version" : Config.ConfigVersion = New Version(CInt(Value.Substring(0, 1)), _
                CInt(Value.Substring(2, 1)), CInt(Value.Substring(4)), 0)
            Case "revert-summaries" : If Value <> "" Then Config.RevertSummaries = GetList(Value)
        End Select
    End Sub

    Private Sub SetRevertPatterns(ByVal Values As List(Of String))
        For Each Item As String In Values
            Config.RevertPatterns.Add(New Regex("^" & Item & "$", RegexOptions.Compiled Or RegexOptions.IgnoreCase))
        Next Item
    End Sub

    Private Sub SetMinor(ByVal Value As String)
        Config.MinorManual = False
        Config.MinorNotifications = False
        Config.MinorOther = False
        Config.MinorReports = False
        Config.MinorReverts = False
        Config.MinorTags = False
        Config.MinorWarnings = False

        For Each Item As String In Value.Split(","c)
            Item = Item.Trim(","c, " "c, LF).ToLower

            Select Case Item
                Case "manual" : Config.MinorManual = True
                Case "notifications" : Config.MinorNotifications = True
                Case "other" : Config.MinorOther = True
                Case "reports" : Config.MinorReports = True
                Case "reverts" : Config.MinorReverts = True
                Case "tags" : Config.MinorTags = True
                Case "warnings" : Config.MinorWarnings = True
            End Select
        Next Item
    End Sub

    Private Function ParseVersion(ByVal VersionString As String) As Version
        Return New Version(CInt(VersionString.Substring(0, 1)), _
            CInt(VersionString.Substring(2, 1)), CInt(VersionString.Substring(4)))
    End Function

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
        Config.WatchManual = False
        Config.WatchNotifications = False
        Config.WatchOther = False
        Config.WatchReports = False
        Config.WatchReverts = False
        Config.WatchTags = False
        Config.WatchWarnings = False

        For Each Item As String In Value.Split(","c)
            Item = Item.Trim(","c, " "c, LF).ToLower

            Select Case Item
                Case "manual" : Config.WatchManual = True
                Case "notifications" : Config.WatchNotifications = True
                Case "other" : Config.WatchOther = True
                Case "reports" : Config.WatchReports = True
                Case "reverts" : Config.WatchReverts = True
                Case "tags" : Config.WatchTags = True
                Case "warnings" : Config.WatchWarnings = True
            End Select
        Next Item
    End Sub

    Public Sub LoadLocalConfig()
        'Read from local configuration file

        If Not File.Exists(LocalConfigPath() & Config.LocalConfigLocation) _
            Then File.WriteAllText(LocalConfigPath() & Config.LocalConfigLocation, My.Resources.DefaultLocalConfig)

        QueueNames.Clear()
        InitialiseShortcuts()

        If File.Exists(LocalConfigPath() & Config.LocalConfigLocation) Then
            For Each Item As KeyValuePair(Of String, String) In _
                ProcessConfigFile(File.ReadAllText(LocalConfigPath() & Config.LocalConfigLocation))

                SetLocalConfigOption(Item.Key, Item.Value)
            Next Item
        End If

        'Load projects if not already present; for compatibility with config files from previous versions
        If Config.Projects.Count = 0 Then
            For Each Item As KeyValuePair(Of String, String) In ProcessConfigFile(My.Resources.DefaultLocalConfig)
                SetLocalConfigOption(Item.Key, Item.Value)
            Next Item
        End If

        If Config.Project Is Nothing Then Config.Project = Config.DefaultProject
        If Config.Language Is Nothing Then Config.Language = Config.DefaultLanguage
    End Sub

    Private Sub SetLocalConfigOption(ByVal OptionName As String, ByVal OptionValue As String)
        Select Case OptionName
            Case "irc" : Config.IrcMode = CBool(OptionValue)
            Case "language" : Config.Language = OptionValue
            Case "log-file" : Config.LogFile = OptionValue
            Case "password" : Config.Password = OptionValue : Config.RememberPassword = True
            Case "project" : Config.Project = Config.Projects(OptionValue)
            Case "projects" : SetProjects(OptionValue)
            Case "proxy-enabled" : Config.ProxyEnabled = CBool(OptionValue)
            Case "proxy-port" : Config.ProxyPort = OptionValue
            Case "proxy-server" : Config.ProxyServer = OptionValue
            Case "proxy-userdomain" : Config.ProxyUserDomain = OptionValue
            Case "proxy-username" : Config.ProxyUsername = OptionValue
            Case "queue-right-align" : Config.RightAlignQueue = CBool(OptionValue)
            Case "username" : Config.Username = OptionValue
            Case "window-height" : Config.WindowSize.Height = CInt(OptionValue)
            Case "window-left" : Config.WindowPosition.X = CInt(OptionValue)
            Case "window-maximize" : Config.WindowMaximize = CBool(OptionValue)
            Case "window-top" : Config.WindowPosition.Y = CInt(OptionValue)
            Case "window-width" : Config.WindowSize.Width = CInt(OptionValue)
            Case "shortcuts" : SetShortcutsFromConfig(OptionValue)
            Case "revert-summaries" : SetRevertSummaries(OptionValue)

            Case Else : If OptionName.StartsWith("queues-") _
                Then QueueNames.Add(OptionName.Substring(7), GetList(OptionValue))
        End Select
    End Sub

    Private Sub SetProjects(ByVal Value As String)
        For Each Item As String In Value.Replace(LF, "").Replace("\,", Convert.ToChar(1)).Split _
            (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

            Item = Item.Trim(" "c).Replace(Convert.ToChar(1), ",")

            Dim Subitems As String() = Item.Split(";"c)

            If Subitems.Length >= 2 Then
                Dim NewProject As New Project
                NewProject.Name = Subitems(0).Trim(" "c)
                NewProject.Path = Subitems(1).Trim(" "c)
                If Subitems.Length >= 3 Then NewProject.IrcChannel = Subitems(2).Trim(" "c)

                Config.Projects.Add(NewProject.Name, NewProject)
            End If
        Next Item
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
            Items.Add("language:" & CStr(Config.Language))
            Items.Add("log-file:" & Config.LogFile)
            If Config.RememberPassword Then Items.Add("password:" & Config.Password)

            Items.Add("projects:")

            For Each Project As Project In Config.Projects.Values
                If Project.Name <> "localhost" _
                    Then Items.Add("    " & Project.Name & ";" & Project.Path & ";" & Project.IrcChannel & ",")
            Next Project

            Items.Add("project:" & Config.Project.Name)
            Items.Add("proxy-enabled:" & CStr(Config.ProxyEnabled).ToLower)
            Items.Add("proxy-port:" & Config.ProxyPort)
            Items.Add("proxy-server:" & Config.ProxyServer)
            Items.Add("proxy-userdomain:" & Config.ProxyUserDomain)
            Items.Add("proxy-username:" & Config.ProxyUsername)

            For Each Project As String In QueueNames.Keys
                For Each Item As String In QueueNames(Project)
                    Item = Item.Replace(",", "\,")
                Next Item

                Items.Add("queues-" & Project & ":" & String.Join(",", QueueNames(Project).ToArray))
            Next Project

            Items.Add("queue-right-align:" & CStr(Config.RightAlignQueue).ToLower)
            If Config.RememberMe Then Items.Add("username:" & Config.Username)
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

            For Each Item As String In Config.RevertSummaries
                If Not Summaries.Contains(Item) Then Summaries.Add(Item.Replace(",", "\,"))
            Next Item

            Items.Add("revert-summaries:" & String.Join(",", Summaries.ToArray))

            File.WriteAllLines(LocalConfigPath() & Config.LocalConfigLocation, Items.ToArray)
        End If
    End Sub

    Private Sub SetRevertSummaries(ByVal Value As String)
        For Each Item As String In Value.Replace(LF, "").Replace("\,", Convert.ToChar(1)).Split _
            (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

            Config.RevertSummaries.Add(Item.Replace(Convert.ToChar(1), ","))
        Next Item
    End Sub

    Public Sub LoadLanguages()
        'Load localized message files
        Config.Messages.Clear()
        Config.Languages.Clear()

        If Not Directory.Exists(L10nLocation) OrElse Directory.GetFiles(L10nLocation).Length = 0 Then
            'On first run, use the built-in localization files to get as far as displaying the login screen
            LoadLanguage("en", My.Resources.en)
            LoadLanguage("bg", My.Resources.bg)
            LoadLanguage("de", My.Resources.de)
            LoadLanguage("es", My.Resources.es)
            LoadLanguage("no", My.Resources.no)
            LoadLanguage("pt", My.Resources.pt)
            LoadLanguage("ru", My.Resources.ru)

        Else
            For Each FileName As String In Directory.GetFiles(L10nLocation)
                LoadLanguage(Path.GetFileNameWithoutExtension(FileName), _
                    File.ReadAllText(L10nLocation() & Path.DirectorySeparatorChar & Path.GetFileName(FileName)))
            Next FileName
        End If

        If Config.Language Is Nothing OrElse Not Config.Messages.ContainsKey(Config.Language) _
            Then Config.Language = Config.DefaultLanguage
    End Sub

    Public Sub LoadLanguage(ByVal Name As String, ByVal MessageFile As String)
        'Load message file
        If Not Config.Languages.Contains(Name) Then Config.Languages.Add(Name)
        If Config.Messages.ContainsKey(Name) Then Config.Messages.Remove(Name)
        Config.Messages.Add(Name, New Dictionary(Of String, String))

        If MessageFile IsNot Nothing Then
            For Each Item As String In MessageFile.Split(New String() {LF}, StringSplitOptions.RemoveEmptyEntries)
                If Item.Contains(":") Then
                    Dim MsgName As String = Item.Substring(0, Item.IndexOf(":")).Trim(" "c, CR)
                    Dim MsgValue As String = Item.Substring(Item.IndexOf(":") + 1).Trim(" "c, CR)

                    If Config.Messages(Name).ContainsKey(MsgName) Then
                        Log("Warning: Duplicate definition of message '" & MsgName & "' in language '" & Name & "'")
                    Else
                        Config.Messages(Name).Add(MsgName.ToLower, MsgValue)
                    End If
                End If
            Next Item
        End If
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
        Return LocalConfigPath() & "\Lists\" & Config.Project.Name
    End Function

    Public Sub SetQueues()
        Queue.All.Clear()

        If Not QueueNames.ContainsKey(Config.Project.Name) Then QueueNames.Add(Config.Project.Name, New List(Of String))

        'Load queues from application data subfolder
        If Directory.Exists(QueuesLocation) Then
            For Each Name As String In QueueNames(Config.Project.Name)
                Dim Items As New List(Of String)(File.ReadAllLines(QueuesLocation() & "\" & Name & ".txt"))
                Dim Queue As Queue = Nothing

                For Each Item As String In Items
                    If Item.Contains(":") Then
                        Dim OptionName As String = Item.Substring(0, Item.IndexOf(":")), _
                            OptionValue As String = Item.Substring(Item.IndexOf(":") + 1)

                        Try
                            Select Case OptionName
                                Case "name" : Queue = New Queue(OptionValue)
                                Case "filter-anonymous" : Queue.FilterAnonymous = CType(OptionValue, QueueFilter)
                                Case "filter-assisted" : Queue.FilterAssisted = CType(OptionValue, QueueFilter)
                                Case "filter-bot" : Queue.FilterBot = CType(OptionValue, QueueFilter)
                                Case "filter-huggle" : Queue.FilterHuggle = CType(OptionValue, QueueFilter)
                                Case "filter-ignored" : Queue.FilterIgnored = CType(OptionValue, QueueFilter)
                                Case "filter-me" : Queue.FilterMe = CType(OptionValue, QueueFilter)
                                Case "filter-new-pages" : Queue.FilterNewPage = CType(OptionValue, QueueFilter)
                                Case "filter-notifications" : Queue.FilterNotifications = CType(OptionValue, QueueFilter)
                                Case "filter-own-userspace" : Queue.FilterOwnUserspace = CType(OptionValue, QueueFilter)
                                Case "filter-reverts" : Queue.FilterReverts = CType(OptionValue, QueueFilter)
                                Case "filter-tags" : Queue.FilterTags = CType(OptionValue, QueueFilter)
                                Case "filter-warnings" : Queue.FilterWarnings = CType(OptionValue, QueueFilter)
                                Case "ignore-pages" : Queue.IgnorePages = CBool(OptionValue)
                                Case "list" : If AllLists.ContainsKey(OptionValue) Then Queue.ListName = OptionValue
                                Case "page-regex" : Queue.PageRegex = New Regex(OptionValue, RegexOptions.Compiled)
                                Case "remove-after" : Queue.RemoveAfter = CInt(OptionValue)
                                Case "remove-old" : Queue.RemoveOld = CBool(OptionValue)
                                Case "remove-viewed" : Queue.RemoveViewed = CBool(OptionValue)
                                Case "sort-order" : Queue.SortOrder = CType(OptionValue, QueueSortOrder)
                                Case "spaces" : Queue.Spaces.AddRange(SetQueueSpaces(OptionValue))
                                Case "summary-regex" : Queue.SummaryRegex = New Regex(OptionValue, RegexOptions.Compiled)
                                Case "type" : Queue.Type = SetQueueType(OptionValue)
                                Case "user-regex" : Queue.UserRegex = New Regex(OptionValue, RegexOptions.Compiled)
                            End Select

                        Catch ex As Exception
                            'Ignore malformed config entries
                        End Try
                    End If
                Next Item

                Queue.Reset()
            Next Name
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
            Queue.Default.FilterNotifications = QueueFilter.Exclude
            Queue.Default.FilterOwnUserspace = QueueFilter.Exclude
            Queue.Default.FilterReverts = QueueFilter.Exclude
            Queue.Default.FilterTags = QueueFilter.Exclude
            Queue.Default.FilterWarnings = QueueFilter.Exclude
            Queue.Default.IgnorePages = True
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
            NewQueue.Preload = False
            NewQueue.Reset()
        End If

        If Not Queue.All.ContainsKey("My edits") Then
            Dim NewQueue As New Queue("My edits")
            NewQueue.Type = QueueType.Live
            NewQueue.SortOrder = QueueSortOrder.Time
            NewQueue.FilterMe = QueueFilter.Require
            NewQueue.Preload = False
            NewQueue.RemoveViewed = False
            NewQueue.Reset()
        End If

        If Not Queue.All.ContainsKey("Huggle edits") Then
            Dim NewQueue As New Queue("Huggle edits")
            NewQueue.Type = QueueType.Live
            NewQueue.SortOrder = QueueSortOrder.Time
            NewQueue.FilterHuggle = QueueFilter.Require
            NewQueue.Reset()
        End If

        If Not Queue.All.ContainsKey("All assisted edits") Then
            Dim NewQueue As New Queue("All assisted edits")
            NewQueue.Type = QueueType.Live
            NewQueue.SortOrder = QueueSortOrder.Time
            NewQueue.FilterAssisted = QueueFilter.Require
            NewQueue.FilterBot = QueueFilter.Exclude
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
            Items.Add("filter-assisted:" & CStr(CInt(Queue.FilterAssisted)))
            Items.Add("filter-bot:" & CStr(CInt(Queue.FilterBot)))
            Items.Add("filter-huggle:" & CStr(CInt(Queue.FilterHuggle)))
            Items.Add("filter-ignored:" & CStr(CInt(Queue.FilterIgnored)))
            Items.Add("filter-me:" & CStr(CInt(Queue.FilterMe)))
            Items.Add("filter-new-pages:" & CStr(CInt(Queue.FilterNewPage)))
            Items.Add("filter-notifications:" & CStr(CInt(Queue.FilterNotifications)))
            Items.Add("filter-own-userspace:" & CStr(CInt(Queue.FilterOwnUserspace)))
            Items.Add("filter-reverts:" & CStr(CInt(Queue.FilterReverts)))
            Items.Add("filter-tags:" & CStr(CInt(Queue.FilterTags)))
            Items.Add("filter-warnings:" & CStr(CInt(Queue.FilterWarnings)))
            Items.Add("ignore-pages:" & CStr(Queue.IgnorePages).ToLower)

            If Queue.ListName IsNot Nothing Then Items.Add("list:" & Queue.ListName)
            If Queue.PageRegex IsNot Nothing Then Items.Add("page-regex:" & Queue.PageRegex.ToString)

            Items.Add("remove-after:" & CStr(Queue.RemoveAfter).ToLower)
            Items.Add("remove-old:" & CStr(Queue.RemoveOld).ToLower)
            Items.Add("remove-viewed:" & CStr(Queue.RemoveViewed))
            Items.Add("sort-order:" & CStr(CInt(Queue.SortOrder)))

            If Queue.Spaces.Count > 0 Then
                Dim Spaces As New List(Of String)

                For Each Space As Space In Queue.Spaces
                    If Space IsNot Nothing AndAlso Not Spaces.Contains(CStr(Space.Number)) _
                        Then Spaces.Add(CStr(Space.Number))
                Next Space

                Items.Add("spaces:" & String.Join(",", Spaces.ToArray))
            End If

            If Queue.SummaryRegex IsNot Nothing Then Items.Add("summary-regex:" & Queue.SummaryRegex.ToString)
            If Queue.UserRegex IsNot Nothing Then Items.Add("user-regex:" & Queue.UserRegex.ToString)

            File.WriteAllLines(QueuesLocation() & "\" & Queue.Name & ".txt", Items.ToArray)
        Next Queue
    End Sub

    Private Function QueuesLocation() As String
        Return LocalConfigPath() & "\Queues\" & Config.Project.Name
    End Function

    Public Function L10nLocation() As String
        Return LocalConfigPath() & "\Localization"
    End Function

End Module
