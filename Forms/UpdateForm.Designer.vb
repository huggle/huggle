<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateForm
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
        Me.Message = New System.Windows.Forms.Label()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.Download = New System.Windows.Forms.Button()
        Me.Status = New System.Windows.Forms.Label()
        Me.Logo = New System.Windows.Forms.PictureBox()
        Me.Progress = New System.Windows.Forms.ProgressBar()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Message
        '
        Me.Message.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Message.Location = New System.Drawing.Point(54, 9)
        Me.Message.Name = "Message"
        Me.Message.Size = New System.Drawing.Size(448, 43)
        Me.Message.TabIndex = 0
        Me.Message.Text = "This version of Huggle is out of date." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Updating to the latest version, 0.0.0, is" & _
            " recommended, and may be required in future." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Download and run the latest versio" & _
            "n now?"
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(427, 55)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 4
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Download
        '
        Me.Download.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Download.Location = New System.Drawing.Point(346, 55)
        Me.Download.Name = "Download"
        Me.Download.Size = New System.Drawing.Size(75, 23)
        Me.Download.TabIndex = 3
        Me.Download.Text = "Update"
        Me.Download.UseVisualStyleBackColor = True
        '
        'Status
        '
        Me.Status.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Status.Location = New System.Drawing.Point(12, 60)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(328, 30)
        Me.Status.TabIndex = 1
        Me.Status.Text = " "
        '
        'Logo
        '
        Me.Logo.ErrorImage = Nothing
        Me.Logo.Image = Global.Huggle.My.Resources.Resources.user_template
        Me.Logo.InitialImage = Nothing
        Me.Logo.Location = New System.Drawing.Point(12, 11)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(36, 36)
        Me.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Logo.TabIndex = 4
        Me.Logo.TabStop = False
        '
        'Progress
        '
        Me.Progress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Progress.Location = New System.Drawing.Point(213, 58)
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(127, 18)
        Me.Progress.TabIndex = 2
        Me.Progress.Visible = False
        '
        'UpdateForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 90)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.Download)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.Message)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UpdateForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New version available"
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Message As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Download As System.Windows.Forms.Button
    Friend WithEvents Status As System.Windows.Forms.Label
    Friend WithEvents Logo As System.Windows.Forms.PictureBox
    Friend WithEvents Progress As System.Windows.Forms.ProgressBar
End Class
