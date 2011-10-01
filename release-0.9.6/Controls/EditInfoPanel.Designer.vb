<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditInfoPanel
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Change = New System.Windows.Forms.Label
        Me.Summary = New System.Windows.Forms.Label
        Me.Time = New System.Windows.Forms.Label
        Me.PageUser = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Change
        '
        Me.Change.AutoSize = True
        Me.Change.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Change.Location = New System.Drawing.Point(187, 22)
        Me.Change.Name = "Change"
        Me.Change.Size = New System.Drawing.Size(51, 13)
        Me.Change.TabIndex = 6
        Me.Change.Text = "+123456"
        '
        'Summary
        '
        Me.Summary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Summary.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Summary.Location = New System.Drawing.Point(2, 38)
        Me.Summary.Name = "Summary"
        Me.Summary.Size = New System.Drawing.Size(352, 36)
        Me.Summary.TabIndex = 7
        Me.Summary.Text = "Summary" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Time
        '
        Me.Time.AutoSize = True
        Me.Time.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Time.Location = New System.Drawing.Point(2, 22)
        Me.Time.Name = "Time"
        Me.Time.Size = New System.Drawing.Size(167, 13)
        Me.Time.TabIndex = 5
        Me.Time.Text = "00:00, 31 September 2008 (UTC)"
        '
        'PageUser
        '
        Me.PageUser.AutoSize = True
        Me.PageUser.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PageUser.Location = New System.Drawing.Point(3, 4)
        Me.PageUser.Name = "PageUser"
        Me.PageUser.Size = New System.Drawing.Size(108, 13)
        Me.PageUser.TabIndex = 4
        Me.PageUser.Text = "Page / User Name"
        '
        'EditInfoPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Change)
        Me.Controls.Add(Me.Summary)
        Me.Controls.Add(Me.Time)
        Me.Controls.Add(Me.PageUser)
        Me.Name = "EditInfoPanel"
        Me.Size = New System.Drawing.Size(357, 84)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Change As System.Windows.Forms.Label
    Friend WithEvents Summary As System.Windows.Forms.Label
    Friend WithEvents Time As System.Windows.Forms.Label
    Friend WithEvents PageUser As System.Windows.Forms.Label

End Class
