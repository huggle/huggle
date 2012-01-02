<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClosingForm
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
        Me.Status = New System.Windows.Forms.Label()
        Me.Progress = New System.Windows.Forms.ProgressBar()
        Me.Logo = New System.Windows.Forms.PictureBox()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Status
        '
        Me.Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Status.Location = New System.Drawing.Point(16, 101)
        Me.Status.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(393, 26)
        Me.Status.TabIndex = 0
        Me.Status.Text = " "
        '
        'Progress
        '
        Me.Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Progress.Location = New System.Drawing.Point(16, 130)
        Me.Progress.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Progress.Maximum = 3
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(393, 28)
        Me.Progress.Step = 1
        Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.Progress.TabIndex = 1
        '
        'Logo
        '
        Me.Logo.Image = Global.Huggle.My.Resources.Resources.huggle_logo
        Me.Logo.Location = New System.Drawing.Point(50, 1)
        Me.Logo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(320, 80)
        Me.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Logo.TabIndex = 3
        Me.Logo.TabStop = False
        '
        'ClosingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(425, 174)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.Status)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ClosingForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Huggle"
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Status As System.Windows.Forms.Label
    Friend WithEvents Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents Logo As System.Windows.Forms.PictureBox
End Class
