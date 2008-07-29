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
        Me.QueueSourcesList = New System.Windows.Forms.ListBox
        Me.QueueItems = New System.Windows.Forms.ListBox
        Me.Source = New System.Windows.Forms.TextBox
        Me.SourceType = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Intersect = New System.Windows.Forms.Button
        Me.Combine = New System.Windows.Forms.Button
        Me.Replace = New System.Windows.Forms.Button
        Me.SourceLabel = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.AddQueue = New System.Windows.Forms.Button
        Me.RemoveQueue = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Browse = New System.Windows.Forms.Button
        Me.Sort = New System.Windows.Forms.Button
        Me.RemoveItem = New System.Windows.Forms.Button
        Me.AddItem = New System.Windows.Forms.Button
        Me.Clear = New System.Windows.Forms.Button
        Me.ArticlesOnly = New System.Windows.Forms.Button
        Me.Count = New System.Windows.Forms.Label
        Me.Rename = New System.Windows.Forms.Button
        Me.Save = New System.Windows.Forms.Button
        Me.Copy = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'QueueSourcesList
        '
        Me.QueueSourcesList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.QueueSourcesList.FormattingEnabled = True
        Me.QueueSourcesList.IntegralHeight = False
        Me.QueueSourcesList.Location = New System.Drawing.Point(12, 28)
        Me.QueueSourcesList.Name = "QueueSourcesList"
        Me.QueueSourcesList.Size = New System.Drawing.Size(156, 270)
        Me.QueueSourcesList.Sorted = True
        Me.QueueSourcesList.TabIndex = 1
        '
        'QueueItems
        '
        Me.QueueItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.QueueItems.Enabled = False
        Me.QueueItems.FormattingEnabled = True
        Me.QueueItems.IntegralHeight = False
        Me.QueueItems.Location = New System.Drawing.Point(187, 94)
        Me.QueueItems.Name = "QueueItems"
        Me.QueueItems.Size = New System.Drawing.Size(298, 204)
        Me.QueueItems.TabIndex = 15
        '
        'Source
        '
        Me.Source.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Source.Enabled = False
        Me.Source.Location = New System.Drawing.Point(257, 38)
        Me.Source.Name = "Source"
        Me.Source.Size = New System.Drawing.Size(155, 20)
        Me.Source.TabIndex = 9
        '
        'SourceType
        '
        Me.SourceType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SourceType.Enabled = False
        Me.SourceType.FormattingEnabled = True
        Me.SourceType.Location = New System.Drawing.Point(257, 12)
        Me.SourceType.MaxDropDownItems = 20
        Me.SourceType.Name = "SourceType"
        Me.SourceType.Size = New System.Drawing.Size(155, 21)
        Me.SourceType.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(184, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Source type:"
        '
        'Intersect
        '
        Me.Intersect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Intersect.Enabled = False
        Me.Intersect.Location = New System.Drawing.Point(345, 65)
        Me.Intersect.Name = "Intersect"
        Me.Intersect.Size = New System.Drawing.Size(67, 23)
        Me.Intersect.TabIndex = 12
        Me.Intersect.Text = "Intersect"
        Me.Intersect.UseVisualStyleBackColor = True
        '
        'Combine
        '
        Me.Combine.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Combine.Enabled = False
        Me.Combine.Location = New System.Drawing.Point(272, 65)
        Me.Combine.Name = "Combine"
        Me.Combine.Size = New System.Drawing.Size(67, 23)
        Me.Combine.TabIndex = 11
        Me.Combine.Text = "Combine"
        Me.Combine.UseVisualStyleBackColor = True
        '
        'Replace
        '
        Me.Replace.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Replace.Enabled = False
        Me.Replace.Location = New System.Drawing.Point(418, 65)
        Me.Replace.Name = "Replace"
        Me.Replace.Size = New System.Drawing.Size(67, 23)
        Me.Replace.TabIndex = 13
        Me.Replace.Text = "Replace"
        Me.Replace.UseVisualStyleBackColor = True
        '
        'SourceLabel
        '
        Me.SourceLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SourceLabel.Location = New System.Drawing.Point(171, 41)
        Me.SourceLabel.Name = "SourceLabel"
        Me.SourceLabel.Size = New System.Drawing.Size(80, 16)
        Me.SourceLabel.TabIndex = 8
        Me.SourceLabel.Text = "Source:"
        Me.SourceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Queues:"
        '
        'AddQueue
        '
        Me.AddQueue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AddQueue.Location = New System.Drawing.Point(12, 304)
        Me.AddQueue.Name = "AddQueue"
        Me.AddQueue.Size = New System.Drawing.Size(75, 23)
        Me.AddQueue.TabIndex = 2
        Me.AddQueue.Text = "Add"
        Me.AddQueue.UseVisualStyleBackColor = True
        '
        'RemoveQueue
        '
        Me.RemoveQueue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RemoveQueue.Enabled = False
        Me.RemoveQueue.Location = New System.Drawing.Point(93, 304)
        Me.RemoveQueue.Name = "RemoveQueue"
        Me.RemoveQueue.Size = New System.Drawing.Size(75, 23)
        Me.RemoveQueue.TabIndex = 3
        Me.RemoveQueue.Text = "Remove"
        Me.RemoveQueue.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(410, 333)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 22
        Me.OK.Text = "Close"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Browse
        '
        Me.Browse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Browse.Enabled = False
        Me.Browse.Location = New System.Drawing.Point(418, 36)
        Me.Browse.Name = "Browse"
        Me.Browse.Size = New System.Drawing.Size(67, 23)
        Me.Browse.TabIndex = 10
        Me.Browse.Text = "Browse..."
        Me.Browse.UseVisualStyleBackColor = True
        Me.Browse.Visible = False
        '
        'Sort
        '
        Me.Sort.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Sort.Enabled = False
        Me.Sort.Location = New System.Drawing.Point(319, 304)
        Me.Sort.Name = "Sort"
        Me.Sort.Size = New System.Drawing.Size(60, 23)
        Me.Sort.TabIndex = 18
        Me.Sort.Text = "Sort"
        Me.Sort.UseVisualStyleBackColor = True
        '
        'RemoveItem
        '
        Me.RemoveItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RemoveItem.Enabled = False
        Me.RemoveItem.Location = New System.Drawing.Point(253, 304)
        Me.RemoveItem.Name = "RemoveItem"
        Me.RemoveItem.Size = New System.Drawing.Size(60, 23)
        Me.RemoveItem.TabIndex = 17
        Me.RemoveItem.Text = "Remove"
        Me.RemoveItem.UseVisualStyleBackColor = True
        '
        'AddItem
        '
        Me.AddItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddItem.Enabled = False
        Me.AddItem.Location = New System.Drawing.Point(187, 304)
        Me.AddItem.Name = "AddItem"
        Me.AddItem.Size = New System.Drawing.Size(60, 23)
        Me.AddItem.TabIndex = 16
        Me.AddItem.Text = "Add"
        Me.AddItem.UseVisualStyleBackColor = True
        '
        'Clear
        '
        Me.Clear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Clear.Enabled = False
        Me.Clear.Location = New System.Drawing.Point(385, 304)
        Me.Clear.Name = "Clear"
        Me.Clear.Size = New System.Drawing.Size(60, 23)
        Me.Clear.TabIndex = 19
        Me.Clear.Text = "Clear"
        Me.Clear.UseVisualStyleBackColor = True
        '
        'ArticlesOnly
        '
        Me.ArticlesOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ArticlesOnly.Enabled = False
        Me.ArticlesOnly.Location = New System.Drawing.Point(187, 333)
        Me.ArticlesOnly.Name = "ArticlesOnly"
        Me.ArticlesOnly.Size = New System.Drawing.Size(75, 23)
        Me.ArticlesOnly.TabIndex = 20
        Me.ArticlesOnly.Text = "Articles only"
        Me.ArticlesOnly.UseVisualStyleBackColor = True
        '
        'Count
        '
        Me.Count.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Count.AutoSize = True
        Me.Count.Location = New System.Drawing.Point(184, 75)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(40, 13)
        Me.Count.TabIndex = 14
        Me.Count.Text = "0 items"
        '
        'Rename
        '
        Me.Rename.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Rename.Enabled = False
        Me.Rename.Location = New System.Drawing.Point(93, 333)
        Me.Rename.Name = "Rename"
        Me.Rename.Size = New System.Drawing.Size(75, 23)
        Me.Rename.TabIndex = 5
        Me.Rename.Text = "Rename"
        Me.Rename.UseVisualStyleBackColor = True
        '
        'Save
        '
        Me.Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Save.Enabled = False
        Me.Save.Location = New System.Drawing.Point(268, 333)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(75, 23)
        Me.Save.TabIndex = 21
        Me.Save.Text = "Save..."
        Me.Save.UseVisualStyleBackColor = True
        '
        'Copy
        '
        Me.Copy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Copy.Enabled = False
        Me.Copy.Location = New System.Drawing.Point(12, 333)
        Me.Copy.Name = "Copy"
        Me.Copy.Size = New System.Drawing.Size(75, 23)
        Me.Copy.TabIndex = 4
        Me.Copy.Text = "Copy"
        Me.Copy.UseVisualStyleBackColor = True
        '
        'QueueForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 368)
        Me.Controls.Add(Me.Copy)
        Me.Controls.Add(Me.Rename)
        Me.Controls.Add(Me.Count)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.ArticlesOnly)
        Me.Controls.Add(Me.AddItem)
        Me.Controls.Add(Me.RemoveItem)
        Me.Controls.Add(Me.Clear)
        Me.Controls.Add(Me.Sort)
        Me.Controls.Add(Me.Browse)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.RemoveQueue)
        Me.Controls.Add(Me.AddQueue)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.SourceLabel)
        Me.Controls.Add(Me.Replace)
        Me.Controls.Add(Me.Combine)
        Me.Controls.Add(Me.Intersect)
        Me.Controls.Add(Me.Source)
        Me.Controls.Add(Me.SourceType)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.QueueItems)
        Me.Controls.Add(Me.QueueSourcesList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "QueueForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Queues"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents QueueSourcesList As System.Windows.Forms.ListBox
    Friend WithEvents QueueItems As System.Windows.Forms.ListBox
    Friend WithEvents Source As System.Windows.Forms.TextBox
    Friend WithEvents SourceType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Intersect As System.Windows.Forms.Button
    Friend WithEvents Combine As System.Windows.Forms.Button
    Friend WithEvents Replace As System.Windows.Forms.Button
    Friend WithEvents SourceLabel As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AddQueue As System.Windows.Forms.Button
    Friend WithEvents RemoveQueue As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Browse As System.Windows.Forms.Button
    Friend WithEvents Sort As System.Windows.Forms.Button
    Friend WithEvents RemoveItem As System.Windows.Forms.Button
    Friend WithEvents AddItem As System.Windows.Forms.Button
    Friend WithEvents Clear As System.Windows.Forms.Button
    Friend WithEvents ArticlesOnly As System.Windows.Forms.Button
    Friend WithEvents Count As System.Windows.Forms.Label
    Friend WithEvents Rename As System.Windows.Forms.Button
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents Copy As System.Windows.Forms.Button
End Class
