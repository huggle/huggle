//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Addshore

//Copyright (C) 2011 Huggle team

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.


using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace huggle3
{
    static class Config
    {
        public readonly  static int ContribsBlockSize  = 100;
        public readonly  static int HistoryBlockSize  = 200;
        public readonly  static int HistoryScrollSpeed = 40;
        public readonly  static int FullHistoryBlockSize = 500;
        public readonly  static int IrcConnectionTimeout = 60000;
        public readonly  static string LocalConfigLocation = "config.txt";
        public readonly  static int RequestTimeout = 30000;
        public static int QueueSize = 5000;
        public static int HistoryLenght = 1000;
        public static int HistoryTrim = 400;
        public static int QueueWidth = 160; // main form
        public static int RequestAttempts = 3;
        public static int RequestRetryInterval = 1000;
        /// <summary>
        /// Short path for wiki
        /// </summary>
        public readonly static string ShortWikiPath = "wiki/";
        /// <summary>
        /// Location of the global config file
        /// </summary>
        public readonly static string GlobalConfigLocation = "Huggle/Config";
        /// <summary>
        /// Short path for root of wiki
        /// </summary>
        public readonly static string WikiPath = "w/";
        /// <summary>
        /// URL to metawiki
        /// </summary>
        public static string Metawiki = "http://meta.wikimedia.org/";
        /// <summary>
        /// The current whitelist
        /// </summary>
        public static List<string> Whitelist = new List<string>();
        public static Dictionary<string, List<string>> AllLists = new Dictionary<string,List<string>>();

        // Be carefull when changing anything below, never use those variables unless you know what you do:
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// <summary>
        /// Always true on testing or devel
        /// </summary>
        public readonly  static bool Beta = true;
        /// <summary>
        /// never commit changes to this unless you are release manager
        /// </summary>
        public static platform _Platform = platform.windows32;
        // if this is true huggle will bypass some stuff and produce special output
        public static bool devs = true;

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        /// <summary>
        /// Dirrent types of wiki edit
        /// </summary>
        public readonly static string[] EditTypes = { "blocknote", "deletenote", "deletetag", "deletereq", "manual", "message", "note", "prodtag", "protectreq", "report", "revert", "speedytag", "tag", "warning" };

        public static int QueueTop = 80; // location of queue
        public static int QueueLeft = 20; // left


        //////////
        //Values only used at runtime
        //////////

        /// <summary>
        /// Has the user config changed
        /// </summary>
        public static bool ConfigChanged = false;
        public static Version ConfigVersion = new Version(0, 0, 0);
        /// <summary>
        /// Default Language
        /// </summary>
        public static string DefaultLanguage = "en";
        /// <summary>
        /// Pending requests
        /// </summary>
        public static List<request_core.Request> PendingRequests = new List<request_core.Request>();
        public static List<edit> PendingWarnings;
        /// <summary>
        /// Languages info
        /// </summary>
        public static List<string> Languages = new List<string>();
        /// <summary>
        /// Latest version of huggle
        /// </summary>
        public static Version LatestVersion = new Version(0, 0, 0);
        /// <summary>
        /// Content of languages
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> Messages = new Dictionary<string,Dictionary<string,string>>();
        /// <summary>
        /// User password preconfigured with something, to avoid potential
        /// issue with mw api (it must not be blank)
        /// </summary>
        public static string Password = "xx";
        public static Dictionary<string, string> WarningMessages = new Dictionary<string,string>();

        //////////
        //Values stored in local config file
        //////////

        /// <summary>
        /// Language to be used by huggle
        /// </summary>
        public static string Language;
        /// <summary>
        /// Proxy Username
        /// </summary>
        public static string ProxyUsername = "";
        /// <summary>
        /// Proxy User domain
        /// </summary>
        public static string ProxyUserDomain = "";
        /// <summary>
        /// Proxy Server
        /// </summary>
        public static string ProxyServer = "";
        /// <summary>
        /// Proxy Port
        /// </summary>
        public static string ProxyPort = "";
        /// <summary>
        /// Is the proxy enabled
        /// </summary>
        public static bool ProxyEnabled = false;
        /// <summary>
        /// Remeber the user username on login
        /// </summary>
        public static bool RememberMe = true;
        /// <summary>
        /// remeber the user password on login
        /// </summary>
        public static bool RememberPassword = false;
        /// <summary>
        /// Username
        /// </summary>
        public static string Username = "";
        /// <summary>
        /// State
        /// </summary>
        public static bool WindowMaximize = true;
        /// <summary>
        /// Position of main
        /// </summary>
        public static System.Drawing.Point WindowPosition = new System.Drawing.Point();
        /// <summary>
        /// Size
        /// </summary>
        public static System.Drawing.Size WindowSize = new System.Drawing.Size();

        //////////
        //Values changeable through global / project / user config pages
        //////////

        public static string AfdLocation = ""; // location of afd
        public static bool AIV = false; // if AIV is available on project
        public static string AIVBotLocation = ""; // AIV bot page
        public static string AIVLocation = ""; // AIV location
        public static string AivSingleNote; // Note
        public static bool Approval = false; // If requires an approval on special page
        public static List<string> AssistedSummaries = new List<string>(); // Summaries of others
        public static bool AutoAdvance = false; // Automaticaly advance
        public static bool AutoReport  = true; // Report users without prompt
        public static bool AutoWarn  = true; // Automaticaly submit messages to talk
        public static bool AutoWhitelist  = true; // Update whitelist
        public static bool Block = false; // Block is allowed over huggle
        public static List<string> BlockExpiryOptions = new List<string>(); // Options for block
        public static string BlockMessage; // Default ms
        public static bool BlockMessageDefault = true; // Use default message instead of preconfigured ones
        public static string BlockMessageIndef; // Message for indefinite block
        public static string BlockReason; // Default block reason
        public static string BlockSummary; // Summary
        public static string BlockTime = "indefinite"; // Block time
        public static string BlockTimeAnon = "24 hours"; // block time
        public static string CfdLocation; // Location of CFD
        public static string ConfigSummary = ""; // Summary for config
        public static bool ConfirmIgnored = true; // Confirm if revert ignored user
        public static bool ConfirmMultiple = false; // Confirm if do action on more users
        public static bool ConfirmPage = true; // HUH
        public static bool ConfirmRange = true; // Confirm if user belong to same ip range
        public static string ProdLogs_Name = "ProdLogs"; // Logs
        public static bool ProdLogs = false; // Save them
        public static bool SlowIrc = true; // If slow irc mode is active
        public static bool ConfirmSame = true; // Confirm if revert to same id
        public static bool ConfirmSelfRevert = true; // Confirm self
        public static bool ConfirmWarned = true; // Confirm revert of warned user
        public static int CountBatchSize = 20; // comment me
        public static List<string> CustomRevertSummaries = new List<string>(); // comment me
        public static string DefaultQueue = ""; // Default queue 1
        public static string WhitelistUrl = "http://toolserver.org/~petrb/huggle/wl.php"; // whitelist
        public static string DefaultQueue2; // default 2
        public static string DefaultSummary = ""; // Default summary for all edits
        public static bool Delete = false; // Allow deleting of pages
        public static Int32 Platform = 86; // deprecated
        public static string DiffFontSize = "8"; // Size of fonts
        public static string DocsLocation = "http://en.wikipedia.org/wiki/Wikipedia:Huggle"; // Location of manual
        public static string DownloadLocation = "http://huggle.googlecode.com/files/huggle $1.exe"; // Download of x86, deprecated
        public static string Downloadloc64 = "http://huggle.googlecode.com/files/huggle $1x64.exe"; // Download of x64, deprecated
        public static bool Email; // If emails are allowed
        public static string EmailSubject = ""; // comment me
        public static string TestWp = "http://hub.tm-irc.org/test/"; // Huggle wiki
        public static bool Enabled; // If user has config file
        public static bool EnabledForAll = false; // If huggle is enabled for all
        public static bool ExtendReports = false; // comment
        public static string FeedbackLocation = "http://en.wikipedia.org/wiki/WT:HG"; // Location of feedback
        public static List<string> GoToPages = new List<string>(); // comment me
        public static string IconsLocation = "http://en.wikipedia.org/wiki/Wikipedia:Huggle/Icons"; // Locations of graphics
        public static string IfdLocation = ""; // IFD loc
        public static List<string> IgnoredPages = new List<string>(); // Ignored pages
        public static bool Initialised = false; // If huggle is initialised ok
        public static string IrcChannel = ""; // Default irc
        public static bool IrcMode = false; // Using irc?
        public static int IrcPort = 6667; // Port
        public static string IrcServer = ""; // Server name
        public static string IrcServerName = ""; // Server name
        public static string IrcUsername = ""; // Irc user name
        public static string LocalizatonPath = "Huggle/Localization/"; // Localization
        public static string LogFile = ""; // Name of a log file
        public static int MaxReportLinks = 6; // Max
        public static string MfdLocation = ""; // MFD location
        public static Dictionary<string, bool> Minor = new Dictionary<string,bool>(); // Minor edits
        public static Version MinVersion; // Min allowed version
        public static int MinWarningWait = 10; // Wait between warning
        public static bool MonthHeadings = false; // Heading
        public static List<string> MultipleRevertSummaryParts = new List<string>(); // Summary
        public static bool OpenInBrowser = false; // Open link in new tab
        public static Regex PageBlankedPattern; // blanked
        public static Regex PageCreatedPattern; // created
        public static Regex PageRedirectedPattern; // redirect
        public static Regex PageReplacedPattern; // replaced
        public static bool Patrol = false; // Patrol tool
        public static bool PatrolSpeedy = false; // Patrol
        public static int Preloads = 2; // How many preloads to get
        public static bool Prod = false; // prod
        public static string ProdMessage; // Message for prod
        public static string ProdMessageSummary = ""; // Summary
        public static string ProdMessageTitle = ""; // Prod message title
        public static string ProdSummary = ""; // Prod summary when editing log
        public static string Project = ""; // Current wiki
        public static Dictionary<string, string> Projects = new Dictionary<string, string>(); //  wikis
        public static string ProjectConfigLocation; // Location of config
        public static bool PromptForBlock = true; // Ask when blocking people
        public static bool PromptForReport = false; // Prompt
        public static string Csd_Log_Page = ""; // Log page for csd
        public static bool Protect = false; // If protecting is used
        public static string ProtectionReason = ""; // Reason
        public static bool ProtectionRequests = false; // Protection requeust
        public static string ProtectionRequestPage = ""; // Request page
        public static string ProtectionRequestReason = ""; // Reason
        public static string ProtectionRequestSummary  = ""; // Summary
        public static string ProtectionTime = "indefinite"; // no comment
        public static int QueueBuilderLimit = 10; // 
        public static bool QuickSight; // comment me
        public static int RcBlockSize = 100; // comment me
        public static string ReportExtendSummary = ""; // comment me
        public static bool ReportLinkDiffs = true; // comment me
        public static string ReportSummary = ""; // Summary
        public static bool RequireAdmin = false; // Require sysop
        public static bool RequireAutoconfirmed = false; // Require auto confirmed
        public static bool RequireConfig = true; // Require config
        public static int RequireEdits = 0; // edits 
        public static bool RequireRev = false; // Require reviewer
        public static bool RequireRollback = false; // Require rollback
        public static int RequireTime; // Require time
        public static List<Regex> RevertPatterns = new List<Regex>(); // comment me
        public static string RevertSummary = ""; // Summary
        public static List<string> RevertSummaries = new List<string>(); // Summary
        public static string RfdLocation = ""; // RFD
        public static bool RightAlignQueue = false; // comment me
        public static List<string> Rights = new List<string>(); // comment me
        public static string RollbackSummary = ""; // comment me
        public static bool RightPending = false; // comment me
        public static bool SaveConfig = true; // comment me
        public static Dictionary<string, string> SensitiveAddresses = new Dictionary<string, string>(); // comment me
        public static List<string> SharedIPTemplates = new List<string>(); // comment me
        public static bool ShowNewEdits = true; // comment me
        public static bool ShowLog = true; // comment me
        public static bool ShowNewMessages = true; // comment me
        public static bool ShowQueue = true; // comment me
        public static bool ShowToolTips = true; // comment me
        public static bool ShowTwoQueues = false; // comment me
        public static bool Sight = false; // comment me
        public static string SingleRevertSummary; // comment me
        public static bool SockReports = false; // comment me
        public static string SockReportLocation = ""; // comment me
        public static bool Speedy = false; // comment me
        //public static Dictionary<string, SpeedyCriterion> SpeedyCriteria;
        public static string SpeedyDeleteSummary = ""; // comment me
        public static string AssociatedDeletion = "G8 - nonexistent dependency"; // comment me
        public static string SpeedyMessageSummar = ""; // comment me
        public static string SpeedyMessageTitle = ""; // comment me
        public static string SpeedySummary = ""; // comment me
        public static string StartupPage = "Project:HG"; // comment me
        public static string Summary = ""; // comment me
        public static List<string> Tags = new List<string>(); // comment me
        public static List<string> TagSummaries = new List<string>(); // comment me
        public static string TemplateMessageSummary; // comment me
        public static List<string> TemplateMessages = new List<string>(); // comment me
        public static List<string> TemplateMessagesGlobal = new List<string>(); // comment me
        public static Dictionary<string, string> TemplateSummary = new Dictionary<string, string>(); // comment me
        public static Dictionary<string, string> GlobalSumm = new Dictionary<string, string>(); // comment me
        public static string TfdLocation = ""; // comment me
        public static string TranslateLocation = "http://meta.wikimedia.org/wiki/Huggle/Localization"; // comment me
        public static bool TrayIcon = true; // comment me
        public static bool TRR = false; // comment me
        public static Dictionary<string, string> WelcomesList = new Dictionary<string, string>(); // comment me
        public static bool WelcomeEnabled = false; // comment me
        public static string TRRLocation = ""; // comment me
        public static bool UAA = false; // comment me
        public static string UAALocation = ""; // comment me
        public static string UAABotLocation = ""; // comment me
        public static string UndoSummary; // comment me
        public static bool UpdateWhitelist = false; // comment me
        public static bool UpdateWhitelistManual = false; // comment me
        public static bool UseAdminFunctions = true;// comment me
        public static string UserAgent = "Huggle/"; //+ Version.ToString() + "." + Version.Minor.ToString() + "." +  Version.Build.ToString() + " http://en.wikipedia.org/wiki/Wikipedia:Huggle";
        public static string UserConfigLocation = "Special:Mypage/huggle.css"; // comment me
        public static string UserListLocation; // comment me
        public static string UserListUpdateSummary = ""; // comment me
        public static bool UsernameListed = false; // comment me
        public static bool UseIrc = true; // comment me
        public static bool UseRollback = true; // comment me
        public static List<string> Agf = new List<string>(); // comment me
        public static bool UsePending = false; // comment me
        public static Dictionary<Regex, user.UserLevel> UserTalkSummaries = new Dictionary<Regex, user.UserLevel>(); // comment me
        public static string VandalReportReason = ""; // comment me
        public static bool UseCSummaries = false; // comment me
        public static int WarningAge = 36; // comment me
        public static bool WarningImLevel = false; // comment me
        public static string WarningMode = ""; // comment me
        //public static Dictionary<string> WarningTypes;
        //public static Dictionary<> Watch;
        public static int WhitelistEditCount = 500; // comment me
        public static string WhitelistLocation = ""; // comment me
        public static bool WhitelistSplit = false; // comment me
        public static Dictionary<string, string> WhitelistTimestamps = new Dictionary<string, string>(); // comment me
        public static string WhitelistUpdateSummary = ""; // comment me
        public static bool Xfd = false; // comment me
        public static string XfdDiscussionSummary = ""; // comment me
        public static string XfdLogSummary = ""; // comment me
        public static string XfdMessage = ""; // comment me
        public static string XfdMessageSummary = ""; // comment me
        public static bool TemplatePs = false; // comment me
        public static string XfdMessageTitle = ""; // comment me
        public static string XfdSummary = ""; // comment me
        public static bool WriteUser = false; // comment me
        public static bool RevisionAccess = false; // comment me
        public static bool RevisionR = false; // comment me

        public static bool WelcomeUser = false; // comment me
        public static Dictionary<string, string> WelcomeString = new Dictionary<string, string>(); // comment me
        public static string WarnSummary = ""; // comment me
        public static string WarnSummary2 = ""; // comment me
        public static string WarnSummary3 = ""; // comment me
        public static string WarnSummary4 = ""; // comment me

        public static bool WatchDelete = false; // comment me
        public static string Welcome = ""; // comment me
        public static string WelcomeAnon = ""; // comment me
        public static string WelcomeSummary = ""; // comment me

        public enum platform
        {
            windows32,
            linux32,
            unix32,
            macos32,
            windows64,
            linux64,
            macos64
        }
    }

    
}
