Class ConfigForm

    Private ShortcutKeysClone As Dictionary(Of String, Shortcut)
    Private Initializing As Boolean

    Private Sub ConfigForm_Load() Handles MyBase.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("config-title")

        Initializing = True

        'Localization
        Localize(Me, "config")
        ShortcutList.Columns(0).Text = Msg("config-shortcutaction")
        ShortcutList.Columns(1).Text = Msg("config-shortcut")
        Templates.Columns(0).Text = Msg("config-templatetext")
        Templates.Columns(1).Text = Msg("config-template")
        ViewLocalConfig.Text = Msg("config-viewlocalconfig")
        OK.Text = Msg("ok")
        Cancel.Text = Msg("cancel")

        'Set to current config values
        RememberMe.Checked = Config.RememberMe
        RememberPassword.Checked = Config.RememberPassword
        AutoWhitelist.Checked = Config.AutoWhitelist
        TrayIcon.Checked = Config.TrayIcon
        ShowQueue.Checked = Config.ShowQueue
        RightAlignQueue.Checked = Config.RightAlignQueue
        ShowLog.Checked = Config.ShowLog
        ShowToolTips.Checked = Config.ShowToolTips
        OpenInBrowser.Checked = Config.OpenInBrowser
        ShowNewEdits.Checked = Config.ShowNewEdits
        Preloading.Checked = (Config.Preloads > 0)
        Preloads.Enabled = Preloading.Checked
        Preloads.Value = Math.Max(Math.Min(Config.Preloads, Preloads.Maximum), Preloads.Minimum)
        IrcMode.Checked = Config.IrcMode
        IrcPort.Text = CStr(Config.IrcPort)
        DiffFontSize.Value = CInt(Config.DiffFontSize)
        LogFile.Text = Config.LogFile

        DefaultSummary.Text = Config.DefaultSummary
        UndoSummary.Text = Config.UndoSummary

        ConfirmMultiple.Checked = Config.ConfirmMultiple
        ConfirmSame.Checked = Config.ConfirmSame
        ConfirmSelfRevert.Checked = Config.ConfirmSelfRevert
        ConfirmWarned.Checked = Config.ConfirmWarned
        ConfirmRange.Checked = Config.ConfirmRange
        AutoAdvance.Checked = Config.AutoAdvance
        UseRollback.Checked = Config.UseRollback

        For Each Item As KeyValuePair(Of String, Boolean) In Config.Minor
            Minor.Items.Add(Msg("edittype-" & Item.Key), Item.Value)
        Next Item

        For Each Item As KeyValuePair(Of String, Boolean) In Config.Watch
            Watchlist.Items.Add(Msg("edittype-" & Item.Key), Item.Value)
        Next Item

        For Each Item As String In Config.CustomRevertSummaries
            RevertSummaries.Items.Add(Item)
        Next Item

        ClearSummaries.Enabled = (Config.RevertSummaries.Count > 0)

        ReportLinkExamples.Checked = Config.ReportLinkDiffs
        ExtendReports.Enabled = ReportLinkExamples.Checked
        ExtendReports.Checked = Config.ExtendReports
        ReportNone.Checked = (Not Config.AutoReport AndAlso Not Config.PromptForReport)
        ReportPrompt.Checked = Config.PromptForReport
        ReportAuto.Checked = Config.AutoReport

        For Each Item As String In Config.TemplateMessages
            Item = Item.Replace("\;", Convert.ToChar(1))
            If Item <> "" Then
                Dim NewListItem As New ListViewItem(Item.Substring(0, Item.IndexOf(";")).Replace(Convert.ToChar(1), ";"))
                NewListItem.SubItems.Add(Item.Substring(Item.IndexOf(";") + 1).Replace(Convert.ToChar(1), ";"))
                Templates.Items.Add(NewListItem)
            End If
        Next Item

        UseAdminFunctions.Checked = Config.UseAdminFunctions
        PromptForBlock.Checked = Config.PromptForBlock
        BlockReason.Text = Config.BlockReason
        BlockTime.Text = Config.BlockTime
        BlockTimeAnon.Text = Config.BlockTimeAnon
        WatchDelete.Checked = Config.WatchDelete

        ColorComment.BackColor = Highlight.CommentC
        ColorExternalLink.BackColor = Highlight.ExternalC
        ColorHtmlTag.BackColor = Highlight.HtmlC
        ColorImage.BackColor = Highlight.ImageHC
        ColorLink.BackColor = Highlight.LinkC
        ColorMagicWord.BackColor = Highlight.MagicWordC
        ColorParam.BackColor = Highlight.ParameterHC
        ColorParamCall.BackColor = Highlight.ParamCallC
        ColorParamName.BackColor = Highlight.ParamNameC
        ColorReference.BackColor = Highlight.ReferenceHC
        ColorTemplate.BackColor = Highlight.TemplateC

        InitialiseShortcutList()

        Initializing = False
    End Sub

    Private Sub InitialiseShortcutList()
        ShortcutKeysClone = New Dictionary(Of String, Shortcut)(ShortcutKeys)

        ShortcutList.Items.Clear()
        ShortcutList.BeginUpdate()

        For Each Item As KeyValuePair(Of String, Shortcut) In ShortcutKeysClone
            ChangeShortcut.Text = Item.Value.ToString

            Dim NewListViewItem As New ListViewItem
            NewListViewItem.Text = Item.Key
            NewListViewItem.SubItems.Add(Item.Value.ToString)
            ShortcutList.Items.Add(NewListViewItem)
        Next Item

        ShortcutList.Sort()
        ShortcutList.EndUpdate()
    End Sub

    Private Sub ConfigForm_FormClosing() Handles Me.FormClosing
        If DialogResult = DialogResult.OK Then
            ShortcutKeys = ShortcutKeysClone

            Config.RememberMe = RememberMe.Checked
            Config.RememberPassword = RememberPassword.Checked
            Config.AutoWhitelist = AutoWhitelist.Checked
            Config.TrayIcon = TrayIcon.Checked
            Config.ShowQueue = ShowQueue.Checked
            Config.RightAlignQueue = RightAlignQueue.Checked
            Config.ShowLog = ShowLog.Checked
            Config.ShowToolTips = ShowToolTips.Checked
            Config.OpenInBrowser = OpenInBrowser.Checked
            Config.ShowNewEdits = ShowNewEdits.Checked
            If Preloading.Checked Then Config.Preloads = CInt(Preloads.Value) Else Config.Preloads = 0
            Config.IrcMode = IrcMode.Checked
            Config.IrcPort = CInt(IrcPort.Text)
            Config.DiffFontSize = CStr(DiffFontSize.Value)
            Config.LogFile = LogFile.Text

            Config.DefaultSummary = DefaultSummary.Text
            Config.UndoSummary = UndoSummary.Text

            Config.ConfirmMultiple = ConfirmMultiple.Checked
            Config.ConfirmSame = ConfirmSame.Checked
            Config.ConfirmSelfRevert = ConfirmSelfRevert.Checked
            Config.ConfirmWarned = ConfirmWarned.Checked
            Config.ConfirmRange = ConfirmRange.Checked
            Config.AutoAdvance = AutoAdvance.Checked
            Config.UseRollback = UseRollback.Checked

            For Each Item As String In Config.Watch.Keys
                Config.Minor(Item) = False
            Next Item

            For Each Item As String In Config.Minor.Keys
                Config.Watch(Item) = False
            Next Item

            For Each Item As Object In Minor.CheckedItems
                Dim ItemName As String = CStr(Item)

                For Each Item2 As String In Config.Minor.Keys
                    If ItemName = Msg("edittype-" & Item2) Then
                        Config.Minor(Item2) = True
                        Exit For
                    End If
                Next Item2
            Next Item

            For Each Item As Object In Watchlist.CheckedItems
                Dim ItemName As String = CStr(Item)

                For Each Item2 As String In Config.Watch.Keys
                    If ItemName = Msg("edittype-" & Item2) Then
                        Config.Watch(Item2) = True
                        Exit For
                    End If
                Next Item2
            Next Item

            Config.CustomRevertSummaries.Clear()

            For Each Item As String In RevertSummaries.Items
                Config.CustomRevertSummaries.Add(Item)
            Next Item

            Config.ReportLinkDiffs = ReportLinkExamples.Checked
            Config.ExtendReports = ExtendReports.Checked
            Config.AutoReport = ReportAuto.Checked
            Config.PromptForReport = ReportPrompt.Checked

            Config.TemplateMessages.Clear()

            For Each Item As ListViewItem In Templates.Items
                Config.TemplateMessages.Add(Item.Text.Replace(";", "\;") & ";" & _
                    Item.SubItems(1).Text.Replace(";", "\;"))
            Next Item

            Config.UseAdminFunctions = UseAdminFunctions.Checked
            Config.PromptForBlock = PromptForBlock.Checked
            Config.BlockReason = BlockReason.Text
            Config.BlockTime = BlockTime.Text
            Config.BlockTimeAnon = BlockTimeAnon.Text
            Config.WatchDelete = WatchDelete.Checked

        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub ConfigForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If Not ChangeShortcut.Focused AndAlso e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Dim NewConfigRequest As New SaveUserConfigRequest
        NewConfigRequest.Start()

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub AddSummary_Click() Handles AddSummary.Click
        Dim Item As String = InputBox.Show(Msg("config-summaryprompt") & ":")
        If Item <> "" Then RevertSummaries.Items.Add(Item)
    End Sub

    Private Sub AddTemplate_Click() Handles AddTemplate.Click
        Dim NewAddTemplateForm As New AddTemplateForm

        If NewAddTemplateForm.ShowDialog = DialogResult.OK Then
            Dim NewListItem As New ListViewItem(NewAddTemplateForm.DisplayText)
            NewListItem.SubItems.Add(NewAddTemplateForm.Template)
            Templates.Items.Add(NewListItem)
        End If
    End Sub

    Private Sub ClearRevertSummaries_Click() Handles ClearSummaries.Click
        Config.RevertSummaries.Clear()
        ClearSummaries.Enabled = False
    End Sub

    Private Sub Defaults_Click() Handles Defaults.Click
        If MessageBox.Show(Msg("config-defaultsprompt"), "Huggle", _
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            InitialiseShortcuts()
            InitialiseShortcutList()
            ChangeShortcut.Clear()
        End If
    End Sub

    Private Sub LogFileBrowse_Click() Handles LogFileBrowse.Click
        Dim Dialog As New SaveFileDialog

        Dialog.Title = Msg("config-logbrowsetitle")
        Dialog.FileName = "huggle.log"
        Dialog.Filter = "Text file|*.txt"

        If Dialog.ShowDialog = DialogResult.OK Then LogFile.Text = Dialog.FileName
    End Sub

    Private Sub NewShortcut_GotFocus() Handles ChangeShortcut.GotFocus
        ChangeShortcut.Clear()
    End Sub

    Private Sub NewShortcut_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles ChangeShortcut.KeyDown
        e.SuppressKeyPress = True

        If e.KeyCode <> Keys.ControlKey AndAlso e.KeyCode <> Keys.ShiftKey AndAlso e.KeyCode <> Keys.Alt _
            AndAlso ShortcutList.SelectedItems.Count > 0 Then

            Dim NewShortcut As New Shortcut(e.KeyCode, e.Control, e.Alt, e.Shift)

            'Detect conflicts
            For Each Item As KeyValuePair(Of String, Shortcut) In ShortcutKeys
                If Item.Key <> ShortcutList.SelectedItems(0).Text AndAlso Item.Value = NewShortcut Then
                    MessageBox.Show(Msg("config-shortcutconflict", NewShortcut.ToString, Item.Key), "Huggle", _
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next Item

            ChangeShortcut.Text = NewShortcut.ToString

            ShortcutKeysClone(ShortcutList.SelectedItems(0).Text) = NewShortcut
            ShortcutList.SelectedItems(0).SubItems(1).Text = NewShortcut.ToString
        End If
    End Sub

    Private Sub NoShortcut_Click() Handles NoShortcut.Click
        If ShortcutList.SelectedItems.Count > 0 Then
            ShortcutKeysClone(ShortcutList.SelectedItems(0).Text) = New Shortcut(Keys.None)
            ShortcutList.SelectedItems(0).SubItems(1).Text = Msg("config-noshortcut")
            ChangeShortcut.Clear()
            ChangeShortcut.Focus()
        End If
    End Sub

    Private Sub Preloading_CheckedChanged() Handles Preloading.CheckedChanged
        Preloads.Enabled = (Preloading.Checked)
    End Sub

    Private Sub RememberPassword_CheckedChanged() Handles RememberPassword.CheckedChanged
        If Not Initializing AndAlso RememberPassword.Checked Then MessageBox.Show _
            ("If this option is selected, your password will be stored locally, unencrypted." & CRLF & _
            "If this bothers you, do not select this option (though it shouldn't, as your password " & CRLF & _
            "is sent unencrypted across the Web every time you log in anyway).", "Huggle", _
            MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub RemoveTemplate_Click() Handles RemoveTemplate.Click
        If Templates.SelectedItems.Count > 0 Then Templates.Items.Remove(Templates.SelectedItems(0))
    End Sub

    Private Sub RemoveSummary_Click() Handles RemoveSummary.Click
        If RevertSummaries.SelectedIndex > -1 Then RevertSummaries.Items.RemoveAt(RevertSummaries.SelectedIndex)
    End Sub

    Private Sub ReportLinkExamples_CheckedChanged() Handles ReportLinkExamples.CheckedChanged
        ExtendReports.Enabled = ReportLinkExamples.Checked
    End Sub

    Private Sub RevertSummaries_SelectedIndexChanged() Handles RevertSummaries.SelectedIndexChanged
        RemoveSummary.Enabled = (RevertSummaries.SelectedIndex > -1)
    End Sub

    Private Sub Shortcuts_MouseUp() Handles ShortcutList.MouseUp
        ChangeShortcut.Focus()
    End Sub

    Private Sub Shortcuts_SelectedIndexChanged() Handles ShortcutList.SelectedIndexChanged
        If ShortcutList.SelectedItems.Count > 0 Then
            ChangeShortcutLabel.Visible = True
            ChangeShortcutLabel.Text = Msg("config-changeshortcut", ShortcutList.SelectedItems(0).Text) & ":"
            NoShortcut.Visible = True
            ChangeShortcut.Visible = True
            ChangeShortcut.Text = ShortcutList.SelectedItems(0).SubItems(1).Text
            ChangeShortcut.Focus()
        End If
    End Sub

    Private Sub Templates_SelectedIndexChanged() Handles Templates.SelectedIndexChanged
        RemoveTemplate.Enabled = (Templates.SelectedItems.Count > 0)
    End Sub

    Private Sub SetColor(ByVal sender As Object, ByVal e As EventArgs) Handles ColorComment.Click, _
        ColorExternalLink.Click, ColorHtmlTag.Click, ColorImage.Click, ColorLink.Click, ColorMagicWord.Click, _
        ColorParam.Click, ColorParamCall.Click, ColorParamName.Click, ColorReference.Click, ColorTemplate.Click

        Dim Control As Control = CType(sender, Control)
        Dim NewColorDialog As New ColorDialog
        NewColorDialog.AnyColor = True
        NewColorDialog.FullOpen = True
        NewColorDialog.Color = Control.BackColor
        If NewColorDialog.ShowDialog = DialogResult.OK Then Control.BackColor = NewColorDialog.Color
    End Sub

    Private Sub ViewLocalConfig_LinkClicked(ByVal s As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles ViewLocalConfig.LinkClicked

        Process.Start("""" & LocalConfigPath() & """")
    End Sub

End Class