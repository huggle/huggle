<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QueueForm
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
        Me.QueueList = New System.Windows.Forms.ListBox
        Me.QueuesLabel = New System.Windows.Forms.Label
        Me.Add = New System.Windows.Forms.Button
        Me.Delete = New System.Windows.Forms.Button
        Me.Rename = New System.Windows.Forms.Button
        Me.Copy = New System.Windows.Forms.Button
        Me.Tip = New System.Windows.Forms.ToolTip(Me.components)
        Me.QueuesEmpty = New System.Windows.Forms.Label
        Me.TypeGroup = New System.Windows.Forms.GroupBox
        Me.DynamicList = New System.Windows.Forms.RadioButton
        Me.Live = New System.Windows.Forms.RadioButton
        Me.LiveList = New System.Windows.Forms.RadioButton
        Me.FixedList = New System.Windows.Forms.RadioButton
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.PagesTab = New System.Windows.Forms.TabPage
        Me.ListBuilder = New System.Windows.Forms.Button
        Me.ListSelector = New System.Windows.Forms.ComboBox
        Me.ListSelectorLabel = New System.Windows.Forms.Label
        Me.QueuePages = New System.Windows.Forms.ListBox
        Me.DynamicListTab = New System.Windows.Forms.TabPage
        Me.DynamicSource = New System.Windows.Forms.TextBox
        Me.DynamicSourceType = New System.Windows.Forms.ComboBox
        Me.RefreshInterval = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.RefreshAlways = New System.Windows.Forms.CheckBox
        Me.RefreshReAdd = New System.Windows.Forms.CheckBox
        Me.DynamicSourceLabel = New System.Windows.Forms.Label
        Me.DynamicSourceTypeLabel = New System.Windows.Forms.Label
        Me.OptionsTab = New System.Windows.Forms.TabPage
        Me.PreloadGroup = New System.Windows.Forms.GroupBox
        Me.AllDiffs = New System.Windows.Forms.RadioButton
        Me.PreloadDiffs = New System.Windows.Forms.RadioButton
        Me.NoDiffs = New System.Windows.Forms.RadioButton
        Me.IgnorePages = New System.Windows.Forms.CheckBox
        Me.RemoveAfter = New System.Windows.Forms.CheckBox
        Me.RemoveAfterTime = New System.Windows.Forms.NumericUpDown
        Me.RemoveAfterTimeLabel = New System.Windows.Forms.Label
        Me.RemoveViewed = New System.Windows.Forms.CheckBox
        Me.RemoveOld = New System.Windows.Forms.CheckBox
        Me.SortOrderLabel = New System.Windows.Forms.Label
        Me.SortOrder = New System.Windows.Forms.ComboBox
        Me.TitleFiltersTab = New System.Windows.Forms.TabPage
        Me.Apply = New System.Windows.Forms.Button
        Me.ApplyFiltersLabel = New System.Windows.Forms.Label
        Me.PageFiltersGroup = New System.Windows.Forms.GroupBox
        Me.CheckAllSpaces = New System.Windows.Forms.Button
        Me.PageRegex = New Huggle.RegexBox
        Me.NamespacesLabel = New System.Windows.Forms.Label
        Me.Namespaces = New System.Windows.Forms.CheckedListBox
        Me.PageRegexLabel = New System.Windows.Forms.Label
        Me.EditFiltersTab = New System.Windows.Forms.TabPage
        Me.ExampleLabel3 = New System.Windows.Forms.Label
        Me.ExampleLabel2 = New System.Windows.Forms.Label
        Me.ExampleLabel1 = New System.Windows.Forms.Label
        Me.EditFiltersGroup = New System.Windows.Forms.GroupBox
        Me.FilterBot = New Huggle.TriState
        Me.SummaryRegex = New Huggle.RegexBox
        Me.UserRegex = New Huggle.RegexBox
        Me.FilterAssisted = New Huggle.TriState
        Me.FilterHuggle = New Huggle.TriState
        Me.FilterMe = New Huggle.TriState
        Me.FilterTags = New Huggle.TriState
        Me.FilterWarnings = New Huggle.TriState
        Me.FilterNotifications = New Huggle.TriState
        Me.FilterReverts = New Huggle.TriState
        Me.FilterOwnUserspace = New Huggle.TriState
        Me.FilterIgnored = New Huggle.TriState
        Me.FilterAnonymous = New Huggle.TriState
        Me.FilterNewPage = New Huggle.TriState
        Me.SummaryRegexLabel = New System.Windows.Forms.Label
        Me.UserRegexLabel = New System.Windows.Forms.Label
        Me.Example2 = New Huggle.TriState
        Me.Example1 = New Huggle.TriState
        Me.Example3 = New Huggle.TriState
        Me.UsersTab = New System.Windows.Forms.TabPage
        Me.UserGroup = New System.Windows.Forms.GroupBox
        Me.UserCount = New System.Windows.Forms.Label
        Me.UserBrowse = New System.Windows.Forms.Button
        Me.UserSource = New System.Windows.Forms.TextBox
        Me.UserSourceLabel = New System.Windows.Forms.Label
        Me.UserSourceTypeLabel = New System.Windows.Forms.Label
        Me.ClearUsers = New System.Windows.Forms.Button
        Me.RemoveUser = New System.Windows.Forms.Button
        Me.UserSourceType = New System.Windows.Forms.ComboBox
        Me.AddUser = New System.Windows.Forms.Button
        Me.Users = New System.Windows.Forms.ListBox
        Me.RevisionTab = New System.Windows.Forms.TabPage
        Me.RevisionGroup = New System.Windows.Forms.GroupBox
        Me.RevisionTabNote = New System.Windows.Forms.Label
        Me.RevisionRegexLabel = New System.Windows.Forms.Label
        Me.RevisionRegex = New Huggle.RegexBox
        Me.OK = New System.Windows.Forms.Button
        Me.MoveUp = New System.Windows.Forms.Button
        Me.MoveDown = New System.Windows.Forms.Button
        Me.TrayNotification = New System.Windows.Forms.CheckBox
        Me.TypeGroup.SuspendLayout()
        Me.Tabs.SuspendLayout()
        Me.PagesTab.SuspendLayout()
        Me.DynamicListTab.SuspendLayout()
        CType(Me.RefreshInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OptionsTab.SuspendLayout()
        Me.PreloadGroup.SuspendLayout()
        CType(Me.RemoveAfterTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TitleFiltersTab.SuspendLayout()
        Me.PageFiltersGroup.SuspendLayout()
        Me.EditFiltersTab.SuspendLayout()
        Me.EditFiltersGroup.SuspendLayout()
        Me.UsersTab.SuspendLayout()
        Me.UserGroup.SuspendLayout()
        Me.RevisionTab.SuspendLayout()
        Me.RevisionGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'QueueList
        '
        Me.QueueList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.QueueList.FormattingEnabled = True
        Me.QueueList.IntegralHeight = False
        Me.QueueList.Location = New System.Drawing.Point(12, 28)
        Me.QueueList.Name = "QueueList"
        Me.QueueList.Size = New System.Drawing.Size(156, 390)
        Me.QueueList.TabIndex = 1
        '
        'QueuesLabel
        '
        Me.QueuesLabel.AutoSize = True
        Me.QueuesLabel.Location = New System.Drawing.Point(9, 12)
        Me.QueuesLabel.Name = "QueuesLabel"
        Me.QueuesLabel.Size = New System.Drawing.Size(47, 13)
        Me.QueuesLabel.TabIndex = 0
        Me.QueuesLabel.Text = "Queues:"
        '
        'Add
        '
        Me.Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Add.Location = New System.Drawing.Point(12, 424)
        Me.Add.Name = "Add"
        Me.Add.Size = New System.Drawing.Size(60, 23)
        Me.Add.TabIndex = 3
        Me.Add.Text = "Add"
        Me.Tip.SetToolTip(Me.Add, "Add a queue")
        Me.Add.UseVisualStyleBackColor = True
        '
        'Delete
        '
        Me.Delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Delete.Enabled = False
        Me.Delete.Location = New System.Drawing.Point(78, 424)
        Me.Delete.Name = "Delete"
        Me.Delete.Size = New System.Drawing.Size(61, 23)
        Me.Delete.TabIndex = 4
        Me.Delete.Text = "Delete"
        Me.Tip.SetToolTip(Me.Delete, "Remove selected queue")
        Me.Delete.UseVisualStyleBackColor = True
        '
        'Rename
        '
        Me.Rename.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Rename.Enabled = False
        Me.Rename.Location = New System.Drawing.Point(78, 453)
        Me.Rename.Name = "Rename"
        Me.Rename.Size = New System.Drawing.Size(61, 23)
        Me.Rename.TabIndex = 6
        Me.Rename.Text = "Rename"
        Me.Tip.SetToolTip(Me.Rename, "Rename selected queue")
        Me.Rename.UseVisualStyleBackColor = True
        '
        'Copy
        '
        Me.Copy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Copy.Enabled = False
        Me.Copy.Location = New System.Drawing.Point(12, 453)
        Me.Copy.Name = "Copy"
        Me.Copy.Size = New System.Drawing.Size(60, 23)
        Me.Copy.TabIndex = 5
        Me.Copy.Text = "Copy"
        Me.Tip.SetToolTip(Me.Copy, "Copy selected queue")
        Me.Copy.UseVisualStyleBackColor = True
        '
        'QueuesEmpty
        '
        Me.QueuesEmpty.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.QueuesEmpty.BackColor = System.Drawing.SystemColors.Window
        Me.QueuesEmpty.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.QueuesEmpty.Location = New System.Drawing.Point(21, 193)
        Me.QueuesEmpty.Name = "QueuesEmpty"
        Me.QueuesEmpty.Size = New System.Drawing.Size(138, 36)
        Me.QueuesEmpty.TabIndex = 2
        Me.QueuesEmpty.Text = "No queues defined" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click ""Add"" to create one"
        Me.QueuesEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.QueuesEmpty.Visible = False
        '
        'TypeGroup
        '
        Me.TypeGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TypeGroup.Controls.Add(Me.DynamicList)
        Me.TypeGroup.Controls.Add(Me.Live)
        Me.TypeGroup.Controls.Add(Me.LiveList)
        Me.TypeGroup.Controls.Add(Me.FixedList)
        Me.TypeGroup.Location = New System.Drawing.Point(186, 12)
        Me.TypeGroup.Name = "TypeGroup"
        Me.TypeGroup.Size = New System.Drawing.Size(446, 116)
        Me.TypeGroup.TabIndex = 7
        Me.TypeGroup.TabStop = False
        Me.TypeGroup.Text = "Page list type"
        '
        'DynamicList
        '
        Me.DynamicList.AutoSize = True
        Me.DynamicList.Location = New System.Drawing.Point(10, 65)
        Me.DynamicList.Name = "DynamicList"
        Me.DynamicList.Size = New System.Drawing.Size(392, 17)
        Me.DynamicList.TabIndex = 2
        Me.DynamicList.TabStop = True
        Me.DynamicList.Text = "Dynamic list: Use a list of pages periodically generated from a specified source"
        Me.DynamicList.UseVisualStyleBackColor = True
        '
        'Live
        '
        Me.Live.AutoSize = True
        Me.Live.Location = New System.Drawing.Point(10, 88)
        Me.Live.Name = "Live"
        Me.Live.Size = New System.Drawing.Size(293, 17)
        Me.Live.TabIndex = 2
        Me.Live.TabStop = True
        Me.Live.Text = "Don't use a list, just show any edit that matches the filters"
        Me.Live.UseVisualStyleBackColor = True
        '
        'LiveList
        '
        Me.LiveList.AutoSize = True
        Me.LiveList.Location = New System.Drawing.Point(10, 42)
        Me.LiveList.Name = "LiveList"
        Me.LiveList.Size = New System.Drawing.Size(383, 17)
        Me.LiveList.TabIndex = 1
        Me.LiveList.TabStop = True
        Me.LiveList.Text = "Live fixed list: Use a list of pages, show edits to those pages as they happen"
        Me.LiveList.UseVisualStyleBackColor = True
        '
        'FixedList
        '
        Me.FixedList.Location = New System.Drawing.Point(10, 19)
        Me.FixedList.Name = "FixedList"
        Me.FixedList.Size = New System.Drawing.Size(356, 17)
        Me.FixedList.TabIndex = 0
        Me.FixedList.TabStop = True
        Me.FixedList.Text = "Fixed list: Use a list of pages, show the most recent edit to each one"
        Me.FixedList.UseVisualStyleBackColor = True
        '
        'Tabs
        '
        Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Controls.Add(Me.PagesTab)
        Me.Tabs.Controls.Add(Me.DynamicListTab)
        Me.Tabs.Controls.Add(Me.OptionsTab)
        Me.Tabs.Controls.Add(Me.TitleFiltersTab)
        Me.Tabs.Controls.Add(Me.EditFiltersTab)
        Me.Tabs.Controls.Add(Me.UsersTab)
        Me.Tabs.Controls.Add(Me.RevisionTab)
        Me.Tabs.Location = New System.Drawing.Point(186, 134)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(446, 313)
        Me.Tabs.TabIndex = 8
        '
        'PagesTab
        '
        Me.PagesTab.BackColor = System.Drawing.Color.Transparent
        Me.PagesTab.Controls.Add(Me.ListBuilder)
        Me.PagesTab.Controls.Add(Me.ListSelector)
        Me.PagesTab.Controls.Add(Me.ListSelectorLabel)
        Me.PagesTab.Controls.Add(Me.QueuePages)
        Me.PagesTab.Location = New System.Drawing.Point(4, 22)
        Me.PagesTab.Name = "PagesTab"
        Me.PagesTab.Padding = New System.Windows.Forms.Padding(3)
        Me.PagesTab.Size = New System.Drawing.Size(438, 287)
        Me.PagesTab.TabIndex = 0
        Me.PagesTab.Text = "Page list"
        Me.PagesTab.UseVisualStyleBackColor = True
        '
        'ListBuilder
        '
        Me.ListBuilder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBuilder.Location = New System.Drawing.Point(339, 10)
        Me.ListBuilder.Name = "ListBuilder"
        Me.ListBuilder.Size = New System.Drawing.Size(93, 23)
        Me.ListBuilder.TabIndex = 2
        Me.ListBuilder.Text = "List Builder..."
        Me.ListBuilder.UseVisualStyleBackColor = True
        '
        'ListSelector
        '
        Me.ListSelector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ListSelector.FormattingEnabled = True
        Me.ListSelector.Location = New System.Drawing.Point(34, 11)
        Me.ListSelector.Name = "ListSelector"
        Me.ListSelector.Size = New System.Drawing.Size(299, 21)
        Me.ListSelector.TabIndex = 1
        '
        'ListSelectorLabel
        '
        Me.ListSelectorLabel.AutoSize = True
        Me.ListSelectorLabel.Location = New System.Drawing.Point(6, 14)
        Me.ListSelectorLabel.Name = "ListSelectorLabel"
        Me.ListSelectorLabel.Size = New System.Drawing.Size(26, 13)
        Me.ListSelectorLabel.TabIndex = 0
        Me.ListSelectorLabel.Text = "List:"
        '
        'QueuePages
        '
        Me.QueuePages.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueuePages.Enabled = False
        Me.QueuePages.IntegralHeight = False
        Me.QueuePages.Location = New System.Drawing.Point(6, 40)
        Me.QueuePages.Name = "QueuePages"
        Me.QueuePages.Size = New System.Drawing.Size(426, 241)
        Me.QueuePages.TabIndex = 3
        '
        'DynamicListTab
        '
        Me.DynamicListTab.Controls.Add(Me.DynamicSource)
        Me.DynamicListTab.Controls.Add(Me.DynamicSourceType)
        Me.DynamicListTab.Controls.Add(Me.RefreshInterval)
        Me.DynamicListTab.Controls.Add(Me.Label4)
        Me.DynamicListTab.Controls.Add(Me.Label3)
        Me.DynamicListTab.Controls.Add(Me.RefreshAlways)
        Me.DynamicListTab.Controls.Add(Me.RefreshReAdd)
        Me.DynamicListTab.Controls.Add(Me.DynamicSourceLabel)
        Me.DynamicListTab.Controls.Add(Me.DynamicSourceTypeLabel)
        Me.DynamicListTab.Location = New System.Drawing.Point(4, 22)
        Me.DynamicListTab.Name = "DynamicListTab"
        Me.DynamicListTab.Size = New System.Drawing.Size(438, 287)
        Me.DynamicListTab.TabIndex = 4
        Me.DynamicListTab.Text = "List source"
        Me.DynamicListTab.UseVisualStyleBackColor = True
        '
        'DynamicSource
        '
        Me.DynamicSource.Location = New System.Drawing.Point(88, 42)
        Me.DynamicSource.Name = "DynamicSource"
        Me.DynamicSource.Size = New System.Drawing.Size(211, 20)
        Me.DynamicSource.TabIndex = 5
        '
        'DynamicSourceType
        '
        Me.DynamicSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DynamicSourceType.FormattingEnabled = True
        Me.DynamicSourceType.Items.AddRange(New Object() {"Category"})
        Me.DynamicSourceType.Location = New System.Drawing.Point(88, 15)
        Me.DynamicSourceType.Name = "DynamicSourceType"
        Me.DynamicSourceType.Size = New System.Drawing.Size(211, 21)
        Me.DynamicSourceType.TabIndex = 4
        '
        'RefreshInterval
        '
        Me.RefreshInterval.Location = New System.Drawing.Point(109, 77)
        Me.RefreshInterval.Maximum = New Decimal(New Integer() {600, 0, 0, 0})
        Me.RefreshInterval.Minimum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.RefreshInterval.Name = "RefreshInterval"
        Me.RefreshInterval.Size = New System.Drawing.Size(53, 20)
        Me.RefreshInterval.TabIndex = 3
        Me.RefreshInterval.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(168, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "seconds"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Refresh list every"
        '
        'RefreshAlways
        '
        Me.RefreshAlways.AutoSize = True
        Me.RefreshAlways.Location = New System.Drawing.Point(18, 109)
        Me.RefreshAlways.Name = "RefreshAlways"
        Me.RefreshAlways.Size = New System.Drawing.Size(227, 17)
        Me.RefreshAlways.TabIndex = 1
        Me.RefreshAlways.Text = "Refresh list even when queue is not active"
        Me.RefreshAlways.UseVisualStyleBackColor = True
        '
        'RefreshReAdd
        '
        Me.RefreshReAdd.AutoSize = True
        Me.RefreshReAdd.Location = New System.Drawing.Point(18, 132)
        Me.RefreshReAdd.Name = "RefreshReAdd"
        Me.RefreshReAdd.Size = New System.Drawing.Size(313, 17)
        Me.RefreshReAdd.TabIndex = 1
        Me.RefreshReAdd.Text = "When refreshing, re-add pages that were previously removed"
        Me.RefreshReAdd.UseVisualStyleBackColor = True
        '
        'DynamicSourceLabel
        '
        Me.DynamicSourceLabel.AutoSize = True
        Me.DynamicSourceLabel.Location = New System.Drawing.Point(38, 45)
        Me.DynamicSourceLabel.Name = "DynamicSourceLabel"
        Me.DynamicSourceLabel.Size = New System.Drawing.Size(44, 13)
        Me.DynamicSourceLabel.TabIndex = 0
        Me.DynamicSourceLabel.Text = "Source:"
        '
        'DynamicSourceTypeLabel
        '
        Me.DynamicSourceTypeLabel.AutoSize = True
        Me.DynamicSourceTypeLabel.Location = New System.Drawing.Point(15, 18)
        Me.DynamicSourceTypeLabel.Name = "DynamicSourceTypeLabel"
        Me.DynamicSourceTypeLabel.Size = New System.Drawing.Size(67, 13)
        Me.DynamicSourceTypeLabel.TabIndex = 0
        Me.DynamicSourceTypeLabel.Text = "Source type:"
        '
        'OptionsTab
        '
        Me.OptionsTab.Controls.Add(Me.PreloadGroup)
        Me.OptionsTab.Controls.Add(Me.TrayNotification)
        Me.OptionsTab.Controls.Add(Me.IgnorePages)
        Me.OptionsTab.Controls.Add(Me.RemoveAfter)
        Me.OptionsTab.Controls.Add(Me.RemoveAfterTime)
        Me.OptionsTab.Controls.Add(Me.RemoveAfterTimeLabel)
        Me.OptionsTab.Controls.Add(Me.RemoveViewed)
        Me.OptionsTab.Controls.Add(Me.RemoveOld)
        Me.OptionsTab.Controls.Add(Me.SortOrderLabel)
        Me.OptionsTab.Controls.Add(Me.SortOrder)
        Me.OptionsTab.Location = New System.Drawing.Point(4, 22)
        Me.OptionsTab.Name = "OptionsTab"
        Me.OptionsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.OptionsTab.Size = New System.Drawing.Size(438, 287)
        Me.OptionsTab.TabIndex = 3
        Me.OptionsTab.Text = "Queue options"
        Me.OptionsTab.UseVisualStyleBackColor = True
        '
        'PreloadGroup
        '
        Me.PreloadGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PreloadGroup.Controls.Add(Me.AllDiffs)
        Me.PreloadGroup.Controls.Add(Me.PreloadDiffs)
        Me.PreloadGroup.Controls.Add(Me.NoDiffs)
        Me.PreloadGroup.Location = New System.Drawing.Point(17, 182)
        Me.PreloadGroup.Name = "PreloadGroup"
        Me.PreloadGroup.Size = New System.Drawing.Size(402, 89)
        Me.PreloadGroup.TabIndex = 12
        Me.PreloadGroup.TabStop = False
        Me.PreloadGroup.Text = "Diffs"
        '
        'AllDiffs
        '
        Me.AllDiffs.AutoSize = True
        Me.AllDiffs.Location = New System.Drawing.Point(11, 66)
        Me.AllDiffs.Name = "AllDiffs"
        Me.AllDiffs.Size = New System.Drawing.Size(357, 17)
        Me.AllDiffs.TabIndex = 0
        Me.AllDiffs.TabStop = True
        Me.AllDiffs.Text = "Load diffs for every revision in the queue (do not use on large queues!)"
        Me.AllDiffs.UseVisualStyleBackColor = True
        '
        'PreloadDiffs
        '
        Me.PreloadDiffs.AutoSize = True
        Me.PreloadDiffs.Location = New System.Drawing.Point(11, 42)
        Me.PreloadDiffs.Name = "PreloadDiffs"
        Me.PreloadDiffs.Size = New System.Drawing.Size(346, 17)
        Me.PreloadDiffs.TabIndex = 0
        Me.PreloadDiffs.TabStop = True
        Me.PreloadDiffs.Text = "Preload diffs when queue is active, if system options are set to do so"
        Me.PreloadDiffs.UseVisualStyleBackColor = True
        '
        'NoDiffs
        '
        Me.NoDiffs.AutoSize = True
        Me.NoDiffs.Location = New System.Drawing.Point(11, 19)
        Me.NoDiffs.Name = "NoDiffs"
        Me.NoDiffs.Size = New System.Drawing.Size(226, 17)
        Me.NoDiffs.TabIndex = 0
        Me.NoDiffs.TabStop = True
        Me.NoDiffs.Text = "Load diffs only when the revision is viewed"
        Me.NoDiffs.UseVisualStyleBackColor = True
        '
        'IgnorePages
        '
        Me.IgnorePages.AutoSize = True
        Me.IgnorePages.Location = New System.Drawing.Point(17, 122)
        Me.IgnorePages.Name = "IgnorePages"
        Me.IgnorePages.Size = New System.Drawing.Size(243, 17)
        Me.IgnorePages.TabIndex = 11
        Me.IgnorePages.Text = "Ignore edits to pages on the ignored pages list"
        Me.IgnorePages.UseVisualStyleBackColor = True
        '
        'RemoveAfter
        '
        Me.RemoveAfter.AutoSize = True
        Me.RemoveAfter.Location = New System.Drawing.Point(17, 99)
        Me.RemoveAfter.Name = "RemoveAfter"
        Me.RemoveAfter.Size = New System.Drawing.Size(115, 17)
        Me.RemoveAfter.TabIndex = 4
        Me.RemoveAfter.Text = "Remove edits after"
        Me.RemoveAfter.UseVisualStyleBackColor = True
        '
        'RemoveAfterTime
        '
        Me.RemoveAfterTime.Enabled = False
        Me.RemoveAfterTime.Location = New System.Drawing.Point(132, 97)
        Me.RemoveAfterTime.Maximum = New Decimal(New Integer() {240, 0, 0, 0})
        Me.RemoveAfterTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.RemoveAfterTime.Name = "RemoveAfterTime"
        Me.RemoveAfterTime.Size = New System.Drawing.Size(53, 20)
        Me.RemoveAfterTime.TabIndex = 5
        Me.RemoveAfterTime.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'RemoveAfterTimeLabel
        '
        Me.RemoveAfterTimeLabel.AutoSize = True
        Me.RemoveAfterTimeLabel.Location = New System.Drawing.Point(191, 100)
        Me.RemoveAfterTimeLabel.Name = "RemoveAfterTimeLabel"
        Me.RemoveAfterTimeLabel.Size = New System.Drawing.Size(43, 13)
        Me.RemoveAfterTimeLabel.TabIndex = 6
        Me.RemoveAfterTimeLabel.Text = "minutes"
        '
        'RemoveViewed
        '
        Me.RemoveViewed.AutoSize = True
        Me.RemoveViewed.Location = New System.Drawing.Point(17, 53)
        Me.RemoveViewed.Name = "RemoveViewed"
        Me.RemoveViewed.Size = New System.Drawing.Size(157, 17)
        Me.RemoveViewed.TabIndex = 2
        Me.RemoveViewed.Text = "Remove edits when viewed"
        Me.RemoveViewed.UseVisualStyleBackColor = True
        '
        'RemoveOld
        '
        Me.RemoveOld.AutoSize = True
        Me.RemoveOld.Location = New System.Drawing.Point(17, 76)
        Me.RemoveOld.Name = "RemoveOld"
        Me.RemoveOld.Size = New System.Drawing.Size(202, 17)
        Me.RemoveOld.TabIndex = 3
        Me.RemoveOld.Text = "Remove older edits to the same page"
        Me.RemoveOld.UseVisualStyleBackColor = True
        '
        'SortOrderLabel
        '
        Me.SortOrderLabel.AutoSize = True
        Me.SortOrderLabel.Location = New System.Drawing.Point(14, 18)
        Me.SortOrderLabel.Name = "SortOrderLabel"
        Me.SortOrderLabel.Size = New System.Drawing.Size(56, 13)
        Me.SortOrderLabel.TabIndex = 0
        Me.SortOrderLabel.Text = "Sort order:"
        '
        'SortOrder
        '
        Me.SortOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SortOrder.FormattingEnabled = True
        Me.SortOrder.Items.AddRange(New Object() {"Newest edits first", "Oldest edits first", "Most suspicious edits first"})
        Me.SortOrder.Location = New System.Drawing.Point(76, 15)
        Me.SortOrder.Name = "SortOrder"
        Me.SortOrder.Size = New System.Drawing.Size(143, 21)
        Me.SortOrder.TabIndex = 1
        '
        'TitleFiltersTab
        '
        Me.TitleFiltersTab.BackColor = System.Drawing.Color.Transparent
        Me.TitleFiltersTab.Controls.Add(Me.Apply)
        Me.TitleFiltersTab.Controls.Add(Me.ApplyFiltersLabel)
        Me.TitleFiltersTab.Controls.Add(Me.PageFiltersGroup)
        Me.TitleFiltersTab.Location = New System.Drawing.Point(4, 22)
        Me.TitleFiltersTab.Name = "TitleFiltersTab"
        Me.TitleFiltersTab.Padding = New System.Windows.Forms.Padding(3)
        Me.TitleFiltersTab.Size = New System.Drawing.Size(438, 287)
        Me.TitleFiltersTab.TabIndex = 1
        Me.TitleFiltersTab.Text = "Page title"
        Me.TitleFiltersTab.UseVisualStyleBackColor = True
        '
        'Apply
        '
        Me.Apply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Apply.Location = New System.Drawing.Point(357, 258)
        Me.Apply.Name = "Apply"
        Me.Apply.Size = New System.Drawing.Size(75, 23)
        Me.Apply.TabIndex = 2
        Me.Apply.Text = "Apply"
        Me.Apply.UseVisualStyleBackColor = True
        '
        'ApplyFiltersLabel
        '
        Me.ApplyFiltersLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ApplyFiltersLabel.AutoSize = True
        Me.ApplyFiltersLabel.Location = New System.Drawing.Point(6, 263)
        Me.ApplyFiltersLabel.Name = "ApplyFiltersLabel"
        Me.ApplyFiltersLabel.Size = New System.Drawing.Size(268, 13)
        Me.ApplyFiltersLabel.TabIndex = 1
        Me.ApplyFiltersLabel.Text = "Click Apply to apply these filters to the existing page list."
        '
        'PageFiltersGroup
        '
        Me.PageFiltersGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PageFiltersGroup.Controls.Add(Me.CheckAllSpaces)
        Me.PageFiltersGroup.Controls.Add(Me.PageRegex)
        Me.PageFiltersGroup.Controls.Add(Me.NamespacesLabel)
        Me.PageFiltersGroup.Controls.Add(Me.Namespaces)
        Me.PageFiltersGroup.Controls.Add(Me.PageRegexLabel)
        Me.PageFiltersGroup.Location = New System.Drawing.Point(6, 6)
        Me.PageFiltersGroup.Name = "PageFiltersGroup"
        Me.PageFiltersGroup.Size = New System.Drawing.Size(426, 246)
        Me.PageFiltersGroup.TabIndex = 0
        Me.PageFiltersGroup.TabStop = False
        Me.PageFiltersGroup.Text = "Page title filters"
        '
        'CheckAllSpaces
        '
        Me.CheckAllSpaces.Location = New System.Drawing.Point(6, 190)
        Me.CheckAllSpaces.Name = "CheckAllSpaces"
        Me.CheckAllSpaces.Size = New System.Drawing.Size(107, 23)
        Me.CheckAllSpaces.TabIndex = 4
        Me.CheckAllSpaces.Text = "Check/clear all"
        Me.CheckAllSpaces.UseVisualStyleBackColor = True
        '
        'PageRegex
        '
        Me.PageRegex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PageRegex.Location = New System.Drawing.Point(173, 19)
        Me.PageRegex.Multiline = False
        Me.PageRegex.Name = "PageRegex"
        Me.PageRegex.Size = New System.Drawing.Size(247, 38)
        Me.PageRegex.TabIndex = 1
        '
        'NamespacesLabel
        '
        Me.NamespacesLabel.AutoSize = True
        Me.NamespacesLabel.Location = New System.Drawing.Point(6, 44)
        Me.NamespacesLabel.Name = "NamespacesLabel"
        Me.NamespacesLabel.Size = New System.Drawing.Size(72, 13)
        Me.NamespacesLabel.TabIndex = 2
        Me.NamespacesLabel.Text = "Namespaces:"
        '
        'Namespaces
        '
        Me.Namespaces.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Namespaces.CheckOnClick = True
        Me.Namespaces.FormattingEnabled = True
        Me.Namespaces.Location = New System.Drawing.Point(6, 60)
        Me.Namespaces.MultiColumn = True
        Me.Namespaces.Name = "Namespaces"
        Me.Namespaces.Size = New System.Drawing.Size(414, 124)
        Me.Namespaces.TabIndex = 3
        '
        'PageRegexLabel
        '
        Me.PageRegexLabel.AutoSize = True
        Me.PageRegexLabel.Location = New System.Drawing.Point(6, 22)
        Me.PageRegexLabel.Name = "PageRegexLabel"
        Me.PageRegexLabel.Size = New System.Drawing.Size(161, 13)
        Me.PageRegexLabel.TabIndex = 0
        Me.PageRegexLabel.Text = "Title matches regular expression:"
        '
        'EditFiltersTab
        '
        Me.EditFiltersTab.Controls.Add(Me.ExampleLabel3)
        Me.EditFiltersTab.Controls.Add(Me.ExampleLabel2)
        Me.EditFiltersTab.Controls.Add(Me.ExampleLabel1)
        Me.EditFiltersTab.Controls.Add(Me.EditFiltersGroup)
        Me.EditFiltersTab.Controls.Add(Me.Example2)
        Me.EditFiltersTab.Controls.Add(Me.Example1)
        Me.EditFiltersTab.Controls.Add(Me.Example3)
        Me.EditFiltersTab.Location = New System.Drawing.Point(4, 22)
        Me.EditFiltersTab.Name = "EditFiltersTab"
        Me.EditFiltersTab.Size = New System.Drawing.Size(438, 287)
        Me.EditFiltersTab.TabIndex = 2
        Me.EditFiltersTab.Text = "Filters"
        Me.EditFiltersTab.UseVisualStyleBackColor = True
        '
        'ExampleLabel3
        '
        Me.ExampleLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ExampleLabel3.AutoSize = True
        Me.ExampleLabel3.Location = New System.Drawing.Point(280, 262)
        Me.ExampleLabel3.Name = "ExampleLabel3"
        Me.ExampleLabel3.Size = New System.Drawing.Size(125, 13)
        Me.ExampleLabel3.TabIndex = 4
        Me.ExampleLabel3.Text = "Don't check this attribute"
        '
        'ExampleLabel2
        '
        Me.ExampleLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ExampleLabel2.AutoSize = True
        Me.ExampleLabel2.Location = New System.Drawing.Point(150, 262)
        Me.ExampleLabel2.Name = "ExampleLabel2"
        Me.ExampleLabel2.Size = New System.Drawing.Size(105, 13)
        Me.ExampleLabel2.TabIndex = 4
        Me.ExampleLabel2.Text = "Exclude this attribute"
        '
        'ExampleLabel1
        '
        Me.ExampleLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ExampleLabel1.AutoSize = True
        Me.ExampleLabel1.Location = New System.Drawing.Point(23, 262)
        Me.ExampleLabel1.Name = "ExampleLabel1"
        Me.ExampleLabel1.Size = New System.Drawing.Size(104, 13)
        Me.ExampleLabel1.TabIndex = 4
        Me.ExampleLabel1.Text = "Require this attribute"
        '
        'EditFiltersGroup
        '
        Me.EditFiltersGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EditFiltersGroup.Controls.Add(Me.FilterBot)
        Me.EditFiltersGroup.Controls.Add(Me.SummaryRegex)
        Me.EditFiltersGroup.Controls.Add(Me.UserRegex)
        Me.EditFiltersGroup.Controls.Add(Me.FilterAssisted)
        Me.EditFiltersGroup.Controls.Add(Me.FilterHuggle)
        Me.EditFiltersGroup.Controls.Add(Me.FilterMe)
        Me.EditFiltersGroup.Controls.Add(Me.FilterTags)
        Me.EditFiltersGroup.Controls.Add(Me.FilterWarnings)
        Me.EditFiltersGroup.Controls.Add(Me.FilterNotifications)
        Me.EditFiltersGroup.Controls.Add(Me.FilterReverts)
        Me.EditFiltersGroup.Controls.Add(Me.FilterOwnUserspace)
        Me.EditFiltersGroup.Controls.Add(Me.FilterIgnored)
        Me.EditFiltersGroup.Controls.Add(Me.FilterAnonymous)
        Me.EditFiltersGroup.Controls.Add(Me.FilterNewPage)
        Me.EditFiltersGroup.Controls.Add(Me.SummaryRegexLabel)
        Me.EditFiltersGroup.Controls.Add(Me.UserRegexLabel)
        Me.EditFiltersGroup.Location = New System.Drawing.Point(6, 6)
        Me.EditFiltersGroup.Name = "EditFiltersGroup"
        Me.EditFiltersGroup.Size = New System.Drawing.Size(422, 245)
        Me.EditFiltersGroup.TabIndex = 0
        Me.EditFiltersGroup.TabStop = False
        Me.EditFiltersGroup.Text = "Edit filters"
        '
        'FilterBot
        '
        Me.FilterBot.BackColor = System.Drawing.SystemColors.Window
        Me.FilterBot.Location = New System.Drawing.Point(177, 155)
        Me.FilterBot.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterBot.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterBot.Name = "FilterBot"
        Me.FilterBot.Size = New System.Drawing.Size(62, 16)
        Me.FilterBot.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterBot.TabIndex = 9
        '
        'SummaryRegex
        '
        Me.SummaryRegex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SummaryRegex.Location = New System.Drawing.Point(201, 58)
        Me.SummaryRegex.Multiline = False
        Me.SummaryRegex.Name = "SummaryRegex"
        Me.SummaryRegex.Size = New System.Drawing.Size(215, 38)
        Me.SummaryRegex.TabIndex = 1
        '
        'UserRegex
        '
        Me.UserRegex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserRegex.Location = New System.Drawing.Point(201, 19)
        Me.UserRegex.Multiline = False
        Me.UserRegex.Name = "UserRegex"
        Me.UserRegex.Size = New System.Drawing.Size(212, 38)
        Me.UserRegex.TabIndex = 1
        '
        'FilterAssisted
        '
        Me.FilterAssisted.BackColor = System.Drawing.SystemColors.Window
        Me.FilterAssisted.Location = New System.Drawing.Point(177, 177)
        Me.FilterAssisted.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterAssisted.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterAssisted.Name = "FilterAssisted"
        Me.FilterAssisted.Size = New System.Drawing.Size(85, 16)
        Me.FilterAssisted.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterAssisted.TabIndex = 7
        '
        'FilterHuggle
        '
        Me.FilterHuggle.BackColor = System.Drawing.SystemColors.Window
        Me.FilterHuggle.Location = New System.Drawing.Point(177, 199)
        Me.FilterHuggle.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterHuggle.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterHuggle.Name = "FilterHuggle"
        Me.FilterHuggle.Size = New System.Drawing.Size(80, 16)
        Me.FilterHuggle.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterHuggle.TabIndex = 7
        '
        'FilterMe
        '
        Me.FilterMe.BackColor = System.Drawing.SystemColors.Window
        Me.FilterMe.Location = New System.Drawing.Point(177, 221)
        Me.FilterMe.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterMe.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterMe.Name = "FilterMe"
        Me.FilterMe.Size = New System.Drawing.Size(60, 16)
        Me.FilterMe.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterMe.TabIndex = 8
        '
        'FilterTags
        '
        Me.FilterTags.BackColor = System.Drawing.SystemColors.Window
        Me.FilterTags.Location = New System.Drawing.Point(9, 221)
        Me.FilterTags.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterTags.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterTags.Name = "FilterTags"
        Me.FilterTags.Size = New System.Drawing.Size(45, 16)
        Me.FilterTags.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterTags.TabIndex = 8
        '
        'FilterWarnings
        '
        Me.FilterWarnings.BackColor = System.Drawing.SystemColors.Window
        Me.FilterWarnings.Location = New System.Drawing.Point(9, 199)
        Me.FilterWarnings.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterWarnings.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterWarnings.Name = "FilterWarnings"
        Me.FilterWarnings.Size = New System.Drawing.Size(66, 16)
        Me.FilterWarnings.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterWarnings.TabIndex = 8
        '
        'FilterNotifications
        '
        Me.FilterNotifications.BackColor = System.Drawing.SystemColors.Window
        Me.FilterNotifications.Location = New System.Drawing.Point(9, 177)
        Me.FilterNotifications.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterNotifications.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterNotifications.Name = "FilterNotifications"
        Me.FilterNotifications.Size = New System.Drawing.Size(79, 16)
        Me.FilterNotifications.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterNotifications.TabIndex = 8
        '
        'FilterReverts
        '
        Me.FilterReverts.BackColor = System.Drawing.SystemColors.Window
        Me.FilterReverts.Location = New System.Drawing.Point(9, 155)
        Me.FilterReverts.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterReverts.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterReverts.Name = "FilterReverts"
        Me.FilterReverts.Size = New System.Drawing.Size(58, 16)
        Me.FilterReverts.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterReverts.TabIndex = 6
        '
        'FilterOwnUserspace
        '
        Me.FilterOwnUserspace.BackColor = System.Drawing.SystemColors.Window
        Me.FilterOwnUserspace.Location = New System.Drawing.Point(9, 120)
        Me.FilterOwnUserspace.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterOwnUserspace.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterOwnUserspace.Name = "FilterOwnUserspace"
        Me.FilterOwnUserspace.Size = New System.Drawing.Size(130, 16)
        Me.FilterOwnUserspace.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterOwnUserspace.TabIndex = 4
        '
        'FilterIgnored
        '
        Me.FilterIgnored.BackColor = System.Drawing.SystemColors.Window
        Me.FilterIgnored.Location = New System.Drawing.Point(177, 120)
        Me.FilterIgnored.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterIgnored.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterIgnored.Name = "FilterIgnored"
        Me.FilterIgnored.Size = New System.Drawing.Size(85, 16)
        Me.FilterIgnored.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterIgnored.TabIndex = 5
        '
        'FilterAnonymous
        '
        Me.FilterAnonymous.BackColor = System.Drawing.SystemColors.Window
        Me.FilterAnonymous.Location = New System.Drawing.Point(177, 98)
        Me.FilterAnonymous.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterAnonymous.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterAnonymous.Name = "FilterAnonymous"
        Me.FilterAnonymous.Size = New System.Drawing.Size(104, 16)
        Me.FilterAnonymous.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterAnonymous.TabIndex = 3
        '
        'FilterNewPage
        '
        Me.FilterNewPage.BackColor = System.Drawing.SystemColors.Window
        Me.FilterNewPage.Location = New System.Drawing.Point(9, 98)
        Me.FilterNewPage.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterNewPage.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterNewPage.Name = "FilterNewPage"
        Me.FilterNewPage.Size = New System.Drawing.Size(75, 16)
        Me.FilterNewPage.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterNewPage.TabIndex = 2
        '
        'SummaryRegexLabel
        '
        Me.SummaryRegexLabel.AutoSize = True
        Me.SummaryRegexLabel.Location = New System.Drawing.Point(11, 60)
        Me.SummaryRegexLabel.Name = "SummaryRegexLabel"
        Me.SummaryRegexLabel.Size = New System.Drawing.Size(184, 13)
        Me.SummaryRegexLabel.TabIndex = 0
        Me.SummaryRegexLabel.Text = "Summary matches regular expression:"
        '
        'UserRegexLabel
        '
        Me.UserRegexLabel.AutoSize = True
        Me.UserRegexLabel.Location = New System.Drawing.Point(6, 22)
        Me.UserRegexLabel.Name = "UserRegexLabel"
        Me.UserRegexLabel.Size = New System.Drawing.Size(189, 13)
        Me.UserRegexLabel.TabIndex = 0
        Me.UserRegexLabel.Text = "Username matches regular expression:"
        '
        'Example2
        '
        Me.Example2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Example2.BackColor = System.Drawing.SystemColors.Window
        Me.Example2.Enabled = False
        Me.Example2.Location = New System.Drawing.Point(132, 261)
        Me.Example2.MaximumSize = New System.Drawing.Size(640, 16)
        Me.Example2.MinimumSize = New System.Drawing.Size(16, 16)
        Me.Example2.Name = "Example2"
        Me.Example2.Size = New System.Drawing.Size(19, 16)
        Me.Example2.State = System.Windows.Forms.CheckState.Unchecked
        Me.Example2.TabIndex = 2
        '
        'Example1
        '
        Me.Example1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Example1.BackColor = System.Drawing.SystemColors.Window
        Me.Example1.Enabled = False
        Me.Example1.Location = New System.Drawing.Point(6, 261)
        Me.Example1.MaximumSize = New System.Drawing.Size(640, 16)
        Me.Example1.MinimumSize = New System.Drawing.Size(16, 16)
        Me.Example1.Name = "Example1"
        Me.Example1.Size = New System.Drawing.Size(19, 16)
        Me.Example1.State = System.Windows.Forms.CheckState.Checked
        Me.Example1.TabIndex = 1
        '
        'Example3
        '
        Me.Example3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Example3.BackColor = System.Drawing.SystemColors.Window
        Me.Example3.Enabled = False
        Me.Example3.Location = New System.Drawing.Point(263, 261)
        Me.Example3.MaximumSize = New System.Drawing.Size(640, 16)
        Me.Example3.MinimumSize = New System.Drawing.Size(16, 16)
        Me.Example3.Name = "Example3"
        Me.Example3.Size = New System.Drawing.Size(19, 16)
        Me.Example3.State = System.Windows.Forms.CheckState.Indeterminate
        Me.Example3.TabIndex = 3
        '
        'UsersTab
        '
        Me.UsersTab.Controls.Add(Me.UserGroup)
        Me.UsersTab.Location = New System.Drawing.Point(4, 22)
        Me.UsersTab.Name = "UsersTab"
        Me.UsersTab.Padding = New System.Windows.Forms.Padding(3)
        Me.UsersTab.Size = New System.Drawing.Size(438, 287)
        Me.UsersTab.TabIndex = 5
        Me.UsersTab.Text = "User list"
        Me.UsersTab.UseVisualStyleBackColor = True
        '
        'UserGroup
        '
        Me.UserGroup.Controls.Add(Me.UserCount)
        Me.UserGroup.Controls.Add(Me.UserBrowse)
        Me.UserGroup.Controls.Add(Me.UserSource)
        Me.UserGroup.Controls.Add(Me.UserSourceLabel)
        Me.UserGroup.Controls.Add(Me.UserSourceTypeLabel)
        Me.UserGroup.Controls.Add(Me.ClearUsers)
        Me.UserGroup.Controls.Add(Me.RemoveUser)
        Me.UserGroup.Controls.Add(Me.UserSourceType)
        Me.UserGroup.Controls.Add(Me.AddUser)
        Me.UserGroup.Controls.Add(Me.Users)
        Me.UserGroup.Location = New System.Drawing.Point(6, 6)
        Me.UserGroup.Name = "UserGroup"
        Me.UserGroup.Size = New System.Drawing.Size(426, 275)
        Me.UserGroup.TabIndex = 7
        Me.UserGroup.TabStop = False
        Me.UserGroup.Text = "Show only edits from these users"
        '
        'UserCount
        '
        Me.UserCount.AutoSize = True
        Me.UserCount.Location = New System.Drawing.Point(6, 92)
        Me.UserCount.Name = "UserCount"
        Me.UserCount.Size = New System.Drawing.Size(40, 13)
        Me.UserCount.TabIndex = 12
        Me.UserCount.Text = "0 items"
        '
        'UserBrowse
        '
        Me.UserBrowse.Location = New System.Drawing.Point(252, 53)
        Me.UserBrowse.Name = "UserBrowse"
        Me.UserBrowse.Size = New System.Drawing.Size(70, 23)
        Me.UserBrowse.TabIndex = 11
        Me.UserBrowse.Text = "Browse..."
        Me.UserBrowse.UseVisualStyleBackColor = True
        Me.UserBrowse.Visible = False
        '
        'UserSource
        '
        Me.UserSource.Location = New System.Drawing.Point(79, 56)
        Me.UserSource.Name = "UserSource"
        Me.UserSource.Size = New System.Drawing.Size(167, 20)
        Me.UserSource.TabIndex = 10
        '
        'UserSourceLabel
        '
        Me.UserSourceLabel.Location = New System.Drawing.Point(0, 59)
        Me.UserSourceLabel.Name = "UserSourceLabel"
        Me.UserSourceLabel.Size = New System.Drawing.Size(73, 17)
        Me.UserSourceLabel.TabIndex = 9
        Me.UserSourceLabel.Text = "Source:"
        Me.UserSourceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'UserSourceTypeLabel
        '
        Me.UserSourceTypeLabel.Location = New System.Drawing.Point(0, 32)
        Me.UserSourceTypeLabel.Name = "UserSourceTypeLabel"
        Me.UserSourceTypeLabel.Size = New System.Drawing.Size(73, 18)
        Me.UserSourceTypeLabel.TabIndex = 8
        Me.UserSourceTypeLabel.Text = "Source type:"
        Me.UserSourceTypeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ClearUsers
        '
        Me.ClearUsers.Location = New System.Drawing.Point(90, 239)
        Me.ClearUsers.Name = "ClearUsers"
        Me.ClearUsers.Size = New System.Drawing.Size(75, 23)
        Me.ClearUsers.TabIndex = 4
        Me.ClearUsers.Text = "Clear"
        Me.ClearUsers.UseVisualStyleBackColor = True
        '
        'RemoveUser
        '
        Me.RemoveUser.Location = New System.Drawing.Point(9, 239)
        Me.RemoveUser.Name = "RemoveUser"
        Me.RemoveUser.Size = New System.Drawing.Size(75, 23)
        Me.RemoveUser.TabIndex = 3
        Me.RemoveUser.Text = "Remove"
        Me.RemoveUser.UseVisualStyleBackColor = True
        '
        'UserSourceType
        '
        Me.UserSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UserSourceType.FormattingEnabled = True
        Me.UserSourceType.Items.AddRange(New Object() {"(Manually add items)", "Text file"})
        Me.UserSourceType.Location = New System.Drawing.Point(79, 29)
        Me.UserSourceType.Name = "UserSourceType"
        Me.UserSourceType.Size = New System.Drawing.Size(167, 21)
        Me.UserSourceType.TabIndex = 7
        '
        'AddUser
        '
        Me.AddUser.Location = New System.Drawing.Point(252, 82)
        Me.AddUser.Name = "AddUser"
        Me.AddUser.Size = New System.Drawing.Size(70, 23)
        Me.AddUser.TabIndex = 2
        Me.AddUser.Text = "Add"
        Me.AddUser.UseVisualStyleBackColor = True
        '
        'Users
        '
        Me.Users.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Users.FormattingEnabled = True
        Me.Users.IntegralHeight = False
        Me.Users.Location = New System.Drawing.Point(9, 111)
        Me.Users.Name = "Users"
        Me.Users.Size = New System.Drawing.Size(313, 122)
        Me.Users.Sorted = True
        Me.Users.TabIndex = 1
        '
        'RevisionTab
        '
        Me.RevisionTab.Controls.Add(Me.RevisionGroup)
        Me.RevisionTab.Location = New System.Drawing.Point(4, 22)
        Me.RevisionTab.Name = "RevisionTab"
        Me.RevisionTab.Size = New System.Drawing.Size(438, 287)
        Me.RevisionTab.TabIndex = 6
        Me.RevisionTab.Text = "Revision content"
        Me.RevisionTab.UseVisualStyleBackColor = True
        '
        'RevisionGroup
        '
        Me.RevisionGroup.Controls.Add(Me.RevisionTabNote)
        Me.RevisionGroup.Controls.Add(Me.RevisionRegexLabel)
        Me.RevisionGroup.Controls.Add(Me.RevisionRegex)
        Me.RevisionGroup.Location = New System.Drawing.Point(6, 6)
        Me.RevisionGroup.Name = "RevisionGroup"
        Me.RevisionGroup.Size = New System.Drawing.Size(423, 270)
        Me.RevisionGroup.TabIndex = 0
        Me.RevisionGroup.TabStop = False
        Me.RevisionGroup.Text = "Revision content filters"
        '
        'RevisionTabNote
        '
        Me.RevisionTabNote.Location = New System.Drawing.Point(6, 231)
        Me.RevisionTabNote.Name = "RevisionTabNote"
        Me.RevisionTabNote.Size = New System.Drawing.Size(411, 39)
        Me.RevisionTabNote.TabIndex = 4
        Me.RevisionTabNote.Text = "Queue must load diff of every revision (set on Queue options tab) for settings on" & _
            " this page to work properly. Do not use with large queues."
        '
        'RevisionRegexLabel
        '
        Me.RevisionRegexLabel.AutoSize = True
        Me.RevisionRegexLabel.Location = New System.Drawing.Point(6, 26)
        Me.RevisionRegexLabel.Name = "RevisionRegexLabel"
        Me.RevisionRegexLabel.Size = New System.Drawing.Size(385, 13)
        Me.RevisionRegexLabel.TabIndex = 3
        Me.RevisionRegexLabel.Text = "Content modified in revision (as determined from diff) matches regular expression" & _
            ":"
        '
        'RevisionRegex
        '
        Me.RevisionRegex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RevisionRegex.Location = New System.Drawing.Point(9, 45)
        Me.RevisionRegex.Multiline = True
        Me.RevisionRegex.Name = "RevisionRegex"
        Me.RevisionRegex.Size = New System.Drawing.Size(334, 60)
        Me.RevisionRegex.TabIndex = 2
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(557, 453)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 9
        Me.OK.Text = "Close"
        Me.OK.UseVisualStyleBackColor = True
        '
        'MoveUp
        '
        Me.MoveUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MoveUp.Enabled = False
        Me.MoveUp.Image = Global.Huggle.My.Resources.Resources.gray_up
        Me.MoveUp.Location = New System.Drawing.Point(145, 424)
        Me.MoveUp.Name = "MoveUp"
        Me.MoveUp.Size = New System.Drawing.Size(23, 23)
        Me.MoveUp.TabIndex = 4
        Me.MoveUp.UseVisualStyleBackColor = True
        '
        'MoveDown
        '
        Me.MoveDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MoveDown.Enabled = False
        Me.MoveDown.Image = Global.Huggle.My.Resources.Resources.gray_down
        Me.MoveDown.Location = New System.Drawing.Point(145, 453)
        Me.MoveDown.Name = "MoveDown"
        Me.MoveDown.Size = New System.Drawing.Size(23, 23)
        Me.MoveDown.TabIndex = 4
        Me.MoveDown.UseVisualStyleBackColor = True
        '
        'TrayNotification
        '
        Me.TrayNotification.Location = New System.Drawing.Point(17, 145)
        Me.TrayNotification.Name = "TrayNotification"
        Me.TrayNotification.Size = New System.Drawing.Size(316, 31)
        Me.TrayNotification.TabIndex = 11
        Me.TrayNotification.Text = "Show tray notification when a revision is added to this queue" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(requires ""Show tr" & _
            "ay icon"" in system options)"
        Me.TrayNotification.UseVisualStyleBackColor = True
        '
        'QueueForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 488)
        Me.Controls.Add(Me.MoveDown)
        Me.Controls.Add(Me.MoveUp)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Tabs)
        Me.Controls.Add(Me.TypeGroup)
        Me.Controls.Add(Me.Copy)
        Me.Controls.Add(Me.Rename)
        Me.Controls.Add(Me.QueuesEmpty)
        Me.Controls.Add(Me.Delete)
        Me.Controls.Add(Me.Add)
        Me.Controls.Add(Me.QueuesLabel)
        Me.Controls.Add(Me.QueueList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "QueueForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Queues"
        Me.TypeGroup.ResumeLayout(False)
        Me.TypeGroup.PerformLayout()
        Me.Tabs.ResumeLayout(False)
        Me.PagesTab.ResumeLayout(False)
        Me.PagesTab.PerformLayout()
        Me.DynamicListTab.ResumeLayout(False)
        Me.DynamicListTab.PerformLayout()
        CType(Me.RefreshInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.OptionsTab.ResumeLayout(False)
        Me.OptionsTab.PerformLayout()
        Me.PreloadGroup.ResumeLayout(False)
        Me.PreloadGroup.PerformLayout()
        CType(Me.RemoveAfterTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TitleFiltersTab.ResumeLayout(False)
        Me.TitleFiltersTab.PerformLayout()
        Me.PageFiltersGroup.ResumeLayout(False)
        Me.PageFiltersGroup.PerformLayout()
        Me.EditFiltersTab.ResumeLayout(False)
        Me.EditFiltersTab.PerformLayout()
        Me.EditFiltersGroup.ResumeLayout(False)
        Me.EditFiltersGroup.PerformLayout()
        Me.UsersTab.ResumeLayout(False)
        Me.UserGroup.ResumeLayout(False)
        Me.UserGroup.PerformLayout()
        Me.RevisionTab.ResumeLayout(False)
        Me.RevisionGroup.ResumeLayout(False)
        Me.RevisionGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents QueueList As System.Windows.Forms.ListBox
    Friend WithEvents QueuesLabel As System.Windows.Forms.Label
    Friend WithEvents Add As System.Windows.Forms.Button
    Friend WithEvents Delete As System.Windows.Forms.Button
    Friend WithEvents Rename As System.Windows.Forms.Button
    Friend WithEvents Copy As System.Windows.Forms.Button
    Friend WithEvents Tip As System.Windows.Forms.ToolTip
    Friend WithEvents QueuesEmpty As System.Windows.Forms.Label
    Friend WithEvents TypeGroup As System.Windows.Forms.GroupBox
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents PagesTab As System.Windows.Forms.TabPage
    Friend WithEvents TitleFiltersTab As System.Windows.Forms.TabPage
    Friend WithEvents PageFiltersGroup As System.Windows.Forms.GroupBox
    Friend WithEvents PageRegexLabel As System.Windows.Forms.Label
    Friend WithEvents EditFiltersTab As System.Windows.Forms.TabPage
    Friend WithEvents EditFiltersGroup As System.Windows.Forms.GroupBox
    Friend WithEvents UserRegexLabel As System.Windows.Forms.Label
    Friend WithEvents FilterNewPage As Huggle.TriState
    Friend WithEvents Example2 As Huggle.TriState
    Friend WithEvents Example1 As Huggle.TriState
    Friend WithEvents Example3 As Huggle.TriState
    Friend WithEvents NamespacesLabel As System.Windows.Forms.Label
    Friend WithEvents Namespaces As System.Windows.Forms.CheckedListBox
    Friend WithEvents Live As System.Windows.Forms.RadioButton
    Friend WithEvents LiveList As System.Windows.Forms.RadioButton
    Friend WithEvents FixedList As System.Windows.Forms.RadioButton
    Friend WithEvents OptionsTab As System.Windows.Forms.TabPage
    Friend WithEvents RemoveOld As System.Windows.Forms.CheckBox
    Friend WithEvents SortOrderLabel As System.Windows.Forms.Label
    Friend WithEvents SortOrder As System.Windows.Forms.ComboBox
    Friend WithEvents RemoveAfter As System.Windows.Forms.CheckBox
    Friend WithEvents RemoveAfterTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents RemoveAfterTimeLabel As System.Windows.Forms.Label
    Friend WithEvents FilterIgnored As Huggle.TriState
    Friend WithEvents FilterAnonymous As Huggle.TriState
    Friend WithEvents FilterOwnUserspace As Huggle.TriState
    Friend WithEvents FilterReverts As Huggle.TriState
    Friend WithEvents ListBuilder As System.Windows.Forms.Button
    Friend WithEvents ListSelector As System.Windows.Forms.ComboBox
    Friend WithEvents ListSelectorLabel As System.Windows.Forms.Label
    Friend WithEvents QueuePages As System.Windows.Forms.ListBox
    Friend WithEvents Apply As System.Windows.Forms.Button
    Friend WithEvents ApplyFiltersLabel As System.Windows.Forms.Label
    Friend WithEvents FilterNotifications As Huggle.TriState
    Friend WithEvents FilterHuggle As Huggle.TriState
    Friend WithEvents RemoveViewed As System.Windows.Forms.CheckBox
    Friend WithEvents PageRegex As Huggle.RegexBox
    Friend WithEvents UserRegex As Huggle.RegexBox
    Friend WithEvents FilterTags As Huggle.TriState
    Friend WithEvents FilterWarnings As Huggle.TriState
    Friend WithEvents FilterBot As Huggle.TriState
    Friend WithEvents FilterAssisted As Huggle.TriState
    Friend WithEvents ExampleLabel3 As System.Windows.Forms.Label
    Friend WithEvents ExampleLabel2 As System.Windows.Forms.Label
    Friend WithEvents ExampleLabel1 As System.Windows.Forms.Label
    Friend WithEvents SummaryRegex As Huggle.RegexBox
    Friend WithEvents SummaryRegexLabel As System.Windows.Forms.Label
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents IgnorePages As System.Windows.Forms.CheckBox
    Friend WithEvents FilterMe As Huggle.TriState
    Friend WithEvents MoveUp As System.Windows.Forms.Button
    Friend WithEvents MoveDown As System.Windows.Forms.Button
    Friend WithEvents DynamicListTab As System.Windows.Forms.TabPage
    Friend WithEvents RefreshInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RefreshReAdd As System.Windows.Forms.CheckBox
    Friend WithEvents DynamicSourceLabel As System.Windows.Forms.Label
    Friend WithEvents DynamicSourceTypeLabel As System.Windows.Forms.Label
    Friend WithEvents DynamicSource As System.Windows.Forms.TextBox
    Friend WithEvents DynamicSourceType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents RefreshAlways As System.Windows.Forms.CheckBox
    Friend WithEvents DynamicList As System.Windows.Forms.RadioButton
    Friend WithEvents CheckAllSpaces As System.Windows.Forms.Button
    Friend WithEvents UsersTab As System.Windows.Forms.TabPage
    Friend WithEvents Users As System.Windows.Forms.ListBox
    Friend WithEvents ClearUsers As System.Windows.Forms.Button
    Friend WithEvents RemoveUser As System.Windows.Forms.Button
    Friend WithEvents AddUser As System.Windows.Forms.Button
    Friend WithEvents UserGroup As System.Windows.Forms.GroupBox
    Friend WithEvents UserSourceTypeLabel As System.Windows.Forms.Label
    Friend WithEvents UserSourceType As System.Windows.Forms.ComboBox
    Friend WithEvents UserSource As System.Windows.Forms.TextBox
    Friend WithEvents UserSourceLabel As System.Windows.Forms.Label
    Friend WithEvents UserCount As System.Windows.Forms.Label
    Friend WithEvents UserBrowse As System.Windows.Forms.Button
    Friend WithEvents PreloadGroup As System.Windows.Forms.GroupBox
    Friend WithEvents PreloadDiffs As System.Windows.Forms.RadioButton
    Friend WithEvents NoDiffs As System.Windows.Forms.RadioButton
    Friend WithEvents AllDiffs As System.Windows.Forms.RadioButton
    Friend WithEvents RevisionTab As System.Windows.Forms.TabPage
    Friend WithEvents RevisionGroup As System.Windows.Forms.GroupBox
    Friend WithEvents RevisionRegexLabel As System.Windows.Forms.Label
    Friend WithEvents RevisionRegex As Huggle.RegexBox
    Friend WithEvents RevisionTabNote As System.Windows.Forms.Label
    Friend WithEvents TrayNotification As System.Windows.Forms.CheckBox
End Class
