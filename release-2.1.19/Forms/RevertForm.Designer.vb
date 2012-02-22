<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RevertForm
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
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.SummaryLabel = New System.Windows.Forms.Label()
        Me.Summary = New System.Windows.Forms.ComboBox()
        Me.CurrentOnly = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(472, 64)
        Me.Cancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(100, 28)
        Me.Cancel.TabIndex = 4
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(364, 64)
        Me.OK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(100, 28)
        Me.OK.TabIndex = 3
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'SummaryLabel
        '
        Me.SummaryLabel.AutoSize = True
        Me.SummaryLabel.Location = New System.Drawing.Point(16, 11)
        Me.SummaryLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.SummaryLabel.Name = "SummaryLabel"
        Me.SummaryLabel.Size = New System.Drawing.Size(269, 17)
        Me.SummaryLabel.TabIndex = 0
        Me.SummaryLabel.Text = "Revert summary (leave blank for default):"
        '
        'Summary
        '
        Me.Summary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Summary.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Summary.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Summary.FormattingEnabled = True
        Me.Summary.Location = New System.Drawing.Point(20, 31)
        Me.Summary.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Summary.Name = "Summary"
        Me.Summary.Size = New System.Drawing.Size(551, 24)
        Me.Summary.TabIndex = 1
        '
        'CurrentOnly
        '
        Me.CurrentOnly.AutoSize = True
        Me.CurrentOnly.Location = New System.Drawing.Point(20, 64)
        Me.CurrentOnly.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CurrentOnly.Name = "CurrentOnly"
        Me.CurrentOnly.Size = New System.Drawing.Size(236, 21)
        Me.CurrentOnly.TabIndex = 2
        Me.CurrentOnly.Text = "Revert only the selected revision"
        Me.CurrentOnly.UseVisualStyleBackColor = True
        '
        'RevertForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(588, 105)
        Me.Controls.Add(Me.CurrentOnly)
        Me.Controls.Add(Me.Summary)
        Me.Controls.Add(Me.SummaryLabel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RevertForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Revert"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents SummaryLabel As System.Windows.Forms.Label
    Friend WithEvents Summary As System.Windows.Forms.ComboBox
    Friend WithEvents CurrentOnly As System.Windows.Forms.CheckBox
End Class
