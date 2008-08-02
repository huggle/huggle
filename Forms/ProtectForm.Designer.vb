<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProtectForm
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
        Me.Reason = New System.Windows.Forms.TextBox
        Me.Expiry = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ProtectType = New System.Windows.Forms.GroupBox
        Me.NoProtection = New System.Windows.Forms.RadioButton
        Me.FullProtection = New System.Windows.Forms.RadioButton
        Me.MoveProtection = New System.Windows.Forms.CheckBox
        Me.SemiProtection = New System.Windows.Forms.RadioButton
        Me.ProtectionLog = New System.Windows.Forms.ListView
        Me.Label3 = New System.Windows.Forms.Label
        Me.CurrentLevel = New System.Windows.Forms.Label
        Me.ProtectType.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(418, 249)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 5
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(499, 249)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 6
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Reason
        '
        Me.Reason.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Reason.Location = New System.Drawing.Point(65, 12)
        Me.Reason.Multiline = True
        Me.Reason.Name = "Reason"
        Me.Reason.Size = New System.Drawing.Size(347, 59)
        Me.Reason.TabIndex = 1
        '
        'Expiry
        '
        Me.Expiry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Expiry.Location = New System.Drawing.Point(65, 77)
        Me.Expiry.Name = "Expiry"
        Me.Expiry.Size = New System.Drawing.Size(347, 20)
        Me.Expiry.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Reason:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Expiry:"
        '
        'ProtectType
        '
        Me.ProtectType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProtectType.Controls.Add(Me.NoProtection)
        Me.ProtectType.Controls.Add(Me.FullProtection)
        Me.ProtectType.Controls.Add(Me.MoveProtection)
        Me.ProtectType.Controls.Add(Me.SemiProtection)
        Me.ProtectType.Location = New System.Drawing.Point(418, 12)
        Me.ProtectType.Name = "ProtectType"
        Me.ProtectType.Size = New System.Drawing.Size(156, 116)
        Me.ProtectType.TabIndex = 4
        Me.ProtectType.TabStop = False
        Me.ProtectType.Text = "Protection type"
        '
        'NoProtection
        '
        Me.NoProtection.AutoSize = True
        Me.NoProtection.Location = New System.Drawing.Point(12, 19)
        Me.NoProtection.Name = "NoProtection"
        Me.NoProtection.Size = New System.Drawing.Size(51, 17)
        Me.NoProtection.TabIndex = 3
        Me.NoProtection.Text = "None"
        Me.NoProtection.UseVisualStyleBackColor = True
        '
        'FullProtection
        '
        Me.FullProtection.AutoSize = True
        Me.FullProtection.Location = New System.Drawing.Point(12, 65)
        Me.FullProtection.Name = "FullProtection"
        Me.FullProtection.Size = New System.Drawing.Size(91, 17)
        Me.FullProtection.TabIndex = 1
        Me.FullProtection.Text = "Full protection"
        Me.FullProtection.UseVisualStyleBackColor = True
        '
        'MoveProtection
        '
        Me.MoveProtection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MoveProtection.AutoSize = True
        Me.MoveProtection.Location = New System.Drawing.Point(12, 88)
        Me.MoveProtection.Name = "MoveProtection"
        Me.MoveProtection.Size = New System.Drawing.Size(103, 17)
        Me.MoveProtection.TabIndex = 2
        Me.MoveProtection.Text = "Move protection"
        Me.MoveProtection.UseVisualStyleBackColor = True
        '
        'SemiProtection
        '
        Me.SemiProtection.AutoSize = True
        Me.SemiProtection.Checked = True
        Me.SemiProtection.Location = New System.Drawing.Point(12, 42)
        Me.SemiProtection.Name = "SemiProtection"
        Me.SemiProtection.Size = New System.Drawing.Size(98, 17)
        Me.SemiProtection.TabIndex = 0
        Me.SemiProtection.TabStop = True
        Me.SemiProtection.Text = "Semi-protection"
        Me.SemiProtection.UseVisualStyleBackColor = True
        '
        'ProtectionLog
        '
        Me.ProtectionLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProtectionLog.FullRowSelect = True
        Me.ProtectionLog.GridLines = True
        Me.ProtectionLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ProtectionLog.Location = New System.Drawing.Point(12, 134)
        Me.ProtectionLog.MultiSelect = False
        Me.ProtectionLog.Name = "ProtectionLog"
        Me.ProtectionLog.ShowGroups = False
        Me.ProtectionLog.Size = New System.Drawing.Size(562, 109)
        Me.ProtectionLog.TabIndex = 13
        Me.ProtectionLog.UseCompatibleStateImageBehavior = False
        Me.ProtectionLog.View = System.Windows.Forms.View.Details
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Protection log:"
        '
        'CurrentLevel
        '
        Me.CurrentLevel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CurrentLevel.AutoSize = True
        Me.CurrentLevel.Location = New System.Drawing.Point(12, 259)
        Me.CurrentLevel.Name = "CurrentLevel"
        Me.CurrentLevel.Size = New System.Drawing.Size(119, 13)
        Me.CurrentLevel.TabIndex = 18
        Me.CurrentLevel.Text = "Current protection level:"
        '
        'ProtectForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 284)
        Me.Controls.Add(Me.CurrentLevel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ProtectionLog)
        Me.Controls.Add(Me.ProtectType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Expiry)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ProtectForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Protect page"
        Me.ProtectType.ResumeLayout(False)
        Me.ProtectType.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Reason As System.Windows.Forms.TextBox
    Friend WithEvents Expiry As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ProtectType As System.Windows.Forms.GroupBox
    Friend WithEvents MoveProtection As System.Windows.Forms.CheckBox
    Friend WithEvents FullProtection As System.Windows.Forms.RadioButton
    Friend WithEvents SemiProtection As System.Windows.Forms.RadioButton
    Friend WithEvents ProtectionLog As System.Windows.Forms.ListView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CurrentLevel As System.Windows.Forms.Label
    Friend WithEvents NoProtection As System.Windows.Forms.RadioButton
End Class
