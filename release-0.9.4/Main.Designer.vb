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
        Me.ScrollTimer = New System.Windows.Forms.Timer(Me.components)
        Me.RevertTimer = New System.Windows.Forms.Timer(Me.components)
        Me.RcReqTimer = New System.Windows.Forms.Timer(Me.components)
        Me.LogMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LogCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.LogClear = New System.Windows.Forms.ToolStripMenuItem
        Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TrayMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TrayRestore = New System.Windows.Forms.ToolStripMenuItem
        Me.TrayExit = New System.Windows.Forms.ToolStripMenuItem
        Me.TopMenu = New System.Windows.Forms.MenuStrip
        Me.MenuSystem = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemMessages = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemReconnectIRC = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemSaveLog = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator8 = New System.Windows.Forms.ToolStripSeparator
        Me.SystemLists = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemRequests = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemStatistics = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator18 = New System.Windows.Forms.ToolStripSeparator
        Me.SystemShowLog = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemShowQueue = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemShowTwoQueues = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemOptions = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator3 = New System.Windows.Forms.ToolStripSeparator
        Me.SystemLogOut = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemExit = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuQueue = New System.Windows.Forms.ToolStripMenuItem
        Me.QueueNext = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator1 = New System.Windows.Forms.ToolStripSeparator
        Me.QueueTrim = New System.Windows.Forms.ToolStripMenuItem
        Me.QueueClear = New System.Windows.Forms.ToolStripMenuItem
        Me.QueueClearAll = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator29 = New System.Windows.Forms.ToolStripSeparator
        Me.QueueOptions = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuGoto = New System.Windows.Forms.ToolStripMenuItem
        Me.GotoMyTalk = New System.Windows.Forms.ToolStripMenuItem
        Me.GotoMyContribs = New System.Windows.Forms.ToolStripMenuItem
        Me.GoSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.MenuRevision = New System.Windows.Forms.ToolStripMenuItem
        Me.RevisionView = New System.Windows.Forms.ToolStripMenuItem
        Me.RevisionRevert = New System.Windows.Forms.ToolStripMenuItem
        Me.RevertMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RevertCurrentOnly = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator28 = New System.Windows.Forms.ToolStripSeparator
        Me.Separator20 = New System.Windows.Forms.ToolStripSeparator
        Me.RevertAdvanced = New System.Windows.Forms.ToolStripMenuItem
        Me.RevisionSight = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator30 = New System.Windows.Forms.ToolStripSeparator
        Me.RevisionPrevious = New System.Windows.Forms.ToolStripMenuItem
        Me.RevisionNext = New System.Windows.Forms.ToolStripMenuItem
        Me.RevisionLatest = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuPage = New System.Windows.Forms.ToolStripMenuItem
        Me.PageSwitchTalk = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator27 = New System.Windows.Forms.ToolStripSeparator
        Me.PageViewLatest = New System.Windows.Forms.ToolStripMenuItem
        Me.PageHistory = New System.Windows.Forms.ToolStripMenuItem
        Me.PageHistoryPage = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator4 = New System.Windows.Forms.ToolStripSeparator
        Me.PageEdit = New System.Windows.Forms.ToolStripMenuItem
        Me.PageTag = New System.Windows.Forms.ToolStripMenuItem
        Me.PageReqDeletion = New System.Windows.Forms.ToolStripMenuItem
        Me.TagDeleteMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PageXfd = New System.Windows.Forms.ToolStripMenuItem
        Me.PageTagProd = New System.Windows.Forms.ToolStripMenuItem
        Me.PageTagSpeedy = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator23 = New System.Windows.Forms.ToolStripSeparator
        Me.PageTagDeleteB = New System.Windows.Forms.ToolStripDropDownButton
        Me.PageReqProtection = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator14 = New System.Windows.Forms.ToolStripSeparator
        Me.PageWatch = New System.Windows.Forms.ToolStripMenuItem
        Me.PagePurge = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator26 = New System.Windows.Forms.ToolStripSeparator
        Me.PagePatrol = New System.Windows.Forms.ToolStripMenuItem
        Me.PageMove = New System.Windows.Forms.ToolStripMenuItem
        Me.PageProtect = New System.Windows.Forms.ToolStripMenuItem
        Me.PageDelete = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuUser = New System.Windows.Forms.ToolStripMenuItem
        Me.UserInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.UserIgnore = New System.Windows.Forms.ToolStripMenuItem
        Me.UserContribs = New System.Windows.Forms.ToolStripMenuItem
        Me.UserTalk = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator5 = New System.Windows.Forms.ToolStripSeparator
        Me.UserMessage = New System.Windows.Forms.ToolStripMenuItem
        Me.UserEmail = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator25 = New System.Windows.Forms.ToolStripSeparator
        Me.UserWarn = New System.Windows.Forms.ToolStripMenuItem
        Me.WarnMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Separator22 = New System.Windows.Forms.ToolStripSeparator
        Me.WarnAdvanced = New System.Windows.Forms.ToolStripMenuItem
        Me.WarnB = New System.Windows.Forms.ToolStripDropDownButton
        Me.UserReport = New System.Windows.Forms.ToolStripMenuItem
        Me.ReportMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UserReportVandalism = New System.Windows.Forms.ToolStripMenuItem
        Me.UserReportUsername = New System.Windows.Forms.ToolStripMenuItem
        Me.UserReport3rr = New System.Windows.Forms.ToolStripMenuItem
        Me.UserReportSock = New System.Windows.Forms.ToolStripMenuItem
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
        Me.HelpDocumentation = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpFeedback = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator16 = New System.Windows.Forms.ToolStripSeparator
        Me.HelpAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStats = New System.Windows.Forms.ToolStripMenuItem
        Me.RevertB = New System.Windows.Forms.ToolStripSplitButton
        Me.UserReportB = New System.Windows.Forms.ToolStripDropDownButton
        Me.QueueScroll2 = New System.Windows.Forms.VScrollBar
        Me.QueueSelector = New System.Windows.Forms.ComboBox
        Me.QueueArea = New Huggle.QueuePanel
        Me.ActionsStrip = New System.Windows.Forms.ToolStrip
        Me.PageViewB = New System.Windows.Forms.ToolStripButton
        Me.PageEditB = New System.Windows.Forms.ToolStripButton
        Me.PageWatchB = New System.Windows.Forms.ToolStripButton
        Me.PageTagB = New System.Windows.Forms.ToolStripButton
        Me.PageDeleteB = New System.Windows.Forms.ToolStripButton
        Me.Separator15 = New System.Windows.Forms.ToolStripSeparator
        Me.UserInfoB = New System.Windows.Forms.ToolStripButton
        Me.UserTalkB = New System.Windows.Forms.ToolStripButton
        Me.UserMessageB = New System.Windows.Forms.ToolStripButton
        Me.UserIgnoreB = New System.Windows.Forms.ToolStripButton
        Me.UserBlockB = New System.Windows.Forms.ToolStripButton
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
        Me.HistoryStrip = New System.Windows.Forms.ToolStrip
        Me.PageLabel = New System.Windows.Forms.ToolStripLabel
        Me.PageB = New System.Windows.Forms.ToolStripComboBox
        Me.HistoryB = New System.Windows.Forms.ToolStripButton
        Me.HistoryScrollLB = New System.Windows.Forms.ToolStripButton
        Me.History = New Huggle.HistoryStrip
        Me.HistoryScrollRB = New System.Windows.Forms.ToolStripButton
        Me.MainStrip = New System.Windows.Forms.ToolStrip
        Me.RevertWarnB = New System.Windows.Forms.ToolStripSplitButton
        Me.RevertWarnMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Separator24 = New System.Windows.Forms.ToolStripSeparator
        Me.RevertWarnAdvanced = New System.Windows.Forms.ToolStripMenuItem
        Me.NextDiffB = New System.Windows.Forms.ToolStripButton
        Me.SightAndNext = New System.Windows.Forms.ToolStripButton
        Me.Separator17 = New System.Windows.Forms.ToolStripSeparator
        Me.TemplateB = New System.Windows.Forms.ToolStripDropDownButton
        Me.TemplateMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UserMessageWelcome = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator13 = New System.Windows.Forms.ToolStripSeparator
        Me.Separator21 = New System.Windows.Forms.ToolStripSeparator
        Me.UserMessageOther = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator9 = New System.Windows.Forms.ToolStripSeparator
        Me.CancelB = New System.Windows.Forms.ToolStripButton
        Me.UndoB = New System.Windows.Forms.ToolStripDropDownButton
        Me.UndoMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Separator19 = New System.Windows.Forms.ToolStripSeparator
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.InitialTabPage = New System.Windows.Forms.TabPage
        Me.InitialTab = New Huggle.BrowserTab
        Me.RateUpdateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.LayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.Status = New Huggle.ListView2
        Me.Url = New System.Windows.Forms.ColumnHeader
        Me.Details = New System.Windows.Forms.ColumnHeader
        Me.ToolbarPanel = New System.Windows.Forms.Panel
        Me.ContribsStrip = New System.Windows.Forms.ToolStrip
        Me.UserLabel = New System.Windows.Forms.ToolStripLabel
        Me.UserB = New System.Windows.Forms.ToolStripComboBox
        Me.ContribsB = New System.Windows.Forms.ToolStripButton
        Me.ContribsScrollLB = New System.Windows.Forms.ToolStripButton
        Me.Contribs = New Huggle.ContribsStrip
        Me.ContribsScrollRB = New System.Windows.Forms.ToolStripButton
        Me.CentralPanel = New System.Windows.Forms.Panel
        Me.QueueContainer = New System.Windows.Forms.SplitContainer
        Me.QueueScroll = New System.Windows.Forms.VScrollBar
        Me.QueueArea2 = New Huggle.QueuePanel
        Me.QueueSelector2 = New System.Windows.Forms.ComboBox
        Me.EditInfo = New Huggle.EditInfoPanel
        Me.LogMenu.SuspendLayout()
        Me.TrayMenu.SuspendLayout()
        Me.TopMenu.SuspendLayout()
        Me.RevertMenu.SuspendLayout()
        Me.TagDeleteMenu.SuspendLayout()
        Me.WarnMenu.SuspendLayout()
        Me.ReportMenu.SuspendLayout()
        Me.ActionsStrip.SuspendLayout()
        Me.NavigationStrip.SuspendLayout()
        Me.HistoryStrip.SuspendLayout()
        Me.MainStrip.SuspendLayout()
        Me.RevertWarnMenu.SuspendLayout()
        Me.TemplateMenu.SuspendLayout()
        Me.Tabs.SuspendLayout()
        Me.InitialTabPage.SuspendLayout()
        Me.LayoutPanel.SuspendLayout()
        Me.ToolbarPanel.SuspendLayout()
        Me.ContribsStrip.SuspendLayout()
        Me.CentralPanel.SuspendLayout()
        Me.QueueContainer.Panel1.SuspendLayout()
        Me.QueueContainer.Panel2.SuspendLayout()
        Me.QueueContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'ScrollTimer
        '
        '
        'RevertTimer
        '
        Me.RevertTimer.Interval = 1000
        '
        'RcReqTimer
        '
        Me.RcReqTimer.Enabled = True
        Me.RcReqTimer.Interval = 2000
        '
        'LogMenu
        '
        Me.LogMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogCopy, Me.LogClear})
        Me.LogMenu.Name = "LogContext"
        Me.LogMenu.Size = New System.Drawing.Size(100, 48)
        '
        'LogCopy
        '
        Me.LogCopy.Name = "LogCopy"
        Me.LogCopy.Size = New System.Drawing.Size(99, 22)
        Me.LogCopy.Text = "Copy"
        '
        'LogClear
        '
        Me.LogClear.Name = "LogClear"
        Me.LogClear.Size = New System.Drawing.Size(99, 22)
        Me.LogClear.Text = "Clear"
        '
        'TrayIcon
        '
        Me.TrayIcon.BalloonTipText = "You have new messages (click to view)."
        Me.TrayIcon.BalloonTipTitle = "huggle"
        Me.TrayIcon.ContextMenuStrip = Me.TrayMenu
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
        Me.TopMenu.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TopMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuSystem, Me.MenuQueue, Me.MenuGoto, Me.MenuRevision, Me.MenuPage, Me.MenuUser, Me.MenuBrowser, Me.MenuHelp, Me.MenuStats})
        Me.TopMenu.Location = New System.Drawing.Point(0, 0)
        Me.TopMenu.Name = "TopMenu"
        Me.TopMenu.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.TopMenu.Size = New System.Drawing.Size(822, 24)
        Me.TopMenu.TabIndex = 0
        '
        'MenuSystem
        '
        Me.MenuSystem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemMessages, Me.SystemReconnectIRC, Me.SystemSaveLog, Me.Separator8, Me.SystemLists, Me.SystemRequests, Me.SystemStatistics, Me.Separator18, Me.SystemShowLog, Me.SystemShowQueue, Me.SystemShowTwoQueues, Me.SystemOptions, Me.Separator3, Me.SystemLogOut, Me.SystemExit})
        Me.MenuSystem.Name = "MenuSystem"
        Me.MenuSystem.Size = New System.Drawing.Size(54, 24)
        Me.MenuSystem.Text = "&System"
        '
        'SystemMessages
        '
        Me.SystemMessages.Enabled = False
        Me.SystemMessages.Name = "SystemMessages"
        Me.SystemMessages.ShortcutKeyDisplayString = ""
        Me.SystemMessages.Size = New System.Drawing.Size(173, 22)
        Me.SystemMessages.Text = "Show new messages"
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
        'Separator8
        '
        Me.Separator8.Name = "Separator8"
        Me.Separator8.Size = New System.Drawing.Size(170, 6)
        '
        'SystemLists
        '
        Me.SystemLists.Name = "SystemLists"
        Me.SystemLists.Size = New System.Drawing.Size(173, 22)
        Me.SystemLists.Text = "List Builder..."
        '
        'SystemRequests
        '
        Me.SystemRequests.Name = "SystemRequests"
        Me.SystemRequests.Size = New System.Drawing.Size(173, 22)
        Me.SystemRequests.Text = "Requests..."
        '
        'SystemStatistics
        '
        Me.SystemStatistics.Name = "SystemStatistics"
        Me.SystemStatistics.Size = New System.Drawing.Size(173, 22)
        Me.SystemStatistics.Text = "Statistics..."
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
        Me.SystemShowQueue.Checked = True
        Me.SystemShowQueue.CheckOnClick = True
        Me.SystemShowQueue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SystemShowQueue.Name = "SystemShowQueue"
        Me.SystemShowQueue.Size = New System.Drawing.Size(173, 22)
        Me.SystemShowQueue.Text = "Show queue"
        '
        'SystemShowTwoQueues
        '
        Me.SystemShowTwoQueues.CheckOnClick = True
        Me.SystemShowTwoQueues.Name = "SystemShowTwoQueues"
        Me.SystemShowTwoQueues.Size = New System.Drawing.Size(173, 22)
        Me.SystemShowTwoQueues.Text = "Show two queues"
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
        'SystemLogOut
        '
        Me.SystemLogOut.Name = "SystemLogOut"
        Me.SystemLogOut.Size = New System.Drawing.Size(173, 22)
        Me.SystemLogOut.Text = "Log out"
        '
        'SystemExit
        '
        Me.SystemExit.Name = "SystemExit"
        Me.SystemExit.Size = New System.Drawing.Size(173, 22)
        Me.SystemExit.Text = "Exit"
        '
        'MenuQueue
        '
        Me.MenuQueue.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QueueNext, Me.Separator1, Me.QueueTrim, Me.QueueClear, Me.QueueClearAll, Me.Separator29, Me.QueueOptions})
        Me.MenuQueue.Name = "MenuQueue"
        Me.MenuQueue.Size = New System.Drawing.Size(51, 24)
        Me.MenuQueue.Text = "&Queue"
        '
        'QueueNext
        '
        Me.QueueNext.Enabled = False
        Me.QueueNext.Name = "QueueNext"
        Me.QueueNext.ShortcutKeyDisplayString = ""
        Me.QueueNext.Size = New System.Drawing.Size(164, 22)
        Me.QueueNext.Text = "Next"
        '
        'Separator1
        '
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Size = New System.Drawing.Size(161, 6)
        '
        'QueueTrim
        '
        Me.QueueTrim.Enabled = False
        Me.QueueTrim.Name = "QueueTrim"
        Me.QueueTrim.Size = New System.Drawing.Size(164, 22)
        Me.QueueTrim.Text = "Trim..."
        '
        'QueueClear
        '
        Me.QueueClear.Enabled = False
        Me.QueueClear.Name = "QueueClear"
        Me.QueueClear.Size = New System.Drawing.Size(164, 22)
        Me.QueueClear.Text = "Clear current"
        '
        'QueueClearAll
        '
        Me.QueueClearAll.Name = "QueueClearAll"
        Me.QueueClearAll.Size = New System.Drawing.Size(164, 22)
        Me.QueueClearAll.Text = "Clear all"
        '
        'Separator29
        '
        Me.Separator29.Name = "Separator29"
        Me.Separator29.Size = New System.Drawing.Size(161, 6)
        '
        'QueueOptions
        '
        Me.QueueOptions.Name = "QueueOptions"
        Me.QueueOptions.Size = New System.Drawing.Size(164, 22)
        Me.QueueOptions.Text = "Manage Queues..."
        '
        'MenuGoto
        '
        Me.MenuGoto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GotoMyTalk, Me.GotoMyContribs, Me.GoSeparator})
        Me.MenuGoto.Name = "MenuGoto"
        Me.MenuGoto.Size = New System.Drawing.Size(45, 24)
        Me.MenuGoto.Text = "&Go to"
        '
        'GotoMyTalk
        '
        Me.GotoMyTalk.Name = "GotoMyTalk"
        Me.GotoMyTalk.Size = New System.Drawing.Size(153, 22)
        Me.GotoMyTalk.Text = "My talk page"
        '
        'GotoMyContribs
        '
        Me.GotoMyContribs.Name = "GotoMyContribs"
        Me.GotoMyContribs.Size = New System.Drawing.Size(153, 22)
        Me.GotoMyContribs.Text = "My contributions"
        '
        'GoSeparator
        '
        Me.GoSeparator.Name = "GoSeparator"
        Me.GoSeparator.Size = New System.Drawing.Size(150, 6)
        '
        'MenuRevision
        '
        Me.MenuRevision.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RevisionView, Me.RevisionRevert, Me.RevisionSight, Me.Separator30, Me.RevisionPrevious, Me.RevisionNext, Me.RevisionLatest})
        Me.MenuRevision.Enabled = False
        Me.MenuRevision.Name = "MenuRevision"
        Me.MenuRevision.Size = New System.Drawing.Size(59, 24)
        Me.MenuRevision.Text = "&Revision"
        '
        'RevisionView
        '
        Me.RevisionView.Name = "RevisionView"
        Me.RevisionView.Size = New System.Drawing.Size(152, 22)
        Me.RevisionView.Text = "View"
        '
        'RevisionRevert
        '
        Me.RevisionRevert.DropDown = Me.RevertMenu
        Me.RevisionRevert.Name = "RevisionRevert"
        Me.RevisionRevert.Size = New System.Drawing.Size(152, 22)
        Me.RevisionRevert.Text = "Revert"
        '
        'RevertMenu
        '
        Me.RevertMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RevertCurrentOnly, Me.Separator28, Me.Separator20, Me.RevertAdvanced})
        Me.RevertMenu.Name = "RevertMenu"
        Me.RevertMenu.OwnerItem = Me.RevertB
        Me.RevertMenu.Size = New System.Drawing.Size(191, 60)
        '
        'RevertCurrentOnly
        '
        Me.RevertCurrentOnly.Name = "RevertCurrentOnly"
        Me.RevertCurrentOnly.Size = New System.Drawing.Size(190, 22)
        Me.RevertCurrentOnly.Text = "Revert only this revision"
        '
        'Separator28
        '
        Me.Separator28.Name = "Separator28"
        Me.Separator28.Size = New System.Drawing.Size(187, 6)
        '
        'Separator20
        '
        Me.Separator20.Name = "Separator20"
        Me.Separator20.Size = New System.Drawing.Size(187, 6)
        '
        'RevertAdvanced
        '
        Me.RevertAdvanced.Name = "RevertAdvanced"
        Me.RevertAdvanced.ShortcutKeyDisplayString = "Y"
        Me.RevertAdvanced.Size = New System.Drawing.Size(190, 22)
        Me.RevertAdvanced.Text = "Advanced..."
        '
        'RevisionSight
        '
        Me.RevisionSight.Name = "RevisionSight"
        Me.RevisionSight.Size = New System.Drawing.Size(152, 22)
        Me.RevisionSight.Text = "Sight"
        '
        'Separator30
        '
        Me.Separator30.Name = "Separator30"
        Me.Separator30.Size = New System.Drawing.Size(149, 6)
        '
        'RevisionPrevious
        '
        Me.RevisionPrevious.Name = "RevisionPrevious"
        Me.RevisionPrevious.Size = New System.Drawing.Size(152, 22)
        Me.RevisionPrevious.Text = "Previous"
        '
        'RevisionNext
        '
        Me.RevisionNext.Name = "RevisionNext"
        Me.RevisionNext.Size = New System.Drawing.Size(152, 22)
        Me.RevisionNext.Text = "Next"
        '
        'RevisionLatest
        '
        Me.RevisionLatest.Name = "RevisionLatest"
        Me.RevisionLatest.Size = New System.Drawing.Size(152, 22)
        Me.RevisionLatest.Text = "Latest"
        '
        'MenuPage
        '
        Me.MenuPage.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageSwitchTalk, Me.Separator27, Me.PageViewLatest, Me.PageHistory, Me.PageHistoryPage, Me.Separator4, Me.PageEdit, Me.PageTag, Me.PageReqDeletion, Me.PageReqProtection, Me.Separator14, Me.PageWatch, Me.PagePurge, Me.Separator26, Me.PagePatrol, Me.PageMove, Me.PageProtect, Me.PageDelete})
        Me.MenuPage.Enabled = False
        Me.MenuPage.Name = "MenuPage"
        Me.MenuPage.Size = New System.Drawing.Size(43, 24)
        Me.MenuPage.Text = "&Page"
        '
        'PageSwitchTalk
        '
        Me.PageSwitchTalk.Name = "PageSwitchTalk"
        Me.PageSwitchTalk.Size = New System.Drawing.Size(178, 22)
        Me.PageSwitchTalk.Text = "Switch to talk page"
        '
        'Separator27
        '
        Me.Separator27.Name = "Separator27"
        Me.Separator27.Size = New System.Drawing.Size(175, 6)
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
        'PageHistoryPage
        '
        Me.PageHistoryPage.Name = "PageHistoryPage"
        Me.PageHistoryPage.Size = New System.Drawing.Size(178, 22)
        Me.PageHistoryPage.Text = "Show history page"
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
        'PageReqDeletion
        '
        Me.PageReqDeletion.DropDown = Me.TagDeleteMenu
        Me.PageReqDeletion.Name = "PageReqDeletion"
        Me.PageReqDeletion.Size = New System.Drawing.Size(178, 22)
        Me.PageReqDeletion.Text = "Request deletion"
        '
        'TagDeleteMenu
        '
        Me.TagDeleteMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageXfd, Me.PageTagProd, Me.PageTagSpeedy, Me.Separator23})
        Me.TagDeleteMenu.Name = "SpeedyMenu"
        Me.TagDeleteMenu.OwnerItem = Me.PageReqDeletion
        Me.TagDeleteMenu.Size = New System.Drawing.Size(190, 76)
        '
        'PageXfd
        '
        Me.PageXfd.Name = "PageXfd"
        Me.PageXfd.ShortcutKeyDisplayString = ""
        Me.PageXfd.Size = New System.Drawing.Size(189, 22)
        Me.PageXfd.Text = "Nominate for deletion..."
        '
        'PageTagProd
        '
        Me.PageTagProd.Name = "PageTagProd"
        Me.PageTagProd.ShortcutKeyDisplayString = ""
        Me.PageTagProd.Size = New System.Drawing.Size(189, 22)
        Me.PageTagProd.Text = "Proposed deletion..."
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
        Me.PageTagDeleteB.Image = Global.Huggle.My.Resources.Resources.page_tag_delete
        Me.PageTagDeleteB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageTagDeleteB.Name = "PageTagDeleteB"
        Me.PageTagDeleteB.ShowDropDownArrow = False
        Me.PageTagDeleteB.Size = New System.Drawing.Size(32, 32)
        Me.PageTagDeleteB.ToolTipText = "Tag this page for deletion [S]"
        '
        'PageReqProtection
        '
        Me.PageReqProtection.Name = "PageReqProtection"
        Me.PageReqProtection.Size = New System.Drawing.Size(178, 22)
        Me.PageReqProtection.Text = "Request protection..."
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
        'Separator26
        '
        Me.Separator26.Name = "Separator26"
        Me.Separator26.Size = New System.Drawing.Size(175, 6)
        '
        'PagePatrol
        '
        Me.PagePatrol.Name = "PagePatrol"
        Me.PagePatrol.Size = New System.Drawing.Size(178, 22)
        Me.PagePatrol.Text = "Mark patrolled"
        '
        'PageMove
        '
        Me.PageMove.Name = "PageMove"
        Me.PageMove.Size = New System.Drawing.Size(178, 22)
        Me.PageMove.Text = "Move..."
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
        Me.MenuUser.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserInfo, Me.UserIgnore, Me.UserContribs, Me.UserTalk, Me.Separator5, Me.UserMessage, Me.UserEmail, Me.Separator25, Me.UserWarn, Me.UserReport, Me.UserBlock})
        Me.MenuUser.Enabled = False
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
        'UserEmail
        '
        Me.UserEmail.Name = "UserEmail"
        Me.UserEmail.Size = New System.Drawing.Size(180, 22)
        Me.UserEmail.Text = "E-mail..."
        '
        'Separator25
        '
        Me.Separator25.Name = "Separator25"
        Me.Separator25.Size = New System.Drawing.Size(177, 6)
        '
        'UserWarn
        '
        Me.UserWarn.DropDown = Me.WarnMenu
        Me.UserWarn.Name = "UserWarn"
        Me.UserWarn.ShortcutKeyDisplayString = ""
        Me.UserWarn.Size = New System.Drawing.Size(180, 22)
        Me.UserWarn.Text = "Warn"
        '
        'WarnMenu
        '
        Me.WarnMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Separator22, Me.WarnAdvanced})
        Me.WarnMenu.Name = "WarnMenu"
        Me.WarnMenu.OwnerItem = Me.UserWarn
        Me.WarnMenu.Size = New System.Drawing.Size(135, 32)
        '
        'Separator22
        '
        Me.Separator22.Name = "Separator22"
        Me.Separator22.Size = New System.Drawing.Size(131, 6)
        '
        'WarnAdvanced
        '
        Me.WarnAdvanced.Name = "WarnAdvanced"
        Me.WarnAdvanced.ShortcutKeyDisplayString = ""
        Me.WarnAdvanced.Size = New System.Drawing.Size(134, 22)
        Me.WarnAdvanced.Text = "&Advanced..."
        '
        'WarnB
        '
        Me.WarnB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.WarnB.DropDown = Me.WarnMenu
        Me.WarnB.Enabled = False
        Me.WarnB.Image = Global.Huggle.My.Resources.Resources.user_warn
        Me.WarnB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WarnB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.WarnB.Name = "WarnB"
        Me.WarnB.Padding = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.WarnB.ShowDropDownArrow = False
        Me.WarnB.Size = New System.Drawing.Size(41, 52)
        Me.WarnB.ToolTipText = "Warn user"
        '
        'UserReport
        '
        Me.UserReport.DropDown = Me.ReportMenu
        Me.UserReport.Name = "UserReport"
        Me.UserReport.ShortcutKeyDisplayString = ""
        Me.UserReport.Size = New System.Drawing.Size(180, 22)
        Me.UserReport.Text = "Report"
        '
        'ReportMenu
        '
        Me.ReportMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserReportVandalism, Me.UserReportUsername, Me.UserReport3rr, Me.UserReportSock})
        Me.ReportMenu.Name = "ReportMenu"
        Me.ReportMenu.OwnerItem = Me.UserReportB
        Me.ReportMenu.Size = New System.Drawing.Size(213, 92)
        '
        'UserReportVandalism
        '
        Me.UserReportVandalism.Name = "UserReportVandalism"
        Me.UserReportVandalism.Size = New System.Drawing.Size(212, 22)
        Me.UserReportVandalism.Text = "Vandalism after final warning"
        '
        'UserReportUsername
        '
        Me.UserReportUsername.Name = "UserReportUsername"
        Me.UserReportUsername.Size = New System.Drawing.Size(212, 22)
        Me.UserReportUsername.Text = "Inappropriate username"
        '
        'UserReport3rr
        '
        Me.UserReport3rr.Name = "UserReport3rr"
        Me.UserReport3rr.Size = New System.Drawing.Size(212, 22)
        Me.UserReport3rr.Text = "Three-revert rule violation"
        '
        'UserReportSock
        '
        Me.UserReportSock.Name = "UserReportSock"
        Me.UserReportSock.Size = New System.Drawing.Size(212, 22)
        Me.UserReportSock.Text = "Abuse of multiple accounts"
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
        Me.BrowserCloseTab.Enabled = False
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
        Me.MenuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpDocumentation, Me.HelpFeedback, Me.Separator16, Me.HelpAbout})
        Me.MenuHelp.Name = "MenuHelp"
        Me.MenuHelp.Size = New System.Drawing.Size(40, 24)
        Me.MenuHelp.Text = "&Help"
        '
        'HelpDocumentation
        '
        Me.HelpDocumentation.Name = "HelpDocumentation"
        Me.HelpDocumentation.ShortcutKeyDisplayString = ""
        Me.HelpDocumentation.Size = New System.Drawing.Size(151, 22)
        Me.HelpDocumentation.Text = "Documentation"
        '
        'HelpFeedback
        '
        Me.HelpFeedback.Name = "HelpFeedback"
        Me.HelpFeedback.Size = New System.Drawing.Size(151, 22)
        Me.HelpFeedback.Text = "Feedback"
        '
        'Separator16
        '
        Me.Separator16.Name = "Separator16"
        Me.Separator16.Size = New System.Drawing.Size(148, 6)
        '
        'HelpAbout
        '
        Me.HelpAbout.Name = "HelpAbout"
        Me.HelpAbout.Size = New System.Drawing.Size(151, 22)
        Me.HelpAbout.Text = "About Huggle..."
        '
        'MenuStats
        '
        Me.MenuStats.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.MenuStats.Name = "MenuStats"
        Me.MenuStats.Size = New System.Drawing.Size(22, 24)
        Me.MenuStats.Text = " "
        '
        'RevertB
        '
        Me.RevertB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RevertB.DropDown = Me.RevertMenu
        Me.RevertB.DropDownButtonWidth = 16
        Me.RevertB.Enabled = False
        Me.RevertB.Image = Global.Huggle.My.Resources.Resources.diff_revert
        Me.RevertB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.RevertB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RevertB.Name = "RevertB"
        Me.RevertB.Size = New System.Drawing.Size(57, 52)
        Me.RevertB.ToolTipText = "Revert this revision [R]"
        '
        'UserReportB
        '
        Me.UserReportB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserReportB.DropDown = Me.ReportMenu
        Me.UserReportB.Enabled = False
        Me.UserReportB.Image = Global.Huggle.My.Resources.Resources.user_report
        Me.UserReportB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserReportB.Name = "UserReportB"
        Me.UserReportB.ShowDropDownArrow = False
        Me.UserReportB.Size = New System.Drawing.Size(32, 32)
        Me.UserReportB.ToolTipText = "Report user [B]"
        '
        'QueueScroll2
        '
        Me.QueueScroll2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueScroll2.Enabled = False
        Me.QueueScroll2.LargeChange = 0
        Me.QueueScroll2.Location = New System.Drawing.Point(178, 27)
        Me.QueueScroll2.Maximum = 0
        Me.QueueScroll2.Name = "QueueScroll2"
        Me.QueueScroll2.Size = New System.Drawing.Size(18, 68)
        Me.QueueScroll2.SmallChange = 0
        Me.QueueScroll2.TabIndex = 2
        '
        'QueueSelector
        '
        Me.QueueSelector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.QueueSelector.FormattingEnabled = True
        Me.QueueSelector.Location = New System.Drawing.Point(3, 4)
        Me.QueueSelector.MaxDropDownItems = 20
        Me.QueueSelector.Name = "QueueSelector"
        Me.QueueSelector.Size = New System.Drawing.Size(173, 21)
        Me.QueueSelector.TabIndex = 0
        '
        'QueueArea
        '
        Me.QueueArea.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueArea.BackColor = System.Drawing.SystemColors.Control
        Me.QueueArea.Location = New System.Drawing.Point(0, 27)
        Me.QueueArea.Margin = New System.Windows.Forms.Padding(0)
        Me.QueueArea.Name = "QueueArea"
        Me.QueueArea.Size = New System.Drawing.Size(174, 97)
        Me.QueueArea.TabIndex = 1
        '
        'ActionsStrip
        '
        Me.ActionsStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.ActionsStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ActionsStrip.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.ActionsStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageViewB, Me.PageEditB, Me.PageWatchB, Me.PageTagB, Me.PageTagDeleteB, Me.PageDeleteB, Me.Separator15, Me.UserInfoB, Me.UserTalkB, Me.UserMessageB, Me.UserIgnoreB, Me.UserReportB, Me.UserBlockB})
        Me.ActionsStrip.Location = New System.Drawing.Point(428, 55)
        Me.ActionsStrip.Name = "ActionsStrip"
        Me.ActionsStrip.Size = New System.Drawing.Size(393, 35)
        Me.ActionsStrip.TabIndex = 4
        '
        'PageViewB
        '
        Me.PageViewB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageViewB.Enabled = False
        Me.PageViewB.Image = Global.Huggle.My.Resources.Resources.page_view
        Me.PageViewB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageViewB.Name = "PageViewB"
        Me.PageViewB.Size = New System.Drawing.Size(32, 32)
        Me.PageViewB.ToolTipText = "View this revision [V]"
        '
        'PageEditB
        '
        Me.PageEditB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageEditB.Enabled = False
        Me.PageEditB.Image = Global.Huggle.My.Resources.Resources.page_edit
        Me.PageEditB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageEditB.Name = "PageEditB"
        Me.PageEditB.Size = New System.Drawing.Size(32, 32)
        Me.PageEditB.ToolTipText = "Edit this page [E]"
        '
        'PageWatchB
        '
        Me.PageWatchB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageWatchB.Enabled = False
        Me.PageWatchB.Image = Global.Huggle.My.Resources.Resources.page_watch
        Me.PageWatchB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageWatchB.Name = "PageWatchB"
        Me.PageWatchB.Size = New System.Drawing.Size(32, 32)
        Me.PageWatchB.ToolTipText = "Watch this page [L]"
        '
        'PageTagB
        '
        Me.PageTagB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageTagB.Enabled = False
        Me.PageTagB.Image = Global.Huggle.My.Resources.Resources.page_tag
        Me.PageTagB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageTagB.Name = "PageTagB"
        Me.PageTagB.Size = New System.Drawing.Size(32, 32)
        Me.PageTagB.ToolTipText = "Tag this page [G]"
        '
        'PageDeleteB
        '
        Me.PageDeleteB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PageDeleteB.Enabled = False
        Me.PageDeleteB.Image = Global.Huggle.My.Resources.Resources.page_delete
        Me.PageDeleteB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PageDeleteB.Name = "PageDeleteB"
        Me.PageDeleteB.Size = New System.Drawing.Size(32, 32)
        Me.PageDeleteB.ToolTipText = "Delete this page [S]"
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
        Me.UserInfoB.Image = Global.Huggle.My.Resources.Resources.user_info
        Me.UserInfoB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserInfoB.Name = "UserInfoB"
        Me.UserInfoB.Size = New System.Drawing.Size(32, 32)
        Me.UserInfoB.ToolTipText = "Show user information [?]"
        '
        'UserTalkB
        '
        Me.UserTalkB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserTalkB.Enabled = False
        Me.UserTalkB.Image = Global.Huggle.My.Resources.Resources.user_talk
        Me.UserTalkB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserTalkB.Name = "UserTalkB"
        Me.UserTalkB.Size = New System.Drawing.Size(32, 32)
        Me.UserTalkB.ToolTipText = "Show user talk page [U]"
        '
        'UserMessageB
        '
        Me.UserMessageB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserMessageB.Enabled = False
        Me.UserMessageB.Image = Global.Huggle.My.Resources.Resources.user_message
        Me.UserMessageB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserMessageB.Name = "UserMessageB"
        Me.UserMessageB.Size = New System.Drawing.Size(32, 32)
        Me.UserMessageB.ToolTipText = "Message user [N]"
        '
        'UserIgnoreB
        '
        Me.UserIgnoreB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserIgnoreB.Enabled = False
        Me.UserIgnoreB.Image = Global.Huggle.My.Resources.Resources.user_whitelist
        Me.UserIgnoreB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserIgnoreB.Name = "UserIgnoreB"
        Me.UserIgnoreB.Size = New System.Drawing.Size(32, 32)
        Me.UserIgnoreB.ToolTipText = "Ignore all contributions by this user [I]"
        '
        'UserBlockB
        '
        Me.UserBlockB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.UserBlockB.Enabled = False
        Me.UserBlockB.Image = Global.Huggle.My.Resources.Resources.user_block
        Me.UserBlockB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.UserBlockB.Name = "UserBlockB"
        Me.UserBlockB.Size = New System.Drawing.Size(32, 32)
        Me.UserBlockB.ToolTipText = "Block user [Ctrl + B]"
        '
        'NavigationStrip
        '
        Me.NavigationStrip.AllowItemReorder = True
        Me.NavigationStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.NavigationStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.NavigationStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BrowserBackB, Me.BrowserForwardB, Me.Separator10, Me.BrowserOpenB, Me.BrowserNewTabB, Me.BrowserCloseTabB, Me.Separator11, Me.HistoryPrevB, Me.HistoryNextB, Me.HistoryLastB, Me.HistoryDiffToCurB, Me.Separator12, Me.ContribsPrevB, Me.ContribsNextB, Me.ContribsLastB})
        Me.NavigationStrip.Location = New System.Drawing.Point(1, 55)
        Me.NavigationStrip.Name = "NavigationStrip"
        Me.NavigationStrip.Size = New System.Drawing.Size(426, 35)
        Me.NavigationStrip.TabIndex = 1
        '
        'BrowserBackB
        '
        Me.BrowserBackB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BrowserBackB.DropDownButtonWidth = 12
        Me.BrowserBackB.Enabled = False
        Me.BrowserBackB.Image = Global.Huggle.My.Resources.Resources.browser_prev
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
        Me.BrowserForwardB.Image = Global.Huggle.My.Resources.Resources.browser_next
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
        Me.BrowserOpenB.Image = Global.Huggle.My.Resources.Resources.browser_open
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
        Me.BrowserNewTabB.Image = Global.Huggle.My.Resources.Resources.browser_add_tab
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
        Me.BrowserCloseTabB.Image = Global.Huggle.My.Resources.Resources.browser_remove_tab
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
        Me.HistoryPrevB.Image = Global.Huggle.My.Resources.Resources.history_previous
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
        Me.HistoryNextB.Image = Global.Huggle.My.Resources.Resources.history_next
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
        Me.HistoryLastB.Image = Global.Huggle.My.Resources.Resources.history_last
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
        Me.HistoryDiffToCurB.Image = Global.Huggle.My.Resources.Resources.history_to_cur
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
        Me.ContribsPrevB.Image = Global.Huggle.My.Resources.Resources.contribs_prev
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
        Me.ContribsNextB.Image = Global.Huggle.My.Resources.Resources.contribs_next
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
        Me.ContribsLastB.Image = Global.Huggle.My.Resources.Resources.contribs_last
        Me.ContribsLastB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContribsLastB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ContribsLastB.Name = "ContribsLastB"
        Me.ContribsLastB.Size = New System.Drawing.Size(32, 32)
        Me.ContribsLastB.ToolTipText = "Show latest revision by this user [Ctrl + C]"
        '
        'HistoryStrip
        '
        Me.HistoryStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.HistoryStrip.GripMargin = New System.Windows.Forms.Padding(0)
        Me.HistoryStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HistoryStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageLabel, Me.PageB, Me.HistoryB, Me.HistoryScrollLB, Me.History, Me.HistoryScrollRB})
        Me.HistoryStrip.Location = New System.Drawing.Point(412, 2)
        Me.HistoryStrip.Name = "HistoryStrip"
        Me.HistoryStrip.Padding = New System.Windows.Forms.Padding(0)
        Me.HistoryStrip.Size = New System.Drawing.Size(407, 25)
        Me.HistoryStrip.TabIndex = 2
        '
        'PageLabel
        '
        Me.PageLabel.AutoSize = False
        Me.PageLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PageLabel.Margin = New System.Windows.Forms.Padding(1, 6, 1, 0)
        Me.PageLabel.Name = "PageLabel"
        Me.PageLabel.Size = New System.Drawing.Size(40, 17)
        Me.PageLabel.Text = "Page"
        Me.PageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.HistoryScrollLB.Image = Global.Huggle.My.Resources.Resources.gray_previous
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
        Me.History.Size = New System.Drawing.Size(95, 20)
        '
        'HistoryScrollRB
        '
        Me.HistoryScrollRB.AutoSize = False
        Me.HistoryScrollRB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HistoryScrollRB.Enabled = False
        Me.HistoryScrollRB.Image = Global.Huggle.My.Resources.Resources.gray_next
        Me.HistoryScrollRB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.HistoryScrollRB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HistoryScrollRB.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.HistoryScrollRB.Name = "HistoryScrollRB"
        Me.HistoryScrollRB.Size = New System.Drawing.Size(23, 21)
        '
        'MainStrip
        '
        Me.MainStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.MainStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.MainStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RevertWarnB, Me.NextDiffB, Me.SightAndNext, Me.Separator17, Me.RevertB, Me.TemplateB, Me.WarnB, Me.Separator9, Me.CancelB, Me.UndoB, Me.Separator19})
        Me.MainStrip.Location = New System.Drawing.Point(1, 0)
        Me.MainStrip.Name = "MainStrip"
        Me.MainStrip.Size = New System.Drawing.Size(410, 55)
        Me.MainStrip.TabIndex = 0
        '
        'RevertWarnB
        '
        Me.RevertWarnB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RevertWarnB.DropDown = Me.RevertWarnMenu
        Me.RevertWarnB.DropDownButtonWidth = 16
        Me.RevertWarnB.Enabled = False
        Me.RevertWarnB.Image = Global.Huggle.My.Resources.Resources.diff_revert_warn
        Me.RevertWarnB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.RevertWarnB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RevertWarnB.Name = "RevertWarnB"
        Me.RevertWarnB.Size = New System.Drawing.Size(69, 52)
        Me.RevertWarnB.ToolTipText = "Revert this revision, and issue a user warning [Q]"
        '
        'RevertWarnMenu
        '
        Me.RevertWarnMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Separator24, Me.RevertWarnAdvanced})
        Me.RevertWarnMenu.Name = "WarnMenu"
        Me.RevertWarnMenu.OwnerItem = Me.RevertWarnB
        Me.RevertWarnMenu.Size = New System.Drawing.Size(135, 32)
        '
        'Separator24
        '
        Me.Separator24.Name = "Separator24"
        Me.Separator24.Size = New System.Drawing.Size(131, 6)
        '
        'RevertWarnAdvanced
        '
        Me.RevertWarnAdvanced.Name = "RevertWarnAdvanced"
        Me.RevertWarnAdvanced.Size = New System.Drawing.Size(134, 22)
        Me.RevertWarnAdvanced.Text = "&Advanced..."
        '
        'NextDiffB
        '
        Me.NextDiffB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NextDiffB.Enabled = False
        Me.NextDiffB.Image = Global.Huggle.My.Resources.Resources.diff_next
        Me.NextDiffB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.NextDiffB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NextDiffB.Name = "NextDiffB"
        Me.NextDiffB.Size = New System.Drawing.Size(52, 52)
        Me.NextDiffB.ToolTipText = "Show the next revision in the queue [Space]"
        '
        'SightAndNext
        '
        Me.SightAndNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SightAndNext.Enabled = False
        Me.SightAndNext.Image = Global.Huggle.My.Resources.Resources.sight_and_next
        Me.SightAndNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SightAndNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SightAndNext.Name = "SightAndNext"
        Me.SightAndNext.Size = New System.Drawing.Size(52, 52)
        '
        'Separator17
        '
        Me.Separator17.Name = "Separator17"
        Me.Separator17.Size = New System.Drawing.Size(6, 55)
        '
        'TemplateB
        '
        Me.TemplateB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TemplateB.DropDown = Me.TemplateMenu
        Me.TemplateB.Enabled = False
        Me.TemplateB.Image = Global.Huggle.My.Resources.Resources.user_template
        Me.TemplateB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TemplateB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TemplateB.Name = "TemplateB"
        Me.TemplateB.Padding = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.TemplateB.ShowDropDownArrow = False
        Me.TemplateB.Size = New System.Drawing.Size(42, 52)
        Me.TemplateB.ToolTipText = "Send template message to user [T]"
        '
        'TemplateMenu
        '
        Me.TemplateMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserMessageWelcome, Me.Separator13, Me.Separator21, Me.UserMessageOther})
        Me.TemplateMenu.Name = "TemplateMenu"
        Me.TemplateMenu.OwnerItem = Me.TemplateB
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
        'Separator9
        '
        Me.Separator9.Name = "Separator9"
        Me.Separator9.Size = New System.Drawing.Size(6, 55)
        '
        'CancelB
        '
        Me.CancelB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CancelB.Enabled = False
        Me.CancelB.Image = Global.Huggle.My.Resources.Resources.cancel_all
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
        Me.UndoB.Image = Global.Huggle.My.Resources.Resources.undo
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
        'Tabs
        '
        Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Controls.Add(Me.InitialTabPage)
        Me.Tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.Tabs.ItemSize = New System.Drawing.Size(1, 1)
        Me.Tabs.Location = New System.Drawing.Point(197, 0)
        Me.Tabs.Margin = New System.Windows.Forms.Padding(1)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.Padding = New System.Drawing.Point(0, 0)
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(625, 229)
        Me.Tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.Tabs.TabIndex = 1
        '
        'InitialTabPage
        '
        Me.InitialTabPage.Controls.Add(Me.InitialTab)
        Me.InitialTabPage.Location = New System.Drawing.Point(4, 5)
        Me.InitialTabPage.Name = "InitialTabPage"
        Me.InitialTabPage.Size = New System.Drawing.Size(617, 220)
        Me.InitialTabPage.TabIndex = 0
        Me.InitialTabPage.UseVisualStyleBackColor = True
        '
        'InitialTab
        '
        Me.InitialTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InitialTab.Location = New System.Drawing.Point(0, 0)
        Me.InitialTab.Margin = New System.Windows.Forms.Padding(0)
        Me.InitialTab.Name = "InitialTab"
        Me.InitialTab.Size = New System.Drawing.Size(617, 220)
        Me.InitialTab.TabIndex = 0
        '
        'RateUpdateTimer
        '
        Me.RateUpdateTimer.Enabled = True
        Me.RateUpdateTimer.Interval = 1000
        '
        'LayoutPanel
        '
        Me.LayoutPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LayoutPanel.ColumnCount = 1
        Me.LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.LayoutPanel.Controls.Add(Me.Status, 0, 2)
        Me.LayoutPanel.Controls.Add(Me.ToolbarPanel, 0, 0)
        Me.LayoutPanel.Controls.Add(Me.CentralPanel, 0, 1)
        Me.LayoutPanel.Location = New System.Drawing.Point(0, 27)
        Me.LayoutPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutPanel.Name = "LayoutPanel"
        Me.LayoutPanel.RowCount = 3
        Me.LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 91.0!))
        Me.LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88.0!))
        Me.LayoutPanel.Size = New System.Drawing.Size(822, 406)
        Me.LayoutPanel.TabIndex = 1
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
        Me.Status.Location = New System.Drawing.Point(0, 318)
        Me.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.Status.MultiSelect = False
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(822, 88)
        Me.Status.TabIndex = 2
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
        'ToolbarPanel
        '
        Me.ToolbarPanel.BackColor = System.Drawing.SystemColors.Control
        Me.ToolbarPanel.Controls.Add(Me.NavigationStrip)
        Me.ToolbarPanel.Controls.Add(Me.ActionsStrip)
        Me.ToolbarPanel.Controls.Add(Me.MainStrip)
        Me.ToolbarPanel.Controls.Add(Me.HistoryStrip)
        Me.ToolbarPanel.Controls.Add(Me.ContribsStrip)
        Me.ToolbarPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolbarPanel.Location = New System.Drawing.Point(0, 0)
        Me.ToolbarPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.ToolbarPanel.Name = "ToolbarPanel"
        Me.ToolbarPanel.Size = New System.Drawing.Size(822, 91)
        Me.ToolbarPanel.TabIndex = 0
        '
        'ContribsStrip
        '
        Me.ContribsStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.ContribsStrip.GripMargin = New System.Windows.Forms.Padding(0)
        Me.ContribsStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ContribsStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UserLabel, Me.UserB, Me.ContribsB, Me.ContribsScrollLB, Me.Contribs, Me.ContribsScrollRB})
        Me.ContribsStrip.Location = New System.Drawing.Point(412, 28)
        Me.ContribsStrip.Name = "ContribsStrip"
        Me.ContribsStrip.Size = New System.Drawing.Size(408, 25)
        Me.ContribsStrip.TabIndex = 3
        '
        'UserLabel
        '
        Me.UserLabel.AutoSize = False
        Me.UserLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserLabel.Margin = New System.Windows.Forms.Padding(1, 6, 1, 0)
        Me.UserLabel.Name = "UserLabel"
        Me.UserLabel.Size = New System.Drawing.Size(40, 17)
        Me.UserLabel.Text = "User"
        Me.UserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.ContribsScrollLB.Image = Global.Huggle.My.Resources.Resources.gray_previous
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
        Me.Contribs.Size = New System.Drawing.Size(95, 21)
        '
        'ContribsScrollRB
        '
        Me.ContribsScrollRB.AutoSize = False
        Me.ContribsScrollRB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ContribsScrollRB.Enabled = False
        Me.ContribsScrollRB.Image = Global.Huggle.My.Resources.Resources.gray_next
        Me.ContribsScrollRB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContribsScrollRB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ContribsScrollRB.Margin = New System.Windows.Forms.Padding(1, 4, 1, 0)
        Me.ContribsScrollRB.Name = "ContribsScrollRB"
        Me.ContribsScrollRB.Size = New System.Drawing.Size(23, 21)
        '
        'CentralPanel
        '
        Me.CentralPanel.Controls.Add(Me.QueueContainer)
        Me.CentralPanel.Controls.Add(Me.Tabs)
        Me.CentralPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CentralPanel.Location = New System.Drawing.Point(0, 91)
        Me.CentralPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.CentralPanel.Name = "CentralPanel"
        Me.CentralPanel.Size = New System.Drawing.Size(822, 227)
        Me.CentralPanel.TabIndex = 1
        '
        'QueueContainer
        '
        Me.QueueContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.QueueContainer.Location = New System.Drawing.Point(0, 0)
        Me.QueueContainer.Margin = New System.Windows.Forms.Padding(0)
        Me.QueueContainer.Name = "QueueContainer"
        Me.QueueContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'QueueContainer.Panel1
        '
        Me.QueueContainer.Panel1.Controls.Add(Me.QueueScroll)
        Me.QueueContainer.Panel1.Controls.Add(Me.QueueSelector)
        Me.QueueContainer.Panel1.Controls.Add(Me.QueueArea)
        Me.QueueContainer.Panel1MinSize = 50
        '
        'QueueContainer.Panel2
        '
        Me.QueueContainer.Panel2.Controls.Add(Me.QueueScroll2)
        Me.QueueContainer.Panel2.Controls.Add(Me.QueueArea2)
        Me.QueueContainer.Panel2.Controls.Add(Me.QueueSelector2)
        Me.QueueContainer.Panel2MinSize = 50
        Me.QueueContainer.Size = New System.Drawing.Size(198, 226)
        Me.QueueContainer.SplitterDistance = 124
        Me.QueueContainer.SplitterWidth = 6
        Me.QueueContainer.TabIndex = 0
        '
        'QueueScroll
        '
        Me.QueueScroll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueScroll.Enabled = False
        Me.QueueScroll.LargeChange = 0
        Me.QueueScroll.Location = New System.Drawing.Point(178, 27)
        Me.QueueScroll.Maximum = 0
        Me.QueueScroll.Name = "QueueScroll"
        Me.QueueScroll.Size = New System.Drawing.Size(18, 97)
        Me.QueueScroll.SmallChange = 0
        Me.QueueScroll.TabIndex = 2
        '
        'QueueArea2
        '
        Me.QueueArea2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueArea2.BackColor = System.Drawing.SystemColors.Control
        Me.QueueArea2.Location = New System.Drawing.Point(0, 27)
        Me.QueueArea2.Margin = New System.Windows.Forms.Padding(0)
        Me.QueueArea2.Name = "QueueArea2"
        Me.QueueArea2.Size = New System.Drawing.Size(174, 68)
        Me.QueueArea2.TabIndex = 1
        '
        'QueueSelector2
        '
        Me.QueueSelector2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueSelector2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.QueueSelector2.FormattingEnabled = True
        Me.QueueSelector2.Location = New System.Drawing.Point(3, 3)
        Me.QueueSelector2.MaxDropDownItems = 20
        Me.QueueSelector2.Name = "QueueSelector2"
        Me.QueueSelector2.Size = New System.Drawing.Size(173, 21)
        Me.QueueSelector2.TabIndex = 0
        '
        'EditInfo
        '
        Me.EditInfo.Location = New System.Drawing.Point(520, 311)
        Me.EditInfo.Name = "EditInfo"
        Me.EditInfo.Size = New System.Drawing.Size(353, 78)
        Me.EditInfo.TabIndex = 2
        Me.EditInfo.Visible = False
        '
        'Main
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(822, 433)
        Me.Controls.Add(Me.EditInfo)
        Me.Controls.Add(Me.TopMenu)
        Me.Controls.Add(Me.LayoutPanel)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.TopMenu
        Me.MinimumSize = New System.Drawing.Size(830, 400)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Huggle"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.LogMenu.ResumeLayout(False)
        Me.TrayMenu.ResumeLayout(False)
        Me.TopMenu.ResumeLayout(False)
        Me.TopMenu.PerformLayout()
        Me.RevertMenu.ResumeLayout(False)
        Me.TagDeleteMenu.ResumeLayout(False)
        Me.WarnMenu.ResumeLayout(False)
        Me.ReportMenu.ResumeLayout(False)
        Me.ActionsStrip.ResumeLayout(False)
        Me.ActionsStrip.PerformLayout()
        Me.NavigationStrip.ResumeLayout(False)
        Me.NavigationStrip.PerformLayout()
        Me.HistoryStrip.ResumeLayout(False)
        Me.HistoryStrip.PerformLayout()
        Me.MainStrip.ResumeLayout(False)
        Me.MainStrip.PerformLayout()
        Me.RevertWarnMenu.ResumeLayout(False)
        Me.TemplateMenu.ResumeLayout(False)
        Me.Tabs.ResumeLayout(False)
        Me.InitialTabPage.ResumeLayout(False)
        Me.LayoutPanel.ResumeLayout(False)
        Me.ToolbarPanel.ResumeLayout(False)
        Me.ToolbarPanel.PerformLayout()
        Me.ContribsStrip.ResumeLayout(False)
        Me.ContribsStrip.PerformLayout()
        Me.CentralPanel.ResumeLayout(False)
        Me.QueueContainer.Panel1.ResumeLayout(False)
        Me.QueueContainer.Panel2.ResumeLayout(False)
        Me.QueueContainer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RevertTimer As System.Windows.Forms.Timer
    Friend WithEvents RcReqTimer As System.Windows.Forms.Timer
    Friend WithEvents LogMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents LogCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents TrayMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TrayRestore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrayExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TopMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuQueue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuPage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpDocumentation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueNext As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents QueueTrim As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents PageReqDeletion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserContribs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserTalk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UserMessage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserWarn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserBlock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageReqProtection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BrowserNewEdits As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BrowserNewContribs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BrowserOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RateUpdateTimer As System.Windows.Forms.Timer
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents InitialTabPage As System.Windows.Forms.TabPage
    Friend WithEvents InitialTab As Huggle.BrowserTab
    Friend WithEvents UserInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageProtect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MainStrip As System.Windows.Forms.ToolStrip
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
    Friend WithEvents History As Huggle.HistoryStrip
    Friend WithEvents PageLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ActionsStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents PageViewB As System.Windows.Forms.ToolStripButton
    Friend WithEvents PageEditB As System.Windows.Forms.ToolStripButton
    Friend WithEvents PageWatchB As System.Windows.Forms.ToolStripButton
    Friend WithEvents Separator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator19 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UserInfoB As System.Windows.Forms.ToolStripButton
    Friend WithEvents UserTalkB As System.Windows.Forms.ToolStripButton
    Friend WithEvents UserMessageB As System.Windows.Forms.ToolStripButton
    Friend WithEvents HistoryScrollLB As System.Windows.Forms.ToolStripButton
    Friend WithEvents HistoryScrollRB As System.Windows.Forms.ToolStripButton
    Friend WithEvents MenuStats As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NextDiffB As System.Windows.Forms.ToolStripButton
    Friend WithEvents PageDeleteB As System.Windows.Forms.ToolStripButton
    Friend WithEvents UndoMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RevertMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RevertAdvanced As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WarnB As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents WarnMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents WarnAdvanced As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TemplateB As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents TemplateMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UserMessageOther As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserMessageWelcome As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertB As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents Status As Huggle.ListView2
    Friend WithEvents Url As System.Windows.Forms.ColumnHeader
    Friend WithEvents Details As System.Windows.Forms.ColumnHeader
    Friend WithEvents HelpFeedback As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PagePatrol As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UndoB As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents CancelB As System.Windows.Forms.ToolStripButton
    Friend WithEvents TagDeleteMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PageTagSpeedy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator23 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PageTagDeleteB As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents PageXfd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageTagProd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageTagB As System.Windows.Forms.ToolStripButton
    Friend WithEvents PageTag As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PageHistoryPage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertWarnB As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents PagePurge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator16 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuGoto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GotoMyTalk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GotoMyContribs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserEmail As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator25 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Separator26 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GoSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PageSwitchTalk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator27 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents QueueClear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueClearAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueScroll2 As System.Windows.Forms.VScrollBar
    Friend WithEvents QueueSelector As System.Windows.Forms.ComboBox
    Friend WithEvents QueueArea As Huggle.QueuePanel
    Friend WithEvents LogClear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator29 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UserIgnoreB As System.Windows.Forms.ToolStripButton
    Private WithEvents ScrollTimer As System.Windows.Forms.Timer
    Friend WithEvents EditInfo As Huggle.EditInfoPanel
    Friend WithEvents ReportMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UserReportVandalism As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserReportUsername As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserReport3rr As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserReportB As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents UserBlockB As System.Windows.Forms.ToolStripButton
    Friend WithEvents RevertCurrentOnly As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator28 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PageDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertWarnMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Separator24 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RevertWarnAdvanced As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuRevision As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevisionView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevisionRevert As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator30 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RevisionPrevious As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevisionNext As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevisionLatest As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevisionSight As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SightAndNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents UserReportSock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuSystem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemMessages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemReconnectIRC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemSaveLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SystemLists As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemRequests As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemStatistics As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SystemShowLog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemShowQueue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SystemLogOut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ToolbarPanel As System.Windows.Forms.Panel
    Friend WithEvents CentralPanel As System.Windows.Forms.Panel
    Friend WithEvents ContribsStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents UserLabel As System.Windows.Forms.ToolStripLabel
    Friend WithEvents UserB As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ContribsB As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContribsScrollLB As System.Windows.Forms.ToolStripButton
    Friend WithEvents Contribs As Huggle.ContribsStrip
    Friend WithEvents ContribsScrollRB As System.Windows.Forms.ToolStripButton
    Friend WithEvents QueueContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents QueueScroll As System.Windows.Forms.VScrollBar
    Friend WithEvents QueueArea2 As Huggle.QueuePanel
    Friend WithEvents QueueSelector2 As System.Windows.Forms.ComboBox
    Friend WithEvents SystemShowTwoQueues As System.Windows.Forms.ToolStripMenuItem
End Class
