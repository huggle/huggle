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

            Dim i As Integer, ConfigItems As New List(Of String)(Result.Split(New String() {LF}, _
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
                    .Trim(LF).Replace("\n", LF).Trim(" "c)

                Try
                    SetSharedConfigOption(OptionName, OptionValue)
                    SetProjectConfigOption(OptionName, OptionValue)

                Catch ex As Exception
                    'Ignore malformed config entries
                End Try
            Next Item

            Config.AIV = (Config.AIVLocation IsNot Nothing AndAlso Config.AIVLocation.Length > 0)
            Config.UAA = (Config.UAALocation IsNot Nothing AndAlso Config.UAALocation.Length > 0)
            Config.TRR = (Config.TRRLocation IsNot Nothing AndAlso Config.TRRLocation.Length > 0)

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

            Dim i As Integer, ConfigItems As New List(Of String)(Result.Split(New String() {LF}, _
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
                    .Trim(LF).Replace("\n", LF).Trim(" "c)

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

            Dim Items As New List(Of String)

            Items.Add("enable:true")
            Items.Add("version:" & Config.Version.Major.ToString & "." & Config.Version.Minor.ToString & "." & _
                Config.Version.Build.ToString)
            Items.Add("")

            Items.Add("auto-advance:" & CStr(Config.AutoAdvance).ToLower)
            Items.Add("auto-whitelist:" & CStr(Config.AutoWhitelist).ToLower)
            Items.Add("confirm-multiple:" & CStr(Config.ConfirmMultiple).ToLower)
            Items.Add("confirm-same:" & CStr(Config.ConfirmSame).ToLower)
            Items.Add("confirm-self-revert:" & CStr(Config.ConfirmSelfRevert).ToLower)
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

            Items.Add("revert-summaries:" & LF & "    " & _
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

            Dim WatchItems As New List(Of String)

            If Config.WatchReverts Then WatchItems.Add("reverts")
            If Config.WatchWarnings Then WatchItems.Add("warnings")
            If Config.WatchTags Then WatchItems.Add("tags")
            If Config.WatchReports Then WatchItems.Add("reports")
            If Config.WatchNotifications Then WatchItems.Add("notifications")
            If Config.WatchOther Then WatchItems.Add("other")
            If WatchItems.Count = 0 Then WatchItems.Add("none")

            Items.Add("watchlist:" & String.Join(",", WatchItems.ToArray))

            Data.Text = String.Join(LF, Items.ToArray)
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
            Dim Result As String = GetUrl(GlobalConfigLocation)

            If Result Is Nothing Then
                Fail()
                Return False
            End If

            Dim i As Integer = 1, ConfigItems As New List(Of String)(Result.Split(LF))

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
                    Dim OptionValue As String = Item.Substring(Item.IndexOf(":") + 1).Trim(LF).Replace("\n", LF)

                    Try
                        SetGlobalConfigOption(OptionName, OptionValue)

                    Catch ex As Exception
                        'Ignore malformed config entries
                    End Try
                End If
            Next Item

            Complete()
            Return True
        End Function

    End Class

End Namespace
