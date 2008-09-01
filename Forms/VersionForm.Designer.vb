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
        Me.Update = New System.Windows.Forms.Button
        Me.Status = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Progress = New System.Windows.Forms.ProgressBar
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Message
        '
        Me.Message.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Message.Location = New System.Drawing.Point(54, 9)
        Me.Message.Name = "Message"
        Me.Message.Size = New System.Drawing.Size(411, 43)
        Me.Message.TabIndex = 0
        Me.Message.Text = "This version of Huggle is out of date." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Updating to the latest version, 0.0.0, is" & _
            " recommended, and may be required in future." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Download and run the latest versio" & _
            "n now?"
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(390, 55)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 1
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Update
        '
        Me.Update.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Update.Location = New System.Drawing.Point(309, 55)
        Me.Update.Name = "Update"
        Me.Update.Size = New System.Drawing.Size(75, 23)
        Me.Update.TabIndex = 1
        Me.Update.Text = "Update"
        Me.Update.UseVisualStyleBackColor = True
        '
        'Status
        '
        Me.Status.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Status.AutoSize = True
        Me.Status.Location = New System.Drawing.Point(12, 60)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(10, 13)
        Me.Status.TabIndex = 3
        Me.Status.Text = " "
        '
        'PictureBox1
        '
        Me.PictureBox1.ErrorImage = Nothing
        Me.PictureBox1.Image = Global.Huggle.My.Resources.Resources.user_template
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(12, 11)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(36, 36)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'Progress
        '
        Me.Progress.Location = New System.Drawing.Point(176, 58)
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(127, 18)
        Me.Progress.TabIndex = 5
        Me.Progress.Visible = False
        '
        'VersionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(477, 90)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.Update)
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
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Message As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Update As System.Windows.Forms.Button
    Friend WithEvents Status As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Progress As System.Windows.Forms.ProgressBar
End Class
