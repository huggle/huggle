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
        Me.Status = New System.Windows.Forms.Label
        Me.Logo = New System.Windows.Forms.Label
        Me.Progress = New System.Windows.Forms.ProgressBar
        Me.SuspendLayout()
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(12, 52)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(268, 30)
        Me.Status.TabIndex = 1
        Me.Status.Text = " "
        '
        'Logo
        '
        Me.Logo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Logo.Font = New System.Drawing.Font("Tahoma", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Logo.Location = New System.Drawing.Point(12, 2)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(271, 50)
        Me.Logo.TabIndex = 0
        Me.Logo.Text = "huggle"
        Me.Logo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Progress
        '
        Me.Progress.Location = New System.Drawing.Point(15, 85)
        Me.Progress.Maximum = 3
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(265, 23)
        Me.Progress.Step = 1
        Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.Progress.TabIndex = 2
        '
        'ClosingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(295, 115)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.Status)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ClosingForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Huggle"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Status As System.Windows.Forms.Label
    Friend WithEvents Logo As System.Windows.Forms.Label
    Friend WithEvents Progress As System.Windows.Forms.ProgressBar
End Class
