<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.Tabs = New System.Windows.Forms.TabControl()
        Me.GeneralTab = New System.Windows.Forms.TabPage()
        Me.History = New System.Windows.Forms.CheckBox()
        Me.RememberPassword = New System.Windows.Forms.CheckBox()
        Me.RememberMe = New System.Windows.Forms.CheckBox()
        Me.DiffFontSize = New System.Windows.Forms.NumericUpDown()
        Me.PreloadCount = New System.Windows.Forms.NumericUpDown()
        Me.IrcMode = New System.Windows.Forms.CheckBox()
        Me.LogFileBrowse = New System.Windows.Forms.Button()
        Me.LogFile = New System.Windows.Forms.TextBox()
        Me.LogFileLabel = New System.Windows.Forms.Label()
        Me.OpenInBrowser = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.DiffFontSizeLabel = New System.Windows.Forms.Label()
        Me.ShowNewEdits = New System.Windows.Forms.CheckBox()
        Me.Preloads = New System.Windows.Forms.CheckBox()
        Me.IrcPortLabel = New System.Windows.Forms.Label()
        Me.IrcPort = New System.Windows.Forms.TextBox()
        Me.AutoWhitelist = New System.Windows.Forms.CheckBox()
        Me.InterfaceTab = New System.Windows.Forms.TabPage()
        Me.RightAlignQueue = New System.Windows.Forms.CheckBox()
        Me.ShowTwoQueues = New System.Windows.Forms.CheckBox()
        Me.ShowQueue = New System.Windows.Forms.CheckBox()
        Me.ShowLog = New System.Windows.Forms.CheckBox()
        Me.ShowNewMessages = New System.Windows.Forms.CheckBox()
        Me.ShowToolTips = New System.Windows.Forms.CheckBox()
        Me.TrayIcon = New System.Windows.Forms.CheckBox()
        Me.KeyboardTab = New System.Windows.Forms.TabPage()
        Me.Defaults = New System.Windows.Forms.Button()
        Me.NoShortcut = New System.Windows.Forms.Button()
        Me.ChangeShortcut = New System.Windows.Forms.TextBox()
        Me.ChangeShortcutLabel = New System.Windows.Forms.Label()
        Me.ShortcutListLabel = New System.Windows.Forms.Label()
        Me.ShortcutList = New System.Windows.Forms.ListView()
        Me.ActionColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ShortcutColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.EditingTab = New System.Windows.Forms.TabPage()
        Me.UndoSummary = New System.Windows.Forms.TextBox()
        Me.UndoSummaryLabel = New System.Windows.Forms.Label()
        Me.DefaultSummary = New System.Windows.Forms.TextBox()
        Me.DefaultSummaryLabel = New System.Windows.Forms.Label()
        Me.WatchlistLabel = New System.Windows.Forms.Label()
        Me.MinorLabel = New System.Windows.Forms.Label()
        Me.Watchlist = New System.Windows.Forms.CheckedListBox()
        Me.Minor = New System.Windows.Forms.CheckedListBox()
        Me.RevertTab = New System.Windows.Forms.TabPage()
        Me.ConfirmPage = New System.Windows.Forms.CheckBox()
        Me.ConfirmWarned = New System.Windows.Forms.CheckBox()
        Me.ClearSummaries = New System.Windows.Forms.Button()
        Me.ClearSummariesLabel = New System.Windows.Forms.Label()
        Me.ConfirmSelfRevert = New System.Windows.Forms.CheckBox()
        Me.AddSummary = New System.Windows.Forms.Button()
        Me.RemoveSummary = New System.Windows.Forms.Button()
        Me.RevertSummariesLabel = New System.Windows.Forms.Label()
        Me.RevertSummaries = New System.Windows.Forms.ListBox()
        Me.UseRollback = New System.Windows.Forms.CheckBox()
        Me.AutoAdvance = New System.Windows.Forms.CheckBox()
        Me.ConfirmSame = New System.Windows.Forms.CheckBox()
        Me.ConfirmMultiple = New System.Windows.Forms.CheckBox()
        Me.ConfirmRange = New System.Windows.Forms.CheckBox()
        Me.ReportingTab = New System.Windows.Forms.TabPage()
        Me.ExtendReports = New System.Windows.Forms.CheckBox()
        Me.ReportLinkExamples = New System.Windows.Forms.CheckBox()
        Me.AutoReportGroup = New System.Windows.Forms.GroupBox()
        Me.ReportAuto = New System.Windows.Forms.RadioButton()
        Me.AutoReportLabel = New System.Windows.Forms.Label()
        Me.ReportNone = New System.Windows.Forms.RadioButton()
        Me.ReportPrompt = New System.Windows.Forms.RadioButton()
        Me.TemplatesTab = New System.Windows.Forms.TabPage()
        Me.btImport = New System.Windows.Forms.Button()
        Me.UseCustom = New System.Windows.Forms.CheckBox()
        Me.AddTemplate = New System.Windows.Forms.Button()
        Me.RemoveTemplate = New System.Windows.Forms.Button()
        Me.TemplatesLabel = New System.Windows.Forms.Label()
        Me.Templates = New System.Windows.Forms.ListView()
        Me.DisplayColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TemplateColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.EditorTab = New System.Windows.Forms.TabPage()
        Me.HighlightGroup = New System.Windows.Forms.GroupBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.ColorParamCall = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.ColorHtmlTag = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.ColorTemplate = New System.Windows.Forms.Button()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.ColorParamName = New System.Windows.Forms.Button()
        Me.ColorExternalLink = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.ColorParam = New System.Windows.Forms.Button()
        Me.ColorMagicWord = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ColorImage = New System.Windows.Forms.Button()
        Me.ColorLink = New System.Windows.Forms.Button()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.ColorReference = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.ColorComment = New System.Windows.Forms.Button()
        Me.AdminTab = New System.Windows.Forms.TabPage()
        Me.WatchDelete = New System.Windows.Forms.CheckBox()
        Me.BlockTime = New System.Windows.Forms.TextBox()
        Me.BlockTimeRegLabel = New System.Windows.Forms.Label()
        Me.BlockTimeAnonLabel = New System.Windows.Forms.Label()
        Me.BlockTimeLabel = New System.Windows.Forms.Label()
        Me.BlockTimeAnon = New System.Windows.Forms.TextBox()
        Me.BlockReasonLabel = New System.Windows.Forms.Label()
        Me.BlockReason = New System.Windows.Forms.TextBox()
        Me.PromptForBlock = New System.Windows.Forms.CheckBox()
        Me.UseAdminFunctions = New System.Windows.Forms.CheckBox()
        Me.ViewLocalConfig = New System.Windows.Forms.LinkLabel()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker3 = New System.ComponentModel.BackgroundWorker()
        Me.Tabs.SuspendLayout()
        Me.GeneralTab.SuspendLayout()
        CType(Me.DiffFontSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PreloadCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.InterfaceTab.SuspendLayout()
        Me.KeyboardTab.SuspendLayout()
        Me.EditingTab.SuspendLayout()
        Me.RevertTab.SuspendLayout()
        Me.ReportingTab.SuspendLayout()
        Me.AutoReportGroup.SuspendLayout()
        Me.TemplatesTab.SuspendLayout()
        Me.EditorTab.SuspendLayout()
        Me.HighlightGroup.SuspendLayout()
        Me.AdminTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(595, 462)
        Me.Cancel.Margin = New System.Windows.Forms.Padding(4)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(100, 28)
        Me.Cancel.TabIndex = 3
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(487, 462)
        Me.OK.Margin = New System.Windows.Forms.Padding(4)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(100, 28)
        Me.OK.TabIndex = 2
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Tabs
        '
        Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Controls.Add(Me.GeneralTab)
        Me.Tabs.Controls.Add(Me.InterfaceTab)
        Me.Tabs.Controls.Add(Me.KeyboardTab)
        Me.Tabs.Controls.Add(Me.EditingTab)
        Me.Tabs.Controls.Add(Me.RevertTab)
        Me.Tabs.Controls.Add(Me.ReportingTab)
        Me.Tabs.Controls.Add(Me.TemplatesTab)
        Me.Tabs.Controls.Add(Me.EditorTab)
        Me.Tabs.Controls.Add(Me.AdminTab)
        Me.Tabs.ItemSize = New System.Drawing.Size(49, 19)
        Me.Tabs.Location = New System.Drawing.Point(8, 12)
        Me.Tabs.Margin = New System.Windows.Forms.Padding(4)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(692, 442)
        Me.Tabs.TabIndex = 0
        '
        'GeneralTab
        '
        Me.GeneralTab.Controls.Add(Me.History)
        Me.GeneralTab.Controls.Add(Me.RememberPassword)
        Me.GeneralTab.Controls.Add(Me.RememberMe)
        Me.GeneralTab.Controls.Add(Me.DiffFontSize)
        Me.GeneralTab.Controls.Add(Me.PreloadCount)
        Me.GeneralTab.Controls.Add(Me.IrcMode)
        Me.GeneralTab.Controls.Add(Me.LogFileBrowse)
        Me.GeneralTab.Controls.Add(Me.LogFile)
        Me.GeneralTab.Controls.Add(Me.LogFileLabel)
        Me.GeneralTab.Controls.Add(Me.OpenInBrowser)
        Me.GeneralTab.Controls.Add(Me.Label2)
        Me.GeneralTab.Controls.Add(Me.Label1)
        Me.GeneralTab.Controls.Add(Me.Label14)
        Me.GeneralTab.Controls.Add(Me.DiffFontSizeLabel)
        Me.GeneralTab.Controls.Add(Me.ShowNewEdits)
        Me.GeneralTab.Controls.Add(Me.Preloads)
        Me.GeneralTab.Controls.Add(Me.IrcPortLabel)
        Me.GeneralTab.Controls.Add(Me.IrcPort)
        Me.GeneralTab.Controls.Add(Me.AutoWhitelist)
        Me.GeneralTab.Location = New System.Drawing.Point(4, 23)
        Me.GeneralTab.Margin = New System.Windows.Forms.Padding(4)
        Me.GeneralTab.Name = "GeneralTab"
        Me.GeneralTab.Padding = New System.Windows.Forms.Padding(4)
        Me.GeneralTab.Size = New System.Drawing.Size(684, 415)
        Me.GeneralTab.TabIndex = 0
        Me.GeneralTab.Text = "General"
        Me.GeneralTab.UseVisualStyleBackColor = True
        '
        'History
        '
        Me.History.AutoSize = True
        Me.History.Location = New System.Drawing.Point(12, 217)
        Me.History.Margin = New System.Windows.Forms.Padding(4)
        Me.History.Name = "History"
        Me.History.Size = New System.Drawing.Size(115, 21)
        Me.History.TabIndex = 18
        Me.History.Text = "Download diff"
        Me.History.UseVisualStyleBackColor = True
        '
        'RememberPassword
        '
        Me.RememberPassword.AutoSize = True
        Me.RememberPassword.Location = New System.Drawing.Point(12, 47)
        Me.RememberPassword.Margin = New System.Windows.Forms.Padding(4)
        Me.RememberPassword.Name = "RememberPassword"
        Me.RememberPassword.Size = New System.Drawing.Size(185, 21)
        Me.RememberPassword.TabIndex = 1
        Me.RememberPassword.Text = "Remember my password"
        Me.RememberPassword.UseVisualStyleBackColor = True
        '
        'RememberMe
        '
        Me.RememberMe.AutoSize = True
        Me.RememberMe.Location = New System.Drawing.Point(12, 18)
        Me.RememberMe.Margin = New System.Windows.Forms.Padding(4)
        Me.RememberMe.Name = "RememberMe"
        Me.RememberMe.Size = New System.Drawing.Size(188, 21)
        Me.RememberMe.TabIndex = 0
        Me.RememberMe.Text = "Remember my username"
        Me.RememberMe.UseVisualStyleBackColor = True
        '
        'DiffFontSize
        '
        Me.DiffFontSize.Location = New System.Drawing.Point(153, 316)
        Me.DiffFontSize.Margin = New System.Windows.Forms.Padding(4)
        Me.DiffFontSize.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.DiffFontSize.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.DiffFontSize.Name = "DiffFontSize"
        Me.DiffFontSize.Size = New System.Drawing.Size(67, 22)
        Me.DiffFontSize.TabIndex = 13
        Me.DiffFontSize.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'PreloadCount
        '
        Me.PreloadCount.Location = New System.Drawing.Point(215, 161)
        Me.PreloadCount.Margin = New System.Windows.Forms.Padding(4)
        Me.PreloadCount.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.PreloadCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.PreloadCount.Name = "PreloadCount"
        Me.PreloadCount.Size = New System.Drawing.Size(67, 22)
        Me.PreloadCount.TabIndex = 6
        Me.PreloadCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'IrcMode
        '
        Me.IrcMode.AutoSize = True
        Me.IrcMode.Location = New System.Drawing.Point(12, 188)
        Me.IrcMode.Margin = New System.Windows.Forms.Padding(4)
        Me.IrcMode.Name = "IrcMode"
        Me.IrcMode.Size = New System.Drawing.Size(303, 21)
        Me.IrcMode.TabIndex = 8
        Me.IrcMode.Text = "Use IRC feed for recent changes if possible"
        Me.IrcMode.UseVisualStyleBackColor = True
        '
        'LogFileBrowse
        '
        Me.LogFileBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogFileBrowse.Location = New System.Drawing.Point(561, 359)
        Me.LogFileBrowse.Margin = New System.Windows.Forms.Padding(4)
        Me.LogFileBrowse.Name = "LogFileBrowse"
        Me.LogFileBrowse.Size = New System.Drawing.Size(100, 28)
        Me.LogFileBrowse.TabIndex = 17
        Me.LogFileBrowse.Text = "Browse..."
        Me.LogFileBrowse.UseVisualStyleBackColor = True
        '
        'LogFile
        '
        Me.LogFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogFile.Location = New System.Drawing.Point(153, 362)
        Me.LogFile.Margin = New System.Windows.Forms.Padding(4)
        Me.LogFile.Name = "LogFile"
        Me.LogFile.Size = New System.Drawing.Size(399, 22)
        Me.LogFile.TabIndex = 16
        '
        'LogFileLabel
        '
        Me.LogFileLabel.Location = New System.Drawing.Point(0, 366)
        Me.LogFileLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LogFileLabel.Name = "LogFileLabel"
        Me.LogFileLabel.Size = New System.Drawing.Size(153, 22)
        Me.LogFileLabel.TabIndex = 15
        Me.LogFileLabel.Text = "Log file:"
        Me.LogFileLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'OpenInBrowser
        '
        Me.OpenInBrowser.AutoSize = True
        Me.OpenInBrowser.Location = New System.Drawing.Point(12, 103)
        Me.OpenInBrowser.Margin = New System.Windows.Forms.Padding(4)
        Me.OpenInBrowser.Name = "OpenInBrowser"
        Me.OpenInBrowser.Size = New System.Drawing.Size(298, 21)
        Me.OpenInBrowser.TabIndex = 3
        Me.OpenInBrowser.Text = "Open browser links in new browser window"
        Me.OpenInBrowser.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(290, 163)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "(1 - 5)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(223, 277)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 17)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "(6664 - 6669)"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(223, 319)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(20, 17)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "pt"
        '
        'DiffFontSizeLabel
        '
        Me.DiffFontSizeLabel.Location = New System.Drawing.Point(0, 319)
        Me.DiffFontSizeLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DiffFontSizeLabel.Name = "DiffFontSizeLabel"
        Me.DiffFontSizeLabel.Size = New System.Drawing.Size(153, 22)
        Me.DiffFontSizeLabel.TabIndex = 12
        Me.DiffFontSizeLabel.Text = "Diff font size:"
        Me.DiffFontSizeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ShowNewEdits
        '
        Me.ShowNewEdits.AutoSize = True
        Me.ShowNewEdits.Location = New System.Drawing.Point(12, 132)
        Me.ShowNewEdits.Margin = New System.Windows.Forms.Padding(4)
        Me.ShowNewEdits.Name = "ShowNewEdits"
        Me.ShowNewEdits.Size = New System.Drawing.Size(374, 21)
        Me.ShowNewEdits.TabIndex = 4
        Me.ShowNewEdits.Text = "Show new edits to the selected page as they are made"
        Me.ShowNewEdits.UseVisualStyleBackColor = True
        '
        'Preloads
        '
        Me.Preloads.AutoSize = True
        Me.Preloads.Location = New System.Drawing.Point(12, 160)
        Me.Preloads.Margin = New System.Windows.Forms.Padding(4)
        Me.Preloads.Name = "Preloads"
        Me.Preloads.Size = New System.Drawing.Size(195, 21)
        Me.Preloads.TabIndex = 5
        Me.Preloads.Text = "Enable preloading of diffs:"
        Me.Preloads.UseVisualStyleBackColor = True
        '
        'IrcPortLabel
        '
        Me.IrcPortLabel.Location = New System.Drawing.Point(0, 277)
        Me.IrcPortLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.IrcPortLabel.Name = "IrcPortLabel"
        Me.IrcPortLabel.Size = New System.Drawing.Size(153, 21)
        Me.IrcPortLabel.TabIndex = 9
        Me.IrcPortLabel.Text = "IRC port:"
        Me.IrcPortLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'IrcPort
        '
        Me.IrcPort.Location = New System.Drawing.Point(153, 273)
        Me.IrcPort.Margin = New System.Windows.Forms.Padding(4)
        Me.IrcPort.Name = "IrcPort"
        Me.IrcPort.Size = New System.Drawing.Size(65, 22)
        Me.IrcPort.TabIndex = 10
        '
        'AutoWhitelist
        '
        Me.AutoWhitelist.AutoSize = True
        Me.AutoWhitelist.Location = New System.Drawing.Point(12, 75)
        Me.AutoWhitelist.Margin = New System.Windows.Forms.Padding(4)
        Me.AutoWhitelist.Name = "AutoWhitelist"
        Me.AutoWhitelist.Size = New System.Drawing.Size(205, 21)
        Me.AutoWhitelist.TabIndex = 2
        Me.AutoWhitelist.Text = "Automatically whitelist users"
        Me.AutoWhitelist.UseVisualStyleBackColor = True
        '
        'InterfaceTab
        '
        Me.InterfaceTab.Controls.Add(Me.RightAlignQueue)
        Me.InterfaceTab.Controls.Add(Me.ShowTwoQueues)
        Me.InterfaceTab.Controls.Add(Me.ShowQueue)
        Me.InterfaceTab.Controls.Add(Me.ShowLog)
        Me.InterfaceTab.Controls.Add(Me.ShowNewMessages)
        Me.InterfaceTab.Controls.Add(Me.ShowToolTips)
        Me.InterfaceTab.Controls.Add(Me.TrayIcon)
        Me.InterfaceTab.Location = New System.Drawing.Point(4, 23)
        Me.InterfaceTab.Margin = New System.Windows.Forms.Padding(4)
        Me.InterfaceTab.Name = "InterfaceTab"
        Me.InterfaceTab.Padding = New System.Windows.Forms.Padding(4)
        Me.InterfaceTab.Size = New System.Drawing.Size(684, 415)
        Me.InterfaceTab.TabIndex = 9
        Me.InterfaceTab.Text = "Interface"
        Me.InterfaceTab.UseVisualStyleBackColor = True
        '
        'RightAlignQueue
        '
        Me.RightAlignQueue.AutoSize = True
        Me.RightAlignQueue.Location = New System.Drawing.Point(12, 132)
        Me.RightAlignQueue.Margin = New System.Windows.Forms.Padding(4)
        Me.RightAlignQueue.Name = "RightAlignQueue"
        Me.RightAlignQueue.Size = New System.Drawing.Size(273, 21)
        Me.RightAlignQueue.TabIndex = 3
        Me.RightAlignQueue.Text = "Show queue on the right of the window"
        Me.RightAlignQueue.UseVisualStyleBackColor = True
        '
        'ShowTwoQueues
        '
        Me.ShowTwoQueues.AutoSize = True
        Me.ShowTwoQueues.Location = New System.Drawing.Point(12, 103)
        Me.ShowTwoQueues.Margin = New System.Windows.Forms.Padding(4)
        Me.ShowTwoQueues.Name = "ShowTwoQueues"
        Me.ShowTwoQueues.Size = New System.Drawing.Size(140, 21)
        Me.ShowTwoQueues.TabIndex = 2
        Me.ShowTwoQueues.Text = "Show two queues"
        Me.ShowTwoQueues.UseVisualStyleBackColor = True
        '
        'ShowQueue
        '
        Me.ShowQueue.AutoSize = True
        Me.ShowQueue.Location = New System.Drawing.Point(12, 75)
        Me.ShowQueue.Margin = New System.Windows.Forms.Padding(4)
        Me.ShowQueue.Name = "ShowQueue"
        Me.ShowQueue.Size = New System.Drawing.Size(161, 21)
        Me.ShowQueue.TabIndex = 2
        Me.ShowQueue.Text = "Show revision queue"
        Me.ShowQueue.UseVisualStyleBackColor = True
        '
        'ShowLog
        '
        Me.ShowLog.AutoSize = True
        Me.ShowLog.Location = New System.Drawing.Point(12, 47)
        Me.ShowLog.Margin = New System.Windows.Forms.Padding(4)
        Me.ShowLog.Name = "ShowLog"
        Me.ShowLog.Size = New System.Drawing.Size(87, 21)
        Me.ShowLog.TabIndex = 1
        Me.ShowLog.Text = "Show log"
        Me.ShowLog.UseVisualStyleBackColor = True
        '
        'ShowNewMessages
        '
        Me.ShowNewMessages.AutoSize = True
        Me.ShowNewMessages.Location = New System.Drawing.Point(12, 188)
        Me.ShowNewMessages.Margin = New System.Windows.Forms.Padding(4)
        Me.ShowNewMessages.Name = "ShowNewMessages"
        Me.ShowNewMessages.Size = New System.Drawing.Size(186, 21)
        Me.ShowNewMessages.TabIndex = 5
        Me.ShowNewMessages.Text = "Show new messages bar"
        Me.ShowNewMessages.UseVisualStyleBackColor = True
        '
        'ShowToolTips
        '
        Me.ShowToolTips.AutoSize = True
        Me.ShowToolTips.Location = New System.Drawing.Point(12, 160)
        Me.ShowToolTips.Margin = New System.Windows.Forms.Padding(4)
        Me.ShowToolTips.Name = "ShowToolTips"
        Me.ShowToolTips.Size = New System.Drawing.Size(179, 21)
        Me.ShowToolTips.TabIndex = 4
        Me.ShowToolTips.Text = "Show tooltips on menus"
        Me.ShowToolTips.UseVisualStyleBackColor = True
        '
        'TrayIcon
        '
        Me.TrayIcon.AutoSize = True
        Me.TrayIcon.Location = New System.Drawing.Point(12, 18)
        Me.TrayIcon.Margin = New System.Windows.Forms.Padding(4)
        Me.TrayIcon.Name = "TrayIcon"
        Me.TrayIcon.Size = New System.Drawing.Size(122, 21)
        Me.TrayIcon.TabIndex = 0
        Me.TrayIcon.Text = "Show tray icon"
        Me.TrayIcon.UseVisualStyleBackColor = True
        '
        'KeyboardTab
        '
        Me.KeyboardTab.Controls.Add(Me.Defaults)
        Me.KeyboardTab.Controls.Add(Me.NoShortcut)
        Me.KeyboardTab.Controls.Add(Me.ChangeShortcut)
        Me.KeyboardTab.Controls.Add(Me.ChangeShortcutLabel)
        Me.KeyboardTab.Controls.Add(Me.ShortcutListLabel)
        Me.KeyboardTab.Controls.Add(Me.ShortcutList)
        Me.KeyboardTab.Location = New System.Drawing.Point(4, 23)
        Me.KeyboardTab.Margin = New System.Windows.Forms.Padding(4)
        Me.KeyboardTab.Name = "KeyboardTab"
        Me.KeyboardTab.Padding = New System.Windows.Forms.Padding(4)
        Me.KeyboardTab.Size = New System.Drawing.Size(684, 415)
        Me.KeyboardTab.TabIndex = 7
        Me.KeyboardTab.Text = "Keyboard"
        Me.KeyboardTab.UseVisualStyleBackColor = True
        '
        'Defaults
        '
        Me.Defaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Defaults.Location = New System.Drawing.Point(573, 336)
        Me.Defaults.Margin = New System.Windows.Forms.Padding(4)
        Me.Defaults.Name = "Defaults"
        Me.Defaults.Size = New System.Drawing.Size(100, 28)
        Me.Defaults.TabIndex = 5
        Me.Defaults.Text = "Defaults"
        Me.Defaults.UseVisualStyleBackColor = True
        '
        'NoShortcut
        '
        Me.NoShortcut.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NoShortcut.Location = New System.Drawing.Point(491, 336)
        Me.NoShortcut.Margin = New System.Windows.Forms.Padding(4)
        Me.NoShortcut.Name = "NoShortcut"
        Me.NoShortcut.Size = New System.Drawing.Size(75, 28)
        Me.NoShortcut.TabIndex = 4
        Me.NoShortcut.Text = "None"
        Me.NoShortcut.UseVisualStyleBackColor = True
        Me.NoShortcut.Visible = False
        '
        'ChangeShortcut
        '
        Me.ChangeShortcut.AcceptsReturn = True
        Me.ChangeShortcut.AcceptsTab = True
        Me.ChangeShortcut.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChangeShortcut.Location = New System.Drawing.Point(381, 338)
        Me.ChangeShortcut.Margin = New System.Windows.Forms.Padding(4)
        Me.ChangeShortcut.Multiline = True
        Me.ChangeShortcut.Name = "ChangeShortcut"
        Me.ChangeShortcut.Size = New System.Drawing.Size(100, 24)
        Me.ChangeShortcut.TabIndex = 3
        Me.ChangeShortcut.TabStop = False
        Me.ChangeShortcut.Visible = False
        '
        'ChangeShortcutLabel
        '
        Me.ChangeShortcutLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ChangeShortcutLabel.AutoSize = True
        Me.ChangeShortcutLabel.Location = New System.Drawing.Point(4, 342)
        Me.ChangeShortcutLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ChangeShortcutLabel.Name = "ChangeShortcutLabel"
        Me.ChangeShortcutLabel.Size = New System.Drawing.Size(141, 17)
        Me.ChangeShortcutLabel.TabIndex = 2
        Me.ChangeShortcutLabel.Text = "Change shortcut for :"
        Me.ChangeShortcutLabel.Visible = False
        '
        'ShortcutListLabel
        '
        Me.ShortcutListLabel.AutoSize = True
        Me.ShortcutListLabel.Location = New System.Drawing.Point(8, 18)
        Me.ShortcutListLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ShortcutListLabel.Name = "ShortcutListLabel"
        Me.ShortcutListLabel.Size = New System.Drawing.Size(135, 17)
        Me.ShortcutListLabel.TabIndex = 0
        Me.ShortcutListLabel.Text = "Keyboard shortcuts:"
        '
        'ShortcutList
        '
        Me.ShortcutList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShortcutList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ActionColumn, Me.ShortcutColumn})
        Me.ShortcutList.FullRowSelect = True
        Me.ShortcutList.GridLines = True
        Me.ShortcutList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ShortcutList.HideSelection = False
        Me.ShortcutList.Location = New System.Drawing.Point(8, 38)
        Me.ShortcutList.Margin = New System.Windows.Forms.Padding(4)
        Me.ShortcutList.MultiSelect = False
        Me.ShortcutList.Name = "ShortcutList"
        Me.ShortcutList.Size = New System.Drawing.Size(664, 282)
        Me.ShortcutList.TabIndex = 1
        Me.ShortcutList.UseCompatibleStateImageBehavior = False
        Me.ShortcutList.View = System.Windows.Forms.View.Details
        '
        'ActionColumn
        '
        Me.ActionColumn.Text = "Action"
        Me.ActionColumn.Width = 244
        '
        'ShortcutColumn
        '
        Me.ShortcutColumn.Text = "Shortcut"
        Me.ShortcutColumn.Width = 175
        '
        'EditingTab
        '
        Me.EditingTab.Controls.Add(Me.UndoSummary)
        Me.EditingTab.Controls.Add(Me.UndoSummaryLabel)
        Me.EditingTab.Controls.Add(Me.DefaultSummary)
        Me.EditingTab.Controls.Add(Me.DefaultSummaryLabel)
        Me.EditingTab.Controls.Add(Me.WatchlistLabel)
        Me.EditingTab.Controls.Add(Me.MinorLabel)
        Me.EditingTab.Controls.Add(Me.Watchlist)
        Me.EditingTab.Controls.Add(Me.Minor)
        Me.EditingTab.Location = New System.Drawing.Point(4, 23)
        Me.EditingTab.Margin = New System.Windows.Forms.Padding(4)
        Me.EditingTab.Name = "EditingTab"
        Me.EditingTab.Padding = New System.Windows.Forms.Padding(4)
        Me.EditingTab.Size = New System.Drawing.Size(684, 415)
        Me.EditingTab.TabIndex = 2
        Me.EditingTab.Text = "Editing"
        Me.EditingTab.UseVisualStyleBackColor = True
        '
        'UndoSummary
        '
        Me.UndoSummary.Location = New System.Drawing.Point(12, 334)
        Me.UndoSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.UndoSummary.Name = "UndoSummary"
        Me.UndoSummary.Size = New System.Drawing.Size(456, 22)
        Me.UndoSummary.TabIndex = 7
        '
        'UndoSummaryLabel
        '
        Me.UndoSummaryLabel.AutoSize = True
        Me.UndoSummaryLabel.Location = New System.Drawing.Point(8, 314)
        Me.UndoSummaryLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.UndoSummaryLabel.Name = "UndoSummaryLabel"
        Me.UndoSummaryLabel.Size = New System.Drawing.Size(226, 17)
        Me.UndoSummaryLabel.TabIndex = 6
        Me.UndoSummaryLabel.Text = "Summary when undoing own edits:"
        '
        'DefaultSummary
        '
        Me.DefaultSummary.Location = New System.Drawing.Point(12, 278)
        Me.DefaultSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.DefaultSummary.Name = "DefaultSummary"
        Me.DefaultSummary.Size = New System.Drawing.Size(456, 22)
        Me.DefaultSummary.TabIndex = 5
        '
        'DefaultSummaryLabel
        '
        Me.DefaultSummaryLabel.AutoSize = True
        Me.DefaultSummaryLabel.Location = New System.Drawing.Point(8, 258)
        Me.DefaultSummaryLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DefaultSummaryLabel.Name = "DefaultSummaryLabel"
        Me.DefaultSummaryLabel.Size = New System.Drawing.Size(223, 17)
        Me.DefaultSummaryLabel.TabIndex = 4
        Me.DefaultSummaryLabel.Text = "Default summary for manual edits:"
        '
        'WatchlistLabel
        '
        Me.WatchlistLabel.AutoSize = True
        Me.WatchlistLabel.Location = New System.Drawing.Point(332, 18)
        Me.WatchlistLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.WatchlistLabel.Name = "WatchlistLabel"
        Me.WatchlistLabel.Size = New System.Drawing.Size(110, 17)
        Me.WatchlistLabel.TabIndex = 2
        Me.WatchlistLabel.Text = "Add to watchlist:"
        '
        'MinorLabel
        '
        Me.MinorLabel.AutoSize = True
        Me.MinorLabel.Location = New System.Drawing.Point(8, 18)
        Me.MinorLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.MinorLabel.Name = "MinorLabel"
        Me.MinorLabel.Size = New System.Drawing.Size(101, 17)
        Me.MinorLabel.TabIndex = 0
        Me.MinorLabel.Text = "Mark as minor:"
        '
        'Watchlist
        '
        Me.Watchlist.FormattingEnabled = True
        Me.Watchlist.Location = New System.Drawing.Point(336, 38)
        Me.Watchlist.Margin = New System.Windows.Forms.Padding(4)
        Me.Watchlist.Name = "Watchlist"
        Me.Watchlist.Size = New System.Drawing.Size(315, 191)
        Me.Watchlist.TabIndex = 3
        '
        'Minor
        '
        Me.Minor.FormattingEnabled = True
        Me.Minor.Location = New System.Drawing.Point(12, 38)
        Me.Minor.Margin = New System.Windows.Forms.Padding(4)
        Me.Minor.Name = "Minor"
        Me.Minor.Size = New System.Drawing.Size(315, 191)
        Me.Minor.TabIndex = 1
        '
        'RevertTab
        '
        Me.RevertTab.Controls.Add(Me.ConfirmPage)
        Me.RevertTab.Controls.Add(Me.ConfirmWarned)
        Me.RevertTab.Controls.Add(Me.ClearSummaries)
        Me.RevertTab.Controls.Add(Me.ClearSummariesLabel)
        Me.RevertTab.Controls.Add(Me.ConfirmSelfRevert)
        Me.RevertTab.Controls.Add(Me.AddSummary)
        Me.RevertTab.Controls.Add(Me.RemoveSummary)
        Me.RevertTab.Controls.Add(Me.RevertSummariesLabel)
        Me.RevertTab.Controls.Add(Me.RevertSummaries)
        Me.RevertTab.Controls.Add(Me.UseRollback)
        Me.RevertTab.Controls.Add(Me.AutoAdvance)
        Me.RevertTab.Controls.Add(Me.ConfirmSame)
        Me.RevertTab.Controls.Add(Me.ConfirmMultiple)
        Me.RevertTab.Controls.Add(Me.ConfirmRange)
        Me.RevertTab.Location = New System.Drawing.Point(4, 23)
        Me.RevertTab.Margin = New System.Windows.Forms.Padding(4)
        Me.RevertTab.Name = "RevertTab"
        Me.RevertTab.Padding = New System.Windows.Forms.Padding(4)
        Me.RevertTab.Size = New System.Drawing.Size(684, 415)
        Me.RevertTab.TabIndex = 5
        Me.RevertTab.Text = "Reverting"
        Me.RevertTab.UseVisualStyleBackColor = True
        '
        'ConfirmPage
        '
        Me.ConfirmPage.AutoSize = True
        Me.ConfirmPage.Location = New System.Drawing.Point(12, 162)
        Me.ConfirmPage.Margin = New System.Windows.Forms.Padding(4)
        Me.ConfirmPage.Name = "ConfirmPage"
        Me.ConfirmPage.Size = New System.Drawing.Size(252, 21)
        Me.ConfirmPage.TabIndex = 3
        Me.ConfirmPage.Text = "Confirm reversion of ignored pages"
        Me.ConfirmPage.UseVisualStyleBackColor = True
        '
        'ConfirmWarned
        '
        Me.ConfirmWarned.AutoSize = True
        Me.ConfirmWarned.Location = New System.Drawing.Point(12, 103)
        Me.ConfirmWarned.Margin = New System.Windows.Forms.Padding(4)
        Me.ConfirmWarned.Name = "ConfirmWarned"
        Me.ConfirmWarned.Size = New System.Drawing.Size(317, 21)
        Me.ConfirmWarned.TabIndex = 3
        Me.ConfirmWarned.Text = "Confirm reversion to an edit by a warned user"
        Me.ConfirmWarned.UseVisualStyleBackColor = True
        '
        'ClearSummaries
        '
        Me.ClearSummaries.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ClearSummaries.Enabled = False
        Me.ClearSummaries.Location = New System.Drawing.Point(573, 364)
        Me.ClearSummaries.Margin = New System.Windows.Forms.Padding(4)
        Me.ClearSummaries.Name = "ClearSummaries"
        Me.ClearSummaries.Size = New System.Drawing.Size(100, 28)
        Me.ClearSummaries.TabIndex = 12
        Me.ClearSummaries.Text = "Clear"
        Me.ClearSummaries.UseVisualStyleBackColor = True
        '
        'ClearSummariesLabel
        '
        Me.ClearSummariesLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ClearSummariesLabel.Location = New System.Drawing.Point(8, 370)
        Me.ClearSummariesLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ClearSummariesLabel.Name = "ClearSummariesLabel"
        Me.ClearSummariesLabel.Size = New System.Drawing.Size(565, 36)
        Me.ClearSummariesLabel.TabIndex = 11
        Me.ClearSummariesLabel.Text = "Summaries entered manually are remembered across sessions; click to clear these:"
        '
        'ConfirmSelfRevert
        '
        Me.ConfirmSelfRevert.AutoSize = True
        Me.ConfirmSelfRevert.Location = New System.Drawing.Point(12, 75)
        Me.ConfirmSelfRevert.Margin = New System.Windows.Forms.Padding(4)
        Me.ConfirmSelfRevert.Name = "ConfirmSelfRevert"
        Me.ConfirmSelfRevert.Size = New System.Drawing.Size(311, 21)
        Me.ConfirmSelfRevert.TabIndex = 2
        Me.ConfirmSelfRevert.Text = "Confirm reversion of own edits (except undo)"
        Me.ConfirmSelfRevert.UseVisualStyleBackColor = True
        '
        'AddSummary
        '
        Me.AddSummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AddSummary.Location = New System.Drawing.Point(8, 331)
        Me.AddSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.AddSummary.Name = "AddSummary"
        Me.AddSummary.Size = New System.Drawing.Size(93, 28)
        Me.AddSummary.TabIndex = 9
        Me.AddSummary.Text = "Add"
        Me.AddSummary.UseVisualStyleBackColor = True
        '
        'RemoveSummary
        '
        Me.RemoveSummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RemoveSummary.Enabled = False
        Me.RemoveSummary.Location = New System.Drawing.Point(109, 331)
        Me.RemoveSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.RemoveSummary.Name = "RemoveSummary"
        Me.RemoveSummary.Size = New System.Drawing.Size(93, 28)
        Me.RemoveSummary.TabIndex = 10
        Me.RemoveSummary.Text = "Remove"
        Me.RemoveSummary.UseVisualStyleBackColor = True
        '
        'RevertSummariesLabel
        '
        Me.RevertSummariesLabel.AutoSize = True
        Me.RevertSummariesLabel.Location = New System.Drawing.Point(8, 254)
        Me.RevertSummariesLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.RevertSummariesLabel.Name = "RevertSummariesLabel"
        Me.RevertSummariesLabel.Size = New System.Drawing.Size(262, 17)
        Me.RevertSummariesLabel.TabIndex = 7
        Me.RevertSummariesLabel.Text = "Reversion summaries available in menu:"
        '
        'RevertSummaries
        '
        Me.RevertSummaries.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RevertSummaries.FormattingEnabled = True
        Me.RevertSummaries.ItemHeight = 16
        Me.RevertSummaries.Location = New System.Drawing.Point(8, 273)
        Me.RevertSummaries.Margin = New System.Windows.Forms.Padding(4)
        Me.RevertSummaries.Name = "RevertSummaries"
        Me.RevertSummaries.Size = New System.Drawing.Size(644, 52)
        Me.RevertSummaries.TabIndex = 8
        '
        'UseRollback
        '
        Me.UseRollback.AutoSize = True
        Me.UseRollback.Location = New System.Drawing.Point(12, 219)
        Me.UseRollback.Margin = New System.Windows.Forms.Padding(4)
        Me.UseRollback.Name = "UseRollback"
        Me.UseRollback.Size = New System.Drawing.Size(179, 21)
        Me.UseRollback.TabIndex = 6
        Me.UseRollback.Text = "Use rollback if available"
        Me.UseRollback.UseVisualStyleBackColor = True
        '
        'AutoAdvance
        '
        Me.AutoAdvance.AutoSize = True
        Me.AutoAdvance.Location = New System.Drawing.Point(12, 191)
        Me.AutoAdvance.Margin = New System.Windows.Forms.Padding(4)
        Me.AutoAdvance.Name = "AutoAdvance"
        Me.AutoAdvance.Size = New System.Drawing.Size(342, 21)
        Me.AutoAdvance.TabIndex = 5
        Me.AutoAdvance.Text = "After reverting, move to the next edit in the queue"
        Me.AutoAdvance.UseVisualStyleBackColor = True
        '
        'ConfirmSame
        '
        Me.ConfirmSame.AutoSize = True
        Me.ConfirmSame.Location = New System.Drawing.Point(12, 47)
        Me.ConfirmSame.Margin = New System.Windows.Forms.Padding(4)
        Me.ConfirmSame.Name = "ConfirmSame"
        Me.ConfirmSame.Size = New System.Drawing.Size(375, 21)
        Me.ConfirmSame.TabIndex = 1
        Me.ConfirmSame.Text = "Confirm reversion to an edit by the user being reverted"
        Me.ConfirmSame.UseVisualStyleBackColor = True
        '
        'ConfirmMultiple
        '
        Me.ConfirmMultiple.AutoSize = True
        Me.ConfirmMultiple.Location = New System.Drawing.Point(12, 18)
        Me.ConfirmMultiple.Margin = New System.Windows.Forms.Padding(4)
        Me.ConfirmMultiple.Name = "ConfirmMultiple"
        Me.ConfirmMultiple.Size = New System.Drawing.Size(356, 21)
        Me.ConfirmMultiple.TabIndex = 0
        Me.ConfirmMultiple.Text = "Confirm reversion of multiple edits by the same user"
        Me.ConfirmMultiple.UseVisualStyleBackColor = True
        '
        'ConfirmRange
        '
        Me.ConfirmRange.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ConfirmRange.Location = New System.Drawing.Point(12, 132)
        Me.ConfirmRange.Margin = New System.Windows.Forms.Padding(4)
        Me.ConfirmRange.Name = "ConfirmRange"
        Me.ConfirmRange.Size = New System.Drawing.Size(641, 34)
        Me.ConfirmRange.TabIndex = 4
        Me.ConfirmRange.Text = "Confirm reversion to edit by an anonymous user in the same /16 range as the user " & _
            "being reverted"
        Me.ConfirmRange.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ConfirmRange.UseVisualStyleBackColor = True
        '
        'ReportingTab
        '
        Me.ReportingTab.Controls.Add(Me.ExtendReports)
        Me.ReportingTab.Controls.Add(Me.ReportLinkExamples)
        Me.ReportingTab.Controls.Add(Me.AutoReportGroup)
        Me.ReportingTab.Location = New System.Drawing.Point(4, 23)
        Me.ReportingTab.Margin = New System.Windows.Forms.Padding(4)
        Me.ReportingTab.Name = "ReportingTab"
        Me.ReportingTab.Padding = New System.Windows.Forms.Padding(4)
        Me.ReportingTab.Size = New System.Drawing.Size(684, 415)
        Me.ReportingTab.TabIndex = 3
        Me.ReportingTab.Text = "Reporting"
        Me.ReportingTab.UseVisualStyleBackColor = True
        '
        'ExtendReports
        '
        Me.ExtendReports.AutoSize = True
        Me.ExtendReports.Location = New System.Drawing.Point(36, 47)
        Me.ExtendReports.Margin = New System.Windows.Forms.Padding(4)
        Me.ExtendReports.Name = "ExtendReports"
        Me.ExtendReports.Size = New System.Drawing.Size(287, 21)
        Me.ExtendReports.TabIndex = 1
        Me.ExtendReports.Text = "Extend reports after additional vandalism"
        Me.ExtendReports.UseVisualStyleBackColor = True
        '
        'ReportLinkExamples
        '
        Me.ReportLinkExamples.AutoSize = True
        Me.ReportLinkExamples.Location = New System.Drawing.Point(12, 18)
        Me.ReportLinkExamples.Margin = New System.Windows.Forms.Padding(4)
        Me.ReportLinkExamples.Name = "ReportLinkExamples"
        Me.ReportLinkExamples.Size = New System.Drawing.Size(334, 21)
        Me.ReportLinkExamples.TabIndex = 0
        Me.ReportLinkExamples.Text = "Include links to instances of vandalism in reports"
        Me.ReportLinkExamples.UseVisualStyleBackColor = True
        '
        'AutoReportGroup
        '
        Me.AutoReportGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AutoReportGroup.Controls.Add(Me.ReportAuto)
        Me.AutoReportGroup.Controls.Add(Me.AutoReportLabel)
        Me.AutoReportGroup.Controls.Add(Me.ReportNone)
        Me.AutoReportGroup.Controls.Add(Me.ReportPrompt)
        Me.AutoReportGroup.Location = New System.Drawing.Point(8, 85)
        Me.AutoReportGroup.Margin = New System.Windows.Forms.Padding(4)
        Me.AutoReportGroup.Name = "AutoReportGroup"
        Me.AutoReportGroup.Padding = New System.Windows.Forms.Padding(4)
        Me.AutoReportGroup.Size = New System.Drawing.Size(665, 134)
        Me.AutoReportGroup.TabIndex = 2
        Me.AutoReportGroup.TabStop = False
        Me.AutoReportGroup.Text = "Auto-report"
        '
        'ReportAuto
        '
        Me.ReportAuto.AutoSize = True
        Me.ReportAuto.Location = New System.Drawing.Point(12, 96)
        Me.ReportAuto.Margin = New System.Windows.Forms.Padding(4)
        Me.ReportAuto.Name = "ReportAuto"
        Me.ReportAuto.Size = New System.Drawing.Size(190, 21)
        Me.ReportAuto.TabIndex = 3
        Me.ReportAuto.TabStop = True
        Me.ReportAuto.Text = "Issue report automatically"
        Me.ReportAuto.UseVisualStyleBackColor = True
        '
        'AutoReportLabel
        '
        Me.AutoReportLabel.AutoSize = True
        Me.AutoReportLabel.Location = New System.Drawing.Point(8, 20)
        Me.AutoReportLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.AutoReportLabel.Name = "AutoReportLabel"
        Me.AutoReportLabel.Size = New System.Drawing.Size(308, 17)
        Me.AutoReportLabel.TabIndex = 0
        Me.AutoReportLabel.Text = "When asked to warn a user with a final warning:"
        '
        'ReportNone
        '
        Me.ReportNone.AutoSize = True
        Me.ReportNone.Location = New System.Drawing.Point(12, 39)
        Me.ReportNone.Margin = New System.Windows.Forms.Padding(4)
        Me.ReportNone.Name = "ReportNone"
        Me.ReportNone.Size = New System.Drawing.Size(98, 21)
        Me.ReportNone.TabIndex = 1
        Me.ReportNone.TabStop = True
        Me.ReportNone.Text = "Do nothing"
        Me.ReportNone.UseVisualStyleBackColor = True
        '
        'ReportPrompt
        '
        Me.ReportPrompt.AutoSize = True
        Me.ReportPrompt.Location = New System.Drawing.Point(12, 68)
        Me.ReportPrompt.Margin = New System.Windows.Forms.Padding(4)
        Me.ReportPrompt.Name = "ReportPrompt"
        Me.ReportPrompt.Size = New System.Drawing.Size(137, 21)
        Me.ReportPrompt.TabIndex = 2
        Me.ReportPrompt.TabStop = True
        Me.ReportPrompt.Text = "Prompt for report"
        Me.ReportPrompt.UseVisualStyleBackColor = True
        '
        'TemplatesTab
        '
        Me.TemplatesTab.Controls.Add(Me.btImport)
        Me.TemplatesTab.Controls.Add(Me.UseCustom)
        Me.TemplatesTab.Controls.Add(Me.AddTemplate)
        Me.TemplatesTab.Controls.Add(Me.RemoveTemplate)
        Me.TemplatesTab.Controls.Add(Me.TemplatesLabel)
        Me.TemplatesTab.Controls.Add(Me.Templates)
        Me.TemplatesTab.Location = New System.Drawing.Point(4, 23)
        Me.TemplatesTab.Margin = New System.Windows.Forms.Padding(4)
        Me.TemplatesTab.Name = "TemplatesTab"
        Me.TemplatesTab.Padding = New System.Windows.Forms.Padding(4)
        Me.TemplatesTab.Size = New System.Drawing.Size(684, 415)
        Me.TemplatesTab.TabIndex = 6
        Me.TemplatesTab.Text = "Templates"
        Me.TemplatesTab.UseVisualStyleBackColor = True
        '
        'btImport
        '
        Me.btImport.Location = New System.Drawing.Point(8, 368)
        Me.btImport.Margin = New System.Windows.Forms.Padding(4)
        Me.btImport.Name = "btImport"
        Me.btImport.Size = New System.Drawing.Size(195, 27)
        Me.btImport.TabIndex = 5
        Me.btImport.Text = "Get from global"
        Me.btImport.UseVisualStyleBackColor = True
        '
        'UseCustom
        '
        Me.UseCustom.AutoSize = True
        Me.UseCustom.Location = New System.Drawing.Point(15, 343)
        Me.UseCustom.Margin = New System.Windows.Forms.Padding(4)
        Me.UseCustom.Name = "UseCustom"
        Me.UseCustom.Size = New System.Drawing.Size(18, 17)
        Me.UseCustom.TabIndex = 4
        Me.UseCustom.UseVisualStyleBackColor = True
        '
        'AddTemplate
        '
        Me.AddTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AddTemplate.Location = New System.Drawing.Point(8, 306)
        Me.AddTemplate.Margin = New System.Windows.Forms.Padding(4)
        Me.AddTemplate.Name = "AddTemplate"
        Me.AddTemplate.Size = New System.Drawing.Size(93, 28)
        Me.AddTemplate.TabIndex = 2
        Me.AddTemplate.Text = "Add"
        Me.AddTemplate.UseVisualStyleBackColor = True
        '
        'RemoveTemplate
        '
        Me.RemoveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RemoveTemplate.Enabled = False
        Me.RemoveTemplate.Location = New System.Drawing.Point(109, 306)
        Me.RemoveTemplate.Margin = New System.Windows.Forms.Padding(4)
        Me.RemoveTemplate.Name = "RemoveTemplate"
        Me.RemoveTemplate.Size = New System.Drawing.Size(93, 28)
        Me.RemoveTemplate.TabIndex = 3
        Me.RemoveTemplate.Text = "Remove"
        Me.RemoveTemplate.UseVisualStyleBackColor = True
        '
        'TemplatesLabel
        '
        Me.TemplatesLabel.AutoSize = True
        Me.TemplatesLabel.Location = New System.Drawing.Point(8, 15)
        Me.TemplatesLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TemplatesLabel.Name = "TemplatesLabel"
        Me.TemplatesLabel.Size = New System.Drawing.Size(168, 17)
        Me.TemplatesLabel.TabIndex = 0
        Me.TemplatesLabel.Text = "User template messages:"
        '
        'Templates
        '
        Me.Templates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Templates.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.DisplayColumn, Me.TemplateColumn, Me.Col3})
        Me.Templates.FullRowSelect = True
        Me.Templates.GridLines = True
        Me.Templates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.Templates.Location = New System.Drawing.Point(8, 34)
        Me.Templates.Margin = New System.Windows.Forms.Padding(4)
        Me.Templates.Name = "Templates"
        Me.Templates.Size = New System.Drawing.Size(664, 264)
        Me.Templates.TabIndex = 1
        Me.Templates.UseCompatibleStateImageBehavior = False
        Me.Templates.View = System.Windows.Forms.View.Details
        '
        'DisplayColumn
        '
        Me.DisplayColumn.Text = "Display text"
        Me.DisplayColumn.Width = 280
        '
        'TemplateColumn
        '
        Me.TemplateColumn.Text = "Template"
        Me.TemplateColumn.Width = 322
        '
        'Col3
        '
        Me.Col3.Text = "Summary for revert"
        Me.Col3.Width = 270
        '
        'EditorTab
        '
        Me.EditorTab.Controls.Add(Me.HighlightGroup)
        Me.EditorTab.Location = New System.Drawing.Point(4, 23)
        Me.EditorTab.Margin = New System.Windows.Forms.Padding(4)
        Me.EditorTab.Name = "EditorTab"
        Me.EditorTab.Padding = New System.Windows.Forms.Padding(4)
        Me.EditorTab.Size = New System.Drawing.Size(684, 415)
        Me.EditorTab.TabIndex = 8
        Me.EditorTab.Text = "Editor"
        Me.EditorTab.UseVisualStyleBackColor = True
        '
        'HighlightGroup
        '
        Me.HighlightGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HighlightGroup.Controls.Add(Me.Label26)
        Me.HighlightGroup.Controls.Add(Me.ColorParamCall)
        Me.HighlightGroup.Controls.Add(Me.Label25)
        Me.HighlightGroup.Controls.Add(Me.ColorHtmlTag)
        Me.HighlightGroup.Controls.Add(Me.Label24)
        Me.HighlightGroup.Controls.Add(Me.ColorTemplate)
        Me.HighlightGroup.Controls.Add(Me.Label30)
        Me.HighlightGroup.Controls.Add(Me.Label23)
        Me.HighlightGroup.Controls.Add(Me.ColorParamName)
        Me.HighlightGroup.Controls.Add(Me.ColorExternalLink)
        Me.HighlightGroup.Controls.Add(Me.Label29)
        Me.HighlightGroup.Controls.Add(Me.Label22)
        Me.HighlightGroup.Controls.Add(Me.ColorParam)
        Me.HighlightGroup.Controls.Add(Me.ColorMagicWord)
        Me.HighlightGroup.Controls.Add(Me.Label28)
        Me.HighlightGroup.Controls.Add(Me.Label21)
        Me.HighlightGroup.Controls.Add(Me.ColorImage)
        Me.HighlightGroup.Controls.Add(Me.ColorLink)
        Me.HighlightGroup.Controls.Add(Me.Label27)
        Me.HighlightGroup.Controls.Add(Me.ColorReference)
        Me.HighlightGroup.Controls.Add(Me.Label20)
        Me.HighlightGroup.Controls.Add(Me.ColorComment)
        Me.HighlightGroup.Location = New System.Drawing.Point(8, 7)
        Me.HighlightGroup.Margin = New System.Windows.Forms.Padding(4)
        Me.HighlightGroup.Name = "HighlightGroup"
        Me.HighlightGroup.Padding = New System.Windows.Forms.Padding(4)
        Me.HighlightGroup.Size = New System.Drawing.Size(665, 343)
        Me.HighlightGroup.TabIndex = 0
        Me.HighlightGroup.TabStop = False
        Me.HighlightGroup.Text = "Syntax highlight colors"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(269, 39)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(99, 17)
        Me.Label26.TabIndex = 12
        Me.Label26.Text = "Parameter call"
        '
        'ColorParamCall
        '
        Me.ColorParamCall.BackColor = System.Drawing.Color.Black
        Me.ColorParamCall.Location = New System.Drawing.Point(412, 33)
        Me.ColorParamCall.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorParamCall.Name = "ColorParamCall"
        Me.ColorParamCall.Size = New System.Drawing.Size(59, 28)
        Me.ColorParamCall.TabIndex = 13
        Me.ColorParamCall.UseVisualStyleBackColor = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(24, 218)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(70, 17)
        Me.Label25.TabIndex = 10
        Me.Label25.Text = "HTML tag"
        '
        'ColorHtmlTag
        '
        Me.ColorHtmlTag.BackColor = System.Drawing.Color.Black
        Me.ColorHtmlTag.Location = New System.Drawing.Point(167, 212)
        Me.ColorHtmlTag.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorHtmlTag.Name = "ColorHtmlTag"
        Me.ColorHtmlTag.Size = New System.Drawing.Size(59, 28)
        Me.ColorHtmlTag.TabIndex = 11
        Me.ColorHtmlTag.UseVisualStyleBackColor = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(24, 182)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(67, 17)
        Me.Label24.TabIndex = 8
        Me.Label24.Text = "Template"
        '
        'ColorTemplate
        '
        Me.ColorTemplate.BackColor = System.Drawing.Color.Black
        Me.ColorTemplate.Location = New System.Drawing.Point(167, 176)
        Me.ColorTemplate.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorTemplate.Name = "ColorTemplate"
        Me.ColorTemplate.Size = New System.Drawing.Size(59, 28)
        Me.ColorTemplate.TabIndex = 9
        Me.ColorTemplate.UseVisualStyleBackColor = False
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(269, 75)
        Me.Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(113, 17)
        Me.Label30.TabIndex = 14
        Me.Label30.Text = "Parameter name"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(24, 146)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(84, 17)
        Me.Label23.TabIndex = 6
        Me.Label23.Text = "External link"
        '
        'ColorParamName
        '
        Me.ColorParamName.BackColor = System.Drawing.Color.Black
        Me.ColorParamName.Location = New System.Drawing.Point(412, 69)
        Me.ColorParamName.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorParamName.Name = "ColorParamName"
        Me.ColorParamName.Size = New System.Drawing.Size(59, 28)
        Me.ColorParamName.TabIndex = 15
        Me.ColorParamName.UseVisualStyleBackColor = False
        '
        'ColorExternalLink
        '
        Me.ColorExternalLink.BackColor = System.Drawing.Color.Black
        Me.ColorExternalLink.Location = New System.Drawing.Point(167, 140)
        Me.ColorExternalLink.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorExternalLink.Name = "ColorExternalLink"
        Me.ColorExternalLink.Size = New System.Drawing.Size(59, 28)
        Me.ColorExternalLink.TabIndex = 7
        Me.ColorExternalLink.UseVisualStyleBackColor = False
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(269, 182)
        Me.Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(136, 17)
        Me.Label29.TabIndex = 20
        Me.Label29.Text = "Template parameter"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(24, 111)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(79, 17)
        Me.Label22.TabIndex = 4
        Me.Label22.Text = "Magic word"
        '
        'ColorParam
        '
        Me.ColorParam.BackColor = System.Drawing.Color.Black
        Me.ColorParam.Location = New System.Drawing.Point(412, 176)
        Me.ColorParam.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorParam.Name = "ColorParam"
        Me.ColorParam.Size = New System.Drawing.Size(59, 28)
        Me.ColorParam.TabIndex = 21
        Me.ColorParam.UseVisualStyleBackColor = False
        '
        'ColorMagicWord
        '
        Me.ColorMagicWord.BackColor = System.Drawing.Color.Black
        Me.ColorMagicWord.Location = New System.Drawing.Point(167, 105)
        Me.ColorMagicWord.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorMagicWord.Name = "ColorMagicWord"
        Me.ColorMagicWord.Size = New System.Drawing.Size(59, 28)
        Me.ColorMagicWord.TabIndex = 5
        Me.ColorMagicWord.UseVisualStyleBackColor = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(269, 146)
        Me.Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(46, 17)
        Me.Label28.TabIndex = 18
        Me.Label28.Text = "Image"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(24, 75)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(34, 17)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "Link"
        '
        'ColorImage
        '
        Me.ColorImage.BackColor = System.Drawing.Color.Black
        Me.ColorImage.Location = New System.Drawing.Point(412, 140)
        Me.ColorImage.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorImage.Name = "ColorImage"
        Me.ColorImage.Size = New System.Drawing.Size(59, 28)
        Me.ColorImage.TabIndex = 19
        Me.ColorImage.UseVisualStyleBackColor = False
        '
        'ColorLink
        '
        Me.ColorLink.BackColor = System.Drawing.Color.Black
        Me.ColorLink.Location = New System.Drawing.Point(167, 69)
        Me.ColorLink.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorLink.Name = "ColorLink"
        Me.ColorLink.Size = New System.Drawing.Size(59, 28)
        Me.ColorLink.TabIndex = 3
        Me.ColorLink.UseVisualStyleBackColor = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(269, 111)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(74, 17)
        Me.Label27.TabIndex = 16
        Me.Label27.Text = "Reference"
        '
        'ColorReference
        '
        Me.ColorReference.BackColor = System.Drawing.Color.Black
        Me.ColorReference.Location = New System.Drawing.Point(412, 105)
        Me.ColorReference.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorReference.Name = "ColorReference"
        Me.ColorReference.Size = New System.Drawing.Size(59, 28)
        Me.ColorReference.TabIndex = 17
        Me.ColorReference.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(24, 39)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(67, 17)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Comment"
        '
        'ColorComment
        '
        Me.ColorComment.BackColor = System.Drawing.Color.Black
        Me.ColorComment.Location = New System.Drawing.Point(167, 33)
        Me.ColorComment.Margin = New System.Windows.Forms.Padding(4)
        Me.ColorComment.Name = "ColorComment"
        Me.ColorComment.Size = New System.Drawing.Size(59, 28)
        Me.ColorComment.TabIndex = 1
        Me.ColorComment.UseVisualStyleBackColor = False
        '
        'AdminTab
        '
        Me.AdminTab.Controls.Add(Me.WatchDelete)
        Me.AdminTab.Controls.Add(Me.BlockTime)
        Me.AdminTab.Controls.Add(Me.BlockTimeRegLabel)
        Me.AdminTab.Controls.Add(Me.BlockTimeAnonLabel)
        Me.AdminTab.Controls.Add(Me.BlockTimeLabel)
        Me.AdminTab.Controls.Add(Me.BlockTimeAnon)
        Me.AdminTab.Controls.Add(Me.BlockReasonLabel)
        Me.AdminTab.Controls.Add(Me.BlockReason)
        Me.AdminTab.Controls.Add(Me.PromptForBlock)
        Me.AdminTab.Controls.Add(Me.UseAdminFunctions)
        Me.AdminTab.Location = New System.Drawing.Point(4, 23)
        Me.AdminTab.Margin = New System.Windows.Forms.Padding(4)
        Me.AdminTab.Name = "AdminTab"
        Me.AdminTab.Size = New System.Drawing.Size(684, 415)
        Me.AdminTab.TabIndex = 1
        Me.AdminTab.Text = "Admin"
        Me.AdminTab.UseVisualStyleBackColor = True
        '
        'WatchDelete
        '
        Me.WatchDelete.AutoSize = True
        Me.WatchDelete.Location = New System.Drawing.Point(12, 218)
        Me.WatchDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.WatchDelete.Name = "WatchDelete"
        Me.WatchDelete.Size = New System.Drawing.Size(222, 21)
        Me.WatchDelete.TabIndex = 9
        Me.WatchDelete.Text = "Add deleted pages to watchlist"
        Me.WatchDelete.UseVisualStyleBackColor = True
        Me.WatchDelete.Visible = False
        '
        'BlockTime
        '
        Me.BlockTime.Location = New System.Drawing.Point(217, 172)
        Me.BlockTime.Margin = New System.Windows.Forms.Padding(4)
        Me.BlockTime.Name = "BlockTime"
        Me.BlockTime.Size = New System.Drawing.Size(132, 22)
        Me.BlockTime.TabIndex = 8
        '
        'BlockTimeRegLabel
        '
        Me.BlockTimeRegLabel.Location = New System.Drawing.Point(3, 176)
        Me.BlockTimeRegLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.BlockTimeRegLabel.Name = "BlockTimeRegLabel"
        Me.BlockTimeRegLabel.Size = New System.Drawing.Size(212, 21)
        Me.BlockTimeRegLabel.TabIndex = 7
        Me.BlockTimeRegLabel.Text = "registered users:"
        Me.BlockTimeRegLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BlockTimeAnonLabel
        '
        Me.BlockTimeAnonLabel.Location = New System.Drawing.Point(0, 149)
        Me.BlockTimeAnonLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.BlockTimeAnonLabel.Name = "BlockTimeAnonLabel"
        Me.BlockTimeAnonLabel.Size = New System.Drawing.Size(215, 21)
        Me.BlockTimeAnonLabel.TabIndex = 5
        Me.BlockTimeAnonLabel.Text = "anonymous users:"
        Me.BlockTimeAnonLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BlockTimeLabel
        '
        Me.BlockTimeLabel.AutoSize = True
        Me.BlockTimeLabel.Location = New System.Drawing.Point(8, 121)
        Me.BlockTimeLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.BlockTimeLabel.Name = "BlockTimeLabel"
        Me.BlockTimeLabel.Size = New System.Drawing.Size(167, 17)
        Me.BlockTimeLabel.TabIndex = 4
        Me.BlockTimeLabel.Text = "Default block duration for"
        '
        'BlockTimeAnon
        '
        Me.BlockTimeAnon.Location = New System.Drawing.Point(217, 145)
        Me.BlockTimeAnon.Margin = New System.Windows.Forms.Padding(4)
        Me.BlockTimeAnon.Name = "BlockTimeAnon"
        Me.BlockTimeAnon.Size = New System.Drawing.Size(132, 22)
        Me.BlockTimeAnon.TabIndex = 6
        '
        'BlockReasonLabel
        '
        Me.BlockReasonLabel.Location = New System.Drawing.Point(3, 82)
        Me.BlockReasonLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.BlockReasonLabel.Name = "BlockReasonLabel"
        Me.BlockReasonLabel.Size = New System.Drawing.Size(212, 21)
        Me.BlockReasonLabel.TabIndex = 2
        Me.BlockReasonLabel.Text = "Default block reason:"
        Me.BlockReasonLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BlockReason
        '
        Me.BlockReason.Location = New System.Drawing.Point(217, 79)
        Me.BlockReason.Margin = New System.Windows.Forms.Padding(4)
        Me.BlockReason.Name = "BlockReason"
        Me.BlockReason.Size = New System.Drawing.Size(259, 22)
        Me.BlockReason.TabIndex = 3
        '
        'PromptForBlock
        '
        Me.PromptForBlock.AutoSize = True
        Me.PromptForBlock.Location = New System.Drawing.Point(12, 47)
        Me.PromptForBlock.Margin = New System.Windows.Forms.Padding(4)
        Me.PromptForBlock.Name = "PromptForBlock"
        Me.PromptForBlock.Size = New System.Drawing.Size(403, 21)
        Me.PromptForBlock.TabIndex = 1
        Me.PromptForBlock.Text = "Prompt for block if asked to warn a user with a final warning"
        Me.PromptForBlock.UseVisualStyleBackColor = True
        '
        'UseAdminFunctions
        '
        Me.UseAdminFunctions.AutoSize = True
        Me.UseAdminFunctions.Location = New System.Drawing.Point(12, 18)
        Me.UseAdminFunctions.Margin = New System.Windows.Forms.Padding(4)
        Me.UseAdminFunctions.Name = "UseAdminFunctions"
        Me.UseAdminFunctions.Size = New System.Drawing.Size(273, 21)
        Me.UseAdminFunctions.TabIndex = 0
        Me.UseAdminFunctions.Text = "Use administrator functions if available"
        Me.UseAdminFunctions.UseVisualStyleBackColor = True
        '
        'ViewLocalConfig
        '
        Me.ViewLocalConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ViewLocalConfig.AutoSize = True
        Me.ViewLocalConfig.Location = New System.Drawing.Point(9, 468)
        Me.ViewLocalConfig.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ViewLocalConfig.Name = "ViewLocalConfig"
        Me.ViewLocalConfig.Size = New System.Drawing.Size(196, 17)
        Me.ViewLocalConfig.TabIndex = 1
        Me.ViewLocalConfig.TabStop = True
        Me.ViewLocalConfig.Text = "View local configuration folder"
        '
        'ConfigForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 502)
        Me.Controls.Add(Me.ViewLocalConfig)
        Me.Controls.Add(Me.Tabs)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "ConfigForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Options"
        Me.Tabs.ResumeLayout(False)
        Me.GeneralTab.ResumeLayout(False)
        Me.GeneralTab.PerformLayout()
        CType(Me.DiffFontSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PreloadCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.InterfaceTab.ResumeLayout(False)
        Me.InterfaceTab.PerformLayout()
        Me.KeyboardTab.ResumeLayout(False)
        Me.KeyboardTab.PerformLayout()
        Me.EditingTab.ResumeLayout(False)
        Me.EditingTab.PerformLayout()
        Me.RevertTab.ResumeLayout(False)
        Me.RevertTab.PerformLayout()
        Me.ReportingTab.ResumeLayout(False)
        Me.ReportingTab.PerformLayout()
        Me.AutoReportGroup.ResumeLayout(False)
        Me.AutoReportGroup.PerformLayout()
        Me.TemplatesTab.ResumeLayout(False)
        Me.TemplatesTab.PerformLayout()
        Me.EditorTab.ResumeLayout(False)
        Me.HighlightGroup.ResumeLayout(False)
        Me.HighlightGroup.PerformLayout()
        Me.AdminTab.ResumeLayout(False)
        Me.AdminTab.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents GeneralTab As System.Windows.Forms.TabPage
    Friend WithEvents AdminTab As System.Windows.Forms.TabPage
    Friend WithEvents EditingTab As System.Windows.Forms.TabPage
    Friend WithEvents WatchlistLabel As System.Windows.Forms.Label
    Friend WithEvents MinorLabel As System.Windows.Forms.Label
    Friend WithEvents Watchlist As System.Windows.Forms.CheckedListBox
    Friend WithEvents Minor As System.Windows.Forms.CheckedListBox
    Friend WithEvents ReportingTab As System.Windows.Forms.TabPage
    Friend WithEvents AutoReportGroup As System.Windows.Forms.GroupBox
    Friend WithEvents ReportAuto As System.Windows.Forms.RadioButton
    Friend WithEvents AutoReportLabel As System.Windows.Forms.Label
    Friend WithEvents ReportNone As System.Windows.Forms.RadioButton
    Friend WithEvents ReportPrompt As System.Windows.Forms.RadioButton
    Friend WithEvents ExtendReports As System.Windows.Forms.CheckBox
    Friend WithEvents ReportLinkExamples As System.Windows.Forms.CheckBox
    Friend WithEvents AutoWhitelist As System.Windows.Forms.CheckBox
    Friend WithEvents Preloads As System.Windows.Forms.CheckBox
    Friend WithEvents IrcPortLabel As System.Windows.Forms.Label
    Friend WithEvents IrcPort As System.Windows.Forms.TextBox
    Friend WithEvents ShowNewEdits As System.Windows.Forms.CheckBox
    Friend WithEvents PromptForBlock As System.Windows.Forms.CheckBox
    Friend WithEvents UseAdminFunctions As System.Windows.Forms.CheckBox
    Friend WithEvents RevertTab As System.Windows.Forms.TabPage
    Friend WithEvents AddSummary As System.Windows.Forms.Button
    Friend WithEvents RemoveSummary As System.Windows.Forms.Button
    Friend WithEvents RevertSummariesLabel As System.Windows.Forms.Label
    Friend WithEvents RevertSummaries As System.Windows.Forms.ListBox
    Friend WithEvents UseRollback As System.Windows.Forms.CheckBox
    Friend WithEvents AutoAdvance As System.Windows.Forms.CheckBox
    Friend WithEvents ConfirmSame As System.Windows.Forms.CheckBox
    Friend WithEvents ConfirmMultiple As System.Windows.Forms.CheckBox
    Friend WithEvents BlockTimeRegLabel As System.Windows.Forms.Label
    Friend WithEvents BlockTimeAnonLabel As System.Windows.Forms.Label
    Friend WithEvents BlockTimeLabel As System.Windows.Forms.Label
    Friend WithEvents BlockTimeAnon As System.Windows.Forms.TextBox
    Friend WithEvents BlockReasonLabel As System.Windows.Forms.Label
    Friend WithEvents BlockReason As System.Windows.Forms.TextBox
    Friend WithEvents BlockTime As System.Windows.Forms.TextBox
    Friend WithEvents TemplatesTab As System.Windows.Forms.TabPage
    Friend WithEvents DisplayColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents TemplateColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents AddTemplate As System.Windows.Forms.Button
    Friend WithEvents RemoveTemplate As System.Windows.Forms.Button
    Friend WithEvents TemplatesLabel As System.Windows.Forms.Label
    Friend WithEvents Templates As System.Windows.Forms.ListView
    Friend WithEvents DiffFontSizeLabel As System.Windows.Forms.Label
    Friend WithEvents DefaultSummary As System.Windows.Forms.TextBox
    Friend WithEvents DefaultSummaryLabel As System.Windows.Forms.Label
    Friend WithEvents UndoSummary As System.Windows.Forms.TextBox
    Friend WithEvents UndoSummaryLabel As System.Windows.Forms.Label
    Friend WithEvents OpenInBrowser As System.Windows.Forms.CheckBox
    Friend WithEvents LogFileBrowse As System.Windows.Forms.Button
    Friend WithEvents LogFile As System.Windows.Forms.TextBox
    Friend WithEvents LogFileLabel As System.Windows.Forms.Label
    Friend WithEvents KeyboardTab As System.Windows.Forms.TabPage
    Friend WithEvents ShortcutListLabel As System.Windows.Forms.Label
    Friend WithEvents ShortcutList As System.Windows.Forms.ListView
    Friend WithEvents ActionColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents ShortcutColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents ChangeShortcut As System.Windows.Forms.TextBox
    Friend WithEvents ChangeShortcutLabel As System.Windows.Forms.Label
    Friend WithEvents NoShortcut As System.Windows.Forms.Button
    Friend WithEvents Defaults As System.Windows.Forms.Button
    Friend WithEvents ConfirmSelfRevert As System.Windows.Forms.CheckBox
    Friend WithEvents ClearSummaries As System.Windows.Forms.Button
    Friend WithEvents ClearSummariesLabel As System.Windows.Forms.Label
    Friend WithEvents EditorTab As System.Windows.Forms.TabPage
    Friend WithEvents HighlightGroup As System.Windows.Forms.GroupBox
    Friend WithEvents ColorComment As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents ColorParamCall As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents ColorHtmlTag As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents ColorTemplate As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents ColorExternalLink As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents ColorMagicWord As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents ColorLink As System.Windows.Forms.Button
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents ColorParamName As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents ColorParam As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents ColorImage As System.Windows.Forms.Button
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents ColorReference As System.Windows.Forms.Button
    Friend WithEvents IrcMode As System.Windows.Forms.CheckBox
    Friend WithEvents InterfaceTab As System.Windows.Forms.TabPage
    Friend WithEvents ShowQueue As System.Windows.Forms.CheckBox
    Friend WithEvents ShowLog As System.Windows.Forms.CheckBox
    Friend WithEvents ShowToolTips As System.Windows.Forms.CheckBox
    Friend WithEvents TrayIcon As System.Windows.Forms.CheckBox
    Friend WithEvents RightAlignQueue As System.Windows.Forms.CheckBox
    Friend WithEvents PreloadCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents DiffFontSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents RememberPassword As System.Windows.Forms.CheckBox
    Friend WithEvents RememberMe As System.Windows.Forms.CheckBox
    Friend WithEvents ViewLocalConfig As System.Windows.Forms.LinkLabel
    Friend WithEvents ConfirmRange As System.Windows.Forms.CheckBox
    Friend WithEvents ConfirmWarned As System.Windows.Forms.CheckBox
    Friend WithEvents WatchDelete As System.Windows.Forms.CheckBox
    Friend WithEvents ShowNewMessages As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ShowTwoQueues As System.Windows.Forms.CheckBox
    Friend WithEvents ConfirmPage As System.Windows.Forms.CheckBox
    Friend WithEvents UseCustom As System.Windows.Forms.CheckBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker3 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btImport As System.Windows.Forms.Button
    Friend WithEvents Col3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents History As System.Windows.Forms.CheckBox
End Class
