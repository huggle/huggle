<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WarningForm
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
        Me.OK = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.LevelGroup = New System.Windows.Forms.GroupBox
        Me.LevelFinal = New System.Windows.Forms.RadioButton
        Me.Level3 = New System.Windows.Forms.RadioButton
        Me.Level2 = New System.Windows.Forms.RadioButton
        Me.Level1 = New System.Windows.Forms.RadioButton
        Me.LevelAuto = New System.Windows.Forms.RadioButton
        Me.WarnLogLabel = New System.Windows.Forms.Label
        Me.WarnTypeLabel = New System.Windows.Forms.Label
        Me.WarnType = New System.Windows.Forms.ComboBox
        Me.WarnLog = New Huggle.WarnLog
        Me.LevelGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.AllowDrop = True
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(255, 260)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 4
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.AllowDrop = True
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(336, 260)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 5
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'LevelGroup
        '
        Me.LevelGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LevelGroup.Controls.Add(Me.LevelFinal)
        Me.LevelGroup.Controls.Add(Me.Level3)
        Me.LevelGroup.Controls.Add(Me.Level2)
        Me.LevelGroup.Controls.Add(Me.Level1)
        Me.LevelGroup.Controls.Add(Me.LevelAuto)
        Me.LevelGroup.Location = New System.Drawing.Point(12, 12)
        Me.LevelGroup.Name = "LevelGroup"
        Me.LevelGroup.Size = New System.Drawing.Size(399, 46)
        Me.LevelGroup.TabIndex = 0
        Me.LevelGroup.TabStop = False
        Me.LevelGroup.Text = "Warning level"
        '
        'LevelFinal
        '
        Me.LevelFinal.AutoSize = True
        Me.LevelFinal.Location = New System.Drawing.Point(303, 19)
        Me.LevelFinal.Name = "LevelFinal"
        Me.LevelFinal.Size = New System.Drawing.Size(88, 17)
        Me.LevelFinal.TabIndex = 4
        Me.LevelFinal.Text = "Level &4 (final)"
        Me.LevelFinal.UseVisualStyleBackColor = True
        '
        'Level3
        '
        Me.Level3.AutoSize = True
        Me.Level3.Location = New System.Drawing.Point(233, 19)
        Me.Level3.Name = "Level3"
        Me.Level3.Size = New System.Drawing.Size(60, 17)
        Me.Level3.TabIndex = 3
        Me.Level3.Text = "Level &3"
        Me.Level3.UseVisualStyleBackColor = True
        '
        'Level2
        '
        Me.Level2.AutoSize = True
        Me.Level2.Location = New System.Drawing.Point(162, 19)
        Me.Level2.Name = "Level2"
        Me.Level2.Size = New System.Drawing.Size(60, 17)
        Me.Level2.TabIndex = 2
        Me.Level2.Text = "Level &2"
        Me.Level2.UseVisualStyleBackColor = True
        '
        'Level1
        '
        Me.Level1.AutoSize = True
        Me.Level1.Location = New System.Drawing.Point(91, 19)
        Me.Level1.Name = "Level1"
        Me.Level1.Size = New System.Drawing.Size(60, 17)
        Me.Level1.TabIndex = 1
        Me.Level1.Text = "Level &1"
        Me.Level1.UseVisualStyleBackColor = True
        '
        'LevelAuto
        '
        Me.LevelAuto.AutoSize = True
        Me.LevelAuto.Checked = True
        Me.LevelAuto.Location = New System.Drawing.Point(9, 19)
        Me.LevelAuto.Name = "LevelAuto"
        Me.LevelAuto.Size = New System.Drawing.Size(72, 17)
        Me.LevelAuto.TabIndex = 0
        Me.LevelAuto.TabStop = True
        Me.LevelAuto.Text = "&Automatic"
        Me.LevelAuto.UseVisualStyleBackColor = True
        '
        'WarnLogLabel
        '
        Me.WarnLogLabel.AutoSize = True
        Me.WarnLogLabel.Location = New System.Drawing.Point(12, 100)
        Me.WarnLogLabel.Name = "WarnLogLabel"
        Me.WarnLogLabel.Size = New System.Drawing.Size(112, 13)
        Me.WarnLogLabel.TabIndex = 2
        Me.WarnLogLabel.Text = "Warnings for this user:"
        '
        'WarnTypeLabel
        '
        Me.WarnTypeLabel.AutoSize = True
        Me.WarnTypeLabel.Location = New System.Drawing.Point(15, 70)
        Me.WarnTypeLabel.Name = "WarnTypeLabel"
        Me.WarnTypeLabel.Size = New System.Drawing.Size(73, 13)
        Me.WarnTypeLabel.TabIndex = 6
        Me.WarnTypeLabel.Text = "Warning type:"
        '
        'WarnType
        '
        Me.WarnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.WarnType.FormattingEnabled = True
        Me.WarnType.Location = New System.Drawing.Point(94, 67)
        Me.WarnType.Name = "WarnType"
        Me.WarnType.Size = New System.Drawing.Size(167, 21)
        Me.WarnType.TabIndex = 7
        '
        'WarnLog
        '
        Me.WarnLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarnLog.GridLines = True
        Me.WarnLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.WarnLog.Location = New System.Drawing.Point(15, 116)
        Me.WarnLog.Name = "WarnLog"
        Me.WarnLog.Size = New System.Drawing.Size(396, 138)
        Me.WarnLog.TabIndex = 8
        Me.WarnLog.UseCompatibleStateImageBehavior = False
        Me.WarnLog.User = Nothing
        Me.WarnLog.View = System.Windows.Forms.View.Details
        '
        'WarningForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 295)
        Me.Controls.Add(Me.WarnLog)
        Me.Controls.Add(Me.WarnType)
        Me.Controls.Add(Me.WarnTypeLabel)
        Me.Controls.Add(Me.WarnLogLabel)
        Me.Controls.Add(Me.LevelGroup)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WarningForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Warn user"
        Me.LevelGroup.ResumeLayout(False)
        Me.LevelGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents LevelGroup As System.Windows.Forms.GroupBox
    Friend WithEvents LevelFinal As System.Windows.Forms.RadioButton
    Friend WithEvents Level3 As System.Windows.Forms.RadioButton
    Friend WithEvents Level2 As System.Windows.Forms.RadioButton
    Friend WithEvents Level1 As System.Windows.Forms.RadioButton
    Friend WithEvents LevelAuto As System.Windows.Forms.RadioButton
    Friend WithEvents WarnLogLabel As System.Windows.Forms.Label
    Friend WithEvents WarnTypeLabel As System.Windows.Forms.Label
    Friend WithEvents WarnType As System.Windows.Forms.ComboBox
    Friend WithEvents WarnLog As Huggle.WarnLog
End Class
