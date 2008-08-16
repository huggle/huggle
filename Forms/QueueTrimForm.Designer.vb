<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QueueTrimForm
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
        Me.AgeLabel1 = New System.Windows.Forms.Label
        Me.AgeLabel2 = New System.Windows.Forms.Label
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Age = New System.Windows.Forms.NumericUpDown
        CType(Me.Age, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AgeLabel1
        '
        Me.AgeLabel1.AutoSize = True
        Me.AgeLabel1.Location = New System.Drawing.Point(12, 9)
        Me.AgeLabel1.Name = "AgeLabel1"
        Me.AgeLabel1.Size = New System.Drawing.Size(192, 13)
        Me.AgeLabel1.TabIndex = 0
        Me.AgeLabel1.Text = "Discard all queued revisions older than "
        '
        'AgeLabel2
        '
        Me.AgeLabel2.AutoSize = True
        Me.AgeLabel2.Location = New System.Drawing.Point(253, 9)
        Me.AgeLabel2.Name = "AgeLabel2"
        Me.AgeLabel2.Size = New System.Drawing.Size(46, 13)
        Me.AgeLabel2.TabIndex = 2
        Me.AgeLabel2.Text = "minutes."
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(216, 32)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 4
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(135, 32)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 3
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Age
        '
        Me.Age.Location = New System.Drawing.Point(203, 6)
        Me.Age.Maximum = New Decimal(New Integer() {240, 0, 0, 0})
        Me.Age.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Age.Name = "Age"
        Me.Age.Size = New System.Drawing.Size(44, 20)
        Me.Age.TabIndex = 5
        Me.Age.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Age.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'QueueTrimForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 67)
        Me.Controls.Add(Me.Age)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.AgeLabel2)
        Me.Controls.Add(Me.AgeLabel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "QueueTrimForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Trim queue"
        CType(Me.Age, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AgeLabel1 As System.Windows.Forms.Label
    Friend WithEvents AgeLabel2 As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Age As System.Windows.Forms.NumericUpDown
End Class
