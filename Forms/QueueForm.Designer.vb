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
        Me.QueueMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.QueueMenuView = New System.Windows.Forms.ToolStripMenuItem
        Me.QueueMenuEdit = New System.Windows.Forms.ToolStripMenuItem
        Me.QueueMenuRemove = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.QueueMenuSort = New System.Windows.Forms.ToolStripMenuItem
        Me.QueuesLabel = New System.Windows.Forms.Label
        Me.AddQueue = New System.Windows.Forms.Button
        Me.RemoveQueue = New System.Windows.Forms.Button
        Me.Sort = New System.Windows.Forms.Button
        Me.Clear = New System.Windows.Forms.Button
        Me.Rename = New System.Windows.Forms.Button
        Me.Save = New System.Windows.Forms.Button
        Me.Copy = New System.Windows.Forms.Button
        Me.Tip = New System.Windows.Forms.ToolTip(Me.components)
        Me.From = New System.Windows.Forms.TextBox
        Me.Cancel = New System.Windows.Forms.Button
        Me.Limit = New System.Windows.Forms.NumericUpDown
        Me.Exclude = New System.Windows.Forms.Button
        Me.Combine = New System.Windows.Forms.Button
        Me.Intersect = New System.Windows.Forms.Button
        Me.SourceType = New System.Windows.Forms.ComboBox
        Me.QueuesEmpty = New System.Windows.Forms.Label
        Me.TypeGroup = New System.Windows.Forms.GroupBox
        Me.Live = New System.Windows.Forms.RadioButton
        Me.LiveList = New System.Windows.Forms.RadioButton
        Me.FixedList = New System.Windows.Forms.RadioButton
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.PagesTab = New System.Windows.Forms.TabPage
        Me.QueueEmpty = New System.Windows.Forms.Label
        Me.FromLabel = New System.Windows.Forms.Label
        Me.Progress = New System.Windows.Forms.Label
        Me.LimitLabel = New System.Windows.Forms.Label
        Me.Actions = New System.Windows.Forms.Button
        Me.QueueSelector = New System.Windows.Forms.ComboBox
        Me.Throbber = New huggle.Throbber
        Me.Count = New System.Windows.Forms.Label
        Me.SourceLabel = New System.Windows.Forms.Label
        Me.SourceTypeLabel = New System.Windows.Forms.Label
        Me.QueuePages = New System.Windows.Forms.ListBox
        Me.Source = New System.Windows.Forms.TextBox
        Me.Browse = New System.Windows.Forms.Button
        Me.PageFiltersTab = New System.Windows.Forms.TabPage
        Me.ApplyFilters = New System.Windows.Forms.Button
        Me.ApplyFiltersLabel = New System.Windows.Forms.Label
        Me.PageFiltersGroup = New System.Windows.Forms.GroupBox
        Me.PageRegexLabel = New System.Windows.Forms.Label
        Me.PageRegex = New System.Windows.Forms.TextBox
        Me.ArticlesOnly = New System.Windows.Forms.CheckBox
        Me.EditFiltersTab = New System.Windows.Forms.TabPage
        Me.EditFiltersGroup = New System.Windows.Forms.GroupBox
        Me.FilterNewPage = New huggle.TriState
        Me.UserRegexLabel = New System.Windows.Forms.Label
        Me.UserRegex = New System.Windows.Forms.TextBox
        Me.QueueMenu.SuspendLayout()
        CType(Me.Limit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TypeGroup.SuspendLayout()
        Me.Tabs.SuspendLayout()
        Me.PagesTab.SuspendLayout()
        Me.PageFiltersTab.SuspendLayout()
        Me.PageFiltersGroup.SuspendLayout()
        Me.EditFiltersTab.SuspendLayout()
        Me.EditFiltersGroup.SuspendLayout()
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
        Me.QueueList.Size = New System.Drawing.Size(156, 370)
        Me.QueueList.Sorted = True
        Me.QueueList.TabIndex = 1
        '
        'QueueMenu
        '
        Me.QueueMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QueueMenuView, Me.QueueMenuEdit, Me.QueueMenuRemove, Me.ToolStripSeparator1, Me.QueueMenuSort})
        Me.QueueMenu.Name = "QueueMenu"
        Me.QueueMenu.Size = New System.Drawing.Size(114, 98)
        '
        'QueueMenuView
        '
        Me.QueueMenuView.Name = "QueueMenuView"
        Me.QueueMenuView.Size = New System.Drawing.Size(113, 22)
        Me.QueueMenuView.Text = "View"
        '
        'QueueMenuEdit
        '
        Me.QueueMenuEdit.Name = "QueueMenuEdit"
        Me.QueueMenuEdit.Size = New System.Drawing.Size(113, 22)
        Me.QueueMenuEdit.Text = "Edit"
        '
        'QueueMenuRemove
        '
        Me.QueueMenuRemove.Name = "QueueMenuRemove"
        Me.QueueMenuRemove.Size = New System.Drawing.Size(113, 22)
        Me.QueueMenuRemove.Text = "Remove"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(110, 6)
        '
        'QueueMenuSort
        '
        Me.QueueMenuSort.Name = "QueueMenuSort"
        Me.QueueMenuSort.Size = New System.Drawing.Size(113, 22)
        Me.QueueMenuSort.Text = "Sort"
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
        'AddQueue
        '
        Me.AddQueue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AddQueue.Location = New System.Drawing.Point(12, 404)
        Me.AddQueue.Name = "AddQueue"
        Me.AddQueue.Size = New System.Drawing.Size(75, 23)
        Me.AddQueue.TabIndex = 3
        Me.AddQueue.Text = "Add"
        Me.Tip.SetToolTip(Me.AddQueue, "Add a queue")
        Me.AddQueue.UseVisualStyleBackColor = True
        '
        'RemoveQueue
        '
        Me.RemoveQueue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RemoveQueue.Enabled = False
        Me.RemoveQueue.Location = New System.Drawing.Point(93, 404)
        Me.RemoveQueue.Name = "RemoveQueue"
        Me.RemoveQueue.Size = New System.Drawing.Size(75, 23)
        Me.RemoveQueue.TabIndex = 4
        Me.RemoveQueue.Text = "Remove"
        Me.Tip.SetToolTip(Me.RemoveQueue, "Remove selected queue")
        Me.RemoveQueue.UseVisualStyleBackColor = True
        '
        'Sort
        '
        Me.Sort.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Sort.Enabled = False
        Me.Sort.Location = New System.Drawing.Point(152, 293)
        Me.Sort.Name = "Sort"
        Me.Sort.Size = New System.Drawing.Size(67, 23)
        Me.Sort.TabIndex = 30
        Me.Sort.Text = "Sort"
        Me.Tip.SetToolTip(Me.Sort, "Sort queue alphabetically")
        Me.Sort.UseVisualStyleBackColor = True
        '
        'Clear
        '
        Me.Clear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Clear.Enabled = False
        Me.Clear.Location = New System.Drawing.Point(79, 293)
        Me.Clear.Name = "Clear"
        Me.Clear.Size = New System.Drawing.Size(67, 23)
        Me.Clear.TabIndex = 29
        Me.Clear.Text = "Clear"
        Me.Tip.SetToolTip(Me.Clear, "Clear queue")
        Me.Clear.UseVisualStyleBackColor = True
        '
        'Rename
        '
        Me.Rename.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Rename.Enabled = False
        Me.Rename.Location = New System.Drawing.Point(93, 433)
        Me.Rename.Name = "Rename"
        Me.Rename.Size = New System.Drawing.Size(75, 23)
        Me.Rename.TabIndex = 6
        Me.Rename.Text = "Rename"
        Me.Tip.SetToolTip(Me.Rename, "Rename selected queue")
        Me.Rename.UseVisualStyleBackColor = True
        '
        'Save
        '
        Me.Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Save.Enabled = False
        Me.Save.Location = New System.Drawing.Point(6, 293)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(67, 23)
        Me.Save.TabIndex = 28
        Me.Save.Text = "Save..."
        Me.Tip.SetToolTip(Me.Save, "Save queue to file")
        Me.Save.UseVisualStyleBackColor = True
        '
        'Copy
        '
        Me.Copy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Copy.Enabled = False
        Me.Copy.Location = New System.Drawing.Point(12, 433)
        Me.Copy.Name = "Copy"
        Me.Copy.Size = New System.Drawing.Size(75, 23)
        Me.Copy.TabIndex = 5
        Me.Copy.Text = "Copy"
        Me.Tip.SetToolTip(Me.Copy, "Copy selected queue")
        Me.Copy.UseVisualStyleBackColor = True
        '
        'From
        '
        Me.From.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.From.Location = New System.Drawing.Point(275, 8)
        Me.From.Name = "From"
        Me.From.Size = New System.Drawing.Size(107, 20)
        Me.From.TabIndex = 49
        Me.Tip.SetToolTip(Me.From, "Only add pages with titles alphabetically greater than this one")
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(295, 114)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(67, 23)
        Me.Cancel.TabIndex = 61
        Me.Cancel.Text = "Cancel"
        Me.Tip.SetToolTip(Me.Cancel, "Stop this query")
        Me.Cancel.UseVisualStyleBackColor = True
        Me.Cancel.Visible = False
        '
        'Limit
        '
        Me.Limit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Limit.Enabled = False
        Me.Limit.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.Limit.Location = New System.Drawing.Point(330, 34)
        Me.Limit.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Limit.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Limit.Name = "Limit"
        Me.Limit.Size = New System.Drawing.Size(52, 20)
        Me.Limit.TabIndex = 55
        Me.Limit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Tip.SetToolTip(Me.Limit, "Maximum number of pages that can be returned by the query")
        Me.Limit.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Exclude
        '
        Me.Exclude.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Exclude.Enabled = False
        Me.Exclude.Location = New System.Drawing.Point(315, 61)
        Me.Exclude.Name = "Exclude"
        Me.Exclude.Size = New System.Drawing.Size(67, 23)
        Me.Exclude.TabIndex = 60
        Me.Exclude.Text = "Exclude"
        Me.Tip.SetToolTip(Me.Exclude, "Remove any pages from the queue that appear in the query" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(new queue = old queue " & _
                "AND NOT query)")
        Me.Exclude.UseVisualStyleBackColor = True
        '
        'Combine
        '
        Me.Combine.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Combine.Enabled = False
        Me.Combine.Location = New System.Drawing.Point(169, 61)
        Me.Combine.Name = "Combine"
        Me.Combine.Size = New System.Drawing.Size(67, 23)
        Me.Combine.TabIndex = 58
        Me.Combine.Text = "Combine"
        Me.Tip.SetToolTip(Me.Combine, "Add query results to the queue" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(new queue = old queue OR query)")
        Me.Combine.UseVisualStyleBackColor = True
        '
        'Intersect
        '
        Me.Intersect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Intersect.Enabled = False
        Me.Intersect.Location = New System.Drawing.Point(242, 61)
        Me.Intersect.Name = "Intersect"
        Me.Intersect.Size = New System.Drawing.Size(67, 23)
        Me.Intersect.TabIndex = 59
        Me.Intersect.Text = "Intersect"
        Me.Tip.SetToolTip(Me.Intersect, "Remove any pages from the queue that don't appear in query results" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(new queue = " & _
                "old queue AND query)")
        Me.Intersect.UseVisualStyleBackColor = True
        '
        'SourceType
        '
        Me.SourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SourceType.Enabled = False
        Me.SourceType.FormattingEnabled = True
        Me.SourceType.Location = New System.Drawing.Point(74, 7)
        Me.SourceType.MaxDropDownItems = 20
        Me.SourceType.Name = "SourceType"
        Me.SourceType.Size = New System.Drawing.Size(156, 21)
        Me.SourceType.TabIndex = 47
        Me.Tip.SetToolTip(Me.SourceType, "Type of query to make")
        '
        'QueuesEmpty
        '
        Me.QueuesEmpty.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.QueuesEmpty.BackColor = System.Drawing.SystemColors.Window
        Me.QueuesEmpty.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.QueuesEmpty.Location = New System.Drawing.Point(21, 180)
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
        Me.TypeGroup.Controls.Add(Me.Live)
        Me.TypeGroup.Controls.Add(Me.LiveList)
        Me.TypeGroup.Controls.Add(Me.FixedList)
        Me.TypeGroup.Location = New System.Drawing.Point(186, 12)
        Me.TypeGroup.Name = "TypeGroup"
        Me.TypeGroup.Size = New System.Drawing.Size(396, 90)
        Me.TypeGroup.TabIndex = 7
        Me.TypeGroup.TabStop = False
        Me.TypeGroup.Text = "Queue type"
        '
        'Live
        '
        Me.Live.AutoSize = True
        Me.Live.Location = New System.Drawing.Point(14, 65)
        Me.Live.Name = "Live"
        Me.Live.Size = New System.Drawing.Size(300, 17)
        Me.Live.TabIndex = 2
        Me.Live.Text = "Live: Don't use a list; show any edit that matches the filters"
        Me.Live.UseVisualStyleBackColor = True
        '
        'LiveList
        '
        Me.LiveList.AutoSize = True
        Me.LiveList.Location = New System.Drawing.Point(14, 42)
        Me.LiveList.Name = "LiveList"
        Me.LiveList.Size = New System.Drawing.Size(358, 17)
        Me.LiveList.TabIndex = 1
        Me.LiveList.Text = "Live list: Use a list of pages, show edits to those pages as they happen"
        Me.LiveList.UseVisualStyleBackColor = True
        '
        'FixedList
        '
        Me.FixedList.AutoSize = True
        Me.FixedList.Checked = True
        Me.FixedList.Location = New System.Drawing.Point(14, 19)
        Me.FixedList.Name = "FixedList"
        Me.FixedList.Size = New System.Drawing.Size(345, 17)
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
        Me.Tabs.Controls.Add(Me.PageFiltersTab)
        Me.Tabs.Controls.Add(Me.EditFiltersTab)
        Me.Tabs.Location = New System.Drawing.Point(186, 108)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(396, 348)
        Me.Tabs.TabIndex = 32
        '
        'PagesTab
        '
        Me.PagesTab.BackColor = System.Drawing.Color.Transparent
        Me.PagesTab.Controls.Add(Me.QueueEmpty)
        Me.PagesTab.Controls.Add(Me.FromLabel)
        Me.PagesTab.Controls.Add(Me.From)
        Me.PagesTab.Controls.Add(Me.Cancel)
        Me.PagesTab.Controls.Add(Me.Progress)
        Me.PagesTab.Controls.Add(Me.LimitLabel)
        Me.PagesTab.Controls.Add(Me.Actions)
        Me.PagesTab.Controls.Add(Me.Sort)
        Me.PagesTab.Controls.Add(Me.Clear)
        Me.PagesTab.Controls.Add(Me.Save)
        Me.PagesTab.Controls.Add(Me.QueueSelector)
        Me.PagesTab.Controls.Add(Me.Limit)
        Me.PagesTab.Controls.Add(Me.Throbber)
        Me.PagesTab.Controls.Add(Me.Count)
        Me.PagesTab.Controls.Add(Me.SourceLabel)
        Me.PagesTab.Controls.Add(Me.Exclude)
        Me.PagesTab.Controls.Add(Me.Combine)
        Me.PagesTab.Controls.Add(Me.Intersect)
        Me.PagesTab.Controls.Add(Me.SourceType)
        Me.PagesTab.Controls.Add(Me.SourceTypeLabel)
        Me.PagesTab.Controls.Add(Me.QueuePages)
        Me.PagesTab.Controls.Add(Me.Source)
        Me.PagesTab.Controls.Add(Me.Browse)
        Me.PagesTab.Location = New System.Drawing.Point(4, 22)
        Me.PagesTab.Name = "PagesTab"
        Me.PagesTab.Padding = New System.Windows.Forms.Padding(3)
        Me.PagesTab.Size = New System.Drawing.Size(388, 322)
        Me.PagesTab.TabIndex = 0
        Me.PagesTab.Text = "Page list"
        Me.PagesTab.UseVisualStyleBackColor = True
        '
        'QueueEmpty
        '
        Me.QueueEmpty.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueEmpty.BackColor = System.Drawing.SystemColors.Window
        Me.QueueEmpty.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.QueueEmpty.Location = New System.Drawing.Point(18, 180)
        Me.QueueEmpty.Name = "QueueEmpty"
        Me.QueueEmpty.Size = New System.Drawing.Size(359, 36)
        Me.QueueEmpty.TabIndex = 65
        Me.QueueEmpty.Text = "No items in queue" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enter a source and click ""Add"""
        Me.QueueEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.QueueEmpty.Visible = False
        '
        'FromLabel
        '
        Me.FromLabel.AutoSize = True
        Me.FromLabel.Location = New System.Drawing.Point(240, 11)
        Me.FromLabel.Name = "FromLabel"
        Me.FromLabel.Size = New System.Drawing.Size(33, 13)
        Me.FromLabel.TabIndex = 48
        Me.FromLabel.Text = "From:"
        '
        'Progress
        '
        Me.Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Progress.Location = New System.Drawing.Point(74, 87)
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(308, 15)
        Me.Progress.TabIndex = 63
        Me.Progress.Text = " "
        '
        'LimitLabel
        '
        Me.LimitLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LimitLabel.AutoSize = True
        Me.LimitLabel.Location = New System.Drawing.Point(292, 37)
        Me.LimitLabel.Name = "LimitLabel"
        Me.LimitLabel.Size = New System.Drawing.Size(36, 13)
        Me.LimitLabel.TabIndex = 54
        Me.LimitLabel.Text = "Up to:"
        '
        'Actions
        '
        Me.Actions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Actions.Enabled = False
        Me.Actions.Location = New System.Drawing.Point(225, 293)
        Me.Actions.Name = "Actions"
        Me.Actions.Size = New System.Drawing.Size(67, 23)
        Me.Actions.TabIndex = 30
        Me.Actions.Text = "Actions..."
        Me.Actions.UseVisualStyleBackColor = True
        '
        'QueueSelector
        '
        Me.QueueSelector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.QueueSelector.FormattingEnabled = True
        Me.QueueSelector.Location = New System.Drawing.Point(74, 34)
        Me.QueueSelector.Name = "QueueSelector"
        Me.QueueSelector.Size = New System.Drawing.Size(211, 21)
        Me.QueueSelector.Sorted = True
        Me.QueueSelector.TabIndex = 52
        '
        'Throbber
        '
        Me.Throbber.BackColor = System.Drawing.Color.White
        Me.Throbber.Location = New System.Drawing.Point(6, 68)
        Me.Throbber.Name = "Throbber"
        Me.Throbber.Size = New System.Drawing.Size(58, 10)
        Me.Throbber.TabIndex = 56
        '
        'Count
        '
        Me.Count.AutoSize = True
        Me.Count.Location = New System.Drawing.Point(6, 89)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(40, 13)
        Me.Count.TabIndex = 62
        Me.Count.Text = "0 items"
        '
        'SourceLabel
        '
        Me.SourceLabel.Location = New System.Drawing.Point(1, 37)
        Me.SourceLabel.Name = "SourceLabel"
        Me.SourceLabel.Size = New System.Drawing.Size(72, 16)
        Me.SourceLabel.TabIndex = 50
        Me.SourceLabel.Text = "Source:"
        Me.SourceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SourceTypeLabel
        '
        Me.SourceTypeLabel.AutoSize = True
        Me.SourceTypeLabel.Location = New System.Drawing.Point(6, 10)
        Me.SourceTypeLabel.Name = "SourceTypeLabel"
        Me.SourceTypeLabel.Size = New System.Drawing.Size(67, 13)
        Me.SourceTypeLabel.TabIndex = 46
        Me.SourceTypeLabel.Text = "Source type:"
        '
        'QueuePages
        '
        Me.QueuePages.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueuePages.Enabled = False
        Me.QueuePages.IntegralHeight = False
        Me.QueuePages.Location = New System.Drawing.Point(6, 105)
        Me.QueuePages.Name = "QueuePages"
        Me.QueuePages.Size = New System.Drawing.Size(376, 182)
        Me.QueuePages.TabIndex = 64
        '
        'Source
        '
        Me.Source.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Source.Enabled = False
        Me.Source.Location = New System.Drawing.Point(74, 35)
        Me.Source.Name = "Source"
        Me.Source.Size = New System.Drawing.Size(211, 20)
        Me.Source.TabIndex = 51
        '
        'Browse
        '
        Me.Browse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Browse.Enabled = False
        Me.Browse.Location = New System.Drawing.Point(218, 33)
        Me.Browse.Name = "Browse"
        Me.Browse.Size = New System.Drawing.Size(67, 23)
        Me.Browse.TabIndex = 53
        Me.Browse.Text = "Browse..."
        Me.Browse.UseVisualStyleBackColor = True
        Me.Browse.Visible = False
        '
        'PageFiltersTab
        '
        Me.PageFiltersTab.BackColor = System.Drawing.Color.Transparent
        Me.PageFiltersTab.Controls.Add(Me.ApplyFilters)
        Me.PageFiltersTab.Controls.Add(Me.ApplyFiltersLabel)
        Me.PageFiltersTab.Controls.Add(Me.PageFiltersGroup)
        Me.PageFiltersTab.Location = New System.Drawing.Point(4, 22)
        Me.PageFiltersTab.Name = "PageFiltersTab"
        Me.PageFiltersTab.Padding = New System.Windows.Forms.Padding(3)
        Me.PageFiltersTab.Size = New System.Drawing.Size(388, 322)
        Me.PageFiltersTab.TabIndex = 1
        Me.PageFiltersTab.Text = "Page filters"
        Me.PageFiltersTab.UseVisualStyleBackColor = True
        '
        'ApplyFilters
        '
        Me.ApplyFilters.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ApplyFilters.Location = New System.Drawing.Point(301, 287)
        Me.ApplyFilters.Name = "ApplyFilters"
        Me.ApplyFilters.Size = New System.Drawing.Size(75, 23)
        Me.ApplyFilters.TabIndex = 57
        Me.ApplyFilters.Text = "Apply"
        Me.ApplyFilters.UseVisualStyleBackColor = True
        '
        'ApplyFiltersLabel
        '
        Me.ApplyFiltersLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ApplyFiltersLabel.AutoSize = True
        Me.ApplyFiltersLabel.Location = New System.Drawing.Point(5, 286)
        Me.ApplyFiltersLabel.Name = "ApplyFiltersLabel"
        Me.ApplyFiltersLabel.Size = New System.Drawing.Size(268, 26)
        Me.ApplyFiltersLabel.TabIndex = 56
        Me.ApplyFiltersLabel.Text = "Click Apply to apply these filters to the existing page list." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "These filters also" & _
            " affect the output of the list builder."
        '
        'PageFiltersGroup
        '
        Me.PageFiltersGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PageFiltersGroup.Controls.Add(Me.PageRegexLabel)
        Me.PageFiltersGroup.Controls.Add(Me.PageRegex)
        Me.PageFiltersGroup.Controls.Add(Me.ArticlesOnly)
        Me.PageFiltersGroup.Location = New System.Drawing.Point(6, 6)
        Me.PageFiltersGroup.Name = "PageFiltersGroup"
        Me.PageFiltersGroup.Size = New System.Drawing.Size(376, 275)
        Me.PageFiltersGroup.TabIndex = 55
        Me.PageFiltersGroup.TabStop = False
        Me.PageFiltersGroup.Text = "Page filters"
        '
        'PageRegexLabel
        '
        Me.PageRegexLabel.AutoSize = True
        Me.PageRegexLabel.Location = New System.Drawing.Point(6, 45)
        Me.PageRegexLabel.Name = "PageRegexLabel"
        Me.PageRegexLabel.Size = New System.Drawing.Size(161, 13)
        Me.PageRegexLabel.TabIndex = 55
        Me.PageRegexLabel.Text = "Title matches regular expression:"
        '
        'PageRegex
        '
        Me.PageRegex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PageRegex.Location = New System.Drawing.Point(173, 42)
        Me.PageRegex.Name = "PageRegex"
        Me.PageRegex.Size = New System.Drawing.Size(197, 20)
        Me.PageRegex.TabIndex = 54
        '
        'ArticlesOnly
        '
        Me.ArticlesOnly.AutoSize = True
        Me.ArticlesOnly.Location = New System.Drawing.Point(9, 22)
        Me.ArticlesOnly.Name = "ArticlesOnly"
        Me.ArticlesOnly.Size = New System.Drawing.Size(82, 17)
        Me.ArticlesOnly.TabIndex = 53
        Me.ArticlesOnly.Text = "Articles only"
        Me.ArticlesOnly.UseVisualStyleBackColor = True
        '
        'EditFiltersTab
        '
        Me.EditFiltersTab.Controls.Add(Me.EditFiltersGroup)
        Me.EditFiltersTab.Location = New System.Drawing.Point(4, 22)
        Me.EditFiltersTab.Name = "EditFiltersTab"
        Me.EditFiltersTab.Size = New System.Drawing.Size(388, 322)
        Me.EditFiltersTab.TabIndex = 2
        Me.EditFiltersTab.Text = "Filters"
        Me.EditFiltersTab.UseVisualStyleBackColor = True
        '
        'EditFiltersGroup
        '
        Me.EditFiltersGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EditFiltersGroup.Controls.Add(Me.FilterNewPage)
        Me.EditFiltersGroup.Controls.Add(Me.UserRegexLabel)
        Me.EditFiltersGroup.Controls.Add(Me.UserRegex)
        Me.EditFiltersGroup.Location = New System.Drawing.Point(6, 6)
        Me.EditFiltersGroup.Name = "EditFiltersGroup"
        Me.EditFiltersGroup.Size = New System.Drawing.Size(376, 309)
        Me.EditFiltersGroup.TabIndex = 2
        Me.EditFiltersGroup.TabStop = False
        Me.EditFiltersGroup.Text = "Edit filters"
        '
        'FilterNewPage
        '
        Me.FilterNewPage.BackColor = System.Drawing.SystemColors.Window
        Me.FilterNewPage.Label = "New page"
        Me.FilterNewPage.Location = New System.Drawing.Point(9, 64)
        Me.FilterNewPage.MaximumSize = New System.Drawing.Size(640, 16)
        Me.FilterNewPage.MinimumSize = New System.Drawing.Size(16, 16)
        Me.FilterNewPage.Name = "FilterNewPage"
        Me.FilterNewPage.Size = New System.Drawing.Size(75, 16)
        Me.FilterNewPage.State = System.Windows.Forms.CheckState.Indeterminate
        Me.FilterNewPage.TabIndex = 58
        '
        'UserRegexLabel
        '
        Me.UserRegexLabel.AutoSize = True
        Me.UserRegexLabel.Location = New System.Drawing.Point(6, 22)
        Me.UserRegexLabel.Name = "UserRegexLabel"
        Me.UserRegexLabel.Size = New System.Drawing.Size(189, 13)
        Me.UserRegexLabel.TabIndex = 57
        Me.UserRegexLabel.Text = "Username matches regular expression:"
        '
        'UserRegex
        '
        Me.UserRegex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserRegex.Location = New System.Drawing.Point(201, 19)
        Me.UserRegex.Name = "UserRegex"
        Me.UserRegex.Size = New System.Drawing.Size(169, 20)
        Me.UserRegex.TabIndex = 56
        '
        'QueueForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 468)
        Me.Controls.Add(Me.Tabs)
        Me.Controls.Add(Me.TypeGroup)
        Me.Controls.Add(Me.QueuesEmpty)
        Me.Controls.Add(Me.Copy)
        Me.Controls.Add(Me.Rename)
        Me.Controls.Add(Me.RemoveQueue)
        Me.Controls.Add(Me.AddQueue)
        Me.Controls.Add(Me.QueuesLabel)
        Me.Controls.Add(Me.QueueList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "QueueForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Queues"
        Me.QueueMenu.ResumeLayout(False)
        CType(Me.Limit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TypeGroup.ResumeLayout(False)
        Me.TypeGroup.PerformLayout()
        Me.Tabs.ResumeLayout(False)
        Me.PagesTab.ResumeLayout(False)
        Me.PagesTab.PerformLayout()
        Me.PageFiltersTab.ResumeLayout(False)
        Me.PageFiltersTab.PerformLayout()
        Me.PageFiltersGroup.ResumeLayout(False)
        Me.PageFiltersGroup.PerformLayout()
        Me.EditFiltersTab.ResumeLayout(False)
        Me.EditFiltersGroup.ResumeLayout(False)
        Me.EditFiltersGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents QueueList As System.Windows.Forms.ListBox
    Friend WithEvents QueuesLabel As System.Windows.Forms.Label
    Friend WithEvents AddQueue As System.Windows.Forms.Button
    Friend WithEvents RemoveQueue As System.Windows.Forms.Button
    Friend WithEvents Sort As System.Windows.Forms.Button
    Friend WithEvents Clear As System.Windows.Forms.Button
    Friend WithEvents Rename As System.Windows.Forms.Button
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents Copy As System.Windows.Forms.Button
    Friend WithEvents Tip As System.Windows.Forms.ToolTip
    Friend WithEvents QueuesEmpty As System.Windows.Forms.Label
    Friend WithEvents TypeGroup As System.Windows.Forms.GroupBox
    Friend WithEvents LiveList As System.Windows.Forms.RadioButton
    Friend WithEvents FixedList As System.Windows.Forms.RadioButton
    Friend WithEvents QueueMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents QueueMenuRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents QueueMenuSort As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueMenuView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueMenuEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Live As System.Windows.Forms.RadioButton
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents PagesTab As System.Windows.Forms.TabPage
    Friend WithEvents QueueEmpty As System.Windows.Forms.Label
    Friend WithEvents FromLabel As System.Windows.Forms.Label
    Friend WithEvents From As System.Windows.Forms.TextBox
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Progress As System.Windows.Forms.Label
    Friend WithEvents LimitLabel As System.Windows.Forms.Label
    Friend WithEvents QueueSelector As System.Windows.Forms.ComboBox
    Friend WithEvents Limit As System.Windows.Forms.NumericUpDown
    Friend WithEvents Throbber As huggle.Throbber
    Friend WithEvents Count As System.Windows.Forms.Label
    Friend WithEvents SourceLabel As System.Windows.Forms.Label
    Friend WithEvents Exclude As System.Windows.Forms.Button
    Friend WithEvents Combine As System.Windows.Forms.Button
    Friend WithEvents Intersect As System.Windows.Forms.Button
    Friend WithEvents SourceType As System.Windows.Forms.ComboBox
    Friend WithEvents SourceTypeLabel As System.Windows.Forms.Label
    Friend WithEvents QueuePages As System.Windows.Forms.ListBox
    Friend WithEvents Source As System.Windows.Forms.TextBox
    Friend WithEvents Browse As System.Windows.Forms.Button
    Friend WithEvents PageFiltersTab As System.Windows.Forms.TabPage
    Friend WithEvents Actions As System.Windows.Forms.Button
    Friend WithEvents PageFiltersGroup As System.Windows.Forms.GroupBox
    Friend WithEvents PageRegexLabel As System.Windows.Forms.Label
    Friend WithEvents PageRegex As System.Windows.Forms.TextBox
    Friend WithEvents ArticlesOnly As System.Windows.Forms.CheckBox
    Friend WithEvents ApplyFilters As System.Windows.Forms.Button
    Friend WithEvents ApplyFiltersLabel As System.Windows.Forms.Label
    Friend WithEvents EditFiltersTab As System.Windows.Forms.TabPage
    Friend WithEvents EditFiltersGroup As System.Windows.Forms.GroupBox
    Friend WithEvents UserRegexLabel As System.Windows.Forms.Label
    Friend WithEvents UserRegex As System.Windows.Forms.TextBox
    Friend WithEvents FilterNewPage As huggle.TriState
End Class
