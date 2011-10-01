//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Petrb

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
        public readonly static string ShortWikiPath = "wiki/";
        public readonly static string GlobalConfigLocation = "Huggle/Config";
        public readonly static string WikiPath = "w/"; // short path for root of wiki
        public static string Metawiki = "http://meta.wikimedia.org/";
        public static Dictionary<string, List<string>> AllLists = new Dictionary<string,List<string>>();
        public readonly  static bool Beta = true; // always true on testing or devel

        public readonly static string[] EditTypes = { "blocknote", "deletenote", "deletetag", "deletereq", "manual", "message", "note", "prodtag", "protectreq", "report", "revert", "speedytag", "tag", "warning" };

        public static int QueueTop = 80;
        public static int QueueLeft = 20;

        public static bool devs = true;

        //Values only used at runtime

        public static bool ConfigChanged = false; //
        public static Version ConfigVersion = new Version(0, 0, 0);
        public static string DefaultLanguage = "en";
        public static List<string> Languages = new List<string>();
        public static Version LatestVersion = new Version(0, 0, 0);
        public static Dictionary<string, Dictionary<string, string>> Messages = new Dictionary<string,Dictionary<string,string>>();
        public static string Password = "xx";
        public static Dictionary<string, string> WarningMessages = new Dictionary<string,string>();
        //Values stored in local config file

        public static string Language;
        public static string ProxyUsername = "";
        public static string ProxyUserDomain = "";
        public static string ProxyServer = "";
        public static string ProxyPort = "";
        public static bool ProxyEnabled = false;
        public static bool RememberMe = true;
        public static bool RememberPassword = false;
        public static string Username = "";
        public static bool WindowMaximize = true;
        public static System.Drawing.Point WindowPosition = new System.Drawing.Point();
        public static System.Drawing.Size WindowSize = new System.Drawing.Size();

        //Values changeable through global / project / user config pages

        public static string AfdLocation = "";
        public static bool AIV = false;
        public static string AIVBotLocation = "";
        public static string AIVLocation = "";
        public static string AivSingleNote;
        public static bool Approval = false;
        public static List<string> AssistedSummaries = new List<string>();
        public static bool AutoAdvance = false;
        public static bool AutoReport  = true;
        public static bool AutoWarn  = true;
        public static bool AutoWhitelist  = true;
        public static bool Block = false;
        public static List<string> BlockExpiryOptions = new List<string>();
        public static string BlockMessage;
        public static bool BlockMessageDefault = true;
        public static string BlockMessageIndef;
        public static string BlockReason;
        public static string BlockSummary;
        public static string BlockTime = "indefinite";
        public static string BlockTimeAnon = "24 hours";
        public static string CfdLocation;
        public static string ConfigSummary = "";
        public static bool ConfirmIgnored = true;
        public static bool ConfirmMultiple = false;
        public static bool ConfirmPage = true;
        public static bool ConfirmRange = true;
        public static string ProdLogs_Name = "ProdLogs";
        public static bool ProdLogs = false;
        public static bool SlowIrc = true;
        public static bool ConfirmSame = true;
        public static bool ConfirmSelfRevert = true;
        public static bool ConfirmWarned = true;
        public static int CountBatchSize = 20;
        public static List<string> CustomRevertSummaries;
        public static string DefaultQueue = "";
        public static string WhitelistUrl = "http://toolserver.org/~petrb/huggle/wl.php";
        public static string DefaultQueue2;
        public static string DefaultSummary = "";
        public static bool Delete = false;
        public static Int32 Platform = 86;
        public static string DiffFontSize = "8";
        public static string DocsLocation = "http://en.wikipedia.org/wiki/Wikipedia:Huggle";
        public static string DownloadLocation = "http://huggle3.googlecode.com/files/huggle $1.exe";
        public static string Downloadloc64 = "http://huggle3.googlecode.com/files/huggle $1x64.exe";
        public static bool Email;
        public static string EmailSubject = "";
        public static string TestWp = "http://hub.tm-irc.org/test/";
        public static bool Enabled;
        public static bool EnabledForAll = false;
        public static bool ExtendReports = false;
        public static string FeedbackLocation = "";
        public static List<string> GoToPages;
        public static string IconsLocation = "http://en.wikipedia.org/wiki/Wikipedia:Huggle/Icons";
        public static string IfdLocation = "";
        public static List<string> IgnoredPages = new List<string>();
        public static bool Initialised = false;
        public static string IrcChannel = "";
        public static bool IrcMode = false;
        public static int IrcPort = 6667;
        public static string IrcServer = "";
        public static string IrcServerName = "";
        public static string IrcUsername = "";
        public static string LocalizatonPath = "Huggle/Localization/";
        public static string LogFile = "";
        public static int MaxReportLinks = 6;
        public static string MfdLocation = "";
        public static Dictionary<string, bool> Minor;
        public static Version MinVersion;
        public static int MinWarningWait = 10;
        public static bool MonthHeadings = false;
        public static List<string> MultipleRevertSummaryParts = new List<string>();
        public static bool OpenInBrowser = false;
        public static Regex PageBlankedPattern;
        public static Regex PageCreatedPattern;
        public static Regex PageRedirectedPattern;
        public static Regex PageReplacedPattern;
        public static bool Patrol = false;
        public static bool PatrolSpeedy = false;
        public static int Preloads = 2;
        public static bool Prod = false;
        public static string ProdMessage;
        public static string ProdMessageSummary = "";
        public static string ProdMessageTitle = "";
        public static string ProdSummary = "";
        public static string Project = "";
        public static Dictionary<string, string> Projects = new Dictionary<string, string>();
        public static string ProjectConfigLocation;
        public static bool PromptForBlock = true;
        public static bool  PromptForReport = false;
        public static string Csd_Log_Page = "";
        public static bool Protect = false;
        public static string ProtectionReason = "";
        public static bool ProtectionRequests = false;
        public static string ProtectionRequestPage = "";
        public static string ProtectionRequestReason = "";
        public static string ProtectionRequestSummary  = "";
        public static string ProtectionTime = "indefinite";
        public static int QueueBuilderLimit = 10;
        public static bool QuickSight;
        public static int RcBlockSize = 100;
        public static string ReportExtendSummary = "";
        public static bool ReportLinkDiffs = true;
        public static string ReportSummary = "";
        public static bool RequireAdmin = false;
        public static bool RequireAutoconfirmed = false;
        public static bool RequireConfig = true;
        public static int RequireEdits = 0;
        public static bool RequireRev = false;
        public static bool RequireRollback = false;
        public static int RequireTime;
        public static List<Regex> RevertPatterns = new List<Regex>();
        public static string RevertSummary = "";
        public static List<string> RevertSummaries =  new List<string>();
        public static string RfdLocation = "";
        public static bool RightAlignQueue = false;
        public static List<string> Rights;
        public static string RollbackSummary = "";
        public static bool RightPending = false;
        public static bool SaveConfig = true;
        public static Dictionary<string, string> SensitiveAddresses = new Dictionary<string,string>();
        public static List<string> SharedIPTemplates = new List<string>();
        public static bool ShowNewEdits = true;
        public static bool ShowLog = true;
        public static bool ShowNewMessages = true;
        public static bool ShowQueue = true;
        public static bool ShowToolTips = true;
        public static bool ShowTwoQueues = false;
        public static bool Sight = false;
        public static string SingleRevertSummary;
        public static bool SockReports = false;
        public static string SockReportLocation = "";
        public static bool Speedy = false;
        //public static Dictionary<string, SpeedyCriterion> SpeedyCriteria;
        public static string SpeedyDeleteSummary = "";
        public static string AssociatedDeletion = "G8 - nonexistent dependency";
        public static string SpeedyMessageSummar = "";
        public static string SpeedyMessageTitle = "";
        public static string SpeedySummary = "";
        public static string StartupPage = "Project:HG";
        public static string Summary = "";
        public static List<string> Tags = new List<string>();
        public static List<string> TagSummaries = new List<string>();
        public static string TemplateMessageSummary;
        public static List<string> TemplateMessages = new List<string>();
        public static List<string> TemplateMessagesGlobal = new List<string>();
        public static Dictionary <string, string> TemplateSummary = new Dictionary<string,string>();
        public static Dictionary<string, string>  GlobalSumm = new Dictionary<string,string>();
        public static string TfdLocation = "";
        public static string TranslateLocation = "http://meta.wikimedia.org/wiki/Huggle/Localization";
        public static bool TrayIcon = true;
        public static bool TRR = false;
        public static Dictionary<string, string> WelcomesList = new Dictionary<string,string>();
        public static bool WelcomeEnabled = false;
        public static string TRRLocation = "";
        public static bool UAA = false;
        public static string  UAALocation = "";
        public static string UAABotLocation = "";
        public static string UndoSummary;
        public static bool UpdateWhitelist = false;
        public static bool UpdateWhitelistManual = false;
        public static bool UseAdminFunctions = true;
        public static string UserAgent = "Huggle2/"; //+ Version.ToString() + "." + Version.Minor.ToString() + "." +  Version.Build.ToString() + " http://en.wikipedia.org/wiki/Wikipedia:Huggle";
        public static string UserConfigLocation = "Special:Mypage/huggle.css";
        public static string UserListLocation;
        public static string UserListUpdateSummary = "";
        public static bool UsernameListed = false;
        public static bool UseIrc = true;
        public static bool UseRollback = true;
        public static List<string> Agf = new List<string>();
        public static bool UsePending = false;
        public static Dictionary<Regex, user.UserLevel> UserTalkSummaries = new Dictionary<Regex,user.UserLevel>();
        public static string VandalReportReason = "";
        public static bool UseCSummaries = false;
        public static int WarningAge = 36;
        public static bool WarningImLevel = false;
        public static string WarningMode = "";
        //public static Dictionary<string> WarningTypes;
        //public static Dictionary<> Watch;
        public static int WhitelistEditCount = 500;
        public static string WhitelistLocation = "";
        public static bool WhitelistSplit = false;
        public static Dictionary<string, string> WhitelistTimestamps = new Dictionary<string,string>();
        public static string WhitelistUpdateSummary = "";
        public static bool Xfd  = false;
        public static string XfdDiscussionSummary = "";
        public static string XfdLogSummary = "";
        public static string XfdMessage = "";
        public static string XfdMessageSummary = "";
        public static bool TemplatePs = false;
        public static string XfdMessageTitle = "";
        public static string XfdSummary = "";
        public static bool WriteUser = false;
        public static bool RevisionAccess = false;
        public static bool RevisionR = false;
    
        public static bool WelcomeUse  = false;
        public static Dictionary<string, string> WelcomeString = new Dictionary<string,string>();
        public static string WarnSummary = "";
        public static string WarnSummary2 = "";
        public static string WarnSummary3 = "";
        public static string WarnSummary4 = "";
    
        public static bool WatchDelete = false;
        public static string Welcome = "";
        public static string WelcomeAnon = "";
        public static string WelcomeSummary = "";

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
