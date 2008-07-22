<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditForm
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
        Me.Summary = New System.Windows.Forms.TextBox
        Me.SummaryLabel = New System.Windows.Forms.Label
        Me.Minor = New System.Windows.Forms.CheckBox
        Me.Watch = New System.Windows.Forms.CheckBox
        Me.Save = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.EditTab = New System.Windows.Forms.TabPage
        Me.WaitMessage = New System.Windows.Forms.Label
        Me.PageText = New System.Windows.Forms.RichTextBox
        Me.PreviewTab = New System.Windows.Forms.TabPage
        Me.Preview = New System.Windows.Forms.WebBrowser
        Me.KeystrokeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuBar = New System.Windows.Forms.MenuStrip
        Me.PageMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.PageSaveToFile = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.PageSave = New System.Windows.Forms.ToolStripMenuItem
        Me.PageCancel = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditUndo = New System.Windows.Forms.ToolStripMenuItem
        Me.EditRedo = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.EditCut = New System.Windows.Forms.ToolStripMenuItem
        Me.EditCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.EditPaste = New System.Windows.Forms.ToolStripMenuItem
        Me.EditDelete = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.EditSelectAll = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewSyntaxColoring = New System.Windows.Forms.ToolStripMenuItem
        Me.Tabs.SuspendLayout()
        Me.EditTab.SuspendLayout()
        Me.PreviewTab.SuspendLayout()
        Me.MenuBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'Summary
        '
        Me.Summary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Summary.Enabled = False
        Me.Summary.Location = New System.Drawing.Point(57, 467)
        Me.Summary.Name = "Summary"
        Me.Summary.Size = New System.Drawing.Size(628, 20)
        Me.Summary.TabIndex = 2
        '
        'SummaryLabel
        '
        Me.SummaryLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SummaryLabel.AutoSize = True
        Me.SummaryLabel.Location = New System.Drawing.Point(4, 470)
        Me.SummaryLabel.Name = "SummaryLabel"
        Me.SummaryLabel.Size = New System.Drawing.Size(53, 13)
        Me.SummaryLabel.TabIndex = 1
        Me.SummaryLabel.Text = "Summary:"
        '
        'Minor
        '
        Me.Minor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Minor.AutoSize = True
        Me.Minor.Enabled = False
        Me.Minor.Location = New System.Drawing.Point(57, 494)
        Me.Minor.Name = "Minor"
        Me.Minor.Size = New System.Drawing.Size(72, 17)
        Me.Minor.TabIndex = 3
        Me.Minor.Text = "Minor edit"
        Me.Minor.UseVisualStyleBackColor = True
        '
        'Watch
        '
        Me.Watch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Watch.AutoSize = True
        Me.Watch.Enabled = False
        Me.Watch.Location = New System.Drawing.Point(135, 494)
        Me.Watch.Name = "Watch"
        Me.Watch.Size = New System.Drawing.Size(104, 17)
        Me.Watch.TabIndex = 4
        Me.Watch.Text = "Watch this page"
        Me.Watch.UseVisualStyleBackColor = True
        '
        'Save
        '
        Me.Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Save.Enabled = False
        Me.Save.Location = New System.Drawing.Point(529, 493)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(75, 23)
        Me.Save.TabIndex = 5
        Me.Save.Text = "Save"
        Me.Save.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(610, 493)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 6
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Tabs
        '
        Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Controls.Add(Me.EditTab)
        Me.Tabs.Controls.Add(Me.PreviewTab)
        Me.Tabs.ItemSize = New System.Drawing.Size(80, 20)
        Me.Tabs.Location = New System.Drawing.Point(3, 27)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(686, 429)
        Me.Tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.Tabs.TabIndex = 0
        '
        'EditTab
        '
        Me.EditTab.Controls.Add(Me.WaitMessage)
        Me.EditTab.Controls.Add(Me.PageText)
        Me.EditTab.Location = New System.Drawing.Point(4, 24)
        Me.EditTab.Name = "EditTab"
        Me.EditTab.Padding = New System.Windows.Forms.Padding(3)
        Me.EditTab.Size = New System.Drawing.Size(678, 401)
        Me.EditTab.TabIndex = 0
        Me.EditTab.Text = "Edit"
        Me.EditTab.UseVisualStyleBackColor = True
        '
        'WaitMessage
        '
        Me.WaitMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WaitMessage.BackColor = System.Drawing.SystemColors.Control
        Me.WaitMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WaitMessage.Location = New System.Drawing.Point(6, 8)
        Me.WaitMessage.Name = "WaitMessage"
        Me.WaitMessage.Size = New System.Drawing.Size(666, 387)
        Me.WaitMessage.TabIndex = 1
        Me.WaitMessage.Text = " "
        Me.WaitMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PageText
        '
        Me.PageText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PageText.DetectUrls = False
        Me.PageText.Location = New System.Drawing.Point(6, 8)
        Me.PageText.Name = "PageText"
        Me.PageText.Size = New System.Drawing.Size(666, 387)
        Me.PageText.TabIndex = 0
        Me.PageText.Text = ""
        '
        'PreviewTab
        '
        Me.PreviewTab.Controls.Add(Me.Preview)
        Me.PreviewTab.Location = New System.Drawing.Point(4, 24)
        Me.PreviewTab.Name = "PreviewTab"
        Me.PreviewTab.Padding = New System.Windows.Forms.Padding(3)
        Me.PreviewTab.Size = New System.Drawing.Size(678, 401)
        Me.PreviewTab.TabIndex = 1
        Me.PreviewTab.Text = "Preview"
        Me.PreviewTab.UseVisualStyleBackColor = True
        '
        'Preview
        '
        Me.Preview.AllowWebBrowserDrop = False
        Me.Preview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Preview.IsWebBrowserContextMenuEnabled = False
        Me.Preview.Location = New System.Drawing.Point(3, 3)
        Me.Preview.MinimumSize = New System.Drawing.Size(20, 20)
        Me.Preview.Name = "Preview"
        Me.Preview.ScriptErrorsSuppressed = True
        Me.Preview.Size = New System.Drawing.Size(672, 395)
        Me.Preview.TabIndex = 0
        Me.Preview.WebBrowserShortcutsEnabled = False
        '
        'KeystrokeTimer
        '
        Me.KeystrokeTimer.Enabled = True
        Me.KeystrokeTimer.Interval = 1000
        '
        'MenuBar
        '
        Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageMenu, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem})
        Me.MenuBar.Location = New System.Drawing.Point(0, 0)
        Me.MenuBar.Name = "MenuBar"
        Me.MenuBar.Size = New System.Drawing.Size(692, 24)
        Me.MenuBar.TabIndex = 8
        Me.MenuBar.Text = "MenuStrip1"
        '
        'PageMenu
        '
        Me.PageMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PageSaveToFile, Me.ToolStripSeparator2, Me.PageSave, Me.PageCancel})
        Me.PageMenu.Name = "PageMenu"
        Me.PageMenu.Size = New System.Drawing.Size(43, 20)
        Me.PageMenu.Text = "Page"
        '
        'PageSaveToFile
        '
        Me.PageSaveToFile.Name = "PageSaveToFile"
        Me.PageSaveToFile.Size = New System.Drawing.Size(140, 22)
        Me.PageSaveToFile.Text = "Save to file..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(137, 6)
        '
        'PageSave
        '
        Me.PageSave.Name = "PageSave"
        Me.PageSave.Size = New System.Drawing.Size(140, 22)
        Me.PageSave.Text = "Save"
        '
        'PageCancel
        '
        Me.PageCancel.Name = "PageCancel"
        Me.PageCancel.Size = New System.Drawing.Size(140, 22)
        Me.PageCancel.Text = "Cancel"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditUndo, Me.EditRedo, Me.ToolStripSeparator3, Me.EditCut, Me.EditCopy, Me.EditPaste, Me.EditDelete, Me.ToolStripSeparator1, Me.EditSelectAll})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'EditUndo
        '
        Me.EditUndo.Enabled = False
        Me.EditUndo.Name = "EditUndo"
        Me.EditUndo.Size = New System.Drawing.Size(117, 22)
        Me.EditUndo.Text = "Undo"
        '
        'EditRedo
        '
        Me.EditRedo.Enabled = False
        Me.EditRedo.Name = "EditRedo"
        Me.EditRedo.Size = New System.Drawing.Size(117, 22)
        Me.EditRedo.Text = "Redo"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(114, 6)
        '
        'EditCut
        '
        Me.EditCut.Enabled = False
        Me.EditCut.Name = "EditCut"
        Me.EditCut.Size = New System.Drawing.Size(117, 22)
        Me.EditCut.Text = "Cut"
        '
        'EditCopy
        '
        Me.EditCopy.Enabled = False
        Me.EditCopy.Name = "EditCopy"
        Me.EditCopy.Size = New System.Drawing.Size(117, 22)
        Me.EditCopy.Text = "Copy"
        '
        'EditPaste
        '
        Me.EditPaste.Enabled = False
        Me.EditPaste.Name = "EditPaste"
        Me.EditPaste.Size = New System.Drawing.Size(117, 22)
        Me.EditPaste.Text = "Paste"
        '
        'EditDelete
        '
        Me.EditDelete.Enabled = False
        Me.EditDelete.Name = "EditDelete"
        Me.EditDelete.Size = New System.Drawing.Size(117, 22)
        Me.EditDelete.Text = "Delete"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(114, 6)
        '
        'EditSelectAll
        '
        Me.EditSelectAll.Name = "EditSelectAll"
        Me.EditSelectAll.Size = New System.Drawing.Size(117, 22)
        Me.EditSelectAll.Text = "Select All"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewSyntaxColoring})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'ViewSyntaxColoring
        '
        Me.ViewSyntaxColoring.Checked = True
        Me.ViewSyntaxColoring.CheckOnClick = True
        Me.ViewSyntaxColoring.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ViewSyntaxColoring.Name = "ViewSyntaxColoring"
        Me.ViewSyntaxColoring.Size = New System.Drawing.Size(148, 22)
        Me.ViewSyntaxColoring.Text = "Syntax coloring"
        '
        'EditForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 523)
        Me.Controls.Add(Me.MenuBar)
        Me.Controls.Add(Me.Tabs)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.Watch)
        Me.Controls.Add(Me.Minor)
        Me.Controls.Add(Me.SummaryLabel)
        Me.Controls.Add(Me.Summary)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuBar
        Me.MinimumSize = New System.Drawing.Size(430, 300)
        Me.Name = "EditForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit"
        Me.Tabs.ResumeLayout(False)
        Me.EditTab.ResumeLayout(False)
        Me.PreviewTab.ResumeLayout(False)
        Me.MenuBar.ResumeLayout(False)
        Me.MenuBar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Summary As System.Windows.Forms.TextBox
    Friend WithEvents SummaryLabel As System.Windows.Forms.Label
    Friend WithEvents Minor As System.Windows.Forms.CheckBox
    Friend WithEvents Watch As System.Windows.Forms.CheckBox
    Friend WithEvents Save As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents EditTab As System.Windows.Forms.TabPage
    Friend WithEvents WaitMessage As System.Windows.Forms.Label
    Friend WithEvents PreviewTab As System.Windows.Forms.TabPage
    Friend WithEvents Preview As System.Windows.Forms.WebBrowser
    Friend WithEvents KeystrokeTimer As System.Windows.Forms.Timer
    Friend WithEvents PageText As System.Windows.Forms.RichTextBox
    Friend WithEvents MenuBar As System.Windows.Forms.MenuStrip
    Friend WithEvents PageMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageSaveToFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PageSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PageCancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditPaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewSyntaxColoring As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditRedo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditSelectAll As System.Windows.Forms.ToolStripMenuItem
End Class
