<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserInfoForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserInfoForm))
        Me.WarnLog = New System.Windows.Forms.ListView
        Me.BlockLog = New System.Windows.Forms.ListView
        Me.BlockLabel = New System.Windows.Forms.Label
        Me.WarnLabel = New System.Windows.Forms.Label
        Me.OK = New System.Windows.Forms.Button
        Me.SharedIPLabel = New System.Windows.Forms.Label
        Me.EditCount = New System.Windows.Forms.Label
        Me.WhitelistedLabel = New System.Windows.Forms.Label
        Me.EditCountLabel = New System.Windows.Forms.Label
        Me.Whitelisted = New System.Windows.Forms.Label
        Me.SharedIP = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Anonymous = New System.Windows.Forms.Label
        Me.SessionEditCount = New System.Windows.Forms.Label
        Me.Collapse = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'WarnLog
        '
        Me.WarnLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarnLog.FullRowSelect = True
        Me.WarnLog.GridLines = True
        Me.WarnLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.WarnLog.Location = New System.Drawing.Point(12, 196)
        Me.WarnLog.MultiSelect = False
        Me.WarnLog.Name = "WarnLog"
        Me.WarnLog.ShowGroups = False
        Me.WarnLog.Size = New System.Drawing.Size(518, 136)
        Me.WarnLog.TabIndex = 9
        Me.WarnLog.UseCompatibleStateImageBehavior = False
        Me.WarnLog.View = System.Windows.Forms.View.Details
        '
        'BlockLog
        '
        Me.BlockLog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BlockLog.FullRowSelect = True
        Me.BlockLog.GridLines = True
        Me.BlockLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.BlockLog.Location = New System.Drawing.Point(12, 74)
        Me.BlockLog.MultiSelect = False
        Me.BlockLog.Name = "BlockLog"
        Me.BlockLog.ShowGroups = False
        Me.BlockLog.Size = New System.Drawing.Size(518, 94)
        Me.BlockLog.TabIndex = 7
        Me.BlockLog.UseCompatibleStateImageBehavior = False
        Me.BlockLog.View = System.Windows.Forms.View.Details
        '
        'BlockLabel
        '
        Me.BlockLabel.AutoSize = True
        Me.BlockLabel.Location = New System.Drawing.Point(9, 58)
        Me.BlockLabel.Name = "BlockLabel"
        Me.BlockLabel.Size = New System.Drawing.Size(42, 13)
        Me.BlockLabel.TabIndex = 6
        Me.BlockLabel.Text = "Blocks:"
        '
        'WarnLabel
        '
        Me.WarnLabel.AutoSize = True
        Me.WarnLabel.Location = New System.Drawing.Point(9, 180)
        Me.WarnLabel.Name = "WarnLabel"
        Me.WarnLabel.Size = New System.Drawing.Size(55, 13)
        Me.WarnLabel.TabIndex = 8
        Me.WarnLabel.Text = "Warnings:"
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(455, 339)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 10
        Me.OK.Text = "Close"
        Me.OK.UseVisualStyleBackColor = True
        '
        'SharedIPLabel
        '
        Me.SharedIPLabel.AutoSize = True
        Me.SharedIPLabel.Location = New System.Drawing.Point(9, 31)
        Me.SharedIPLabel.Name = "SharedIPLabel"
        Me.SharedIPLabel.Size = New System.Drawing.Size(101, 13)
        Me.SharedIPLabel.TabIndex = 0
        Me.SharedIPLabel.Text = "Shared/dynamic IP:"
        '
        'EditCount
        '
        Me.EditCount.AutoSize = True
        Me.EditCount.Location = New System.Drawing.Point(452, 9)
        Me.EditCount.Name = "EditCount"
        Me.EditCount.Size = New System.Drawing.Size(16, 13)
        Me.EditCount.TabIndex = 5
        Me.EditCount.Text = "..."
        '
        'WhitelistedLabel
        '
        Me.WhitelistedLabel.AutoSize = True
        Me.WhitelistedLabel.Location = New System.Drawing.Point(175, 9)
        Me.WhitelistedLabel.Name = "WhitelistedLabel"
        Me.WhitelistedLabel.Size = New System.Drawing.Size(62, 13)
        Me.WhitelistedLabel.TabIndex = 2
        Me.WhitelistedLabel.Text = "Whitelisted:"
        '
        'EditCountLabel
        '
        Me.EditCountLabel.AutoSize = True
        Me.EditCountLabel.Location = New System.Drawing.Point(354, 9)
        Me.EditCountLabel.Name = "EditCountLabel"
        Me.EditCountLabel.Size = New System.Drawing.Size(84, 13)
        Me.EditCountLabel.TabIndex = 4
        Me.EditCountLabel.Text = "Number of edits:"
        '
        'Whitelisted
        '
        Me.Whitelisted.AutoSize = True
        Me.Whitelisted.Location = New System.Drawing.Point(285, 9)
        Me.Whitelisted.Name = "Whitelisted"
        Me.Whitelisted.Size = New System.Drawing.Size(21, 13)
        Me.Whitelisted.TabIndex = 3
        Me.Whitelisted.Text = "No"
        '
        'SharedIP
        '
        Me.SharedIP.AutoSize = True
        Me.SharedIP.Location = New System.Drawing.Point(116, 31)
        Me.SharedIP.Name = "SharedIP"
        Me.SharedIP.Size = New System.Drawing.Size(21, 13)
        Me.SharedIP.TabIndex = 1
        Me.SharedIP.Text = "No"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(354, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Edits this session:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Anonymous:"
        '
        'Anonymous
        '
        Me.Anonymous.AutoSize = True
        Me.Anonymous.Location = New System.Drawing.Point(116, 9)
        Me.Anonymous.Name = "Anonymous"
        Me.Anonymous.Size = New System.Drawing.Size(21, 13)
        Me.Anonymous.TabIndex = 13
        Me.Anonymous.Text = "No"
        '
        'SessionEditCount
        '
        Me.SessionEditCount.AutoSize = True
        Me.SessionEditCount.Location = New System.Drawing.Point(452, 31)
        Me.SessionEditCount.Name = "SessionEditCount"
        Me.SessionEditCount.Size = New System.Drawing.Size(16, 13)
        Me.SessionEditCount.TabIndex = 14
        Me.SessionEditCount.Text = "..."
        '
        'Collapse
        '
        Me.Collapse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Collapse.Image = Global.huggle.My.Resources.Resources.up_gray
        Me.Collapse.Location = New System.Drawing.Point(12, 338)
        Me.Collapse.Name = "Collapse"
        Me.Collapse.Size = New System.Drawing.Size(27, 27)
        Me.Collapse.TabIndex = 15
        Me.Collapse.UseVisualStyleBackColor = True
        '
        'UserInfoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 374)
        Me.Controls.Add(Me.Collapse)
        Me.Controls.Add(Me.SessionEditCount)
        Me.Controls.Add(Me.Anonymous)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.SharedIP)
        Me.Controls.Add(Me.Whitelisted)
        Me.Controls.Add(Me.EditCountLabel)
        Me.Controls.Add(Me.WhitelistedLabel)
        Me.Controls.Add(Me.EditCount)
        Me.Controls.Add(Me.SharedIPLabel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.WarnLabel)
        Me.Controls.Add(Me.BlockLabel)
        Me.Controls.Add(Me.WarnLog)
        Me.Controls.Add(Me.BlockLog)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(550, 120)
        Me.Name = "UserInfoForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User info"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents WarnLog As System.Windows.Forms.ListView
    Friend WithEvents BlockLog As System.Windows.Forms.ListView
    Friend WithEvents BlockLabel As System.Windows.Forms.Label
    Friend WithEvents WarnLabel As System.Windows.Forms.Label
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents SharedIPLabel As System.Windows.Forms.Label
    Friend WithEvents EditCount As System.Windows.Forms.Label
    Friend WithEvents WhitelistedLabel As System.Windows.Forms.Label
    Friend WithEvents EditCountLabel As System.Windows.Forms.Label
    Friend WithEvents Whitelisted As System.Windows.Forms.Label
    Friend WithEvents SharedIP As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Anonymous As System.Windows.Forms.Label
    Friend WithEvents SessionEditCount As System.Windows.Forms.Label
    Friend WithEvents Collapse As System.Windows.Forms.Button
End Class
