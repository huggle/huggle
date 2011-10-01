<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MessageForm
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
        Me.Subject = New System.Windows.Forms.TextBox
        Me.Message = New System.Windows.Forms.TextBox
        Me.SubjectLabel = New System.Windows.Forms.Label
        Me.MessageLabel = New System.Windows.Forms.Label
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.AutoSign = New System.Windows.Forms.CheckBox
        Me.Summary = New System.Windows.Forms.TextBox
        Me.Help = New System.Windows.Forms.Label
        Me.SummaryLabel = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Subject
        '
        Me.Subject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Subject.Location = New System.Drawing.Point(73, 51)
        Me.Subject.Name = "Subject"
        Me.Subject.Size = New System.Drawing.Size(445, 20)
        Me.Subject.TabIndex = 2
        '
        'Message
        '
        Me.Message.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Message.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Message.Location = New System.Drawing.Point(73, 77)
        Me.Message.Multiline = True
        Me.Message.Name = "Message"
        Me.Message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Message.Size = New System.Drawing.Size(445, 133)
        Me.Message.TabIndex = 4
        '
        'SubjectLabel
        '
        Me.SubjectLabel.AutoSize = True
        Me.SubjectLabel.Location = New System.Drawing.Point(21, 54)
        Me.SubjectLabel.Name = "SubjectLabel"
        Me.SubjectLabel.Size = New System.Drawing.Size(46, 13)
        Me.SubjectLabel.TabIndex = 1
        Me.SubjectLabel.Text = "Subject:"
        '
        'MessageLabel
        '
        Me.MessageLabel.AutoSize = True
        Me.MessageLabel.Location = New System.Drawing.Point(14, 80)
        Me.MessageLabel.Name = "MessageLabel"
        Me.MessageLabel.Size = New System.Drawing.Size(53, 13)
        Me.MessageLabel.TabIndex = 3
        Me.MessageLabel.Text = "Message:"
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(443, 265)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 9
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(362, 265)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 8
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'AutoSign
        '
        Me.AutoSign.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AutoSign.AutoSize = True
        Me.AutoSign.Checked = True
        Me.AutoSign.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutoSign.Location = New System.Drawing.Point(73, 216)
        Me.AutoSign.Name = "AutoSign"
        Me.AutoSign.Size = New System.Drawing.Size(173, 17)
        Me.AutoSign.TabIndex = 5
        Me.AutoSign.Text = "Automatically append signature"
        Me.AutoSign.UseVisualStyleBackColor = True
        '
        'Summary
        '
        Me.Summary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Summary.Location = New System.Drawing.Point(73, 239)
        Me.Summary.Name = "Summary"
        Me.Summary.Size = New System.Drawing.Size(445, 20)
        Me.Summary.TabIndex = 7
        '
        'Help
        '
        Me.Help.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Help.Location = New System.Drawing.Point(73, 12)
        Me.Help.Name = "Help"
        Me.Help.Size = New System.Drawing.Size(449, 30)
        Me.Help.TabIndex = 0
        Me.Help.Text = "Specify one or both of Subject and Summary. If no summary is given, the subject w" & _
            "ill be used; if no subject is given, no header will be added."
        '
        'SummaryLabel
        '
        Me.SummaryLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SummaryLabel.AutoSize = True
        Me.SummaryLabel.Location = New System.Drawing.Point(14, 242)
        Me.SummaryLabel.Name = "SummaryLabel"
        Me.SummaryLabel.Size = New System.Drawing.Size(53, 13)
        Me.SummaryLabel.TabIndex = 6
        Me.SummaryLabel.Text = "Summary:"
        '
        'MessageForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(530, 300)
        Me.Controls.Add(Me.SummaryLabel)
        Me.Controls.Add(Me.Help)
        Me.Controls.Add(Me.Summary)
        Me.Controls.Add(Me.AutoSign)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.MessageLabel)
        Me.Controls.Add(Me.SubjectLabel)
        Me.Controls.Add(Me.Message)
        Me.Controls.Add(Me.Subject)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(433, 230)
        Me.Name = "MessageForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New message"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Subject As System.Windows.Forms.TextBox
    Friend WithEvents Message As System.Windows.Forms.TextBox
    Friend WithEvents SubjectLabel As System.Windows.Forms.Label
    Friend WithEvents MessageLabel As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents AutoSign As System.Windows.Forms.CheckBox
    Friend WithEvents Summary As System.Windows.Forms.TextBox
    Friend WithEvents Help As System.Windows.Forms.Label
    Friend WithEvents SummaryLabel As System.Windows.Forms.Label
End Class
