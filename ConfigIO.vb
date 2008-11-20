Imports System.IO
Imports System.Text.RegularExpressions

Module ConfigIO

    Public Function ProcessConfigFile(ByVal File As String) As Dictionary(Of String, String)

        'Convert a configuration file to a list of option name/option value pairs
        'Remove comments and lines without ':', combine multi-line options, replace \n with line break,
        'strip leading/trailing whitespace and split into keys/values

        Dim Items As New List(Of String)(File.Replace(CR, "").Replace(Tab, "    ").Split(LF))
        Dim Result As New Dictionary(Of String, String)

        Dim i As Integer, Indent As Integer

        While i < Items.Count
            If i > 0 AndAlso Items(i).StartsWith(" ") AndAlso Items(i).Replace(" ", "") <> "" Then
                If Indent = 0 Then
                    While Items(i)(Indent) = " "
                        Indent += 1
                    End While
                End If

                For j As Integer = 1 To Indent
                    Items(i) = Items(i).Substring(1)
                    If Not Items(i).StartsWith(" ") Then Exit For
                Next j

                Items(i - 1) &= Convert.ToChar(2) & Items(i).TrimEnd(" "c)
                Items.RemoveAt(i)

            ElseIf Items(i).StartsWith("#") OrElse Items(i).StartsWith("<") OrElse Not Items(i).Contains(":") Then
                Items.RemoveAt(i)
            Else
                Indent = 0
                Items(i) = Items(i).Replace("\n", LF).Trim(" "c)
                i += 1
            End If
        End While

        For Each Item As String In Items
            Dim Name As String = Item.Split(":"c)(0)
            Dim Value As String = Item.Substring(Item.IndexOf(":"c) + 1)

            If Not Result.ContainsKey(Name) Then Result.Add(Name, Value)
        Next Item

        Return Result
    End Function

    'Converts a comma-separated list to a list
    Function GetList(ByVal Value As String) As List(Of String)
        Dim List As New List(Of String)

        For Each Item As String In Value.Replace(Convert.ToChar(2), "").Replace("\,", Convert.ToChar(1)).Split(","c)
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

    Function GetNestedConfig(ByVal Text As String) As Dictionary(Of String, String)
        Return ProcessConfigFile(Text.Replace(Convert.ToChar(2), LF))
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
            Case "projects" : Config.Projects = GetDictionary(Value)
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
            Case "confirm-page" : Config.ConfirmPage = CBool(Value)
            Case "confirm-range" : Config.ConfirmRange = CBool(Value)
            Case "confirm-same" : Config.ConfirmSame = CBool(Value)
            Case "confirm-self-revert" : Config.ConfirmSelfRevert = CBool(Value)
            Case "confirm-warned" : Config.ConfirmWarned = CBool(Value)
            Case "default-summary" : Config.DefaultSummary = Value
            Case "diff-font-size" : Config.DiffFontSize = Value
            Case "irc" : Config.UseIrc = CBool(Value)
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
            Case "vandal-report-reason" : Config.VandalReportReason = Value
            Case "watch" : SetWatch(Value)
            Case "welcome" : Config.Welcome = Value
            Case "welcome-anon" : Config.WelcomeAnon = Value
            Case "warn-summary" : Config.WarnSummary = Value
            Case "warn-summary-2" : Config.WarnSummary2 = Value
            Case "warn-summary-3" : Config.WarnSummary3 = Value
            Case "warn-summary-4" : Config.WarnSummary4 = Value

            Case Else 'Warnings
                If Config.WarningTypes.ContainsKey(Regex.Match(Name, "(.*)(1|2|3|4|4im)").Groups(1).Value) _
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
            Case "default-queue" : Config.DefaultQueue = Value
            Case "default-queue-2" : Config.DefaultQueue2 = Value
            Case "delete" : Config.Delete = CBool(Value)
            Case "email" : Config.Email = CBool(Value)
            Case "email-subject" : Config.EmailSubject = Value
            Case "enable-all" : Config.EnabledForAll = CBool(Value)
            Case "go" : Config.GoToPages = GetList(Value)
            Case "irc-channel" : Config.IrcChannel = Value
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
            Case "queues" : SetQueues(Value)
            Case "quick-sight" : Config.QuickSight = CBool(Value)
            Case "rc-block-size" : Config.RcBlockSize = CInt(Value)
            Case "require-admin" : Config.RequireAdmin = CBool(Value)
            Case "require-autconfirmed" : Config.RequireAutoconfirmed = CBool(Value)
            Case "require-config" : Config.RequireConfig = CBool(Value)
            Case "require-edits" : Config.RequireEdits = CInt(Value)
            Case "require-rollback" : Config.RequireRollback = CBool(Value)
            Case "require-time" : Config.RequireTime = CInt(Value)
            Case "revert-patterns" : SetRevertPatterns(GetList(Value))
            Case "revert-summaries" : Config.CustomRevertSummaries = GetList(Value)
            Case "rollback-summary" : Config.RollbackSummary = Value
            Case "rfd" : Config.RfdLocation = Value
            Case "save-config" : Config.SaveConfig = CBool(Value)
            Case "shared-ip-templates" : Config.SharedIPTemplates = GetList(Value)
            Case "sight" : Config.Sight = CBool(Value)
            Case "single-revert-summary" : Config.SingleRevertSummary = Value
            Case "sock-reports" : Config.SockReportLocation = Value
            Case "speedy-delete-summary" : Config.SpeedyDeleteSummary = Value
            Case "speedy-options" : SetSpeedyOptions(Value)
            Case "startup-message-location" : Config.StartupPage = Value
            Case "summary" : Config.Summary = Value
            Case "tag-summaries" : Config.TagSummaries = GetList(Value)
            Case "template-message-summary" : Config.TemplateMessageSummary = Value
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
            Case "welcome-summary" : Config.WelcomeSummary = Value
            Case "warning-types" : Config.WarningTypes = GetDictionary(Value)
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
            Case "username-listed" : Config.UsernameListed = CBool(Value)
            Case "version" : Config.ConfigVersion = ParseVersion(Value)
        End Select
    End Sub

    Private Sub SetLocalConfigOption(ByVal Name As String, ByVal Value As String)
        'Local config only
        Select Case Name
            Case "language" : Config.Language = Value
            Case "log-file" : Config.LogFile = Value
            Case "password" : Config.Password = Value : Config.RememberPassword = True
            Case "project" : Config.Project = Value
            Case "projects" : Config.Projects = GetDictionary(Value)
            Case "proxy-enabled" : Config.ProxyEnabled = CBool(Value)
            Case "proxy-port" : Config.ProxyPort = Value
            Case "proxy-server" : Config.ProxyServer = Value
            Case "proxy-userdomain" : Config.ProxyUserDomain = Value
            Case "proxy-username" : Config.ProxyUsername = Value
            Case "queue-right-align" : Config.RightAlignQueue = CBool(Value)
            Case "revert-summaries" : Config.RevertSummaries = GetList(Value)
            Case "shortcuts" : SetShortcuts(Value)
            Case "show-new-messages" : Config.ShowNewMessages = CBool(Value)
            Case "show-two-queues" : Config.ShowTwoQueues = CBool(Value)
            Case "username" : Config.Username = Value
            Case "whitelist-timestamps" : Config.WhitelistTimestamps = GetDictionary(Value)
            Case "window-height" : Config.WindowSize.Height = CInt(Value)
            Case "window-left" : Config.WindowPosition.X = CInt(Value)
            Case "window-maximize" : Config.WindowMaximize = CBool(Value)
            Case "window-top" : Config.WindowPosition.Y = CInt(Value)
            Case "window-width" : Config.WindowSize.Width = CInt(Value)

            Case Else : If Name.StartsWith("queues-") Then QueueNames.Add(Name.Substring(7), GetList(Value))
        End Select
    End Sub

    Private Sub SetMinor(ByVal Value As String)
        For Each Item As String In GetList(Value.ToLower)
            If Config.Minor.ContainsKey(Item) Then Config.Minor(Item) = True
        Next Item
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

    Private Sub SetQueues(ByVal Value As String)
        For Each Item As KeyValuePair(Of String, String) In GetNestedConfig(Value)
            If Queue.All.ContainsKey(Item.Key) Then Queue.All.Remove(Item.Key)

            Dim NewQueue As New Queue(Item.Key)

            For Each Subitem As KeyValuePair(Of String, String) In GetNestedConfig(Item.Value)
                SetQueueOption(NewQueue, Subitem.Key, Subitem.Value)
            Next Subitem

            If NewQueue.Spaces.Count = 0 Then NewQueue.Spaces.AddRange(Space.All)

            NewQueue.Reset()
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
        Config.WatchDelete = False

        For Each Item As String In GetList(Value.ToLower)
            If Config.Watch.ContainsKey(Item) Then Config.Watch(Item) = True
            If Item = "delete" Then Config.WatchDelete = True
        Next Item
    End Sub

    Public Function L10nLocation() As String
        Return MakePath(LocalConfigPath(), "Localization")
    End Function

    Public Function ListsLocation() As String
        Return MakePath(LocalConfigPath(), "Lists", Config.Project)
    End Function

    Public Function QueuesLocation() As String
        Return MakePath(LocalConfigPath(), "Queues", Config.Project)
    End Function

    Public Function WhitelistsLocation() As String
        Return MakePath(LocalConfigPath(), "Whitelists")
    End Function

    Public Sub LoadLocalConfig()
        'Read from local configuration file

        If Not File.Exists(MakePath(LocalConfigPath, Config.LocalConfigLocation)) Then
            Try
                For Each List As KeyValuePair(Of String, List(Of String)) In AllLists
                    File.WriteAllText(MakePath(LocalConfigPath, Config.LocalConfigLocation), _
                        My.Resources.DefaultLocalConfig)
                Next List

            Catch ex As IOException
                MessageBox.Show("Unable to create configuration file: " & CRLF & ex.Message, "Huggle", _
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        QueueNames.Clear()
        InitialiseShortcuts()

        If File.Exists(MakePath(LocalConfigPath, Config.LocalConfigLocation)) Then
            For Each Item As KeyValuePair(Of String, String) In _
                ProcessConfigFile(File.ReadAllText(MakePath(LocalConfigPath, Config.LocalConfigLocation)))

                Try
                    SetLocalConfigOption(Item.Key, Item.Value)
                Catch
                    'Ignore malformed config entries
                End Try
            Next Item
        End If

        'Load projects if not already present
        If Config.Projects.Count = 0 Then
            For Each Item As KeyValuePair(Of String, String) In ProcessConfigFile(My.Resources.DefaultLocalConfig)
                SetLocalConfigOption(Item.Key, Item.Value)
            Next Item
        End If

#If DEBUG Then
        'If the app is in debug mode add a localhost wiki to the project list
        If Not Config.Projects.ContainsKey("localhost") Then Config.Projects.Add("localhost", "http://localhost/")
#End If

        SetUserTalkSummaries(My.Resources.WarningSummaries)
    End Sub

    Public Sub SaveLocalConfig()
        'Write to local configuration file
        Dim Items As New List(Of String)

        Items.Add("language:" & CStr(Config.Language))
        If Not String.IsNullOrEmpty(Config.LogFile) Then Items.Add("log-file:" & Config.LogFile)
        If Config.RememberPassword Then Items.Add("password:" & Config.Password)

        Items.Add("projects:")

        For Each Item As KeyValuePair(Of String, String) In Config.Projects
            If Item.Key <> "localhost" Then Items.Add("    " & Item.Key & ";" & Item.Value & ",")
        Next Item

        Items.Add("project:" & Config.Project)
        Items.Add("proxy-enabled:" & CStr(Config.ProxyEnabled).ToLower)
        If Not String.IsNullOrEmpty(Config.ProxyPort) Then Items.Add("proxy-port:" & Config.ProxyPort)
        If Not String.IsNullOrEmpty(Config.ProxyServer) Then Items.Add("proxy-server:" & Config.ProxyServer)
        If Not String.IsNullOrEmpty(Config.ProxyUserDomain) Then Items.Add("proxy-userdomain:" & Config.ProxyUserDomain)
        If Not String.IsNullOrEmpty(Config.ProxyUsername) Then Items.Add("proxy-username:" & Config.ProxyUsername)

        Items.Add("queue-right-align:" & CStr(Config.RightAlignQueue).ToLower)
        Items.Add("show-two-queues:" & CStr(Config.ShowTwoQueues).ToLower)
        Items.Add("show-new-messages" & CStr(Config.ShowNewMessages).ToLower)
        If Config.RememberMe Then Items.Add("username:" & Config.Username)
        Items.Add("whitelist-timestamps:")

        For Each Item As KeyValuePair(Of String, String) In Config.WhitelistTimestamps
            Items.Add("   " & Item.Key & ";" & Item.Value & ",")
        Next Item

        If MainForm IsNot Nothing Then
            Items.Add("window-height:" & CStr(MainForm.Height))
            Items.Add("window-left:" & CStr(MainForm.Left))
            Items.Add("window-maximize:" & CStr(MainForm.WindowState = FormWindowState.Maximized).ToLower)
            Items.Add("window-top:" & CStr(MainForm.Top))
            Items.Add("window-width:" & CStr(MainForm.Width))
        End If

        Items.Add("shortcuts:")

        For Each Item As KeyValuePair(Of String, Shortcut) In ShortcutKeys
            Items.Add("   " & Item.Key & ";" & CInt(Item.Value.Key).ToString & ";" & CInt(Item.Value.Control) _
                .ToString & ";" & CInt(Item.Value.Alt).ToString & ";" & CInt(Item.Value.Shift).ToString & ",")
        Next Item

        Dim Summaries As New List(Of String)

        For Each Item As String In Config.RevertSummaries
            If Not Summaries.Contains(Item) Then Summaries.Add(Item.Replace(",", "\,"))
        Next Item

        Items.Add("revert-summaries:")
        Items.Add(String.Join("," & CRLF, Summaries.ToArray))

        Try
            If Not Directory.Exists(LocalConfigPath) Then Directory.CreateDirectory(LocalConfigPath)
            File.WriteAllLines(MakePath(LocalConfigPath(), Config.LocalConfigLocation), Items.ToArray)
        Catch ex As IOException
            MessageBox.Show("Unable to save Huggle configuration: " & CRLF & ex.Message, "Huggle", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LoadLanguages()
        'Load localized message files
        Config.Messages.Clear()
        Config.Languages.Clear()

        If Not Directory.Exists(L10nLocation) OrElse Directory.GetFiles(L10nLocation).Length = 0 Then
            'On first run, use the built-in localization files to get as far as displaying the login screen
            LoadLanguage("en", My.Resources.en)
            LoadLanguage("de", My.Resources.de)
            LoadLanguage("pt", My.Resources.pt)

        Else
            For Each FileName As String In Directory.GetFiles(L10nLocation)
                LoadLanguage(Path.GetFileNameWithoutExtension(FileName), File.ReadAllText(FileName))
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
                    Dim MsgValue As String = Item.Substring(Item.IndexOf(":") + 1).Trim(" "c, CR).Replace("\n", CRLF)

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
            For Each FileName As String In Directory.GetFiles(ListsLocation)
                AllLists.Add(Path.GetFileNameWithoutExtension(FileName), _
                    New List(Of String)(File.ReadAllLines(FileName)))
            Next FileName
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
            Try
                For Each List As KeyValuePair(Of String, List(Of String)) In AllLists
                    File.WriteAllLines(ListsLocation() & "\" & List.Key & ".txt", List.Value.ToArray)
                Next List
            Catch ex As IOException
                MessageBox.Show("Unable to save lists: " & CRLF & ex.Message, "Huggle", _
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Sub LoadQueues()
        'Load queues from application data subfolder
        If Directory.Exists(QueuesLocation) Then
            For Each QueuePath As String In Directory.GetFiles(QueuesLocation)
                If Not QueueNames.ContainsKey(Config.Project) Then QueueNames.Add(Config.Project, New List(Of String))
                QueueNames(Config.Project).Add(Path.GetFileNameWithoutExtension(QueuePath))

                Dim ConfigItems As Dictionary(Of String, String) = ProcessConfigFile(File.ReadAllText(QueuePath))

                If ConfigItems.ContainsKey("name") Then
                    If Queue.All.ContainsKey(ConfigItems("name")) Then Queue.All.Remove(ConfigItems("name"))
                    Dim NewQueue As New Queue(ConfigItems("name"))

                    For Each Item As KeyValuePair(Of String, String) In ConfigItems
                        SetQueueOption(NewQueue, Item.Key, Item.Value)
                    Next Item

                    NewQueue.Reset()
                End If

            Next QueuePath
        End If

        If Queue.All.Count = 0 Then SetDefaultQueues()
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
            Items.Add("diff:" & Queue.DiffMode.ToString)
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
            Items.Add("refresh-always:" & CStr(Queue.RefreshAlways))
            Items.Add("refresh-interval:" & CStr(Queue.RefreshInterval))
            Items.Add("refresh-readd:" & CStr(Queue.RefreshReAdd))
            Items.Add("remove-after:" & CStr(Queue.RemoveAfter).ToLower)
            Items.Add("remove-old:" & CStr(Queue.RemoveOld).ToLower)
            Items.Add("remove-viewed:" & CStr(Queue.RemoveViewed))
            If Queue.RevisionRegex IsNot Nothing Then Items.Add("revision-regex:" & Queue.RevisionRegex.ToString)
            Items.Add("sort-order:" & CStr(CInt(Queue.SortOrder)))
            Items.Add("source:" & Queue.DynamicSource)
            Items.Add("source-type:" & Queue.DynamicSourceType)

            If Queue.Spaces.Count > 0 Then
                Dim Spaces As New List(Of String)

                For Each Space As Space In Queue.Spaces
                    If Space IsNot Nothing AndAlso Not Spaces.Contains(CStr(Space.Number)) _
                        Then Spaces.Add(CStr(Space.Number))
                Next Space

                Items.Add("spaces:" & String.Join(",", Spaces.ToArray))
            End If

            If Queue.SummaryRegex IsNot Nothing Then Items.Add("summary-regex:" & Queue.SummaryRegex.ToString)
            Items.Add("tray-notification:" & CStr(Queue.TrayNotification).ToLower)
            If Queue.UserRegex IsNot Nothing Then Items.Add("user-regex:" & Queue.UserRegex.ToString)
            If Queue.Users.Count > 0 Then Items.Add("users:" & String.Join(",", Queue.Users.ToArray))

            Try
                File.WriteAllLines(MakePath(QueuesLocation(), Queue.Name & ".txt"), Items.ToArray)

            Catch ex As IOException
                MessageBox.Show("Unable to save queues: " & CRLF & ex.Message, "Huggle", _
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next Queue
    End Sub

    Private Sub SetQueueOption(ByVal Queue As Queue, ByVal Name As String, ByVal Value As String)
        Select Case Name
            Case "diff" : Queue.DiffMode = SetQueueDiffMode(Value)
            Case "filter-anonymous" : Queue.FilterAnonymous = GetQueueFilter(Value)
            Case "filter-assisted" : Queue.FilterAssisted = GetQueueFilter(Value)
            Case "filter-bot" : Queue.FilterBot = GetQueueFilter(Value)
            Case "filter-huggle" : Queue.FilterHuggle = GetQueueFilter(Value)
            Case "filter-ignored" : Queue.FilterIgnored = GetQueueFilter(Value)
            Case "filter-me" : Queue.FilterMe = GetQueueFilter(Value)
            Case "filter-new-pages" : Queue.FilterNewPage = GetQueueFilter(Value)
            Case "filter-notifications" : Queue.FilterNotifications = GetQueueFilter(Value)
            Case "filter-own-userspace" : Queue.FilterOwnUserspace = GetQueueFilter(Value)
            Case "filter-reverts" : Queue.FilterReverts = GetQueueFilter(Value)
            Case "filter-tags" : Queue.FilterTags = GetQueueFilter(Value)
            Case "filter-warnings" : Queue.FilterWarnings = GetQueueFilter(Value)
            Case "ignore-pages" : Queue.IgnorePages = CBool(Value)
            Case "list" : If AllLists.ContainsKey(Value) Then Queue.ListName = Value
            Case "page-regex" : Queue.PageRegex = New Regex(Value, RegexOptions.Compiled)
            Case "refresh-always" : Queue.RefreshAlways = CBool(Value)
            Case "refresh-interval" : Queue.RefreshInterval = CInt(Value)
            Case "refresh-readd" : Queue.RefreshReAdd = CBool(Value)
            Case "revision-regex" : Queue.RevisionRegex = New Regex(Value, RegexOptions.Compiled)
            Case "remove-after" : Queue.RemoveAfter = CInt(Value)
            Case "remove-old" : Queue.RemoveOld = CBool(Value)
            Case "remove-viewed" : Queue.RemoveViewed = CBool(Value)
            Case "sort-order" : Queue.SortOrder = SetQueueSortOrder(Value)
            Case "source" : Queue.DynamicSource = Value
            Case "source-type" : Queue.DynamicSourceType = Value
            Case "spaces" : Queue.Spaces = SetQueueSpaces(Value)
            Case "summary-regex" : Queue.SummaryRegex = New Regex(Value, RegexOptions.Compiled)
            Case "tray-notification" : Queue.TrayNotification = CBool(Value)
            Case "type" : Queue.Type = SetQueueType(Value)
            Case "user-regex" : Queue.UserRegex = New Regex(Value, RegexOptions.Compiled)
            Case "users" : Queue.Users = GetList(Value)
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

        If Not Queue.All.ContainsKey("Filtered new pages") Then
            Dim NewQueue As New Queue("Filtered new pages")
            NewQueue.Type = QueueType.Live
            NewQueue.SortOrder = QueueSortOrder.Time
            NewQueue.FilterNewPage = QueueFilter.Require
            NewQueue.FilterIgnored = QueueFilter.Exclude
            NewQueue.Spaces = New List(Of Space)(New Space() {Space.Article})
            NewQueue.Reset()
        End If

        If Not Queue.All.ContainsKey("All edits") Then
            Dim NewQueue As New Queue("All edits")
            NewQueue.Type = QueueType.Live
            NewQueue.SortOrder = QueueSortOrder.Time
            NewQueue.Reset()
        End If

        If Not Queue.All.ContainsKey("All new pages") Then
            Dim NewQueue As New Queue("All new pages")
            NewQueue.Type = QueueType.Live
            NewQueue.SortOrder = QueueSortOrder.Time
            NewQueue.FilterNewPage = QueueFilter.Require
            NewQueue.Reset()
        End If
    End Sub

    Private Function SetQueueDiffMode(ByVal Value As String) As DiffMode
        Select Case Value.ToLower
            Case "0", "none" : Return DiffMode.None
            Case "1", "preload" : Return DiffMode.Preload
            Case "2", "all" : Return DiffMode.All
        End Select
    End Function

    Private Function GetQueueFilter(ByVal Value As String) As QueueFilter
        Select Case Value.ToLower
            Case "0", "exclude" : Return QueueFilter.Exclude
            Case "1", "require" : Return QueueFilter.Require
            Case "2", "none" : Return QueueFilter.None
        End Select
    End Function

    Private Function SetQueueSpaces(ByVal Value As String) As List(Of Space)
        Dim Spaces As New List(Of Space)

        For Each Item As String In GetList(Value)
            If Item = "all" Then
                For Each Space As Space In Space.All
                    If Not Spaces.Contains(Space) Then Spaces.Add(Space)
                Next Space
            Else
                If Not Spaces.Contains(Space.GetSpace(CInt(Item))) Then Spaces.Add(Space.GetSpace(CInt(Item)))
            End If
        Next Item

        Return Spaces
    End Function

    Private Function SetQueueSortOrder(ByVal Value As String) As QueueSortOrder
        Select Case Value.ToLower
            Case "0", "time" : Return QueueSortOrder.Time
            Case "1", "timereverse", "time-reverse" : Return QueueSortOrder.TimeReverse
            Case "2", "quality" : Return QueueSortOrder.Quality
        End Select
    End Function

    Private Function SetQueueType(ByVal Value As String) As QueueType
        Select Case Value.ToLower
            Case "fixedlist", "fixed-list" : Return QueueType.FixedList
            Case "livelist", "live-list" : Return QueueType.LiveList
            Case "dynamic" : Return QueueType.Dynamic
            Case Else : Return QueueType.Live
        End Select
    End Function

    Public Sub SaveWhitelist()
        If Whitelist.Count > 0 Then
            Try
                If Directory.Exists(WhitelistsLocation()) OrElse Directory.CreateDirectory(WhitelistsLocation()).Exists _
                    Then File.WriteAllLines(MakePath(WhitelistsLocation(), Config.Project & ".txt"), Whitelist.ToArray)
            Catch ex As IOException
                Log("Unable to save whitelist: " & ex.Message)
            End Try
        End If
    End Sub

    Public Sub SetUserTalkSummaries(ByVal Text As String)
        Config.UserTalkSummaries.Clear()

        For Each Item As String In Split(Text, CRLF)
            If Item.Contains(Tab) Then
                Dim LevelName As String = Item.Substring(0, Item.IndexOf(Tab))
                Dim Value As String = Item.Substring(Item.IndexOf(Tab) + 1)

                If Not String.IsNullOrEmpty(Value) Then
                    Dim Level As UserLevel = UserLevel.None

                    Select Case LevelName
                        Case "b" : Level = UserLevel.Blocked
                        Case "n" : Level = UserLevel.Notification
                        Case "w" : Level = UserLevel.Warning
                        Case "w1" : Level = UserLevel.Warn1
                        Case "w2" : Level = UserLevel.Warn2
                        Case "w3" : Level = UserLevel.Warn3
                        Case "w4" : Level = UserLevel.WarnFinal
                        Case Else : Continue For
                    End Select

                    Config.UserTalkSummaries.Add(New Regex(Value, RegexOptions.Compiled), Level)
                End If
            End If
        Next Item
    End Sub

End Module