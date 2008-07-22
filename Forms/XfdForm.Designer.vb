<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XfdForm
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
        Me.Reason = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Notify = New System.Windows.Forms.CheckBox
        Me.Category = New System.Windows.Forms.ComboBox
        Me.CategoryLabel = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.NominationType = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(333, 161)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 8
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(252, 161)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 7
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Reason
        '
        Me.Reason.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Reason.Location = New System.Drawing.Point(12, 44)
        Me.Reason.Multiline = True
        Me.Reason.Name = "Reason"
        Me.Reason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Reason.Size = New System.Drawing.Size(396, 83)
        Me.Reason.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Reason:"
        '
        'Notify
        '
        Me.Notify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Notify.AutoSize = True
        Me.Notify.Checked = True
        Me.Notify.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Notify.Location = New System.Drawing.Point(12, 137)
        Me.Notify.Name = "Notify"
        Me.Notify.Size = New System.Drawing.Size(89, 17)
        Me.Notify.TabIndex = 4
        Me.Notify.Text = "Notify creator"
        Me.Notify.UseVisualStyleBackColor = True
        '
        'Category
        '
        Me.Category.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Category.FormattingEnabled = True
        Me.Category.Items.AddRange(New Object() {"(Nominator unsure of category)", "Media and music", "Organisation, corporation, or product", "Biographical", "Society topics", "Web or internet", "Games or sports", "Technology and science", "Fiction and the arts", "Places and transportation", "Indiscernible or unclassifiable topic"})
        Me.Category.Location = New System.Drawing.Point(207, 133)
        Me.Category.MaxDropDownItems = 12
        Me.Category.Name = "Category"
        Me.Category.Size = New System.Drawing.Size(201, 21)
        Me.Category.TabIndex = 6
        '
        'CategoryLabel
        '
        Me.CategoryLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CategoryLabel.AutoSize = True
        Me.CategoryLabel.Location = New System.Drawing.Point(149, 137)
        Me.CategoryLabel.Name = "CategoryLabel"
        Me.CategoryLabel.Size = New System.Drawing.Size(52, 13)
        Me.CategoryLabel.TabIndex = 5
        Me.CategoryLabel.Text = "Category:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Nomination type:"
        '
        'NominationType
        '
        Me.NominationType.AutoSize = True
        Me.NominationType.Location = New System.Drawing.Point(93, 9)
        Me.NominationType.Name = "NominationType"
        Me.NominationType.Size = New System.Drawing.Size(36, 13)
        Me.NominationType.TabIndex = 1
        Me.NominationType.Text = "Article"
        '
        'XfdForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 196)
        Me.Controls.Add(Me.NominationType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CategoryLabel)
        Me.Controls.Add(Me.Category)
        Me.Controls.Add(Me.Notify)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "XfdForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Nominate for deletion"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Reason As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Notify As System.Windows.Forms.CheckBox
    Friend WithEvents Category As System.Windows.Forms.ComboBox
    Friend WithEvents CategoryLabel As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NominationType As System.Windows.Forms.Label
End Class
