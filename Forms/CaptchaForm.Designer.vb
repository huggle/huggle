<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CaptchaForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CaptchaForm))
        Me.Answer = New System.Windows.Forms.TextBox
        Me.OK = New System.Windows.Forms.Button
        Me.Captcha = New System.Windows.Forms.PictureBox
        CType(Me.Captcha, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Answer
        '
        Me.Answer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Answer.Location = New System.Drawing.Point(12, 113)
        Me.Answer.Name = "Answer"
        Me.Answer.Size = New System.Drawing.Size(236, 20)
        Me.Answer.TabIndex = 0
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(254, 111)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(68, 23)
        Me.OK.TabIndex = 1
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Captcha
        '
        Me.Captcha.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Captcha.BackColor = System.Drawing.SystemColors.Control
        Me.Captcha.Location = New System.Drawing.Point(12, 12)
        Me.Captcha.Name = "Captcha"
        Me.Captcha.Size = New System.Drawing.Size(310, 93)
        Me.Captcha.TabIndex = 2
        Me.Captcha.TabStop = False
        '
        'CaptchaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 145)
        Me.Controls.Add(Me.Captcha)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Answer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CaptchaForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Captcha required to log in"
        CType(Me.Captcha, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Answer As System.Windows.Forms.TextBox
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Captcha As System.Windows.Forms.PictureBox
End Class
