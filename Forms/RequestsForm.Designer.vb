<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RequestsForm
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
        Me.components = New System.ComponentModel.Container()
        Me.CancelledLabel = New System.Windows.Forms.Label()
        Me.CancelledColour = New System.Windows.Forms.Label()
        Me.FailedLabel = New System.Windows.Forms.Label()
        Me.FailedColour = New System.Windows.Forms.Label()
        Me.InProgressLabel = New System.Windows.Forms.Label()
        Me.InProgressColour = New System.Windows.Forms.Label()
        Me.CompletedLabel = New System.Windows.Forms.Label()
        Me.CompletedColour = New System.Windows.Forms.Label()
        Me.CancelAll = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.Clear = New System.Windows.Forms.Button()
        Me.ListMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyListItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.List = New Huggle.ListView2()
        Me.DateColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TypeColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ActionColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.QueryColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ListMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'CancelledLabel
        '
        Me.CancelledLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CancelledLabel.AutoSize = True
        Me.CancelledLabel.Location = New System.Drawing.Point(32, 340)
        Me.CancelledLabel.Name = "CancelledLabel"
        Me.CancelledLabel.Size = New System.Drawing.Size(54, 13)
        Me.CancelledLabel.TabIndex = 2
        Me.CancelledLabel.Text = "Cancelled"
        '
        'CancelledColour
        '
        Me.CancelledColour.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CancelledColour.BackColor = System.Drawing.Color.DarkGray
        Me.CancelledColour.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CancelledColour.Location = New System.Drawing.Point(12, 340)
        Me.CancelledColour.Name = "CancelledColour"
        Me.CancelledColour.Size = New System.Drawing.Size(14, 14)
        Me.CancelledColour.TabIndex = 1
        Me.CancelledColour.Text = "   "
        '
        'FailedLabel
        '
        Me.FailedLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FailedLabel.AutoSize = True
        Me.FailedLabel.Location = New System.Drawing.Point(128, 340)
        Me.FailedLabel.Name = "FailedLabel"
        Me.FailedLabel.Size = New System.Drawing.Size(35, 13)
        Me.FailedLabel.TabIndex = 4
        Me.FailedLabel.Text = "Failed"
        '
        'FailedColour
        '
        Me.FailedColour.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FailedColour.BackColor = System.Drawing.Color.LightCoral
        Me.FailedColour.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.FailedColour.Location = New System.Drawing.Point(108, 340)
        Me.FailedColour.Name = "FailedColour"
        Me.FailedColour.Size = New System.Drawing.Size(14, 14)
        Me.FailedColour.TabIndex = 3
        Me.FailedColour.Text = "   "
        '
        'InProgressLabel
        '
        Me.InProgressLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.InProgressLabel.AutoSize = True
        Me.InProgressLabel.Location = New System.Drawing.Point(204, 340)
        Me.InProgressLabel.Name = "InProgressLabel"
        Me.InProgressLabel.Size = New System.Drawing.Size(59, 13)
        Me.InProgressLabel.TabIndex = 6
        Me.InProgressLabel.Text = "In progress"
        '
        'InProgressColour
        '
        Me.InProgressColour.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.InProgressColour.BackColor = System.Drawing.Color.LightSteelBlue
        Me.InProgressColour.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.InProgressColour.Location = New System.Drawing.Point(184, 340)
        Me.InProgressColour.Name = "InProgressColour"
        Me.InProgressColour.Size = New System.Drawing.Size(14, 14)
        Me.InProgressColour.TabIndex = 5
        Me.InProgressColour.Text = "   "
        '
        'CompletedLabel
        '
        Me.CompletedLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CompletedLabel.AutoSize = True
        Me.CompletedLabel.Location = New System.Drawing.Point(301, 341)
        Me.CompletedLabel.Name = "CompletedLabel"
        Me.CompletedLabel.Size = New System.Drawing.Size(57, 13)
        Me.CompletedLabel.TabIndex = 8
        Me.CompletedLabel.Text = "Completed"
        '
        'CompletedColour
        '
        Me.CompletedColour.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CompletedColour.BackColor = System.Drawing.Color.White
        Me.CompletedColour.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CompletedColour.Location = New System.Drawing.Point(281, 340)
        Me.CompletedColour.Name = "CompletedColour"
        Me.CompletedColour.Size = New System.Drawing.Size(14, 14)
        Me.CompletedColour.TabIndex = 7
        Me.CompletedColour.Text = "   "
        '
        'CancelAll
        '
        Me.CancelAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelAll.Location = New System.Drawing.Point(373, 336)
        Me.CancelAll.Name = "CancelAll"
        Me.CancelAll.Size = New System.Drawing.Size(75, 23)
        Me.CancelAll.TabIndex = 9
        Me.CancelAll.Text = "Cancel all"
        Me.CancelAll.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(535, 336)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 11
        Me.OK.Text = "Close"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Clear
        '
        Me.Clear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Clear.Location = New System.Drawing.Point(454, 336)
        Me.Clear.Name = "Clear"
        Me.Clear.Size = New System.Drawing.Size(75, 23)
        Me.Clear.TabIndex = 10
        Me.Clear.Text = "Clear"
        Me.Clear.UseVisualStyleBackColor = True
        '
        'ListMenu
        '
        Me.ListMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyListItem, Me.CancelItem})
        Me.ListMenu.Name = "ListMenu"
        Me.ListMenu.Size = New System.Drawing.Size(133, 48)
        '
        'CopyListItem
        '
        Me.CopyListItem.Name = "CopyListItem"
        Me.CopyListItem.Size = New System.Drawing.Size(132, 22)
        Me.CopyListItem.Text = "Copy Query"
        '
        'CancelItem
        '
        Me.CancelItem.Name = "CancelItem"
        Me.CancelItem.Size = New System.Drawing.Size(132, 22)
        Me.CancelItem.Text = "Cancel"
        '
        'List
        '
        Me.List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.List.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.DateColumn, Me.TypeColumn, Me.ActionColumn, Me.QueryColumn})
        Me.List.ContextMenuStrip = Me.ListMenu
        Me.List.FullRowSelect = True
        Me.List.GridLines = True
        Me.List.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.List.Location = New System.Drawing.Point(12, 12)
        Me.List.MultiSelect = False
        Me.List.Name = "List"
        Me.List.Size = New System.Drawing.Size(598, 317)
        Me.List.TabIndex = 0
        Me.List.UseCompatibleStateImageBehavior = False
        Me.List.View = System.Windows.Forms.View.Details
        '
        'DateColumn
        '
        Me.DateColumn.Text = "Time"
        Me.DateColumn.Width = 79
        '
        'TypeColumn
        '
        Me.TypeColumn.Text = "Type"
        Me.TypeColumn.Width = 110
        '
        'ActionColumn
        '
        Me.ActionColumn.Text = ""
        Me.ActionColumn.Width = 40
        '
        'QueryColumn
        '
        Me.QueryColumn.Text = "Query"
        Me.QueryColumn.Width = 342
        '
        'RequestsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(622, 366)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Clear)
        Me.Controls.Add(Me.CancelAll)
        Me.Controls.Add(Me.CompletedColour)
        Me.Controls.Add(Me.CompletedLabel)
        Me.Controls.Add(Me.InProgressColour)
        Me.Controls.Add(Me.InProgressLabel)
        Me.Controls.Add(Me.FailedColour)
        Me.Controls.Add(Me.FailedLabel)
        Me.Controls.Add(Me.CancelledColour)
        Me.Controls.Add(Me.CancelledLabel)
        Me.Controls.Add(Me.List)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(623, 38)
        Me.Name = "RequestsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Requests"
        Me.ListMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents List As Huggle.ListView2
    Friend WithEvents DateColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents TypeColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents ActionColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents QueryColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents CancelledLabel As System.Windows.Forms.Label
    Friend WithEvents CancelledColour As System.Windows.Forms.Label
    Friend WithEvents FailedLabel As System.Windows.Forms.Label
    Friend WithEvents FailedColour As System.Windows.Forms.Label
    Friend WithEvents InProgressLabel As System.Windows.Forms.Label
    Friend WithEvents InProgressColour As System.Windows.Forms.Label
    Friend WithEvents CompletedLabel As System.Windows.Forms.Label
    Friend WithEvents CompletedColour As System.Windows.Forms.Label
    Friend WithEvents CancelAll As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Clear As System.Windows.Forms.Button
    Friend WithEvents ListMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyListItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CancelItem As System.Windows.Forms.ToolStripMenuItem
End Class
