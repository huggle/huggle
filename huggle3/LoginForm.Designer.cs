namespace huggle3
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.textName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btLogin = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.lbl_Project = new System.Windows.Forms.Label();
            this.lbl_Language = new System.Windows.Forms.Label();
            this.cmLanguage = new System.Windows.Forms.ComboBox();
            this.cmProject = new System.Windows.Forms.ComboBox();
            this.StatusBox = new System.Windows.Forms.Label();
            this.StatusBar = new System.Windows.Forms.ProgressBar();
            this.lProxy = new System.Windows.Forms.LinkLabel();
            this.lTranslate = new System.Windows.Forms.LinkLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.checkBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(120, 81);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(189, 20);
            this.textName.TabIndex = 1;
            this.textName.TextChanged += new System.EventHandler(this.controlChanged);
            this.textName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPressLogin);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::huggle3.Properties.Resources.hugglelogo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(325, 74);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(120, 107);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(188, 20);
            this.textPassword.TabIndex = 3;
            this.textPassword.TextChanged += new System.EventHandler(this.controlChanged);
            this.textPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPressLogin);
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(7, 110);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(83, 13);
            this.lPassword.TabIndex = 2;
            this.lPassword.Text = "[login-password]";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(7, 84);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(84, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "[login-username]";
            // 
            // btLogin
            // 
            this.btLogin.Enabled = false;
            this.btLogin.Location = new System.Drawing.Point(10, 267);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(100, 25);
            this.btLogin.TabIndex = 10;
            this.btLogin.Text = "[login-start]";
            this.btLogin.UseVisualStyleBackColor = true;
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // btExit
            // 
            this.btExit.Location = new System.Drawing.Point(10, 298);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(100, 25);
            this.btExit.TabIndex = 12;
            this.btExit.Text = "[exit]";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // lbl_Project
            // 
            this.lbl_Project.AutoSize = true;
            this.lbl_Project.Location = new System.Drawing.Point(7, 136);
            this.lbl_Project.Name = "lbl_Project";
            this.lbl_Project.Size = new System.Drawing.Size(70, 13);
            this.lbl_Project.TabIndex = 4;
            this.lbl_Project.Text = "[login-project]";
            // 
            // lbl_Language
            // 
            this.lbl_Language.AutoSize = true;
            this.lbl_Language.Location = new System.Drawing.Point(7, 163);
            this.lbl_Language.Name = "lbl_Language";
            this.lbl_Language.Size = new System.Drawing.Size(82, 13);
            this.lbl_Language.TabIndex = 6;
            this.lbl_Language.Text = "[login-language]";
            // 
            // cmLanguage
            // 
            this.cmLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmLanguage.FormattingEnabled = true;
            this.cmLanguage.Location = new System.Drawing.Point(120, 160);
            this.cmLanguage.Name = "cmLanguage";
            this.cmLanguage.Size = new System.Drawing.Size(188, 21);
            this.cmLanguage.TabIndex = 7;
            this.cmLanguage.SelectedIndexChanged += new System.EventHandler(this.cmLanguage_SelectedIndexChanged);
            this.cmLanguage.SelectedValueChanged += new System.EventHandler(this.controlChanged);
            // 
            // cmProject
            // 
            this.cmProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmProject.FormattingEnabled = true;
            this.cmProject.Location = new System.Drawing.Point(120, 133);
            this.cmProject.Name = "cmProject";
            this.cmProject.Size = new System.Drawing.Size(188, 21);
            this.cmProject.TabIndex = 5;
            this.cmProject.SelectedValueChanged += new System.EventHandler(this.controlChanged);
            // 
            // StatusBox
            // 
            this.StatusBox.AutoSize = true;
            this.StatusBox.Location = new System.Drawing.Point(8, 237);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(62, 13);
            this.StatusBox.TabIndex = 9;
            this.StatusBox.Text = "Status label";
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(11, 187);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(297, 24);
            this.StatusBar.TabIndex = 8;
            // 
            // lProxy
            // 
            this.lProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lProxy.AutoSize = true;
            this.lProxy.Location = new System.Drawing.Point(223, 273);
            this.lProxy.Name = "lProxy";
            this.lProxy.Size = new System.Drawing.Size(90, 13);
            this.lProxy.TabIndex = 13;
            this.lProxy.TabStop = true;
            this.lProxy.Text = "[login-proxygroup]";
            this.lProxy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lTranslate
            // 
            this.lTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lTranslate.AutoSize = true;
            this.lTranslate.Location = new System.Drawing.Point(223, 304);
            this.lTranslate.Name = "lTranslate";
            this.lTranslate.Size = new System.Drawing.Size(82, 13);
            this.lTranslate.TabIndex = 0;
            this.lTranslate.TabStop = true;
            this.lTranslate.Text = "[login-language]";
            this.lTranslate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(10, 217);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(44, 17);
            this.checkBox.TabIndex = 14;
            this.checkBox.Text = "[ssl]";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 331);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.lTranslate);
            this.Controls.Add(this.lProxy);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.cmProject);
            this.Controls.Add(this.cmLanguage);
            this.Controls.Add(this.lbl_Language);
            this.Controls.Add(this.lbl_Project);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btLogin);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Huggle";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btLogin;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Label lbl_Project;
        private System.Windows.Forms.Label lbl_Language;
        private System.Windows.Forms.ComboBox cmLanguage;
        private System.Windows.Forms.ComboBox cmProject;
        private System.Windows.Forms.LinkLabel lProxy;
        private System.Windows.Forms.LinkLabel lTranslate;
        public System.Windows.Forms.Label StatusBox;
        public System.Windows.Forms.ProgressBar StatusBar;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox checkBox;
    }
}