<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutForm
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
        Me.Logo = New System.Windows.Forms.Label
        Me.Version = New System.Windows.Forms.Label
        Me.Disclaimer = New System.Windows.Forms.LinkLabel
        Me.Icons = New System.Windows.Forms.LinkLabel
        Me.ContributorsLabel = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel4 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel5 = New System.Windows.Forms.LinkLabel
        Me.ContributorsLayoutPanel = New System.Windows.Forms.FlowLayoutPanel
        Me.LinkLabel6 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel7 = New System.Windows.Forms.LinkLabel
        Me.ContributorsLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(288, 220)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 5
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Logo
        '
        Me.Logo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Logo.Font = New System.Drawing.Font("Tahoma", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Logo.Location = New System.Drawing.Point(12, 9)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(351, 50)
        Me.Logo.TabIndex = 0
        Me.Logo.Text = "huggle"
        Me.Logo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Version
        '
        Me.Version.AutoSize = True
        Me.Version.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Version.Location = New System.Drawing.Point(12, 64)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(49, 13)
        Me.Version.TabIndex = 1
        Me.Version.Text = "Version"
        '
        'Disclaimer
        '
        Me.Disclaimer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Disclaimer.LinkArea = New System.Windows.Forms.LinkArea(142, 22)
        Me.Disclaimer.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Disclaimer.Location = New System.Drawing.Point(12, 131)
        Me.Disclaimer.Name = "Disclaimer"
        Me.Disclaimer.Size = New System.Drawing.Size(351, 51)
        Me.Disclaimer.TabIndex = 3
        Me.Disclaimer.TabStop = True
        Me.Disclaimer.Text = "Use of this application is subject to Wikipedia policies and guidelines. Responsi" & _
            "bility for edits rests with the owner of the account. Please read the documentat" & _
            "ion before using this application."
        Me.Disclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Disclaimer.UseCompatibleTextRendering = True
        '
        'Icons
        '
        Me.Icons.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Icons.LinkArea = New System.Windows.Forms.LinkArea(84, 11)
        Me.Icons.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Icons.Location = New System.Drawing.Point(12, 188)
        Me.Icons.Name = "Icons"
        Me.Icons.Size = New System.Drawing.Size(351, 22)
        Me.Icons.TabIndex = 4
        Me.Icons.TabStop = True
        Me.Icons.Text = "Contains images available under the terms of the GNU Lesser General Public Licens" & _
            "e; see details."
        Me.Icons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Icons.UseCompatibleTextRendering = True
        '
        'ContributorsLabel
        '
        Me.ContributorsLabel.AutoSize = True
        Me.ContributorsLabel.Location = New System.Drawing.Point(12, 92)
        Me.ContributorsLabel.Name = "ContributorsLabel"
        Me.ContributorsLabel.Size = New System.Drawing.Size(66, 13)
        Me.ContributorsLabel.TabIndex = 7
        Me.ContributorsLabel.Text = "Contributors:"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.Location = New System.Drawing.Point(6, 3)
        Me.LinkLabel1.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(52, 13)
        Me.LinkLabel1.TabIndex = 8
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Tag = "Addshore"
        Me.LinkLabel1.Text = "Addshore"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.Location = New System.Drawing.Point(104, 22)
        Me.LinkLabel2.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(52, 13)
        Me.LinkLabel2.TabIndex = 8
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Tag = "Soxred93"
        Me.LinkLabel2.Text = "Soxred93"
        '
        'LinkLabel3
        '
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel3.Location = New System.Drawing.Point(209, 3)
        Me.LinkLabel3.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(59, 13)
        Me.LinkLabel3.TabIndex = 8
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Tag = "CWii"
        Me.LinkLabel3.Text = "Compwhizii"
        '
        'LinkLabel4
        '
        Me.LinkLabel4.AutoSize = True
        Me.LinkLabel4.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel4.Location = New System.Drawing.Point(6, 22)
        Me.LinkLabel4.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Size = New System.Drawing.Size(36, 13)
        Me.LinkLabel4.TabIndex = 8
        Me.LinkLabel4.TabStop = True
        Me.LinkLabel4.Tag = "Gurch"
        Me.LinkLabel4.Text = "Gurch"
        '
        'LinkLabel5
        '
        Me.LinkLabel5.AutoSize = True
        Me.LinkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel5.Location = New System.Drawing.Point(134, 3)
        Me.LinkLabel5.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.LinkLabel5.Name = "LinkLabel5"
        Me.LinkLabel5.Size = New System.Drawing.Size(63, 13)
        Me.LinkLabel5.TabIndex = 9
        Me.LinkLabel5.TabStop = True
        Me.LinkLabel5.Tag = "Calvin 1998"
        Me.LinkLabel5.Text = "Calvin 1998"
        '
        'ContributorsLayoutPanel
        '
        Me.ContributorsLayoutPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ContributorsLayoutPanel.Controls.Add(Me.LinkLabel1)
        Me.ContributorsLayoutPanel.Controls.Add(Me.LinkLabel7)
        Me.ContributorsLayoutPanel.Controls.Add(Me.LinkLabel5)
        Me.ContributorsLayoutPanel.Controls.Add(Me.LinkLabel3)
        Me.ContributorsLayoutPanel.Controls.Add(Me.LinkLabel4)
        Me.ContributorsLayoutPanel.Controls.Add(Me.LinkLabel6)
        Me.ContributorsLayoutPanel.Controls.Add(Me.LinkLabel2)
        Me.ContributorsLayoutPanel.Location = New System.Drawing.Point(75, 89)
        Me.ContributorsLayoutPanel.Name = "ContributorsLayoutPanel"
        Me.ContributorsLayoutPanel.Size = New System.Drawing.Size(288, 43)
        Me.ContributorsLayoutPanel.TabIndex = 10
        '
        'LinkLabel6
        '
        Me.LinkLabel6.AutoSize = True
        Me.LinkLabel6.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel6.Location = New System.Drawing.Point(54, 22)
        Me.LinkLabel6.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.LinkLabel6.Name = "LinkLabel6"
        Me.LinkLabel6.Size = New System.Drawing.Size(38, 13)
        Me.LinkLabel6.TabIndex = 9
        Me.LinkLabel6.TabStop = True
        Me.LinkLabel6.Tag = "Reedy"
        Me.LinkLabel6.Text = "Reedy"
        '
        'LinkLabel7
        '
        Me.LinkLabel7.AutoSize = True
        Me.LinkLabel7.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel7.Location = New System.Drawing.Point(70, 3)
        Me.LinkLabel7.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.LinkLabel7.Name = "LinkLabel7"
        Me.LinkLabel7.Size = New System.Drawing.Size(52, 13)
        Me.LinkLabel7.TabIndex = 9
        Me.LinkLabel7.TabStop = True
        Me.LinkLabel7.Tag = "Bartledan"
        Me.LinkLabel7.Text = "Bartledan"
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 255)
        Me.Controls.Add(Me.ContributorsLayoutPanel)
        Me.Controls.Add(Me.ContributorsLabel)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.Disclaimer)
        Me.Controls.Add(Me.Icons)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About huggle"
        Me.ContributorsLayoutPanel.ResumeLayout(False)
        Me.ContributorsLayoutPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Logo As System.Windows.Forms.Label
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Disclaimer As System.Windows.Forms.LinkLabel
    Friend WithEvents Icons As System.Windows.Forms.LinkLabel
    Friend WithEvents ContributorsLabel As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel4 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel5 As System.Windows.Forms.LinkLabel
    Friend WithEvents ContributorsLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents LinkLabel6 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel7 As System.Windows.Forms.LinkLabel
End Class
