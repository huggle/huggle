<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartupForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartupForm))
        Me.OK = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.ConfigLink = New System.Windows.Forms.LinkLabel
        Me.DocsLink = New System.Windows.Forms.LinkLabel
        Me.Label2 = New System.Windows.Forms.LinkLabel
        Me.Label3 = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(498, 249)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(88, 23)
        Me.OK.TabIndex = 0
        Me.OK.Text = "Continue >"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(578, 27)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Please read the following carefully before using this application"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ConfigLink
        '
        Me.ConfigLink.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConfigLink.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.ConfigLink.LinkArea = New System.Windows.Forms.LinkArea(45, 27)
        Me.ConfigLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.ConfigLink.Location = New System.Drawing.Point(12, 212)
        Me.ConfigLink.Name = "ConfigLink"
        Me.ConfigLink.Size = New System.Drawing.Size(570, 34)
        Me.ConfigLink.TabIndex = 5
        Me.ConfigLink.TabStop = True
        Me.ConfigLink.Text = "If you have not used Huggle before, you must create a configuration page on Wikip" & _
            "edia; see the documentation for details."
        Me.ConfigLink.UseCompatibleTextRendering = True
        '
        'DocsLink
        '
        Me.DocsLink.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DocsLink.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.DocsLink.LinkArea = New System.Windows.Forms.LinkArea(42, 13)
        Me.DocsLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.DocsLink.Location = New System.Drawing.Point(12, 192)
        Me.DocsLink.Name = "DocsLink"
        Me.DocsLink.Size = New System.Drawing.Size(570, 20)
        Me.DocsLink.TabIndex = 4
        Me.DocsLink.TabStop = True
        Me.DocsLink.Text = "If you have not already done so, read the documentation thoroughly before using H" & _
            "uggle."
        Me.DocsLink.UseCompatibleTextRendering = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.LinkArea = New System.Windows.Forms.LinkArea(0, 0)
        Me.Label2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Label2.Location = New System.Drawing.Point(12, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(570, 88)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.LinkArea = New System.Windows.Forms.LinkArea(0, 0)
        Me.Label3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Label3.Location = New System.Drawing.Point(12, 141)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(570, 51)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Responsibility for edits rests with the owner of the account with which they are " & _
            "made." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Use of an automated tool is not an excuse."
        '
        'StartupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 284)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DocsLink)
        Me.Controls.Add(Me.ConfigLink)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "StartupForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Huggle"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ConfigLink As System.Windows.Forms.LinkLabel
    Friend WithEvents DocsLink As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label3 As System.Windows.Forms.LinkLabel
End Class
