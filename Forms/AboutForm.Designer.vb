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
        Me.Disclaimer = New System.Windows.Forms.LinkLabel
        Me.Icons = New System.Windows.Forms.LinkLabel
        Me.ContributorsLabel = New System.Windows.Forms.Label
        Me.Contributor1 = New System.Windows.Forms.LinkLabel
        Me.Contributor7 = New System.Windows.Forms.LinkLabel
        Me.Contributor4 = New System.Windows.Forms.LinkLabel
        Me.Contributor5 = New System.Windows.Forms.LinkLabel
        Me.Contributor3 = New System.Windows.Forms.LinkLabel
        Me.Contributor2 = New System.Windows.Forms.LinkLabel
        Me.Contributor6 = New System.Windows.Forms.LinkLabel
        Me.Logo = New System.Windows.Forms.PictureBox
        Me.Contributors = New System.Windows.Forms.TableLayoutPanel
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Contributors.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(213, 264)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 4
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Disclaimer
        '
        Me.Disclaimer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Disclaimer.LinkArea = New System.Windows.Forms.LinkArea(142, 22)
        Me.Disclaimer.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Disclaimer.Location = New System.Drawing.Point(12, 172)
        Me.Disclaimer.Name = "Disclaimer"
        Me.Disclaimer.Size = New System.Drawing.Size(276, 51)
        Me.Disclaimer.TabIndex = 2
        Me.Disclaimer.TabStop = True
        Me.Disclaimer.Text = "Use of this application is subject to Wikipedia policies and guidelines. Responsi" & _
            "bility for edits rests with the owner of the account. Please read the documentat" & _
            "ion before using this application."
        Me.Disclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Disclaimer.UseCompatibleTextRendering = True
        '
        'Icons
        '
        Me.Icons.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Icons.LinkArea = New System.Windows.Forms.LinkArea(84, 11)
        Me.Icons.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Icons.Location = New System.Drawing.Point(12, 225)
        Me.Icons.Name = "Icons"
        Me.Icons.Size = New System.Drawing.Size(276, 36)
        Me.Icons.TabIndex = 3
        Me.Icons.TabStop = True
        Me.Icons.Text = "Contains images available under the terms of the GNU Lesser General Public Licens" & _
            "e; see details."
        Me.Icons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Icons.UseCompatibleTextRendering = True
        '
        'ContributorsLabel
        '
        Me.ContributorsLabel.AutoSize = True
        Me.ContributorsLabel.Location = New System.Drawing.Point(9, 90)
        Me.ContributorsLabel.Name = "ContributorsLabel"
        Me.ContributorsLabel.Size = New System.Drawing.Size(66, 13)
        Me.ContributorsLabel.TabIndex = 0
        Me.ContributorsLabel.Text = "Contributors:"
        '
        'Contributor1
        '
        Me.Contributor1.AutoSize = True
        Me.Contributor1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor1.Location = New System.Drawing.Point(3, 3)
        Me.Contributor1.Margin = New System.Windows.Forms.Padding(3)
        Me.Contributor1.Name = "Contributor1"
        Me.Contributor1.Size = New System.Drawing.Size(52, 13)
        Me.Contributor1.TabIndex = 0
        Me.Contributor1.TabStop = True
        Me.Contributor1.Tag = "http://en.wikipedia.org/wiki/User:Addshore"
        Me.Contributor1.Text = "Addshore"
        '
        'Contributor7
        '
        Me.Contributor7.AutoSize = True
        Me.Contributor7.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor7.Location = New System.Drawing.Point(106, 43)
        Me.Contributor7.Margin = New System.Windows.Forms.Padding(3)
        Me.Contributor7.Name = "Contributor7"
        Me.Contributor7.Size = New System.Drawing.Size(52, 13)
        Me.Contributor7.TabIndex = 6
        Me.Contributor7.TabStop = True
        Me.Contributor7.Tag = "http://en.wikipedia.org/wiki/User:Soxred93"
        Me.Contributor7.Text = "Soxred93"
        '
        'Contributor4
        '
        Me.Contributor4.AutoSize = True
        Me.Contributor4.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor4.Location = New System.Drawing.Point(3, 63)
        Me.Contributor4.Margin = New System.Windows.Forms.Padding(3)
        Me.Contributor4.Name = "Contributor4"
        Me.Contributor4.Size = New System.Drawing.Size(59, 13)
        Me.Contributor4.TabIndex = 3
        Me.Contributor4.TabStop = True
        Me.Contributor4.Tag = "http://en.wikipedia.org/wiki/User:CWii"
        Me.Contributor4.Text = "Compwhizii"
        '
        'Contributor5
        '
        Me.Contributor5.AutoSize = True
        Me.Contributor5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor5.Location = New System.Drawing.Point(106, 3)
        Me.Contributor5.Margin = New System.Windows.Forms.Padding(3)
        Me.Contributor5.Name = "Contributor5"
        Me.Contributor5.Size = New System.Drawing.Size(36, 13)
        Me.Contributor5.TabIndex = 4
        Me.Contributor5.TabStop = True
        Me.Contributor5.Tag = "http://en.wikipedia.org/wiki/User_talk:Gurch"
        Me.Contributor5.Text = "Gurch"
        '
        'Contributor3
        '
        Me.Contributor3.AutoSize = True
        Me.Contributor3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor3.Location = New System.Drawing.Point(3, 43)
        Me.Contributor3.Margin = New System.Windows.Forms.Padding(3)
        Me.Contributor3.Name = "Contributor3"
        Me.Contributor3.Size = New System.Drawing.Size(63, 13)
        Me.Contributor3.TabIndex = 2
        Me.Contributor3.TabStop = True
        Me.Contributor3.Tag = "http://en.wikipedia.org/wiki/User:Calvin 1998"
        Me.Contributor3.Text = "Calvin 1998"
        '
        'Contributor2
        '
        Me.Contributor2.AutoSize = True
        Me.Contributor2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor2.Location = New System.Drawing.Point(3, 23)
        Me.Contributor2.Margin = New System.Windows.Forms.Padding(3)
        Me.Contributor2.Name = "Contributor2"
        Me.Contributor2.Size = New System.Drawing.Size(52, 13)
        Me.Contributor2.TabIndex = 1
        Me.Contributor2.TabStop = True
        Me.Contributor2.Tag = "http://en.wikipedia.org/wiki/User:Bartledan"
        Me.Contributor2.Text = "Bartledan"
        '
        'Contributor6
        '
        Me.Contributor6.AutoSize = True
        Me.Contributor6.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor6.Location = New System.Drawing.Point(106, 23)
        Me.Contributor6.Margin = New System.Windows.Forms.Padding(3)
        Me.Contributor6.Name = "Contributor6"
        Me.Contributor6.Size = New System.Drawing.Size(38, 13)
        Me.Contributor6.TabIndex = 5
        Me.Contributor6.TabStop = True
        Me.Contributor6.Tag = "http://en.wikipedia.org/wiki/User:Reedy"
        Me.Contributor6.Text = "Reedy"
        '
        'Logo
        '
        Me.Logo.BackColor = System.Drawing.Color.Transparent
        Me.Logo.ErrorImage = Nothing
        Me.Logo.Image = Global.Huggle.My.Resources.Resources.huggle_logo
        Me.Logo.InitialImage = Nothing
        Me.Logo.Location = New System.Drawing.Point(0, 0)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(300, 80)
        Me.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Logo.TabIndex = 15
        Me.Logo.TabStop = False
        '
        'Contributors
        '
        Me.Contributors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Contributors.ColumnCount = 2
        Me.Contributors.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Contributors.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Contributors.Controls.Add(Me.Contributor1, 0, 0)
        Me.Contributors.Controls.Add(Me.Contributor2, 0, 1)
        Me.Contributors.Controls.Add(Me.Contributor3, 0, 2)
        Me.Contributors.Controls.Add(Me.Contributor4, 0, 3)
        Me.Contributors.Controls.Add(Me.Contributor5, 1, 0)
        Me.Contributors.Controls.Add(Me.Contributor7, 1, 2)
        Me.Contributors.Controls.Add(Me.Contributor6, 1, 1)
        Me.Contributors.Location = New System.Drawing.Point(81, 87)
        Me.Contributors.Name = "Contributors"
        Me.Contributors.RowCount = 4
        Me.Contributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.Contributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.Contributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.Contributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.Contributors.Size = New System.Drawing.Size(207, 80)
        Me.Contributors.TabIndex = 1
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 299)
        Me.Controls.Add(Me.Contributors)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.ContributorsLabel)
        Me.Controls.Add(Me.Disclaimer)
        Me.Controls.Add(Me.Icons)
        Me.Controls.Add(Me.OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Huggle"
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Contributors.ResumeLayout(False)
        Me.Contributors.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Disclaimer As System.Windows.Forms.LinkLabel
    Friend WithEvents Icons As System.Windows.Forms.LinkLabel
    Friend WithEvents ContributorsLabel As System.Windows.Forms.Label
    Friend WithEvents Contributor1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor7 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor4 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor5 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor3 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor6 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor2 As System.Windows.Forms.LinkLabel
    Friend WithEvents Logo As System.Windows.Forms.PictureBox
    Friend WithEvents Contributors As System.Windows.Forms.TableLayoutPanel
End Class
