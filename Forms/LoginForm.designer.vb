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
        Me.OK = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.ProxyGroup = New System.Windows.Forms.GroupBox
        Me.Proxy = New System.Windows.Forms.CheckBox
        Me.ProxyDomain = New System.Windows.Forms.TextBox
        Me.ProxyDomainLabel = New System.Windows.Forms.Label
        Me.ProxyPasswordLabel = New System.Windows.Forms.Label
        Me.ProxyUsernameLabel = New System.Windows.Forms.Label
        Me.ProxyPassword = New System.Windows.Forms.TextBox
        Me.ProxyUsername = New System.Windows.Forms.TextBox
        Me.ProxyPortLabel = New System.Windows.Forms.Label
        Me.ProxyAddressLabel = New System.Windows.Forms.Label
        Me.ProxyAddress = New System.Windows.Forms.TextBox
        Me.Project = New System.Windows.Forms.ComboBox
        Me.ProjectLabel = New System.Windows.Forms.Label
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UsernameLabel = New System.Windows.Forms.Label
        Me.Password = New System.Windows.Forms.TextBox
        Me.Username = New System.Windows.Forms.TextBox
        Me.Status = New System.Windows.Forms.Label
        Me.Progress = New System.Windows.Forms.ProgressBar
        Me.ShowProxySettings = New System.Windows.Forms.Button
        Me.HideProxySettings = New System.Windows.Forms.Button
        Me.Logo = New System.Windows.Forms.PictureBox
        Me.LanguageLabel = New System.Windows.Forms.Label
        Me.Language = New System.Windows.Forms.ComboBox
        Me.ProxyPort = New Huggle.IntegerTextBox
        Me.ProxyGroup.SuspendLayout()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(132, 353)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 7
        Me.OK.Text = "Login"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(213, 353)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 8
        Me.Cancel.Text = "Exit"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'ProxyGroup
        '
        Me.ProxyGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyGroup.Controls.Add(Me.ProxyPort)
        Me.ProxyGroup.Controls.Add(Me.Proxy)
        Me.ProxyGroup.Controls.Add(Me.ProxyDomain)
        Me.ProxyGroup.Controls.Add(Me.ProxyDomainLabel)
        Me.ProxyGroup.Controls.Add(Me.ProxyPasswordLabel)
        Me.ProxyGroup.Controls.Add(Me.ProxyUsernameLabel)
        Me.ProxyGroup.Controls.Add(Me.ProxyPassword)
        Me.ProxyGroup.Controls.Add(Me.ProxyUsername)
        Me.ProxyGroup.Controls.Add(Me.ProxyPortLabel)
        Me.ProxyGroup.Controls.Add(Me.ProxyAddressLabel)
        Me.ProxyGroup.Controls.Add(Me.ProxyAddress)
        Me.ProxyGroup.Location = New System.Drawing.Point(12, 196)
        Me.ProxyGroup.Name = "ProxyGroup"
        Me.ProxyGroup.Size = New System.Drawing.Size(276, 151)
        Me.ProxyGroup.TabIndex = 6
        Me.ProxyGroup.TabStop = False
        Me.ProxyGroup.Text = "Proxy settings"
        Me.ProxyGroup.Visible = False
        '
        'Proxy
        '
        Me.Proxy.AutoSize = True
        Me.Proxy.Location = New System.Drawing.Point(9, 23)
        Me.Proxy.Name = "Proxy"
        Me.Proxy.Size = New System.Drawing.Size(114, 17)
        Me.Proxy.TabIndex = 0
        Me.Proxy.Text = "Use a proxy server"
        Me.Proxy.UseVisualStyleBackColor = True
        '
        'ProxyDomain
        '
        Me.ProxyDomain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyDomain.Enabled = False
        Me.ProxyDomain.Location = New System.Drawing.Point(180, 72)
        Me.ProxyDomain.MaxLength = 255
        Me.ProxyDomain.Name = "ProxyDomain"
        Me.ProxyDomain.Size = New System.Drawing.Size(74, 20)
        Me.ProxyDomain.TabIndex = 6
        '
        'ProxyDomainLabel
        '
        Me.ProxyDomainLabel.AutoSize = True
        Me.ProxyDomainLabel.Enabled = False
        Me.ProxyDomainLabel.Location = New System.Drawing.Point(130, 75)
        Me.ProxyDomainLabel.Name = "ProxyDomainLabel"
        Me.ProxyDomainLabel.Size = New System.Drawing.Size(46, 13)
        Me.ProxyDomainLabel.TabIndex = 5
        Me.ProxyDomainLabel.Text = "Domain:"
        '
        'ProxyPasswordLabel
        '
        Me.ProxyPasswordLabel.AutoSize = True
        Me.ProxyPasswordLabel.Enabled = False
        Me.ProxyPasswordLabel.Location = New System.Drawing.Point(8, 127)
        Me.ProxyPasswordLabel.Name = "ProxyPasswordLabel"
        Me.ProxyPasswordLabel.Size = New System.Drawing.Size(56, 13)
        Me.ProxyPasswordLabel.TabIndex = 9
        Me.ProxyPasswordLabel.Text = "Password:"
        '
        'ProxyUsernameLabel
        '
        Me.ProxyUsernameLabel.AutoSize = True
        Me.ProxyUsernameLabel.Enabled = False
        Me.ProxyUsernameLabel.Location = New System.Drawing.Point(6, 101)
        Me.ProxyUsernameLabel.Name = "ProxyUsernameLabel"
        Me.ProxyUsernameLabel.Size = New System.Drawing.Size(58, 13)
        Me.ProxyUsernameLabel.TabIndex = 7
        Me.ProxyUsernameLabel.Text = "Username:"
        '
        'ProxyPassword
        '
        Me.ProxyPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyPassword.Enabled = False
        Me.ProxyPassword.Location = New System.Drawing.Point(68, 124)
        Me.ProxyPassword.MaxLength = 255
        Me.ProxyPassword.Name = "ProxyPassword"
        Me.ProxyPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.ProxyPassword.Size = New System.Drawing.Size(186, 20)
        Me.ProxyPassword.TabIndex = 10
        '
        'ProxyUsername
        '
        Me.ProxyUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyUsername.Enabled = False
        Me.ProxyUsername.Location = New System.Drawing.Point(68, 98)
        Me.ProxyUsername.MaxLength = 255
        Me.ProxyUsername.Name = "ProxyUsername"
        Me.ProxyUsername.Size = New System.Drawing.Size(186, 20)
        Me.ProxyUsername.TabIndex = 8
        '
        'ProxyPortLabel
        '
        Me.ProxyPortLabel.AutoSize = True
        Me.ProxyPortLabel.Enabled = False
        Me.ProxyPortLabel.Location = New System.Drawing.Point(35, 75)
        Me.ProxyPortLabel.Name = "ProxyPortLabel"
        Me.ProxyPortLabel.Size = New System.Drawing.Size(29, 13)
        Me.ProxyPortLabel.TabIndex = 3
        Me.ProxyPortLabel.Text = "Port:"
        '
        'ProxyAddressLabel
        '
        Me.ProxyAddressLabel.AutoSize = True
        Me.ProxyAddressLabel.Enabled = False
        Me.ProxyAddressLabel.Location = New System.Drawing.Point(16, 49)
        Me.ProxyAddressLabel.Name = "ProxyAddressLabel"
        Me.ProxyAddressLabel.Size = New System.Drawing.Size(48, 13)
        Me.ProxyAddressLabel.TabIndex = 1
        Me.ProxyAddressLabel.Text = "Address:"
        '
        'ProxyAddress
        '
        Me.ProxyAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyAddress.Enabled = False
        Me.ProxyAddress.Location = New System.Drawing.Point(68, 46)
        Me.ProxyAddress.MaxLength = 255
        Me.ProxyAddress.Name = "ProxyAddress"
        Me.ProxyAddress.Size = New System.Drawing.Size(186, 20)
        Me.ProxyAddress.TabIndex = 2
        '
        'Project
        '
        Me.Project.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Project.FormattingEnabled = True
        Me.Project.Location = New System.Drawing.Point(80, 116)
        Me.Project.MaxDropDownItems = 20
        Me.Project.Name = "Project"
        Me.Project.Size = New System.Drawing.Size(144, 21)
        Me.Project.TabIndex = 1
        '
        'ProjectLabel
        '
        Me.ProjectLabel.AutoSize = True
        Me.ProjectLabel.Location = New System.Drawing.Point(33, 119)
        Me.ProjectLabel.Name = "ProjectLabel"
        Me.ProjectLabel.Size = New System.Drawing.Size(43, 13)
        Me.ProjectLabel.TabIndex = 0
        Me.ProjectLabel.Text = "Project:"
        '
        'PasswordLabel
        '
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.Location = New System.Drawing.Point(20, 172)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(56, 13)
        Me.PasswordLabel.TabIndex = 4
        Me.PasswordLabel.Text = "Password:"
        '
        'UsernameLabel
        '
        Me.UsernameLabel.AutoSize = True
        Me.UsernameLabel.Location = New System.Drawing.Point(18, 146)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(58, 13)
        Me.UsernameLabel.TabIndex = 2
        Me.UsernameLabel.Text = "Username:"
        '
        'Password
        '
        Me.Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Password.Location = New System.Drawing.Point(80, 169)
        Me.Password.MaxLength = 255
        Me.Password.Name = "Password"
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.Password.Size = New System.Drawing.Size(188, 20)
        Me.Password.TabIndex = 5
        '
        'Username
        '
        Me.Username.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Username.Location = New System.Drawing.Point(80, 143)
        Me.Username.MaxLength = 255
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(188, 20)
        Me.Username.TabIndex = 3
        '
        'Status
        '
        Me.Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Status.Location = New System.Drawing.Point(12, 378)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(276, 28)
        Me.Status.TabIndex = 11
        Me.Status.Text = " "
        '
        'Progress
        '
        Me.Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Progress.Enabled = False
        Me.Progress.Location = New System.Drawing.Point(12, 407)
        Me.Progress.Maximum = 10
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(276, 19)
        Me.Progress.Step = 1
        Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.Progress.TabIndex = 12
        '
        'ShowProxySettings
        '
        Me.ShowProxySettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ShowProxySettings.Location = New System.Drawing.Point(12, 353)
        Me.ShowProxySettings.Name = "ShowProxySettings"
        Me.ShowProxySettings.Size = New System.Drawing.Size(103, 23)
        Me.ShowProxySettings.TabIndex = 10
        Me.ShowProxySettings.Text = "Proxy settings >>"
        Me.ShowProxySettings.UseVisualStyleBackColor = True
        '
        'HideProxySettings
        '
        Me.HideProxySettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.HideProxySettings.Location = New System.Drawing.Point(12, 353)
        Me.HideProxySettings.Name = "HideProxySettings"
        Me.HideProxySettings.Size = New System.Drawing.Size(102, 23)
        Me.HideProxySettings.TabIndex = 9
        Me.HideProxySettings.Text = "<< Proxy settings"
        Me.HideProxySettings.UseVisualStyleBackColor = True
        Me.HideProxySettings.Visible = False
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
        Me.Logo.TabIndex = 14
        Me.Logo.TabStop = False
        '
        'LanguageLabel
        '
        Me.LanguageLabel.AutoSize = True
        Me.LanguageLabel.Location = New System.Drawing.Point(18, 92)
        Me.LanguageLabel.Name = "LanguageLabel"
        Me.LanguageLabel.Size = New System.Drawing.Size(58, 13)
        Me.LanguageLabel.TabIndex = 15
        Me.LanguageLabel.Text = "Language:"
        '
        'Language
        '
        Me.Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Language.FormattingEnabled = True
        Me.Language.Location = New System.Drawing.Point(80, 89)
        Me.Language.Name = "Language"
        Me.Language.Size = New System.Drawing.Size(108, 21)
        Me.Language.TabIndex = 16
        '
        'ProxyPort
        '
        Me.ProxyPort.Enabled = False
        Me.ProxyPort.Location = New System.Drawing.Point(68, 72)
        Me.ProxyPort.MaxLength = 5
        Me.ProxyPort.Name = "ProxyPort"
        Me.ProxyPort.Size = New System.Drawing.Size(55, 20)
        Me.ProxyPort.TabIndex = 4
        Me.ProxyPort.Text = "80"
        Me.ProxyPort.Value = 80
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 438)
        Me.Controls.Add(Me.Language)
        Me.Controls.Add(Me.LanguageLabel)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.ShowProxySettings)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.ProxyGroup)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.ProjectLabel)
        Me.Controls.Add(Me.Username)
        Me.Controls.Add(Me.Project)
        Me.Controls.Add(Me.Password)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.HideProxySettings)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Huggle"
        Me.ProxyGroup.ResumeLayout(False)
        Me.ProxyGroup.PerformLayout()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents ProxyGroup As System.Windows.Forms.GroupBox
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents Password As System.Windows.Forms.TextBox
    Friend WithEvents Username As System.Windows.Forms.TextBox
    Friend WithEvents Status As System.Windows.Forms.Label
    Friend WithEvents Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents Project As System.Windows.Forms.ComboBox
    Friend WithEvents ProjectLabel As System.Windows.Forms.Label
    Friend WithEvents ProxyPortLabel As System.Windows.Forms.Label
    Friend WithEvents ProxyAddressLabel As System.Windows.Forms.Label
    Friend WithEvents ProxyAddress As System.Windows.Forms.TextBox
    Friend WithEvents ProxyPasswordLabel As System.Windows.Forms.Label
    Friend WithEvents ProxyUsernameLabel As System.Windows.Forms.Label
    Friend WithEvents ProxyPassword As System.Windows.Forms.TextBox
    Friend WithEvents ProxyUsername As System.Windows.Forms.TextBox
    Friend WithEvents ProxyDomain As System.Windows.Forms.TextBox
    Friend WithEvents ProxyDomainLabel As System.Windows.Forms.Label
    Friend WithEvents ShowProxySettings As System.Windows.Forms.Button
    Friend WithEvents HideProxySettings As System.Windows.Forms.Button
    Friend WithEvents Proxy As System.Windows.Forms.CheckBox
    Friend WithEvents Logo As System.Windows.Forms.PictureBox
    Friend WithEvents ProxyPort As Huggle.IntegerTextBox
    Friend WithEvents LanguageLabel As System.Windows.Forms.Label
    Friend WithEvents Language As System.Windows.Forms.ComboBox
End Class
