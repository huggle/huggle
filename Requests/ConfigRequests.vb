Imports System.Threading

Module ConfigRequests

    Class GetConfigRequest

        'Read per-user and project config pages

        Private _ProjectConfig As Boolean

        Public Function GetConfig(ByVal ProjectConfig As Boolean) As Boolean
            _ProjectConfig = ProjectConfig
            Return Process()
        End Function

        Public Sub StartInThread(ByVal ProjectConfig As Boolean)
            _ProjectConfig = ProjectConfig

            Dim RequestThread As New Thread(AddressOf GetConfigInThread)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub GetConfigInThread()
            Process()
            If Config.Enabled Then Callback(AddressOf GetUserConfigDone) Else Callback(AddressOf GetUserConfigFailed)
        End Sub

        Public Function Process() As Boolean
            Dim Result As String

            If _ProjectConfig Then Result = GetText(GetPage(Config.ProjectConfigLocation)) _
                Else Result = GetText(GetPage(Config.UserConfigLocation))

            If Result Is Nothing Then Return False

            If Not _ProjectConfig Then Config.Enabled = (Not Config.RequireConfig)

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
                        'Global and user config
                        Select Case OptionName
                            Case "enable" : Config.Enabled = CBool(OptionValue)
                            Case "admin" : Config.UseAdminFunctions = CBool(OptionValue)
                            Case "anonymous" : SetAnonymous(OptionValue)
                            Case "auto-advance" : Config.AutoAdvance = CBool(OptionValue)
                            Case "auto-whitelist" : Config.AutoWhitelist = CBool(OptionValue)
                            Case "blocktime" : Config.BlockTime = OptionValue
                            Case "blocktime-anon" : Config.BlockTimeAnon = OptionValue
                            Case "block-message" : Config.BlockMessage = OptionValue
                            Case "block-message-default" : Config.BlockMessageDefault = CBool(OptionValue)
                            Case "block-message-indef" : Config.BlockMessageIndef = OptionValue
                            Case "block-prompt" : Config.PromptForBlock = CBool(OptionValue)
                            Case "block-reason" : Config.BlockReason = OptionValue
                            Case "block-summary" : Config.BlockSummary = "Notification: Blocked"
                            Case "confirm-ignored" : Config.ConfirmIgnored = CBool(OptionValue)
                            Case "confirm-multiple" : Config.ConfirmMultiple = CBool(OptionValue)
                            Case "confirm-same" : Config.ConfirmSame = CBool(OptionValue)
                            Case "confirm-self-revert" : Config.ConfirmSelfRevert = CBool(OptionValue)
                            Case "default-summary" : Config.DefaultSummary = OptionValue
                            Case "diff-font-size" : Config.DiffFontSize = OptionValue
                            Case "extend-reports" : Config.ExtendReports = CBool(OptionValue)
                            Case "irc-port" : Config.IrcPort = CInt(OptionValue)
                            Case "minor" : SetMinor(OptionValue)
                            Case "namespaces" : SetNamespaces(OptionValue)
                            Case "new-pages" : Config.ShowNewPages = CBool(OptionValue)
                            Case "open-in-browser" : Config.OpenInBrowser = CBool(OptionValue)
                            Case "patrol-speedy" : Config.PatrolSpeedy = CBool(OptionValue)
                            Case "preload" : Config.Preloads = CInt(OptionValue)
                            Case "prod" : Config.Prod = CBool(OptionValue)
                            Case "prod-message" : Config.ProdMessage = OptionValue
                            Case "prod-message-summary" : Config.ProdMessageSummary = OptionValue
                            Case "prod-message-title" : Config.ProdMessageTitle = OptionValue
                            Case "prod-summary" : Config.ProdSummary = OptionValue
                            Case "protection-reason" : Config.ProtectionReason = OptionValue
                            Case "protection-requests" : Config.ProtectionRequests = CBool(OptionValue)
                            Case "report" : SetReport(OptionValue)
                            Case "report-extend-summary" : Config.ReportExtendSummary = OptionValue
                            Case "report-summary" : Config.ReportSummary = OptionValue
                            Case "revert-summaries" : SetRevertSummaries(OptionValue)
                            Case "rollback" : Config.UseRollback = CBool(OptionValue)
                            Case "show-log" : Config.ShowLog = CBool(OptionValue)
                            Case "show-new-edits" : Config.ShowNewEdits = CBool(OptionValue)
                            Case "show-queue" : Config.ShowQueue = CBool(OptionValue)
                            Case "show-tool-tips" : Config.ShowToolTips = CBool(OptionValue)
                            Case "speedy" : Config.Speedy = CBool(OptionValue)
                            Case "speedy-message-summary" : Config.SpeedyMessageSummary = OptionValue
                            Case "speedy-message-title" : Config.SpeedyMessageTitle = OptionValue
                            Case "speedy-summary" : Config.SpeedySummary = OptionValue
                            Case "tags" : SetTags(OptionValue)
                            Case "tray-icon" : Config.TrayIcon = CBool(OptionValue)
                            Case "undo-summary" : Config.UndoSummary = OptionValue
                            Case "update-whitelist" : Config.UpdateWhitelist = CBool(OptionValue)
                            Case "watchlist" : SetWatch(OptionValue)
                            Case "welcome" : Config.Welcome = OptionValue
                            Case "welcome-anon" : Config.WelcomeAnon = OptionValue
                        End Select

                        'User config only
                        If Not _ProjectConfig Then
                            Select Case OptionName
                                Case "templates" : Config.TemplateMessages = SetTemplateMessages(OptionValue)
                                Case "version" : CheckConfigVersion(OptionValue)
                            End Select
                        End If

                        'Global config only
                        If _ProjectConfig Then
                            Select Case OptionName
                                Case "afd" : Config.AfdLocation = OptionValue
                                Case "aiv" : Config.AIVLocation = OptionValue
                                Case "aivbot" : Config.AIVBotLocation = OptionValue
                                Case "aiv-reports" : Config.AIV = CBool(OptionValue)
                                Case "aiv-single-note" : Config.AivSingleNote = OptionValue
                                Case "approval" : Config.Approval = CBool(OptionValue)
                                Case "block" : Config.Block = CBool(OptionValue)
                                Case "cfd" : Config.CfdLocation = OptionValue
                                Case "config-summary" : Config.ConfigSummary = OptionValue
                                Case "delete" : Config.Delete = CBool(OptionValue)
                                Case "enable-all" : Config.EnabledForAll = CBool(OptionValue)
                                Case "feedback" : Config.FeedbackLocation = OptionValue
                                Case "go" : SetGo(OptionValue)
                                Case "ifd" : Config.IfdLocation = OptionValue
                                Case "ignore" : SetIgnore(OptionValue)
                                Case "manual-revert-summary" : Config.ManualRevertSummary = OptionValue
                                Case "mfd" : Config.MfdLocation = OptionValue
                                Case "min-version" : VersionOK = CheckMinVersion(OptionValue)
                                Case "patrol" : Config.Patrol = CBool(OptionValue)
                                Case "protect" : Config.Protect = CBool(OptionValue)
                                Case "protection-request-page" : Config.ProtectionRequestPage = OptionValue
                                Case "protection-request-reason" : Config.ProtectionRequestReason = OptionValue
                                Case "protection-request-summary" : Config.ProtectionRequestSummary = OptionValue
                                Case "rc-block-size" : Config.RcBlockSize = CInt(OptionValue)
                                Case "report-link-diffs" : Config.ReportLinkDiffs = CBool(OptionValue)
                                Case "require-admin" : Config.RequireAdmin = CBool(OptionValue)
                                Case "require-config" : Config.RequireConfig = CBool(OptionValue)
                                Case "require-edits" : Config.RequireEdits = CInt(OptionValue)
                                Case "require-rollback" : Config.RequireRollback = CBool(OptionValue)
                                Case "require-time" : Config.RequireTime = CInt(OptionValue)
                                Case "rollback-summary" : Config.RollbackSummary = OptionValue
                                Case "rollback-summary-unknown" : Config.RollbackSummaryUnknown = OptionValue
                                Case "rfd" : Config.RfdLocation = OptionValue
                                Case "speedy-delete-summary" : Config.SpeedyDeleteSummary = OptionValue
                                Case "speedy-options" : SetSpeedyOptions(OptionValue)
                                Case "summary" : Config.Summary = OptionValue
                                Case "templates" : Config.TemplateMessagesGlobal = SetTemplateMessages(OptionValue)
                                Case "tfd" : Config.TfdLocation = OptionValue
                                Case "uaa" : Config.UAALocation = OptionValue
                                Case "uaabot" : Config.UAABotLocation = OptionValue
                                Case "userlist" : Config.UserListLocation = OptionValue
                                Case "userlist-update-summary" : Config.UserListUpdateSummary = OptionValue
                                Case "version" : If VersionOK Then CheckVersion(OptionValue)
                                Case "warning-im-level" : Config.WarningImLevel = CBool(OptionValue)
                                Case "warning-mode" : Config.WarningMode = OptionValue
                                Case "warning-month-headings" : Config.MonthHeadings = CBool(OptionValue)
                                Case "warning-series" : SetWarningSeries(OptionValue)
                                Case "welcome-summary" : Config.WelcomeSummary = OptionValue
                                Case "whitelist" : Config.WhitelistLocation = OptionValue
                                Case "whitelist-edit-count" : Config.WhitelistEditCount = CInt(OptionValue)
                                Case "whitelist-enabled" : Config.WhitelistEnabled = CBool(OptionValue)
                                Case "whitelist-update-summary" : Config.WhitelistUpdateSummary = OptionValue
                                Case "xfd" : Config.Xfd = CBool(OptionValue)
                                Case "xfd-discussion-summary" : Config.XfdDiscussionSummary = OptionValue
                                Case "xfd-log-summary" : Config.XfdLogSummary = OptionValue
                                Case "xfd-message" : Config.XfdMessage = OptionValue
                                Case "xfd-message-summary" : Config.XfdMessageSummary = OptionValue
                                Case "xfd-message-title" : Config.XfdMessageTitle = OptionValue
                                Case "xfd-summary" : Config.XfdSummary = OptionValue

                                Case "warn-summary" : Config.WarnSummary = OptionValue
                                Case "warn-summary-2" : Config.WarnSummary2 = OptionValue
                                Case "warn-summary-3" : Config.WarnSummary3 = OptionValue
                                Case "warn-summary-4" : Config.WarnSummary4 = OptionValue

                                Case Else
                                    For Each Item2 As String In Config.WarningSeries
                                        If OptionName.StartsWith(Item2) Then
                                            Select Case OptionName.Substring(Item2.Length)
                                                Case "1", "2", "3", "4", "4im"
                                                    WarningMessages.Add(OptionName, OptionValue)
                                            End Select

                                            Exit For
                                        End If
                                    Next Item2
                            End Select
                        End If

                    Catch ex As Exception
                        'Ignore malformed config entries
                    End Try
                End If
            Next Item

            Return True
        End Function

        Private Sub GetUserConfigDone(ByVal O As Object)
            If Config.UseAdminFunctions AndAlso Administrator Then
                Main.PageTagSpeedy.ShortcutKeyDisplayString = ""
                Main.UserReport.ShortcutKeyDisplayString = ""
                Main.PageDeleteB.Image = My.Resources.page_delete
                Main.UserBlock.Visible = True
                Main.PageDelete.Visible = False
            Else
                Main.PageTagSpeedy.ShortcutKeyDisplayString = "S"
                Main.UserReport.ShortcutKeyDisplayString = "B"
                Main.PageDeleteB.Image = My.Resources.page_speedy
                Main.UserBlock.Visible = False
                Main.PageDelete.Visible = False
            End If

            Main.TrayIcon.Visible = Config.TrayIcon
            Log("Loaded configuration page.")
        End Sub

        Private Sub GetUserConfigFailed(ByVal O As Object)
            Log("Failed to load configuration page.")
        End Sub

        Private Sub CheckConfigVersion(ByVal VersionString As String)
            Config.ConfigVersion = New Version(CInt(VersionString.Substring(0, 1)), _
                CInt(VersionString.Substring(2, 1)), CInt(VersionString.Substring(4)))
        End Sub

        Private Sub CheckVersion(ByVal VersionString As String)
            Dim NewVersion As New Version(CInt(VersionString.Substring(0, 1)), _
                CInt(VersionString.Substring(2, 1)), CInt(VersionString.Substring(4)))

            If NewVersion > Version Then
                Dim NewNewVersionForm As New VersionForm

                NewNewVersionForm.VersionMessage.Text = "You are currently using version " & _
                    Version.ToString & " of huggle. The latest available version is " & _
                    NewVersion.ToString & "." & vbCrLf & vbCrLf & "See the documentation page for details of how to " & _
                    "obtain the most recent version. This version will continue to function."
                NewNewVersionForm.ShowDialog()
            End If
        End Sub

        Private Function CheckMinVersion(ByVal VersionString As String) As Boolean
            Dim MinVersion As New Version(CInt(VersionString.Substring(0, 1)), _
                        CInt(VersionString.Substring(2, 1)), CInt(VersionString.Substring(4)))

            Config.MinVersion = MinVersion.ToString

            Return MinVersion <= Version
        End Function

        Private Sub SetIgnore(ByVal Value As String)
            Value = Value.Replace("\,", Chr(1))

            For Each Item As String In Value.Split(","c)
                Item = Item.Trim(","c, " "c, CChar(vbLf)).Replace(Chr(1), ",")
                If Not Config.IgnoredPages.Contains(Item) Then Config.IgnoredPages.Add(Item)
            Next Item
        End Sub

        Private Sub SetWarningSeries(ByVal Value As String)
            Value = Value.Replace("\,", Chr(1))

            For Each Item As String In Value.Split(","c)
                Item = Item.Trim(","c, " "c, CChar(vbLf)).Replace(Chr(1), ",")
                If Not Config.WarningSeries.Contains(Item) Then Config.WarningSeries.Add(Item)
            Next Item
        End Sub

        Private Sub SetGo(ByVal Value As String)
            Value = Value.Replace("\,", Chr(1))

            For Each Item As String In Value.Split(","c)
                Item = Item.Trim(","c, " "c, CChar(vbLf)).Replace(Chr(1), ",")
                If Not Config.GoToPages.Contains(Item) Then Config.GoToPages.Add(Item)
            Next Item
        End Sub

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

        Private Sub SetRevertSummaries(ByVal Value As String)
            Config.CustomRevertSummaries.Clear()

            For Each Item As String In Value.Replace(vbLf, "").Replace(vbCr, "").Replace("\,", Chr(1)).Split _
                (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

                Config.CustomRevertSummaries.Add(Item.Trim(" "c).Replace(Chr(1), ","))
            Next Item
        End Sub

        Private Function SetTemplateMessages(ByVal Value As String) As List(Of String)
            Dim Result As New List(Of String)

            For Each Item As String In Value.Replace(vbLf, "").Replace(vbCr, "").Replace("\,", Chr(1)).Split _
                (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

                If Not (Item.Trim(" "c) = "") Then Result.Add(Item.Trim(" "c).Replace(Chr(1), ","))
            Next Item

            Return Result
        End Function

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

                For Each Subitem As String In Subitems
                    Item = Item.Trim(" "c).Replace(Chr(2), ";")
                Next Subitem

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
            Log("Updating configuration page...", GetPage(Config.UserConfigLocation), True)

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEdit(GetPage(Config.UserConfigLocation))

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Dim ConfigItems As New List(Of String)

            ConfigItems.Add("enable:true")
            ConfigItems.Add("version:" & Version.ToString)
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

            If Templates.Count > 0 Then _
                ConfigItems.Add("templates:" & vbCrLf & "    " & Strings.Join(Templates.ToArray, "," & vbCrLf & "    "))
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

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            Delog(GetPage(Config.UserConfigLocation))
            If Closing Then ClosingForm.Close()
            If Main IsNot Nothing Then Main.Configure()
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(GetPage(Config.UserConfigLocation))
            Log("Failed to update configuration")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

    Class GlobalConfigRequest

        'Process global configuration page

        Public Function GetConfig() As Boolean
            Dim Result As String = GetText(Config.GlobalConfigLocation)

            If Result Is Nothing Then Return False

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
                            Case "user-agent" : Config.UserAgent = OptionValue.Replace("$1", Config.Version.ToString)
                            Case "user-config" : Config.UserConfigLocation = OptionValue
                            Case "projects" : SetProjects(OptionValue)
                            Case "sensitive-addresses" : SetSensitiveAddresses(OptionValue)
                        End Select

                    Catch ex As Exception
                        'Ignore malformed config entries
                    End Try
                End If
            Next Item

            Return True
        End Function

        Private Sub SetProjects(ByVal Value As String)
            Config.Projects.Clear()

            For Each Item As String In Value.Replace(vbLf, "").Replace(vbCr, "").Replace("\,", Chr(1)).Split _
                (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

                Config.Projects.Add(Item.Trim(" "c).Replace(Chr(1), ","))
            Next Item

#If DEBUG Then
            Config.Projects.Add("localhost;localhost")
#End If
        End Sub

        Private Sub SetSensitiveAddresses(ByVal Value As String)
            Config.SensitiveAddresses.Clear()

            For Each Item As String In Value.Replace(vbLf, "").Replace(vbCr, "").Replace("\,", Chr(1)).Split _
                (New String() {","}, StringSplitOptions.RemoveEmptyEntries)

                If Item.Contains(";") Then Config.SensitiveAddresses.Add(Item.Trim(" "c).Replace(Chr(1), ","))
            Next Item
        End Sub

    End Class

End Module
