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
        /// SSL
        /// </summary>
        public static bool UseSsl = true;
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
        /// <summary>
        /// Size
        /// </summary>
        public static int ItemSize = 17;
        

        //////////
        //Values changeable through global / project / user config pages
        //////////

        /// <summary>
        /// location of afd
        /// </summary>
        public static string AfdLocation = ""; 
        /// <summary>
        /// If AIV is available on the project
        /// </summary>
        public static bool AIV = false; 
        /// <summary>
        /// AIV bot page
        /// </summary>
        public static string AIVBotLocation = "";
        /// <summary>
        /// AIV location
        /// </summary>
        public static string AIVLocation = "";
        /// <summary>
        /// Note
        /// </summary>
        public static string AivSingleNote;
        /// <summary>
        /// If requires an approval on special page
        /// </summary>
        public static bool Approval = false;
        /// <summary>
        /// Summaries of others
        /// </summary>
        public static List<string> AssistedSummaries = new List<string>();
        /// <summary>
        /// Automaticaly advance
        /// </summary>
        public static bool AutoAdvance = false;
        /// <summary>
        /// Report users without prompt
        /// </summary>
        public static bool AutoReport  = true;
        /// <summary>
        /// Automaticaly submit messages to talk
        /// </summary>
        public static bool AutoWarn  = true;
        /// <summary>
        /// Comment me (better :))
        /// </summary>
        public static bool AutoWhitelist  = true; // Update whitelist
        /// <summary>
        /// Block is allowed over huggle
        /// </summary>
        public static bool Block = false;
        /// <summary>
        /// Options for block
        /// </summary>
        public static List<string> BlockExpiryOptions = new List<string>();
        /// <summary>
        /// Block message
        /// </summary>
        public static string BlockMessage; 
        /// <summary>
        /// Use default message instead of preconfigured ones
        /// </summary>
        public static bool BlockMessageDefault = true;
        /// <summary>
        /// Message for indefinite block
        /// </summary>
        public static string BlockMessageIndef;
        public static string BlockReason; // Default block reason
        /// <summary>
        /// Summary
        /// </summary>
        public static string BlockSummary;
        /// <summary>
        /// Block time
        /// </summary>
        public static string BlockTime = "indefinite";
        /// <summary>
        /// block time
        /// </summary>
        public static string BlockTimeAnon = "24 hours";
        /// <summary>
        /// Location of CFD
        /// </summary>
        public static string CfdLocation;
        /// <summary>
        /// Summary for config
        /// </summary>
        public static string ConfigSummary = "";
        /// <summary>
        /// Confirm if revert ignored user
        /// </summary>
        public static bool ConfirmIgnored = true;
        /// <summary>
        /// Confirm if do action on more users
        /// </summary>
        public static bool ConfirmMultiple = false;
        /// <summary>
        /// 
        /// </summary>
        public static bool ConfirmPage = true; // HUH
        /// <summary>
        /// Confirm if user belong to same ip range
        /// </summary>
        public static bool ConfirmRange = true;
        /// <summary>
        /// Logs of prod
        /// </summary>
        public static string ProdLogs_Name = "ProdLogs";
        /// <summary>
        /// Save them
        /// </summary>
        public static bool ProdLogs = false;
        /// <summary>
        /// If slow irc mode is active
        /// </summary>
        public static bool SlowIrc = true;
        /// <summary>
        /// Confirm if revert to same id
        /// </summary>
        public static bool ConfirmSame = true;
        /// <summary>
        /// Confirm self
        /// </summary>
        public static bool ConfirmSelfRevert = true;
        /// <summary>
        /// Confirm revert of warned user
        /// </summary>
        public static bool ConfirmWarned = true;
        public static int CountBatchSize = 20; // comment me
        public static List<string> CustomRevertSummaries = new List<string>(); // comment me
        /// <summary>
        /// Default queue
        /// </summary>
        public static string DefaultQueue = "";
        /// <summary>
        /// Whitelist
        /// </summary>
        public static string WhitelistUrl = "http://toolserver.org/~petrb/huggle/wl.php";
        /// <summary>
        /// Default
        /// </summary>
        public static string DefaultQueue2;
        /// <summary>
        /// Default summary for all edits
        /// </summary>
        public static string DefaultSummary = "";
        /// <summary>
        /// If deleting using huggle is available
        /// </summary>
        public static bool Delete = false;
        /// <summary>
        /// DEPRECATED: platform id
        /// </summary>
        public static Int32 Platform = 86;
        /// <summary>
        /// Size of font
        /// </summary>
        public static string DiffFontSize = "8";
        /// <summary>
        /// Location of manual
        /// </summary>
        public static string DocsLocation = "http://en.wikipedia.org/wiki/Wikipedia:Huggle";
        /// <summary>
        /// DEPRECATED
        /// </summary>
        public static string DownloadLocation = "http://huggle.googlecode.com/files/huggle $1.exe";
        /// <summary>
        /// DEPRECATED
        /// </summary>
        public static string Downloadloc64 = "http://huggle.googlecode.com/files/huggle $1x64.exe"; // Download of x64, deprecated
        /// <summary>
        /// Emails
        /// </summary>
        public static bool Email;
        /// <summary>
        /// Default subject of email
        /// </summary>
        public static string EmailSubject = "";
        /// <summary>
        /// URL of test wiki
        /// </summary>
        public static string TestWp = "http://hub.tm-irc.org/test/";
        /// <summary>
        /// If huggle is enabled
        /// </summary>
        public static bool Enabled;
        /// <summary>
        /// If huggle is enabled on global
        /// </summary>
        public static bool EnabledForAll = false;
        /// <summary>
        /// If reports should be extended
        /// </summary>
        public static bool ExtendReports = false;
        /// <summary>
        /// Location of feedback page
        /// </summary>
        public static string FeedbackLocation = "http://en.wikipedia.org/wiki/WT:HG";
        /// <summary>
        /// List
        /// </summary>
        public static List<string> GoToPages = new List<string>();
        /// <summary>
        /// Location of icons
        /// </summary>
        public static string IconsLocation = "http://en.wikipedia.org/wiki/Wikipedia:Huggle/Icons";
        /// <summary>
        /// Location of IFD
        /// </summary>
        public static string IfdLocation = "";
        public static List<string> IgnoredPages = new List<string>(); // Ignored pages
        /// <summary>
        /// If huggle is loaded
        /// </summary>
        public static bool Initialised = false;
        /// <summary>
        /// IRC channel for feed
        /// </summary>
        public static string IrcChannel = "";
        /// <summary>
        /// If IRC feed is enabled
        /// </summary>
        public static bool IrcMode = false;
        /// <summary>
        /// Irc port
        /// </summary>
        public static int IrcPort = 6667;
        /// <summary>
        /// Server name
        /// </summary>
        public static string IrcServer = "";
        /// <summary>
        /// Server ident
        /// </summary>
        public static string IrcServerName = "";
        /// <summary>
        /// User name
        /// </summary>
        public static string IrcUsername = "";
        /// <summary>
        /// Localization
        /// </summary>
        public static string LocalizatonPath = "Huggle/Localization/";
        /// <summary>
        /// Log file
        /// </summary>
        public static string LogFile = "";
        /// <summary>
        /// Links
        /// </summary>
        public static int MaxReportLinks = 6;
        /// <summary>
        /// Location of MFD
        /// </summary>
        public static string MfdLocation = ""; // MFD location
        /// <summary>
        /// Minor
        /// </summary>
        public static Dictionary<string, bool> Minor = new Dictionary<string,bool>(); // Minor edits
        /// <summary>
        /// Minimal allowed version for use on project
        /// </summary>
        public static Version MinVersion; // Min allowed version
        /// <summary>
        /// Wait between issue of warning
        /// </summary>
        public static int MinWarningWait = 10; // Wait between warning
        /// <summary>
        /// If headers are allowed on project
        /// </summary>
        public static bool MonthHeadings = false;
        /// <summary>
        /// Parts of multiple revert
        /// </summary>
        public static List<string> MultipleRevertSummaryParts = new List<string>(); // Summary
        /// <summary>
        /// If links should be opened in a browser
        /// </summary>
        public static bool OpenInBrowser = false; // Open link in new tab
        /// <summary>
        /// Regex for blanked page
        /// </summary>
        public static Regex PageBlankedPattern; // blanked
        /// <summary>
        /// Regex for created page
        /// </summary>
        public static Regex PageCreatedPattern; // created
        /// <summary>
        /// Regex for redirect
        /// </summary>
        public static Regex PageRedirectedPattern; // redirect
        /// <summary>
        /// Regex for replaced page
        /// </summary>
        public static Regex PageReplacedPattern; // replaced
        /// <summary>
        /// If patroling is allowed
        /// </summary>
        public static bool Patrol = false; // Patrol
        /// <summary>
        /// If speedy patroling is enabled
        /// </summary>
        public static bool PatrolSpeedy = false;
        /// <summary>
        /// Preloads n
        /// </summary>
        public static int Preloads = 2; // How many preloads to get
        /// <summary>
        /// If PROD is enabled
        /// </summary>
        public static bool Prod = false; // prod
        /// <summary>
        /// Message for PROD
        /// </summary>
        public static string ProdMessage; // Message for prod
        /// <summary>
        /// Summary message
        /// </summary>
        public static string ProdMessageSummary = ""; // Summary
        /// <summary>
        /// Title for talk
        /// </summary>
        public static string ProdMessageTitle = ""; // Prod message title
        /// <summary>
        /// Summary for talk
        /// </summary>
        public static string ProdSummary = ""; // Prod summary when editing log
        /// <summary>
        /// Active wiki
        /// </summary>
        public static string Project = ""; // Current wiki
        /// <summary>
        /// List of projects which are available
        /// </summary>
        public static Dictionary<string, string> Projects = new Dictionary<string, string>(); //  wikis
        /// <summary>
        /// Location of config
        /// </summary>
        public static string ProjectConfigLocation; // Location of config
        /// <summary>
        /// Confirmation when you want to block someone
        /// </summary>
        public static bool PromptForBlock = true; // Ask when blocking people
        /// <summary>
        /// Confirmation for reporting of user
        /// </summary>
        public static bool PromptForReport = false; // Prompt
        /// <summary>
        /// Log page for CSD
        /// </summary>
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
        public static Dictionary<string, page.SpeedyCriterion> SpeedyCriteria = new Dictionary<string,page.SpeedyCriterion>();
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
        public static string UserAgent;
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
