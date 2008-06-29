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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TagForm))
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Summary = New System.Windows.Forms.TextBox
        Me.TagSelector = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToSpeedy = New System.Windows.Forms.Button
        Me.ToProd = New System.Windows.Forms.Button
        Me.Explanation = New System.Windows.Forms.LinkLabel
        Me.InsertAtEnd = New System.Windows.Forms.CheckBox
        Me.TagText = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(378, 235)
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
        Me.OK.Location = New System.Drawing.Point(297, 235)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 8
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Tag(s):"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 186)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Summary: "
        '
        'Summary
        '
        Me.Summary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Summary.Location = New System.Drawing.Point(66, 183)
        Me.Summary.MaxLength = 250
        Me.Summary.Name = "Summary"
        Me.Summary.Size = New System.Drawing.Size(387, 20)
        Me.Summary.TabIndex = 6
        '
        'TagSelector
        '
        Me.TagSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TagSelector.FormattingEnabled = True
        Me.TagSelector.Location = New System.Drawing.Point(66, 47)
        Me.TagSelector.Name = "TagSelector"
        Me.TagSelector.Size = New System.Drawing.Size(387, 21)
        Me.TagSelector.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Add tag:"
        '
        'ToSpeedy
        '
        Me.ToSpeedy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ToSpeedy.Enabled = False
        Me.ToSpeedy.Location = New System.Drawing.Point(12, 235)
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
        Me.ToProd.Location = New System.Drawing.Point(93, 235)
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
        Me.Explanation.Size = New System.Drawing.Size(441, 35)
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
        Me.InsertAtEnd.Location = New System.Drawing.Point(66, 209)
        Me.InsertAtEnd.Name = "InsertAtEnd"
        Me.InsertAtEnd.Size = New System.Drawing.Size(160, 17)
        Me.InsertAtEnd.TabIndex = 7
        Me.InsertAtEnd.Text = "Insert at the end of the page"
        Me.InsertAtEnd.UseVisualStyleBackColor = True
        '
        'TagText
        '
        Me.TagText.AcceptsTab = True
        Me.TagText.DetectUrls = False
        Me.TagText.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TagText.Location = New System.Drawing.Point(66, 73)
        Me.TagText.Name = "TagText"
        Me.TagText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.TagText.Size = New System.Drawing.Size(387, 104)
        Me.TagText.TabIndex = 12
        Me.TagText.Text = ""
        '
        'TagForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 270)
        Me.Controls.Add(Me.TagText)
        Me.Controls.Add(Me.InsertAtEnd)
        Me.Controls.Add(Me.Explanation)
        Me.Controls.Add(Me.ToProd)
        Me.Controls.Add(Me.ToSpeedy)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TagSelector)
        Me.Controls.Add(Me.Summary)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TagForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Tag page"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Summary As System.Windows.Forms.TextBox
    Friend WithEvents TagSelector As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToSpeedy As System.Windows.Forms.Button
    Friend WithEvents ToProd As System.Windows.Forms.Button
    Friend WithEvents Explanation As System.Windows.Forms.LinkLabel
    Friend WithEvents InsertAtEnd As System.Windows.Forms.CheckBox
    Friend WithEvents TagText As System.Windows.Forms.RichTextBox
End Class
