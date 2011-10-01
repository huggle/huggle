<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QueuePanel
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
        Me.Count = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Count
        '
        Me.Count.AutoSize = True
        Me.Count.Location = New System.Drawing.Point(3, 4)
        Me.Count.Name = "Count"
        Me.Count.Size = New System.Drawing.Size(10, 13)
        Me.Count.TabIndex = 0
        Me.Count.Text = " "
        '
        'QueuePanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Count)
        Me.Name = "QueuePanel"
        Me.Size = New System.Drawing.Size(200, 200)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Count As System.Windows.Forms.Label

End Class
