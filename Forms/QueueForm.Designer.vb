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
        Me.Queues = New System.Windows.Forms.ListBox
        Me.Queue = New System.Windows.Forms.ListBox
        Me.Source = New System.Windows.Forms.TextBox
        Me.SourceType = New System.Windows.Forms.ComboBox
        Me.SourceTypeLabel = New System.Windows.Forms.Label
        Me.Intersect = New System.Windows.Forms.Button
        Me.Combine = New System.Windows.Forms.Button
        Me.Exclude = New System.Windows.Forms.Button
        Me.SourceLabel = New System.Windows.Forms.Label
        Me.QueuesLabel = New System.Windows.Forms.Label
        Me.AddQueue = New System.Windows.Forms.Button
        Me.RemoveQueue = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Sort = New System.Windows.Forms.Button
        Me.RemoveItem = New System.Windows.Forms.Button
        Me.AddItem = New System.Windows.Forms.Button
        Me.Clear = New System.Windows.Forms.Button
        Me.Count = New System.Windows.Forms.Label
        Me.Rename = New System.Windows.Forms.Button
        Me.Save = New System.Windows.Forms.Button
        Me.Copy = New System.Windows.Forms.Button
        Me.Limit = New System.Windows.Forms.NumericUpDown
        Me.LimitLabel = New System.Windows.Forms.Label
        Me.Cancel = New System.Windows.Forms.Button
        Me.Browse = New System.Windows.Forms.Button
        Me.Progress = New System.Windows.Forms.Label
        Me.ArticlesOnly = New System.Windows.Forms.CheckBox
        Me.Throbber = New huggle.Throbber
        Me.QueueSelector = New System.Windows.Forms.ComboBox
        Me.Tip = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.Limit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Queues
        '
        Me.Queues.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Queues.FormattingEnabled = True
        Me.Queues.IntegralHeight = False
        Me.Queues.Location = New System.Drawing.Point(12, 28)
        Me.Queues.Name = "Queues"
        Me.Queues.Size = New System.Drawing.Size(156, 290)
        Me.Queues.Sorted = True
        Me.Queues.TabIndex = 1
        '
        'Queue
        '
        Me.Queue.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Queue.Enabled = False
        Me.Queue.FormattingEnabled = True
        Me.Queue.IntegralHeight = False
        Me.Queue.Location = New System.Drawing.Point(186, 114)
        Me.Queue.Name = "Queue"
        Me.Queue.Size = New System.Drawing.Size(282, 204)
        Me.Queue.TabIndex = 21
        '
        'Source
        '
        Me.Source.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Source.Enabled = False
        Me.Source.Location = New System.Drawing.Point(247, 38)
        Me.Source.Name = "Source"
        Me.Source.Size = New System.Drawing.Size(221, 20)
        Me.Source.TabIndex = 11
        '
        'SourceType
        '
        Me.SourceType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SourceType.Enabled = False
        Me.SourceType.FormattingEnabled = True
        Me.SourceType.Location = New System.Drawing.Point(247, 12)
        Me.SourceType.MaxDropDownItems = 20
        Me.SourceType.Name = "SourceType"
        Me.SourceType.Size = New System.Drawing.Size(125, 21)
        Me.SourceType.TabIndex = 7
        Me.Tip.SetToolTip(Me.SourceType, "Type of query to make")
        '
        'SourceTypeLabel
        '
        Me.SourceTypeLabel.AutoSize = True
        Me.SourceTypeLabel.Location = New System.Drawing.Point(179, 15)
        Me.SourceTypeLabel.Name = "SourceTypeLabel"
        Me.SourceTypeLabel.Size = New System.Drawing.Size(67, 13)
        Me.SourceTypeLabel.TabIndex = 6
        Me.SourceTypeLabel.Text = "Source type:"
        '
        'Intersect
        '
        Me.Intersect.Enabled = False
        Me.Intersect.Location = New System.Drawing.Point(322, 63)
        Me.Intersect.Name = "Intersect"
        Me.Intersect.Size = New System.Drawing.Size(70, 23)
        Me.Intersect.TabIndex = 16
        Me.Intersect.Text = "Intersect"
        Me.Tip.SetToolTip(Me.Intersect, "Remove any pages from the queue that don't appear in query results" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(new queue = " & _
                "old queue AND query)")
        Me.Intersect.UseVisualStyleBackColor = True
        '
        'Combine
        '
        Me.Combine.Enabled = False
        Me.Combine.Location = New System.Drawing.Point(246, 63)
        Me.Combine.Name = "Combine"
        Me.Combine.Size = New System.Drawing.Size(70, 23)
        Me.Combine.TabIndex = 15
        Me.Combine.Text = "Combine"
        Me.Tip.SetToolTip(Me.Combine, "Add query results to the queue" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(new queue = old queue OR query)")
        Me.Combine.UseVisualStyleBackColor = True
        '
        'Exclude
        '
        Me.Exclude.Enabled = False
        Me.Exclude.Location = New System.Drawing.Point(398, 63)
        Me.Exclude.Name = "Exclude"
        Me.Exclude.Size = New System.Drawing.Size(70, 23)
        Me.Exclude.TabIndex = 17
        Me.Exclude.Text = "Exclude"
        Me.Tip.SetToolTip(Me.Exclude, "Remove any pages from the queue that appear in the query" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(new queue = old queue " & _
                "AND NOT query)")
        Me.Exclude.UseVisualStyleBackColor = True
        '
        'SourceLabel
        '
        Me.SourceLabel.Location = New System.Drawing.Point(174, 41)
        Me.SourceLabel.Name = "SourceLabel"
        Me.SourceLabel.Size = New System.Drawing.Size(72, 16)
        Me.SourceLabel.TabIndex = 10
        Me.SourceLabel.Text = "Source:"
        Me.SourceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.AddQueue.Location = New System.Drawing.Point(12, 324)
        Me.AddQueue.Name = "AddQueue"
        Me.AddQueue.Size = New System.Drawing.Size(75, 23)
        Me.AddQueue.TabIndex = 2
        Me.AddQueue.Text = "Add"
        Me.Tip.SetToolTip(Me.AddQueue, "Add a queue")
        Me.AddQueue.UseVisualStyleBackColor = True
        '
        'RemoveQueue
        '
        Me.RemoveQueue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RemoveQueue.Enabled = False
        Me.RemoveQueue.Location = New System.Drawing.Point(93, 324)
        Me.RemoveQueue.Name = "RemoveQueue"
        Me.RemoveQueue.Size = New System.Drawing.Size(75, 23)
        Me.RemoveQueue.TabIndex = 3
        Me.RemoveQueue.Text = "Remove"
        Me.Tip.SetToolTip(Me.RemoveQueue, "Remove selected queue")
        Me.RemoveQueue.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(393, 353)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 28
        Me.OK.Text = "Close"
        Me.Tip.SetToolTip(Me.OK, "Close window")
        Me.OK.UseVisualStyleBackColor = True
        '
        'Sort
        '
        Me.Sort.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Sort.Enabled = False
        Me.Sort.Location = New System.Drawing.Point(330, 324)
        Me.Sort.Name = "Sort"
        Me.Sort.Size = New System.Drawing.Size(66, 23)
        Me.Sort.TabIndex = 24
        Me.Sort.Text = "Sort"
        Me.Tip.SetToolTip(Me.Sort, "Sort queue alphabetically")
        Me.Sort.UseVisualStyleBackColor = True
        '
        'RemoveItem
        '
        Me.RemoveItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RemoveItem.Enabled = False
        Me.RemoveItem.Location = New System.Drawing.Point(258, 324)
        Me.RemoveItem.Name = "RemoveItem"
        Me.RemoveItem.Size = New System.Drawing.Size(66, 23)
        Me.RemoveItem.TabIndex = 23
        Me.RemoveItem.Text = "Remove"
        Me.Tip.SetToolTip(Me.RemoveItem, "Remove selected page")
        Me.RemoveItem.UseVisualStyleBackColor = True
        '
        'AddItem
        '
        Me.AddItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AddItem.Enabled = False
        Me.AddItem.Location = New System.Drawing.Point(186, 324)
        Me.AddItem.Name = "AddItem"
        Me.AddItem.Size = New System.Drawing.Size(66, 23)
        Me.AddItem.TabIndex = 22
        Me.AddItem.Text = "Add"
        Me.Tip.SetToolTip(Me.AddItem, "Add a page")
        Me.AddItem.UseVisualStyleBackColor = True
        '
        'Clear
        '
        Me.Clear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Clear.Enabled = False
        Me.Clear.Location = New System.Drawing.Point(402, 324)
        Me.Clear.Name = "Clear"
        Me.Clear.Size = New System.Drawing.Size(66, 23)
        Me.Clear.TabIndex = 25
        Me.Clear.Text = "Clear"
        Me.Tip.SetToolTip(Me.Clear, "Clear queue")
        Me.Clear.UseVisualStyleBackColor = True
        '
        'Count
        '
        Me.Count.AutoSize = True
        Me.Count.Location = New System.Drawing.Point(183, 95)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(40, 13)
        Me.Count.TabIndex = 19
        Me.Count.Text = "0 items"
        '
        'Rename
        '
        Me.Rename.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Rename.Enabled = False
        Me.Rename.Location = New System.Drawing.Point(93, 353)
        Me.Rename.Name = "Rename"
        Me.Rename.Size = New System.Drawing.Size(75, 23)
        Me.Rename.TabIndex = 5
        Me.Rename.Text = "Rename"
        Me.Tip.SetToolTip(Me.Rename, "Rename selected queue")
        Me.Rename.UseVisualStyleBackColor = True
        '
        'Save
        '
        Me.Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Save.Enabled = False
        Me.Save.Location = New System.Drawing.Point(274, 353)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(75, 23)
        Me.Save.TabIndex = 27
        Me.Save.Text = "Save..."
        Me.Tip.SetToolTip(Me.Save, "Save queue to file")
        Me.Save.UseVisualStyleBackColor = True
        '
        'Copy
        '
        Me.Copy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Copy.Enabled = False
        Me.Copy.Location = New System.Drawing.Point(12, 353)
        Me.Copy.Name = "Copy"
        Me.Copy.Size = New System.Drawing.Size(75, 23)
        Me.Copy.TabIndex = 4
        Me.Copy.Text = "Copy"
        Me.Tip.SetToolTip(Me.Copy, "Copy selected queue")
        Me.Copy.UseVisualStyleBackColor = True
        '
        'Limit
        '
        Me.Limit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Limit.Enabled = False
        Me.Limit.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.Limit.Location = New System.Drawing.Point(416, 12)
        Me.Limit.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Limit.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Limit.Name = "Limit"
        Me.Limit.Size = New System.Drawing.Size(52, 20)
        Me.Limit.TabIndex = 9
        Me.Limit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Tip.SetToolTip(Me.Limit, "Maximum number of pages that can be returned by the query")
        Me.Limit.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'LimitLabel
        '
        Me.LimitLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LimitLabel.AutoSize = True
        Me.LimitLabel.Location = New System.Drawing.Point(379, 15)
        Me.LimitLabel.Name = "LimitLabel"
        Me.LimitLabel.Size = New System.Drawing.Size(36, 13)
        Me.LimitLabel.TabIndex = 8
        Me.LimitLabel.Text = "Up to:"
        '
        'Cancel
        '
        Me.Cancel.Location = New System.Drawing.Point(393, 124)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(70, 23)
        Me.Cancel.TabIndex = 18
        Me.Cancel.Text = "Cancel"
        Me.Tip.SetToolTip(Me.Cancel, "Stop this query")
        Me.Cancel.UseVisualStyleBackColor = True
        Me.Cancel.Visible = False
        '
        'Browse
        '
        Me.Browse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Browse.Enabled = False
        Me.Browse.Location = New System.Drawing.Point(398, 37)
        Me.Browse.Name = "Browse"
        Me.Browse.Size = New System.Drawing.Size(70, 23)
        Me.Browse.TabIndex = 12
        Me.Browse.Text = "Browse..."
        Me.Browse.UseVisualStyleBackColor = True
        Me.Browse.Visible = False
        '
        'Progress
        '
        Me.Progress.AutoSize = True
        Me.Progress.Location = New System.Drawing.Point(242, 95)
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(10, 13)
        Me.Progress.TabIndex = 20
        Me.Progress.Text = " "
        '
        'ArticlesOnly
        '
        Me.ArticlesOnly.AutoSize = True
        Me.ArticlesOnly.Enabled = False
        Me.ArticlesOnly.Location = New System.Drawing.Point(186, 357)
        Me.ArticlesOnly.Name = "ArticlesOnly"
        Me.ArticlesOnly.Size = New System.Drawing.Size(82, 17)
        Me.ArticlesOnly.TabIndex = 26
        Me.ArticlesOnly.Text = "Articles only"
        Me.Tip.SetToolTip(Me.ArticlesOnly, "Ignore pages that are not articles")
        Me.ArticlesOnly.UseVisualStyleBackColor = True
        '
        'Throbber
        '
        Me.Throbber.BackColor = System.Drawing.Color.White
        Me.Throbber.Location = New System.Drawing.Point(186, 70)
        Me.Throbber.Name = "Throbber"
        Me.Throbber.Size = New System.Drawing.Size(54, 10)
        Me.Throbber.TabIndex = 14
        '
        'QueueSelector
        '
        Me.QueueSelector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.QueueSelector.FormattingEnabled = True
        Me.QueueSelector.Location = New System.Drawing.Point(247, 38)
        Me.QueueSelector.Name = "QueueSelector"
        Me.QueueSelector.Size = New System.Drawing.Size(222, 21)
        Me.QueueSelector.Sorted = True
        Me.QueueSelector.TabIndex = 13
        '
        'QueueForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 388)
        Me.Controls.Add(Me.QueueSelector)
        Me.Controls.Add(Me.ArticlesOnly)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.LimitLabel)
        Me.Controls.Add(Me.Limit)
        Me.Controls.Add(Me.Throbber)
        Me.Controls.Add(Me.Copy)
        Me.Controls.Add(Me.Rename)
        Me.Controls.Add(Me.Count)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.AddItem)
        Me.Controls.Add(Me.RemoveItem)
        Me.Controls.Add(Me.Clear)
        Me.Controls.Add(Me.Sort)
        Me.Controls.Add(Me.Browse)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.RemoveQueue)
        Me.Controls.Add(Me.AddQueue)
        Me.Controls.Add(Me.QueuesLabel)
        Me.Controls.Add(Me.SourceLabel)
        Me.Controls.Add(Me.Exclude)
        Me.Controls.Add(Me.Combine)
        Me.Controls.Add(Me.Intersect)
        Me.Controls.Add(Me.Source)
        Me.Controls.Add(Me.SourceType)
        Me.Controls.Add(Me.SourceTypeLabel)
        Me.Controls.Add(Me.Queue)
        Me.Controls.Add(Me.Queues)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "QueueForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Queues"
        CType(Me.Limit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Queues As System.Windows.Forms.ListBox
    Friend WithEvents Queue As System.Windows.Forms.ListBox
    Friend WithEvents Source As System.Windows.Forms.TextBox
    Friend WithEvents SourceType As System.Windows.Forms.ComboBox
    Friend WithEvents SourceTypeLabel As System.Windows.Forms.Label
    Friend WithEvents Intersect As System.Windows.Forms.Button
    Friend WithEvents Combine As System.Windows.Forms.Button
    Friend WithEvents Exclude As System.Windows.Forms.Button
    Friend WithEvents SourceLabel As System.Windows.Forms.Label
    Friend WithEvents QueuesLabel As System.Windows.Forms.Label
    Friend WithEvents AddQueue As System.Windows.Forms.Button
    Friend WithEvents RemoveQueue As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Sort As System.Windows.Forms.Button
    Friend WithEvents RemoveItem As System.Windows.Forms.Button
    Friend WithEvents AddItem As System.Windows.Forms.Button
    Friend WithEvents Clear As System.Windows.Forms.Button
    Friend WithEvents Count As System.Windows.Forms.Label
    Friend WithEvents Rename As System.Windows.Forms.Button
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents Copy As System.Windows.Forms.Button
    Friend WithEvents Throbber As huggle.Throbber
    Friend WithEvents Limit As System.Windows.Forms.NumericUpDown
    Friend WithEvents LimitLabel As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Browse As System.Windows.Forms.Button
    Friend WithEvents Progress As System.Windows.Forms.Label
    Friend WithEvents ArticlesOnly As System.Windows.Forms.CheckBox
    Friend WithEvents QueueSelector As System.Windows.Forms.ComboBox
    Friend WithEvents Tip As System.Windows.Forms.ToolTip
End Class
