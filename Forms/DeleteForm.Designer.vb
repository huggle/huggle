<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeleteForm
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
        Me.ReasonLabel = New System.Windows.Forms.Label
        Me.Reason = New System.Windows.Forms.ComboBox
        Me.DeletionLogLabel = New System.Windows.Forms.Label
        Me.DeletionLog = New Huggle.PageLog
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(488, 178)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 5
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(407, 178)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 4
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'ReasonLabel
        '
        Me.ReasonLabel.AutoSize = True
        Me.ReasonLabel.Location = New System.Drawing.Point(12, 15)
        Me.ReasonLabel.Name = "ReasonLabel"
        Me.ReasonLabel.Size = New System.Drawing.Size(47, 13)
        Me.ReasonLabel.TabIndex = 0
        Me.ReasonLabel.Text = "Reason:"
        '
        'Reason
        '
        Me.Reason.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Reason.FormattingEnabled = True
        Me.Reason.Location = New System.Drawing.Point(65, 12)
        Me.Reason.MaxDropDownItems = 20
        Me.Reason.Name = "Reason"
        Me.Reason.Size = New System.Drawing.Size(498, 21)
        Me.Reason.TabIndex = 1
        '
        'DeletionLogLabel
        '
        Me.DeletionLogLabel.AutoSize = True
        Me.DeletionLogLabel.Location = New System.Drawing.Point(12, 45)
        Me.DeletionLogLabel.Name = "DeletionLogLabel"
        Me.DeletionLogLabel.Size = New System.Drawing.Size(66, 13)
        Me.DeletionLogLabel.TabIndex = 2
        Me.DeletionLogLabel.Text = "Deletion log:"
        '
        'DeletionLog
        '
        Me.DeletionLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DeletionLog.GridLines = True
        Me.DeletionLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.DeletionLog.Location = New System.Drawing.Point(15, 61)
        Me.DeletionLog.Mode = Huggle.PageLog.ViewMode.Delete
        Me.DeletionLog.Name = "DeletionLog"
        Me.DeletionLog.Page = Nothing
        Me.DeletionLog.Size = New System.Drawing.Size(548, 111)
        Me.DeletionLog.TabIndex = 6
        Me.DeletionLog.UseCompatibleStateImageBehavior = False
        Me.DeletionLog.View = System.Windows.Forms.View.Details
        '
        'DeleteForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(575, 213)
        Me.Controls.Add(Me.DeletionLog)
        Me.Controls.Add(Me.DeletionLogLabel)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.ReasonLabel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DeleteForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Delete page"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents ReasonLabel As System.Windows.Forms.Label
    Friend WithEvents Reason As System.Windows.Forms.ComboBox
    Friend WithEvents DeletionLogLabel As System.Windows.Forms.Label
    Friend WithEvents DeletionLog As Huggle.PageLog
End Class
