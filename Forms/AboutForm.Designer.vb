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
        Me.Contributor1 = New System.Windows.Forms.LinkLabel
        Me.Contributor7 = New System.Windows.Forms.LinkLabel
        Me.Contributor4 = New System.Windows.Forms.LinkLabel
        Me.Contributor5 = New System.Windows.Forms.LinkLabel
        Me.Contributor3 = New System.Windows.Forms.LinkLabel
        Me.ContributorsLayoutPanel = New System.Windows.Forms.FlowLayoutPanel
        Me.Contributor2 = New System.Windows.Forms.LinkLabel
        Me.Contributor6 = New System.Windows.Forms.LinkLabel
        Me.ContributorsLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(289, 211)
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
        Me.Logo.Size = New System.Drawing.Size(352, 50)
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
        Me.Disclaimer.Size = New System.Drawing.Size(352, 51)
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
        Me.Icons.Size = New System.Drawing.Size(352, 22)
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
        'Contributor1
        '
        Me.Contributor1.AutoSize = True
        Me.Contributor1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor1.Location = New System.Drawing.Point(6, 3)
        Me.Contributor1.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.Contributor1.Name = "Contributor1"
        Me.Contributor1.Size = New System.Drawing.Size(52, 13)
        Me.Contributor1.TabIndex = 8
        Me.Contributor1.TabStop = True
        Me.Contributor1.Tag = "http://en.wikipedia.org/wiki/User:Addshore"
        Me.Contributor1.Text = "Addshore"
        '
        'Contributor7
        '
        Me.Contributor7.AutoSize = True
        Me.Contributor7.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor7.Location = New System.Drawing.Point(104, 22)
        Me.Contributor7.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.Contributor7.Name = "Contributor7"
        Me.Contributor7.Size = New System.Drawing.Size(52, 13)
        Me.Contributor7.TabIndex = 8
        Me.Contributor7.TabStop = True
        Me.Contributor7.Tag = "http://en.wikipedia.org/wiki/User:Soxred93"
        Me.Contributor7.Text = "Soxred93"
        '
        'Contributor4
        '
        Me.Contributor4.AutoSize = True
        Me.Contributor4.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor4.Location = New System.Drawing.Point(209, 3)
        Me.Contributor4.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.Contributor4.Name = "Contributor4"
        Me.Contributor4.Size = New System.Drawing.Size(59, 13)
        Me.Contributor4.TabIndex = 8
        Me.Contributor4.TabStop = True
        Me.Contributor4.Tag = "http://en.wikipedia.org/wiki/User:CWii"
        Me.Contributor4.Text = "Compwhizii"
        '
        'Contributor5
        '
        Me.Contributor5.AutoSize = True
        Me.Contributor5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor5.Location = New System.Drawing.Point(6, 22)
        Me.Contributor5.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.Contributor5.Name = "Contributor5"
        Me.Contributor5.Size = New System.Drawing.Size(36, 13)
        Me.Contributor5.TabIndex = 8
        Me.Contributor5.TabStop = True
        Me.Contributor5.Tag = "http://en.wikipedia.org/wiki/User:Gurch"
        Me.Contributor5.Text = "Gurch"
        '
        'Contributor3
        '
        Me.Contributor3.AutoSize = True
        Me.Contributor3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor3.Location = New System.Drawing.Point(134, 3)
        Me.Contributor3.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.Contributor3.Name = "Contributor3"
        Me.Contributor3.Size = New System.Drawing.Size(63, 13)
        Me.Contributor3.TabIndex = 9
        Me.Contributor3.TabStop = True
        Me.Contributor3.Tag = "http://en.wikipedia.org/wiki/User:Calvin 1998"
        Me.Contributor3.Text = "Calvin 1998"
        '
        'ContributorsLayoutPanel
        '
        Me.ContributorsLayoutPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ContributorsLayoutPanel.Controls.Add(Me.Contributor1)
        Me.ContributorsLayoutPanel.Controls.Add(Me.Contributor2)
        Me.ContributorsLayoutPanel.Controls.Add(Me.Contributor3)
        Me.ContributorsLayoutPanel.Controls.Add(Me.Contributor4)
        Me.ContributorsLayoutPanel.Controls.Add(Me.Contributor5)
        Me.ContributorsLayoutPanel.Controls.Add(Me.Contributor6)
        Me.ContributorsLayoutPanel.Controls.Add(Me.Contributor7)
        Me.ContributorsLayoutPanel.Location = New System.Drawing.Point(75, 89)
        Me.ContributorsLayoutPanel.Name = "ContributorsLayoutPanel"
        Me.ContributorsLayoutPanel.Size = New System.Drawing.Size(289, 43)
        Me.ContributorsLayoutPanel.TabIndex = 10
        '
        'Contributor2
        '
        Me.Contributor2.AutoSize = True
        Me.Contributor2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor2.Location = New System.Drawing.Point(70, 3)
        Me.Contributor2.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.Contributor2.Name = "Contributor2"
        Me.Contributor2.Size = New System.Drawing.Size(52, 13)
        Me.Contributor2.TabIndex = 9
        Me.Contributor2.TabStop = True
        Me.Contributor2.Tag = "http://en.wikipedia.org/wiki/User:Bartledan"
        Me.Contributor2.Text = "Bartledan"
        '
        'Contributor6
        '
        Me.Contributor6.AutoSize = True
        Me.Contributor6.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor6.Location = New System.Drawing.Point(54, 22)
        Me.Contributor6.Margin = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.Contributor6.Name = "Contributor6"
        Me.Contributor6.Size = New System.Drawing.Size(38, 13)
        Me.Contributor6.TabIndex = 9
        Me.Contributor6.TabStop = True
        Me.Contributor6.Tag = "http://en.wikipedia.org/wiki/User:Reedy"
        Me.Contributor6.Text = "Reedy"
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(376, 246)
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
    Friend WithEvents Contributor1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor7 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor4 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor5 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor3 As System.Windows.Forms.LinkLabel
    Friend WithEvents ContributorsLayoutPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Contributor6 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor2 As System.Windows.Forms.LinkLabel
End Class
