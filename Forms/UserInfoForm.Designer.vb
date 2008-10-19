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
        Me.BlockLogLabel = New System.Windows.Forms.Label
        Me.WarnLogLabel = New System.Windows.Forms.Label
        Me.OK = New System.Windows.Forms.Button
        Me.SharedIPLabel = New System.Windows.Forms.Label
        Me.Edits = New System.Windows.Forms.Label
        Me.IgnoredLabel = New System.Windows.Forms.Label
        Me.EditsLabel = New System.Windows.Forms.Label
        Me.Ignored = New System.Windows.Forms.Label
        Me.SharedIP = New System.Windows.Forms.Label
        Me.SessionEditsLabel = New System.Windows.Forms.Label
        Me.AnonymousLabel = New System.Windows.Forms.Label
        Me.Anonymous = New System.Windows.Forms.Label
        Me.SessionEdits = New System.Windows.Forms.Label
        Me.BlockLog = New Huggle.UserLog
        Me.WarnLog = New Huggle.WarnLog
        Me.SuspendLayout()
        '
        'BlockLogLabel
        '
        Me.BlockLogLabel.AutoSize = True
        Me.BlockLogLabel.Location = New System.Drawing.Point(9, 58)
        Me.BlockLogLabel.Name = "BlockLogLabel"
        Me.BlockLogLabel.Size = New System.Drawing.Size(42, 13)
        Me.BlockLogLabel.TabIndex = 6
        Me.BlockLogLabel.Text = "Blocks:"
        '
        'WarnLogLabel
        '
        Me.WarnLogLabel.AutoSize = True
        Me.WarnLogLabel.Location = New System.Drawing.Point(9, 180)
        Me.WarnLogLabel.Name = "WarnLogLabel"
        Me.WarnLogLabel.Size = New System.Drawing.Size(55, 13)
        Me.WarnLogLabel.TabIndex = 8
        Me.WarnLogLabel.Text = "Warnings:"
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
        'Edits
        '
        Me.Edits.AutoSize = True
        Me.Edits.Location = New System.Drawing.Point(452, 9)
        Me.Edits.Name = "Edits"
        Me.Edits.Size = New System.Drawing.Size(16, 13)
        Me.Edits.TabIndex = 5
        Me.Edits.Text = "..."
        '
        'IgnoredLabel
        '
        Me.IgnoredLabel.AutoSize = True
        Me.IgnoredLabel.Location = New System.Drawing.Point(175, 9)
        Me.IgnoredLabel.Name = "IgnoredLabel"
        Me.IgnoredLabel.Size = New System.Drawing.Size(62, 13)
        Me.IgnoredLabel.TabIndex = 2
        Me.IgnoredLabel.Text = "Whitelisted:"
        '
        'EditsLabel
        '
        Me.EditsLabel.AutoSize = True
        Me.EditsLabel.Location = New System.Drawing.Point(354, 9)
        Me.EditsLabel.Name = "EditsLabel"
        Me.EditsLabel.Size = New System.Drawing.Size(84, 13)
        Me.EditsLabel.TabIndex = 4
        Me.EditsLabel.Text = "Number of edits:"
        '
        'Ignored
        '
        Me.Ignored.AutoSize = True
        Me.Ignored.Location = New System.Drawing.Point(285, 9)
        Me.Ignored.Name = "Ignored"
        Me.Ignored.Size = New System.Drawing.Size(21, 13)
        Me.Ignored.TabIndex = 3
        Me.Ignored.Text = "No"
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
        'SessionEditsLabel
        '
        Me.SessionEditsLabel.AutoSize = True
        Me.SessionEditsLabel.Location = New System.Drawing.Point(354, 31)
        Me.SessionEditsLabel.Name = "SessionEditsLabel"
        Me.SessionEditsLabel.Size = New System.Drawing.Size(90, 13)
        Me.SessionEditsLabel.TabIndex = 11
        Me.SessionEditsLabel.Text = "Edits this session:"
        '
        'AnonymousLabel
        '
        Me.AnonymousLabel.AutoSize = True
        Me.AnonymousLabel.Location = New System.Drawing.Point(9, 9)
        Me.AnonymousLabel.Name = "AnonymousLabel"
        Me.AnonymousLabel.Size = New System.Drawing.Size(65, 13)
        Me.AnonymousLabel.TabIndex = 12
        Me.AnonymousLabel.Text = "Anonymous:"
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
        'SessionEdits
        '
        Me.SessionEdits.AutoSize = True
        Me.SessionEdits.Location = New System.Drawing.Point(452, 31)
        Me.SessionEdits.Name = "SessionEdits"
        Me.SessionEdits.Size = New System.Drawing.Size(16, 13)
        Me.SessionEdits.TabIndex = 14
        Me.SessionEdits.Text = "..."
        '
        'BlockLog
        '
        Me.BlockLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BlockLog.GridLines = True
        Me.BlockLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.BlockLog.Location = New System.Drawing.Point(12, 74)
        Me.BlockLog.Name = "BlockLog"
        Me.BlockLog.Size = New System.Drawing.Size(518, 103)
        Me.BlockLog.TabIndex = 15
        Me.BlockLog.UseCompatibleStateImageBehavior = False
        Me.BlockLog.User = Nothing
        Me.BlockLog.View = System.Windows.Forms.View.Details
        '
        'WarnLog
        '
        Me.WarnLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarnLog.GridLines = True
        Me.WarnLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.WarnLog.Location = New System.Drawing.Point(12, 196)
        Me.WarnLog.Name = "WarnLog"
        Me.WarnLog.Size = New System.Drawing.Size(518, 137)
        Me.WarnLog.TabIndex = 16
        Me.WarnLog.UseCompatibleStateImageBehavior = False
        Me.WarnLog.User = Nothing
        Me.WarnLog.View = System.Windows.Forms.View.Details
        '
        'UserInfoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 368)
        Me.Controls.Add(Me.WarnLog)
        Me.Controls.Add(Me.BlockLog)
        Me.Controls.Add(Me.SessionEdits)
        Me.Controls.Add(Me.Anonymous)
        Me.Controls.Add(Me.AnonymousLabel)
        Me.Controls.Add(Me.SessionEditsLabel)
        Me.Controls.Add(Me.SharedIP)
        Me.Controls.Add(Me.Ignored)
        Me.Controls.Add(Me.EditsLabel)
        Me.Controls.Add(Me.IgnoredLabel)
        Me.Controls.Add(Me.Edits)
        Me.Controls.Add(Me.SharedIPLabel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.WarnLogLabel)
        Me.Controls.Add(Me.BlockLogLabel)
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
    Friend WithEvents BlockLogLabel As System.Windows.Forms.Label
    Friend WithEvents WarnLogLabel As System.Windows.Forms.Label
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents SharedIPLabel As System.Windows.Forms.Label
    Friend WithEvents Edits As System.Windows.Forms.Label
    Friend WithEvents IgnoredLabel As System.Windows.Forms.Label
    Friend WithEvents EditsLabel As System.Windows.Forms.Label
    Friend WithEvents Ignored As System.Windows.Forms.Label
    Friend WithEvents SharedIP As System.Windows.Forms.Label
    Friend WithEvents SessionEditsLabel As System.Windows.Forms.Label
    Friend WithEvents AnonymousLabel As System.Windows.Forms.Label
    Friend WithEvents Anonymous As System.Windows.Forms.Label
    Friend WithEvents SessionEdits As System.Windows.Forms.Label
    Friend WithEvents BlockLog As Huggle.UserLog
    Friend WithEvents WarnLog As Huggle.WarnLog
End Class
