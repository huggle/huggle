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
        Me.Email = New System.Windows.Forms.CheckBox
        Me.Creation = New System.Windows.Forms.CheckBox
        Me.Autoblock = New System.Windows.Forms.CheckBox
        Me.AnonOnly = New System.Windows.Forms.CheckBox
        Me.DurationLabel = New System.Windows.Forms.Label
        Me.ReasonLabel = New System.Windows.Forms.Label
        Me.SharedIPWarning = New System.Windows.Forms.Label
        Me.Reason = New System.Windows.Forms.ComboBox
        Me.UserTalk = New System.Windows.Forms.Button
        Me.UserContribs = New System.Windows.Forms.Button
        Me.BlockLogLabel = New System.Windows.Forms.Label
        Me.WarnLogLabel = New System.Windows.Forms.Label
        Me.Message = New System.Windows.Forms.ComboBox
        Me.MessageLabel = New System.Windows.Forms.Label
        Me.Duration = New System.Windows.Forms.ComboBox
        Me.BlockLog = New Huggle.UserLog
        Me.WarnLog = New Huggle.WarnLog
        Me.TalkEdit = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(507, 411)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 19
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Location = New System.Drawing.Point(426, 411)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 18
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Email
        '
        Me.Email.AutoSize = True
        Me.Email.Location = New System.Drawing.Point(187, 96)
        Me.Email.Name = "Email"
        Me.Email.Size = New System.Drawing.Size(83, 17)
        Me.Email.TabIndex = 9
        Me.Email.Text = "Block e-mail"
        Me.Email.UseVisualStyleBackColor = True
        '
        'Creation
        '
        Me.Creation.AutoSize = True
        Me.Creation.Location = New System.Drawing.Point(187, 73)
        Me.Creation.Name = "Creation"
        Me.Creation.Size = New System.Drawing.Size(136, 17)
        Me.Creation.TabIndex = 7
        Me.Creation.Text = "Block account creation"
        Me.Creation.UseVisualStyleBackColor = True
        '
        'Autoblock
        '
        Me.Autoblock.AutoSize = True
        Me.Autoblock.Location = New System.Drawing.Point(10, 96)
        Me.Autoblock.Name = "Autoblock"
        Me.Autoblock.Size = New System.Drawing.Size(114, 17)
        Me.Autoblock.TabIndex = 8
        Me.Autoblock.Text = "Enable autoblocks"
        Me.Autoblock.UseVisualStyleBackColor = True
        '
        'AnonOnly
        '
        Me.AnonOnly.AutoSize = True
        Me.AnonOnly.Location = New System.Drawing.Point(10, 73)
        Me.AnonOnly.Name = "AnonOnly"
        Me.AnonOnly.Size = New System.Drawing.Size(160, 17)
        Me.AnonOnly.TabIndex = 6
        Me.AnonOnly.Text = "Block anonymous users only"
        Me.AnonOnly.UseVisualStyleBackColor = True
        '
        'DurationLabel
        '
        Me.DurationLabel.AutoSize = True
        Me.DurationLabel.Location = New System.Drawing.Point(12, 41)
        Me.DurationLabel.Name = "DurationLabel"
        Me.DurationLabel.Size = New System.Drawing.Size(50, 13)
        Me.DurationLabel.TabIndex = 2
        Me.DurationLabel.Text = "Duration:"
        '
        'ReasonLabel
        '
        Me.ReasonLabel.AutoSize = True
        Me.ReasonLabel.Location = New System.Drawing.Point(15, 15)
        Me.ReasonLabel.Name = "ReasonLabel"
        Me.ReasonLabel.Size = New System.Drawing.Size(47, 13)
        Me.ReasonLabel.TabIndex = 0
        Me.ReasonLabel.Text = "Reason:"
        '
        'SharedIPWarning
        '
        Me.SharedIPWarning.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SharedIPWarning.AutoSize = True
        Me.SharedIPWarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SharedIPWarning.Location = New System.Drawing.Point(7, 414)
        Me.SharedIPWarning.Name = "SharedIPWarning"
        Me.SharedIPWarning.Size = New System.Drawing.Size(409, 16)
        Me.SharedIPWarning.TabIndex = 17
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
        Me.Reason.Size = New System.Drawing.Size(516, 21)
        Me.Reason.TabIndex = 1
        '
        'UserTalk
        '
        Me.UserTalk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserTalk.Location = New System.Drawing.Point(426, 73)
        Me.UserTalk.Name = "UserTalk"
        Me.UserTalk.Size = New System.Drawing.Size(75, 23)
        Me.UserTalk.TabIndex = 11
        Me.UserTalk.Text = "Talk"
        Me.UserTalk.UseVisualStyleBackColor = True
        '
        'UserContribs
        '
        Me.UserContribs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserContribs.Location = New System.Drawing.Point(507, 73)
        Me.UserContribs.Name = "UserContribs"
        Me.UserContribs.Size = New System.Drawing.Size(75, 23)
        Me.UserContribs.TabIndex = 12
        Me.UserContribs.Text = "Contribs"
        Me.UserContribs.UseVisualStyleBackColor = True
        '
        'BlockLogLabel
        '
        Me.BlockLogLabel.AutoSize = True
        Me.BlockLogLabel.Location = New System.Drawing.Point(7, 145)
        Me.BlockLogLabel.Name = "BlockLogLabel"
        Me.BlockLogLabel.Size = New System.Drawing.Size(54, 13)
        Me.BlockLogLabel.TabIndex = 13
        Me.BlockLogLabel.Text = "Block log:"
        '
        'WarnLogLabel
        '
        Me.WarnLogLabel.AutoSize = True
        Me.WarnLogLabel.Location = New System.Drawing.Point(7, 252)
        Me.WarnLogLabel.Name = "WarnLogLabel"
        Me.WarnLogLabel.Size = New System.Drawing.Size(55, 13)
        Me.WarnLogLabel.TabIndex = 15
        Me.WarnLogLabel.Text = "Warnings:"
        '
        'Message
        '
        Me.Message.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Message.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Message.FormattingEnabled = True
        Me.Message.Items.AddRange(New Object() {"(none)", "(standard block message)", "{{anonblock}}", "{{schoolblock}}"})
        Me.Message.Location = New System.Drawing.Point(373, 38)
        Me.Message.Name = "Message"
        Me.Message.Size = New System.Drawing.Size(208, 21)
        Me.Message.TabIndex = 5
        '
        'MessageLabel
        '
        Me.MessageLabel.AutoSize = True
        Me.MessageLabel.Location = New System.Drawing.Point(267, 41)
        Me.MessageLabel.Name = "MessageLabel"
        Me.MessageLabel.Size = New System.Drawing.Size(103, 13)
        Me.MessageLabel.TabIndex = 4
        Me.MessageLabel.Text = "Talk page message:"
        '
        'Duration
        '
        Me.Duration.FormattingEnabled = True
        Me.Duration.Location = New System.Drawing.Point(65, 38)
        Me.Duration.Name = "Duration"
        Me.Duration.Size = New System.Drawing.Size(178, 21)
        Me.Duration.TabIndex = 3
        '
        'BlockLog
        '
        Me.BlockLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BlockLog.FullRowSelect = True
        Me.BlockLog.GridLines = True
        Me.BlockLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.BlockLog.Location = New System.Drawing.Point(10, 161)
        Me.BlockLog.Name = "BlockLog"
        Me.BlockLog.Size = New System.Drawing.Size(571, 88)
        Me.BlockLog.TabIndex = 14
        Me.BlockLog.UseCompatibleStateImageBehavior = False
        Me.BlockLog.User = Nothing
        Me.BlockLog.View = System.Windows.Forms.View.Details
        '
        'WarnLog
        '
        Me.WarnLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarnLog.FullRowSelect = True
        Me.WarnLog.GridLines = True
        Me.WarnLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.WarnLog.Location = New System.Drawing.Point(10, 268)
        Me.WarnLog.Name = "WarnLog"
        Me.WarnLog.Size = New System.Drawing.Size(572, 137)
        Me.WarnLog.TabIndex = 16
        Me.WarnLog.UseCompatibleStateImageBehavior = False
        Me.WarnLog.User = Nothing
        Me.WarnLog.View = System.Windows.Forms.View.Details
        '
        'TalkEdit
        '
        Me.TalkEdit.AutoSize = True
        Me.TalkEdit.Location = New System.Drawing.Point(10, 119)
        Me.TalkEdit.Name = "TalkEdit"
        Me.TalkEdit.Size = New System.Drawing.Size(206, 17)
        Me.TalkEdit.TabIndex = 10
        Me.TalkEdit.Text = "Block editing of user talk page by user"
        Me.TalkEdit.UseVisualStyleBackColor = True
        '
        'BlockForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 443)
        Me.Controls.Add(Me.WarnLog)
        Me.Controls.Add(Me.BlockLog)
        Me.Controls.Add(Me.Duration)
        Me.Controls.Add(Me.MessageLabel)
        Me.Controls.Add(Me.Message)
        Me.Controls.Add(Me.WarnLogLabel)
        Me.Controls.Add(Me.BlockLogLabel)
        Me.Controls.Add(Me.UserContribs)
        Me.Controls.Add(Me.UserTalk)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.SharedIPWarning)
        Me.Controls.Add(Me.Email)
        Me.Controls.Add(Me.TalkEdit)
        Me.Controls.Add(Me.Creation)
        Me.Controls.Add(Me.Autoblock)
        Me.Controls.Add(Me.AnonOnly)
        Me.Controls.Add(Me.DurationLabel)
        Me.Controls.Add(Me.ReasonLabel)
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
    Friend WithEvents Email As System.Windows.Forms.CheckBox
    Friend WithEvents Creation As System.Windows.Forms.CheckBox
    Friend WithEvents Autoblock As System.Windows.Forms.CheckBox
    Friend WithEvents AnonOnly As System.Windows.Forms.CheckBox
    Friend WithEvents DurationLabel As System.Windows.Forms.Label
    Friend WithEvents ReasonLabel As System.Windows.Forms.Label
    Friend WithEvents SharedIPWarning As System.Windows.Forms.Label
    Friend WithEvents Reason As System.Windows.Forms.ComboBox
    Friend WithEvents UserTalk As System.Windows.Forms.Button
    Friend WithEvents UserContribs As System.Windows.Forms.Button
    Friend WithEvents BlockLogLabel As System.Windows.Forms.Label
    Friend WithEvents WarnLogLabel As System.Windows.Forms.Label
    Friend WithEvents Message As System.Windows.Forms.ComboBox
    Friend WithEvents MessageLabel As System.Windows.Forms.Label
    Friend WithEvents Duration As System.Windows.Forms.ComboBox
    Friend WithEvents BlockLog As Huggle.UserLog
    Friend WithEvents WarnLog As Huggle.WarnLog
    Friend WithEvents TalkEdit As System.Windows.Forms.CheckBox
End Class
