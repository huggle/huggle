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
        Me.Credit = New System.Windows.Forms.LinkLabel
        Me.Version = New System.Windows.Forms.Label
        Me.Disclaimer = New System.Windows.Forms.LinkLabel
        Me.Icons = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(235, 183)
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
        Me.Logo.Size = New System.Drawing.Size(298, 50)
        Me.Logo.TabIndex = 0
        Me.Logo.Text = "huggle"
        Me.Logo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Credit
        '
        Me.Credit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Credit.AutoSize = True
        Me.Credit.LinkArea = New System.Windows.Forms.LinkArea(13, 5)
        Me.Credit.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Credit.Location = New System.Drawing.Point(203, 68)
        Me.Credit.Name = "Credit"
        Me.Credit.Size = New System.Drawing.Size(107, 17)
        Me.Credit.TabIndex = 2
        Me.Credit.TabStop = True
        Me.Credit.Text = "Developed by Gurch"
        Me.Credit.UseCompatibleTextRendering = True
        '
        'Version
        '
        Me.Version.AutoSize = True
        Me.Version.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Version.Location = New System.Drawing.Point(9, 68)
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
        Me.Disclaimer.Location = New System.Drawing.Point(12, 94)
        Me.Disclaimer.Name = "Disclaimer"
        Me.Disclaimer.Size = New System.Drawing.Size(298, 51)
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
        Me.Icons.Location = New System.Drawing.Point(12, 156)
        Me.Icons.Name = "Icons"
        Me.Icons.Size = New System.Drawing.Size(298, 22)
        Me.Icons.TabIndex = 4
        Me.Icons.TabStop = True
        Me.Icons.Text = "Contains images available under the terms of the GNU Lesser General Public Licens" & _
            "e; see details."
        Me.Icons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Icons.UseCompatibleTextRendering = True
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 218)
        Me.Controls.Add(Me.Credit)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Logo As System.Windows.Forms.Label
    Friend WithEvents Credit As System.Windows.Forms.LinkLabel
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Disclaimer As System.Windows.Forms.LinkLabel
    Friend WithEvents Icons As System.Windows.Forms.LinkLabel
End Class
