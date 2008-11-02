Imports System.IO
Imports System.Text.RegularExpressions

Module ConfigIO

    Public Function ProcessConfigFile(ByVal File As String) As Dictionary(Of String, String)

        'Convert a configuration file to a list of option name/option value pairs
        'Remove comments and lines without ':', combine multi-line options, replace \n with line break,
        'strip leading/trailing whitespace and split into keys/values

        Dim Items As New List(Of String)(File.Replace(CR, "").Replace(Tab, "   ").Split(LF))
        Dim Result As New Dictionary(Of String, String)

        Dim i As Integer

        While i < Items.Count
            If i > 0 AndAlso Items(i).StartsWith(" ") Then
                Items(i - 1) &= Items(i).Trim(" "c)
                Items.RemoveAt(i)
            ElseIf Items(i).StartsWith("#") OrElse Items(i).StartsWith("<") OrElse Not Items(i).Contains(":") Then
                Items.RemoveAt(i)
            Else
                Items(i) = Items(i).Replace("\n", LF).Trim(" "c)
                i += 1
            End If
        End While

        For Each Item As String In Items
            Dim Name As String = Item.Split(":"c)(0).ToLower
            Dim Value As String = Item.Substring(Item.IndexOf(":"c) + 1)

            If Result.ContainsKey(Name) _
                Then Log("Warning: Duplicate definition for option '" & Name & "' in configuration file") _
                Else Result.Add(Name, Value)
        Next Item

        Return Result
    End Function

    'Converts a comma-separated list to a list
    Function GetList(ByVal Value As String) As List(Of String)
        Dim List As New List(Of String)

        For Each Item As String In Value.Replace("\,", Convert.ToChar(1)).Split(","c)
            Item = Item.Trim(" "c, Tab, CR, LF).Replace(Convert.ToChar(1), ",")
            If Not List.Contains(Item) AndAlso Item.Length > 0 Then List.Add(Item)
        Next Item

        Return List
    End Function

    'Converts a comma-separated list of semicolon-separated pairs to a dictionary
    Function GetDictionary(ByVal Text As String) As Dictionary(Of String, String)
        Dim Result As New Dictionary(Of String, String)

        For Each Item As String In GetList(Text)
            Item = Item.Trim(" "c, Tab, CR, LF).Replace("\;", Convert.ToChar(2))

            If Item.Contains(";") Then
                Dim Key As String = Item.Split(";"c)(0).Replace(Convert.ToChar(2), ";")
                Dim Value As String = Item.Split(";"c)(1).Replace(Convert.ToChar(2), ";")
                If Not Result.ContainsKey(Key) Then Result.Add(Key, Value)
            End If
        Next Item

        Return Result
    End Function

    'Converts a comma-separated list of semicolon-separated fields to a list of lists
    Function GetRecordList(ByVal Text As String, ByVal Fields As Integer) As List(Of List(Of String))
        Dim Result As New List(Of List(Of String))

        For Each Record As String In GetList(Text)
            Dim List As New List(Of String)

            For Each Field As String In Record.Replace("\;", Convert.ToChar(2)).Split(";"c)
                Field = Field.Trim(" "c, Tab, CR, LF).Replace(Convert.ToChar(2), ";")
                If List.Count < Fields Then List.Add(Field)
            Next Field

            While List.Count < Fields
                List.Add("")
            End While

            Result.Add(List)
        Next Record

        Return Result
    End Function

    'Convert a string of the form x.y.zzzz to a Version
    Private Function ParseVersion(ByVal Text As String) As Version
        Return New Version(CInt(Text.Substring(0, 1)), CInt(Text.Substring(2, 1)), CInt(Text.Substring(4)))
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
            Case "sensitive-addresses" : Config.SensitiveAddresses = GetDictionary(Value)
            Case "user-agent" : Config.UserAgent = Value.Replace("$1", Config.Version.ToString)
            Case "user-config" : Config.UserConfigLocation = Value
            Case "version" : Config.LatestVersion = ParseVersion(Value)
        End Select
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

            Case Else 'Warnings
                If Config.WarningSeries.Contains(Regex.Match(Name, "(.*)(1|2|3|4|4im)").Groups(1).Value) _
                    Then Config.WarningMessages.Add(Name, Value)
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
            Case "namespace-aliases" : SetNamespaceAliases(Value)
            Case "namespace-names" : SetNamespaceNames(Value)
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
            Case "revert-summaries" : If Value <> "" Then Config.RevertSummaries = GetList(Value)
            Case "templates" : Config.TemplateMessages = GetList(Value)
            Case "version" : Config.ConfigVersion = ParseVersion(Value)
        End Select
    End Sub

    Private Sub SetLocalConfigOption(ByVal Name As String, ByVal Value As String)
        'Local config only
        Select Case Name
            Case "irc" : Config.IrcMode = CBool(Value)
            Case "language" : Config.Language = Value
            Case "log-file" : Config.LogFile = Value
            Case "password" : Config.Password = Value : Config.RememberPassword = True
            Case "project" : Config.Project = Config.Projects(Value)
            Case "projects" : SetProjects(Value)
            Case "proxy-enabled" : Config.ProxyEnabled = CBool(Value)
            Case "proxy-port" : Config.ProxyPort = Value
            Case "proxy-server" : Config.ProxyServer = Value
            Case "proxy-userdomain" : Config.ProxyUserDomain = Value
            Case "proxy-username" : Config.ProxyUsername = Value
            Case "queue-right-align" : Config.RightAlignQueue = CBool(Value)
            Case "username" : Config.Username = Value
            Case "window-height" : Config.WindowSize.Height = CInt(Value)
            Case "window-left" : Config.WindowPosition.X = CInt(Value)
            Case "window-maximize" : Config.WindowMaximize = CBool(Value)
            Case "window-top" : Config.WindowPosition.Y = CInt(Value)
            Case "window-width" : Config.WindowSize.Width = CInt(Value)
            Case "shortcuts" : SetShortcuts(Value)
            Case "revert-summaries" : Config.RevertSummaries = GetList(Value)

            Case Else : If Name.StartsWith("queues-") Then QueueNames.Add(Name.Substring(7), GetList(Value))
        End Select
    End Sub

    Private Sub SetMinor(ByVal Value As String)
        Dim Items As List(Of String) = GetList(Value.ToLower)

        Config.MinorManual = (Items.Contains("manual"))
        Config.MinorNotifications = (Items.Contains("notifications"))
        Config.MinorOther = (Items.Contains("reports"))
        Config.MinorReverts = (Items.Contains("reverts"))
        Config.MinorTags = (Items.Contains("tags"))
        Config.MinorWarnings = (Items.Contains("warnings"))
    End Sub

    Private Sub SetNamespaceNames(ByVal Value As String)
        For Each Item As KeyValuePair(Of String, String) In GetDictionary(Value)
            Space.SetName(CInt(Item.Key), Item.Value)
        Next Item
    End Sub

    Private Sub SetNamespaceAliases(ByVal Value As String)
        For Each Item As KeyValuePair(Of String, String) In GetDictionary(Value)
            If Not Space.Aliases.ContainsKey(Item.Key) Then Space.Aliases.Add(Item.Key, CInt(Item.Value))
        Next Item
    End Sub

    Private Sub SetProjects(ByVal Value As String)
        Config.Projects.Clear()

        For Each Item As List(Of String) In GetRecordList(Value, 3)
            Dim NewProject As New Project

            NewProject.Name = Item(0)
            NewProject.Path = Item(1)
            NewProject.IrcChannel = Item(2)

            Config.Projects.Add(NewProject.Name, NewProject)
        Next Item
    End Sub

    Private Sub SetReport(ByVal Value As String)
        Config.AutoReport = (Value = "auto")
        Config.PromptForReport = (Value = "prompt")
    End Sub

    Private Sub SetRevertPatterns(ByVal Values As List(Of String))
        For Each Item As String In Values
            Config.RevertPatterns.Add(New Regex("^" & Item & "$", RegexOptions.Compiled Or RegexOptions.IgnoreCase))
        Next Item
    End Sub

    Private Sub SetShortcuts(ByVal Value As String)
        For Each Item As List(Of String) In GetRecordList(Value, 5)
            If ShortcutKeys.ContainsKey(Item(0)) Then ShortcutKeys(Item(0)) = _
                New Shortcut(CType(CInt(Item(1)), Keys), (Item(2) <> "0"), (Item(3) <> "0"), (Item(4) <> "0"))
        Next Item
    End Sub

    Private Sub SetSpeedyOptions(ByVal Value As String)
        Config.SpeedyCriteria.Clear()

        For Each Item As List(Of String) In GetRecordList(Value, 5)
            Dim Criterion As New SpeedyCriterion

            Criterion.Code = Item(0)
            Criterion.Description = Item(1)

            If Criterion.Code.Contains("-") _
                Then Criterion.DisplayCode = Criterion.Code.Substring(0, Criterion.Code.IndexOf("-")) _
                Else Criterion.DisplayCode = Criterion.Code

            If Item(2).Contains("|") Then
                Criterion.Template = Item(2).Split("|"c)(0)
                Criterion.Parameter = Item(2).Split("|"c)(1)
            Else
                Criterion.Template = Item(2)
            End If

            Criterion.Message = Item(3)
            Criterion.Notify = (Item(4) = "notify")

            Config.SpeedyCriteria.Add(Criterion.Code, Criterion)
        Next Item
    End Sub

    Private Sub SetTags(ByVal Value As String)
        Config.Tags.Clear()

        For Each Item As String In GetList(Value)
            Config.Tags.Add("{{" & Item & "}}")
        Next Item
    End Sub

    Private Sub SetWatch(ByVal Value As String)
        Dim Items As List(Of String) = GetList(Value.ToLower)

        Config.WatchManual = (Items.Contains("manual"))
        Config.WatchNotifications = (Items.Contains("notifications"))
        Config.WatchOther = (Items.Contains("reports"))
        Config.WatchReverts = (Items.Contains("reverts"))
        Config.WatchTags = (Items.Contains("tags"))
        Config.WatchWarnings = (Items.Contains("warnings"))
    End Sub

    Public Function L10nLocation() As String
        Return LocalConfigPath() & "\Localization"
    End Function

    Public Function ListsLocation() As String
        Return LocalConfigPath() & "\Lists\" & Config.Project.Name
    End Function

    Public Function QueuesLocation() As String
        Return LocalConfigPath() & "\Queues\" & Config.Project.Name
    End Function

    Public Sub LoadLocalConfig()
        'Read from local configuration file

        If Not File.Exists(LocalConfigPath() & Config.LocalConfigLocation) _
            Then File.WriteAllText(LocalConfigPath() & Config.LocalConfigLocation, My.Resources.DefaultLocalConfig)

        QueueNames.Clear()
        InitialiseShortcuts()

        If File.Exists(LocalConfigPath() & Config.LocalConfigLocation) Then
            For Each Item As KeyValuePair(Of String, String) In _
                ProcessConfigFile(File.ReadAllText(LocalConfigPath() & Config.LocalConfigLocation))

                Try
                    SetLocalConfigOption(Item.Key, Item.Value)
                Catch
                    'Ignore malformed config entries
                End Try
            Next Item
        End If

        'Load projects if not already present; for compatibility with config files from previous versions
        If Config.Projects.Count = 0 Then
            For Each Item As KeyValuePair(Of String, String) In ProcessConfigFile(My.Resources.DefaultLocalConfig)
                SetLocalConfigOption(Item.Key, Item.Value)
            Next Item
        End If
    End Sub

    Public Sub SaveLocalConfig()
        'Write to local configuration file
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

        If MainForm IsNot Nothing Then
            Items.Add("window-height:" & CStr(MainForm.Height))
            Items.Add("window-left:" & CStr(MainForm.Left))
            Items.Add("window-maximize:" & CStr(MainForm.WindowState = FormWindowState.Maximized).ToLower)
            Items.Add("window-top:" & CStr(MainForm.Top))
            Items.Add("window-width:" & CStr(MainForm.Width))
        End If

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
        If AllLists.Count > 0 Then
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
        End If
    End Sub

    Public Sub LoadQueues()
        Queue.All.Clear()

        If Not QueueNames.ContainsKey(Config.Project.Name) Then QueueNames.Add(Config.Project.Name, New List(Of String))

        'Load queues from application data subfolder
        If Directory.Exists(QueuesLocation) Then
            For Each QueueName As String In QueueNames(Config.Project.Name)

                Dim ConfigItems As Dictionary(Of String, String) = _
                    ProcessConfigFile(File.ReadAllText(QueuesLocation() & "\" & QueueName & ".txt"))

                If ConfigItems.ContainsKey("name") Then
                    Dim Queue As New Queue(ConfigItems("name"))

                    For Each Item As KeyValuePair(Of String, String) In ConfigItems
                        SetQueueOption(Queue, Item.Key, Item.Value)
                    Next Item

                    If Queue IsNot Nothing Then Queue.Reset()
                End If
            Next QueueName
        End If

        If Queue.All.ContainsKey("Filtered edits") Then Queue.Default = Queue.All("Filtered edits")
        SetDefaultQueues()
    End Sub

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

    Private Sub SetQueueOption(ByVal Queue As Queue, ByVal Name As String, ByVal Value As String)
        Select Case Name
            Case "filter-anonymous" : Queue.FilterAnonymous = CType(Value, QueueFilter)
            Case "filter-assisted" : Queue.FilterAssisted = CType(Value, QueueFilter)
            Case "filter-bot" : Queue.FilterBot = CType(Value, QueueFilter)
            Case "filter-huggle" : Queue.FilterHuggle = CType(Value, QueueFilter)
            Case "filter-ignored" : Queue.FilterIgnored = CType(Value, QueueFilter)
            Case "filter-me" : Queue.FilterMe = CType(Value, QueueFilter)
            Case "filter-new-pages" : Queue.FilterNewPage = CType(Value, QueueFilter)
            Case "filter-notifications" : Queue.FilterNotifications = CType(Value, QueueFilter)
            Case "filter-own-userspace" : Queue.FilterOwnUserspace = CType(Value, QueueFilter)
            Case "filter-reverts" : Queue.FilterReverts = CType(Value, QueueFilter)
            Case "filter-tags" : Queue.FilterTags = CType(Value, QueueFilter)
            Case "filter-warnings" : Queue.FilterWarnings = CType(Value, QueueFilter)
            Case "ignore-pages" : Queue.IgnorePages = CBool(Value)
            Case "list" : If AllLists.ContainsKey(Value) Then Queue.ListName = Value
            Case "page-regex" : Queue.PageRegex = New Regex(Value, RegexOptions.Compiled)
            Case "remove-after" : Queue.RemoveAfter = CInt(Value)
            Case "remove-old" : Queue.RemoveOld = CBool(Value)
            Case "remove-viewed" : Queue.RemoveViewed = CBool(Value)
            Case "sort-order" : Queue.SortOrder = CType(Value, QueueSortOrder)
            Case "spaces" : Queue.Spaces.AddRange(SetQueueSpaces(Value))
            Case "summary-regex" : Queue.SummaryRegex = New Regex(Value, RegexOptions.Compiled)
            Case "type" : Queue.Type = SetQueueType(Value)
            Case "user-regex" : Queue.UserRegex = New Regex(Value, RegexOptions.Compiled)
        End Select
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
        Dim Spaces As New List(Of Space)

        For Each Item As String In GetList(Value)
            If Not Spaces.Contains(Space.GetSpace(CInt(Item))) Then Spaces.Add(Space.GetSpace(CInt(Item)))
        Next Item

        Return Spaces
    End Function

    Private Function SetQueueType(ByVal Value As String) As QueueType
        Select Case Value
            Case "FixedList" : Return QueueType.FixedList
            Case "LiveList" : Return QueueType.LiveList
            Case Else : Return QueueType.Live
        End Select
    End Function

End Module