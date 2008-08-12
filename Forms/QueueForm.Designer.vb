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
        Me.QueuePages = New System.Windows.Forms.ListBox
        Me.QueueMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.QueueMenuView = New System.Windows.Forms.ToolStripMenuItem
        Me.QueueMenuEdit = New System.Windows.Forms.ToolStripMenuItem
        Me.QueueMenuRemove = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.QueueMenuSort = New System.Windows.Forms.ToolStripMenuItem
        Me.QueuesLabel = New System.Windows.Forms.Label
        Me.AddQueue = New System.Windows.Forms.Button
        Me.RemoveQueue = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Sort = New System.Windows.Forms.Button
        Me.Clear = New System.Windows.Forms.Button
        Me.Rename = New System.Windows.Forms.Button
        Me.Save = New System.Windows.Forms.Button
        Me.Copy = New System.Windows.Forms.Button
        Me.Tip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Filters = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.Limit = New System.Windows.Forms.NumericUpDown
        Me.Exclude = New System.Windows.Forms.Button
        Me.Combine = New System.Windows.Forms.Button
        Me.Intersect = New System.Windows.Forms.Button
        Me.SourceType = New System.Windows.Forms.ComboBox
        Me.From = New System.Windows.Forms.TextBox
        Me.QueuesEmpty = New System.Windows.Forms.Label
        Me.QueueEmpty = New System.Windows.Forms.Label
        Me.TypeGroup = New System.Windows.Forms.GroupBox
        Me.LiveType = New System.Windows.Forms.RadioButton
        Me.FixedType = New System.Windows.Forms.RadioButton
        Me.QueueSelector = New System.Windows.Forms.ComboBox
        Me.Progress = New System.Windows.Forms.Label
        Me.LimitLabel = New System.Windows.Forms.Label
        Me.Count = New System.Windows.Forms.Label
        Me.Browse = New System.Windows.Forms.Button
        Me.SourceLabel = New System.Windows.Forms.Label
        Me.Source = New System.Windows.Forms.TextBox
        Me.SourceTypeLabel = New System.Windows.Forms.Label
        Me.FromLabel = New System.Windows.Forms.Label
        Me.Throbber = New huggle.Throbber
        Me.QueueMenu.SuspendLayout()
        CType(Me.Limit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TypeGroup.SuspendLayout()
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
        Me.QueueList.Size = New System.Drawing.Size(156, 320)
        Me.QueueList.Sorted = True
        Me.QueueList.TabIndex = 1
        '
        'QueuePages
        '
        Me.QueuePages.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueuePages.Enabled = False
        Me.QueuePages.IntegralHeight = False
        Me.QueuePages.Location = New System.Drawing.Point(186, 184)
        Me.QueuePages.Name = "QueuePages"
        Me.QueuePages.Size = New System.Drawing.Size(376, 193)
        Me.QueuePages.TabIndex = 26
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
        Me.AddQueue.Location = New System.Drawing.Point(12, 354)
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
        Me.RemoveQueue.Location = New System.Drawing.Point(93, 354)
        Me.RemoveQueue.Name = "RemoveQueue"
        Me.RemoveQueue.Size = New System.Drawing.Size(75, 23)
        Me.RemoveQueue.TabIndex = 4
        Me.RemoveQueue.Text = "Remove"
        Me.Tip.SetToolTip(Me.RemoveQueue, "Remove selected queue")
        Me.RemoveQueue.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(487, 383)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 31
        Me.OK.Text = "Close"
        Me.Tip.SetToolTip(Me.OK, "Close window")
        Me.OK.UseVisualStyleBackColor = True
        '
        'Sort
        '
        Me.Sort.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Sort.Enabled = False
        Me.Sort.Location = New System.Drawing.Point(332, 383)
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
        Me.Clear.Location = New System.Drawing.Point(259, 383)
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
        Me.Rename.Location = New System.Drawing.Point(93, 383)
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
        Me.Save.Location = New System.Drawing.Point(186, 383)
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
        Me.Copy.Location = New System.Drawing.Point(12, 383)
        Me.Copy.Name = "Copy"
        Me.Copy.Size = New System.Drawing.Size(75, 23)
        Me.Copy.TabIndex = 5
        Me.Copy.Text = "Copy"
        Me.Tip.SetToolTip(Me.Copy, "Copy selected queue")
        Me.Copy.UseVisualStyleBackColor = True
        '
        'Filters
        '
        Me.Filters.Enabled = False
        Me.Filters.Location = New System.Drawing.Point(251, 140)
        Me.Filters.Name = "Filters"
        Me.Filters.Size = New System.Drawing.Size(67, 23)
        Me.Filters.TabIndex = 19
        Me.Filters.Text = "Filters..."
        Me.Tip.SetToolTip(Me.Filters, "Manipulate the contents of the queue")
        Me.Filters.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(476, 196)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(67, 23)
        Me.Cancel.TabIndex = 23
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
        Me.Limit.Location = New System.Drawing.Point(510, 114)
        Me.Limit.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Limit.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Limit.Name = "Limit"
        Me.Limit.Size = New System.Drawing.Size(52, 20)
        Me.Limit.TabIndex = 17
        Me.Limit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Tip.SetToolTip(Me.Limit, "Maximum number of pages that can be returned by the query")
        Me.Limit.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Exclude
        '
        Me.Exclude.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Exclude.Enabled = False
        Me.Exclude.Location = New System.Drawing.Point(495, 140)
        Me.Exclude.Name = "Exclude"
        Me.Exclude.Size = New System.Drawing.Size(67, 23)
        Me.Exclude.TabIndex = 22
        Me.Exclude.Text = "Exclude"
        Me.Tip.SetToolTip(Me.Exclude, "Remove any pages from the queue that appear in the query" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(new queue = old queue " & _
                "AND NOT query)")
        Me.Exclude.UseVisualStyleBackColor = True
        '
        'Combine
        '
        Me.Combine.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Combine.Enabled = False
        Me.Combine.Location = New System.Drawing.Point(349, 140)
        Me.Combine.Name = "Combine"
        Me.Combine.Size = New System.Drawing.Size(67, 23)
        Me.Combine.TabIndex = 20
        Me.Combine.Text = "Combine"
        Me.Tip.SetToolTip(Me.Combine, "Add query results to the queue" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(new queue = old queue OR query)")
        Me.Combine.UseVisualStyleBackColor = True
        '
        'Intersect
        '
        Me.Intersect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Intersect.Enabled = False
        Me.Intersect.Location = New System.Drawing.Point(422, 140)
        Me.Intersect.Name = "Intersect"
        Me.Intersect.Size = New System.Drawing.Size(67, 23)
        Me.Intersect.TabIndex = 21
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
        Me.SourceType.Location = New System.Drawing.Point(251, 86)
        Me.SourceType.MaxDropDownItems = 20
        Me.SourceType.Name = "SourceType"
        Me.SourceType.Size = New System.Drawing.Size(160, 21)
        Me.SourceType.TabIndex = 9
        Me.Tip.SetToolTip(Me.SourceType, "Type of query to make")
        '
        'From
        '
        Me.From.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.From.Location = New System.Drawing.Point(452, 87)
        Me.From.Name = "From"
        Me.From.Size = New System.Drawing.Size(110, 20)
        Me.From.TabIndex = 11
        Me.Tip.SetToolTip(Me.From, "Only add pages with titles alphabetically greater than this one")
        '
        'QueuesEmpty
        '
        Me.QueuesEmpty.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.QueuesEmpty.BackColor = System.Drawing.SystemColors.Window
        Me.QueuesEmpty.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.QueuesEmpty.Location = New System.Drawing.Point(21, 155)
        Me.QueuesEmpty.Name = "QueuesEmpty"
        Me.QueuesEmpty.Size = New System.Drawing.Size(138, 36)
        Me.QueuesEmpty.TabIndex = 2
        Me.QueuesEmpty.Text = "No queues defined" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click ""Add"" to create one"
        Me.QueuesEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.QueuesEmpty.Visible = False
        '
        'QueueEmpty
        '
        Me.QueueEmpty.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueEmpty.BackColor = System.Drawing.SystemColors.Window
        Me.QueueEmpty.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.QueueEmpty.Location = New System.Drawing.Point(192, 264)
        Me.QueueEmpty.Name = "QueueEmpty"
        Me.QueueEmpty.Size = New System.Drawing.Size(365, 36)
        Me.QueueEmpty.TabIndex = 27
        Me.QueueEmpty.Text = "No items in queue" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enter a source and click ""Add"""
        Me.QueueEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.QueueEmpty.Visible = False
        '
        'TypeGroup
        '
        Me.TypeGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TypeGroup.Controls.Add(Me.LiveType)
        Me.TypeGroup.Controls.Add(Me.FixedType)
        Me.TypeGroup.Location = New System.Drawing.Point(186, 12)
        Me.TypeGroup.Name = "TypeGroup"
        Me.TypeGroup.Size = New System.Drawing.Size(376, 68)
        Me.TypeGroup.TabIndex = 7
        Me.TypeGroup.TabStop = False
        Me.TypeGroup.Text = "Queue type"
        '
        'LiveType
        '
        Me.LiveType.AutoSize = True
        Me.LiveType.Location = New System.Drawing.Point(14, 42)
        Me.LiveType.Name = "LiveType"
        Me.LiveType.Size = New System.Drawing.Size(313, 17)
        Me.LiveType.TabIndex = 1
        Me.LiveType.Text = "Live – Show new edits to pages in the queue as they happen"
        Me.LiveType.UseVisualStyleBackColor = True
        '
        'FixedType
        '
        Me.FixedType.AutoSize = True
        Me.FixedType.Checked = True
        Me.FixedType.Location = New System.Drawing.Point(14, 19)
        Me.FixedType.Name = "FixedType"
        Me.FixedType.Size = New System.Drawing.Size(282, 17)
        Me.FixedType.TabIndex = 0
        Me.FixedType.TabStop = True
        Me.FixedType.Text = "Fixed – Show the most recent edit to each page in turn"
        Me.FixedType.UseVisualStyleBackColor = True
        '
        'QueueSelector
        '
        Me.QueueSelector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.QueueSelector.FormattingEnabled = True
        Me.QueueSelector.Location = New System.Drawing.Point(251, 113)
        Me.QueueSelector.Name = "QueueSelector"
        Me.QueueSelector.Size = New System.Drawing.Size(210, 21)
        Me.QueueSelector.Sorted = True
        Me.QueueSelector.TabIndex = 14
        '
        'Progress
        '
        Me.Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Progress.Location = New System.Drawing.Point(251, 166)
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(311, 16)
        Me.Progress.TabIndex = 25
        Me.Progress.Text = " "
        '
        'LimitLabel
        '
        Me.LimitLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LimitLabel.AutoSize = True
        Me.LimitLabel.Location = New System.Drawing.Point(472, 117)
        Me.LimitLabel.Name = "LimitLabel"
        Me.LimitLabel.Size = New System.Drawing.Size(36, 13)
        Me.LimitLabel.TabIndex = 16
        Me.LimitLabel.Text = "Up to:"
        '
        'Count
        '
        Me.Count.AutoSize = True
        Me.Count.Location = New System.Drawing.Point(183, 167)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(40, 13)
        Me.Count.TabIndex = 24
        Me.Count.Text = "0 items"
        '
        'Browse
        '
        Me.Browse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Browse.Enabled = False
        Me.Browse.Location = New System.Drawing.Point(394, 112)
        Me.Browse.Name = "Browse"
        Me.Browse.Size = New System.Drawing.Size(67, 23)
        Me.Browse.TabIndex = 15
        Me.Browse.Text = "Browse..."
        Me.Browse.UseVisualStyleBackColor = True
        Me.Browse.Visible = False
        '
        'SourceLabel
        '
        Me.SourceLabel.Location = New System.Drawing.Point(178, 116)
        Me.SourceLabel.Name = "SourceLabel"
        Me.SourceLabel.Size = New System.Drawing.Size(72, 16)
        Me.SourceLabel.TabIndex = 12
        Me.SourceLabel.Text = "Source:"
        Me.SourceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Source
        '
        Me.Source.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Source.Enabled = False
        Me.Source.Location = New System.Drawing.Point(251, 114)
        Me.Source.Name = "Source"
        Me.Source.Size = New System.Drawing.Size(210, 20)
        Me.Source.TabIndex = 13
        '
        'SourceTypeLabel
        '
        Me.SourceTypeLabel.AutoSize = True
        Me.SourceTypeLabel.Location = New System.Drawing.Point(183, 89)
        Me.SourceTypeLabel.Name = "SourceTypeLabel"
        Me.SourceTypeLabel.Size = New System.Drawing.Size(67, 13)
        Me.SourceTypeLabel.TabIndex = 8
        Me.SourceTypeLabel.Text = "Source type:"
        '
        'FromLabel
        '
        Me.FromLabel.AutoSize = True
        Me.FromLabel.Location = New System.Drawing.Point(417, 90)
        Me.FromLabel.Name = "FromLabel"
        Me.FromLabel.Size = New System.Drawing.Size(33, 13)
        Me.FromLabel.TabIndex = 10
        Me.FromLabel.Text = "From:"
        '
        'Throbber
        '
        Me.Throbber.BackColor = System.Drawing.Color.White
        Me.Throbber.Location = New System.Drawing.Point(186, 147)
        Me.Throbber.Name = "Throbber"
        Me.Throbber.Size = New System.Drawing.Size(58, 10)
        Me.Throbber.TabIndex = 18
        '
        'QueueForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 418)
        Me.Controls.Add(Me.FromLabel)
        Me.Controls.Add(Me.From)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.LimitLabel)
        Me.Controls.Add(Me.QueueSelector)
        Me.Controls.Add(Me.Limit)
        Me.Controls.Add(Me.Throbber)
        Me.Controls.Add(Me.Count)
        Me.Controls.Add(Me.SourceLabel)
        Me.Controls.Add(Me.Exclude)
        Me.Controls.Add(Me.Combine)
        Me.Controls.Add(Me.Intersect)
        Me.Controls.Add(Me.SourceType)
        Me.Controls.Add(Me.SourceTypeLabel)
        Me.Controls.Add(Me.Filters)
        Me.Controls.Add(Me.TypeGroup)
        Me.Controls.Add(Me.QueueEmpty)
        Me.Controls.Add(Me.QueuesEmpty)
        Me.Controls.Add(Me.Copy)
        Me.Controls.Add(Me.Rename)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.Clear)
        Me.Controls.Add(Me.Sort)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.RemoveQueue)
        Me.Controls.Add(Me.AddQueue)
        Me.Controls.Add(Me.QueuesLabel)
        Me.Controls.Add(Me.QueueList)
        Me.Controls.Add(Me.QueuePages)
        Me.Controls.Add(Me.Browse)
        Me.Controls.Add(Me.Source)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents QueueList As System.Windows.Forms.ListBox
    Friend WithEvents QueuePages As System.Windows.Forms.ListBox
    Friend WithEvents QueuesLabel As System.Windows.Forms.Label
    Friend WithEvents AddQueue As System.Windows.Forms.Button
    Friend WithEvents RemoveQueue As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Sort As System.Windows.Forms.Button
    Friend WithEvents Clear As System.Windows.Forms.Button
    Friend WithEvents Rename As System.Windows.Forms.Button
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents Copy As System.Windows.Forms.Button
    Friend WithEvents Tip As System.Windows.Forms.ToolTip
    Friend WithEvents QueuesEmpty As System.Windows.Forms.Label
    Friend WithEvents QueueEmpty As System.Windows.Forms.Label
    Friend WithEvents TypeGroup As System.Windows.Forms.GroupBox
    Friend WithEvents LiveType As System.Windows.Forms.RadioButton
    Friend WithEvents FixedType As System.Windows.Forms.RadioButton
    Friend WithEvents Filters As System.Windows.Forms.Button
    Friend WithEvents QueueSelector As System.Windows.Forms.ComboBox
    Friend WithEvents Progress As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents LimitLabel As System.Windows.Forms.Label
    Friend WithEvents Limit As System.Windows.Forms.NumericUpDown
    Friend WithEvents Throbber As huggle.Throbber
    Friend WithEvents Count As System.Windows.Forms.Label
    Friend WithEvents Browse As System.Windows.Forms.Button
    Friend WithEvents SourceLabel As System.Windows.Forms.Label
    Friend WithEvents Exclude As System.Windows.Forms.Button
    Friend WithEvents Combine As System.Windows.Forms.Button
    Friend WithEvents Intersect As System.Windows.Forms.Button
    Friend WithEvents Source As System.Windows.Forms.TextBox
    Friend WithEvents SourceType As System.Windows.Forms.ComboBox
    Friend WithEvents SourceTypeLabel As System.Windows.Forms.Label
    Friend WithEvents From As System.Windows.Forms.TextBox
    Friend WithEvents FromLabel As System.Windows.Forms.Label
    Friend WithEvents QueueMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents QueueMenuRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents QueueMenuSort As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueMenuView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QueueMenuEdit As System.Windows.Forms.ToolStripMenuItem
End Class
