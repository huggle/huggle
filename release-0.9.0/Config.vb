Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading

Class Configuration

    'Configuration

    'Constants

    Public ReadOnly ContribsBlockSize As Integer = 100
    Public ReadOnly HistoryBlockSize As Integer = 100
    Public ReadOnly HistoryScrollSpeed As Integer = 40
    Public ReadOnly FullHistoryBlockSize As Integer = 500
    Public ReadOnly IrcConnectionTimeout As Integer = 60000
    Public ReadOnly LocalConfigLocation As String = "config.txt"
    Public ReadOnly RequestTimeout As Integer = 30000
    Public ReadOnly QueueSize As Integer = 5000
    Public ReadOnly QueueWidth As Integer = 160
    Public ReadOnly RequestAttempts As Integer = 3
    Public ReadOnly RequestRetryInterval As Integer = 1000
    Public ReadOnly ShortWikiPath As String = "wiki/"
    Public ReadOnly GlobalConfigLocation As String = "Huggle/Config"
    Public ReadOnly WikiPath As String = "w/"

    Public ReadOnly EditTypes As String() = _
        {"blocknote", "deletenote", "deletetag", "deletereq", "manual", "message", "note", "prodtag", _
         "protectreq", "report", "revert", "speedytag", "tag", "warning"}

    'Values only used at runtime

    Public ConfigChanged As Boolean
    Public ConfigVersion As New Version(0, 0, 0)
    Public DefaultLanguage As String = "en"
    Public Languages As New List(Of String)
    Public LatestVersion As New Version(0, 0, 0)
    Public Messages As New Dictionary(Of String, Dictionary(Of String, String))
    Public Password As String
    Public Version As New Version(Application.ProductVersion)
    Public WarningMessages As New Dictionary(Of String, String)

    'Values stored in local config file

    Public Language As String
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
    Public ConfirmPage As Boolean = True
    Public ConfirmRange As Boolean = True
    Public ConfirmSame As Boolean = True
    Public ConfirmSelfRevert As Boolean = True
    Public ConfirmWarned As Boolean = True
    Public CountBatchSize As Integer = 20
    Public CustomRevertSummaries As New List(Of String)
    Public DefaultQueue As String
    Public DefaultQueue2 As String
    Public DefaultSummary As String = ""
    Public Delete As Boolean
    Public DiffFontSize As String = "8"
    Public DocsLocation As String = "http://en.wikipedia.org/wiki/Wikipedia:Huggle"
    Public DownloadLocation As String = "http://huggle.googlecode.com/files/huggle $1.exe"
    Public Email As Boolean
    Public EmailSubject As String
    Public Enabled As Boolean
    Public EnabledForAll As Boolean
    Public ExtendReports As Boolean
    Public FeedbackLocation As String
    Public GoToPages As New List(Of String)
    Public IconsLocation As String = "http://en.wikipedia.org/wiki/Wikipedia:Huggle/Icons"
    Public IfdLocation As String
    Public IgnoredPages As New List(Of String)
    Public Initialised As Boolean
    Public IrcChannel As String
    Public IrcMode As Boolean
    Public IrcPort As Integer = 6667
    Public IrcServer As String
    Public IrcUsername As String
    Public LocalizatonPath As String = "Huggle/Localization/"
    Public LogFile As String
    Public MaxReportLinks As Integer = 6
    Public MfdLocation As String
    Public Minor As New Dictionary(Of String, Boolean)
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
    Public Project As String
    Public Projects As New Dictionary(Of String, String)
    Public ProjectConfigLocation As String
    Public PromptForBlock As Boolean = True
    Public PromptForReport As Boolean
    Public Protect As Boolean
    Public ProtectionReason As String
    Public ProtectionRequests As Boolean
    Public ProtectionRequestPage As String
    Public ProtectionRequestReason As String
    Public ProtectionRequestSummary As String
    Public ProtectionTime As String = "indefinite"
    Public QueueBuilderLimit As Integer = 10
    Public QuickSight As Boolean
    Public RcBlockSize As Integer = 100
    Public ReportExtendSummary As String
    Public ReportLinkDiffs As Boolean = True
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
    Public Rights As New List(Of String)
    Public RollbackSummary As String
    Public SaveConfig As Boolean = True
    Public SensitiveAddresses As New Dictionary(Of String, String)
    Public SharedIPTemplates As New List(Of String)
    Public ShowNewEdits As Boolean = True
    Public ShowLog As Boolean = True
    Public ShowNewMessages As Boolean = True
    Public ShowQueue As Boolean = True
    Public ShowToolTips As Boolean = True
    Public ShowTwoQueues As Boolean
    Public Sight As Boolean
    Public SingleRevertSummary As String
    Public SockReports As Boolean
    Public SockReportLocation As String
    Public Speedy As Boolean
    Public SpeedyCriteria As New Dictionary(Of String, SpeedyCriterion)
    Public SpeedyDeleteSummary As String
    Public SpeedyMessageSummary As String
    Public SpeedyMessageTitle As String
    Public SpeedySummary As String
    Public StartupPage As String
    Public Summary As String
    Public Tags As New List(Of String)
    Public TagSummaries As New List(Of String)
    Public TemplateMessageSummary As String
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
    Public UsernameListed As Boolean
    Public UseIrc As Boolean = True
    Public UseRollback As Boolean = True
    Public UserTalkSummaries As New Dictionary(Of Regex, UserLevel)
    Public VandalReportReason As String
    Public WarningAge As Integer = 36
    Public WarningImLevel As Boolean
    Public WarningMode As String
    Public WarningTypes As New Dictionary(Of String, String)
    Public Watch As New Dictionary(Of String, Boolean)
    Public WhitelistEditCount As Integer = 500
    Public WhitelistLocation As String
    Public WhitelistSplit As Boolean
    Public WhitelistTimestamps As New Dictionary(Of String, String)
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

    Public WatchDelete As Boolean
    Public Welcome As String
    Public WelcomeAnon As String
    Public WelcomeSummary As String

End Class