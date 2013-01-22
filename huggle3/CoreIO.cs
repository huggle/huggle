//This is a source code or part of Huggle project
//
//This file contains code for core

/// <DOCUMENTATION>
/// There is no documentation for this
/// </DOCUMENTATION>

//Copyright (C) 2011-2012 Huggle team
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace huggle3
{
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
                Dictionary<string, string> return_value = new Dictionary<string, string>();
                string current_value;

                foreach (string Item in GET.list(data))
                {
                    current_value = Item;
                    current_value = current_value.Trim(' ', '\t', '\n').Replace("\\;", Convert.ToChar(2).ToString());

                    if (current_value.Contains(";"))
                    {
                        string KEY = current_value.Split(';')[0].Replace(Convert.ToChar(2), ';');
                        string VAL = current_value.Split(';')[1].Replace(Convert.ToChar(2), ';');
                        if (!return_value.ContainsKey(KEY))
                        {
                            return_value.Add(KEY, VAL);
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
        public static Dictionary<string, string> ProcessConfigFile(string name)
        {
            Core.History("CoreIO.ProcessConfigFile()");
            List<string> Items = new List<string>(name.Replace("\t", "    ").Split('\n'));
            Dictionary<string, string> value = new Dictionary<string, string>();

            int Indent = 0;
            int i = 0;

            while (i < Items.Count)
            {
                if ((i > 0) && (Items[i].StartsWith(" ")) && (Items[i].Replace(" ", "") != ""))
                {
                    if (Indent == 0)
                    {
                        while (Items[i][Indent] == ' ')
                        {
                            Indent += 1;
                        }
                    }

                    Items[i] = Items[i].Substring(Indent);
                    Items[i - 1] = Items[i - 1] + Convert.ToChar(2) + Items[i].TrimEnd(' ');
                    Items.RemoveAt(i);
                }
                else if (Items[i].StartsWith("#") || Items[i].StartsWith("<") || (!Items[i].Contains(":")))
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

            foreach (string Item in Items)
            {
                string Name = Item.Split(':')[0].Trim(Convert.ToChar(2));
                string Value = Item.Substring(Item.IndexOf(":") + 1).Trim(Convert.ToChar(2)).Trim(Convert.ToChar(13));

                if (!value.ContainsKey(Name))
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
                    if (Config.Projects.ContainsKey("test2") == false)
                    {
                        Config.Projects.Add("test2", Config.TestWp);
                    }
                    break;
                case "project":
                    Config.Project = value;
                    break;
                case "proxy-enabled":
                    Config.ProxyEnabled = Boolean.Parse(value);
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
                case "show-new-messages":
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
                    Config.WindowSize.Height = int.Parse(value);
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
            // project config only
            switch (key)
            {
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
                    Config.MultipleRevertSummaryParts = GET.list(value);
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
                case "quick-sight":
                    Config.QuickSight = bool.Parse(value);
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
                case "whitelist-edit-count":
                    Config.WhitelistEditCount = int.Parse(value);
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
                case "protection-reason":
                    Config.ProtectionReason = value;
                    break;
                case "show-queue":
                    Config.ShowQueue = Boolean.Parse(value);
                    break;
                case "show-tool-tips":
                    Config.ShowToolTips = Boolean.Parse(value);
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
                case "warn-summary":
                    Config.WarnSummary = value;
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

        public static void PostLoad()
        {
            Core.History("PostLoad()");
            try
            {
                if (Queue.All.Count == 0)
                {
                    Core.WriteLog("There is no queue in config, registering defaults");
                    Queue edits = new Queue("All edits");
                    edits._Diffs = Queue.DiffMode.Preload;
                    edits._PageRegex = new System.Text.RegularExpressions.Regex(".*");
                    edits._SortOrder = Queue.QueueSortOrder.Quality;
                    edits._SummaryRegex = new System.Text.RegularExpressions.Regex(".*");
                    edits._UserRegex = new System.Text.RegularExpressions.Regex(".*");
                    Queue.All.Add("All edits", edits);
                }
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
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
                    if (!Directory.Exists(Core.LocalPath()))
                    {
                        Directory.CreateDirectory(Core.LocalPath());
                    }
                    File.WriteAllText((Core.LocalPath() + Config.LocalConfigLocation), huggle3.Properties.Resources.DefaultLocalConfig);
                }
                catch (DirectoryNotFoundException A)
                {
                    Core.ExceptionHandler(A);
                    return true;
                }
            }

            if (System.IO.File.Exists(Core.LocalPath() + Config.LocalConfigLocation))
            {
                foreach (KeyValuePair<string, string> Item in ProcessConfigFile(File.ReadAllText(Core.LocalPath() + Config.LocalConfigLocation)))
                {
                    SetLocalConfigOption(Item.Key, Item.Value);
                }
            }
            if (Config.Projects.Count == 0)
            {
                foreach (KeyValuePair<string, string> Item in ProcessConfigFile(huggle3.Properties.Resources.DefaultLocalConfig))
                {
                    SetLocalConfigOption(Item.Key, Item.Value);
                }
            }
            return true;
        }
    }
}
