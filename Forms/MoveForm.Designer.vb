<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MoveForm
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
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.TargetLabel = New System.Windows.Forms.Label()
        Me.ReasonLabel = New System.Windows.Forms.Label()
        Me.Target = New System.Windows.Forms.TextBox()
        Me.Reason = New System.Windows.Forms.TextBox()
        Me.MoveTalk = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(301, 113)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 6
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(220, 113)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 5
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'TargetLabel
        '
        Me.TargetLabel.AutoSize = True
        Me.TargetLabel.Location = New System.Drawing.Point(12, 15)
        Me.TargetLabel.Name = "TargetLabel"
        Me.TargetLabel.Size = New System.Drawing.Size(63, 13)
        Me.TargetLabel.TabIndex = 0
        Me.TargetLabel.Text = "Destination:"
        '
        'ReasonLabel
        '
        Me.ReasonLabel.AutoSize = True
        Me.ReasonLabel.Location = New System.Drawing.Point(28, 41)
        Me.ReasonLabel.Name = "ReasonLabel"
        Me.ReasonLabel.Size = New System.Drawing.Size(47, 13)
        Me.ReasonLabel.TabIndex = 2
        Me.ReasonLabel.Text = "Reason:"
        '
        'Target
        '
        Me.Target.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Target.Location = New System.Drawing.Point(81, 12)
        Me.Target.Name = "Target"
        Me.Target.Size = New System.Drawing.Size(295, 20)
        Me.Target.TabIndex = 1
        '
        'Reason
        '
        Me.Reason.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Reason.Location = New System.Drawing.Point(81, 38)
        Me.Reason.MaxLength = 250
        Me.Reason.Multiline = True
        Me.Reason.Name = "Reason"
        Me.Reason.Size = New System.Drawing.Size(295, 71)
        Me.Reason.TabIndex = 3
        '
        'MoveTalk
        '
        Me.MoveTalk.AutoSize = True
        Me.MoveTalk.Location = New System.Drawing.Point(81, 117)
        Me.MoveTalk.Name = "MoveTalk"
        Me.MoveTalk.Size = New System.Drawing.Size(100, 17)
        Me.MoveTalk.TabIndex = 4
        Me.MoveTalk.Text = "Move talk page"
        Me.MoveTalk.UseVisualStyleBackColor = True
        Me.MoveTalk.Visible = False
        '
        'MoveForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 148)
        Me.Controls.Add(Me.MoveTalk)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.Target)
        Me.Controls.Add(Me.ReasonLabel)
        Me.Controls.Add(Me.TargetLabel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MoveForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Move page"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents TargetLabel As System.Windows.Forms.Label
    Friend WithEvents ReasonLabel As System.Windows.Forms.Label
    Friend WithEvents Target As System.Windows.Forms.TextBox
    Friend WithEvents Reason As System.Windows.Forms.TextBox
    Friend WithEvents MoveTalk As System.Windows.Forms.CheckBox
End Class
