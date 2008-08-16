<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VersionForm
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
        Me.Message = New System.Windows.Forms.Label
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Status = New System.Windows.Forms.Label
        Me.Throbber = New Huggle.Throbber
        Me.SuspendLayout()
        '
        'Message
        '
        Me.Message.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Message.AutoSize = True
        Me.Message.Location = New System.Drawing.Point(12, 9)
        Me.Message.Name = "Message"
        Me.Message.Size = New System.Drawing.Size(404, 39)
        Me.Message.TabIndex = 0
        Me.Message.Text = "This version of Huggle is out of date." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Updating to the latest version, 0.0.0, is" & _
            " recommended, and may be required in future." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Download and run the latest versio" & _
            "n now?"
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(356, 58)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 1
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(275, 58)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 1
        Me.OK.Text = "Update"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Status
        '
        Me.Status.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Status.AutoSize = True
        Me.Status.Location = New System.Drawing.Point(12, 63)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(10, 13)
        Me.Status.TabIndex = 3
        Me.Status.Text = " "
        '
        'Throbber
        '
        Me.Throbber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Throbber.BackColor = System.Drawing.Color.Gainsboro
        Me.Throbber.Location = New System.Drawing.Point(211, 66)
        Me.Throbber.Name = "Throbber"
        Me.Throbber.Size = New System.Drawing.Size(55, 10)
        Me.Throbber.TabIndex = 2
        Me.Throbber.Visible = False
        '
        'VersionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 93)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.Throbber)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Message)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "VersionForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New version available"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Message As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Throbber As Huggle.Throbber
    Friend WithEvents Status As System.Windows.Forms.Label
End Class
