<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmailForm
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
        Me.SubjectLabel = New System.Windows.Forms.Label()
        Me.Subject = New System.Windows.Forms.TextBox()
        Me.MessageLabel = New System.Windows.Forms.Label()
        Me.Message = New System.Windows.Forms.TextBox()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.Send = New System.Windows.Forms.Button()
        Me.CcMe = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'SubjectLabel
        '
        Me.SubjectLabel.AutoSize = True
        Me.SubjectLabel.Location = New System.Drawing.Point(16, 18)
        Me.SubjectLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.SubjectLabel.Name = "SubjectLabel"
        Me.SubjectLabel.Size = New System.Drawing.Size(59, 17)
        Me.SubjectLabel.TabIndex = 0
        Me.SubjectLabel.Text = "Subject:"
        '
        'Subject
        '
        Me.Subject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Subject.Location = New System.Drawing.Point(85, 15)
        Me.Subject.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Subject.Name = "Subject"
        Me.Subject.Size = New System.Drawing.Size(621, 22)
        Me.Subject.TabIndex = 1
        '
        'MessageLabel
        '
        Me.MessageLabel.AutoSize = True
        Me.MessageLabel.Location = New System.Drawing.Point(7, 50)
        Me.MessageLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.MessageLabel.Name = "MessageLabel"
        Me.MessageLabel.Size = New System.Drawing.Size(69, 17)
        Me.MessageLabel.TabIndex = 2
        Me.MessageLabel.Text = "Message:"
        '
        'Message
        '
        Me.Message.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Message.Location = New System.Drawing.Point(85, 47)
        Me.Message.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Message.Multiline = True
        Me.Message.Name = "Message"
        Me.Message.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.Message.Size = New System.Drawing.Size(621, 306)
        Me.Message.TabIndex = 3
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(608, 361)
        Me.Cancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(100, 28)
        Me.Cancel.TabIndex = 6
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Send
        '
        Me.Send.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Send.Enabled = False
        Me.Send.Location = New System.Drawing.Point(500, 361)
        Me.Send.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Send.Name = "Send"
        Me.Send.Size = New System.Drawing.Size(100, 28)
        Me.Send.TabIndex = 5
        Me.Send.Text = "Send"
        Me.Send.UseVisualStyleBackColor = True
        '
        'CcMe
        '
        Me.CcMe.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CcMe.AutoSize = True
        Me.CcMe.Checked = True
        Me.CcMe.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CcMe.Location = New System.Drawing.Point(85, 366)
        Me.CcMe.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CcMe.Name = "CcMe"
        Me.CcMe.Size = New System.Drawing.Size(241, 21)
        Me.CcMe.TabIndex = 4
        Me.CcMe.Text = "E-mail me a copy of this message"
        Me.CcMe.UseVisualStyleBackColor = True
        '
        'EmailForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(724, 404)
        Me.Controls.Add(Me.CcMe)
        Me.Controls.Add(Me.Send)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Message)
        Me.Controls.Add(Me.MessageLabel)
        Me.Controls.Add(Me.Subject)
        Me.Controls.Add(Me.SubjectLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "EmailForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "E-mail user"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SubjectLabel As System.Windows.Forms.Label
    Friend WithEvents Subject As System.Windows.Forms.TextBox
    Friend WithEvents MessageLabel As System.Windows.Forms.Label
    Friend WithEvents Message As System.Windows.Forms.TextBox
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Send As System.Windows.Forms.Button
    Friend WithEvents CcMe As System.Windows.Forms.CheckBox
End Class
