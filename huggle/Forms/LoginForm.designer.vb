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
        Me.OK = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.ProxyGroup = New System.Windows.Forms.GroupBox()
        Me.Proxy = New System.Windows.Forms.CheckBox()
        Me.ProxyDomain = New System.Windows.Forms.TextBox()
        Me.ProxyDomainLabel = New System.Windows.Forms.Label()
        Me.ProxyPasswordLabel = New System.Windows.Forms.Label()
        Me.ProxyUsernameLabel = New System.Windows.Forms.Label()
        Me.ProxyPassword = New System.Windows.Forms.TextBox()
        Me.ProxyUsername = New System.Windows.Forms.TextBox()
        Me.ProxyPortLabel = New System.Windows.Forms.Label()
        Me.ProxyAddressLabel = New System.Windows.Forms.Label()
        Me.ProxyAddress = New System.Windows.Forms.TextBox()
        Me.Project = New System.Windows.Forms.ComboBox()
        Me.ProjectLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.Password = New System.Windows.Forms.TextBox()
        Me.Username = New System.Windows.Forms.TextBox()
        Me.Status = New System.Windows.Forms.Label()
        Me.Progress = New System.Windows.Forms.ProgressBar()
        Me.ShowProxySettings = New System.Windows.Forms.Button()
        Me.HideProxySettings = New System.Windows.Forms.Button()
        Me.Logo = New System.Windows.Forms.PictureBox()
        Me.LanguageLabel = New System.Windows.Forms.Label()
        Me.Language = New System.Windows.Forms.ComboBox()
        Me.Translate = New System.Windows.Forms.Button()
        Me.ProxyPort = New Huggle.IntegerTextBox()
        Me.ProxyGroup.SuspendLayout()
        CType(Me.Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OK
        '
        Me.OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK.Enabled = False
        Me.OK.Location = New System.Drawing.Point(197, 242)
        Me.OK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(100, 28)
        Me.OK.TabIndex = 9
        Me.OK.Text = "Login"
        Me.OK.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel.Location = New System.Drawing.Point(305, 242)
        Me.Cancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(100, 28)
        Me.Cancel.TabIndex = 10
        Me.Cancel.Text = "Exit"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'ProxyGroup
        '
        Me.ProxyGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
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
        Me.ProxyGroup.Location = New System.Drawing.Point(16, 319)
        Me.ProxyGroup.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ProxyGroup.Name = "ProxyGroup"
        Me.ProxyGroup.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ProxyGroup.Size = New System.Drawing.Size(389, 188)
        Me.ProxyGroup.TabIndex = 12
        Me.ProxyGroup.TabStop = False
        Me.ProxyGroup.Text = "Proxy settings"
        Me.ProxyGroup.Visible = False
        '
        'Proxy
        '
        Me.Proxy.AutoSize = True
        Me.Proxy.Location = New System.Drawing.Point(12, 28)
        Me.Proxy.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Proxy.Name = "Proxy"
        Me.Proxy.Size = New System.Drawing.Size(149, 21)
        Me.Proxy.TabIndex = 0
        Me.Proxy.Text = "Use a proxy server"
        Me.Proxy.UseVisualStyleBackColor = True
        '
        'ProxyDomain
        '
        Me.ProxyDomain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyDomain.Enabled = False
        Me.ProxyDomain.Location = New System.Drawing.Point(240, 89)
        Me.ProxyDomain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ProxyDomain.MaxLength = 255
        Me.ProxyDomain.Name = "ProxyDomain"
        Me.ProxyDomain.Size = New System.Drawing.Size(119, 22)
        Me.ProxyDomain.TabIndex = 6
        '
        'ProxyDomainLabel
        '
        Me.ProxyDomainLabel.Enabled = False
        Me.ProxyDomainLabel.Location = New System.Drawing.Point(172, 92)
        Me.ProxyDomainLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ProxyDomainLabel.Name = "ProxyDomainLabel"
        Me.ProxyDomainLabel.Size = New System.Drawing.Size(63, 21)
        Me.ProxyDomainLabel.TabIndex = 5
        Me.ProxyDomainLabel.Text = "Domain:"
        Me.ProxyDomainLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ProxyPasswordLabel
        '
        Me.ProxyPasswordLabel.Enabled = False
        Me.ProxyPasswordLabel.Location = New System.Drawing.Point(4, 156)
        Me.ProxyPasswordLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ProxyPasswordLabel.Name = "ProxyPasswordLabel"
        Me.ProxyPasswordLabel.Size = New System.Drawing.Size(81, 21)
        Me.ProxyPasswordLabel.TabIndex = 9
        Me.ProxyPasswordLabel.Text = "Password:"
        Me.ProxyPasswordLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ProxyUsernameLabel
        '
        Me.ProxyUsernameLabel.Enabled = False
        Me.ProxyUsernameLabel.Location = New System.Drawing.Point(4, 124)
        Me.ProxyUsernameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ProxyUsernameLabel.Name = "ProxyUsernameLabel"
        Me.ProxyUsernameLabel.Size = New System.Drawing.Size(81, 21)
        Me.ProxyUsernameLabel.TabIndex = 7
        Me.ProxyUsernameLabel.Text = "Username:"
        Me.ProxyUsernameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ProxyPassword
        '
        Me.ProxyPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyPassword.Enabled = False
        Me.ProxyPassword.Location = New System.Drawing.Point(91, 153)
        Me.ProxyPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ProxyPassword.MaxLength = 255
        Me.ProxyPassword.Name = "ProxyPassword"
        Me.ProxyPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.ProxyPassword.Size = New System.Drawing.Size(268, 22)
        Me.ProxyPassword.TabIndex = 10
        '
        'ProxyUsername
        '
        Me.ProxyUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyUsername.Enabled = False
        Me.ProxyUsername.Location = New System.Drawing.Point(91, 121)
        Me.ProxyUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ProxyUsername.MaxLength = 255
        Me.ProxyUsername.Name = "ProxyUsername"
        Me.ProxyUsername.Size = New System.Drawing.Size(268, 22)
        Me.ProxyUsername.TabIndex = 8
        '
        'ProxyPortLabel
        '
        Me.ProxyPortLabel.Enabled = False
        Me.ProxyPortLabel.Location = New System.Drawing.Point(4, 92)
        Me.ProxyPortLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ProxyPortLabel.Name = "ProxyPortLabel"
        Me.ProxyPortLabel.Size = New System.Drawing.Size(81, 21)
        Me.ProxyPortLabel.TabIndex = 3
        Me.ProxyPortLabel.Text = "Port:"
        Me.ProxyPortLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ProxyAddressLabel
        '
        Me.ProxyAddressLabel.Enabled = False
        Me.ProxyAddressLabel.Location = New System.Drawing.Point(4, 60)
        Me.ProxyAddressLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ProxyAddressLabel.Name = "ProxyAddressLabel"
        Me.ProxyAddressLabel.Size = New System.Drawing.Size(81, 21)
        Me.ProxyAddressLabel.TabIndex = 1
        Me.ProxyAddressLabel.Text = "Address:"
        Me.ProxyAddressLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ProxyAddress
        '
        Me.ProxyAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProxyAddress.Enabled = False
        Me.ProxyAddress.Location = New System.Drawing.Point(91, 57)
        Me.ProxyAddress.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ProxyAddress.MaxLength = 255
        Me.ProxyAddress.Name = "ProxyAddress"
        Me.ProxyAddress.Size = New System.Drawing.Size(268, 22)
        Me.ProxyAddress.TabIndex = 2
        '
        'Project
        '
        Me.Project.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Project.FormattingEnabled = True
        Me.Project.Location = New System.Drawing.Point(107, 143)
        Me.Project.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Project.MaxDropDownItems = 20
        Me.Project.Name = "Project"
        Me.Project.Size = New System.Drawing.Size(180, 24)
        Me.Project.Sorted = True
        Me.Project.TabIndex = 4
        '
        'ProjectLabel
        '
        Me.ProjectLabel.Location = New System.Drawing.Point(0, 146)
        Me.ProjectLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ProjectLabel.Name = "ProjectLabel"
        Me.ProjectLabel.Size = New System.Drawing.Size(101, 21)
        Me.ProjectLabel.TabIndex = 3
        Me.ProjectLabel.Text = "Project:"
        Me.ProjectLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Location = New System.Drawing.Point(0, 212)
        Me.PasswordLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(101, 21)
        Me.PasswordLabel.TabIndex = 7
        Me.PasswordLabel.Text = "Password:"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Location = New System.Drawing.Point(0, 180)
        Me.UsernameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(101, 21)
        Me.UsernameLabel.TabIndex = 5
        Me.UsernameLabel.Text = "Username:"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Password
        '
        Me.Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Password.Location = New System.Drawing.Point(107, 208)
        Me.Password.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Password.MaxLength = 0
        Me.Password.Name = "Password"
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.Password.Size = New System.Drawing.Size(271, 22)
        Me.Password.TabIndex = 8
        '
        'Username
        '
        Me.Username.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Username.Location = New System.Drawing.Point(107, 176)
        Me.Username.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Username.MaxLength = 255
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(271, 22)
        Me.Username.TabIndex = 6
        '
        'Status
        '
        Me.Status.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Status.Location = New System.Drawing.Point(16, 274)
        Me.Status.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(389, 43)
        Me.Status.TabIndex = 13
        Me.Status.Text = " "
        '
        'Progress
        '
        Me.Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Progress.Enabled = False
        Me.Progress.Location = New System.Drawing.Point(16, 368)
        Me.Progress.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Progress.Maximum = 6
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(389, 23)
        Me.Progress.Step = 1
        Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.Progress.TabIndex = 2
        '
        'ShowProxySettings
        '
        Me.ShowProxySettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowProxySettings.Location = New System.Drawing.Point(16, 242)
        Me.ShowProxySettings.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ShowProxySettings.Name = "ShowProxySettings"
        Me.ShowProxySettings.Size = New System.Drawing.Size(173, 28)
        Me.ShowProxySettings.TabIndex = 11
        Me.ShowProxySettings.Text = "Proxy settings >>"
        Me.ShowProxySettings.UseVisualStyleBackColor = True
        '
        'HideProxySettings
        '
        Me.HideProxySettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HideProxySettings.Location = New System.Drawing.Point(16, 242)
        Me.HideProxySettings.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.HideProxySettings.Name = "HideProxySettings"
        Me.HideProxySettings.Size = New System.Drawing.Size(173, 28)
        Me.HideProxySettings.TabIndex = 13
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
        Me.Logo.Location = New System.Drawing.Point(46, -1)
        Me.Logo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(320, 80)
        Me.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Logo.TabIndex = 14
        Me.Logo.TabStop = False
        '
        'LanguageLabel
        '
        Me.LanguageLabel.Location = New System.Drawing.Point(0, 113)
        Me.LanguageLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LanguageLabel.Name = "LanguageLabel"
        Me.LanguageLabel.Size = New System.Drawing.Size(101, 21)
        Me.LanguageLabel.TabIndex = 0
        Me.LanguageLabel.Text = "Language:"
        Me.LanguageLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Language
        '
        Me.Language.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Language.FormattingEnabled = True
        Me.Language.Location = New System.Drawing.Point(107, 110)
        Me.Language.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Language.MaxDropDownItems = 20
        Me.Language.Name = "Language"
        Me.Language.Size = New System.Drawing.Size(180, 24)
        Me.Language.Sorted = True
        Me.Language.TabIndex = 1
        '
        'Translate
        '
        Me.Translate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Translate.Location = New System.Drawing.Point(305, 110)
        Me.Translate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Translate.Name = "Translate"
        Me.Translate.Size = New System.Drawing.Size(109, 28)
        Me.Translate.TabIndex = 2
        Me.Translate.Text = "Translate"
        Me.Translate.UseVisualStyleBackColor = True
        '
        'ProxyPort
        '
        Me.ProxyPort.Enabled = False
        Me.ProxyPort.Location = New System.Drawing.Point(91, 89)
        Me.ProxyPort.Margin = New System.Windows.Forms.Padding(4)
        Me.ProxyPort.MaxLength = 5
        Me.ProxyPort.Name = "ProxyPort"
        Me.ProxyPort.Size = New System.Drawing.Size(72, 22)
        Me.ProxyPort.TabIndex = 4
        Me.ProxyPort.Text = "80"
        Me.ProxyPort.Value = 80
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 406)
        Me.Controls.Add(Me.Progress)
        Me.Controls.Add(Me.ShowProxySettings)
        Me.Controls.Add(Me.Translate)
        Me.Controls.Add(Me.Language)
        Me.Controls.Add(Me.LanguageLabel)
        Me.Controls.Add(Me.Logo)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.ProjectLabel)
        Me.Controls.Add(Me.Username)
        Me.Controls.Add(Me.Project)
        Me.Controls.Add(Me.Password)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.HideProxySettings)
        Me.Controls.Add(Me.ProxyGroup)
        Me.Controls.Add(Me.Status)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
    Friend WithEvents Translate As System.Windows.Forms.Button
End Class
