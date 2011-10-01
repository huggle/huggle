<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Report3rrForm
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
        Me.MessageLabel = New System.Windows.Forms.Label
        Me.Message = New System.Windows.Forms.TextBox
        Me.Search = New System.Windows.Forms.Button
        Me.WarnLogLabel = New System.Windows.Forms.Label
        Me.SearchLabel = New System.Windows.Forms.Label
        Me.BaseLabel = New System.Windows.Forms.Label
        Me.RevertsListLabel = New System.Windows.Forms.Label
        Me.RevertsList = New System.Windows.Forms.ListBox
        Me.Add = New System.Windows.Forms.Button
        Me.AddBase = New System.Windows.Forms.Button
        Me.Base = New System.Windows.Forms.TextBox
        Me.ReportWarning1 = New System.Windows.Forms.Label
        Me.Status = New System.Windows.Forms.Label
        Me.ReportWarning2 = New System.Windows.Forms.Label
        Me.ReportWarning3 = New System.Windows.Forms.PictureBox
        Me.Throbber = New Huggle.Throbber
        Me.WarnLog = New Huggle.WarnLog
        CType(Me.ReportWarning3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(407, 358)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 17
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(326, 358)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 16
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'MessageLabel
        '
        Me.MessageLabel.AutoSize = True
        Me.MessageLabel.Location = New System.Drawing.Point(6, 214)
        Me.MessageLabel.Name = "MessageLabel"
        Me.MessageLabel.Size = New System.Drawing.Size(105, 13)
        Me.MessageLabel.TabIndex = 12
        Me.MessageLabel.Text = "Comments (optional):"
        '
        'Message
        '
        Me.Message.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Message.Location = New System.Drawing.Point(117, 211)
        Me.Message.Multiline = True
        Me.Message.Name = "Message"
        Me.Message.Size = New System.Drawing.Size(365, 40)
        Me.Message.TabIndex = 13
        '
        'Search
        '
        Me.Search.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Search.Location = New System.Drawing.Point(407, 9)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(75, 23)
        Me.Search.TabIndex = 1
        Me.Search.Text = "Search"
        Me.Search.UseVisualStyleBackColor = True
        '
        'WarnLogLabel
        '
        Me.WarnLogLabel.AutoSize = True
        Me.WarnLogLabel.Location = New System.Drawing.Point(12, 263)
        Me.WarnLogLabel.Name = "WarnLogLabel"
        Me.WarnLogLabel.Size = New System.Drawing.Size(112, 13)
        Me.WarnLogLabel.TabIndex = 14
        Me.WarnLogLabel.Text = "Warnings for this user:"
        '
        'SearchLabel
        '
        Me.SearchLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchLabel.Location = New System.Drawing.Point(9, 12)
        Me.SearchLabel.Name = "SearchLabel"
        Me.SearchLabel.Size = New System.Drawing.Size(397, 20)
        Me.SearchLabel.TabIndex = 0
        Me.SearchLabel.Text = "Click Search to find possible 3RR violations by this user, or enter a report manu" & _
            "ally:"
        '
        'BaseLabel
        '
        Me.BaseLabel.AutoSize = True
        Me.BaseLabel.Location = New System.Drawing.Point(12, 61)
        Me.BaseLabel.Name = "BaseLabel"
        Me.BaseLabel.Size = New System.Drawing.Size(99, 13)
        Me.BaseLabel.TabIndex = 4
        Me.BaseLabel.Text = "Version reverted to:"
        '
        'RevertsListLabel
        '
        Me.RevertsListLabel.AutoSize = True
        Me.RevertsListLabel.Location = New System.Drawing.Point(64, 85)
        Me.RevertsListLabel.Name = "RevertsListLabel"
        Me.RevertsListLabel.Size = New System.Drawing.Size(47, 13)
        Me.RevertsListLabel.TabIndex = 7
        Me.RevertsListLabel.Text = "Reverts:"
        '
        'RevertsList
        '
        Me.RevertsList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RevertsList.FormattingEnabled = True
        Me.RevertsList.Location = New System.Drawing.Point(117, 85)
        Me.RevertsList.Name = "RevertsList"
        Me.RevertsList.Size = New System.Drawing.Size(284, 56)
        Me.RevertsList.TabIndex = 8
        '
        'Add
        '
        Me.Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Add.Location = New System.Drawing.Point(407, 118)
        Me.Add.Name = "Add"
        Me.Add.Size = New System.Drawing.Size(75, 23)
        Me.Add.TabIndex = 9
        Me.Add.Text = "Add Current"
        Me.Add.UseVisualStyleBackColor = True
        '
        'AddBase
        '
        Me.AddBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddBase.Location = New System.Drawing.Point(407, 56)
        Me.AddBase.Name = "AddBase"
        Me.AddBase.Size = New System.Drawing.Size(75, 23)
        Me.AddBase.TabIndex = 6
        Me.AddBase.Text = "Use Current"
        Me.AddBase.UseVisualStyleBackColor = True
        '
        'Base
        '
        Me.Base.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Base.Enabled = False
        Me.Base.Location = New System.Drawing.Point(117, 58)
        Me.Base.Name = "Base"
        Me.Base.Size = New System.Drawing.Size(284, 20)
        Me.Base.TabIndex = 5
        '
        'ReportWarning1
        '
        Me.ReportWarning1.AutoSize = True
        Me.ReportWarning1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportWarning1.Location = New System.Drawing.Point(114, 149)
        Me.ReportWarning1.Name = "ReportWarning1"
        Me.ReportWarning1.Size = New System.Drawing.Size(80, 13)
        Me.ReportWarning1.TabIndex = 10
        Me.ReportWarning1.Text = "IMPORTANT" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ReportWarning1.Visible = False
        '
        'Status
        '
        Me.Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Status.Location = New System.Drawing.Point(77, 35)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(405, 14)
        Me.Status.TabIndex = 3
        '
        'ReportWarning2
        '
        Me.ReportWarning2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReportWarning2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportWarning2.Location = New System.Drawing.Point(114, 163)
        Me.ReportWarning2.Name = "ReportWarning2"
        Me.ReportWarning2.Size = New System.Drawing.Size(368, 45)
        Me.ReportWarning2.TabIndex = 11
        Me.ReportWarning2.Text = "This is only a possible 3RR violation. Do not submit this report until you have r" & _
            "eviewed the above revisions to ensure that they are not reversions of vandalism." & _
            ""
        Me.ReportWarning2.Visible = False
        '
        'ReportWarning3
        '
        Me.ReportWarning3.ErrorImage = Nothing
        Me.ReportWarning3.Image = Global.Huggle.My.Resources.Resources.user_warn
        Me.ReportWarning3.InitialImage = Nothing
        Me.ReportWarning3.Location = New System.Drawing.Point(72, 154)
        Me.ReportWarning3.Name = "ReportWarning3"
        Me.ReportWarning3.Size = New System.Drawing.Size(36, 36)
        Me.ReportWarning3.TabIndex = 19
        Me.ReportWarning3.TabStop = False
        Me.ReportWarning3.Visible = False
        '
        'Throbber
        '
        Me.Throbber.BackColor = System.Drawing.Color.Gainsboro
        Me.Throbber.Location = New System.Drawing.Point(12, 37)
        Me.Throbber.Name = "Throbber"
        Me.Throbber.Size = New System.Drawing.Size(53, 10)
        Me.Throbber.TabIndex = 2
        '
        'WarnLog
        '
        Me.WarnLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarnLog.FullRowSelect = True
        Me.WarnLog.GridLines = True
        Me.WarnLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.WarnLog.Location = New System.Drawing.Point(15, 279)
        Me.WarnLog.Name = "WarnLog"
        Me.WarnLog.Size = New System.Drawing.Size(467, 73)
        Me.WarnLog.TabIndex = 15
        Me.WarnLog.UseCompatibleStateImageBehavior = False
        Me.WarnLog.User = Nothing
        Me.WarnLog.View = System.Windows.Forms.View.Details
        '
        'Report3rrForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 393)
        Me.Controls.Add(Me.WarnLog)
        Me.Controls.Add(Me.Add)
        Me.Controls.Add(Me.ReportWarning1)
        Me.Controls.Add(Me.ReportWarning2)
        Me.Controls.Add(Me.ReportWarning3)
        Me.Controls.Add(Me.Throbber)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.AddBase)
        Me.Controls.Add(Me.RevertsList)
        Me.Controls.Add(Me.Base)
        Me.Controls.Add(Me.RevertsListLabel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.WarnLogLabel)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Message)
        Me.Controls.Add(Me.BaseLabel)
        Me.Controls.Add(Me.MessageLabel)
        Me.Controls.Add(Me.Search)
        Me.Controls.Add(Me.SearchLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Report3rrForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report three-revert rule violation"
        CType(Me.ReportWarning3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents MessageLabel As System.Windows.Forms.Label
    Friend WithEvents Message As System.Windows.Forms.TextBox
    Friend WithEvents Search As System.Windows.Forms.Button
    Friend WithEvents WarnLogLabel As System.Windows.Forms.Label
    Friend WithEvents SearchLabel As System.Windows.Forms.Label
    Friend WithEvents BaseLabel As System.Windows.Forms.Label
    Friend WithEvents RevertsListLabel As System.Windows.Forms.Label
    Friend WithEvents RevertsList As System.Windows.Forms.ListBox
    Friend WithEvents Add As System.Windows.Forms.Button
    Friend WithEvents AddBase As System.Windows.Forms.Button
    Friend WithEvents Base As System.Windows.Forms.TextBox
    Friend WithEvents ReportWarning1 As System.Windows.Forms.Label
    Friend WithEvents Status As System.Windows.Forms.Label
    Friend WithEvents ReportWarning2 As System.Windows.Forms.Label
    Friend WithEvents ReportWarning3 As System.Windows.Forms.PictureBox
    Friend WithEvents Throbber As Huggle.Throbber
    Friend WithEvents WarnLog As Huggle.WarnLog
End Class
