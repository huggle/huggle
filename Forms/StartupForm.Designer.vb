<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartupForm
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
        Me.Browser = New System.Windows.Forms.WebBrowser
        Me.ShowStartupMessage = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(491, 253)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(88, 23)
        Me.OK.TabIndex = 0
        Me.OK.Text = "Continue >"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Browser
        '
        Me.Browser.AllowWebBrowserDrop = False
        Me.Browser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Browser.Location = New System.Drawing.Point(12, 12)
        Me.Browser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.Browser.Name = "Browser"
        Me.Browser.ScriptErrorsSuppressed = True
        Me.Browser.Size = New System.Drawing.Size(567, 235)
        Me.Browser.TabIndex = 1
        '
        'ShowStartupMessage
        '
        Me.ShowStartupMessage.AutoSize = True
        Me.ShowStartupMessage.Checked = True
        Me.ShowStartupMessage.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowStartupMessage.Location = New System.Drawing.Point(12, 257)
        Me.ShowStartupMessage.Name = "ShowStartupMessage"
        Me.ShowStartupMessage.Size = New System.Drawing.Size(158, 17)
        Me.ShowStartupMessage.TabIndex = 2
        Me.ShowStartupMessage.Text = "Show this window at startup"
        Me.ShowStartupMessage.UseVisualStyleBackColor = True
        '
        'StartupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 288)
        Me.Controls.Add(Me.ShowStartupMessage)
        Me.Controls.Add(Me.Browser)
        Me.Controls.Add(Me.OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "StartupForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Messages"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents ShowStartupMessage As System.Windows.Forms.CheckBox
    Friend WithEvents Browser As System.Windows.Forms.WebBrowser
End Class
