<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProtectionForm
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
        Me.Reason = New System.Windows.Forms.TextBox
        Me.ReasonLabel = New System.Windows.Forms.Label
        Me.TypeLabel = New System.Windows.Forms.Label
        Me.TypeSelect = New System.Windows.Forms.ComboBox
        Me.LogLabel = New System.Windows.Forms.Label
        Me.CurrentLevel = New System.Windows.Forms.Label
        Me.ProtectionLog = New Huggle.PageLog
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(470, 236)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 8
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(389, 236)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 7
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Reason
        '
        Me.Reason.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Reason.Location = New System.Drawing.Point(95, 39)
        Me.Reason.Multiline = True
        Me.Reason.Name = "Reason"
        Me.Reason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Reason.Size = New System.Drawing.Size(450, 71)
        Me.Reason.TabIndex = 3
        '
        'ReasonLabel
        '
        Me.ReasonLabel.AutoSize = True
        Me.ReasonLabel.Location = New System.Drawing.Point(46, 42)
        Me.ReasonLabel.Name = "ReasonLabel"
        Me.ReasonLabel.Size = New System.Drawing.Size(47, 13)
        Me.ReasonLabel.TabIndex = 2
        Me.ReasonLabel.Text = "Reason:"
        '
        'TypeLabel
        '
        Me.TypeLabel.AutoSize = True
        Me.TypeLabel.Location = New System.Drawing.Point(12, 15)
        Me.TypeLabel.Name = "TypeLabel"
        Me.TypeLabel.Size = New System.Drawing.Size(81, 13)
        Me.TypeLabel.TabIndex = 0
        Me.TypeLabel.Text = "Protection type:"
        '
        'TypeSelect
        '
        Me.TypeSelect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypeSelect.FormattingEnabled = True
        Me.TypeSelect.Items.AddRange(New Object() {"Semi-protection", "Full protection", "Move protection"})
        Me.TypeSelect.Location = New System.Drawing.Point(95, 12)
        Me.TypeSelect.Name = "TypeSelect"
        Me.TypeSelect.Size = New System.Drawing.Size(209, 21)
        Me.TypeSelect.TabIndex = 1
        '
        'LogLabel
        '
        Me.LogLabel.AutoSize = True
        Me.LogLabel.Location = New System.Drawing.Point(9, 116)
        Me.LogLabel.Name = "LogLabel"
        Me.LogLabel.Size = New System.Drawing.Size(75, 13)
        Me.LogLabel.TabIndex = 4
        Me.LogLabel.Text = "Protection log:"
        '
        'CurrentLevel
        '
        Me.CurrentLevel.AutoSize = True
        Me.CurrentLevel.Location = New System.Drawing.Point(9, 241)
        Me.CurrentLevel.Name = "CurrentLevel"
        Me.CurrentLevel.Size = New System.Drawing.Size(119, 13)
        Me.CurrentLevel.TabIndex = 6
        Me.CurrentLevel.Text = "Current protection level:"
        '
        'ProtectionLog
        '
        Me.ProtectionLog.FullRowSelect = True
        Me.ProtectionLog.GridLines = True
        Me.ProtectionLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ProtectionLog.Location = New System.Drawing.Point(12, 132)
        Me.ProtectionLog.Mode = Huggle.PageLog.ViewMode.Protect
        Me.ProtectionLog.Name = "ProtectionLog"
        Me.ProtectionLog.Page = Nothing
        Me.ProtectionLog.Size = New System.Drawing.Size(533, 98)
        Me.ProtectionLog.TabIndex = 5
        Me.ProtectionLog.UseCompatibleStateImageBehavior = False
        Me.ProtectionLog.View = System.Windows.Forms.View.Details
        '
        'ProtectionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 271)
        Me.Controls.Add(Me.ProtectionLog)
        Me.Controls.Add(Me.CurrentLevel)
        Me.Controls.Add(Me.LogLabel)
        Me.Controls.Add(Me.TypeSelect)
        Me.Controls.Add(Me.TypeLabel)
        Me.Controls.Add(Me.ReasonLabel)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ProtectionForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Request protection"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Reason As System.Windows.Forms.TextBox
    Friend WithEvents ReasonLabel As System.Windows.Forms.Label
    Friend WithEvents TypeLabel As System.Windows.Forms.Label
    Friend WithEvents TypeSelect As System.Windows.Forms.ComboBox
    Friend WithEvents LogLabel As System.Windows.Forms.Label
    Friend WithEvents CurrentLevel As System.Windows.Forms.Label
    Friend WithEvents ProtectionLog As Huggle.PageLog
End Class
