<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RevertAndWarnForm
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Summary = New System.Windows.Forms.ComboBox
        Me.OK = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.WarnType = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.WarnLog = New System.Windows.Forms.ListView
        Me.Level = New System.Windows.Forms.GroupBox
        Me.LevelFinal = New System.Windows.Forms.RadioButton
        Me.Level3 = New System.Windows.Forms.RadioButton
        Me.Level2 = New System.Windows.Forms.RadioButton
        Me.Level1 = New System.Windows.Forms.RadioButton
        Me.LevelAuto = New System.Windows.Forms.RadioButton
        Me.Label3 = New System.Windows.Forms.Label
        Me.Level.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Revert summary (leave blank for default):"
        '
        'Summary
        '
        Me.Summary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Summary.FormattingEnabled = True
        Me.Summary.Location = New System.Drawing.Point(15, 35)
        Me.Summary.Name = "Summary"
        Me.Summary.Size = New System.Drawing.Size(408, 21)
        Me.Summary.TabIndex = 1
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(267, 322)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 7
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(348, 322)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 8
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'WarnType
        '
        Me.WarnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.WarnType.FormattingEnabled = True
        Me.WarnType.Location = New System.Drawing.Point(91, 66)
        Me.WarnType.Name = "WarnType"
        Me.WarnType.Size = New System.Drawing.Size(169, 21)
        Me.WarnType.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Warning type:"
        '
        'WarnLog
        '
        Me.WarnLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarnLog.FullRowSelect = True
        Me.WarnLog.GridLines = True
        Me.WarnLog.Location = New System.Drawing.Point(15, 180)
        Me.WarnLog.MultiSelect = False
        Me.WarnLog.Name = "WarnLog"
        Me.WarnLog.ShowGroups = False
        Me.WarnLog.Size = New System.Drawing.Size(408, 136)
        Me.WarnLog.TabIndex = 6
        Me.WarnLog.UseCompatibleStateImageBehavior = False
        Me.WarnLog.View = System.Windows.Forms.View.Details
        '
        'Level
        '
        Me.Level.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Level.Controls.Add(Me.LevelFinal)
        Me.Level.Controls.Add(Me.Level3)
        Me.Level.Controls.Add(Me.Level2)
        Me.Level.Controls.Add(Me.Level1)
        Me.Level.Controls.Add(Me.LevelAuto)
        Me.Level.Location = New System.Drawing.Point(15, 102)
        Me.Level.Name = "Level"
        Me.Level.Size = New System.Drawing.Size(408, 50)
        Me.Level.TabIndex = 4
        Me.Level.TabStop = False
        Me.Level.Text = "Warning level"
        '
        'LevelFinal
        '
        Me.LevelFinal.AutoSize = True
        Me.LevelFinal.Location = New System.Drawing.Point(312, 19)
        Me.LevelFinal.Name = "LevelFinal"
        Me.LevelFinal.Size = New System.Drawing.Size(88, 17)
        Me.LevelFinal.TabIndex = 4
        Me.LevelFinal.Text = "Level 4 (final)"
        Me.LevelFinal.UseVisualStyleBackColor = True
        '
        'Level3
        '
        Me.Level3.AutoSize = True
        Me.Level3.Location = New System.Drawing.Point(234, 19)
        Me.Level3.Name = "Level3"
        Me.Level3.Size = New System.Drawing.Size(60, 17)
        Me.Level3.TabIndex = 3
        Me.Level3.Text = "Level 3"
        Me.Level3.UseVisualStyleBackColor = True
        '
        'Level2
        '
        Me.Level2.AutoSize = True
        Me.Level2.Location = New System.Drawing.Point(158, 19)
        Me.Level2.Name = "Level2"
        Me.Level2.Size = New System.Drawing.Size(60, 17)
        Me.Level2.TabIndex = 2
        Me.Level2.Text = "Level 2"
        Me.Level2.UseVisualStyleBackColor = True
        '
        'Level1
        '
        Me.Level1.AutoSize = True
        Me.Level1.Location = New System.Drawing.Point(87, 19)
        Me.Level1.Name = "Level1"
        Me.Level1.Size = New System.Drawing.Size(60, 17)
        Me.Level1.TabIndex = 1
        Me.Level1.Text = "Level 1"
        Me.Level1.UseVisualStyleBackColor = True
        '
        'LevelAuto
        '
        Me.LevelAuto.AutoSize = True
        Me.LevelAuto.Checked = True
        Me.LevelAuto.Location = New System.Drawing.Point(6, 19)
        Me.LevelAuto.Name = "LevelAuto"
        Me.LevelAuto.Size = New System.Drawing.Size(72, 17)
        Me.LevelAuto.TabIndex = 0
        Me.LevelAuto.TabStop = True
        Me.LevelAuto.Text = "Automatic"
        Me.LevelAuto.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 164)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Warnings for this user:"
        '
        'RevertAndWarnForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 357)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.WarnType)
        Me.Controls.Add(Me.WarnLog)
        Me.Controls.Add(Me.Level)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Summary)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "RevertAndWarnForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Revert and warn"
        Me.Level.ResumeLayout(False)
        Me.Level.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Summary As System.Windows.Forms.ComboBox
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents WarnType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents WarnLog As System.Windows.Forms.ListView
    Friend WithEvents Level As System.Windows.Forms.GroupBox
    Friend WithEvents LevelFinal As System.Windows.Forms.RadioButton
    Friend WithEvents Level3 As System.Windows.Forms.RadioButton
    Friend WithEvents Level2 As System.Windows.Forms.RadioButton
    Friend WithEvents Level1 As System.Windows.Forms.RadioButton
    Friend WithEvents LevelAuto As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
