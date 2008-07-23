<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.ScrollTimer = New System.Windows.Forms.Timer(Me.components)
        Me.RevertTimer = New System.Windows.Forms.Timer(Me.components)
        Me.RcReqTimer = New System.Windows.Forms.Timer(Me.components)
        Me.LogMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LogContextCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.BlockReqTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TrayMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TrayRestore = New System.Windows.Forms.ToolStripMenuItem
        Me.TrayExit = New System.Windows.Forms.ToolStripMenuItem
        Me.TopMenu = New System.Windows.Forms.MenuStrip
        Me.MenuSystem = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemShowNewMessages = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemReconnectIRC = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemSaveLog = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator18 = New System.Windows.Forms.ToolStripSeparator
        Me.SystemShowLog = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemShowQueue = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator8 = New System.Windows.Forms.ToolStripSeparator
        Me.SystemStats = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemOptions = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator3 = New System.Windows.Forms.ToolStripSeparator
        Me.SystemExit = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuQueue = New System.Windows.Forms.ToolStripMenuItem
        Me.QueueNext = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator1 = New System.Windows.Forms.ToolStripSeparator
        Me.QueueEditSources = New System.Windows.Forms.ToolStripMenuItem
        Me.QueueTrim = New System.Windows.Forms.ToolStripMenuItem
        Me.QueueClear = New System.Windows.Forms.ToolStripMenuItem
        Me.GoToMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.GoMyTalk = New System.Windows.Forms.ToolStripMenuItem
        Me.GoMyContribs = New System.Windows.Forms.ToolStripMenuItem
        Me.GoSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.MenuPage = New System.Windows.Forms.ToolStripMenuItem
        Me.PageView = New System.Windows.Forms.ToolStripMenuItem
        Me.PageViewLatest = New System.Windows.Forms.ToolStripMenuItem
        Me.PageHistory = New System.Windows.Forms.ToolStripMenuItem
        Me.PageShowHistoryPage = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator4 = New System.Windows.Forms.ToolStripSeparator
        Me.PageEdit = New System.Windows.Forms.ToolStripMenuItem
        Me.PageTag = New System.Windows.Forms.ToolStripMenuItem
        Me.PageTagDelete = New System.Windows.Forms.ToolStripMenuItem
        Me.TagDeleteMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PageNominate = New System.Windows.Forms.ToolStripMenuItem
        Me.PageProd = New System.Windows.Forms.ToolStripMenuItem
        Me.PageTagSpeedy = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator23 = New System.Windows.Forms.ToolStripSeparator
        Me.PageTagDeleteB = New System.Windows.Forms.ToolStripDropDownButton
        Me.Separator14 = New System.Windows.Forms.ToolStripSeparator
        Me.PageWatch = New System.Windows.Forms.ToolStripMenuItem
        Me.PagePurge = New System.Windows.Forms.ToolStripMenuItem
        Me.PageMove = New System.Windows.Forms.ToolStripMenuItem
        Me.PageMarkPatrolled = New System.Windows.Forms.ToolStripMenuItem
        Me.PageRequestProtection = New System.Windows.Forms.ToolStripMenuItem
        Me.PageProtect = New System.Windows.Forms.ToolStripMenuItem
        Me.PageDelete = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuUser = New System.Windows.Forms.ToolStripMenuItem
        Me.UserInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.UserIgnore = New System.Windows.Forms.ToolStripMenuItem
        Me.UserContribs = New System.Windows.Forms.ToolStripMenuItem
        Me.UserTalk = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator5 = New System.Windows.Forms.ToolStripSeparator
        Me.UserMessage = New System.Windows.Forms.ToolStripMenuItem
        Me.UserWarn = New System.Windows.Forms.ToolStripMenuItem
        Me.UserReport = New System.Windows.Forms.ToolStripMenuItem
        Me.UserBlock = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuBrowser = New System.Windows.Forms.ToolStripMenuItem
        Me.BrowserNewTab = New System.Windows.Forms.ToolStripMenuItem
        Me.BrowserCloseTab = New System.Windows.Forms.ToolStripMenuItem
        Me.BrowserCloseOthers = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator2 = New System.Windows.Forms.ToolStripSeparator
        Me.BrowserBack = New System.Windows.Forms.ToolStripMenuItem
        Me.BrowserForward = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator6 = New System.Windows.Forms.ToolStripSeparator
        Me.BrowserOpen = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator7 = New System.Windows.Forms.ToolStripSeparator
        Me.BrowserNewEdits = New System.Windows.Forms.ToolStripMenuItem
        Me.BrowserNewContribs = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpDocs = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpFeedback = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator16 = New System.Windows.Forms.ToolStripSeparator
        Me.HelpAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.Stats = New System.Windows.Forms.ToolStripMenuItem
        Me.Splitter = New System.Windows.Forms.SplitContainer
        Me.QueueScroll = New System.Windows.Forms.VScrollBar
        Me.QueueSource = New System.Windows.Forms.ComboBox
        Me.EditInfo = New huggle.EditInfoPanel
        Me.Queue = New huggle.QueuePanel
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.InitialTab = New huggle.BrowserTab
        Me.Status = New huggle.ListView2
        Me.Url = New System.Windows.Forms.ColumnHeader
        Me.Details = New System.Windows.Forms.ColumnHeader
        Me.ToolContainer = New System.Windows.Forms.ToolStripContainer
        Me.MainStrip = New System.Windows.Forms.ToolStrip
        Me.RevertWarnB = New System.Windows.Forms.ToolStripSplitButton
        Me.RevertWarnVandalism = New System.Windows.Forms.ToolStripMenuItem
        Me.RevertWarnSpam = New System.Windows.Forms.ToolStripMenuItem
        Me.RevertWarnTest = New System.Windows.Forms.ToolStripMenuItem
        Me.RevertWarnDelete = New System.Windows.Forms.ToolStripMenuItem
        Me.RevertWarnAttack = New System.Windows.Forms.ToolStripMenuItem
        Me.RevertWarnError = New System.Windows.Forms.ToolStripMenuItem
        Me.RevertWarnNpov = New System.Windows.Forms.ToolStripMenuItem
        Me.RevertWarnUnsourced = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator24 = New System.Windows.Forms.ToolStripSeparator
        Me.RevertWarnAdvanced = New System.Windows.Forms.ToolStripMenuItem
        Me.DiffNextB = New System.Windows.Forms.ToolStripButton
        Me.Separator17 = New System.Windows.Forms.ToolStripSeparator
        Me.UserIgnoreB = New System.Windows.Forms.ToolStripButton
        Me.DiffRevertB = New System.Windows.Forms.ToolStripSplitButton
        Me.RevertMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Separator20 = New System.Windows.Forms.ToolStripSeparator
        Me.DiffRevertSummary = New System.Windows.Forms.ToolStripMenuItem
        Me.UserTemplateB = New System.Windows.Forms.ToolStripDropDownButton
        Me.TemplateMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UserMessageWelcome = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator13 = New System.Windows.Forms.ToolStripSeparator
        Me.Separator21 = New System.Windows.Forms.ToolStripSeparator
        Me.UserMessageOther = New System.Windows.Forms.ToolStripMenuItem
        Me.WarnB = New System.Windows.Forms.ToolStripDropDownButton
        Me.WarnMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.WarnVandalism = New System.Windows.Forms.ToolStripMenuItem
        Me.WarnSpam = New System.Windows.Forms.ToolStripMenuItem
        Me.WarnTest = New System.Windows.Forms.ToolStripMenuItem
        Me.WarnDelete = New System.Windows.Forms.ToolStripMenuItem
        Me.WarnAttack = New System.Windows.Forms.ToolStripMenuItem
        Me.WarnError = New System.Windows.Forms.ToolStripMenuItem
        Me.WarnUnsourced = New System.Windows.Forms.ToolStripMenuItem
        Me.WarnNpov = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator22 = New System.Windows.Forms.ToolStripSeparator
        Me.WarnAdvanced = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator9 = New System.Windows.Forms.ToolStripSeparator
        Me.CancelB = New System.Windows.Forms.ToolStripButton
        Me.UndoB = New System.Windows.Forms.ToolStripDropDownButton
        Me.UndoMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Separator19 = New System.Windows.Forms.ToolStripSeparator
        Me.HistoryStrip = New System.Windows.Forms.ToolStrip
        Me.PageLabel = New System.Windows.Forms.ToolStripLabel
        Me.PageB = New System.Windows.Forms.ToolStripComboBox
        Me.HistoryB = New System.Windows.Forms.ToolStripButton
        Me.HistoryScrollLB = New System.Windows.Forms.ToolStripButton
        Me.History = New System.Windows.Forms.ToolStripLabel
        Me.HistoryScrollRB = New System.Windows.Forms.ToolStripButton
        Me.UserLabel = New System.Windows.Forms.ToolStripLabel
        Me.UserB = New System.Windows.Forms.ToolStripComboBox
        Me.ContribsB = New System.Windows.Forms.ToolStripButton
        Me.ContribsScrollLB = New System.Windows.Forms.ToolStripButton
        Me.Contribs = New System.Windows.Forms.ToolStripLabel
        Me.ContribsScrollRB = New System.Windows.Forms.ToolStripButton
        Me.NavigationStrip = New System.Windows.Forms.ToolStrip
        Me.BrowserBackB = New System.Windows.Forms.ToolStripSplitButton
        Me.BrowserForwardB = New System.Windows.Forms.ToolStripSplitButton
        Me.Separator10 = New System.Windows.Forms.ToolStripSeparator
        Me.BrowserOpenB = New System.Windows.Forms.ToolStripButton
        Me.BrowserNewTabB = New System.Windows.Forms.ToolStripButton
        Me.BrowserCloseTabB = New System.Windows.Forms.ToolStripButton
        Me.Separator11 = New System.Windows.Forms.ToolStripSeparator
        Me.HistoryPrevB = New System.Windows.Forms.ToolStripButton
        Me.HistoryNextB = New System.Windows.Forms.ToolStripButton
        Me.HistoryLastB = New System.Windows.Forms.ToolStripButton
        Me.HistoryDiffToCurB = New System.Windows.Forms.ToolStripButton
        Me.Separator12 = New System.Windows.Forms.ToolStripSeparator
        Me.ContribsPrevB = New System.Windows.Forms.ToolStripButton
        Me.ContribsNextB = New System.Windows.Forms.ToolStripButton
        Me.ContribsLastB = New System.Windows.Forms.ToolStripButton
        Me.ActionsStrip = New System.Windows.Forms.ToolStrip
        Me.PageViewB = New System.Windows.Forms.ToolStripButton
        Me.PageEditB = New System.Windows.Forms.ToolStripButton
        Me.PageTagB = New System.Windows.Forms.ToolStripButton
        Me.PageDeleteB = New System.Windows.Forms.ToolStripButton
        Me.PageWatchB = New System.Windows.Forms.ToolStripButton
        Me.Separator15 = New System.Windows.Forms.ToolStripSeparator
        Me.UserInfoB = New System.Windows.Forms.ToolStripButton
        Me.UserTalkB = New System.Windows.Forms.ToolStripButton
        Me.UserMessageB = New System.Windows.Forms.ToolStripButton
        Me.UserReportB = New System.Windows.Forms.ToolStripButton
        Me.RateUpdateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DrawTimer = New System.Windows.Forms.Timer(Me.components)
        Me.LogMenu.SuspendLayout()
        Me.TrayMenu.SuspendLayout()
        Me.TopMenu.SuspendLayout()
        Me.TagDeleteMenu.SuspendLayout()
        Me.Splitter.Panel1.SuspendLayout()
        Me.Splitter.Panel2.SuspendLayout()
        Me.Splitter.SuspendLayout()
        Me.Tabs.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.ToolContainer.ContentPanel.SuspendLayout()
        Me.ToolContainer.TopToolStripPanel.SuspendLayout()
        Me.ToolContainer.SuspendLayout()
        Me.MainStrip.SuspendLayout()
        Me.RevertMenu.SuspendLayout()
        Me.TemplateMenu.SuspendLayout()
        Me.WarnMenu.SuspendLayout()
        Me.HistoryStrip.SuspendLayout()
        Me.NavigationStrip.SuspendLayout()
        Me.ActionsStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ScrollTimer
        '
        Me.ScrollTimer.Interval = 50
        '
        'RevertTimer
        '
        Me.RevertTimer.Interval = 1000
        '
        'RcReqTimer
        '
        Me.RcReqTimer.Interval = 2000
        '
        'LogMenu
        '
        Me.LogMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogContextCopy})
        Me.LogMenu.Name = "LogContext"
        Me.LogMenu.Size = New System.Drawing.Size(100, 26)
        '
        'LogContextCopy
        '
        Me.LogContextCopy.Name = "LogContextCopy"
        Me.LogContextCopy.Size = New System.Drawing.Size(99, 22)
        Me.LogContextCopy.Text = "Copy"
        '
        'BlockReqTimer
        '
        Me.BlockReqTimer.Interval = 20000
        '
        'TrayIcon
        '
        Me.TrayIcon.BalloonTipText = "You have new messages (click to view)."
        Me.TrayIcon.BalloonTipTitle = "huggle"
        Me.TrayIcon.ContextMenuStrip = Me.TrayMenu
        Me.TrayIcon.Icon = CType(resources.GetObject("TrayIcon.Icon"), System.Drawing.Icon)
        Me.TrayIcon.Text = "Huggle"
        '
        'TrayMenu
        '
        Me.TrayMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TrayRestore, Me.TrayExit})
        Me.TrayMenu.Name = "TrayContext"
        Me.TrayMenu.Size = New System.Drawing.Size(114, 48)
        '
        'TrayRestore
        '
        Me.TrayRestore.Name = "TrayRestore"
        Me.TrayRestore.Size = New System.Drawing.Size(113, 22)
        Me.TrayRestore.Text = "Minimize"
        '
        'TrayExit
        '
        Me.TrayExit.Name = "TrayExit"
        Me.TrayExit.Size = New System.Drawing.Size(113, 22)
        Me.TrayExit.Text = "Exit"
        '
        'TopMenu
        '
        Me.TopMenu.AutoSize = False
        Me.TopMenu.Dock = System.Windows.Forms.DockStyle.None
        Me.TopMenu.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TopMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuSystem, Me.MenuQueue, Me.GoToMenu, Me.MenuPage, Me.MenuUser, Me.MenuBrowser, Me.MenuHelp, Me.Stats})
        Me.TopMenu.Location = New System.Drawing.Point(0, 0)
        Me.TopMenu.Name = "TopMenu"
        Me.TopMenu.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.TopMenu.Size = New System.Drawing.Size(792, 24)
        Me.TopMenu.TabIndex = 0
        '
        'MenuSystem
        '
        Me.MenuSystem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemShowNewMessages, Me.SystemReconnectIRC, Me.SystemSaveLog, Me.Separator18, Me.SystemShowLog, Me.SystemShowQueue, Me.Separator8, Me.SystemStats, Me.SystemOptions, Me.Separator3, Me.SystemExit})
        Me.MenuSystem.Name = "MenuSystem"
        Me.MenuSystem.Size = New System.Drawing.Size(54, 24)
        Me.MenuSystem.Text = "&System"
        '
        'SystemShowNewMessages
        '
        Me.SystemShowNewMessages.Enabled = False
        Me.SystemShowNewMessages.Name = "SystemShowNewMessages"
        Me.SystemShowNewMessages.ShortcutKeyDisplayString = ""
        Me.SystemShowNewMessages.Size = New System.Drawing.Size(173, 22)
        Me.SystemShowNewMessages.Text = "Show new messages"
        '
        'SystemReconnectIRC
        '
        Me.SystemReconnectIRC.Enabled = False
        Me.SystemReconnectIRC.Name = "SystemReconnectIRC"
        Me.SystemReconnectIRC.Size = New System.Drawing.Size(173, 22)
        Me.SystemReconnectIRC.Text = "Reconnect IRC feed"
        '
        'SystemSaveLog
        '
        Me.SystemSaveLog.Name = "SystemSaveLog"
        Me.SystemSaveLog.Size = New System.Drawing.Size(173, 22)
        Me.SystemSaveLog.Text = "Save log..."
        '
        'Separator18
        '
        Me.Separator18.Name = "Separator18"
        Me.Separator18.Size = New System.Drawing.Size(170, 6)
        '
        'SystemShowLog
        '
        Me.SystemShowLog.Checked = True
        Me.SystemShowLog.CheckOnClick = True
        Me.SystemShowLog.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SystemShowLog.Name = "SystemShowLog"
        Me.SystemShowLog.Size = New System.Drawing.Size(173, 22)
        Me.SystemShowLog.Text = "Show log"
        '
        'SystemShowQueue
        '
        Me.SystemShowQueue.CheckOnClick = True
        Me.SystemShowQueue.Name = "SystemShowQueue"
        Me.SystemShowQueue.Size = New System.Drawing.Size(173, 22)
        Me.SystemShowQueue.Text = "Show queue"
        '
        'Separator8
        '
        Me.Separator8.Name = "Separator8"
        Me.Separator8.Size = New System.Drawing.Size(170, 6)
        '
        'SystemStats
        '
        Me.SystemStats.Name = "SystemStats"
        Me.SystemStats.Size = New System.Drawing.Size(173, 22)
        Me.SystemStats.Text = "Statistics..."
        '
        'SystemOptions
        '
        Me.SystemOptions.Name = "SystemOptions"
        Me.SystemOptions.Size = New System.Drawing.Size(173, 22)
        Me.SystemOptions.Text = "Options..."
        '
        'Separator3
        '
        Me.Separator3.Name = "Separator3"
        Me.Separator3.Size = New System.Drawing.Size(170, 6)
        '
        'SystemExit
        '
        Me.SystemExit.Name = "SystemExit"
        Me.SystemExit.Size = New System.Drawing.Size(173, 22)
        Me.SystemExit.Text = "Exit"
        '
        'MenuQueue
        '
        Me.MenuQueue.AutoSize = False
        Me.MenuQueue.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QueueNext, Me.Separator1, Me.QueueEditSources, Me.QueueTrim, Me.QueueClear})
        Me.MenuQueue.Name = "MenuQueue"
        Me.MenuQueue.Size = New System.Drawing.Size(51, 24)
        Me.MenuQueue.Text = "&Queue"
        '
        'QueueNext
        '
        Me.QueueNext.Name = "QueueNext"
        Me.QueueNext.ShortcutKeyDisplayString = ""
        Me.QueueNext.Size = New System.Drawing.Size(124, 22)
        Me.QueueNext.Text = "Next"
        '
        'Separator1
        '
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(121, 6)
        '
        'QueueEditSources
        '
        Me.QueueEditSources.Name = "QueueEditSources"
        Me.QueueEditSources.Size = New System.Drawing.Size(124, 22)
        Me.QueueEditSources.Text = "Sources..."
        '
        'QueueTrim
        '
        Me.QueueTrim.Name = "QueueTrim"
        Me.QueueTrim.Size = New System.Drawing.Size(124, 22)
        Me.QueueTrim.Text = "Trim..."
        '
        'QueueClear
        '
        Me.QueueClear.Name = "QueueClear"
        Me.QueueClear.Size = New System.Drawing.Size(124, 22)
        Me.QueueClear.Text = "Clear"
        '
        'GoToMenu
        '
        Me.GoToMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GoMyTalk, Me.GoMyContribs, Me.GoSeparator})
        Me.GoToMenu.Name = "GoToMenu"
        Me.GoToMenu.Size = New System.Drawing.Size(45, 24)
        Me.GoToMenu.Text = "&Go to"
        '
        'GoMyTalk
        '
        Me.GoMyTalk.Name = "GoMyTalk"
        Me.GoMyTalk.Size = New System.Drawing.Size(153, 22)
        Me.GoMyTalk.Text = "My talk page"
        '
        'GoMyContribs
        '
        Me.GoMyContribs.Name = "GoMyContribs"
        Me.GoMyContribs.Size = New System.Drawing.Size(153, 22)
        Me.GoMyContribs.Text = "My contributions"
        '
        'GoSeparator
        '
        Me.GoSeparator.Name = "GoSeparator"
        Me.GoSeparator.Size = New System.Drawing.Size(150, 6)
        Me.GoSeparator.Visible = False
        '
        'MenuPage
        '
        Me.MenuPage.AutoSize = False
        Me.MenuPage.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageView, Me.PageViewLatest, Me.PageHistory, Me.PageShowHistoryPage, Me.Separator4, Me.PageEdit, Me.PageTag, Me.PageTagDelete, Me.Separator14, Me.PageWatch, Me.PagePurge, Me.PageMove, Me.PageMarkPatrolled, Me.PageRequestProtection, Me.PageProtect, Me.PageDelete})
        Me.MenuPage.Name = "MenuPage"
        Me.MenuPage.Size = New System.Drawing.Size(43, 24)
        Me.MenuPage.Text = "&Page"
        '
        'PageView
        '
        Me.PageView.Name = "PageView"
        Me.PageView.ShortcutKeyDisplayString = ""
        Me.PageView.Size = New System.Drawing.Size(178, 22)
        Me.PageView.Text = "View this revision"
        '
        'PageViewLatest
        '
        Me.PageViewLatest.Name = "PageViewLatest"
        Me.PageViewLatest.ShortcutKeyDisplayString = ""
        Me.PageViewLatest.Size = New System.Drawing.Size(178, 22)
        Me.PageViewLatest.Text = "View latest revision"
        '
        'PageHistory
        '
        Me.PageHistory.Name = "PageHistory"
        Me.PageHistory.ShortcutKeyDisplayString = ""
        Me.PageHistory.Size = New System.Drawing.Size(178, 22)
        Me.PageHistory.Text = "Retrieve history"
        '
        'PageShowHistoryPage
        '
        Me.PageShowHistoryPage.Name = "PageShowHistoryPage"
        Me.PageShowHistoryPage.Size = New System.Drawing.Size(178, 22)
        Me.PageShowHistoryPage.Text = "Show history page"
        '
        'Separator4
        '
        Me.Separator4.Name = "Separator4"
        Me.Separator4.Size = New System.Drawing.Size(175, 6)
        '
        'PageEdit
        '
        Me.PageEdit.Name = "PageEdit"
        Me.PageEdit.ShortcutKeyDisplayString = ""
        Me.PageEdit.Size = New System.Drawing.Size(178, 22)
        Me.PageEdit.Text = "Edit"
        '
        'PageTag
        '
        Me.PageTag.Name = "PageTag"
        Me.PageTag.ShortcutKeyDisplayString = ""
        Me.PageTag.Size = New System.Drawing.Size(178, 22)
        Me.PageTag.Text = "Tag..."
        '
        'PageTagDelete
        '
        Me.PageTagDelete.DropDown = Me.TagDeleteMenu
        Me.PageTagDelete.Name = "PageTagDelete"
        Me.PageTagDelete.Size = New System.Drawing.Size(178, 22)
        Me.PageTagDelete.Text = "Request deletion"
        '
        'TagDeleteMenu
        '
        Me.TagDeleteMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageNominate, Me.PageProd, Me.PageTagSpeedy, Me.Separator23})
        Me.TagDeleteMenu.Name = "SpeedyMenu"
        Me.TagDeleteMenu.Size = New System.Drawing.Size(190, 76)
        '
        'PageNominate
        '
        Me.PageNominate.Name = "PageNominate"
        Me.PageNominate.ShortcutKeyDisplayString = ""
        Me.PageNominate.Size = New System.Drawing.Size(189, 22)
        Me.PageNominate.Text = "Nominate for deletion..."
        '
        'PageProd
        '
        Me.PageProd.Name = "PageProd"
        Me.PageProd.ShortcutKeyDisplayString = ""
        Me.PageProd.Size = New System.Drawing.Size(189, 22)
        Me.PageProd.Text = "Proposed deletion..."
        '
        'PageTagSpeedy
        '
        Me.PageTagSpeedy.Name = "PageTagSpeedy"
        Me.PageTagSpeedy.ShortcutKeyDisplayString = ""
        Me.PageTagSpeedy.Size = New System.Drawing.Size(189, 22)
        Me.PageTagSpeedy.Text = "Speedy deletion..."
        '
        'Separator23
        '
        Me.Separator23.Name = "Separator23"
        Me.Separator23.Size = New System.Drawing.Size(186, 6)
        '
        'PageTagDeleteB
        '
        Me.PageTagDeleteB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageTagDeleteB.DropDown = Me.TagDeleteMenu
        Me.PageTagDeleteB.Enabled = False
        Me.PageTagDeleteB.Image = Global.huggle.My.Resources.Resources.page_speedy
        Me.PageTagDeleteB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageTagDeleteB.Name = "PageTagDeleteB"
        Me.PageTagDeleteB.ShowDropDownArrow = False
        Me.PageTagDeleteB.Size = New System.Drawing.Size(32, 32)
        Me.PageTagDeleteB.ToolTipText = "Tag this page for deletion [S]"
        '
        'Separator14
        '
        Me.Separator14.Name = "Separator14"
        Me.Separator14.Size = New System.Drawing.Size(175, 6)
        '
        'PageWatch
        '
        Me.PageWatch.Name = "PageWatch"
        Me.PageWatch.ShortcutKeyDisplayString = ""
        Me.PageWatch.Size = New System.Drawing.Size(178, 22)
        Me.PageWatch.Text = "Watch"
        '
        'PagePurge
        '
        Me.PagePurge.Name = "PagePurge"
        Me.PagePurge.Size = New System.Drawing.Size(178, 22)
        Me.PagePurge.Text = "Purge"
        '
        'PageMove
        '
        Me.PageMove.Name = "PageMove"
        Me.PageMove.Size = New System.Drawing.Size(178, 22)
        Me.PageMove.Text = "Move..."
        '
        'PageMarkPatrolled
        '
        Me.PageMarkPatrolled.Name = "PageMarkPatrolled"
        Me.PageMarkPatrolled.Size = New System.Drawing.Size(178, 22)
        Me.PageMarkPatrolled.Text = "Mark patrolled"
        '
        'PageRequestProtection
        '
        Me.PageRequestProtection.Name = "PageRequestProtection"
        Me.PageRequestProtection.Size = New System.Drawing.Size(178, 22)
        Me.PageRequestProtection.Text = "Request protection..."
        '
        'PageProtect
        '
        Me.PageProtect.Name = "PageProtect"
        Me.PageProtect.Size = New System.Drawing.Size(178, 22)
        Me.PageProtect.Text = "Protect..."
        Me.PageProtect.Visible = False
        '
        'PageDelete
        '
        Me.PageDelete.Name = "PageDelete"
        Me.PageDelete.ShortcutKeyDisplayString = ""
        Me.PageDelete.Size = New System.Drawing.Size(178, 22)
        Me.PageDelete.Text = "Delete..."
        Me.PageDelete.Visible = False
        '
        'MenuUser
        '
        Me.MenuUser.AutoSize = False
        Me.MenuUser.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserInfo, Me.UserIgnore, Me.UserContribs, Me.UserTalk, Me.Separator5, Me.UserMessage, Me.UserWarn, Me.UserReport, Me.UserBlock})
        Me.MenuUser.Name = "MenuUser"
        Me.MenuUser.Size = New System.Drawing.Size(41, 24)
        Me.MenuUser.Text = "&User"
        '
        'UserInfo
        '
        Me.UserInfo.Name = "UserInfo"
        Me.UserInfo.ShortcutKeyDisplayString = ""
        Me.UserInfo.Size = New System.Drawing.Size(180, 22)
        Me.UserInfo.Text = "Show user info"
        '
        'UserIgnore
        '
        Me.UserIgnore.Name = "UserIgnore"
        Me.UserIgnore.ShortcutKeyDisplayString = ""
        Me.UserIgnore.Size = New System.Drawing.Size(180, 22)
        Me.UserIgnore.Text = "Ignore"
        '
        'UserContribs
        '
        Me.UserContribs.Name = "UserContribs"
        Me.UserContribs.ShortcutKeyDisplayString = ""
        Me.UserContribs.Size = New System.Drawing.Size(180, 22)
        Me.UserContribs.Text = "Retrieve contributions"
        '
        'UserTalk
        '
        Me.UserTalk.Name = "UserTalk"
        Me.UserTalk.ShortcutKeyDisplayString = ""
        Me.UserTalk.Size = New System.Drawing.Size(180, 22)
        Me.UserTalk.Text = "View talk page"
        '
        'Separator5
        '
        Me.Separator5.Name = "Separator5"
        Me.Separator5.Size = New System.Drawing.Size(177, 6)
        '
        'UserMessage
        '
        Me.UserMessage.Name = "UserMessage"
        Me.UserMessage.ShortcutKeyDisplayString = ""
        Me.UserMessage.Size = New System.Drawing.Size(180, 22)
        Me.UserMessage.Text = "Message..."
        '
        'UserWarn
        '
        Me.UserWarn.Name = "UserWarn"
        Me.UserWarn.ShortcutKeyDisplayString = ""
        Me.UserWarn.Size = New System.Drawing.Size(180, 22)
        Me.UserWarn.Text = "Warn..."
        '
        'UserReport
        '
        Me.UserReport.Name = "UserReport"
        Me.UserReport.ShortcutKeyDisplayString = ""
        Me.UserReport.Size = New System.Drawing.Size(180, 22)
        Me.UserReport.Text = "Report..."
        '
        'UserBlock
        '
        Me.UserBlock.Name = "UserBlock"
        Me.UserBlock.ShortcutKeyDisplayString = ""
        Me.UserBlock.Size = New System.Drawing.Size(180, 22)
        Me.UserBlock.Text = "Block..."
        Me.UserBlock.Visible = False
        '
        'MenuBrowser
        '
        Me.MenuBrowser.AutoSize = False
        Me.MenuBrowser.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BrowserNewTab, Me.BrowserCloseTab, Me.BrowserCloseOthers, Me.Separator2, Me.BrowserBack, Me.BrowserForward, Me.Separator6, Me.BrowserOpen, Me.Separator7, Me.BrowserNewEdits, Me.BrowserNewContribs})
        Me.MenuBrowser.Name = "MenuBrowser"
        Me.MenuBrowser.Size = New System.Drawing.Size(58, 24)
        Me.MenuBrowser.Text = "&Browser"
        '
        'BrowserNewTab
        '
        Me.BrowserNewTab.Name = "BrowserNewTab"
        Me.BrowserNewTab.ShortcutKeyDisplayString = ""
        Me.BrowserNewTab.Size = New System.Drawing.Size(227, 22)
        Me.BrowserNewTab.Text = "New tab"
        '
        'BrowserCloseTab
        '
        Me.BrowserCloseTab.Name = "BrowserCloseTab"
        Me.BrowserCloseTab.ShortcutKeyDisplayString = ""
        Me.BrowserCloseTab.Size = New System.Drawing.Size(227, 22)
        Me.BrowserCloseTab.Text = "Close tab"
        '
        'BrowserCloseOthers
        '
        Me.BrowserCloseOthers.Enabled = False
        Me.BrowserCloseOthers.Name = "BrowserCloseOthers"
        Me.BrowserCloseOthers.ShortcutKeyDisplayString = ""
        Me.BrowserCloseOthers.Size = New System.Drawing.Size(227, 22)
        Me.BrowserCloseOthers.Text = "Close other tabs"
        '
        'Separator2
        '
        Me.Separator2.Name = "Separator2"
        Me.Separator2.Size = New System.Drawing.Size(224, 6)
        '
        'BrowserBack
        '
        Me.BrowserBack.Name = "BrowserBack"
        Me.BrowserBack.ShortcutKeyDisplayString = ""
        Me.BrowserBack.Size = New System.Drawing.Size(227, 22)
        Me.BrowserBack.Text = "Back"
        '
        'BrowserForward
        '
        Me.BrowserForward.Name = "BrowserForward"
        Me.BrowserForward.ShortcutKeyDisplayString = ""
        Me.BrowserForward.Size = New System.Drawing.Size(227, 22)
        Me.BrowserForward.Text = "Forward"
        '
        'Separator6
        '
        Me.Separator6.Name = "Separator6"
        Me.Separator6.Size = New System.Drawing.Size(224, 6)
        '
        'BrowserOpen
        '
        Me.BrowserOpen.Name = "BrowserOpen"
        Me.BrowserOpen.ShortcutKeyDisplayString = ""
        Me.BrowserOpen.Size = New System.Drawing.Size(227, 22)
        Me.BrowserOpen.Text = "View this in external browser"
        '
        'Separator7
        '
        Me.Separator7.Name = "Separator7"
        Me.Separator7.Size = New System.Drawing.Size(224, 6)
        '
        'BrowserNewEdits
        '
        Me.BrowserNewEdits.CheckOnClick = True
        Me.BrowserNewEdits.Name = "BrowserNewEdits"
        Me.BrowserNewEdits.Size = New System.Drawing.Size(227, 22)
        Me.BrowserNewEdits.Text = "Show new edits to page"
        '
        'BrowserNewContribs
        '
        Me.BrowserNewContribs.CheckOnClick = True
        Me.BrowserNewContribs.Name = "BrowserNewContribs"
        Me.BrowserNewContribs.Size = New System.Drawing.Size(227, 22)
        Me.BrowserNewContribs.Text = "Show new contributions by user"
        '
        'MenuHelp
        '
        Me.MenuHelp.AutoSize = False
        Me.MenuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpDocs, Me.HelpFeedback, Me.Separator16, Me.HelpAbout})
        Me.MenuHelp.Name = "MenuHelp"
        Me.MenuHelp.Size = New System.Drawing.Size(40, 24)
        Me.MenuHelp.Text = "&Help"
        '
        'HelpDocs
        '
        Me.HelpDocs.Name = "HelpDocs"
        Me.HelpDocs.ShortcutKeyDisplayString = ""
        Me.HelpDocs.Size = New System.Drawing.Size(152, 22)
        Me.HelpDocs.Text = "Documentation"
        '
        'HelpFeedback
        '
        Me.HelpFeedback.Name = "HelpFeedback"
        Me.HelpFeedback.Size = New System.Drawing.Size(152, 22)
        Me.HelpFeedback.Text = "Feedback"
        '
        'Separator16
        '
        Me.Separator16.Name = "Separator16"
        Me.Separator16.Size = New System.Drawing.Size(149, 6)
        '
        'HelpAbout
        '
        Me.HelpAbout.Name = "HelpAbout"
        Me.HelpAbout.Size = New System.Drawing.Size(152, 22)
        Me.HelpAbout.Text = "About huggle..."
        '
        'Stats
        '
        Me.Stats.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Stats.Name = "Stats"
        Me.Stats.Size = New System.Drawing.Size(22, 24)
        Me.Stats.Text = " "
        '
        'Splitter
        '
        Me.Splitter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Splitter.Location = New System.Drawing.Point(0, 0)
        Me.Splitter.Margin = New System.Windows.Forms.Padding(0)
        Me.Splitter.Name = "Splitter"
        Me.Splitter.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'Splitter.Panel1
        '
        Me.Splitter.Panel1.Controls.Add(Me.QueueScroll)
        Me.Splitter.Panel1.Controls.Add(Me.QueueSource)
        Me.Splitter.Panel1.Controls.Add(Me.EditInfo)
        Me.Splitter.Panel1.Controls.Add(Me.Queue)
        Me.Splitter.Panel1.Controls.Add(Me.Tabs)
        Me.Splitter.Panel1MinSize = 100
        '
        'Splitter.Panel2
        '
        Me.Splitter.Panel2.Controls.Add(Me.Status)
        Me.Splitter.Panel2MinSize = 60
        Me.Splitter.Size = New System.Drawing.Size(792, 260)
        Me.Splitter.SplitterDistance = 167
        Me.Splitter.TabIndex = 44
        '
        'QueueScroll
        '
        Me.QueueScroll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.QueueScroll.Enabled = False
        Me.QueueScroll.LargeChange = 0
        Me.QueueScroll.Location = New System.Drawing.Point(160, 29)
        Me.QueueScroll.Maximum = 0
        Me.QueueScroll.Name = "QueueScroll"
        Me.QueueScroll.Size = New System.Drawing.Size(17, 138)
        Me.QueueScroll.SmallChange = 0
        Me.QueueScroll.TabIndex = 48
        '
        'QueueSource
        '
        Me.QueueSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.QueueSource.FormattingEnabled = True
        Me.QueueSource.Location = New System.Drawing.Point(3, 5)
        Me.QueueSource.Name = "QueueSource"
        Me.QueueSource.Size = New System.Drawing.Size(159, 21)
        Me.QueueSource.TabIndex = 7
        '
        'EditInfo
        '
        Me.EditInfo.Location = New System.Drawing.Point(3, 84)
        Me.EditInfo.Name = "EditInfo"
        Me.EditInfo.Size = New System.Drawing.Size(353, 78)
        Me.EditInfo.TabIndex = 45
        Me.EditInfo.Visible = False
        '
        'Queue
        '
        Me.Queue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Queue.BackColor = System.Drawing.SystemColors.Control
        Me.Queue.Location = New System.Drawing.Point(0, 32)
        Me.Queue.Name = "Queue"
        Me.Queue.Size = New System.Drawing.Size(162, 135)
        Me.Queue.TabIndex = 47
        '
        'Tabs
        '
        Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Controls.Add(Me.TabPage1)
        Me.Tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.Tabs.ItemSize = New System.Drawing.Size(1, 1)
        Me.Tabs.Location = New System.Drawing.Point(183, 0)
        Me.Tabs.Margin = New System.Windows.Forms.Padding(1)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.Padding = New System.Drawing.Point(0, 0)
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(609, 167)
        Me.Tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.Tabs.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.InitialTab)
        Me.TabPage1.Location = New System.Drawing.Point(4, 5)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(601, 158)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'InitialTab
        '
        Me.InitialTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InitialTab.Location = New System.Drawing.Point(0, 0)
        Me.InitialTab.Name = "InitialTab"
        Me.InitialTab.Size = New System.Drawing.Size(601, 158)
        Me.InitialTab.TabIndex = 0
        '
        'Status
        '
        Me.Status.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.Status.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Url, Me.Details})
        Me.Status.ContextMenuStrip = Me.LogMenu
        Me.Status.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Status.FullRowSelect = True
        Me.Status.GridLines = True
        Me.Status.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.Status.Location = New System.Drawing.Point(0, 0)
        Me.Status.MultiSelect = False
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(792, 89)
        Me.Status.TabIndex = 0
        Me.Status.UseCompatibleStateImageBehavior = False
        Me.Status.View = System.Windows.Forms.View.Details
        '
        'Url
        '
        Me.Url.Width = 0
        '
        'Details
        '
        Me.Details.Width = 770
        '
        'ToolContainer
        '
        Me.ToolContainer.BottomToolStripPanelVisible = False
        '
        'ToolContainer.ContentPanel
        '
        Me.ToolContainer.ContentPanel.Controls.Add(Me.Splitter)
        Me.ToolContainer.ContentPanel.Size = New System.Drawing.Size(792, 260)
        Me.ToolContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolContainer.LeftToolStripPanelVisible = False
        Me.ToolContainer.Location = New System.Drawing.Point(0, 0)
        Me.ToolContainer.Name = "ToolContainer"
        Me.ToolContainer.RightToolStripPanelVisible = False
        Me.ToolContainer.Size = New System.Drawing.Size(792, 434)
        Me.ToolContainer.TabIndex = 1
        Me.ToolContainer.Text = "ToolStripContainer1"
        '
        'ToolContainer.TopToolStripPanel
        '
        Me.ToolContainer.TopToolStripPanel.Controls.Add(Me.TopMenu)
        Me.ToolContainer.TopToolStripPanel.Controls.Add(Me.MainStrip)
        Me.ToolContainer.TopToolStripPanel.Controls.Add(Me.HistoryStrip)
        Me.ToolContainer.TopToolStripPanel.Controls.Add(Me.NavigationStrip)
        Me.ToolContainer.TopToolStripPanel.Controls.Add(Me.ActionsStrip)
        '
        'MainStrip
        '
        Me.MainStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.MainStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.MainStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RevertWarnB, Me.DiffNextB, Me.Separator17, Me.UserIgnoreB, Me.DiffRevertB, Me.UserTemplateB, Me.WarnB, Me.Separator9, Me.CancelB, Me.UndoB, Me.Separator19})
        Me.MainStrip.Location = New System.Drawing.Point(3, 24)
        Me.MainStrip.Name = "MainStrip"
        Me.MainStrip.Size = New System.Drawing.Size(397, 55)
        Me.MainStrip.TabIndex = 2
        '
        'RevertWarnB
        '
        Me.RevertWarnB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RevertWarnB.DropDownButtonWidth = 16
        Me.RevertWarnB.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RevertWarnVandalism, Me.RevertWarnSpam, Me.RevertWarnTest, Me.RevertWarnDelete, Me.RevertWarnAttack, Me.RevertWarnError, Me.RevertWarnNpov, Me.RevertWarnUnsourced, Me.Separator24, Me.RevertWarnAdvanced})
        Me.RevertWarnB.Enabled = False
        Me.RevertWarnB.Image = Global.huggle.My.Resources.Resources.revert_and_warn_2
        Me.RevertWarnB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.RevertWarnB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RevertWarnB.Name = "RevertWarnB"
        Me.RevertWarnB.Size = New System.Drawing.Size(69, 52)
        Me.RevertWarnB.ToolTipText = "Revert this revision, and issue a user warning [Q]"
        '
        'RevertWarnVandalism
        '
        Me.RevertWarnVandalism.Name = "RevertWarnVandalism"
        Me.RevertWarnVandalism.Size = New System.Drawing.Size(168, 22)
        Me.RevertWarnVandalism.Text = "&Vandalism"
        '
        'RevertWarnSpam
        '
        Me.RevertWarnSpam.Name = "RevertWarnSpam"
        Me.RevertWarnSpam.Size = New System.Drawing.Size(168, 22)
        Me.RevertWarnSpam.Text = "&Spam"
        '
        'RevertWarnTest
        '
        Me.RevertWarnTest.Name = "RevertWarnTest"
        Me.RevertWarnTest.Size = New System.Drawing.Size(168, 22)
        Me.RevertWarnTest.Text = "&Editing tests"
        '
        'RevertWarnDelete
        '
        Me.RevertWarnDelete.Name = "RevertWarnDelete"
        Me.RevertWarnDelete.Size = New System.Drawing.Size(168, 22)
        Me.RevertWarnDelete.Text = "&Removal of content"
        '
        'RevertWarnAttack
        '
        Me.RevertWarnAttack.Name = "RevertWarnAttack"
        Me.RevertWarnAttack.Size = New System.Drawing.Size(168, 22)
        Me.RevertWarnAttack.Text = "&Personal attacks"
        '
        'RevertWarnError
        '
        Me.RevertWarnError.Name = "RevertWarnError"
        Me.RevertWarnError.Size = New System.Drawing.Size(168, 22)
        Me.RevertWarnError.Text = "&Factual errors"
        '
        'RevertWarnNpov
        '
        Me.RevertWarnNpov.Name = "RevertWarnNpov"
        Me.RevertWarnNpov.Size = New System.Drawing.Size(168, 22)
        Me.RevertWarnNpov.Text = "&Biased material"
        '
        'RevertWarnUnsourced
        '
        Me.RevertWarnUnsourced.Name = "RevertWarnUnsourced"
        Me.RevertWarnUnsourced.Size = New System.Drawing.Size(168, 22)
        Me.RevertWarnUnsourced.Text = "&Unsourced material"
        '
        'Separator24
        '
        Me.Separator24.Name = "Separator24"
        Me.Separator24.Size = New System.Drawing.Size(165, 6)
        '
        'RevertWarnAdvanced
        '
        Me.RevertWarnAdvanced.Name = "RevertWarnAdvanced"
        Me.RevertWarnAdvanced.Size = New System.Drawing.Size(168, 22)
        Me.RevertWarnAdvanced.Text = "&Advanced..."
        '
        'DiffNextB
        '
        Me.DiffNextB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DiffNextB.Enabled = False
        Me.DiffNextB.Image = Global.huggle.My.Resources.Resources.arrow_2
        Me.DiffNextB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.DiffNextB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DiffNextB.Name = "DiffNextB"
        Me.DiffNextB.Size = New System.Drawing.Size(52, 52)
        Me.DiffNextB.ToolTipText = "Show the next revision in the queue [Space]"
        '
        'Separator17
        '
        Me.Separator17.Name = "Separator17"
        Me.Separator17.Size = New System.Drawing.Size(6, 55)
        '
        'UserIgnoreB
        '
        Me.UserIgnoreB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserIgnoreB.Enabled = False
        Me.UserIgnoreB.Image = Global.huggle.My.Resources.Resources.user_whitelist
        Me.UserIgnoreB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.UserIgnoreB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserIgnoreB.Name = "UserIgnoreB"
        Me.UserIgnoreB.Padding = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.UserIgnoreB.Size = New System.Drawing.Size(39, 52)
        Me.UserIgnoreB.ToolTipText = "Ignore all contributions by this user [I]"
        '
        'DiffRevertB
        '
        Me.DiffRevertB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DiffRevertB.DropDown = Me.RevertMenu
        Me.DiffRevertB.DropDownButtonWidth = 16
        Me.DiffRevertB.Enabled = False
        Me.DiffRevertB.Image = Global.huggle.My.Resources.Resources.diff_revert
        Me.DiffRevertB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.DiffRevertB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DiffRevertB.Name = "DiffRevertB"
        Me.DiffRevertB.Size = New System.Drawing.Size(57, 52)
        Me.DiffRevertB.ToolTipText = "Revert this revision [R]"
        '
        'RevertMenu
        '
        Me.RevertMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Separator20, Me.DiffRevertSummary})
        Me.RevertMenu.Name = "RevertMenu"
        Me.RevertMenu.Size = New System.Drawing.Size(174, 32)
        '
        'Separator20
        '
        Me.Separator20.Name = "Separator20"
        Me.Separator20.Size = New System.Drawing.Size(170, 6)
        '
        'DiffRevertSummary
        '
        Me.DiffRevertSummary.Name = "DiffRevertSummary"
        Me.DiffRevertSummary.ShortcutKeyDisplayString = "Y"
        Me.DiffRevertSummary.Size = New System.Drawing.Size(173, 22)
        Me.DiffRevertSummary.Text = "Other summary..."
        '
        'UserTemplateB
        '
        Me.UserTemplateB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserTemplateB.DropDown = Me.TemplateMenu
        Me.UserTemplateB.Enabled = False
        Me.UserTemplateB.Image = Global.huggle.My.Resources.Resources.user_template
        Me.UserTemplateB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.UserTemplateB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserTemplateB.Name = "UserTemplateB"
        Me.UserTemplateB.Padding = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.UserTemplateB.ShowDropDownArrow = False
        Me.UserTemplateB.Size = New System.Drawing.Size(42, 52)
        Me.UserTemplateB.ToolTipText = "Send template message to user [T]"
        '
        'TemplateMenu
        '
        Me.TemplateMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserMessageWelcome, Me.Separator13, Me.Separator21, Me.UserMessageOther})
        Me.TemplateMenu.Name = "TemplateMenu"
        Me.TemplateMenu.Size = New System.Drawing.Size(174, 60)
        '
        'UserMessageWelcome
        '
        Me.UserMessageWelcome.Name = "UserMessageWelcome"
        Me.UserMessageWelcome.Size = New System.Drawing.Size(173, 22)
        Me.UserMessageWelcome.Text = "Welcome"
        '
        'Separator13
        '
        Me.Separator13.Name = "Separator13"
        Me.Separator13.Size = New System.Drawing.Size(170, 6)
        '
        'Separator21
        '
        Me.Separator21.Name = "Separator21"
        Me.Separator21.Size = New System.Drawing.Size(170, 6)
        '
        'UserMessageOther
        '
        Me.UserMessageOther.Name = "UserMessageOther"
        Me.UserMessageOther.ShortcutKeyDisplayString = "N"
        Me.UserMessageOther.Size = New System.Drawing.Size(173, 22)
        Me.UserMessageOther.Text = "Other message..."
        '
        'WarnB
        '
        Me.WarnB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.WarnB.DropDown = Me.WarnMenu
        Me.WarnB.Enabled = False
        Me.WarnB.Image = Global.huggle.My.Resources.Resources.user_warn
        Me.WarnB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WarnB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.WarnB.Name = "WarnB"
        Me.WarnB.Padding = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.WarnB.ShowDropDownArrow = False
        Me.WarnB.Size = New System.Drawing.Size(41, 52)
        Me.WarnB.ToolTipText = "Warn user"
        '
        'WarnMenu
        '
        Me.WarnMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WarnVandalism, Me.WarnSpam, Me.WarnTest, Me.WarnDelete, Me.WarnAttack, Me.WarnError, Me.WarnUnsourced, Me.WarnNpov, Me.Separator22, Me.WarnAdvanced})
        Me.WarnMenu.Name = "WarnMenu"
        Me.WarnMenu.Size = New System.Drawing.Size(169, 208)
        '
        'WarnVandalism
        '
        Me.WarnVandalism.Name = "WarnVandalism"
        Me.WarnVandalism.ShortcutKeyDisplayString = ""
        Me.WarnVandalism.Size = New System.Drawing.Size(168, 22)
        Me.WarnVandalism.Text = "&Vandalism"
        '
        'WarnSpam
        '
        Me.WarnSpam.Name = "WarnSpam"
        Me.WarnSpam.ShortcutKeyDisplayString = ""
        Me.WarnSpam.Size = New System.Drawing.Size(168, 22)
        Me.WarnSpam.Text = "&Spam"
        '
        'WarnTest
        '
        Me.WarnTest.Name = "WarnTest"
        Me.WarnTest.ShortcutKeyDisplayString = ""
        Me.WarnTest.Size = New System.Drawing.Size(168, 22)
        Me.WarnTest.Text = "&Editing tests"
        '
        'WarnDelete
        '
        Me.WarnDelete.Name = "WarnDelete"
        Me.WarnDelete.ShortcutKeyDisplayString = ""
        Me.WarnDelete.Size = New System.Drawing.Size(168, 22)
        Me.WarnDelete.Text = "&Removal of content"
        '
        'WarnAttack
        '
        Me.WarnAttack.Name = "WarnAttack"
        Me.WarnAttack.ShortcutKeyDisplayString = ""
        Me.WarnAttack.Size = New System.Drawing.Size(168, 22)
        Me.WarnAttack.Text = "&Personal attacks"
        '
        'WarnError
        '
        Me.WarnError.Name = "WarnError"
        Me.WarnError.ShortcutKeyDisplayString = ""
        Me.WarnError.Size = New System.Drawing.Size(168, 22)
        Me.WarnError.Text = "&Factual errors"
        '
        'WarnUnsourced
        '
        Me.WarnUnsourced.Name = "WarnUnsourced"
        Me.WarnUnsourced.ShortcutKeyDisplayString = ""
        Me.WarnUnsourced.Size = New System.Drawing.Size(168, 22)
        Me.WarnUnsourced.Text = "&Unsourced material"
        '
        'WarnNpov
        '
        Me.WarnNpov.Name = "WarnNpov"
        Me.WarnNpov.ShortcutKeyDisplayString = ""
        Me.WarnNpov.Size = New System.Drawing.Size(168, 22)
        Me.WarnNpov.Text = "&Biased material"
        '
        'Separator22
        '
        Me.Separator22.Name = "Separator22"
        Me.Separator22.Size = New System.Drawing.Size(165, 6)
        '
        'WarnAdvanced
        '
        Me.WarnAdvanced.Name = "WarnAdvanced"
        Me.WarnAdvanced.ShortcutKeyDisplayString = ""
        Me.WarnAdvanced.Size = New System.Drawing.Size(168, 22)
        Me.WarnAdvanced.Text = "&Advanced..."
        '
        'Separator9
        '
        Me.Separator9.Name = "Separator9"
        Me.Separator9.Size = New System.Drawing.Size(6, 55)
        '
        'CancelB
        '
        Me.CancelB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CancelB.Enabled = False
        Me.CancelB.Image = Global.huggle.My.Resources.Resources.cancel_all
        Me.CancelB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.CancelB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CancelB.Name = "CancelB"
        Me.CancelB.Size = New System.Drawing.Size(37, 52)
        Me.CancelB.ToolTipText = "Cancel all pending actions [Esc]"
        '
        'UndoB
        '
        Me.UndoB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UndoB.DropDown = Me.UndoMenu
        Me.UndoB.Enabled = False
        Me.UndoB.Image = Global.huggle.My.Resources.Resources.undo
        Me.UndoB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.UndoB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UndoB.Name = "UndoB"
        Me.UndoB.Padding = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.UndoB.ShowDropDownArrow = False
        Me.UndoB.Size = New System.Drawing.Size(39, 52)
        Me.UndoB.ToolTipText = "Undo recent actions"
        '
        'UndoMenu
        '
        Me.UndoMenu.Name = "UndoMenu"
        Me.UndoMenu.OwnerItem = Me.UndoB
        Me.UndoMenu.Size = New System.Drawing.Size(61, 4)
        '
        'Separator19
        '
        Me.Separator19.Name = "Separator19"
        Me.Separator19.Size = New System.Drawing.Size(6, 55)
        '
        'HistoryStrip
        '
        Me.HistoryStrip.CanOverflow = False
        Me.HistoryStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.HistoryStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HistoryStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageLabel, Me.PageB, Me.HistoryB, Me.HistoryScrollLB, Me.History, Me.HistoryScrollRB, Me.UserLabel, Me.UserB, Me.ContribsB, Me.ContribsScrollLB, Me.Contribs, Me.ContribsScrollRB})
        Me.HistoryStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.HistoryStrip.Location = New System.Drawing.Point(3, 79)
        Me.HistoryStrip.Name = "HistoryStrip"
        Me.HistoryStrip.Padding = New System.Windows.Forms.Padding(3, 0, 1, 0)
        Me.HistoryStrip.Size = New System.Drawing.Size(728, 25)
        Me.HistoryStrip.TabIndex = 4
        '
        'PageLabel
        '
        Me.PageLabel.AutoSize = False
        Me.PageLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PageLabel.Margin = New System.Windows.Forms.Padding(1, 6, 1, 0)
        Me.PageLabel.Name = "PageLabel"
        Me.PageLabel.Size = New System.Drawing.Size(32, 17)
        Me.PageLabel.Text = "Page"
        '
        'PageB
        '
        Me.PageB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.PageB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.PageB.DropDownHeight = 200
        Me.PageB.DropDownWidth = 300
        Me.PageB.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.PageB.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PageB.IntegralHeight = False
        Me.PageB.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.PageB.MaxDropDownItems = 100
        Me.PageB.MaxLength = 255
        Me.PageB.Name = "PageB"
        Me.PageB.Size = New System.Drawing.Size(160, 21)
        Me.PageB.Sorted = True
        '
        'HistoryB
        '
        Me.HistoryB.AutoSize = False
        Me.HistoryB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.HistoryB.Enabled = False
        Me.HistoryB.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HistoryB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HistoryB.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.HistoryB.Name = "HistoryB"
        Me.HistoryB.Size = New System.Drawing.Size(52, 21)
        Me.HistoryB.Text = "History"
        '
        'HistoryScrollLB
        '
        Me.HistoryScrollLB.AutoSize = False
        Me.HistoryScrollLB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HistoryScrollLB.Enabled = False
        Me.HistoryScrollLB.Image = Global.huggle.My.Resources.Resources.gray_previous
        Me.HistoryScrollLB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.HistoryScrollLB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HistoryScrollLB.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.HistoryScrollLB.Name = "HistoryScrollLB"
        Me.HistoryScrollLB.Size = New System.Drawing.Size(23, 21)
        '
        'History
        '
        Me.History.AutoSize = False
        Me.History.BackColor = System.Drawing.Color.Gainsboro
        Me.History.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.History.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.History.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.History.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.History.Name = "History"
        Me.History.Size = New System.Drawing.Size(60, 20)
        '
        'HistoryScrollRB
        '
        Me.HistoryScrollRB.AutoSize = False
        Me.HistoryScrollRB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HistoryScrollRB.Enabled = False
        Me.HistoryScrollRB.Image = Global.huggle.My.Resources.Resources.gray_next
        Me.HistoryScrollRB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.HistoryScrollRB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HistoryScrollRB.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.HistoryScrollRB.Name = "HistoryScrollRB"
        Me.HistoryScrollRB.Size = New System.Drawing.Size(23, 21)
        '
        'UserLabel
        '
        Me.UserLabel.AutoSize = False
        Me.UserLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserLabel.Margin = New System.Windows.Forms.Padding(1, 6, 1, 0)
        Me.UserLabel.Name = "UserLabel"
        Me.UserLabel.Size = New System.Drawing.Size(32, 17)
        Me.UserLabel.Text = "User"
        '
        'UserB
        '
        Me.UserB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.UserB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.UserB.DropDownHeight = 200
        Me.UserB.DropDownWidth = 300
        Me.UserB.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.UserB.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserB.IntegralHeight = False
        Me.UserB.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.UserB.MaxDropDownItems = 100
        Me.UserB.MaxLength = 255
        Me.UserB.Name = "UserB"
        Me.UserB.Size = New System.Drawing.Size(160, 21)
        Me.UserB.Sorted = True
        '
        'ContribsB
        '
        Me.ContribsB.AutoSize = False
        Me.ContribsB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ContribsB.Enabled = False
        Me.ContribsB.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContribsB.Image = CType(resources.GetObject("ContribsB.Image"), System.Drawing.Image)
        Me.ContribsB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ContribsB.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.ContribsB.Name = "ContribsB"
        Me.ContribsB.Size = New System.Drawing.Size(52, 21)
        Me.ContribsB.Text = "Contribs"
        '
        'ContribsScrollLB
        '
        Me.ContribsScrollLB.AutoSize = False
        Me.ContribsScrollLB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ContribsScrollLB.Enabled = False
        Me.ContribsScrollLB.Image = Global.huggle.My.Resources.Resources.gray_previous
        Me.ContribsScrollLB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContribsScrollLB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ContribsScrollLB.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.ContribsScrollLB.Name = "ContribsScrollLB"
        Me.ContribsScrollLB.Size = New System.Drawing.Size(23, 21)
        '
        'Contribs
        '
        Me.Contribs.AutoSize = False
        Me.Contribs.BackColor = System.Drawing.Color.Gainsboro
        Me.Contribs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Contribs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Contribs.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.Contribs.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.Contribs.Name = "Contribs"
        Me.Contribs.Size = New System.Drawing.Size(60, 20)
        '
        'ContribsScrollRB
        '
        Me.ContribsScrollRB.AutoSize = False
        Me.ContribsScrollRB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ContribsScrollRB.Enabled = False
        Me.ContribsScrollRB.Image = Global.huggle.My.Resources.Resources.gray_next
        Me.ContribsScrollRB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContribsScrollRB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ContribsScrollRB.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.ContribsScrollRB.Name = "ContribsScrollRB"
        Me.ContribsScrollRB.Size = New System.Drawing.Size(23, 21)
        '
        'NavigationStrip
        '
        Me.NavigationStrip.AllowItemReorder = True
        Me.NavigationStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.NavigationStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.NavigationStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BrowserBackB, Me.BrowserForwardB, Me.Separator10, Me.BrowserOpenB, Me.BrowserNewTabB, Me.BrowserCloseTabB, Me.Separator11, Me.HistoryPrevB, Me.HistoryNextB, Me.HistoryLastB, Me.HistoryDiffToCurB, Me.Separator12, Me.ContribsPrevB, Me.ContribsNextB, Me.ContribsLastB})
        Me.NavigationStrip.Location = New System.Drawing.Point(3, 104)
        Me.NavigationStrip.Name = "NavigationStrip"
        Me.NavigationStrip.Size = New System.Drawing.Size(426, 35)
        Me.NavigationStrip.TabIndex = 3
        '
        'BrowserBackB
        '
        Me.BrowserBackB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BrowserBackB.DropDownButtonWidth = 12
        Me.BrowserBackB.Enabled = False
        Me.BrowserBackB.Image = Global.huggle.My.Resources.Resources.browser_prev
        Me.BrowserBackB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.BrowserBackB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BrowserBackB.Name = "BrowserBackB"
        Me.BrowserBackB.Size = New System.Drawing.Size(45, 32)
        Me.BrowserBackB.ToolTipText = "Back - ["
        '
        'BrowserForwardB
        '
        Me.BrowserForwardB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BrowserForwardB.DropDownButtonWidth = 12
        Me.BrowserForwardB.Enabled = False
        Me.BrowserForwardB.Image = Global.huggle.My.Resources.Resources.browser_next
        Me.BrowserForwardB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.BrowserForwardB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BrowserForwardB.Name = "BrowserForwardB"
        Me.BrowserForwardB.Size = New System.Drawing.Size(45, 32)
        Me.BrowserForwardB.ToolTipText = "Forward - ]"
        '
        'Separator10
        '
        Me.Separator10.Name = "Separator10"
        Me.Separator10.Size = New System.Drawing.Size(6, 35)
        '
        'BrowserOpenB
        '
        Me.BrowserOpenB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BrowserOpenB.Enabled = False
        Me.BrowserOpenB.Image = Global.huggle.My.Resources.Resources.browser_open
        Me.BrowserOpenB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.BrowserOpenB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BrowserOpenB.Name = "BrowserOpenB"
        Me.BrowserOpenB.Size = New System.Drawing.Size(31, 32)
        Me.BrowserOpenB.ToolTipText = "View this in browser [O]"
        '
        'BrowserNewTabB
        '
        Me.BrowserNewTabB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BrowserNewTabB.Enabled = False
        Me.BrowserNewTabB.Image = Global.huggle.My.Resources.Resources.browser_add_tab
        Me.BrowserNewTabB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.BrowserNewTabB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BrowserNewTabB.Name = "BrowserNewTabB"
        Me.BrowserNewTabB.Size = New System.Drawing.Size(31, 32)
        Me.BrowserNewTabB.ToolTipText = "New tab [+]"
        '
        'BrowserCloseTabB
        '
        Me.BrowserCloseTabB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BrowserCloseTabB.Enabled = False
        Me.BrowserCloseTabB.Image = Global.huggle.My.Resources.Resources.browser_remove_tab
        Me.BrowserCloseTabB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.BrowserCloseTabB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BrowserCloseTabB.Name = "BrowserCloseTabB"
        Me.BrowserCloseTabB.Size = New System.Drawing.Size(31, 32)
        Me.BrowserCloseTabB.ToolTipText = "Close tab [-]"
        '
        'Separator11
        '
        Me.Separator11.Name = "Separator11"
        Me.Separator11.Size = New System.Drawing.Size(6, 35)
        '
        'HistoryPrevB
        '
        Me.HistoryPrevB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HistoryPrevB.Enabled = False
        Me.HistoryPrevB.Image = Global.huggle.My.Resources.Resources.history_previous
        Me.HistoryPrevB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.HistoryPrevB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HistoryPrevB.Name = "HistoryPrevB"
        Me.HistoryPrevB.Size = New System.Drawing.Size(32, 32)
        Me.HistoryPrevB.ToolTipText = "Show previous revision to this page [Z]"
        '
        'HistoryNextB
        '
        Me.HistoryNextB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HistoryNextB.Enabled = False
        Me.HistoryNextB.Image = Global.huggle.My.Resources.Resources.history_next
        Me.HistoryNextB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.HistoryNextB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HistoryNextB.Name = "HistoryNextB"
        Me.HistoryNextB.Size = New System.Drawing.Size(32, 32)
        Me.HistoryNextB.ToolTipText = "Show next revision to this page [X]"
        '
        'HistoryLastB
        '
        Me.HistoryLastB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HistoryLastB.Enabled = False
        Me.HistoryLastB.Image = Global.huggle.My.Resources.Resources.history_last
        Me.HistoryLastB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.HistoryLastB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HistoryLastB.Name = "HistoryLastB"
        Me.HistoryLastB.Size = New System.Drawing.Size(32, 32)
        Me.HistoryLastB.ToolTipText = "Show latest revision to this page [C]"
        '
        'HistoryDiffToCurB
        '
        Me.HistoryDiffToCurB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HistoryDiffToCurB.Enabled = False
        Me.HistoryDiffToCurB.Image = Global.huggle.My.Resources.Resources.history_to_cur
        Me.HistoryDiffToCurB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.HistoryDiffToCurB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HistoryDiffToCurB.Name = "HistoryDiffToCurB"
        Me.HistoryDiffToCurB.Size = New System.Drawing.Size(30, 32)
        Me.HistoryDiffToCurB.ToolTipText = "Show diff between this revision and the latest revision to this page [D]"
        '
        'Separator12
        '
        Me.Separator12.Name = "Separator12"
        Me.Separator12.Size = New System.Drawing.Size(6, 35)
        '
        'ContribsPrevB
        '
        Me.ContribsPrevB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ContribsPrevB.Enabled = False
        Me.ContribsPrevB.Image = Global.huggle.My.Resources.Resources.contribs_prev
        Me.ContribsPrevB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContribsPrevB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ContribsPrevB.Name = "ContribsPrevB"
        Me.ContribsPrevB.Size = New System.Drawing.Size(32, 32)
        Me.ContribsPrevB.ToolTipText = "Show previous revision by this user [Ctrl + Z]"
        '
        'ContribsNextB
        '
        Me.ContribsNextB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ContribsNextB.Enabled = False
        Me.ContribsNextB.Image = Global.huggle.My.Resources.Resources.contribs_next
        Me.ContribsNextB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContribsNextB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ContribsNextB.Name = "ContribsNextB"
        Me.ContribsNextB.Size = New System.Drawing.Size(32, 32)
        Me.ContribsNextB.ToolTipText = "Show next revision by this user [Ctrl + X]"
        '
        'ContribsLastB
        '
        Me.ContribsLastB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ContribsLastB.Enabled = False
        Me.ContribsLastB.Image = Global.huggle.My.Resources.Resources.contribs_last
        Me.ContribsLastB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContribsLastB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ContribsLastB.Name = "ContribsLastB"
        Me.ContribsLastB.Size = New System.Drawing.Size(32, 32)
        Me.ContribsLastB.ToolTipText = "Show latest revision by this user [Ctrl + C]"
        '
        'ActionsStrip
        '
        Me.ActionsStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.ActionsStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ActionsStrip.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.ActionsStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageViewB, Me.PageEditB, Me.PageTagB, Me.PageTagDeleteB, Me.PageDeleteB, Me.PageWatchB, Me.Separator15, Me.UserInfoB, Me.UserTalkB, Me.UserMessageB, Me.UserReportB})
        Me.ActionsStrip.Location = New System.Drawing.Point(3, 139)
        Me.ActionsStrip.Name = "ActionsStrip"
        Me.ActionsStrip.Size = New System.Drawing.Size(309, 35)
        Me.ActionsStrip.TabIndex = 5
        '
        'PageViewB
        '
        Me.PageViewB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageViewB.Enabled = False
        Me.PageViewB.Image = Global.huggle.My.Resources.Resources.page_view
        Me.PageViewB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageViewB.Name = "PageViewB"
        Me.PageViewB.Size = New System.Drawing.Size(32, 32)
        Me.PageViewB.ToolTipText = "View this revision [V]"
        '
        'PageEditB
        '
        Me.PageEditB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageEditB.Enabled = False
        Me.PageEditB.Image = Global.huggle.My.Resources.Resources.page_edit
        Me.PageEditB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageEditB.Name = "PageEditB"
        Me.PageEditB.Size = New System.Drawing.Size(32, 32)
        Me.PageEditB.ToolTipText = "Edit this page [E]"
        '
        'PageTagB
        '
        Me.PageTagB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageTagB.Enabled = False
        Me.PageTagB.Image = Global.huggle.My.Resources.Resources.page_tag
        Me.PageTagB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageTagB.Name = "PageTagB"
        Me.PageTagB.Size = New System.Drawing.Size(32, 32)
        Me.PageTagB.ToolTipText = "Tag this page [G]"
        '
        'PageDeleteB
        '
        Me.PageDeleteB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageDeleteB.Enabled = False
        Me.PageDeleteB.Image = Global.huggle.My.Resources.Resources.page_delete
        Me.PageDeleteB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageDeleteB.Name = "PageDeleteB"
        Me.PageDeleteB.Size = New System.Drawing.Size(32, 32)
        Me.PageDeleteB.ToolTipText = "Delete this page [S]"
        Me.PageDeleteB.Visible = False
        '
        'PageWatchB
        '
        Me.PageWatchB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageWatchB.Enabled = False
        Me.PageWatchB.Image = Global.huggle.My.Resources.Resources.page_watch
        Me.PageWatchB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageWatchB.Name = "PageWatchB"
        Me.PageWatchB.Size = New System.Drawing.Size(32, 32)
        Me.PageWatchB.ToolTipText = "Watch this page [L]"
        '
        'Separator15
        '
        Me.Separator15.Name = "Separator15"
        Me.Separator15.Size = New System.Drawing.Size(6, 35)
        '
        'UserInfoB
        '
        Me.UserInfoB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserInfoB.Enabled = False
        Me.UserInfoB.Image = Global.huggle.My.Resources.Resources.user_info
        Me.UserInfoB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserInfoB.Name = "UserInfoB"
        Me.UserInfoB.Size = New System.Drawing.Size(32, 32)
        Me.UserInfoB.ToolTipText = "Show user information [?]"
        '
        'UserTalkB
        '
        Me.UserTalkB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserTalkB.Enabled = False
        Me.UserTalkB.Image = Global.huggle.My.Resources.Resources.user_talk
        Me.UserTalkB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserTalkB.Name = "UserTalkB"
        Me.UserTalkB.Size = New System.Drawing.Size(32, 32)
        Me.UserTalkB.ToolTipText = "Show user talk page [U]"
        '
        'UserMessageB
        '
        Me.UserMessageB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserMessageB.Enabled = False
        Me.UserMessageB.Image = Global.huggle.My.Resources.Resources.user_message
        Me.UserMessageB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserMessageB.Name = "UserMessageB"
        Me.UserMessageB.Size = New System.Drawing.Size(32, 32)
        Me.UserMessageB.ToolTipText = "Message user [N]"
        '
        'UserReportB
        '
        Me.UserReportB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserReportB.Enabled = False
        Me.UserReportB.Image = Global.huggle.My.Resources.Resources.user_report
        Me.UserReportB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserReportB.Margin = New System.Windows.Forms.Padding(0, 1, 12, 2)
        Me.UserReportB.Name = "UserReportB"
        Me.UserReportB.Size = New System.Drawing.Size(32, 32)
        Me.UserReportB.ToolTipText = "Report user [B]"
        '
        'RateUpdateTimer
        '
        Me.RateUpdateTimer.Enabled = True
        Me.RateUpdateTimer.Interval = 1000
        '
        'DrawTimer
        '
        Me.DrawTimer.Enabled = True
        Me.DrawTimer.Interval = 1000
        '
        'Main
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(792, 434)
        Me.Controls.Add(Me.ToolContainer)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.TopMenu
        Me.MinimumSize = New System.Drawing.Size(800, 400)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Huggle"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.LogMenu.ResumeLayout(False)
        Me.TrayMenu.ResumeLayout(False)
        Me.TopMenu.ResumeLayout(False)
        Me.TopMenu.PerformLayout()
        Me.TagDeleteMenu.ResumeLayout(False)
        Me.Splitter.Panel1.ResumeLayout(False)
        Me.Splitter.Panel2.ResumeLayout(False)
        Me.Splitter.ResumeLayout(False)
        Me.Tabs.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ToolContainer.ContentPanel.ResumeLayout(False)
        Me.ToolContainer.TopToolStripPanel.ResumeLayout(False)
        Me.ToolContainer.TopToolStripPanel.PerformLayout()
        Me.ToolContainer.ResumeLayout(False)
        Me.ToolContainer.PerformLayout()
        Me.MainStrip.ResumeLayout(False)
        Me.MainStrip.PerformLayout()
        Me.RevertMenu.ResumeLayout(False)
        Me.TemplateMenu.ResumeLayout(False)
        Me.WarnMenu.ResumeLayout(False)
        Me.HistoryStrip.ResumeLayout(False)
        Me.HistoryStrip.PerformLayout()
        Me.NavigationStrip.ResumeLayout(False)
        Me.NavigationStrip.PerformLayout()
        Me.ActionsStrip.ResumeLayout(False)
        Me.ActionsStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RevertTimer As System.Windows.Forms.Timer
    Friend WithEvents ScrollTimer As System.Windows.Forms.Timer
    Friend WithEvents RcReqTimer As System.Windows.Forms.Timer
    Friend WithEvents BlockReqTimer As System.Windows.Forms.Timer
    Friend WithEvents LogMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents LogContextCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents TrayMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TrayRestore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrayExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TopMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuSystem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuQueue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuPage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpDocs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents QueueNext As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents QueueTrim As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueClear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuBrowser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BrowserNewTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BrowserCloseTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BrowserCloseOthers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BrowserBack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BrowserForward As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageViewLatest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PageWatch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserIgnore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageMove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageTagDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserContribs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserTalk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UserMessage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserWarn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserBlock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemShowNewMessages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageRequestProtection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BrowserNewEdits As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BrowserNewContribs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BrowserOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Splitter As System.Windows.Forms.SplitContainer
    Friend WithEvents RateUpdateTimer As System.Windows.Forms.Timer
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents InitialTab As huggle.BrowserTab
    Friend WithEvents UserInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageProtect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DrawTimer As System.Windows.Forms.Timer
    Friend WithEvents EditInfo As huggle.EditInfoPanel
    Friend WithEvents Queue As huggle.QueuePanel
    Friend WithEvents PageView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolContainer As System.Windows.Forms.ToolStripContainer
    Friend WithEvents MainStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents UserIgnoreB As System.Windows.Forms.ToolStripButton
    Friend WithEvents Separator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents NavigationStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents BrowserBackB As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents BrowserForwardB As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents Separator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BrowserOpenB As System.Windows.Forms.ToolStripButton
    Friend WithEvents BrowserNewTabB As System.Windows.Forms.ToolStripButton
    Friend WithEvents BrowserCloseTabB As System.Windows.Forms.ToolStripButton
    Friend WithEvents Separator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HistoryPrevB As System.Windows.Forms.ToolStripButton
    Friend WithEvents HistoryNextB As System.Windows.Forms.ToolStripButton
    Friend WithEvents HistoryLastB As System.Windows.Forms.ToolStripButton
    Friend WithEvents HistoryDiffToCurB As System.Windows.Forms.ToolStripButton
    Friend WithEvents Separator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ContribsPrevB As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContribsNextB As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContribsLastB As System.Windows.Forms.ToolStripButton
    Friend WithEvents HistoryStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents PageB As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents HistoryB As System.Windows.Forms.ToolStripButton
    Friend WithEvents History As System.Windows.Forms.ToolStripLabel
    Friend WithEvents PageLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents UserLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents UserB As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ContribsB As System.Windows.Forms.ToolStripButton
    Friend WithEvents ActionsStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents PageViewB As System.Windows.Forms.ToolStripButton
    Friend WithEvents PageEditB As System.Windows.Forms.ToolStripButton
    Friend WithEvents PageWatchB As System.Windows.Forms.ToolStripButton
    Friend WithEvents Separator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator19 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UserInfoB As System.Windows.Forms.ToolStripButton
    Friend WithEvents UserTalkB As System.Windows.Forms.ToolStripButton
    Friend WithEvents UserMessageB As System.Windows.Forms.ToolStripButton
    Friend WithEvents UserReportB As System.Windows.Forms.ToolStripButton
    Friend WithEvents HistoryScrollLB As System.Windows.Forms.ToolStripButton
    Friend WithEvents HistoryScrollRB As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContribsScrollLB As System.Windows.Forms.ToolStripButton
    Friend WithEvents Contribs As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ContribsScrollRB As System.Windows.Forms.ToolStripButton
    Friend WithEvents Stats As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DiffNextB As System.Windows.Forms.ToolStripButton
    Friend WithEvents PageDeleteB As System.Windows.Forms.ToolStripButton
    Friend WithEvents UndoMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RevertMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DiffRevertSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WarnB As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents WarnMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents WarnAdvanced As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents WarnVandalism As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WarnSpam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WarnTest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WarnDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserTemplateB As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents TemplateMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UserMessageOther As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserMessageWelcome As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DiffRevertB As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents Status As huggle.ListView2
    Friend WithEvents Url As System.Windows.Forms.ColumnHeader
    Friend WithEvents Details As System.Windows.Forms.ColumnHeader
    Friend WithEvents SystemReconnectIRC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpFeedback As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemStats As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WarnAttack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageMarkPatrolled As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UndoB As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents CancelB As System.Windows.Forms.ToolStripButton
    Friend WithEvents TagDeleteMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PageTagSpeedy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator23 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PageTagDeleteB As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents PageNominate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageProd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageTagB As System.Windows.Forms.ToolStripButton
    Friend WithEvents PageTag As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SystemSaveLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents WarnError As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WarnUnsourced As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WarnNpov As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageShowHistoryPage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueSource As System.Windows.Forms.ComboBox
    Friend WithEvents QueueEditSources As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertWarnB As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents RevertWarnVandalism As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertWarnSpam As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertWarnTest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertWarnDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertWarnAttack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertWarnError As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertWarnNpov As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertWarnUnsourced As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator24 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RevertWarnAdvanced As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueScroll As System.Windows.Forms.VScrollBar
    Friend WithEvents PagePurge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator16 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SystemShowLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemShowQueue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GoToMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoMyTalk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoMyContribs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoSeparator As System.Windows.Forms.ToolStripSeparator
End Class
