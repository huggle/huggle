<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RegexBox
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
        Me.Pattern = New System.Windows.Forms.TextBox
        Me.Status = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Pattern
        '
        Me.Pattern.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pattern.Location = New System.Drawing.Point(0, 0)
        Me.Pattern.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Pattern.Name = "Pattern"
        Me.Pattern.Size = New System.Drawing.Size(162, 20)
        Me.Pattern.TabIndex = 0
        '
        'Status
        '
        Me.Status.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Status.Location = New System.Drawing.Point(0, 23)
        Me.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(162, 13)
        Me.Status.TabIndex = 1
        Me.Status.Text = " "
        '
        'RegexBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.Pattern)
        Me.Name = "RegexBox"
        Me.Size = New System.Drawing.Size(162, 36)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Pattern As System.Windows.Forms.TextBox
    Friend WithEvents Status As System.Windows.Forms.Label

End Class
