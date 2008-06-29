<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginForm))
        Me.OK = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.Options = New System.Windows.Forms.GroupBox
        Me.Project = New System.Windows.Forms.ComboBox
        Me.ProjectLabel = New System.Windows.Forms.Label
        Me.UseIrc = New System.Windows.Forms.RadioButton
        Me.UseRecentchanges = New System.Windows.Forms.RadioButton
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UsernameLabel = New System.Windows.Forms.Label
        Me.Password = New System.Windows.Forms.TextBox
        Me.Username = New System.Windows.Forms.TextBox
        Me.TitleLabel = New System.Windows.Forms.Label
        Me.Credit = New System.Windows.Forms.LinkLabel
        Me.Version = New System.Windows.Forms.Label
        Me.Status = New System.Windows.Forms.Label
        Me.Progress = New System.Windows.Forms.ProgressBar
        Me.Options.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(174, 220)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 1
        Me.OK.Text = "Login"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(256, 220)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 2
        Me.Cancel.Text = "Exit"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'Options
        '
        Me.Options.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Options.Controls.Add(Me.Project)
        Me.Options.Controls.Add(Me.ProjectLabel)
        Me.Options.Controls.Add(Me.UseIrc)
        Me.Options.Controls.Add(Me.UseRecentchanges)
        Me.Options.Controls.Add(Me.PasswordLabel)
        Me.Options.Controls.Add(Me.UsernameLabel)
        Me.Options.Controls.Add(Me.Password)
        Me.Options.Controls.Add(Me.Username)
        Me.Options.Location = New System.Drawing.Point(10, 71)
        Me.Options.Name = "Options"
        Me.Options.Size = New System.Drawing.Size(321, 143)
        Me.Options.TabIndex = 0
        Me.Options.TabStop = False
        '
        'Project
        '
        Me.Project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Project.FormattingEnabled = True
        Me.Project.Location = New System.Drawing.Point(91, 16)
        Me.Project.Name = "Project"
        Me.Project.Size = New System.Drawing.Size(196, 21)
        Me.Project.TabIndex = 7
        Me.Project.TabStop = False
        '
        'ProjectLabel
        '
        Me.ProjectLabel.AutoSize = True
        Me.ProjectLabel.Location = New System.Drawing.Point(42, 19)
        Me.ProjectLabel.Name = "ProjectLabel"
        Me.ProjectLabel.Size = New System.Drawing.Size(43, 13)
        Me.ProjectLabel.TabIndex = 6
        Me.ProjectLabel.Text = "Project:"
        '
        'UseIrc
        '
        Me.UseIrc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UseIrc.Checked = True
        Me.UseIrc.Location = New System.Drawing.Point(32, 94)
        Me.UseIrc.Name = "UseIrc"
        Me.UseIrc.Size = New System.Drawing.Size(258, 20)
        Me.UseIrc.TabIndex = 4
        Me.UseIrc.TabStop = True
        Me.UseIrc.Text = "Use the IRC recent changes feed"
        Me.UseIrc.UseVisualStyleBackColor = True
        '
        'UseRecentchanges
        '
        Me.UseRecentchanges.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UseRecentchanges.Location = New System.Drawing.Point(32, 117)
        Me.UseRecentchanges.Name = "UseRecentchanges"
        Me.UseRecentchanges.Size = New System.Drawing.Size(258, 19)
        Me.UseRecentchanges.TabIndex = 5
        Me.UseRecentchanges.Text = "Use Special:Recentchanges"
        Me.UseRecentchanges.UseVisualStyleBackColor = True
        '
        'PasswordLabel
        '
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.Location = New System.Drawing.Point(29, 72)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(56, 13)
        Me.PasswordLabel.TabIndex = 2
        Me.PasswordLabel.Text = "Password:"
        '
        'UsernameLabel
        '
        Me.UsernameLabel.AutoSize = True
        Me.UsernameLabel.Location = New System.Drawing.Point(27, 46)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(58, 13)
        Me.UsernameLabel.TabIndex = 0
        Me.UsernameLabel.Text = "Username:"
        '
        'Password
        '
        Me.Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Password.Location = New System.Drawing.Point(91, 69)
        Me.Password.Name = "Password"
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Password.Size = New System.Drawing.Size(196, 20)
        Me.Password.TabIndex = 3
        '
        'Username
        '
        Me.Username.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Username.Location = New System.Drawing.Point(91, 43)
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(196, 20)
        Me.Username.TabIndex = 1
        '
        'TitleLabel
        '
        Me.TitleLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TitleLabel.Font = New System.Drawing.Font("Tahoma", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(10, -4)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(321, 55)
        Me.TitleLabel.TabIndex = 5
        Me.TitleLabel.Text = "huggle"
        Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Credit
        '
        Me.Credit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Credit.AutoSize = True
        Me.Credit.LinkArea = New System.Windows.Forms.LinkArea(13, 5)
        Me.Credit.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.Credit.Location = New System.Drawing.Point(224, 51)
        Me.Credit.Name = "Credit"
        Me.Credit.Size = New System.Drawing.Size(107, 17)
        Me.Credit.TabIndex = 7
        Me.Credit.TabStop = True
        Me.Credit.Text = "Developed by Gurch"
        Me.Credit.UseCompatibleTextRendering = True
        '
        'Version
        '
        Me.Version.AutoSize = True
        Me.Version.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Version.Location = New System.Drawing.Point(9, 51)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(49, 13)
        Me.Version.TabIndex = 6
        Me.Version.Text = "Version"
        '
        'Status
        '
        Me.Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Status.Location = New System.Drawing.Point(11, 246)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(319, 31)
        Me.Status.TabIndex = 3
        Me.Status.Text = " "
        '
        'Progress
        '
        Me.Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Progress.Enabled = False
        Me.Progress.Location = New System.Drawing.Point(12, 261)
        Me.Progress.Maximum = 10
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(319, 19)
        Me.Progress.Step = 1
        Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.Progress.TabIndex = 4
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 296)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.Credit)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.Options)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Huggle"
        Me.Options.ResumeLayout(False)
        Me.Options.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Options As System.Windows.Forms.GroupBox
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents Password As System.Windows.Forms.TextBox
    Friend WithEvents Username As System.Windows.Forms.TextBox
    Friend WithEvents UseRecentchanges As System.Windows.Forms.RadioButton
    Friend WithEvents UseIrc As System.Windows.Forms.RadioButton
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents Credit As System.Windows.Forms.LinkLabel
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Status As System.Windows.Forms.Label
    Friend WithEvents Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents Project As System.Windows.Forms.ComboBox
    Friend WithEvents ProjectLabel As System.Windows.Forms.Label
End Class
