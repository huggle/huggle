<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportForm
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
        Me.MessageLabel = New System.Windows.Forms.Label
        Me.Message = New System.Windows.Forms.TextBox
        Me.ReasonLabel = New System.Windows.Forms.Label
        Me.Reason = New System.Windows.Forms.ComboBox
        Me.WarnLogLabel = New System.Windows.Forms.Label
        Me.Throbber = New Huggle.Throbber
        Me.Status = New System.Windows.Forms.Label
        Me.Splitter = New System.Windows.Forms.SplitContainer
        Me.WarnLog = New System.Windows.Forms.ListView
        Me.ReportWarning3 = New System.Windows.Forms.PictureBox
        Me.ReportWarning2 = New System.Windows.Forms.Label
        Me.ReportWarning = New System.Windows.Forms.Label
        Me.Reversions = New System.Windows.Forms.ListBox
        Me.Splitter.Panel1.SuspendLayout()
        Me.Splitter.Panel2.SuspendLayout()
        Me.Splitter.SuspendLayout()
        CType(Me.ReportWarning3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(407, 250)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 7
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(326, 250)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 6
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'MessageLabel
        '
        Me.MessageLabel.AutoSize = True
        Me.MessageLabel.Location = New System.Drawing.Point(9, 42)
        Me.MessageLabel.Name = "MessageLabel"
        Me.MessageLabel.Size = New System.Drawing.Size(53, 13)
        Me.MessageLabel.TabIndex = 2
        Me.MessageLabel.Text = "Message:"
        '
        'Message
        '
        Me.Message.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Message.Location = New System.Drawing.Point(68, 39)
        Me.Message.Multiline = True
        Me.Message.Name = "Message"
        Me.Message.Size = New System.Drawing.Size(414, 67)
        Me.Message.TabIndex = 3
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
        'Reason
        '
        Me.Reason.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Reason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Reason.FormattingEnabled = True
        Me.Reason.Items.AddRange(New Object() {"Vandalism after final warning", "Inappropriate username", "Three-revert rule violation"})
        Me.Reason.Location = New System.Drawing.Point(68, 12)
        Me.Reason.Name = "Reason"
        Me.Reason.Size = New System.Drawing.Size(220, 21)
        Me.Reason.TabIndex = 1
        Me.Reason.TabStop = False
        '
        'WarnLogLabel
        '
        Me.WarnLogLabel.AutoSize = True
        Me.WarnLogLabel.Location = New System.Drawing.Point(3, 3)
        Me.WarnLogLabel.Name = "WarnLogLabel"
        Me.WarnLogLabel.Size = New System.Drawing.Size(112, 13)
        Me.WarnLogLabel.TabIndex = 4
        Me.WarnLogLabel.Text = "Warnings for this user:"
        '
        'Throbber
        '
        Me.Throbber.BackColor = System.Drawing.Color.Gainsboro
        Me.Throbber.Location = New System.Drawing.Point(3, 4)
        Me.Throbber.Name = "Throbber"
        Me.Throbber.Size = New System.Drawing.Size(53, 10)
        Me.Throbber.TabIndex = 8
        '
        'Status
        '
        Me.Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Status.Location = New System.Drawing.Point(62, 3)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(168, 13)
        Me.Status.TabIndex = 9
        Me.Status.Text = "Status"
        '
        'Splitter
        '
        Me.Splitter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Splitter.Location = New System.Drawing.Point(12, 112)
        Me.Splitter.Name = "Splitter"
        '
        'Splitter.Panel1
        '
        Me.Splitter.Panel1.Controls.Add(Me.WarnLog)
        Me.Splitter.Panel1.Controls.Add(Me.WarnLogLabel)
        '
        'Splitter.Panel2
        '
        Me.Splitter.Panel2.Controls.Add(Me.ReportWarning3)
        Me.Splitter.Panel2.Controls.Add(Me.ReportWarning2)
        Me.Splitter.Panel2.Controls.Add(Me.ReportWarning)
        Me.Splitter.Panel2.Controls.Add(Me.Reversions)
        Me.Splitter.Panel2.Controls.Add(Me.Throbber)
        Me.Splitter.Panel2.Controls.Add(Me.Status)
        Me.Splitter.Size = New System.Drawing.Size(470, 132)
        Me.Splitter.SplitterDistance = 223
        Me.Splitter.TabIndex = 10
        '
        'WarnLog
        '
        Me.WarnLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarnLog.FullRowSelect = True
        Me.WarnLog.GridLines = True
        Me.WarnLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.WarnLog.Location = New System.Drawing.Point(3, 19)
        Me.WarnLog.MultiSelect = False
        Me.WarnLog.Name = "WarnLog"
        Me.WarnLog.ShowGroups = False
        Me.WarnLog.Size = New System.Drawing.Size(217, 110)
        Me.WarnLog.TabIndex = 6
        Me.WarnLog.UseCompatibleStateImageBehavior = False
        Me.WarnLog.View = System.Windows.Forms.View.Details
        '
        'ReportWarning3
        '
        Me.ReportWarning3.ErrorImage = Nothing
        Me.ReportWarning3.Image = Global.Huggle.My.Resources.Resources.user_warn
        Me.ReportWarning3.InitialImage = Nothing
        Me.ReportWarning3.Location = New System.Drawing.Point(3, 83)
        Me.ReportWarning3.Name = "ReportWarning3"
        Me.ReportWarning3.Size = New System.Drawing.Size(36, 36)
        Me.ReportWarning3.TabIndex = 13
        Me.ReportWarning3.TabStop = False
        '
        'ReportWarning2
        '
        Me.ReportWarning2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReportWarning2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportWarning2.Location = New System.Drawing.Point(41, 98)
        Me.ReportWarning2.Name = "ReportWarning2"
        Me.ReportWarning2.Size = New System.Drawing.Size(198, 31)
        Me.ReportWarning2.TabIndex = 12
        Me.ReportWarning2.Text = "This is only a possible 3RR violation. Do not submit this report until you have r" & _
            "eviewed the above revisions to ensure that they are not reversions of vandalism." & _
            ""
        Me.ReportWarning2.Visible = False
        '
        'ReportWarning
        '
        Me.ReportWarning.AutoSize = True
        Me.ReportWarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportWarning.Location = New System.Drawing.Point(41, 80)
        Me.ReportWarning.Name = "ReportWarning"
        Me.ReportWarning.Size = New System.Drawing.Size(80, 13)
        Me.ReportWarning.TabIndex = 11
        Me.ReportWarning.Text = "IMPORTANT" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ReportWarning.Visible = False
        '
        'Reversions
        '
        Me.Reversions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Reversions.FormattingEnabled = True
        Me.Reversions.Location = New System.Drawing.Point(3, 19)
        Me.Reversions.Name = "Reversions"
        Me.Reversions.Size = New System.Drawing.Size(236, 56)
        Me.Reversions.TabIndex = 10
        '
        'ReportForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 285)
        Me.Controls.Add(Me.Splitter)
        Me.Controls.Add(Me.Message)
        Me.Controls.Add(Me.MessageLabel)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.ReasonLabel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ReportForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report user"
        Me.Splitter.Panel1.ResumeLayout(False)
        Me.Splitter.Panel1.PerformLayout()
        Me.Splitter.Panel2.ResumeLayout(False)
        Me.Splitter.Panel2.PerformLayout()
        Me.Splitter.ResumeLayout(False)
        CType(Me.ReportWarning3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents MessageLabel As System.Windows.Forms.Label
    Friend WithEvents Message As System.Windows.Forms.TextBox
    Friend WithEvents ReasonLabel As System.Windows.Forms.Label
    Friend WithEvents Reason As System.Windows.Forms.ComboBox
    Friend WithEvents WarnLogLabel As System.Windows.Forms.Label
    Friend WithEvents Throbber As Huggle.Throbber
    Friend WithEvents Status As System.Windows.Forms.Label
    Friend WithEvents Splitter As System.Windows.Forms.SplitContainer
    Friend WithEvents WarnLog As System.Windows.Forms.ListView
    Friend WithEvents Reversions As System.Windows.Forms.ListBox
    Friend WithEvents ReportWarning As System.Windows.Forms.Label
    Friend WithEvents ReportWarning2 As System.Windows.Forms.Label
    Friend WithEvents ReportWarning3 As System.Windows.Forms.PictureBox
End Class
