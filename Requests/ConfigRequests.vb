Imports System.Threading

Namespace Requests

    Class ConfigRequest : Inherits Request

        Public Function GetProjectConfig() As Boolean
            'Read project config page
            Dim Result As String = GetPageText(Config.ProjectConfigLocation)

            If Result Is Nothing Then
                Fail()
                Return False
            End If

            Dim i As Integer, ConfigItems As New List(Of String)(Result.Split(New String() {vbCrLf}, _
                StringSplitOptions.RemoveEmptyEntries))

            'Combine options broken across several lines into one option
            While i < ConfigItems.Count
                If i > 0 AndAlso ConfigItems(i).StartsWith(" ") Then
                    ConfigItems(i - 1) &= ConfigItems(i)
                    ConfigItems.RemoveAt(i)
                ElseIf ConfigItems(i).StartsWith("#") OrElse Not ConfigItems(i).Contains(":") Then
                    ConfigItems.RemoveAt(i)
                Else
                    i += 1
                End If
            End While

            For Each Item As String In ConfigItems
                Dim OptionName As String = Item.Substring(0, Item.IndexOf(":")).ToLower.Trim(" "c)
                Dim OptionValue As String = Item.Substring(Item.IndexOf(":") + 1) _
                    .Trim(CChar(vbCrLf)).Replace("\n", vbCrLf).Trim(" "c)

                Try
                    SetSharedConfigOption(OptionName, OptionValue)
                    SetProjectConfigOption(OptionName, OptionValue)

                Catch ex As Exception
                    'Ignore malformed config entries
                End Try
            Next Item

            Complete()
            Return True
        End Function

        Public Function GetUserConfig() As Boolean
            'Read user config page
            Dim Result As String = GetPageText _
                (Config.UserConfigLocation.Replace("Special:Mypage", "User:" & Config.Username))

            If Result Is Nothing Then
                Fail()
                Return False
            End If

            Config.Enabled = (Not Config.RequireConfig)

            Dim i As Integer, ConfigItems As New List(Of String)(Result.Split(New String() {vbCrLf}, _
                StringSplitOptions.RemoveEmptyEntries))

            'Combine options broken across several lines into one option
            While i < ConfigItems.Count
                If i > 0 AndAlso ConfigItems(i).StartsWith(" ") Then
                    ConfigItems(i - 1) &= ConfigItems(i)
                    ConfigItems.RemoveAt(i)
                ElseIf ConfigItems(i).StartsWith("#") OrElse Not ConfigItems(i).Contains(":") Then
                    ConfigItems.RemoveAt(i)
                Else
                    i += 1
                End If
            End While

            For Each Item As String In ConfigItems
                Dim OptionName As String = Item.Substring(0, Item.IndexOf(":")).ToLower.Trim(" "c)
                Dim OptionValue As String = Item.Substring(Item.IndexOf(":") + 1) _
                    .Trim(CChar(vbCrLf)).Replace("\n", vbCrLf).Trim(" "c)

                Try
                    SetUserConfigOption(OptionName, OptionValue)
                    SetSharedConfigOption(OptionName, OptionValue)

                Catch ex As Exception
                    'Ignore malformed config entries
                End Try
            Next Item

            Complete()
            Return True
        End Function

        Public Sub GetUserConfigInThread()
            Dim RequestThread As New Thread(AddressOf UserConfigThread)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub UserConfigThread()
            GetUserConfig()
            If Config.Enabled Then Callback(AddressOf GetUserConfigDone) Else Callback(AddressOf GetUserConfigFailed)
        End Sub

        Private Sub GetUserConfigDone()
            If Config.UseAdminFunctions AndAlso Administrator Then
                MainForm.PageTagSpeedy.ShortcutKeyDisplayString = ""
                MainForm.UserReport.ShortcutKeyDisplayString = ""
                MainForm.PageDeleteB.Image = My.Resources.page_delete
                MainForm.UserBlock.Visible = True
                MainForm.PageDelete.Visible = False
            Else
                MainForm.PageTagSpeedy.ShortcutKeyDisplayString = "S"
                MainForm.UserReport.ShortcutKeyDisplayString = "B"
                MainForm.PageDeleteB.Image = My.Resources.page_speedy
                MainForm.UserBlock.Visible = False
                MainForm.PageDelete.Visible = False
            End If

            MainForm.TrayIcon.Visible = Config.TrayIcon
            Log("Loaded configuration page.")
            Complete()
        End Sub

        Private Sub GetUserConfigFailed()
            Log("Failed to load configuration page.")
            Fail()
        End Sub

        Private Sub SetSharedConfigOption(ByVal Name As String, ByVal Value As String)
            'Project and user config
            Select Case Name
                Case "enable" : Config.Enabled = CBool(Value)
                Case "admin" : Config.UseAdminFunctions = CBool(Value)
                Case "anonymous" : SetAnonymous(Value)
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
                Case "confirm-same" : Config.ConfirmSame = CBool(Value)
                Case "confirm-self-revert" : Config.ConfirmSelfRevert = CBool(Value)
                Case "default-summary" : Config.DefaultSummary = Value
                Case "diff-font-size" : Config.DiffFontSize = Value
                Case "extend-reports" : Config.ExtendReports = CBool(Value)
                Case "irc-port" : Config.IrcPort = CInt(Value)
                Case "minor" : SetMinor(Value)
                Case "namespaces" : SetNamespaces(Value)
                Case "new-pages" : Config.ShowNewPages = CBool(Value)
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
                Case "queue-max-age" : Config.QueueMaxAge = CInt(Value)
                Case "report" : SetReport(Value)
                Case "report-extend-summary" : Config.ReportExtendSummary = Value
                Case "report-summary" : Config.ReportSummary = Value
                Case "revert-summaries" : Config.CustomRevertSummaries = GetList(Value)
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
            End Select
        End Sub

        Private Sub SetProjectConfigOption(ByVal Name As String, ByVal Value As String)
            'Project config only
            Select Case Name
                Case "afd" : Config.AfdLocation = Value
                Case "aiv" : Config.AIVLocation = Value
                Case "aivbot" : Config.AIVBotLocation = Value
                Case "aiv-reports" : Config.AIV = CBool(Value)
                Case "aiv-single-note" : Config.AivSingleNote = Value
                Case "approval" : Config.Approval = CBool(Value)
                Case "block" : Config.Block = CBool(Value)
                Case "block-expiry-options" : Config.BlockExpiryOptions = GetList(Value)
                Case "cfd" : Config.CfdLocation = Value
                Case "config-summary" : Config.ConfigSummary = Value
                Case "delete" : Config.Delete = CBool(Value)
                Case "email" : Config.Email = CBool(Value)
                Case "email-subject" : Config.EmailSubject = Value
                Case "enable-all" : Config.EnabledForAll = CBool(Value)
                Case "go" : Config.GoToPages = GetList(Value)
                Case "ifd" : Config.IfdLocation = Value
                Case "ignore" : Config.IgnoredPages = GetList(Value)
                Case "manual-revert-summary" : Config.ManualRevertSummary = Value
                Case "mfd" : Config.MfdLocation = Value
                Case "min-version" : SetMinVersion(Value)
                Case "patrol" : Config.Patrol = CBool(Value)
                Case "protect" : Config.Protect = CBool(Value)
                Case "protection-request-page" : Config.ProtectionRequestPage = Value
                Case "protection-request-reason" : Config.ProtectionRequestReason = Value
                Case "protection-request-summary" : Config.ProtectionRequestSummary = Value
                Case "rc-block-size" : Config.RcBlockSize = CInt(Value)
                Case "report-link-diffs" : Config.ReportLinkDiffs = CBool(Value)
                Case "require-admin" : Config.RequireAdmin = CBool(Value)
                Case "require-config" : Config.RequireConfig = CBool(Value)
                Case "require-edits" : Config.RequireEdits = CInt(Value)
                Case "require-rollback" : Config.RequireRollback = CBool(Value)
                Case "require-time" : Config.RequireTime = CInt(Value)
                Case "rollback-summary" : Config.RollbackSummary = Value
                Case "rollback-summary-unknown" : Config.RollbackSummaryUnknown = Value
                Case "rfd" : Config.RfdLocation = Value
                Case "speedy-delete-summary" : Config.SpeedyDeleteSummary = Value
                Case "speedy-options" : SetSpeedyOptions(Value)
                Case "startup-message-location" : Config.StartupMessageLocation = Value
                Case "summary" : Config.Summary = Value
                Case "templates" : Config.TemplateMessagesGlobal = GetList(Value)
                Case "tfd" : Config.TfdLocation = Value
                Case "uaa" : Config.UAALocation = Value
                Case "uaabot" : Config.UAABotLocation = Value
                Case "update-whitelist-manual" : Config.UpdateWhitelistManual = CBool(Value)
                Case "userlist" : Config.UserListLocation = Value
                Case "userlist-update-summary" : Config.UserListUpdateSummary = Value
                Case "version" : SetLatestVersion(Value)
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

                Case "warn-summary" : Config.WarnSummary = Value
                Case "warn-summary-2" : Config.WarnSummary2 = Value
                Case "warn-summary-3" : Config.WarnSummary3 = Value
                Case "warn-summary-4" : Config.WarnSummary4 = Value

                Case Else
                    For Each Item2 As String In Config.WarningSeries
                        If Name.StartsWith(Item2) Then
                            Select Case Name.Substring(Item2.Length)
                                Case "1", "2", "3", "4", "4im"
                                    WarningMessages.Add(Name, Value)
                            End Select

                            Exit For
                        End If
                    Next Item2
            End Select
        End Sub

        Private Sub SetUserConfigOption(ByVal Name As String, ByVal Value As String)
            'User config only
            Select Case Name
                Case "templates" : Config.TemplateMessages = GetList(Value)
                Case "version" : Config.ConfigVersion = New Version(CInt(Value.Substring(0, 1)), _
                    CInt(Value.Substring(2, 1)), CInt(Value.Substring(4)), 0)
            End Select
        End Sub

        Private Sub SetLatestVersion(ByVal VersionString As String)
            Config.LatestVersion = New Version(CInt(VersionString.Substring(0, 1)), _
                CInt(VersionString.Substring(2, 1)), CInt(VersionString.Substring(4)), 0)
        End Sub

        Private Sub SetMinVersion(ByVal VersionString As String)
            Config.MinVersion = New Version(CInt(VersionString.Substring(0, 1)), _
                CInt(VersionString.Substring(2, 1)), CInt(VersionString.Substring(4)), 0)
        End Sub

        Private Function GetList(ByVal Value As String) As List(Of String)
            'Converts a comma-separated list to a List(Of String)
            Dim List As New List(Of String)

            For Each Item As String In Value.Replace("\,", Chr(1)).Split(","c)
                Item = Item.Trim(" "c, CChar(vbTab), CChar(vbCr), CChar(vbLf)).Replace(Chr(1), ",")
                If Not List.Contains(Item) AndAlso Item.Length > 0 Then List.Add(Item)
            Next Item

            Return List
        End Function

        Private Sub SetNamespaces(ByVal Value As String)
            Config.NamespacesChecked = New List(Of String)

            For Each Item As String In Value.Split(","c)
                Item = Item.Trim(","c, " "c, CChar(vbLf)).ToLower
                If Item = "main" OrElse Item = "(main)" Then Item = "article"

                If Item = "all" Then
                    Config.NamespacesChecked.AddRange(ConfigNamespaces)

                ElseIf Item = "-all" Then
                    Config.NamespacesChecked.Clear()

                ElseIf Item = "alltalk" Then
                    For Each Item2 As String In New String() _
                        {"talk", "user talk", "help talk", "portal talk", "template talk", _
                        "mediawiki talk", "image talk", "category talk", "wikipedia talk"}

                        If Not Config.NamespacesChecked.Contains(Item2) Then Config.NamespacesChecked.Add(Item2)
                    Next Item2

                ElseIf Item = "-alltalk" Then
                    For Each Item2 As String In New String() _
                        {"talk", "user talk", "help talk", "portal talk", "template talk", _
                        "mediawiki talk", "image talk", "category talk", "wikipedia talk"}

                        If Config.NamespacesChecked.Contains(Item2) Then Config.NamespacesChecked.Remove(Item2)
                    Next Item2

                ElseIf Item.StartsWith("-") Then
                    Item = Item.Substring(1)
                    If Config.NamespacesChecked.Contains(Item) Then Config.NamespacesChecked.Remove(Item)

                ElseIf New List(Of String)(ConfigNamespaces).Contains(Item) Then
                    Config.NamespacesChecked.Add(Item)
                End If
            Next Item

            If Config.NamespacesChecked.Count = 0 Then Config.NamespacesChecked.AddRange(ConfigNamespaces)
        End Sub

        Private Sub SetReport(ByVal Value As String)
            Config.AutoReport = (Value = "auto")
            Config.PromptForReport = (Value = "prompt")
        End Sub

        Private Sub SetAnonymous(ByVal Value As String)
            Config.ShowRegistered = (Value <> "only")
            Config.ShowAnonymous = (Value <> "no")
        End Sub

        Private Sub SetMinor(ByVal Value As String)
            Config.MinorNotifications = False
            Config.MinorOther = False
            Config.MinorReports = False
            Config.MinorReverts = False
            Config.MinorTags = False
            Config.MinorWarnings = False

            For Each Item As String In Value.Split(","c)
                Item = Item.Trim(","c, " "c, CChar(vbLf)).ToLower

                Select Case Item
                    Case "reverts" : Config.MinorReverts = True
                    Case "other" : Config.MinorOther = True
                    Case "notifications" : Config.MinorNotifications = True
                    Case "reports" : Config.MinorReports = True
                    Case "tags" : Config.MinorTags = True
                    Case "warnings" : Config.MinorWarnings = True
                End Select
            Next Item
        End Sub

        Private Sub SetWatch(ByVal Value As String)
            Config.WatchNotifications = False
            Config.WatchOther = False
            Config.WatchReports = False
            Config.WatchReverts = False
            Config.WatchTags = False
            Config.WatchWarnings = False

            For Each Item As String In Value.Split(","c)
                Item = Item.Trim(","c, " "c, CChar(vbLf)).ToLower

                Select Case Item
                    Case "reverts" : Config.WatchReverts = True
                    Case "other" : Config.WatchOther = True
                    Case "notifications" : Config.WatchNotifications = True
                    Case "reports" : Config.WatchReports = True
                    Case "tags" : Config.WatchTags = True
                    Case "warnings" : Config.WatchWarnings = True
                End Select
            Next Item
        End Sub

        Private Sub SetTags(ByVal Value As String)
            Config.Tags.Clear()

            For Each Item As String In Value.Replace(vbLf, "").Replace(vbCr, "").Replace("\,", Chr(1)).Split _
                (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

                Config.Tags.Add("{{" & Item.Trim(" "c).Replace(Chr(1), ",") & "}}")
            Next Item
        End Sub

        Private Sub SetSpeedyOptions(ByVal Value As String)
            SpeedyCriteria.Clear()

            For Each Item As String In Value.Replace(vbLf, "").Replace(vbCr, "").Replace("\;", Chr(2)) _
                .Replace("\,", Chr(1)).Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)

                Dim Subitems As New List(Of String)(Item.Trim(" "c).Replace(Chr(1), ",") _
                    .Split(New String() {";"}, StringSplitOptions.RemoveEmptyEntries))

                Item = Item.Trim(" "c).Replace(Chr(2), ";")

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

    End Class

    Class WriteConfigRequest : Inherits Request

        'Update user configuration subpage

        Public Closing As Boolean

        Public Sub Start()
            LogProgress("Updating configuration page...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(Config.UserConfigLocation)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Dim ConfigItems As New List(Of String)

            ConfigItems.Add("enable:true")
            ConfigItems.Add("version:" & _
                Version.Major.ToString & "." & Version.Minor.ToString & "." & Version.Build.ToString)
            ConfigItems.Add("")

            If Not Config.ShowRegistered Then
                ConfigItems.Add("anonymous:only")
            ElseIf Not Config.ShowAnonymous Then
                ConfigItems.Add("anonymous:no")
            Else
                ConfigItems.Add("anonymous:yes")
            End If

            ConfigItems.Add("auto-advance:" & CStr(Config.AutoAdvance).ToLower)
            ConfigItems.Add("auto-whitelist:" & CStr(Config.AutoWhitelist).ToLower)
            ConfigItems.Add("confirm-multiple:" & CStr(Config.ConfirmMultiple).ToLower)
            ConfigItems.Add("confirm-same:" & CStr(Config.ConfirmSame).ToLower)
            ConfigItems.Add("confirm-self-revert:" & CStr(Config.ConfirmSelfRevert).ToLower)
            ConfigItems.Add("default-summary:" & CStr(Config.DefaultSummary))
            ConfigItems.Add("diff-font-size:" & Config.DiffFontSize)
            ConfigItems.Add("extend-reports:" & CStr(Config.ExtendReports).ToLower)
            ConfigItems.Add("irc-port:" & CStr(Config.IrcPort))

            Dim MinorItems As New List(Of String)

            If Config.MinorReverts Then MinorItems.Add("reverts")
            If Config.MinorWarnings Then MinorItems.Add("warnings")
            If Config.MinorTags Then MinorItems.Add("tags")
            If Config.MinorReports Then MinorItems.Add("reports")
            If Config.MinorNotifications Then MinorItems.Add("notifications")
            If Config.MinorOther Then MinorItems.Add("other")
            If MinorItems.Count = 0 Then MinorItems.Add("none")
            ConfigItems.Add("minor:" & Strings.Join(MinorItems.ToArray, ","))

            If Config.NamespacesChecked.Count = 18 Then ConfigItems.Add("namespaces:all") _
                Else ConfigItems.Add("namespaces:" & Strings.Join(Config.NamespacesChecked.ToArray, ","))

            ConfigItems.Add("new-pages:" & CStr(Config.ShowNewPages).ToLower)
            ConfigItems.Add("open-in-browser:" & CStr(Config.OpenInBrowser).ToLower)
            ConfigItems.Add("preload:" & CStr(Config.Preloads))
            ConfigItems.Add("queue-max-age:" & CStr(Config.QueueMaxAge))

            If Config.AutoReport Then
                ConfigItems.Add("report:auto")
            ElseIf Not Config.PromptForReport Then
                ConfigItems.Add("report:prompt")
            Else
                ConfigItems.Add("report:none")
            End If

            ConfigItems.Add("revert-summaries:" & vbCrLf & "    " & _
                Strings.Join(Config.CustomRevertSummaries.ToArray, "," & vbCrLf & "    "))
            ConfigItems.Add("rollback:" & CStr(Config.UseRollback).ToLower)
            ConfigItems.Add("show-new-edits:" & CStr(Config.ShowNewEdits).ToLower)
            ConfigItems.Add("show-queue:" & CStr(Config.ShowQueue).ToLower)
            ConfigItems.Add("show-tool-tips:" & CStr(Config.ShowToolTips).ToLower)

            Dim Templates As New List(Of String)

            For Each Item As String In Config.TemplateMessages
                If Not Config.TemplateMessagesGlobal.Contains(Item) Then Templates.Add(Item)
            Next Item

            ConfigItems.Add("templates:" & vbCrLf & "    " & Strings.Join(Config.TemplateMessages.ToArray, "," & vbCrLf & "    ")) 'Addshore

            ConfigItems.Add("tray-icon:" & CStr(Config.TrayIcon).ToLower)
            ConfigItems.Add("undo-summary:" & Config.UndoSummary)
            ConfigItems.Add("update-whitelist:" & CStr(Config.UpdateWhitelist).ToLower)

            Dim WatchItems As New List(Of String)

            If Config.WatchReverts Then WatchItems.Add("reverts")
            If Config.WatchWarnings Then WatchItems.Add("warnings")
            If Config.WatchTags Then WatchItems.Add("tags")
            If Config.WatchReports Then WatchItems.Add("reports")
            If Config.WatchNotifications Then WatchItems.Add("notifications")
            If Config.WatchOther Then WatchItems.Add("other")
            If WatchItems.Count = 0 Then WatchItems.Add("none")
            ConfigItems.Add("watchlist:" & Strings.Join(WatchItems.ToArray, ","))

            Data.Text = Strings.Join(ConfigItems.ToArray, vbCrLf)
            Data.Minor = True
            Data.Summary = Config.ConfigSummary
            Data = PostEdit(Data)
            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Closing Then ClosingForm.Close()
            If MainForm IsNot Nothing Then MainForm.Configure()
            Complete()
        End Sub

        Private Sub Failed()
            Log("Failed to update user configuration subpage")
            Fail()
        End Sub

    End Class

    Class GlobalConfigRequest : Inherits Request

        'Process global configuration page

        Public Function Process() As Boolean
            Dim Result As String = GetUrl(Config.GlobalConfigLocation)

            If Result Is Nothing Then
                Fail()
                Return False
            End If

            Dim i As Integer = 1, ConfigItems As New List(Of String)(Result.Split(CChar(vbLf)))

            While i < ConfigItems.Count
                If ConfigItems(i).StartsWith(" ") Then
                    ConfigItems(i - 1) &= ConfigItems(i)
                    ConfigItems.RemoveAt(i)
                Else
                    i += 1
                End If
            End While

            For Each Item As String In ConfigItems

                If (Not Item.StartsWith("#")) AndAlso Item.Contains(":") Then
                    Dim OptionName As String = Item.Substring(0, Item.IndexOf(":")).ToLower
                    Dim OptionValue As String = Item.Substring(Item.IndexOf(":") + 1).Trim(CChar(vbLf)).Replace("\n", vbLf)

                    Try
                        'Global config
                        Select Case OptionName
                            Case "enable-all" : Config.EnabledForAll = CBool(OptionValue)
                            Case "config" : Config.ProjectConfigLocation = OptionValue
                            Case "documentation" : Config.DocsLocation = OptionValue
                            Case "feedback" : Config.FeedbackLocation = OptionValue
                            Case "irc-server" : Config.IrcServer = OptionValue
                            Case "sensitive-addresses" : SetSensitiveAddresses(OptionValue)
                            Case "user-agent" : Config.UserAgent = OptionValue.Replace("$1", Config.Version.ToString)
                            Case "user-config" : Config.UserConfigLocation = OptionValue
                        End Select

                    Catch ex As Exception
                        'Ignore malformed config entries
                    End Try
                End If
            Next Item

            Complete()
            Return True
        End Function

        Private Sub SetSensitiveAddresses(ByVal Value As String)
            Config.SensitiveAddresses.Clear()

            For Each Item As String In Value.Replace(vbLf, "").Replace(vbCr, "").Replace("\,", Chr(1)).Split _
                (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

                If Item.Contains(";") Then Config.SensitiveAddresses.Add(Item.Trim(" "c).Replace(Chr(1), ","))
            Next Item
        End Sub

    End Class

End Namespace
