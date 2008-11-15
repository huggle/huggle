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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Criterion = New System.Windows.Forms.ComboBox
        Me.ParamLabel = New System.Windows.Forms.Label
        Me.Param = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(218, 92)
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
        Me.OK.Location = New System.Drawing.Point(137, 92)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Reason:"
        '
        'Criterion
        '
        Me.Criterion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Criterion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.Criterion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Criterion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Criterion.FormattingEnabled = True
        Me.Criterion.Location = New System.Drawing.Point(81, 12)
        Me.Criterion.MaxDropDownItems = 20
        Me.Criterion.Name = "Criterion"
        Me.Criterion.Size = New System.Drawing.Size(212, 21)
        Me.Criterion.TabIndex = 1
        '
        'ParamLabel
        '
        Me.ParamLabel.AutoSize = True
        Me.ParamLabel.Location = New System.Drawing.Point(12, 42)
        Me.ParamLabel.Name = "ParamLabel"
        Me.ParamLabel.Size = New System.Drawing.Size(63, 13)
        Me.ParamLabel.TabIndex = 5
        Me.ParamLabel.Text = "Parameters:"
        Me.ParamLabel.Visible = False
        '
        'Param
        '
        Me.Param.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Param.Location = New System.Drawing.Point(81, 39)
        Me.Param.Name = "Param"
        Me.Param.Size = New System.Drawing.Size(212, 20)
        Me.Param.TabIndex = 6
        Me.Param.Visible = False
        '
        'SpeedyForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(305, 127)
        Me.Controls.Add(Me.Param)
        Me.Controls.Add(Me.ParamLabel)
        Me.Controls.Add(Me.Criterion)
        Me.Controls.Add(Me.Label1)
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Criterion As System.Windows.Forms.ComboBox
    Friend WithEvents ParamLabel As System.Windows.Forms.Label
    Friend WithEvents Param As System.Windows.Forms.TextBox
End Class
