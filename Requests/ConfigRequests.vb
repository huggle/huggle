Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class GlobalConfigRequest : Inherits Request

        'Process global configuration page

        Protected Overrides Sub Process()
            Dim Result As ApiResult = DoApiRequest("action=query&prop=revisions&rvlimit=1&rvprop=content&titles=" & _
                Config.GlobalConfigLocation, Project:="meta")

            If Result.Error Then
                Fail(Msg("loadglobalconfig-fail"), Result.ErrorMessage)
                Exit Sub
            End If

            For Each Item As KeyValuePair(Of String, String) In ProcessConfigFile(Result.Text)

                Try
                    SetGlobalConfigOption(Item.Key, Item.Value)

                Catch ex As Exception
                    'Ignore malformed config entries
                End Try
            Next Item

#If DEBUG Then
            'If the app is in debug mode add a localhost wiki to the project list
            If Not Config.Projects.ContainsKey("localhost") Then Config.Projects.Add("localhost", "http://localhost/")
#End If

            Complete()
        End Sub

    End Class

    Class ProjectConfigRequest : Inherits Request

        'Read project configuration page

        Protected Overrides Sub Process()

            Dim Result As ApiResult = DoApiRequest("action=query&prop=revisions&rvlimit=1&rvprop=content&titles=" & _
                UrlEncode(Config.ProjectConfigLocation))

            If Result.Error Then
                Fail(Msg("loadprojectconfig-fail"), Result.ErrorMessage)
                Exit Sub
            End If

            Dim ConfigFile As String = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>"))

            If ConfigFile Is Nothing Then
                Fail(Msg("login-error-noconfig"))
                Exit Sub
            End If

            For Each Item As KeyValuePair(Of String, String) In ProcessConfigFile(ConfigFile)
                Try
                    SetSharedConfigOption(Item.Key, Item.Value)
                    SetProjectConfigOption(Item.Key, Item.Value)

                Catch ex As Exception
                    'Ignore malformed config entries
                End Try
            Next Item

            Config.AIV = Not String.IsNullOrEmpty(Config.AIVLocation)
            Config.UAA = Not String.IsNullOrEmpty(Config.UAALocation)
            Config.TRR = Not String.IsNullOrEmpty(Config.TRRLocation)

            Config.IrcMode = Not String.IsNullOrEmpty(Config.IrcChannel)

            Complete()
        End Sub

    End Class

    Class UserConfigRequest : Inherits Request

        'Read user configuration page

        Protected Overrides Sub Process()
            Dim Result As ApiResult = DoApiRequest("action=query&prop=revisions&rvlimit=1&rvprop=content&titles=" & _
               UrlEncode(GetPage(Config.UserConfigLocation).Name))

            If Result.Error Then
                Fail(Msg("loaduserconfig-fail"), Result.ErrorMessage)
                Exit Sub
            End If

            Dim ConfigFile As String = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>"))

            If ConfigFile IsNot Nothing Then
                For Each Item As KeyValuePair(Of String, String) In ProcessConfigFile(ConfigFile)
                    Try
                        SetSharedConfigOption(Item.Key, Item.Value)
                        SetUserConfigOption(Item.Key, Item.Value)

                    Catch ex As Exception
                        'Ignore malformed config entries
                    End Try
                Next Item
            End If

            Complete()
        End Sub

    End Class

    Class SaveUserConfigRequest : Inherits Request

        'Update user configuration subpage

        Protected Overrides Sub Process()
            LogProgress(Msg("saveuserconfig-progress"))

            Dim Items As New List(Of String)

            Items.Add("enable:true")
            Items.Add("version:" & Config.Version.Major.ToString & "." & Config.Version.Minor.ToString & "." & _
                Config.Version.Build.ToString)
            Items.Add("")

            Items.Add("auto-advance:" & CStr(Config.AutoAdvance).ToLower)
            Items.Add("auto-whitelist:" & CStr(Config.AutoWhitelist).ToLower)
            Items.Add("confirm-multiple:" & CStr(Config.ConfirmMultiple).ToLower)
            Items.Add("confirm-range:" & CStr(Config.ConfirmRange).ToLower)
            Items.Add("confirm-same:" & CStr(Config.ConfirmSame).ToLower)
            Items.Add("confirm-self-revert:" & CStr(Config.ConfirmSelfRevert).ToLower)
            Items.Add("confirm-warned:" & CStr(Config.ConfirmWarned).ToLower)
            Items.Add("default-summary:" & CStr(Config.DefaultSummary))
            Items.Add("diff-font-size:" & Config.DiffFontSize)
            Items.Add("extend-reports:" & CStr(Config.ExtendReports).ToLower)
            Items.Add("irc-port:" & CStr(Config.IrcPort))

            Dim MinorItems As New List(Of String)

            If Config.MinorReverts Then MinorItems.Add("reverts")
            If Config.MinorWarnings Then MinorItems.Add("warnings")
            If Config.MinorTags Then MinorItems.Add("tags")
            If Config.MinorReports Then MinorItems.Add("reports")
            If Config.MinorNotifications Then MinorItems.Add("notifications")
            If Config.MinorOther Then MinorItems.Add("other")
            If MinorItems.Count = 0 Then MinorItems.Add("none")

            Items.Add("minor:" & String.Join(",", MinorItems.ToArray))
            Items.Add("open-in-browser:" & CStr(Config.OpenInBrowser).ToLower)
            Items.Add("preload:" & CStr(Config.Preloads))

            If Config.AutoReport Then
                Items.Add("report:auto")
            ElseIf Not Config.PromptForReport Then
                Items.Add("report:prompt")
            Else
                Items.Add("report:none")
            End If

            If Config.CustomRevertSummaries.Count > 0 Then Items.Add("revert-summaries:" & LF & "    " & _
                String.Join("," & LF & "    ", Config.CustomRevertSummaries.ToArray))
            Items.Add("rollback:" & CStr(Config.UseRollback).ToLower)
            Items.Add("show-log:" & CStr(Config.ShowLog).ToLower)
            Items.Add("show-new-edits:" & CStr(Config.ShowNewEdits).ToLower)
            Items.Add("show-queue:" & CStr(Config.ShowQueue).ToLower)
            Items.Add("show-tool-tips:" & CStr(Config.ShowToolTips).ToLower)

            Dim Templates As New List(Of String)

            For Each Item As String In Config.TemplateMessages
                If Not Config.TemplateMessagesGlobal.Contains(Item) Then Templates.Add(Item)
            Next Item

            Items.Add("templates:" & LF & "    " & String.Join("," & LF & "    ", Config.TemplateMessages.ToArray))
            Items.Add("tray-icon:" & CStr(Config.TrayIcon).ToLower)
            Items.Add("undo-summary:" & Config.UndoSummary)
            Items.Add("update-whitelist:" & CStr(Config.UpdateWhitelist).ToLower)
            Items.Add("username-listed:" & CStr(Config.UsernameListed).ToLower)

            Dim WatchItems As New List(Of String)

            If Config.WatchReverts Then WatchItems.Add("reverts")
            If Config.WatchWarnings Then WatchItems.Add("warnings")
            If Config.WatchTags Then WatchItems.Add("tags")
            If Config.WatchReports Then WatchItems.Add("reports")
            If Config.WatchNotifications Then WatchItems.Add("notifications")
            If Config.WatchOther Then WatchItems.Add("other")
            If WatchItems.Count = 0 Then WatchItems.Add("none")

            Items.Add("watchlist:" & String.Join(",", WatchItems.ToArray))

            Dim Result As ApiResult = PostEdit(Config.UserConfigLocation, String.Join(LF, Items.ToArray), _
                Config.ConfigSummary, Minor:=True)

            If Result.Error Then Fail(Msg("saveuserconfig-fail"), Result.ErrorMessage) Else Complete()
        End Sub

    End Class

End Namespace
