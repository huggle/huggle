<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpeedyForm
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
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.NotifyCreator = New System.Windows.Forms.CheckBox
        Me.Reason = New System.Windows.Forms.Label
        Me.Criterion = New System.Windows.Forms.ComboBox
        Me.ParametersLabel = New System.Windows.Forms.Label
        Me.Parameters = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(327, 92)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 4
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(246, 92)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 3
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'NotifyCreator
        '
        Me.NotifyCreator.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NotifyCreator.AutoSize = True
        Me.NotifyCreator.Location = New System.Drawing.Point(81, 65)
        Me.NotifyCreator.Name = "NotifyCreator"
        Me.NotifyCreator.Size = New System.Drawing.Size(116, 17)
        Me.NotifyCreator.TabIndex = 2
        Me.NotifyCreator.Text = "Notify page creator"
        Me.NotifyCreator.UseVisualStyleBackColor = True
        '
        'Reason
        '
        Me.Reason.AutoSize = True
        Me.Reason.Location = New System.Drawing.Point(27, 15)
        Me.Reason.Name = "Reason"
        Me.Reason.Size = New System.Drawing.Size(47, 13)
        Me.Reason.TabIndex = 0
        Me.Reason.Text = "Reason:"
        '
        'Criterion
        '
        Me.Criterion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Criterion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.Criterion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Criterion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Criterion.FormattingEnabled = True
        Me.Criterion.Location = New System.Drawing.Point(76, 12)
        Me.Criterion.MaxDropDownItems = 20
        Me.Criterion.Name = "Criterion"
        Me.Criterion.Size = New System.Drawing.Size(326, 21)
        Me.Criterion.TabIndex = 1
        '
        'ParametersLabel
        '
        Me.ParametersLabel.AutoSize = True
        Me.ParametersLabel.Location = New System.Drawing.Point(11, 42)
        Me.ParametersLabel.Name = "ParametersLabel"
        Me.ParametersLabel.Size = New System.Drawing.Size(63, 13)
        Me.ParametersLabel.TabIndex = 5
        Me.ParametersLabel.Text = "Parameters:"
        Me.ParametersLabel.Visible = False
        '
        'Parameters
        '
        Me.Parameters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Parameters.Location = New System.Drawing.Point(76, 39)
        Me.Parameters.Name = "Parameters"
        Me.Parameters.Size = New System.Drawing.Size(326, 20)
        Me.Parameters.TabIndex = 6
        Me.Parameters.Visible = False
        '
        'SpeedyForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 127)
        Me.Controls.Add(Me.Parameters)
        Me.Controls.Add(Me.ParametersLabel)
        Me.Controls.Add(Me.Criterion)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.NotifyCreator)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SpeedyForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Speedy tag page"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents NotifyCreator As System.Windows.Forms.CheckBox
    Friend WithEvents Reason As System.Windows.Forms.Label
    Friend WithEvents Criterion As System.Windows.Forms.ComboBox
    Friend WithEvents ParametersLabel As System.Windows.Forms.Label
    Friend WithEvents Parameters As System.Windows.Forms.TextBox
End Class
