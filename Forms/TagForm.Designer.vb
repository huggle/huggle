<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TagForm
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
        Me.TagsLabel = New System.Windows.Forms.Label
        Me.SummaryLabel = New System.Windows.Forms.Label
        Me.Summary = New System.Windows.Forms.TextBox
        Me.TagSelector = New System.Windows.Forms.ComboBox
        Me.TagSelectorLabel = New System.Windows.Forms.Label
        Me.ToSpeedy = New System.Windows.Forms.Button
        Me.ToProd = New System.Windows.Forms.Button
        Me.Explanation = New System.Windows.Forms.LinkLabel
        Me.InsertAtEnd = New System.Windows.Forms.CheckBox
        Me.Tags = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(299, 242)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 9
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(218, 242)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 8
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'TagsLabel
        '
        Me.TagsLabel.AutoSize = True
        Me.TagsLabel.Location = New System.Drawing.Point(24, 76)
        Me.TagsLabel.Name = "TagsLabel"
        Me.TagsLabel.Size = New System.Drawing.Size(40, 13)
        Me.TagsLabel.TabIndex = 3
        Me.TagsLabel.Text = "Tag(s):"
        '
        'SummaryLabel
        '
        Me.SummaryLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SummaryLabel.AutoSize = True
        Me.SummaryLabel.Location = New System.Drawing.Point(12, 193)
        Me.SummaryLabel.Name = "SummaryLabel"
        Me.SummaryLabel.Size = New System.Drawing.Size(56, 13)
        Me.SummaryLabel.TabIndex = 5
        Me.SummaryLabel.Text = "Summary: "
        '
        'Summary
        '
        Me.Summary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Summary.Location = New System.Drawing.Point(66, 190)
        Me.Summary.MaxLength = 250
        Me.Summary.Name = "Summary"
        Me.Summary.Size = New System.Drawing.Size(308, 20)
        Me.Summary.TabIndex = 6
        '
        'TagSelector
        '
        Me.TagSelector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TagSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TagSelector.FormattingEnabled = True
        Me.TagSelector.Location = New System.Drawing.Point(66, 47)
        Me.TagSelector.Name = "TagSelector"
        Me.TagSelector.Size = New System.Drawing.Size(307, 21)
        Me.TagSelector.TabIndex = 2
        '
        'TagSelectorLabel
        '
        Me.TagSelectorLabel.AutoSize = True
        Me.TagSelectorLabel.Location = New System.Drawing.Point(17, 50)
        Me.TagSelectorLabel.Name = "TagSelectorLabel"
        Me.TagSelectorLabel.Size = New System.Drawing.Size(47, 13)
        Me.TagSelectorLabel.TabIndex = 1
        Me.TagSelectorLabel.Text = "Add tag:"
        '
        'ToSpeedy
        '
        Me.ToSpeedy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ToSpeedy.Enabled = False
        Me.ToSpeedy.Location = New System.Drawing.Point(12, 242)
        Me.ToSpeedy.Name = "ToSpeedy"
        Me.ToSpeedy.Size = New System.Drawing.Size(75, 23)
        Me.ToSpeedy.TabIndex = 10
        Me.ToSpeedy.Text = "Speedy..."
        Me.ToSpeedy.UseVisualStyleBackColor = True
        '
        'ToProd
        '
        Me.ToProd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ToProd.Enabled = False
        Me.ToProd.Location = New System.Drawing.Point(93, 242)
        Me.ToProd.Name = "ToProd"
        Me.ToProd.Size = New System.Drawing.Size(75, 23)
        Me.ToProd.TabIndex = 11
        Me.ToProd.Text = "Prod..."
        Me.ToProd.UseVisualStyleBackColor = True
        '
        'Explanation
        '
        Me.Explanation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Explanation.LinkArea = New System.Windows.Forms.LinkArea(91, 22)
        Me.Explanation.Location = New System.Drawing.Point(12, 9)
        Me.Explanation.Name = "Explanation"
        Me.Explanation.Size = New System.Drawing.Size(362, 35)
        Me.Explanation.TabIndex = 0
        Me.Explanation.TabStop = True
        Me.Explanation.Text = "Tags will be inserted at the start or end of the page." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "For anything more advance" & _
            "d, please edit the page normally instead."
        Me.Explanation.UseCompatibleTextRendering = True
        '
        'InsertAtEnd
        '
        Me.InsertAtEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.InsertAtEnd.AutoSize = True
        Me.InsertAtEnd.Location = New System.Drawing.Point(66, 216)
        Me.InsertAtEnd.Name = "InsertAtEnd"
        Me.InsertAtEnd.Size = New System.Drawing.Size(160, 17)
        Me.InsertAtEnd.TabIndex = 7
        Me.InsertAtEnd.Text = "Insert at the end of the page"
        Me.InsertAtEnd.UseVisualStyleBackColor = True
        '
        'Tags
        '
        Me.Tags.AcceptsTab = True
        Me.Tags.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tags.DetectUrls = False
        Me.Tags.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tags.Location = New System.Drawing.Point(66, 73)
        Me.Tags.Name = "Tags"
        Me.Tags.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.Tags.Size = New System.Drawing.Size(307, 111)
        Me.Tags.TabIndex = 12
        Me.Tags.Text = ""
        '
        'TagForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(386, 277)
        Me.Controls.Add(Me.Tags)
        Me.Controls.Add(Me.InsertAtEnd)
        Me.Controls.Add(Me.Explanation)
        Me.Controls.Add(Me.ToProd)
        Me.Controls.Add(Me.ToSpeedy)
        Me.Controls.Add(Me.TagSelectorLabel)
        Me.Controls.Add(Me.TagSelector)
        Me.Controls.Add(Me.Summary)
        Me.Controls.Add(Me.SummaryLabel)
        Me.Controls.Add(Me.TagsLabel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(375, 214)
        Me.Name = "TagForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Tag page"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents TagsLabel As System.Windows.Forms.Label
    Friend WithEvents SummaryLabel As System.Windows.Forms.Label
    Friend WithEvents Summary As System.Windows.Forms.TextBox
    Friend WithEvents TagSelector As System.Windows.Forms.ComboBox
    Friend WithEvents TagSelectorLabel As System.Windows.Forms.Label
    Friend WithEvents ToSpeedy As System.Windows.Forms.Button
    Friend WithEvents ToProd As System.Windows.Forms.Button
    Friend WithEvents Explanation As System.Windows.Forms.LinkLabel
    Friend WithEvents InsertAtEnd As System.Windows.Forms.CheckBox
    Friend WithEvents Tags As System.Windows.Forms.RichTextBox
End Class
