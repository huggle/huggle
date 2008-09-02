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
        Me.TrrSearch = New System.Windows.Forms.Button
        Me.WarningsPanel = New System.Windows.Forms.Panel
        Me.WarnLog = New System.Windows.Forms.ListView
        Me.WarnLogLabel = New System.Windows.Forms.Label
        Me.TrrPanel = New System.Windows.Forms.Panel
        Me.Throbber = New Huggle.Throbber
        Me.ReportWarning3 = New System.Windows.Forms.PictureBox
        Me.ReportWarning2 = New System.Windows.Forms.Label
        Me.Status = New System.Windows.Forms.Label
        Me.ReportWarning = New System.Windows.Forms.Label
        Me.Base = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.AddBase = New System.Windows.Forms.Button
        Me.Add = New System.Windows.Forms.Button
        Me.RevertsList = New System.Windows.Forms.ListBox
        Me.RevertsListLabel = New System.Windows.Forms.Label
        Me.BaseLabel = New System.Windows.Forms.Label
        Me.WarningsPanel.SuspendLayout()
        Me.TrrPanel.SuspendLayout()
        CType(Me.ReportWarning3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(278, 366)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 6
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(197, 366)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 5
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
        Me.Message.Size = New System.Drawing.Size(285, 67)
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
        Me.Reason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Reason.FormattingEnabled = True
        Me.Reason.Location = New System.Drawing.Point(68, 12)
        Me.Reason.Name = "Reason"
        Me.Reason.Size = New System.Drawing.Size(197, 21)
        Me.Reason.TabIndex = 1
        Me.Reason.TabStop = False
        '
        'TrrSearch
        '
        Me.TrrSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrrSearch.Location = New System.Drawing.Point(263, 8)
        Me.TrrSearch.Name = "TrrSearch"
        Me.TrrSearch.Size = New System.Drawing.Size(75, 23)
        Me.TrrSearch.TabIndex = 1
        Me.TrrSearch.Text = "Search"
        Me.TrrSearch.UseVisualStyleBackColor = True
        '
        'WarningsPanel
        '
        Me.WarningsPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarningsPanel.Controls.Add(Me.WarnLog)
        Me.WarningsPanel.Controls.Add(Me.WarnLogLabel)
        Me.WarningsPanel.Location = New System.Drawing.Point(12, 112)
        Me.WarningsPanel.Name = "WarningsPanel"
        Me.WarningsPanel.Size = New System.Drawing.Size(341, 248)
        Me.WarningsPanel.TabIndex = 7
        '
        'WarnLog
        '
        Me.WarnLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarnLog.FullRowSelect = True
        Me.WarnLog.GridLines = True
        Me.WarnLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.WarnLog.Location = New System.Drawing.Point(0, 19)
        Me.WarnLog.MultiSelect = False
        Me.WarnLog.Name = "WarnLog"
        Me.WarnLog.ShowGroups = False
        Me.WarnLog.Size = New System.Drawing.Size(341, 351)
        Me.WarnLog.TabIndex = 1
        Me.WarnLog.UseCompatibleStateImageBehavior = False
        Me.WarnLog.View = System.Windows.Forms.View.Details
        '
        'WarnLogLabel
        '
        Me.WarnLogLabel.AutoSize = True
        Me.WarnLogLabel.Location = New System.Drawing.Point(3, 3)
        Me.WarnLogLabel.Name = "WarnLogLabel"
        Me.WarnLogLabel.Size = New System.Drawing.Size(112, 13)
        Me.WarnLogLabel.TabIndex = 0
        Me.WarnLogLabel.Text = "Warnings for this user:"
        '
        'TrrPanel
        '
        Me.TrrPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrrPanel.Controls.Add(Me.Throbber)
        Me.TrrPanel.Controls.Add(Me.ReportWarning3)
        Me.TrrPanel.Controls.Add(Me.ReportWarning2)
        Me.TrrPanel.Controls.Add(Me.Status)
        Me.TrrPanel.Controls.Add(Me.ReportWarning)
        Me.TrrPanel.Controls.Add(Me.Base)
        Me.TrrPanel.Controls.Add(Me.Label3)
        Me.TrrPanel.Controls.Add(Me.AddBase)
        Me.TrrPanel.Controls.Add(Me.Add)
        Me.TrrPanel.Controls.Add(Me.RevertsList)
        Me.TrrPanel.Controls.Add(Me.RevertsListLabel)
        Me.TrrPanel.Controls.Add(Me.BaseLabel)
        Me.TrrPanel.Controls.Add(Me.TrrSearch)
        Me.TrrPanel.Location = New System.Drawing.Point(12, 112)
        Me.TrrPanel.Name = "TrrPanel"
        Me.TrrPanel.Size = New System.Drawing.Size(341, 248)
        Me.TrrPanel.TabIndex = 4
        '
        'Throbber
        '
        Me.Throbber.BackColor = System.Drawing.Color.Gainsboro
        Me.Throbber.Location = New System.Drawing.Point(6, 44)
        Me.Throbber.Name = "Throbber"
        Me.Throbber.Size = New System.Drawing.Size(53, 10)
        Me.Throbber.TabIndex = 2
        '
        'ReportWarning3
        '
        Me.ReportWarning3.ErrorImage = Nothing
        Me.ReportWarning3.Image = Global.Huggle.My.Resources.Resources.user_warn
        Me.ReportWarning3.InitialImage = Nothing
        Me.ReportWarning3.Location = New System.Drawing.Point(3, 195)
        Me.ReportWarning3.Name = "ReportWarning3"
        Me.ReportWarning3.Size = New System.Drawing.Size(36, 36)
        Me.ReportWarning3.TabIndex = 19
        Me.ReportWarning3.TabStop = False
        Me.ReportWarning3.Visible = False
        '
        'ReportWarning2
        '
        Me.ReportWarning2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReportWarning2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportWarning2.Location = New System.Drawing.Point(38, 199)
        Me.ReportWarning2.Name = "ReportWarning2"
        Me.ReportWarning2.Size = New System.Drawing.Size(300, 205)
        Me.ReportWarning2.TabIndex = 11
        Me.ReportWarning2.Text = "This is only a possible 3RR violation. Do not submit this report until you have r" & _
            "eviewed the above revisions to ensure that they are not reversions of vandalism." & _
            ""
        Me.ReportWarning2.Visible = False
        '
        'Status
        '
        Me.Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Status.Location = New System.Drawing.Point(62, 42)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(276, 14)
        Me.Status.TabIndex = 3
        '
        'ReportWarning
        '
        Me.ReportWarning.AutoSize = True
        Me.ReportWarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportWarning.Location = New System.Drawing.Point(38, 181)
        Me.ReportWarning.Name = "ReportWarning"
        Me.ReportWarning.Size = New System.Drawing.Size(80, 13)
        Me.ReportWarning.TabIndex = 9
        Me.ReportWarning.Text = "IMPORTANT" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ReportWarning.Visible = False
        '
        'Base
        '
        Me.Base.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Base.Enabled = False
        Me.Base.Location = New System.Drawing.Point(108, 62)
        Me.Base.Name = "Base"
        Me.Base.Size = New System.Drawing.Size(149, 20)
        Me.Base.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(3, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(250, 30)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Click Search to find possible 3RR violations by this user, or enter a report manu" & _
            "ally:"
        '
        'AddBase
        '
        Me.AddBase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddBase.Location = New System.Drawing.Point(263, 60)
        Me.AddBase.Name = "AddBase"
        Me.AddBase.Size = New System.Drawing.Size(75, 23)
        Me.AddBase.TabIndex = 6
        Me.AddBase.Text = "Use Current"
        Me.AddBase.UseVisualStyleBackColor = True
        '
        'Add
        '
        Me.Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Add.Location = New System.Drawing.Point(263, 173)
        Me.Add.Name = "Add"
        Me.Add.Size = New System.Drawing.Size(75, 23)
        Me.Add.TabIndex = 10
        Me.Add.Text = "Add Current"
        Me.Add.UseVisualStyleBackColor = True
        '
        'RevertsList
        '
        Me.RevertsList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RevertsList.FormattingEnabled = True
        Me.RevertsList.Location = New System.Drawing.Point(3, 111)
        Me.RevertsList.Name = "RevertsList"
        Me.RevertsList.Size = New System.Drawing.Size(335, 56)
        Me.RevertsList.TabIndex = 8
        '
        'RevertsListLabel
        '
        Me.RevertsListLabel.AutoSize = True
        Me.RevertsListLabel.Location = New System.Drawing.Point(3, 95)
        Me.RevertsListLabel.Name = "RevertsListLabel"
        Me.RevertsListLabel.Size = New System.Drawing.Size(47, 13)
        Me.RevertsListLabel.TabIndex = 7
        Me.RevertsListLabel.Text = "Reverts:"
        '
        'BaseLabel
        '
        Me.BaseLabel.AutoSize = True
        Me.BaseLabel.Location = New System.Drawing.Point(3, 65)
        Me.BaseLabel.Name = "BaseLabel"
        Me.BaseLabel.Size = New System.Drawing.Size(99, 13)
        Me.BaseLabel.TabIndex = 4
        Me.BaseLabel.Text = "Version reverted to:"
        '
        'ReportForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(365, 401)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.TrrPanel)
        Me.Controls.Add(Me.Message)
        Me.Controls.Add(Me.MessageLabel)
        Me.Controls.Add(Me.Reason)
        Me.Controls.Add(Me.ReasonLabel)
        Me.Controls.Add(Me.WarningsPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ReportForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report user"
        Me.WarningsPanel.ResumeLayout(False)
        Me.WarningsPanel.PerformLayout()
        Me.TrrPanel.ResumeLayout(False)
        Me.TrrPanel.PerformLayout()
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
    Friend WithEvents TrrSearch As System.Windows.Forms.Button
    Friend WithEvents WarningsPanel As System.Windows.Forms.Panel
    Friend WithEvents TrrPanel As System.Windows.Forms.Panel
    Friend WithEvents RevertsListLabel As System.Windows.Forms.Label
    Friend WithEvents BaseLabel As System.Windows.Forms.Label
    Friend WithEvents RevertsList As System.Windows.Forms.ListBox
    Friend WithEvents WarnLog As System.Windows.Forms.ListView
    Friend WithEvents WarnLogLabel As System.Windows.Forms.Label
    Friend WithEvents AddBase As System.Windows.Forms.Button
    Friend WithEvents Add As System.Windows.Forms.Button
    Friend WithEvents ReportWarning3 As System.Windows.Forms.PictureBox
    Friend WithEvents Throbber As Huggle.Throbber
    Friend WithEvents Status As System.Windows.Forms.Label
    Friend WithEvents Base As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ReportWarning2 As System.Windows.Forms.Label
    Friend WithEvents ReportWarning As System.Windows.Forms.Label
End Class
