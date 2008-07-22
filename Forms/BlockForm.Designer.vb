<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BlockForm
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
        Me.BlockLog = New System.Windows.Forms.ListView
        Me.BlockEmail = New System.Windows.Forms.CheckBox
        Me.BlockCreation = New System.Windows.Forms.CheckBox
        Me.EnableAutoblock = New System.Windows.Forms.CheckBox
        Me.AnonOnly = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SharedIPWarning = New System.Windows.Forms.Label
        Me.Reason = New System.Windows.Forms.ComboBox
        Me.UserTalk = New System.Windows.Forms.Button
        Me.UserContribs = New System.Windows.Forms.Button
        Me.WarnLog = New System.Windows.Forms.ListView
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.BlockMessage = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Expiry = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(507, 380)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 18
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(426, 380)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 17
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'BlockLog
        '
        Me.BlockLog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BlockLog.FullRowSelect = True
        Me.BlockLog.GridLines = True
        Me.BlockLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.BlockLog.Location = New System.Drawing.Point(10, 139)
        Me.BlockLog.MultiSelect = False
        Me.BlockLog.Name = "BlockLog"
        Me.BlockLog.ShowGroups = False
        Me.BlockLog.Size = New System.Drawing.Size(572, 84)
        Me.BlockLog.TabIndex = 13
        Me.BlockLog.UseCompatibleStateImageBehavior = False
        Me.BlockLog.View = System.Windows.Forms.View.Details
        '
        'BlockEmail
        '
        Me.BlockEmail.AutoSize = True
        Me.BlockEmail.Location = New System.Drawing.Point(186, 96)
        Me.BlockEmail.Name = "BlockEmail"
        Me.BlockEmail.Size = New System.Drawing.Size(83, 17)
        Me.BlockEmail.TabIndex = 9
        Me.BlockEmail.Text = "Block e-mail"
        Me.BlockEmail.UseVisualStyleBackColor = True
        '
        'BlockCreation
        '
        Me.BlockCreation.AutoSize = True
        Me.BlockCreation.Location = New System.Drawing.Point(186, 73)
        Me.BlockCreation.Name = "BlockCreation"
        Me.BlockCreation.Size = New System.Drawing.Size(136, 17)
        Me.BlockCreation.TabIndex = 7
        Me.BlockCreation.Text = "Block account creation"
        Me.BlockCreation.UseVisualStyleBackColor = True
        '
        'EnableAutoblock
        '
        Me.EnableAutoblock.AutoSize = True
        Me.EnableAutoblock.Location = New System.Drawing.Point(11, 96)
        Me.EnableAutoblock.Name = "EnableAutoblock"
        Me.EnableAutoblock.Size = New System.Drawing.Size(114, 17)
        Me.EnableAutoblock.TabIndex = 8
        Me.EnableAutoblock.Text = "Enable autoblocks"
        Me.EnableAutoblock.UseVisualStyleBackColor = True
        '
        'AnonOnly
        '
        Me.AnonOnly.AutoSize = True
        Me.AnonOnly.Location = New System.Drawing.Point(11, 73)
        Me.AnonOnly.Name = "AnonOnly"
        Me.AnonOnly.Size = New System.Drawing.Size(160, 17)
        Me.AnonOnly.TabIndex = 6
        Me.AnonOnly.Text = "Block anonymous users only"
        Me.AnonOnly.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Expiry:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Reason:"
        '
        'SharedIPWarning
        '
        Me.SharedIPWarning.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SharedIPWarning.AutoSize = True
        Me.SharedIPWarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SharedIPWarning.Location = New System.Drawing.Point(7, 383)
        Me.SharedIPWarning.Name = "SharedIPWarning"
        Me.SharedIPWarning.Size = New System.Drawing.Size(409, 16)
        Me.SharedIPWarning.TabIndex = 16
        Me.SharedIPWarning.Text = "Note: 255.255.255.255 is tagged as a shared or dynamic IP address."
        Me.SharedIPWarning.Visible = False
        '
        'Reason
        '
        Me.Reason.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Reason.FormattingEnabled = True
        Me.Reason.Items.AddRange(New Object() {"[[Wikipedia:Vandalism|Vandalism]]", "[[Wikipedia:Username policy|Inappropriate username]]", "{{anonblock}}", "{{schoolblock}}"})
        Me.Reason.Location = New System.Drawing.Point(65, 12)
        Me.Reason.Name = "Reason"
        Me.Reason.Size = New System.Drawing.Size(517, 21)
        Me.Reason.TabIndex = 1
        '
        'UserTalk
        '
        Me.UserTalk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserTalk.Location = New System.Drawing.Point(426, 73)
        Me.UserTalk.Name = "UserTalk"
        Me.UserTalk.Size = New System.Drawing.Size(75, 23)
        Me.UserTalk.TabIndex = 10
        Me.UserTalk.Text = "Talk"
        Me.UserTalk.UseVisualStyleBackColor = True
        '
        'UserContribs
        '
        Me.UserContribs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserContribs.Location = New System.Drawing.Point(507, 73)
        Me.UserContribs.Name = "UserContribs"
        Me.UserContribs.Size = New System.Drawing.Size(75, 23)
        Me.UserContribs.TabIndex = 11
        Me.UserContribs.Text = "Contribs"
        Me.UserContribs.UseVisualStyleBackColor = True
        '
        'WarnLog
        '
        Me.WarnLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarnLog.FullRowSelect = True
        Me.WarnLog.GridLines = True
        Me.WarnLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.WarnLog.Location = New System.Drawing.Point(10, 250)
        Me.WarnLog.MultiSelect = False
        Me.WarnLog.Name = "WarnLog"
        Me.WarnLog.ShowGroups = False
        Me.WarnLog.Size = New System.Drawing.Size(572, 121)
        Me.WarnLog.TabIndex = 15
        Me.WarnLog.UseCompatibleStateImageBehavior = False
        Me.WarnLog.View = System.Windows.Forms.View.Details
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 123)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Blocks:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 234)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Warnings:"
        '
        'BlockMessage
        '
        Me.BlockMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.BlockMessage.FormattingEnabled = True
        Me.BlockMessage.Items.AddRange(New Object() {"(none)", "(standard block message)", "{{anonblock}}", "{{schoolblock}}"})
        Me.BlockMessage.Location = New System.Drawing.Point(373, 38)
        Me.BlockMessage.Name = "BlockMessage"
        Me.BlockMessage.Size = New System.Drawing.Size(209, 21)
        Me.BlockMessage.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(264, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Talk page message:"
        '
        'Expiry
        '
        Me.Expiry.FormattingEnabled = True
        Me.Expiry.Location = New System.Drawing.Point(65, 38)
        Me.Expiry.Name = "Expiry"
        Me.Expiry.Size = New System.Drawing.Size(178, 21)
        Me.Expiry.TabIndex = 19
        '
        'BlockForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 412)
        Me.Controls.Add(Me.Expiry)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.BlockMessage)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.WarnLog)
        Me.Controls.Add(Me.UserContribs)
        Me.Controls.Add(Me.UserTalk)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.SharedIPWarning)
        Me.Controls.Add(Me.BlockEmail)
        Me.Controls.Add(Me.BlockCreation)
        Me.Controls.Add(Me.EnableAutoblock)
        Me.Controls.Add(Me.AnonOnly)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BlockLog)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BlockForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Block user"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents BlockLog As System.Windows.Forms.ListView
    Friend WithEvents BlockEmail As System.Windows.Forms.CheckBox
    Friend WithEvents BlockCreation As System.Windows.Forms.CheckBox
    Friend WithEvents EnableAutoblock As System.Windows.Forms.CheckBox
    Friend WithEvents AnonOnly As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SharedIPWarning As System.Windows.Forms.Label
    Friend WithEvents Reason As System.Windows.Forms.ComboBox
    Friend WithEvents UserTalk As System.Windows.Forms.Button
    Friend WithEvents UserContribs As System.Windows.Forms.Button
    Friend WithEvents WarnLog As System.Windows.Forms.ListView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents BlockMessage As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Expiry As System.Windows.Forms.ComboBox
End Class
