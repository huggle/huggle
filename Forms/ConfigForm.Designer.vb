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
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.GeneralTab = New System.Windows.Forms.TabPage
        Me.LogFileBrowse = New System.Windows.Forms.Button
        Me.LogFile = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.OpenInBrowser = New System.Windows.Forms.CheckBox
        Me.ShowToolTips = New System.Windows.Forms.CheckBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.DiffFontSize = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.ShowNewEdits = New System.Windows.Forms.CheckBox
        Me.ShowQueue = New System.Windows.Forms.CheckBox
        Me.Preloading = New System.Windows.Forms.CheckBox
        Me.Preloads = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.IrcPort = New System.Windows.Forms.TextBox
        Me.AutoWhitelist = New System.Windows.Forms.CheckBox
        Me.TrayIcon = New System.Windows.Forms.CheckBox
        Me.KeyboardTab = New System.Windows.Forms.TabPage
        Me.Defaults = New System.Windows.Forms.Button
        Me.NoShortcut = New System.Windows.Forms.Button
        Me.ChangeShortcut = New System.Windows.Forms.TextBox
        Me.ChangeShortcutLabel = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.ShortcutList = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.QueueTab = New System.Windows.Forms.TabPage
        Me.QueueMaxAge = New System.Windows.Forms.NumericUpDown
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.ShowNewPages = New System.Windows.Forms.CheckBox
        Me.Namespaces = New System.Windows.Forms.CheckedListBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.ShowAnonymous = New System.Windows.Forms.CheckBox
        Me.ShowRegistered = New System.Windows.Forms.CheckBox
        Me.EditingTab = New System.Windows.Forms.TabPage
        Me.UndoSummary = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.DefaultSummary = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Watchlist = New System.Windows.Forms.CheckedListBox
        Me.Minor = New System.Windows.Forms.CheckedListBox
        Me.RevertTab = New System.Windows.Forms.TabPage
        Me.ClearRevertSummaries = New System.Windows.Forms.Button
        Me.Label19 = New System.Windows.Forms.Label
        Me.ConfirmSelfRevert = New System.Windows.Forms.CheckBox
        Me.AddSummary = New System.Windows.Forms.Button
        Me.RemoveSummary = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.RevertSummaries = New System.Windows.Forms.ListBox
        Me.UseRollback = New System.Windows.Forms.CheckBox
        Me.AutoAdvance = New System.Windows.Forms.CheckBox
        Me.ConfirmSame = New System.Windows.Forms.CheckBox
        Me.ConfirmMultiple = New System.Windows.Forms.CheckBox
        Me.ReportingTab = New System.Windows.Forms.TabPage
        Me.ExtendReports = New System.Windows.Forms.CheckBox
        Me.ReportLinkExamples = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ReportAuto = New System.Windows.Forms.RadioButton
        Me.Label3 = New System.Windows.Forms.Label
        Me.ReportNone = New System.Windows.Forms.RadioButton
        Me.ReportPrompt = New System.Windows.Forms.RadioButton
        Me.TemplatesTab = New System.Windows.Forms.TabPage
        Me.AddTemplate = New System.Windows.Forms.Button
        Me.RemoveTemplate = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.Templates = New System.Windows.Forms.ListView
        Me.DisplayColumn = New System.Windows.Forms.ColumnHeader
        Me.TemplateColumn = New System.Windows.Forms.ColumnHeader
        Me.EditorTab = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.ColorParamCall = New System.Windows.Forms.Button
        Me.Label25 = New System.Windows.Forms.Label
        Me.ColorHtmlTag = New System.Windows.Forms.Button
        Me.Label24 = New System.Windows.Forms.Label
        Me.ColorTemplate = New System.Windows.Forms.Button
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.ColorParamName = New System.Windows.Forms.Button
        Me.ColorExternalLink = New System.Windows.Forms.Button
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.ColorParam = New System.Windows.Forms.Button
        Me.ColorMagicWord = New System.Windows.Forms.Button
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.ColorImage = New System.Windows.Forms.Button
        Me.ColorLink = New System.Windows.Forms.Button
        Me.Label27 = New System.Windows.Forms.Label
        Me.ColorReference = New System.Windows.Forms.Button
        Me.Label20 = New System.Windows.Forms.Label
        Me.ColorComment = New System.Windows.Forms.Button
        Me.AdminTab = New System.Windows.Forms.TabPage
        Me.BlockTime = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.BlockTimeAnon = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.BlockReason = New System.Windows.Forms.TextBox
        Me.PromptForBlock = New System.Windows.Forms.CheckBox
        Me.UseAdminFunctions = New System.Windows.Forms.CheckBox
        Me.Tabs.SuspendLayout()
        Me.GeneralTab.SuspendLayout()
        Me.KeyboardTab.SuspendLayout()
        Me.QueueTab.SuspendLayout()
        CType(Me.QueueMaxAge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.EditingTab.SuspendLayout()
        Me.RevertTab.SuspendLayout()
        Me.ReportingTab.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TemplatesTab.SuspendLayout()
        Me.EditorTab.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.AdminTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(439, 326)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 1
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(358, 326)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 0
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Tabs
        '
        Me.Tabs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Controls.Add(Me.GeneralTab)
        Me.Tabs.Controls.Add(Me.KeyboardTab)
        Me.Tabs.Controls.Add(Me.QueueTab)
        Me.Tabs.Controls.Add(Me.EditingTab)
        Me.Tabs.Controls.Add(Me.RevertTab)
        Me.Tabs.Controls.Add(Me.ReportingTab)
        Me.Tabs.Controls.Add(Me.TemplatesTab)
        Me.Tabs.Controls.Add(Me.EditorTab)
        Me.Tabs.Controls.Add(Me.AdminTab)
        Me.Tabs.ItemSize = New System.Drawing.Size(49, 19)
        Me.Tabs.Location = New System.Drawing.Point(6, 10)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(519, 318)
        Me.Tabs.TabIndex = 2
        '
        'GeneralTab
        '
        Me.GeneralTab.Controls.Add(Me.LogFileBrowse)
        Me.GeneralTab.Controls.Add(Me.LogFile)
        Me.GeneralTab.Controls.Add(Me.Label17)
        Me.GeneralTab.Controls.Add(Me.OpenInBrowser)
        Me.GeneralTab.Controls.Add(Me.ShowToolTips)
        Me.GeneralTab.Controls.Add(Me.Label14)
        Me.GeneralTab.Controls.Add(Me.DiffFontSize)
        Me.GeneralTab.Controls.Add(Me.Label13)
        Me.GeneralTab.Controls.Add(Me.ShowNewEdits)
        Me.GeneralTab.Controls.Add(Me.ShowQueue)
        Me.GeneralTab.Controls.Add(Me.Preloading)
        Me.GeneralTab.Controls.Add(Me.Preloads)
        Me.GeneralTab.Controls.Add(Me.Label6)
        Me.GeneralTab.Controls.Add(Me.IrcPort)
        Me.GeneralTab.Controls.Add(Me.AutoWhitelist)
        Me.GeneralTab.Controls.Add(Me.TrayIcon)
        Me.GeneralTab.Location = New System.Drawing.Point(4, 23)
        Me.GeneralTab.Name = "GeneralTab"
        Me.GeneralTab.Padding = New System.Windows.Forms.Padding(3)
        Me.GeneralTab.Size = New System.Drawing.Size(511, 291)
        Me.GeneralTab.TabIndex = 0
        Me.GeneralTab.Text = "General"
        Me.GeneralTab.UseVisualStyleBackColor = True
        '
        'LogFileBrowse
        '
        Me.LogFileBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogFileBrowse.Location = New System.Drawing.Point(420, 240)
        Me.LogFileBrowse.Name = "LogFileBrowse"
        Me.LogFileBrowse.Size = New System.Drawing.Size(75, 23)
        Me.LogFileBrowse.TabIndex = 29
        Me.LogFileBrowse.Text = "Browse..."
        Me.LogFileBrowse.UseVisualStyleBackColor = True
        '
        'LogFile
        '
        Me.LogFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogFile.Location = New System.Drawing.Point(56, 242)
        Me.LogFile.Name = "LogFile"
        Me.LogFile.Size = New System.Drawing.Size(358, 20)
        Me.LogFile.TabIndex = 28
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 245)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(44, 13)
        Me.Label17.TabIndex = 27
        Me.Label17.Text = "Log file:"
        '
        'OpenInBrowser
        '
        Me.OpenInBrowser.AutoSize = True
        Me.OpenInBrowser.Location = New System.Drawing.Point(9, 107)
        Me.OpenInBrowser.Name = "OpenInBrowser"
        Me.OpenInBrowser.Size = New System.Drawing.Size(229, 17)
        Me.OpenInBrowser.TabIndex = 26
        Me.OpenInBrowser.Text = "Open browser links in new browser window"
        Me.OpenInBrowser.UseVisualStyleBackColor = True
        '
        'ShowToolTips
        '
        Me.ShowToolTips.AutoSize = True
        Me.ShowToolTips.Location = New System.Drawing.Point(9, 84)
        Me.ShowToolTips.Name = "ShowToolTips"
        Me.ShowToolTips.Size = New System.Drawing.Size(138, 17)
        Me.ShowToolTips.TabIndex = 25
        Me.ShowToolTips.Text = "Show tooltips on menus"
        Me.ShowToolTips.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(127, 208)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(16, 13)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "pt"
        '
        'DiffFontSize
        '
        Me.DiffFontSize.Location = New System.Drawing.Point(73, 205)
        Me.DiffFontSize.Name = "DiffFontSize"
        Me.DiffFontSize.Size = New System.Drawing.Size(48, 20)
        Me.DiffFontSize.TabIndex = 23
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 208)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 13)
        Me.Label13.TabIndex = 22
        Me.Label13.Text = "Diff font size:"
        '
        'ShowNewEdits
        '
        Me.ShowNewEdits.AutoSize = True
        Me.ShowNewEdits.Location = New System.Drawing.Point(9, 130)
        Me.ShowNewEdits.Name = "ShowNewEdits"
        Me.ShowNewEdits.Size = New System.Drawing.Size(285, 17)
        Me.ShowNewEdits.TabIndex = 21
        Me.ShowNewEdits.Text = "Show new edits to the selected page as they are made"
        Me.ShowNewEdits.UseVisualStyleBackColor = True
        '
        'ShowQueue
        '
        Me.ShowQueue.AutoSize = True
        Me.ShowQueue.Location = New System.Drawing.Point(9, 61)
        Me.ShowQueue.Name = "ShowQueue"
        Me.ShowQueue.Size = New System.Drawing.Size(125, 17)
        Me.ShowQueue.TabIndex = 16
        Me.ShowQueue.Text = "Show revision queue"
        Me.ShowQueue.UseVisualStyleBackColor = True
        '
        'Preloading
        '
        Me.Preloading.AutoSize = True
        Me.Preloading.Location = New System.Drawing.Point(9, 153)
        Me.Preloading.Name = "Preloading"
        Me.Preloading.Size = New System.Drawing.Size(178, 17)
        Me.Preloading.TabIndex = 9
        Me.Preloading.Text = "Enable preloading of diffs (1 - 5):"
        Me.Preloading.UseVisualStyleBackColor = True
        '
        'Preloads
        '
        Me.Preloads.Location = New System.Drawing.Point(191, 151)
        Me.Preloads.Name = "Preloads"
        Me.Preloads.Size = New System.Drawing.Size(48, 20)
        Me.Preloads.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 182)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(115, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "IRC port (6664 - 6669):"
        '
        'IrcPort
        '
        Me.IrcPort.Location = New System.Drawing.Point(127, 179)
        Me.IrcPort.Name = "IrcPort"
        Me.IrcPort.Size = New System.Drawing.Size(48, 20)
        Me.IrcPort.TabIndex = 5
        '
        'AutoWhitelist
        '
        Me.AutoWhitelist.AutoSize = True
        Me.AutoWhitelist.Location = New System.Drawing.Point(9, 15)
        Me.AutoWhitelist.Name = "AutoWhitelist"
        Me.AutoWhitelist.Size = New System.Drawing.Size(156, 17)
        Me.AutoWhitelist.TabIndex = 4
        Me.AutoWhitelist.Text = "Automatically whitelist users"
        Me.AutoWhitelist.UseVisualStyleBackColor = True
        '
        'TrayIcon
        '
        Me.TrayIcon.AutoSize = True
        Me.TrayIcon.Location = New System.Drawing.Point(9, 38)
        Me.TrayIcon.Name = "TrayIcon"
        Me.TrayIcon.Size = New System.Drawing.Size(96, 17)
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
        Me.KeyboardTab.Controls.Add(Me.Label18)
        Me.KeyboardTab.Controls.Add(Me.ShortcutList)
        Me.KeyboardTab.Location = New System.Drawing.Point(4, 23)
        Me.KeyboardTab.Name = "KeyboardTab"
        Me.KeyboardTab.Padding = New System.Windows.Forms.Padding(3)
        Me.KeyboardTab.Size = New System.Drawing.Size(511, 291)
        Me.KeyboardTab.TabIndex = 7
        Me.KeyboardTab.Text = "Keyboard"
        Me.KeyboardTab.UseVisualStyleBackColor = True
        '
        'Defaults
        '
        Me.Defaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Defaults.Location = New System.Drawing.Point(430, 237)
        Me.Defaults.Name = "Defaults"
        Me.Defaults.Size = New System.Drawing.Size(75, 23)
        Me.Defaults.TabIndex = 5
        Me.Defaults.Text = "Defaults"
        Me.Defaults.UseVisualStyleBackColor = True
        '
        'NoShortcut
        '
        Me.NoShortcut.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NoShortcut.Location = New System.Drawing.Point(368, 237)
        Me.NoShortcut.Name = "NoShortcut"
        Me.NoShortcut.Size = New System.Drawing.Size(56, 23)
        Me.NoShortcut.TabIndex = 4
        Me.NoShortcut.Text = "None"
        Me.NoShortcut.UseVisualStyleBackColor = True
        Me.NoShortcut.Visible = False
        '
        'ChangeShortcut
        '
        Me.ChangeShortcut.AcceptsReturn = True
        Me.ChangeShortcut.AcceptsTab = True
        Me.ChangeShortcut.Location = New System.Drawing.Point(286, 239)
        Me.ChangeShortcut.Multiline = True
        Me.ChangeShortcut.Name = "ChangeShortcut"
        Me.ChangeShortcut.Size = New System.Drawing.Size(76, 20)
        Me.ChangeShortcut.TabIndex = 3
        Me.ChangeShortcut.TabStop = False
        Me.ChangeShortcut.Visible = False
        '
        'ChangeShortcutLabel
        '
        Me.ChangeShortcutLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ChangeShortcutLabel.AutoSize = True
        Me.ChangeShortcutLabel.Location = New System.Drawing.Point(3, 237)
        Me.ChangeShortcutLabel.Name = "ChangeShortcutLabel"
        Me.ChangeShortcutLabel.Size = New System.Drawing.Size(106, 13)
        Me.ChangeShortcutLabel.TabIndex = 2
        Me.ChangeShortcutLabel.Text = "Change shortcut for :"
        Me.ChangeShortcutLabel.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 15)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(101, 13)
        Me.Label18.TabIndex = 1
        Me.Label18.Text = "Keyboard shortcuts:"
        '
        'ShortcutList
        '
        Me.ShortcutList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShortcutList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ShortcutList.FullRowSelect = True
        Me.ShortcutList.GridLines = True
        Me.ShortcutList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ShortcutList.HideSelection = False
        Me.ShortcutList.Location = New System.Drawing.Point(6, 31)
        Me.ShortcutList.MultiSelect = False
        Me.ShortcutList.Name = "ShortcutList"
        Me.ShortcutList.Size = New System.Drawing.Size(499, 191)
        Me.ShortcutList.TabIndex = 0
        Me.ShortcutList.UseCompatibleStateImageBehavior = False
        Me.ShortcutList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Action"
        Me.ColumnHeader1.Width = 244
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Shortcut"
        Me.ColumnHeader2.Width = 175
        '
        'QueueTab
        '
        Me.QueueTab.Controls.Add(Me.QueueMaxAge)
        Me.QueueTab.Controls.Add(Me.Label32)
        Me.QueueTab.Controls.Add(Me.Label31)
        Me.QueueTab.Controls.Add(Me.Label5)
        Me.QueueTab.Controls.Add(Me.ShowNewPages)
        Me.QueueTab.Controls.Add(Me.Namespaces)
        Me.QueueTab.Controls.Add(Me.Label4)
        Me.QueueTab.Controls.Add(Me.ShowAnonymous)
        Me.QueueTab.Controls.Add(Me.ShowRegistered)
        Me.QueueTab.Location = New System.Drawing.Point(4, 23)
        Me.QueueTab.Name = "QueueTab"
        Me.QueueTab.Padding = New System.Windows.Forms.Padding(3)
        Me.QueueTab.Size = New System.Drawing.Size(517, 291)
        Me.QueueTab.TabIndex = 4
        Me.QueueTab.Text = "Queue"
        Me.QueueTab.UseVisualStyleBackColor = True
        '
        'QueueMaxAge
        '
        Me.QueueMaxAge.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.QueueMaxAge.Location = New System.Drawing.Point(172, 212)
        Me.QueueMaxAge.Maximum = New Decimal(New Integer() {1440, 0, 0, 0})
        Me.QueueMaxAge.Name = "QueueMaxAge"
        Me.QueueMaxAge.Size = New System.Drawing.Size(54, 20)
        Me.QueueMaxAge.TabIndex = 23
        Me.QueueMaxAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(232, 214)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(106, 13)
        Me.Label32.TabIndex = 22
        Me.Label32.Text = "minutes (0 to disable)"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(9, 214)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(157, 13)
        Me.Label31.TabIndex = 22
        Me.Label31.Text = "Remove queue items older than"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(186, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Show contributions from namespaces:"
        '
        'ShowNewPages
        '
        Me.ShowNewPages.AutoSize = True
        Me.ShowNewPages.Location = New System.Drawing.Point(12, 179)
        Me.ShowNewPages.Name = "ShowNewPages"
        Me.ShowNewPages.Size = New System.Drawing.Size(240, 17)
        Me.ShowNewPages.TabIndex = 20
        Me.ShowNewPages.Text = "Show new pages in ""filtered changes"" queue"
        Me.ShowNewPages.UseVisualStyleBackColor = True
        '
        'Namespaces
        '
        Me.Namespaces.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Namespaces.CheckOnClick = True
        Me.Namespaces.FormattingEnabled = True
        Me.Namespaces.Items.AddRange(New Object() {"Article", "Talk", "User", "User talk", "Wikipedia", "Wikipedia talk", "Category", "Category talk", "Template", "Template talk", "Image", "Image talk", "Help", "Help talk", "MediaWiki", "MediaWiki talk", "Portal", "Portal talk"})
        Me.Namespaces.Location = New System.Drawing.Point(9, 75)
        Me.Namespaces.Name = "Namespaces"
        Me.Namespaces.Size = New System.Drawing.Size(217, 94)
        Me.Namespaces.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(114, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Show contributions by:"
        '
        'ShowAnonymous
        '
        Me.ShowAnonymous.AutoSize = True
        Me.ShowAnonymous.Location = New System.Drawing.Point(12, 29)
        Me.ShowAnonymous.Name = "ShowAnonymous"
        Me.ShowAnonymous.Size = New System.Drawing.Size(108, 17)
        Me.ShowAnonymous.TabIndex = 16
        Me.ShowAnonymous.Text = "anonymous users"
        Me.ShowAnonymous.UseVisualStyleBackColor = True
        '
        'ShowRegistered
        '
        Me.ShowRegistered.AutoSize = True
        Me.ShowRegistered.Location = New System.Drawing.Point(126, 29)
        Me.ShowRegistered.Name = "ShowRegistered"
        Me.ShowRegistered.Size = New System.Drawing.Size(100, 17)
        Me.ShowRegistered.TabIndex = 17
        Me.ShowRegistered.Text = "registered users"
        Me.ShowRegistered.UseVisualStyleBackColor = True
        '
        'EditingTab
        '
        Me.EditingTab.Controls.Add(Me.UndoSummary)
        Me.EditingTab.Controls.Add(Me.Label16)
        Me.EditingTab.Controls.Add(Me.DefaultSummary)
        Me.EditingTab.Controls.Add(Me.Label15)
        Me.EditingTab.Controls.Add(Me.Label2)
        Me.EditingTab.Controls.Add(Me.Label1)
        Me.EditingTab.Controls.Add(Me.Watchlist)
        Me.EditingTab.Controls.Add(Me.Minor)
        Me.EditingTab.Location = New System.Drawing.Point(4, 23)
        Me.EditingTab.Name = "EditingTab"
        Me.EditingTab.Padding = New System.Windows.Forms.Padding(3)
        Me.EditingTab.Size = New System.Drawing.Size(517, 291)
        Me.EditingTab.TabIndex = 2
        Me.EditingTab.Text = "Editing"
        Me.EditingTab.UseVisualStyleBackColor = True
        '
        'UndoSummary
        '
        Me.UndoSummary.Location = New System.Drawing.Point(9, 199)
        Me.UndoSummary.Name = "UndoSummary"
        Me.UndoSummary.Size = New System.Drawing.Size(343, 20)
        Me.UndoSummary.TabIndex = 15
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 183)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(171, 13)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Summary when undoing own edits:"
        '
        'DefaultSummary
        '
        Me.DefaultSummary.Location = New System.Drawing.Point(9, 154)
        Me.DefaultSummary.Name = "DefaultSummary"
        Me.DefaultSummary.Size = New System.Drawing.Size(343, 20)
        Me.DefaultSummary.TabIndex = 13
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 138)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(165, 13)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "Default summary for manual edits:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(167, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Add to watchlist:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Mark as minor:"
        '
        'Watchlist
        '
        Me.Watchlist.FormattingEnabled = True
        Me.Watchlist.Items.AddRange(New Object() {"Reverts", "Warnings", "Tags", "Reports", "Notifications", "Other"})
        Me.Watchlist.Location = New System.Drawing.Point(170, 31)
        Me.Watchlist.Name = "Watchlist"
        Me.Watchlist.Size = New System.Drawing.Size(155, 94)
        Me.Watchlist.TabIndex = 9
        '
        'Minor
        '
        Me.Minor.FormattingEnabled = True
        Me.Minor.Items.AddRange(New Object() {"Reverts", "Warnings", "Tags", "Reports", "Notifications", "Other"})
        Me.Minor.Location = New System.Drawing.Point(9, 31)
        Me.Minor.Name = "Minor"
        Me.Minor.Size = New System.Drawing.Size(155, 94)
        Me.Minor.TabIndex = 8
        '
        'RevertTab
        '
        Me.RevertTab.Controls.Add(Me.ClearRevertSummaries)
        Me.RevertTab.Controls.Add(Me.Label19)
        Me.RevertTab.Controls.Add(Me.ConfirmSelfRevert)
        Me.RevertTab.Controls.Add(Me.AddSummary)
        Me.RevertTab.Controls.Add(Me.RemoveSummary)
        Me.RevertTab.Controls.Add(Me.Label8)
        Me.RevertTab.Controls.Add(Me.RevertSummaries)
        Me.RevertTab.Controls.Add(Me.UseRollback)
        Me.RevertTab.Controls.Add(Me.AutoAdvance)
        Me.RevertTab.Controls.Add(Me.ConfirmSame)
        Me.RevertTab.Controls.Add(Me.ConfirmMultiple)
        Me.RevertTab.Location = New System.Drawing.Point(4, 23)
        Me.RevertTab.Name = "RevertTab"
        Me.RevertTab.Padding = New System.Windows.Forms.Padding(3)
        Me.RevertTab.Size = New System.Drawing.Size(517, 291)
        Me.RevertTab.TabIndex = 5
        Me.RevertTab.Text = "Reverting"
        Me.RevertTab.UseVisualStyleBackColor = True
        '
        'ClearRevertSummaries
        '
        Me.ClearRevertSummaries.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ClearRevertSummaries.Enabled = False
        Me.ClearRevertSummaries.Location = New System.Drawing.Point(433, 257)
        Me.ClearRevertSummaries.Name = "ClearRevertSummaries"
        Me.ClearRevertSummaries.Size = New System.Drawing.Size(75, 23)
        Me.ClearRevertSummaries.TabIndex = 36
        Me.ClearRevertSummaries.Text = "Clear"
        Me.ClearRevertSummaries.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 262)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(395, 13)
        Me.Label19.TabIndex = 35
        Me.Label19.Text = "Summaries entered manually are remembered across sessions; click to clear these:"
        '
        'ConfirmSelfRevert
        '
        Me.ConfirmSelfRevert.AutoSize = True
        Me.ConfirmSelfRevert.Location = New System.Drawing.Point(9, 61)
        Me.ConfirmSelfRevert.Name = "ConfirmSelfRevert"
        Me.ConfirmSelfRevert.Size = New System.Drawing.Size(235, 17)
        Me.ConfirmSelfRevert.TabIndex = 34
        Me.ConfirmSelfRevert.Text = "Confirm reversion of own edits (except undo)"
        Me.ConfirmSelfRevert.UseVisualStyleBackColor = True
        '
        'AddSummary
        '
        Me.AddSummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AddSummary.Location = New System.Drawing.Point(6, 230)
        Me.AddSummary.Name = "AddSummary"
        Me.AddSummary.Size = New System.Drawing.Size(62, 23)
        Me.AddSummary.TabIndex = 33
        Me.AddSummary.Text = "Add"
        Me.AddSummary.UseVisualStyleBackColor = True
        '
        'RemoveSummary
        '
        Me.RemoveSummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RemoveSummary.Enabled = False
        Me.RemoveSummary.Location = New System.Drawing.Point(74, 230)
        Me.RemoveSummary.Name = "RemoveSummary"
        Me.RemoveSummary.Size = New System.Drawing.Size(62, 23)
        Me.RemoveSummary.TabIndex = 32
        Me.RemoveSummary.Text = "Remove"
        Me.RemoveSummary.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 133)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(195, 13)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Reversion summaries available in menu:"
        '
        'RevertSummaries
        '
        Me.RevertSummaries.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RevertSummaries.FormattingEnabled = True
        Me.RevertSummaries.IntegralHeight = False
        Me.RevertSummaries.Location = New System.Drawing.Point(6, 149)
        Me.RevertSummaries.Name = "RevertSummaries"
        Me.RevertSummaries.Size = New System.Drawing.Size(393, 75)
        Me.RevertSummaries.TabIndex = 30
        '
        'UseRollback
        '
        Me.UseRollback.AutoSize = True
        Me.UseRollback.Location = New System.Drawing.Point(9, 107)
        Me.UseRollback.Name = "UseRollback"
        Me.UseRollback.Size = New System.Drawing.Size(138, 17)
        Me.UseRollback.TabIndex = 29
        Me.UseRollback.Text = "Use rollback if available"
        Me.UseRollback.UseVisualStyleBackColor = True
        '
        'AutoAdvance
        '
        Me.AutoAdvance.AutoSize = True
        Me.AutoAdvance.Location = New System.Drawing.Point(9, 84)
        Me.AutoAdvance.Name = "AutoAdvance"
        Me.AutoAdvance.Size = New System.Drawing.Size(259, 17)
        Me.AutoAdvance.TabIndex = 28
        Me.AutoAdvance.Text = "After reverting, move to the next edit in the queue"
        Me.AutoAdvance.UseVisualStyleBackColor = True
        '
        'ConfirmSame
        '
        Me.ConfirmSame.AutoSize = True
        Me.ConfirmSame.Location = New System.Drawing.Point(9, 38)
        Me.ConfirmSame.Name = "ConfirmSame"
        Me.ConfirmSame.Size = New System.Drawing.Size(280, 17)
        Me.ConfirmSame.TabIndex = 27
        Me.ConfirmSame.Text = "Confirm reversion to an edit by the user being reverted"
        Me.ConfirmSame.UseVisualStyleBackColor = True
        '
        'ConfirmMultiple
        '
        Me.ConfirmMultiple.AutoSize = True
        Me.ConfirmMultiple.Location = New System.Drawing.Point(9, 15)
        Me.ConfirmMultiple.Name = "ConfirmMultiple"
        Me.ConfirmMultiple.Size = New System.Drawing.Size(265, 17)
        Me.ConfirmMultiple.TabIndex = 26
        Me.ConfirmMultiple.Text = "Confirm reversion of multiple edits by the same user"
        Me.ConfirmMultiple.UseVisualStyleBackColor = True
        '
        'ReportingTab
        '
        Me.ReportingTab.Controls.Add(Me.ExtendReports)
        Me.ReportingTab.Controls.Add(Me.ReportLinkExamples)
        Me.ReportingTab.Controls.Add(Me.GroupBox1)
        Me.ReportingTab.Location = New System.Drawing.Point(4, 23)
        Me.ReportingTab.Name = "ReportingTab"
        Me.ReportingTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ReportingTab.Size = New System.Drawing.Size(517, 291)
        Me.ReportingTab.TabIndex = 3
        Me.ReportingTab.Text = "Reporting"
        Me.ReportingTab.UseVisualStyleBackColor = True
        '
        'ExtendReports
        '
        Me.ExtendReports.AutoSize = True
        Me.ExtendReports.Location = New System.Drawing.Point(27, 38)
        Me.ExtendReports.Name = "ExtendReports"
        Me.ExtendReports.Size = New System.Drawing.Size(216, 17)
        Me.ExtendReports.TabIndex = 4
        Me.ExtendReports.Text = "Extend reports after additional vandalism"
        Me.ExtendReports.UseVisualStyleBackColor = True
        '
        'ReportLinkExamples
        '
        Me.ReportLinkExamples.AutoSize = True
        Me.ReportLinkExamples.Location = New System.Drawing.Point(9, 15)
        Me.ReportLinkExamples.Name = "ReportLinkExamples"
        Me.ReportLinkExamples.Size = New System.Drawing.Size(253, 17)
        Me.ReportLinkExamples.TabIndex = 3
        Me.ReportLinkExamples.Text = "Include links to instances of vandalism in reports"
        Me.ReportLinkExamples.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ReportAuto)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ReportNone)
        Me.GroupBox1.Controls.Add(Me.ReportPrompt)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 69)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(302, 109)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Auto-report"
        '
        'ReportAuto
        '
        Me.ReportAuto.AutoSize = True
        Me.ReportAuto.Location = New System.Drawing.Point(9, 78)
        Me.ReportAuto.Name = "ReportAuto"
        Me.ReportAuto.Size = New System.Drawing.Size(144, 17)
        Me.ReportAuto.TabIndex = 3
        Me.ReportAuto.TabStop = True
        Me.ReportAuto.Text = "Issue report automatically"
        Me.ReportAuto.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(234, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "When asked to warn a user with a final warning:"
        '
        'ReportNone
        '
        Me.ReportNone.AutoSize = True
        Me.ReportNone.Location = New System.Drawing.Point(9, 32)
        Me.ReportNone.Name = "ReportNone"
        Me.ReportNone.Size = New System.Drawing.Size(77, 17)
        Me.ReportNone.TabIndex = 1
        Me.ReportNone.TabStop = True
        Me.ReportNone.Text = "Do nothing"
        Me.ReportNone.UseVisualStyleBackColor = True
        '
        'ReportPrompt
        '
        Me.ReportPrompt.AutoSize = True
        Me.ReportPrompt.Location = New System.Drawing.Point(9, 55)
        Me.ReportPrompt.Name = "ReportPrompt"
        Me.ReportPrompt.Size = New System.Drawing.Size(103, 17)
        Me.ReportPrompt.TabIndex = 2
        Me.ReportPrompt.TabStop = True
        Me.ReportPrompt.Text = "Prompt for report"
        Me.ReportPrompt.UseVisualStyleBackColor = True
        '
        'TemplatesTab
        '
        Me.TemplatesTab.Controls.Add(Me.AddTemplate)
        Me.TemplatesTab.Controls.Add(Me.RemoveTemplate)
        Me.TemplatesTab.Controls.Add(Me.Label12)
        Me.TemplatesTab.Controls.Add(Me.Templates)
        Me.TemplatesTab.Location = New System.Drawing.Point(4, 23)
        Me.TemplatesTab.Name = "TemplatesTab"
        Me.TemplatesTab.Padding = New System.Windows.Forms.Padding(3)
        Me.TemplatesTab.Size = New System.Drawing.Size(517, 291)
        Me.TemplatesTab.TabIndex = 6
        Me.TemplatesTab.Text = "Templates"
        Me.TemplatesTab.UseVisualStyleBackColor = True
        '
        'AddTemplate
        '
        Me.AddTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AddTemplate.Location = New System.Drawing.Point(6, 249)
        Me.AddTemplate.Name = "AddTemplate"
        Me.AddTemplate.Size = New System.Drawing.Size(62, 23)
        Me.AddTemplate.TabIndex = 35
        Me.AddTemplate.Text = "Add"
        Me.AddTemplate.UseVisualStyleBackColor = True
        '
        'RemoveTemplate
        '
        Me.RemoveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RemoveTemplate.Enabled = False
        Me.RemoveTemplate.Location = New System.Drawing.Point(74, 249)
        Me.RemoveTemplate.Name = "RemoveTemplate"
        Me.RemoveTemplate.Size = New System.Drawing.Size(62, 23)
        Me.RemoveTemplate.TabIndex = 34
        Me.RemoveTemplate.Text = "Remove"
        Me.RemoveTemplate.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 12)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(125, 13)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "User template messages:"
        '
        'Templates
        '
        Me.Templates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Templates.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.DisplayColumn, Me.TemplateColumn})
        Me.Templates.FullRowSelect = True
        Me.Templates.GridLines = True
        Me.Templates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.Templates.Location = New System.Drawing.Point(6, 28)
        Me.Templates.Name = "Templates"
        Me.Templates.Size = New System.Drawing.Size(502, 215)
        Me.Templates.TabIndex = 0
        Me.Templates.UseCompatibleStateImageBehavior = False
        Me.Templates.View = System.Windows.Forms.View.Details
        '
        'DisplayColumn
        '
        Me.DisplayColumn.Text = "Display text"
        Me.DisplayColumn.Width = 250
        '
        'TemplateColumn
        '
        Me.TemplateColumn.Text = "Template"
        Me.TemplateColumn.Width = 207
        '
        'EditorTab
        '
        Me.EditorTab.Controls.Add(Me.GroupBox2)
        Me.EditorTab.Location = New System.Drawing.Point(4, 23)
        Me.EditorTab.Name = "EditorTab"
        Me.EditorTab.Padding = New System.Windows.Forms.Padding(3)
        Me.EditorTab.Size = New System.Drawing.Size(517, 291)
        Me.EditorTab.TabIndex = 8
        Me.EditorTab.Text = "Editor"
        Me.EditorTab.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.ColorParamCall)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.ColorHtmlTag)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.ColorTemplate)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.ColorParamName)
        Me.GroupBox2.Controls.Add(Me.ColorExternalLink)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.ColorParam)
        Me.GroupBox2.Controls.Add(Me.ColorMagicWord)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.ColorImage)
        Me.GroupBox2.Controls.Add(Me.ColorLink)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.ColorReference)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.ColorComment)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 16)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(493, 265)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Syntax highlight colors"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(18, 206)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(74, 13)
        Me.Label26.TabIndex = 13
        Me.Label26.Text = "Parameter call"
        '
        'ColorParamCall
        '
        Me.ColorParamCall.BackColor = System.Drawing.Color.Black
        Me.ColorParamCall.Location = New System.Drawing.Point(125, 201)
        Me.ColorParamCall.Name = "ColorParamCall"
        Me.ColorParamCall.Size = New System.Drawing.Size(44, 23)
        Me.ColorParamCall.TabIndex = 12
        Me.ColorParamCall.UseVisualStyleBackColor = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(18, 177)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(55, 13)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "HTML tag"
        '
        'ColorHtmlTag
        '
        Me.ColorHtmlTag.BackColor = System.Drawing.Color.Black
        Me.ColorHtmlTag.Location = New System.Drawing.Point(125, 172)
        Me.ColorHtmlTag.Name = "ColorHtmlTag"
        Me.ColorHtmlTag.Size = New System.Drawing.Size(44, 23)
        Me.ColorHtmlTag.TabIndex = 10
        Me.ColorHtmlTag.UseVisualStyleBackColor = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(18, 148)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(51, 13)
        Me.Label24.TabIndex = 9
        Me.Label24.Text = "Template"
        '
        'ColorTemplate
        '
        Me.ColorTemplate.BackColor = System.Drawing.Color.Black
        Me.ColorTemplate.Location = New System.Drawing.Point(125, 143)
        Me.ColorTemplate.Name = "ColorTemplate"
        Me.ColorTemplate.Size = New System.Drawing.Size(44, 23)
        Me.ColorTemplate.TabIndex = 8
        Me.ColorTemplate.UseVisualStyleBackColor = False
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(18, 235)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(84, 13)
        Me.Label30.TabIndex = 7
        Me.Label30.Text = "Parameter name"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(18, 119)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(64, 13)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "External link"
        '
        'ColorParamName
        '
        Me.ColorParamName.BackColor = System.Drawing.Color.Black
        Me.ColorParamName.Location = New System.Drawing.Point(125, 230)
        Me.ColorParamName.Name = "ColorParamName"
        Me.ColorParamName.Size = New System.Drawing.Size(44, 23)
        Me.ColorParamName.TabIndex = 6
        Me.ColorParamName.UseVisualStyleBackColor = False
        '
        'ColorExternalLink
        '
        Me.ColorExternalLink.BackColor = System.Drawing.Color.Black
        Me.ColorExternalLink.Location = New System.Drawing.Point(125, 114)
        Me.ColorExternalLink.Name = "ColorExternalLink"
        Me.ColorExternalLink.Size = New System.Drawing.Size(44, 23)
        Me.ColorExternalLink.TabIndex = 6
        Me.ColorExternalLink.UseVisualStyleBackColor = False
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(195, 90)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(101, 13)
        Me.Label29.TabIndex = 5
        Me.Label29.Text = "Template parameter"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(18, 90)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(62, 13)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "Magic word"
        '
        'ColorParam
        '
        Me.ColorParam.BackColor = System.Drawing.Color.Black
        Me.ColorParam.Location = New System.Drawing.Point(302, 85)
        Me.ColorParam.Name = "ColorParam"
        Me.ColorParam.Size = New System.Drawing.Size(44, 23)
        Me.ColorParam.TabIndex = 4
        Me.ColorParam.UseVisualStyleBackColor = False
        '
        'ColorMagicWord
        '
        Me.ColorMagicWord.BackColor = System.Drawing.Color.Black
        Me.ColorMagicWord.Location = New System.Drawing.Point(125, 85)
        Me.ColorMagicWord.Name = "ColorMagicWord"
        Me.ColorMagicWord.Size = New System.Drawing.Size(44, 23)
        Me.ColorMagicWord.TabIndex = 4
        Me.ColorMagicWord.UseVisualStyleBackColor = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(195, 61)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(36, 13)
        Me.Label28.TabIndex = 3
        Me.Label28.Text = "Image"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(18, 61)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(27, 13)
        Me.Label21.TabIndex = 3
        Me.Label21.Text = "Link"
        '
        'ColorImage
        '
        Me.ColorImage.BackColor = System.Drawing.Color.Black
        Me.ColorImage.Location = New System.Drawing.Point(302, 56)
        Me.ColorImage.Name = "ColorImage"
        Me.ColorImage.Size = New System.Drawing.Size(44, 23)
        Me.ColorImage.TabIndex = 2
        Me.ColorImage.UseVisualStyleBackColor = False
        '
        'ColorLink
        '
        Me.ColorLink.BackColor = System.Drawing.Color.Black
        Me.ColorLink.Location = New System.Drawing.Point(125, 56)
        Me.ColorLink.Name = "ColorLink"
        Me.ColorLink.Size = New System.Drawing.Size(44, 23)
        Me.ColorLink.TabIndex = 2
        Me.ColorLink.UseVisualStyleBackColor = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(195, 32)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(57, 13)
        Me.Label27.TabIndex = 1
        Me.Label27.Text = "Reference"
        '
        'ColorReference
        '
        Me.ColorReference.BackColor = System.Drawing.Color.Black
        Me.ColorReference.Location = New System.Drawing.Point(302, 27)
        Me.ColorReference.Name = "ColorReference"
        Me.ColorReference.Size = New System.Drawing.Size(44, 23)
        Me.ColorReference.TabIndex = 0
        Me.ColorReference.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(18, 32)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(51, 13)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "Comment"
        '
        'ColorComment
        '
        Me.ColorComment.BackColor = System.Drawing.Color.Black
        Me.ColorComment.Location = New System.Drawing.Point(125, 27)
        Me.ColorComment.Name = "ColorComment"
        Me.ColorComment.Size = New System.Drawing.Size(44, 23)
        Me.ColorComment.TabIndex = 0
        Me.ColorComment.UseVisualStyleBackColor = False
        '
        'AdminTab
        '
        Me.AdminTab.Controls.Add(Me.BlockTime)
        Me.AdminTab.Controls.Add(Me.Label11)
        Me.AdminTab.Controls.Add(Me.Label10)
        Me.AdminTab.Controls.Add(Me.Label9)
        Me.AdminTab.Controls.Add(Me.BlockTimeAnon)
        Me.AdminTab.Controls.Add(Me.Label7)
        Me.AdminTab.Controls.Add(Me.BlockReason)
        Me.AdminTab.Controls.Add(Me.PromptForBlock)
        Me.AdminTab.Controls.Add(Me.UseAdminFunctions)
        Me.AdminTab.Location = New System.Drawing.Point(4, 23)
        Me.AdminTab.Name = "AdminTab"
        Me.AdminTab.Size = New System.Drawing.Size(517, 291)
        Me.AdminTab.TabIndex = 1
        Me.AdminTab.Text = "Admin"
        Me.AdminTab.UseVisualStyleBackColor = True
        '
        'BlockTime
        '
        Me.BlockTime.Location = New System.Drawing.Point(129, 140)
        Me.BlockTime.Name = "BlockTime"
        Me.BlockTime.Size = New System.Drawing.Size(100, 20)
        Me.BlockTime.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(39, 143)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "registered users:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(31, 121)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(92, 13)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "anonymous users:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 98)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(126, 13)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "Default block duration for"
        '
        'BlockTimeAnon
        '
        Me.BlockTimeAnon.Location = New System.Drawing.Point(129, 118)
        Me.BlockTimeAnon.Name = "BlockTimeAnon"
        Me.BlockTimeAnon.Size = New System.Drawing.Size(100, 20)
        Me.BlockTimeAnon.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 67)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(108, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Default block reason:"
        '
        'BlockReason
        '
        Me.BlockReason.Location = New System.Drawing.Point(120, 64)
        Me.BlockReason.Name = "BlockReason"
        Me.BlockReason.Size = New System.Drawing.Size(195, 20)
        Me.BlockReason.TabIndex = 2
        '
        'PromptForBlock
        '
        Me.PromptForBlock.AutoSize = True
        Me.PromptForBlock.Location = New System.Drawing.Point(9, 38)
        Me.PromptForBlock.Name = "PromptForBlock"
        Me.PromptForBlock.Size = New System.Drawing.Size(306, 17)
        Me.PromptForBlock.TabIndex = 1
        Me.PromptForBlock.Text = "Prompt for block if asked to warn a user with a final warning"
        Me.PromptForBlock.UseVisualStyleBackColor = True
        '
        'UseAdminFunctions
        '
        Me.UseAdminFunctions.AutoSize = True
        Me.UseAdminFunctions.Location = New System.Drawing.Point(9, 15)
        Me.UseAdminFunctions.Name = "UseAdminFunctions"
        Me.UseAdminFunctions.Size = New System.Drawing.Size(206, 17)
        Me.UseAdminFunctions.TabIndex = 0
        Me.UseAdminFunctions.Text = "Use administrator functions if available"
        Me.UseAdminFunctions.UseVisualStyleBackColor = True
        '
        'ConfigForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 361)
        Me.Controls.Add(Me.Tabs)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "ConfigForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configuration"
        Me.Tabs.ResumeLayout(False)
        Me.GeneralTab.ResumeLayout(False)
        Me.GeneralTab.PerformLayout()
        Me.KeyboardTab.ResumeLayout(False)
        Me.KeyboardTab.PerformLayout()
        Me.QueueTab.ResumeLayout(False)
        Me.QueueTab.PerformLayout()
        CType(Me.QueueMaxAge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.EditingTab.ResumeLayout(False)
        Me.EditingTab.PerformLayout()
        Me.RevertTab.ResumeLayout(False)
        Me.RevertTab.PerformLayout()
        Me.ReportingTab.ResumeLayout(False)
        Me.ReportingTab.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TemplatesTab.ResumeLayout(False)
        Me.TemplatesTab.PerformLayout()
        Me.EditorTab.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.AdminTab.ResumeLayout(False)
        Me.AdminTab.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents GeneralTab As System.Windows.Forms.TabPage
    Friend WithEvents AdminTab As System.Windows.Forms.TabPage
    Friend WithEvents EditingTab As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Watchlist As System.Windows.Forms.CheckedListBox
    Friend WithEvents Minor As System.Windows.Forms.CheckedListBox
    Friend WithEvents ReportingTab As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ReportAuto As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ReportNone As System.Windows.Forms.RadioButton
    Friend WithEvents ReportPrompt As System.Windows.Forms.RadioButton
    Friend WithEvents ExtendReports As System.Windows.Forms.CheckBox
    Friend WithEvents ReportLinkExamples As System.Windows.Forms.CheckBox
    Friend WithEvents TrayIcon As System.Windows.Forms.CheckBox
    Friend WithEvents AutoWhitelist As System.Windows.Forms.CheckBox
    Friend WithEvents Preloading As System.Windows.Forms.CheckBox
    Friend WithEvents Preloads As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents IrcPort As System.Windows.Forms.TextBox
    Friend WithEvents ShowQueue As System.Windows.Forms.CheckBox
    Friend WithEvents ShowNewEdits As System.Windows.Forms.CheckBox
    Friend WithEvents QueueTab As System.Windows.Forms.TabPage
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ShowNewPages As System.Windows.Forms.CheckBox
    Friend WithEvents Namespaces As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ShowAnonymous As System.Windows.Forms.CheckBox
    Friend WithEvents ShowRegistered As System.Windows.Forms.CheckBox
    Friend WithEvents PromptForBlock As System.Windows.Forms.CheckBox
    Friend WithEvents UseAdminFunctions As System.Windows.Forms.CheckBox
    Friend WithEvents RevertTab As System.Windows.Forms.TabPage
    Friend WithEvents AddSummary As System.Windows.Forms.Button
    Friend WithEvents RemoveSummary As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents RevertSummaries As System.Windows.Forms.ListBox
    Friend WithEvents UseRollback As System.Windows.Forms.CheckBox
    Friend WithEvents AutoAdvance As System.Windows.Forms.CheckBox
    Friend WithEvents ConfirmSame As System.Windows.Forms.CheckBox
    Friend WithEvents ConfirmMultiple As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents BlockTimeAnon As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents BlockReason As System.Windows.Forms.TextBox
    Friend WithEvents BlockTime As System.Windows.Forms.TextBox
    Friend WithEvents TemplatesTab As System.Windows.Forms.TabPage
    Friend WithEvents DisplayColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents TemplateColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents AddTemplate As System.Windows.Forms.Button
    Friend WithEvents RemoveTemplate As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Templates As System.Windows.Forms.ListView
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents DiffFontSize As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ShowToolTips As System.Windows.Forms.CheckBox
    Friend WithEvents DefaultSummary As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents UndoSummary As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents OpenInBrowser As System.Windows.Forms.CheckBox
    Friend WithEvents LogFileBrowse As System.Windows.Forms.Button
    Friend WithEvents LogFile As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents KeyboardTab As System.Windows.Forms.TabPage
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents ShortcutList As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ChangeShortcut As System.Windows.Forms.TextBox
    Friend WithEvents ChangeShortcutLabel As System.Windows.Forms.Label
    Friend WithEvents NoShortcut As System.Windows.Forms.Button
    Friend WithEvents Defaults As System.Windows.Forms.Button
    Friend WithEvents ConfirmSelfRevert As System.Windows.Forms.CheckBox
    Friend WithEvents ClearRevertSummaries As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents EditorTab As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
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
    Friend WithEvents QueueMaxAge As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
End Class
