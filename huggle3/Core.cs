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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace huggle3
{
    public static class Languages
    {
        public static string Get(string id)
        {
            try
            {
                // return string
                if (Config.Messages.ContainsKey(Config.Language) != true)
                {
                    return "<invalid>";
                }
                if (Config.Messages[Config.Language].ContainsKey(id) == false)
                { // if there is no such a language it returns the english one
                    if (Config.Messages[Config.DefaultLanguage].ContainsKey(id))
                    {
                        if (Config.Messages[Config.DefaultLanguage][id] == null)
                        {
                            return "<invalid>";
                        }
                        return Config.Messages[Config.DefaultLanguage][id];
                    }
                }
                else
                {
                    // got it
                    if (Config.Messages[Config.Language][id] == null)
                    {
                        return "";
                    }
                    return Config.Messages[Config.Language][id];
                }
            }
            catch (Exception A)
            {
                Core.ExceptionHandler( A );
            }

            return "<invalid> " + id;
        }
    }
    public static class Core
    {
        public static class Threading
        {
            private static int ThreadLast = 0;
            private static int ThreadCount = 0;
            private static List<ThreadS> ThreadList = new List<ThreadS>();
            private class ThreadS
            {
                public string Decription;
                public System.Threading.Thread handle;
                public bool Active = false;
            }

            /// <summary>
            /// This is only called when exiting
            /// </summary>
            /// <returns></returns>
            public static bool DestroyCore()
            {
                // All threads are aborted (usualy when application die)
                int curr = 0;
                while (curr < Core.MThread)
                {
                    KillThread(curr);
                    curr++;
                }
                return true;
            }

            /// <summary>
            /// Kill thread id
            /// </summary>
            /// <param name="N"></param>
            /// <returns></returns>
            public static bool KillThread(int N)
            {
                // request thread to be aborted
                if (ThreadList[N].Active == false)
                {
                    return false;
                }
                if (ThreadList[N].handle.ThreadState == System.Threading.ThreadState.Aborted)
                {
                    // it's already down
                    ThreadList[N].Active = false;
                    ThreadList[N].Decription = "";
                    ThreadCount = ThreadCount - 1;
                    return false;
                }
                ThreadList[N].handle.Abort();
                ThreadList[N].Active = false;
                ThreadList[N].Decription = "";
                ThreadCount = ThreadCount - 1;
                return true;
            }

            /// <summary>
            /// Insted of kill thread, safe method to release handle of thread which is about to be aborted, should be called as last action of thread
            /// </summary>
            /// <param name="Thread"></param>
            public static void ReleaseHandle(int Thread)
            {
                if (ThreadList[Thread].Active == true)
                {
                    ThreadList[Thread].handle = null;
                    ThreadList[Thread].Active = false;
                    ThreadCount = ThreadCount - 1;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ThreadStart"></param>
            /// <param name="name"></param>
            /// <returns></returns>
            public static int CreateThread(System.Threading.ThreadStart ThreadStart, string name)
            {
                try
                {
                    ThreadLast++;
                    int ThreadID = ThreadLast;
                        while (ThreadList[ThreadID].Active != false)
                        {
                            if (ThreadID > Core.MThread || ThreadID > ThreadList.Count)
                            {
                                ThreadID = 0;
                            }
                            else
                            {
                                ThreadID++;
                            }
                        }
                    ThreadLast = ThreadID;
                    ThreadList[ThreadID].Active = true;
                    ThreadList[ThreadID].Decription = name;
                    ThreadList[ThreadID].handle = new System.Threading.Thread(ThreadStart);
                    ThreadCount = ThreadCount  + 1;
                    return ThreadID;
                }
                catch (Exception A)
                {
                    Core.ExceptionHandler(A);
                    return -1;
                }
            }

            /// <summary>
            /// Create thread with no name
            /// </summary>
            /// <param name="ThreadStart"></param>
            /// <returns></returns>
            public static int CreateThread(System.Threading.ThreadStart ThreadStart)
            {
                try
                {
                    ThreadLast++;
                    int ThreadID = ThreadLast;
                    while (ThreadList[ThreadID].Active != false)
                    {
                        if (ThreadID > Core.MThread)
                        {
                            ThreadID = 0;
                        }
                        else
                        {
                            ThreadID++;
                        }
                    }
                    ThreadLast = ThreadID;
                    ThreadList[ThreadID].Active = true;
                    ThreadList[ThreadID].Decription = "Huggle";
                    ThreadList[ThreadID].handle = new System.Threading.Thread(ThreadStart);
                    return ThreadID;
                }
                catch (Exception A)
                {
                    return -1;
                }
            }

            /// <summary>
            /// Thread manager core
            /// </summary>
            public static void ManagerThread()
            {
                System.Threading.Thread.Sleep(2000);
            }

            /// <summary>
            /// Only used for initialisation of the huggle
            /// </summary>
            public static void CreateList()
            {
                ThreadList.Clear();
                int curr = 0;
                while (curr < Core.MThread)
                {
                    curr++;
                    ThreadList.Add(new ThreadS());
                }
            }

            /// <summary>
            /// Thread count
            /// </summary>
            public static int ThCount
            {
                get { return ThreadCount; }
            }

            /// <summary>
            /// Return windows / linux handle of thread
            /// </summary>
            /// <param name="N"></param>
            /// <returns></returns>
            public static System.Threading.Thread GetHandle(int N)
            {
                return ThreadList[N].handle;
            }

            /// <summary>
            /// Start
            /// </summary>
            /// <param name="ID"></param>
            public static void Execute(int ID)
            {
                if (ThreadList[ID].Active == true)
                {
                    ThreadList[ID].handle.Start();
                }
            }
        }
        private static string _history = "";

        private static Exception core_er;

        public static Dictionary<page, string> CustomReverts = new Dictionary<page,string>();
        public static queue Current_Queue;
        public static bool Interrupted = false;
        public static string EditToken;
        public static bool HidingEdit = false;
        public static System.DateTime LastRCTime = new System.DateTime();
        public static edit NullEdit;
        public static string Patrol_Token;
        public static System.Threading.Thread MainThread;
        public static string[] months;

        public const int MThread=600; // Maximum number of threads in core

        /// <summary>
        /// Should contain list of all static arrays of objects accessible everywhere
        /// </summary>
        public struct All
        {
            public static List<space> spaces = new List<space>();
        }

        /// <summary>
        /// Threads which are not managed by core
        /// </summary>
        public struct SpecialThreads
        {
            public static System.Threading.Thread RecoveryThread;
            public static System.Threading.Thread ThreadManager;
        }

        /// <summary>
        /// Represent a block
        /// </summary>
        public class Block
        {
            public System.DateTime Block_Date;
            public string Block_Comment;
            public string Block_Duration;
            public string Block_Action;
            public user Block_Sysop;
            public user Block_User;
        }

        /// <summary>
        /// Used for cache
        /// </summary>
        public class CacheData
        {
            public edit Edit;
            public string Text;
        }

        /// <summary>
        /// History item
        /// </summary>
        public class HistoryItem
        {
            public string text;
            public string url;
            public edit Edit;
            public HistoryItem(string _url)
            {
                this.url = _url;
            }

            public HistoryItem(edit _edit)
            {
                this.Edit = _edit;
            }
        }

        /// <summary>
        /// Handle for page move
        /// </summary>
        public class PageMove
        {
            public System.DateTime Date;
        }

        /// <summary>
        /// Represent a protection of page
        /// </summary>
        public class Protection
        {
            public bool Cascading;
            public bool Pending;
            public string MoveLevel;
            public string CreateLevel;
            public string Action;
            public string Summary;
            public string EditLevel;
            public System.DateTime Date;
            public user Sysop;
        }

        /// <summary>
        /// Command
        /// </summary>
        public class Command
        {
            public edit Edit;
            public string Description;
            public user User;
            public page Page;
        }

        /// <summary>
        /// Type
        /// </summary>
        enum CommandType
        {
            Revert,
            Report,
            Warning,
            Edit,
            Ignore,
        }

        /// <summary>
        /// Contains data of every edit to pages
        /// </summary>
        public class EditData
        {
            public edit Edit;
            public page Page;
            public string CaptchaWord; // deprecated
            public string CaptchaId;
            public string Text;
            public string Summary;
            public string Section;
            public string Token;
            public string EditTime;
            public bool Error;
            public bool Creating;
            public bool Minor;
            public bool CannotUndo;
            public bool Watch;
            public bool AutoSummary;
        }

        /// <summary>
        /// Upload info
        /// </summary>
        public class Upload
        {
            public user User;
            public page File;
        }

        /// <summary>
        /// Warning
        /// </summary>
        public class Warning
        {
            public System.DateTime Date;
            public user User;
            
        }

        /// <summary>
        /// Return path of local user config
        /// </summary>
        /// <returns></returns>
        public static string LocalPath()
        {
            return Application.LocalUserAppDataPath + "huggle3" + Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// Called only for huggle shutdown, used after cleaning up the stuff
        /// </summary>
        public static void ShutdownSystem()
        {
            Core.Threading.DestroyCore();
            if (SpecialThreads.RecoveryThread != null)
            {
                if (SpecialThreads.RecoveryThread.ThreadState == System.Threading.ThreadState.Running)
                {
                    SpecialThreads.RecoveryThread.Abort();
                }
            }
            Application.Exit();
        }

        /// <summary>
        /// Debugging tool
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool History(string text)
        {
            if (_history.Length - Config.HistoryTrim > Config.HistoryLenght)
            {
                _history = _history.Substring(Config.HistoryTrim);
                _history = "{trimmed} " + _history;
            }
            _history = _history + " -> " + text;
            return false;
        }

        /// <summary>
        /// Callback function
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="PostdData"></param>
        public static void callback(System.Threading.SendOrPostCallback Target, Object PostdData)
        {
            Core.History("callback()");
        }

        /// <summary>
        /// This function look up a string
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="from1"></param>
        /// <param name="from2"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static string FindString(string Source, string from1, string from2, string To)
        {
            //
            Core.History("FindString(string, string, string, string)");
            if (Source == null)
            {    return null; }
            
            if (Source.Contains(from1) != true)
            {
                return null;
            }

            Source = Source.Substring(Source.IndexOf(from1) + from1.Length);
            if (Source.Contains(from2) != true)
            {
                return null;
            }
            Source = Source.Substring(Source.IndexOf(from2) + from2.Length);
            if (Source.Contains(To))
            {
                return Source.Substring(0, Source.IndexOf(To));
            }
            return "";
        }

        /// <summary>
        /// This function look up a string in string between other strings
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="from1"></param>
        /// <param name="from2"></param>
        /// <param name="from3"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static string FindString(string Source, string from1, string from2, string from3, string To)
        {
            // same one
            Core.History("FindString(string, string, string, string, string)");
            string temp_v = Source;
            Source = null;
            if (temp_v.Contains(from1) != true)
            {
                return null;
            }
            temp_v = temp_v.Substring(temp_v.IndexOf(from1) + from1.Length);
            if (temp_v.Contains(from2) != true)
            {
                return null;
            }
            temp_v = temp_v.Substring(temp_v.IndexOf(from2) + from2.Length);
            if (temp_v.Contains(from3) != true)
            {
                return null;
            }
            if (temp_v.Contains(To))
            {
                return temp_v.Substring(0, Source.IndexOf(To));
            }
            return "";
        }

        /// <summary>
        /// This function look up a string in string between other strings
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="from"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static string FindString(string Source, string from, string To)
        {
            Core.History("FindString(Source, string, To)");
            if (Source.Contains(from))
            {
                Source = Source.Substring(Source.IndexOf(from) + from.Length);
                if (Source.Contains(To) == false)
                {
                    return "";
                }
                return Source.Substring(0, Source.IndexOf(To));
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string TargetBuild()
        {
            switch (Config._Platform)
            { 
                case Config.platform.windows32:
                    return "Windows x86";
                case Config.platform.linux32:
                    return "Linux x86";
                case Config.platform.macos32:
                    return "MacOS x86";
                case Config.platform.linux64:
                    return "Linux x64";
                case Config.platform.windows64:
                    return "Windows x64";
            }
            return "<unknown build>";
        }

        /// <summary>
        /// Format a page to html
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string FormatHTML(page Page, string Text)
        {
            History("Core.FormatHTML (" + Page + " )");
            try
            {
                string return_value = "";
                if (Text.Contains("<!-- start content -->") && Text.Contains("<!-- end content -->") && Text != "")
                {
                    return_value = Text.Substring(Text.IndexOf("<!-- start content -->"));
                    return_value = return_value.Substring(0, return_value.IndexOf("<!-- end content -->"));
                }
                else if (Text.Contains("<!-- content -->") && Text.Contains("<!-- mw_content -->"))
                {
                    return_value = Text.Substring(Text.IndexOf("<!-- content -->"));
                    return_value = return_value.Substring(0, return_value.IndexOf("<!-- mw_content -->"));
                }
                else if (Text.Contains("</h1>") && Text.Contains("<div class=\"printfooter\">"))
                {
                    return_value = Text.Substring(Text.IndexOf("</h1>"));
                    return_value = return_value.Substring(0, return_value.IndexOf("<div class=\"printfooter\">"));
                }

                if (Text.Contains("<script>") && Text.Contains("</script>"))
                {

                }
                return_value = "<h1>" + Page.Name + "</h1>" + return_value;

                return return_value;
            }
            catch (Exception B)
            { Core.ExceptionHandler(B); }
            return "";
        }

        /// <summary>
        /// Convert to wiki page
        /// </summary>
        /// <param name="text_html"></param>
        /// <returns></returns>
        public static string HtmltoWikitext(string text_html)
        {
            Core.History("HtmltoWikitext(text)");
            if (text_html.EndsWith(")") && text_html.StartsWith("("))
            {
                text_html = text_html.Substring(1, text_html.Length - 2);
                while (text_html.Contains("</a>") && text_html.Contains("<a href="))
                {
                    string target = System.Web.HttpUtility.HtmlDecode(Core.FindString(text_html, "<a href=", "title=\"", "\""));
                    string text = System.Web.HttpUtility.HtmlDecode(Core.FindString(text_html, "<a href=>", ">", "</a>"));

                    if (text == target)
                    {
                        text_html = text_html.Substring(0, text_html.IndexOf("<a href=")) + "[[" + text + "]]" + Core.FindString(text_html, "</a>");
                    }
                    else
                    {
                        text_html = text_html.Substring(0, text_html.IndexOf("<a href=")) + "[[" + target + "|" +text + "]]" + Core.FindString(text_html, "</a>");
                    }
                }
            }
            return CleanupHTML(text_html);
        }

        /// <summary>
        /// Check if page is mediawiki page
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static bool IsMW(string Content)
        {
            if (Content == null)
            {
                return false;
            }
            return System.Text.RegularExpressions.Regex.Match(Content, "<body class=.mediawiki").Success;
        }

        /// <summary>
        /// Strip html
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CleanupHTML(string data)
        {
            string return_value = data;
            if (data == null)
            {
                return data;
            }
            while (return_value.Contains("<") && return_value.Contains(">"))
            {
                return_value = return_value.Substring(0, return_value.IndexOf("<")) + return_value.Substring(return_value.IndexOf(">" + 1));
            }
            return return_value;
        }

        /// <summary>
        /// This function look up a string in string between other strings
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="From"></param>
        /// <returns></returns>
        public static string FindString(string Source, string From)
        {
            Core.History("Core.FindString( string, string)");
            if (From == null)
            {
                return "";
            }
            if (Source == null)
            {
                return "";
            }
            if (Source.Contains(From))
            {
                return Source.Substring(Source.IndexOf(From) + From.Length);
            }

            return "";
        }

        // User
        public static user GetUser(string Name)
        {
            Core.History("GetUser()");
            return new user(Name);
        }

        /// <summary>
        //// this function initialise config
        //// reset values
        //// those values will be default in case that not present in configs
        //// do not change unless you want to change default presets
        /// </summary>
        /// <returns></returns>
        public static bool InitConfig()
        {   
            Core.History("Core.InitConfig()");
            Config.Whitelist.Clear();
            Config.AIVLocation = "";
            Config.Approval = false;
            Config.AutoAdvance = false;
            Config.AutoReport = false;
            Config.AutoWarn = false;
            Config.AutoWhitelist = true;
            Config.Block = false;
            Config.BlockMessage = "";
            Config.BlockMessageDefault = false;
            Config.BlockReason = "Vandalism";
            Config.BlockSummary = "Blocked";
            Config.BlockTime = "0";
            Config.BlockTimeAnon = "0";
            Config.ConfigChanged = false;
            Config.ConfigSummary = "";
            Config.ConfirmIgnored = false;
            Config.ConfirmMultiple = false;
            Config.ConfirmPage = false;
            Config.ConfirmRange = false;
            Config.ConfirmSame = false;
            Config.ConfirmSelfRevert = true;
            Config.ConfirmWarned = false;
            Config.Csd_Log_Page = "Special:MyPage/HgLogs";
            Config.DefaultSummary = "";
            Config.Delete = false;
            Config.Email = false;
            Config.Enabled = false;
            Config.EnabledForAll = false;
            Config.ExtendReports = false;
            Config.FeedbackLocation = "WP:Huggle/Feedback";
            Config.MonthHeadings = true;
            Config.Password = "";
            Config.Patrol = true;
            Config.PatrolSpeedy = true;
            Config.Prod = false;
            Config.ProdLogs = true;
            Config.ProdMessage = "";
            Config.ProdMessageSummary = "";
            Config.Project = "";
            Config.ProtectionReason = "";
            Config.ProtectionRequestPage = "";
            Config.ProtectionRequestReason = "";
            Config.ProtectionRequests = false;
            Config.ProtectionRequestSummary = "";
            Config.ProtectionTime = "";
            Config.ProxyEnabled = false;
            Config.ProxyPort = "";
            Config.QuickSight = false;
            Config.RememberMe = false;
            Config.RememberPassword = false;
            Config.DefaultLanguage = "en";
            Core.months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            return true;
        }

        /// <summary>
        /// Return history
        /// </summary>
        public static string history
        {
            get { return _history; }
        }

        /// <summary>
        /// Languages init
        /// </summary>
        public static void LoadLanguages()
        {
            Config.Messages.Clear();
            Core.History("Core.LoadLanguages()");
            Load_Language("de", huggle3.Properties.Resources.de);
            Load_Language("en", huggle3.Properties.Resources.en);
            Load_Language("es", huggle3.Properties.Resources.es);
            Load_Language("fa", huggle3.Properties.Resources.fa);
            Load_Language("fr", huggle3.Properties.Resources.fr);
            Load_Language("hi", huggle3.Properties.Resources.hi);
            Load_Language("it", huggle3.Properties.Resources.it);
            Load_Language("ja", huggle3.Properties.Resources.ja);
            Load_Language("ka", huggle3.Properties.Resources.ka);
            Load_Language("kn", huggle3.Properties.Resources.kn);
            Load_Language("ml", huggle3.Properties.Resources.ml);
            Load_Language("mr", huggle3.Properties.Resources.mr);
            Load_Language("bg", huggle3.Properties.Resources.bg);
            Load_Language("nl", huggle3.Properties.Resources.nl);
            Load_Language("no", huggle3.Properties.Resources.no);
            Load_Language("oc", huggle3.Properties.Resources.oc);
            Load_Language("or", huggle3.Properties.Resources.or);
            Load_Language("pt", huggle3.Properties.Resources.pt);
            Load_Language("ru", huggle3.Properties.Resources.ru);
            Load_Language("sv", huggle3.Properties.Resources.sv);
            Load_Language("zh", huggle3.Properties.Resources.zh);
            Load_Language("ar", huggle3.Properties.Resources.ar);
        }

        /// <summary>
        /// Site path
        /// </summary>
        /// <returns></returns>
        public static string SitePath()
        {
            // return site path
            return Config.Projects[Config.Project] + Config.WikiPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static page Get_NewPage(string name)
        {
            // create new page
            page NewPage = new page(name);
            if (name == null)
            {
                return null;
            }
            return NewPage;
        }

        /// <summary>
        /// stop everything in system, used for recovery
        /// </summary>
        /// <returns></returns>
        public static bool StopAll()
        {
            return false;
        }

        /// <summary>
        /// Initialise language
        /// </summary>
        /// <param name="language"></param>
        /// <param name="data"></param>
        public static void Load_Language(string language, string data)
        {
            if (Config.Languages.Contains(language) == false)
            {
                if (Config.Messages.ContainsKey(language))
                {
                    Config.Messages.Remove(language);
                }
                Config.Messages.Add(language , new Dictionary<string, string>());
                foreach ( string message in data.Split(new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries) )
                {
                    if ( message.Contains(":") )
                    {
                        string message_value = message.Substring(message.IndexOf(":") + 1).Trim(' ').Replace("\n", "").Replace(Convert.ToChar(13).ToString(), "").Replace(Convert.ToChar(10).ToString(), "");
                        string message_name = message.Substring(0, message.IndexOf(":")).Trim(' ');
                        Config.Messages[language].Add(message_name, message_value);
                    }
                }
                Config.Languages.Add(language);
            }
        }

        /// <summary>
        /// Suspend thread
        /// </summary>
        public static void Suspend()
        {
            Core.Interrupted = true;
            while (Core.Interrupted)
            {
                System.Threading.Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// Initialisation fc
        /// </summary>
        public static void Initialise()
        {   
            Core.History("Core.Initialise()");
            Config.DefaultLanguage = "en";
            MainThread = System.Threading.Thread.CurrentThread;
            Core.Threading.CreateList();
            InitConfig();
            Config.Language = Config.DefaultLanguage;
            System.GC.Collect();
            LoadLanguages();
        }

        /// <summary>
        /// Throw a huggle error dialog in case of error and recover from crash
        /// </summary>
        /// <param name="error_handle"></param>
        /// <param name="panic"></param>
        /// <returns></returns>
        public static bool ExceptionHandler(Exception error_handle, bool panic = false)
        {
            core_er = error_handle;
            if (SpecialThreads.RecoveryThread == null)
            {
                SpecialThreads.RecoveryThread = new System.Threading.Thread(CreateEx);
                SpecialThreads.RecoveryThread.Name = "Recovery thread";
            }
            else if (SpecialThreads.RecoveryThread.ThreadState == System.Threading.ThreadState.Running)
            {
                Core.Suspend();
                return false;
            }
            else if (SpecialThreads.RecoveryThread.ThreadState == System.Threading.ThreadState.Aborted)
            {
                SpecialThreads.RecoveryThread = new System.Threading.Thread(CreateEx);
                SpecialThreads.RecoveryThread.Name = "Recovery thread";
            }
            SpecialThreads.RecoveryThread.Start();
            if (panic == true)
            {
                StopAll();
            }
            if (MainThread == System.Threading.Thread.CurrentThread)
            {
                Core.Suspend();
            }
            
            return true;
        }

        /// <summary>
        /// Create error
        /// </summary>
        public static void CreateEx()
        {
            huggle3.Forms.ExceptionForm fx = new huggle3.Forms.ExceptionForm();
            fx.error = core_er;
            Application.Run(fx);
        }

        /// <summary>
        /// Make a windows / linux path
        /// </summary>
        /// <param name="Items"></param>
        /// <returns></returns>
        public static string MakePath(string[] Items)
        {

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PageName"></param>
        /// <returns></returns>
        public static page GetPage(string PageName)
        {
            // get a new page
            Core.History("GetPage()");
            try
            {
                page Page = new page(PageName);
                return Page;
            }
            catch (Exception weird)
            {
                // weird, probably out of memory or something like that
                Core.ExceptionHandler(weird);
            }
            return null;
        }
    }

    public static class Core_Scripting
    {
        public class plugin
        {
            public int ID;
            
        }

        public static string Main = "main.tcl";
        public static bool Enabled = true;
    }
    public static class Core_IO
    {
        public static class GET
        {
            /// <summary>
            /// Parser
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static Dictionary<string, string> dictionary(string data)
            {
                Dictionary<string, string> return_value = new Dictionary<string,string>();
                string current_value;

                foreach ( string Item in GET.list( data ) )
                {
                    current_value = Item;
                    current_value = current_value.Trim ( ' ', '\t', '\n' ).Replace( "\\;", Convert.ToChar(2 ).ToString());
                    
                    if ( current_value.Contains ( ";" ) )
                    {
                        string KEY = current_value.Split ( ';' )[0].Replace ( Convert.ToChar ( 2 ), ';' );
                        string VAL = current_value.Split ( ';' )[1].Replace ( Convert.ToChar ( 2 ), ';' );
                        if ( ! return_value.ContainsKey( KEY ) )
                        {
                            return_value.Add ( KEY, VAL );
                        }
                    }
                }

                return return_value;
            }

            /// <summary>
            /// Parser of months
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static bool Months(string data)
            {
                return true;
            }

            /// <summary>
            /// Parser
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static List<string> list(string data)
            {
                //parse
                List<string> DATA = new List<string>();
                string current_value;
                foreach (string x in data.Replace(Convert.ToChar(2).ToString(), "").Split(','))
                {
                    current_value = x;
                    current_value = current_value.Trim(' ', '\t', '\n').Replace(Convert.ToChar(1).ToString(), ",");
                    if (DATA.Contains(current_value) != true && current_value != "")
                    {
                        DATA.Add(current_value);
                    }
                }
                return DATA;
            }

            /// <summary>
            /// Parser of list
            /// </summary>
            /// <param name="fields"></param>
            /// <param name="text"></param>
            /// <returns></returns>
            public static List<List<string>> RecordList(int fields, string text)
            {
                List<List<string>> return_value = new List<List<string>>();

                return return_value;
            }
        }

        /// <summary>
        /// Parser
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ProcessConfigFile (string name)
        {
            Core.History("CoreIO.ProcessConfigFile()");
            List<string> Items = new List<string>(name.Replace("\t","    ").Split('\n'));
            Dictionary<string, string> value = new Dictionary<string, string>();

            int Indent = 0;
            int i = 0;
            
            while ( i < Items.Count )
            {
            if ( ( i > 0 ) && ( Items[i].StartsWith(" ") ) && ( Items[i].Replace(" ", "") != "" ) )
            {
                if ( Indent == 0 )
                    {
                                while ( Items[i][Indent] == ' ' )
                                {
                                    Indent += 1;
                                }
                    }

                Items[i] = Items[i].Substring(Indent);
                Items[i - 1] = Items[i - 1] + Convert.ToChar(2) + Items[i].TrimEnd(' ');
                Items.RemoveAt(i);
            }
            else if ( Items[i].StartsWith("#") || Items[i].StartsWith("<") ||  ( ! Items[i].Contains(":") ) )
                {
                    Items.RemoveAt(i);
                }
            else
                {
                char LF = (char)10;
                Indent = 0;
                Items[i] = Items[i].Replace("\n", LF.ToString()).Trim(' ');
                i++;
                }
            }
        
            foreach ( string Item in Items )
            {
                string Name = Item.Split(':')[0].Trim(Convert.ToChar( 2 ));
                string Value = Item.Substring(Item.IndexOf(":") + 1).Trim(Convert.ToChar( 2 )).Trim(Convert.ToChar( 13 ));

                if ( ! value.ContainsKey(Name) )
                {
                    value.Add(Name, Value.Replace("\r", ""));
                }
            }
            return value;
        }

        /// <summary>
        /// Configure global config from meta
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetGlobalConfigOption(string key, string value)
        {
            switch (key)
            {
                case "enable-all":
                    Config.EnabledForAll = bool.Parse(value);
                    break;
                case "config":
                    Config.ProjectConfigLocation = value;
                    break;
                case "documentation":
                    Config.DocsLocation = value;
                    break;
                case "feedback":
                    Config.FeedbackLocation = value;
                    break;
                case "irc-server":
                    Config.IrcServer = value;
                    break;
                case "whitelist-server":
                    Config.WhitelistUrl = value;
                    break;
                case "min-version":
                    break;
                case "projects":
                    break;
                case "sensitive-addresses":
                    break;
                
                case "irc-server-name":
                    Config.IrcServerName = value;
                    break;
            }
            return true;
        }

        /// <summary>
        /// Configure user options
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetUserConfigOption(string key, string value)
        {
            switch (key)
            {
                case "customtsumm":
                    Config.UseCSummaries = bool.Parse(value);
                    break;
                case "templates":
                    Config.TemplateMessages = GET.list(value);
                    break;
            }
            return true;
        }

        /// <summary>
        /// Configure options defined in config.txt
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetLocalConfigOption(string key, string value)
        {
            switch (key)
            { 
                case "language":
                     Config.Language = value;
                     break;
                case "log-file":
                     Config.LogFile = value;
                     break;
                case "password":
                     Config.RememberPassword = true;
                     Config.Password = value;
                     break;
                case "projects":
                     Config.Projects = GET.dictionary(value);
                     if ( Config.Projects.ContainsKey("test2") == false )
                     {
                        Config.Projects.Add("test2", Config.TestWp);
                     }
                    break;
                 case "project":
                     Config.Project = value;
                     break;
                 case "proxy-enabled":
                     Config.ProxyEnabled = Boolean.Parse( value );
                     break;
                 case "proxy-port":
                     Config.ProxyPort = value;
                     break;
                 case "proxy-server":
                     Config.ProxyServer = value;
                     break;
                 case "proxy-userdomain":
                     Config.ProxyUserDomain = value;
                     break;
                 case "proxy-username":
                     Config.ProxyUsername = value;
                     break;
                 case "queue-right-align":
                     Config.RightAlignQueue = Boolean.Parse(value);
                     break;
                 case "revert-summaries":
                     Config.RevertSummaries = GET.list(value);
                     break;
                //case "shortcuts" : SetShortcuts(Value)
                 case "show-new-messages" :
                     Config.ShowNewMessages = bool.Parse(value);
                     break;
                 case "show-two-queues":
                     Config.ShowTwoQueues = Boolean.Parse(value);
                     break;
                case "TestWp":
                     Config.TestWp = value;
                     break;
                 case "username":
                     Config.Username = value;
                     break;
                 case "whitelist-timestamps":
                     Config.WhitelistTimestamps = GET.dictionary(value);
                     break;
                 case "window-height":
                     Config.WindowSize.Height = int.Parse (value);
                     break;
                 //case "window-left":Config.WindowPosition.X = CInt(Value)
                 case "window-maximize":
                     Config.WindowMaximize = Boolean.Parse(value);
                     break;
                 //case "window-top" : Config.WindowPosition.Y = CInt(Value)
            }
                    return true;
        }

        /// <summary>
        /// Project only
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetProjectConfigValue(string key, string value)
        {
            MessageBox.Show("key " + key + value);
            // project config only
            switch (key)
            {
                case "3rr":
                    // 3rr location
                    Config.TRRLocation = value;
                    break;
                case "afd":
                    // afd location
                    Config.AfdLocation = value;
                    break;
                case "aiv":
                    // aiv location
                    Config.AIVLocation = value;
                    break;
                case "aiv-extend-summary":
                    // extend summary
                    Config.ReportExtendSummary = value;
                    break;
                case "approval":
                    // if approval is needed
                    Config.Approval = bool.Parse(value);
                    break;
                case "assisted-summaries":
                    //Config.AssistedSummaries = "";
                    break;
                case "block":
                    // blocking enabled
                    Config.Block = bool.Parse(value);
                    break;
                case "block-expiry-options":
                    Config.BlockExpiryOptions = GET.list(value);
                    break;
                case "cfd":
                    Config.CfdLocation = value;
                    break;
                case "config-summary":
                    Config.ConfigSummary = value;
                    break;
                case "count-batch-size":
                    Config.CountBatchSize = int.Parse(value);
                    break;
                case "default-queue":
                    Config.DefaultLanguage = value;
                    break;
                case "default-queue-2":
                    Config.DefaultQueue2 = value;
                    break;
                case "delete":
                    Config.Delete = bool.Parse(value);
                    break;
                case "email":
                    Config.Email = bool.Parse(value);
                    break;
                case "email-subject":
                    Config.EmailSubject = value;
                    break;
                case "enable-all":
                    Config.EnabledForAll = bool.Parse(value);
                    break;
                case "go":
                    Config.GoToPages = GET.list(value);
                    break;
                case "irc-channel":
                    Config.IrcChannel = value;
                    break;
                case "ifd":
                    Config.IfdLocation = value;
                    break;
                case "ignore":
                    Config.IgnoredPages = GET.list(value);
                    break;
                case "manual-revert-summary":
                    Config.RevertSummary = value;
                    break;
                case "multiple-revert-summary-parts":
                    Config.MultipleRevertSummaryParts =  GET.list(value);
                    break;
                case "mfd":
                    Config.MfdLocation = value;
                    break;
                case "namespace-aliases":

                    break;
                case "page-blanked-pattern":
                    Config.PageBlankedPattern = new System.Text.RegularExpressions.Regex(value, System.Text.RegularExpressions.RegexOptions.Compiled);
                    break;
                case "page-created-pattern":
                    Config.PageCreatedPattern = new System.Text.RegularExpressions.Regex(value, System.Text.RegularExpressions.RegexOptions.Compiled);
                    break;
                case "page-redirected-pattern":
                    Config.PageRedirectedPattern = new System.Text.RegularExpressions.Regex(value, System.Text.RegularExpressions.RegexOptions.Compiled);
                    break;
                case "page-replaced-pattern":
                    Config.PageReplacedPattern = new System.Text.RegularExpressions.Regex(value, System.Text.RegularExpressions.RegexOptions.Compiled);
                    break;
                case "patrol":
                    Config.Patrol = bool.Parse(value);
                    break;
                case "protect":
                    Config.Protect = bool.Parse(value);
                    break;
                case "protection-request-page":
                    Config.ProtectionRequestPage = value;
                    break;
                case "protection-request-reason":
                    Config.ProtectionRequestReason = value;
                    break;
                case "protection-request-summary":
                    Config.ProtectionRequestSummary = value;
                    break;
                case "queues":
                    //
                    break;
                case "quick-sight":
                    Config.QuickSight = bool.Parse(value);
                    break;
                case "rc-block-size":
                    Config.RcBlockSize = int.Parse(value);
                    break;
                case "require-admin":
                    Config.RequireAdmin = bool.Parse(value);
                    break;
                case "require-autoconfirmed":
                    Config.RequireAutoconfirmed = bool.Parse(value);
                    break;
                case "require-config":
                    Config.RequireConfig = bool.Parse(value);
                    break;
                case "require-edits":
                    Config.RequireEdits = int.Parse(value);
                    break;
                case "template-summ":
                    Config.TemplateSummary = GET.dictionary(value);
                    break;
                case "agf":
                    Config.Agf.Add(value);
                    break;
                case "require-rollback":
                    Config.RequireRollback = bool.Parse(value);
                    break;
                case "require-time":
                    Config.RequireTime = int.Parse(value);
                    break;
                case "revert-summaries":
                    Config.RevertSummaries = GET.list(value);
                    break;
                case "rollback-summary":
                    Config.RollbackSummary = value;
                    break;
                case "rfd":
                    Config.RfdLocation = value;
                    break;
                case "save-config":
                    Config.SaveConfig = bool.Parse(value);
                    break;
                case "shared-ip-templates":
                    Config.SharedIPTemplates = GET.list(value);
                    break;
                case "sight":
                    Config.Sight = bool.Parse(value);
                    break;
                case "single-revert-summary":
                    Config.SingleRevertSummary = value;
                    break;
                case "sock-reports":
                    Config.SockReportLocation = value;
                    break;
                case "speedy-delete-summary":
                    Config.SpeedyDeleteSummary = value;
                    break;
                case "speedy-options":
                    break;
                case "startup-message-location":
                    Config.StartupPage = value;
                    break;
                case "summary":
                    Config.Summary = value;
                    break;
                case "tag-summaries":
                    Config.TagSummaries = GET.list(value);
                    break;
                case "expand-report":
                    Config.TemplatePs = true;
                    break;
                case "template-message-summary":
                    Config.TemplateMessageSummary = value;
                    break;
                case "templates":
                    Config.TemplateMessagesGlobal = GET.list(value);
                    break;
                case "tfd":
                    Config.TfdLocation = value;
                    break;
                case "uaa":
                    Config.UAALocation = value;
                    break;
                case "uaabot":
                    Config.UAABotLocation = value;
                    break;
                case "update-whitelist-manual":
                    Config.UpdateWhitelistManual = bool.Parse(value);
                    break;
                case "userlist":
                    Config.UserListLocation = value;
                    break;
                case "userlist-update-summary":
                    Config.UserListUpdateSummary = value;
                    break;
                case "warning-im-level":
                    Config.WarningImLevel = bool.Parse(value);
                    break;
                case "warning-mode":
                    Config.WarningMode = value;
                    break;
                case "warning-month-headings":
                    Config.MonthHeadings = bool.Parse(value);
                    break;
                case "welcome-summary":
                    Config.WelcomeSummary = value;
                    break;
                case "warning-types":
                    
                    break;
                case "whitelist-edit-count":
                    Config.WhitelistEditCount = int.Parse(value);
                    break;
                case "xfd":
                    Config.Xfd = bool.Parse(value);
                    break;
                case "xfd-discussion-summary":
                    Config.XfdDiscussionSummary = value;
                    break;
                case "xfd-log-summary":
                    Config.XfdLogSummary = value;
                    break;
                case "xfd-message":
                    Config.XfdMessage = value;
                    break;
                case "xfd-message-summary":
                    Config.XfdMessageSummary = value;
                    break;
                case "xfd-message-title":
                    Config.XfdMessageTitle = value;
                    break;
                case "xfd-summary":
                    Config.XfdSummary = value;
                    break;
                


            }
            return true;
        }

        public static bool SetSharedConfigKey(string key, string value)
        {
            switch (key)
            {
                case "admin":
                    Config.UseAdminFunctions = Boolean.Parse(value);
                    break;
                case "aiv-extend-reports":
                    Config.ExtendReports = Boolean.Parse(value);
                    break;
                case "blocktime":
                    Config.BlockTime = value;
                    break;
                case "prodlogs":
                    break;
                case "blocktime-anon":
                    Config.BlockTimeAnon = value;
                    break;
                case "block-message-default":
                    Config.BlockMessageDefault = Boolean.Parse(value);
                    break;
                case "block-message":
                    Config.BlockMessage = value;
                    break;
                case "block-prompt":
                    Config.PromptForBlock = Boolean.Parse(value);
                    break;
                case "block-summary":
                    Config.BlockSummary = value;
                    break;
                case "confirm-ignored":
                    Config.ConfirmIgnored = Boolean.Parse(value);
                    break;
                case "confirm-multiple":
                    Config.ConfirmMultiple = Boolean.Parse(value);
                    break;
                case "confirm-same":
                    Config.ConfirmSame = Boolean.Parse(value);
                    break;
                case "confirm-page":
                    Config.ConfirmPage = Boolean.Parse(value);
                    break;
                case "confirm-range":
                    Config.ConfirmRange = Boolean.Parse(value);
                    break;
                case "confirm-self-revert":
                    Config.ConfirmSelfRevert = false;
                    break;
                case "confirm-warned":
                    Config.ConfirmWarned = Boolean.Parse(value);
                    break;
                case "default-summary":
                    Config.DefaultSummary = value;
                    break;
                case "diff-font-size":
                    Config.DiffFontSize = value;
                    break;
                case "irc":
                    Config.UseIrc = Boolean.Parse(value);
                    break;
                case "rollback":
                    Config.RequireRollback = Boolean.Parse(value);
                    break;
                case "minor":
                    
                    break;
                case "open-in-browser":
                    Config.OpenInBrowser = Boolean.Parse(value);
                    break;
                case "patrol-speedy":
                    Config.PatrolSpeedy = Boolean.Parse(value);
                    break;
                case "preload":
                    Config.Preloads = int.Parse(value);
                    break;
                case "prod":
                    Config.Prod = Boolean.Parse(value);
                    break;
                case "prod-log":
                    Config.ProdLogs = Boolean.Parse(value);
                    break;
                case "prod-message":
                    Config.ProdMessage = value;
                    break;
                case "prod-page":
                    Config.ProdLogs_Name = value;
                    break;
                case "prod-message-summary":
                    Config.ProdMessageSummary = value;
                    break;
                case "prod-message-title":
                    Config.ProdMessageTitle = value;
                    break;
                case "prod-summary":
                    Config.ProdSummary = value;
                    break;
                case "protection-reason":
                    Config.ProtectionReason = value;
                    break;
                case "show-queue":
                    Config.ShowQueue = Boolean.Parse(value);
                    break;
                case "show-tool-tips":
                    Config.ShowToolTips = Boolean.Parse(value);
                    break;
                case "speedy-message-title":
                    Config.SpeedyMessageTitle = value;
                    break;
                case "speedy-summary":
                    Config.SpeedySummary = value;
                    break;
                case "tray-icon":
                    Config.TrayIcon = Boolean.Parse(value);
                    break;
                case "undo-summary":
                    Config.UndoSummary = value;
                    break;
                case "update-whitelist":
                    Config.UpdateWhitelist = Boolean.Parse(value);
                    break;
                case "vandal-report-reason":
                    Config.VandalReportReason = value;
                    break;
                case "welcome":
                    Config.Welcome = value;
                    break;
                case "welcome-anon":
                    Config.WelcomeAnon = value;
                    break;
                case "warn-summary":
                    Config.WarnSummary = value;
                    break;
                case "protection-requests":
                    Config.ProtectionRequests = Boolean.Parse(value);
                    break;
                case "irc-port":
                    Config.IrcPort = int.Parse(value);
                    break;
                case "enable":
                    Config.Enabled = Boolean.Parse(value);
                    break;

            }
            return true;
        }

        /// <summary>
        /// config.txt
        /// </summary>
        /// <returns></returns>
        public static bool LoadLocalConfig()
        {
            Core.History("LoadLocalConfig()");
            if (!System.IO.File.Exists(Core.LocalPath() + Config.LocalConfigLocation))
            {
                try
                {
                    if ( ! Directory.Exists(Core.LocalPath() ))
                    {
                        Directory.CreateDirectory(Core.LocalPath());
                    }
                    File.WriteAllText((Core.LocalPath() + Config.LocalConfigLocation), huggle3.Properties.Resources.DefaultLocalConfig);
                } catch ( DirectoryNotFoundException A )
                    {
                        Core.ExceptionHandler(A);
                        return true;
                    }
            }

            if  ( System.IO.File.Exists(Core.LocalPath() + Config.LocalConfigLocation) )
            {
                foreach (KeyValuePair<string,string> Item in ProcessConfigFile(File.ReadAllText(Core.LocalPath() + Config.LocalConfigLocation)))
                {
                    SetLocalConfigOption(Item.Key, Item.Value);
                }
            }
            if (Config.Projects.Count == 0)
            { 
                foreach ( KeyValuePair<string,string> Item in ProcessConfigFile(huggle3.Properties.Resources.DefaultLocalConfig) )
                {
                    SetLocalConfigOption(Item.Key, Item.Value);
                }
            }
            return true;
        }
    }

}
