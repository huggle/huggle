<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TriState
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
        Me._Text = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        '_Text
        '
        Me._Text.AutoSize = True
        Me._Text.Location = New System.Drawing.Point(19, 2)
        Me._Text.Name = "_Text"
        Me._Text.Size = New System.Drawing.Size(10, 13)
        Me._Text.TabIndex = 0
        Me._Text.Text = " "
        '
        'TriState
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.Controls.Add(Me._Text)
        Me.MaximumSize = New System.Drawing.Size(640, 16)
        Me.MinimumSize = New System.Drawing.Size(16, 16)
        Me.Name = "TriState"
        Me.Size = New System.Drawing.Size(72, 16)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents _Text As System.Windows.Forms.Label

End Class
