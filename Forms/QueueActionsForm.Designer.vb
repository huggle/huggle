<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QueueActionsForm
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
        Me.OK = New System.Windows.Forms.Button
        Me.NamespaceTransform = New System.Windows.Forms.Button
        Me.NamespaceTransformGroup = New System.Windows.Forms.GroupBox
        Me.NamespaceTransformLabel = New System.Windows.Forms.Label
        Me.NamespaceTransformSelector = New System.Windows.Forms.ComboBox
        Me.NamespaceTransformGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(307, 68)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 0
        Me.OK.Text = "Close"
        Me.OK.UseVisualStyleBackColor = True
        '
        'NamespaceTransform
        '
        Me.NamespaceTransform.Location = New System.Drawing.Point(237, 18)
        Me.NamespaceTransform.Name = "NamespaceTransform"
        Me.NamespaceTransform.Size = New System.Drawing.Size(75, 23)
        Me.NamespaceTransform.TabIndex = 58
        Me.NamespaceTransform.Text = "Change"
        Me.NamespaceTransform.UseVisualStyleBackColor = True
        '
        'NamespaceTransformGroup
        '
        Me.NamespaceTransformGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NamespaceTransformGroup.Controls.Add(Me.NamespaceTransformLabel)
        Me.NamespaceTransformGroup.Controls.Add(Me.NamespaceTransform)
        Me.NamespaceTransformGroup.Controls.Add(Me.NamespaceTransformSelector)
        Me.NamespaceTransformGroup.Location = New System.Drawing.Point(12, 12)
        Me.NamespaceTransformGroup.Name = "NamespaceTransformGroup"
        Me.NamespaceTransformGroup.Size = New System.Drawing.Size(370, 50)
        Me.NamespaceTransformGroup.TabIndex = 59
        Me.NamespaceTransformGroup.TabStop = False
        Me.NamespaceTransformGroup.Text = "Namespace transform"
        '
        'NamespaceTransformLabel
        '
        Me.NamespaceTransformLabel.AutoSize = True
        Me.NamespaceTransformLabel.Location = New System.Drawing.Point(6, 23)
        Me.NamespaceTransformLabel.Name = "NamespaceTransformLabel"
        Me.NamespaceTransformLabel.Size = New System.Drawing.Size(44, 13)
        Me.NamespaceTransformLabel.TabIndex = 60
        Me.NamespaceTransformLabel.Text = "Change"
        '
        'NamespaceTransformSelector
        '
        Me.NamespaceTransformSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.NamespaceTransformSelector.FormattingEnabled = True
        Me.NamespaceTransformSelector.Items.AddRange(New Object() {"non-talk pages to talk pages", "talk pages to non-talk pages"})
        Me.NamespaceTransformSelector.Location = New System.Drawing.Point(56, 19)
        Me.NamespaceTransformSelector.Name = "NamespaceTransformSelector"
        Me.NamespaceTransformSelector.Size = New System.Drawing.Size(175, 21)
        Me.NamespaceTransformSelector.TabIndex = 59
        '
        'QueueActionsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 103)
        Me.Controls.Add(Me.NamespaceTransformGroup)
        Me.Controls.Add(Me.OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "QueueActionsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Queue actions"
        Me.NamespaceTransformGroup.ResumeLayout(False)
        Me.NamespaceTransformGroup.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents NamespaceTransform As System.Windows.Forms.Button
    Friend WithEvents NamespaceTransformGroup As System.Windows.Forms.GroupBox
    Friend WithEvents NamespaceTransformSelector As System.Windows.Forms.ComboBox
    Friend WithEvents NamespaceTransformLabel As System.Windows.Forms.Label
End Class
