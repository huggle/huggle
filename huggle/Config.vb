'This is a source code or part of Huggle project
'Edit.vb
'This file contains code for configuration actions
'last modified by Addshore

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
Imports System.Text.RegularExpressions
Imports System.Threading

Class Configuration

    'Configuration

    'Constants

    Public ReadOnly ContribsBlockSize As Integer = 100
    Public ReadOnly HistoryBlockSize As Integer = 200
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
    Public Beta As Boolean = False

    Public ReadOnly EditTypes As String() = _
        {"blocknote", "deletenote", "deletetag", "deletereq", "manual", "message", "note", "prodtag", _
         "protectreq", "report", "revert", "speedytag", "tag", "warning"}

    Public Devs As Boolean = True

    'Values only used at runtime

    Public ConfigChanged As Boolean = False
    Public ConfigVersion As New Version(0, 0, 0)
    Public DefaultLanguage As String = "en"
    Public Languages As New List(Of String)
    Public LatestVersion As New Version(0, 0, 0)
    Public Messages As New Dictionary(Of String, Dictionary(Of String, String))
    Public Password As String = "PW"
    Public Version As New Version(Application.ProductVersion)
    Public WarningMessages As New Dictionary(Of String, String)

    'Values stored in local config file

    Public Language As String
    Public ProxyUsername As String = ""
    Public ProxyUserDomain As String = ""
    Public ProxyServer As String = ""
    Public ProxyPort As String = ""
    Public ProxyEnabled As Boolean = False
    Public RememberMe As Boolean = True
    Public RememberPassword As Boolean = False
    Public Username As String = ""
    Public WindowMaximize As Boolean = True
    Public WindowPosition As New Point
    Public WindowSize As New Size
    Public WhitelistUsed As Boolean = True

    'Values changeable through global / project / user config pages

    Public AfdLocation As String = ""
    Public AIV As Boolean = False
    Public AIVBotLocation As String = ""
    Public AIVLocation As String = ""
    Public AivSingleNote As String
    Public Approval As Boolean = False
    Public AssistedSummaries As New List(Of String)
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
    Public ConfigSummary As String = ""
    Public ConfirmIgnored As Boolean = True
    Public ConfirmMultiple As Boolean = False
    Public ConfirmPage As Boolean = True
    Public ConfirmRange As Boolean = True
    Public ProdLogs_Name As String = "ProdLogs"
    Public ProdLogs As Boolean = False
    Public SlowIrc As Boolean = True
    Public ConfirmSame As Boolean = True
    Public ConfirmSelfRevert As Boolean = True
    Public ConfirmWarned As Boolean = True
    Public CountBatchSize As Integer = 20
    Public CustomRevertSummaries As New List(Of String)
    Public DefaultQueue As String = ""
    Public WhitelistUrl As String = "http://toolserver.org/~petrb/huggle/wl.php"
    Public DefaultQueue2 As String
    Public DefaultSummary As String = ""
    Public Delete As Boolean = False
    Public Platform As Long = 86
    Public DiffFontSize As String = "8"
    Public DocsLocation As String = "http://en.wikipedia.org/wiki/Wikipedia:Huggle"
    Public DownloadLocation As String = "http://huggle.googlecode.com/files/huggle $1.exe"
    Public Downloadloc64 As String = "http://huggle.googlecode.com/files/huggle $1x64.exe"
    Public Email As Boolean
    Public EmailSubject As String = ""
    Public TestWp As String = "http://huggle.wmflabs.org/"
    Public Enabled As Boolean
    Public EnabledForAll As Boolean = False
    Public ExtendReports As Boolean = False
    Public FeedbackLocation As String = ""
    Public GoToPages As New List(Of String)
    Public IconsLocation As String = "http://en.wikipedia.org/wiki/Wikipedia:Huggle/Icons"
    Public IfdLocation As String = ""
    Public IgnoredPages As New List(Of String)
    Public Initialised As Boolean = False
    Public IrcChannel As String = ""
    Public IrcMode As Boolean = False
    Public IrcPort As Integer = 6667
    Public IrcServer As String = ""
    Public IrcServerName As String = ""
    Public IrcUsername As String = ""
    Public LocalizatonPath As String = "Huggle/Localization/"
    Public LogFile As String = ""
    Public MaxReportLinks As Integer = 6
    Public MfdLocation As String = ""
    Public Minor As New Dictionary(Of String, Boolean)
    Public MinVersion As Version
    Public MinWarningWait As Integer = 10
    Public MonthHeadings As Boolean = False
    Public MultipleRevertSummaryParts As List(Of String)
    Public OpenInBrowser As Boolean
    Public PageBlankedPattern As Regex
    Public PageCreatedPattern As Regex
    Public PageRedirectedPattern As Regex
    Public PageReplacedPattern As Regex
    Public Patrol As Boolean = False
    Public PatrolSpeedy As Boolean = False
    Public Preloads As Integer = 2
    Public Prod As Boolean = False
    Public ProdMessage As String
    Public ProdMessageSummary As String = ""
    Public ProdMessageTitle As String = ""
    Public ProdSummary As String = ""
    Public Project As String = ""
    Public Projects As New Dictionary(Of String, String)
    Public ProjectConfigLocation As String
    Public PromptForBlock As Boolean = True
    Public PromptForReport As Boolean = False
    Public Csd_Log_Page As String = ""
    Public Protect As Boolean = False
    Public ProtectionReason As String = ""
    Public ProtectionRequests As Boolean = False
    Public ProtectionRequestPage As String = ""
    Public ProtectionRequestReason As String = ""
    Public ProtectionRequestSummary As String = ""
    Public ProtectionTime As String = "indefinite"
    Public QueueBuilderLimit As Integer = 10
    Public QuickSight As Boolean
    Public RcBlockSize As Integer = 100
    Public ReportExtendSummary As String
    Public ReportLinkDiffs As Boolean = True
    Public ReportSummary As String = ""
    Public RequireAdmin As Boolean = False
    Public RequireAutoconfirmed As Boolean = False
    Public RequireConfig As Boolean
    Public RequireEdits As Integer = 0
    Public RequireRev As Boolean = False
    Public RequireRollback As Boolean = False
    Public RequireTime As Integer
    Public RevertPatterns As New List(Of Regex)
    Public RevertSummary As String = ""
    Public RevertSummaries As New List(Of String)
    Public RfdLocation As String = ""
    Public RightAlignQueue As Boolean = False
    Public Rights As New List(Of String)
    Public RollbackSummary As String = ""
    Public RightPending As Boolean = False
    Public SaveConfig As Boolean = True
    Public SensitiveAddresses As New Dictionary(Of String, String)
    Public SharedIPTemplates As New List(Of String)
    Public ShowNewEdits As Boolean = True
    Public ShowLog As Boolean = True
    Public ShowNewMessages As Boolean = True
    Public ShowQueue As Boolean = True
    Public ShowToolTips As Boolean = True
    Public ShowTwoQueues As Boolean = False
    Public Sight As Boolean = False
    Public SingleRevertSummary As String
    Public SockReports As Boolean
    Public SockReportLocation As String
    Public Speedy As Boolean = False
    Public SpeedyCriteria As New Dictionary(Of String, SpeedyCriterion)
    Public SpeedyDeleteSummary As String = ""
    Public AssociatedDeletion As String = "G8 - nonexistent dependency"
    Public SpeedyMessageSummary As String = ""
    Public SpeedyMessageTitle As String = ""
    Public SpeedySummary As String = ""
    Public StartupPage As String = "Project:HG"
    Public Summary As String = ""
    Public Tags As New List(Of String)
    Public TagSummaries As New List(Of String)
    Public TemplateMessageSummary As String
    Public TemplateMessages As New List(Of String)
    Public TemplateMessagesGlobal As New List(Of String)
    Public TemplateSummary As New Dictionary(Of String, String)
    Public GlobalSumm As New Dictionary(Of String, String)
    Public TfdLocation As String = ""
    Public TranslateLocation As String = "http://meta.wikimedia.org/wiki/Huggle/Localization"
    Public TrayIcon As Boolean = True
    Public TRR As Boolean = False
    Public WelcomesList As New Dictionary(Of String, String)
    Public WelcomeEnabled As Boolean = False
    Public TRRLocation As String = ""
    Public UAA As Boolean = False
    Public UAALocation As String = ""
    Public UAABotLocation As String = ""
    Public UndoSummary As String
    Public UpdateWhitelist As Boolean = False
    Public UpdateWhitelistManual As Boolean = False
    Public UseAdminFunctions As Boolean = True
    Public UserAgent As String = "Huggle/" & Version.Major.ToString & "." & Version.Minor.ToString & "." & _
        Version.Build.ToString & " http://en.wikipedia.org/wiki/Wikipedia:Huggle"
    Public UserConfigLocation As String = "Special:Mypage/huggle.css"
    Public UserListLocation As String
    Public UserListUpdateSummary As String = ""
    Public UsernameListed As Boolean = False
    Public UseIrc As Boolean = True
    Public UseRollback As Boolean = True
    Public Agf As New List(Of String)
    Public UsePending As Boolean = False
    Public UserTalkSummaries As New Dictionary(Of Regex, UserLevel)
    Public VandalReportReason As String = ""
    Public UseCSummaries As Boolean = False
    Public WarningAge As Integer = 36
    Public WarningImLevel As Boolean = False
    Public WarningMode As String = ""
    Public WarningTypes As New Dictionary(Of String, String)
    Public Watch As New Dictionary(Of String, Boolean)
    Public WhitelistEditCount As Integer = 500
    Public WhitelistLocation As String = ""
    Public WhitelistSplit As Boolean = False
    Public WhitelistTimestamps As New Dictionary(Of String, String)
    Public WhitelistUpdateSummary As String = ""
    Public Xfd As Boolean = False
    Public XfdDiscussionSummary As String = ""
    Public XfdLogSummary As String = ""
    Public XfdMessage As String = ""
    Public XfdMessageSummary As String = ""
    Public TemplatePs As Boolean = False
    Public XfdMessageTitle As String = ""
    Public XfdSummary As String = ""
    Public WriteUser As Boolean = False
    Public RevisionAccess As Boolean = False
    Public RevisionR As Boolean = False

    Public WelcomeUse As Boolean = False
    Public WelcomeString As New Dictionary(Of String, String)
    Public WarnSummary As String = ""
    Public WarnSummary2 As String = ""
    Public WarnSummary3 As String = ""
    Public WarnSummary4 As String = ""

    Public WatchDelete As Boolean = False
    Public Welcome As String = ""
    Public WelcomeAnon As String
    Public WelcomeSummary As String = ""

End Class