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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Me.OK = New System.Windows.Forms.Button()
        Me.Disclaimer = New System.Windows.Forms.LinkLabel()
        Me.Icons = New System.Windows.Forms.LinkLabel()
        Me.NewContributorsLabel = New System.Windows.Forms.Label()
        Me.Contributor1 = New System.Windows.Forms.LinkLabel()
        Me.Contributor7 = New System.Windows.Forms.LinkLabel()
        Me.Contributor4 = New System.Windows.Forms.LinkLabel()
        Me.Contributor5 = New System.Windows.Forms.LinkLabel()
        Me.Contributor3 = New System.Windows.Forms.LinkLabel()
        Me.Contributor2 = New System.Windows.Forms.LinkLabel()
        Me.Contributor6 = New System.Windows.Forms.LinkLabel()
        Me.Logo = New System.Windows.Forms.PictureBox()
        Me.OldContributors = New System.Windows.Forms.TableLayoutPanel()
        Me.OldContributorsLabel = New System.Windows.Forms.Label()
        Me.NewContributors = New System.Windows.Forms.TableLayoutPanel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Contributor8 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel4 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel5 = New System.Windows.Forms.LinkLabel()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OldContributors.SuspendLayout()
        Me.NewContributors.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(213, 422)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 4
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Disclaimer
        '
        Me.Disclaimer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Disclaimer.LinkArea = New System.Windows.Forms.LinkArea(142, 22)
        Me.Disclaimer.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Disclaimer.Location = New System.Drawing.Point(12, 312)
        Me.Disclaimer.Name = "Disclaimer"
        Me.Disclaimer.Size = New System.Drawing.Size(276, 71)
        Me.Disclaimer.TabIndex = 2
        Me.Disclaimer.TabStop = True
        Me.Disclaimer.Text = resources.GetString("Disclaimer.Text")
        Me.Disclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Disclaimer.UseCompatibleTextRendering = True
        '
        'Icons
        '
        Me.Icons.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Icons.LinkArea = New System.Windows.Forms.LinkArea(84, 11)
        Me.Icons.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Icons.Location = New System.Drawing.Point(12, 374)
        Me.Icons.Name = "Icons"
        Me.Icons.Size = New System.Drawing.Size(276, 45)
        Me.Icons.TabIndex = 3
        Me.Icons.TabStop = True
        Me.Icons.Text = "Contains images available under the terms of the GNU Lesser General Public Licens" & _
            "e; see details."
        Me.Icons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Icons.UseCompatibleTextRendering = True
        '
        'NewContributorsLabel
        '
        Me.NewContributorsLabel.Location = New System.Drawing.Point(3, 104)
        Me.NewContributorsLabel.Name = "NewContributorsLabel"
        Me.NewContributorsLabel.Size = New System.Drawing.Size(64, 13)
        Me.NewContributorsLabel.TabIndex = 0
        Me.NewContributorsLabel.Text = "Contributors:"
        Me.NewContributorsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Contributor7.Location = New System.Drawing.Point(98, 45)
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
        Me.Contributor4.Location = New System.Drawing.Point(3, 45)
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
        Me.Contributor5.Location = New System.Drawing.Point(98, 3)
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
        Me.Contributor3.Location = New System.Drawing.Point(3, 24)
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
        Me.Contributor2.Location = New System.Drawing.Point(3, 3)
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
        Me.Contributor6.Location = New System.Drawing.Point(98, 24)
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
        Me.Logo.Size = New System.Drawing.Size(320, 80)
        Me.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Logo.TabIndex = 15
        Me.Logo.TabStop = False
        '
        'OldContributors
        '
        Me.OldContributors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OldContributors.ColumnCount = 2
        Me.OldContributors.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.OldContributors.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.OldContributors.Controls.Add(Me.Contributor5, 1, 0)
        Me.OldContributors.Controls.Add(Me.Contributor7, 1, 2)
        Me.OldContributors.Controls.Add(Me.Contributor2, 0, 0)
        Me.OldContributors.Controls.Add(Me.Contributor4, 0, 2)
        Me.OldContributors.Controls.Add(Me.Contributor3, 0, 1)
        Me.OldContributors.Controls.Add(Me.Contributor6, 1, 1)
        Me.OldContributors.Location = New System.Drawing.Point(108, 221)
        Me.OldContributors.Name = "OldContributors"
        Me.OldContributors.RowCount = 3
        Me.OldContributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.OldContributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.OldContributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18.0!))
        Me.OldContributors.Size = New System.Drawing.Size(190, 67)
        Me.OldContributors.TabIndex = 1
        '
        'OldContributorsLabel
        '
        Me.OldContributorsLabel.AutoSize = True
        Me.OldContributorsLabel.Location = New System.Drawing.Point(3, 224)
        Me.OldContributorsLabel.Name = "OldContributorsLabel"
        Me.OldContributorsLabel.Size = New System.Drawing.Size(102, 13)
        Me.OldContributorsLabel.TabIndex = 18
        Me.OldContributorsLabel.Text = "Developers (former):"
        '
        'NewContributors
        '
        Me.NewContributors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NewContributors.ColumnCount = 2
        Me.NewContributors.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.21053!))
        Me.NewContributors.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.78947!))
        Me.NewContributors.Controls.Add(Me.Contributor8, 1, 0)
        Me.NewContributors.Controls.Add(Me.Contributor1, 0, 0)
        Me.NewContributors.Controls.Add(Me.LinkLabel2, 1, 1)
        Me.NewContributors.Controls.Add(Me.LinkLabel4, 0, 2)
        Me.NewContributors.Controls.Add(Me.LinkLabel3, 0, 3)
        Me.NewContributors.Controls.Add(Me.LinkLabel1, 1, 2)
        Me.NewContributors.Controls.Add(Me.LinkLabel5, 0, 1)
        Me.NewContributors.Location = New System.Drawing.Point(108, 101)
        Me.NewContributors.Name = "NewContributors"
        Me.NewContributors.RowCount = 4
        Me.NewContributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.NewContributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.NewContributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.NewContributors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.NewContributors.Size = New System.Drawing.Size(190, 81)
        Me.NewContributors.TabIndex = 19
        '
        'LinkLabel3
        '
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel3.Location = New System.Drawing.Point(3, 63)
        Me.LinkLabel3.Margin = New System.Windows.Forms.Padding(3)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(37, 13)
        Me.LinkLabel3.TabIndex = 25
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Tag = "http://en.wikipedia.org/wiki/User:Logan"
        Me.LinkLabel3.Text = "Logan"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.Location = New System.Drawing.Point(105, 43)
        Me.LinkLabel1.Margin = New System.Windows.Forms.Padding(3)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(79, 13)
        Me.LinkLabel1.TabIndex = 19
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Tag = "http://en.wikipedia.org/wiki/User:Thehelpfulone"
        Me.LinkLabel1.Text = "TheHelpfulOne"
        '
        'Contributor8
        '
        Me.Contributor8.AutoSize = True
        Me.Contributor8.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Contributor8.Location = New System.Drawing.Point(105, 3)
        Me.Contributor8.Margin = New System.Windows.Forms.Padding(3)
        Me.Contributor8.Name = "Contributor8"
        Me.Contributor8.Size = New System.Drawing.Size(32, 13)
        Me.Contributor8.TabIndex = 18
        Me.Contributor8.TabStop = True
        Me.Contributor8.Tag = "http://en.wikipedia.org/wiki/User:Petrb"
        Me.Contributor8.Text = "Petrb"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.Location = New System.Drawing.Point(105, 23)
        Me.LinkLabel2.Margin = New System.Windows.Forms.Padding(3)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(66, 13)
        Me.LinkLabel2.TabIndex = 20
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Tag = "http://en.wikipedia.org/wiki/User:Joe_Gazz84"
        Me.LinkLabel2.Text = "Joe_Gazz84"
        '
        'LinkLabel4
        '
        Me.LinkLabel4.AutoSize = True
        Me.LinkLabel4.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel4.Location = New System.Drawing.Point(3, 43)
        Me.LinkLabel4.Margin = New System.Windows.Forms.Padding(3)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Size = New System.Drawing.Size(86, 13)
        Me.LinkLabel4.TabIndex = 24
        Me.LinkLabel4.TabStop = True
        Me.LinkLabel4.Tag = "http://en.wikipedia.org/wiki/User:Matthewrbowker"
        Me.LinkLabel4.Text = "Matthewrbowker"
        '
        'LinkLabel5
        '
        Me.LinkLabel5.AutoSize = True
        Me.LinkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel5.Location = New System.Drawing.Point(3, 23)
        Me.LinkLabel5.Margin = New System.Windows.Forms.Padding(3)
        Me.LinkLabel5.Name = "LinkLabel5"
        Me.LinkLabel5.Size = New System.Drawing.Size(93, 13)
        Me.LinkLabel5.TabIndex = 26
        Me.LinkLabel5.TabStop = True
        Me.LinkLabel5.Tag = "http://en.wikipedia.org/wiki/User:123Hedgehog456"
        Me.LinkLabel5.Text = "123Hedgehog456"
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 457)
        Me.Controls.Add(Me.NewContributors)
        Me.Controls.Add(Me.OldContributorsLabel)
        Me.Controls.Add(Me.OldContributors)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.NewContributorsLabel)
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
        Me.OldContributors.ResumeLayout(False)
        Me.OldContributors.PerformLayout()
        Me.NewContributors.ResumeLayout(False)
        Me.NewContributors.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Disclaimer As System.Windows.Forms.LinkLabel
    Friend WithEvents Icons As System.Windows.Forms.LinkLabel
    Friend WithEvents NewContributorsLabel As System.Windows.Forms.Label
    Friend WithEvents Contributor1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor7 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor4 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor5 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor3 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor6 As System.Windows.Forms.LinkLabel
    Friend WithEvents Contributor2 As System.Windows.Forms.LinkLabel
    Friend WithEvents Logo As System.Windows.Forms.PictureBox
    Friend WithEvents OldContributors As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OldContributorsLabel As System.Windows.Forms.Label
    Friend WithEvents NewContributors As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Contributor8 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel4 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel5 As System.Windows.Forms.LinkLabel
End Class
