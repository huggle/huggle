namespace huggle3
{
    partial class ConfigForm
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
            this.Cancel = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.listSize = new System.Windows.Forms.ListBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.cbRememberUsername = new System.Windows.Forms.CheckBox();
            this.cbRememberPassword = new System.Windows.Forms.CheckBox();
            this.cbAutoWhitelist = new System.Windows.Forms.CheckBox();
            this.cbOIB = new System.Windows.Forms.CheckBox();
            this.cbShowNewEdits = new System.Windows.Forms.CheckBox();
            this.cbDiffPreload = new System.Windows.Forms.CheckBox();
            this.cbIRC = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(628, 415);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(104, 23);
            this.Cancel.TabIndex = 0;
            this.Cancel.Text = "Close";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(510, 415);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(99, 23);
            this.bSave.TabIndex = 1;
            this.bSave.Text = "button1";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(20, 25);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(156, 372);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbIRC);
            this.groupBox1.Controls.Add(this.cbDiffPreload);
            this.groupBox1.Controls.Add(this.cbShowNewEdits);
            this.groupBox1.Controls.Add(this.cbOIB);
            this.groupBox1.Controls.Add(this.cbAutoWhitelist);
            this.groupBox1.Controls.Add(this.cbRememberPassword);
            this.groupBox1.Controls.Add(this.cbRememberUsername);
            this.groupBox1.Controls.Add(this.labelSize);
            this.groupBox1.Controls.Add(this.labelPort);
            this.groupBox1.Controls.Add(this.textPort);
            this.groupBox1.Controls.Add(this.listSize);
            this.groupBox1.Location = new System.Drawing.Point(223, 161);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(95, 82);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(379, 26);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(106, 114);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.groupBox7);
            this.groupBox9.Location = new System.Drawing.Point(370, 274);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(115, 99);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "groupBox9";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(548, 27);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(118, 113);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "groupBox5";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(223, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(95, 115);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(223, 274);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(106, 99);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(551, 274);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(115, 99);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "groupBox6";
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(6, 135);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(97, 111);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "groupBox7";
            // 
            // groupBox8
            // 
            this.groupBox8.Location = new System.Drawing.Point(551, 161);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(97, 82);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "groupBox8";
            // 
            // listSize
            // 
            this.listSize.FormattingEnabled = true;
            this.listSize.Location = new System.Drawing.Point(154, 268);
            this.listSize.Name = "listSize";
            this.listSize.Size = new System.Drawing.Size(87, 17);
            this.listSize.TabIndex = 0;
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(153, 240);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(87, 20);
            this.textPort.TabIndex = 1;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(16, 243);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(83, 13);
            this.labelPort.TabIndex = 2;
            this.labelPort.Text = "[[config-irc-port]]";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(16, 268);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(83, 13);
            this.labelSize.TabIndex = 3;
            this.labelSize.Text = "[[config-diffsize]]";
            // 
            // cbRememberUsername
            // 
            this.cbRememberUsername.AutoSize = true;
            this.cbRememberUsername.Location = new System.Drawing.Point(19, 19);
            this.cbRememberUsername.Name = "cbRememberUsername";
            this.cbRememberUsername.Size = new System.Drawing.Size(165, 17);
            this.cbRememberUsername.TabIndex = 4;
            this.cbRememberUsername.Text = "[[config-remember-username]]";
            this.cbRememberUsername.UseVisualStyleBackColor = true;
            // 
            // cbRememberPassword
            // 
            this.cbRememberPassword.AutoSize = true;
            this.cbRememberPassword.Location = new System.Drawing.Point(19, 42);
            this.cbRememberPassword.Name = "cbRememberPassword";
            this.cbRememberPassword.Size = new System.Drawing.Size(164, 17);
            this.cbRememberPassword.TabIndex = 5;
            this.cbRememberPassword.Text = "[[config-remember-password]]";
            this.cbRememberPassword.UseVisualStyleBackColor = true;
            // 
            // cbAutoWhitelist
            // 
            this.cbAutoWhitelist.AutoSize = true;
            this.cbAutoWhitelist.Location = new System.Drawing.Point(19, 65);
            this.cbAutoWhitelist.Name = "cbAutoWhitelist";
            this.cbAutoWhitelist.Size = new System.Drawing.Size(131, 17);
            this.cbAutoWhitelist.TabIndex = 6;
            this.cbAutoWhitelist.Text = "[[config-auto-whitelist]]";
            this.cbAutoWhitelist.UseVisualStyleBackColor = true;
            // 
            // cbOIB
            // 
            this.cbOIB.AutoSize = true;
            this.cbOIB.Location = new System.Drawing.Point(19, 88);
            this.cbOIB.Name = "cbOIB";
            this.cbOIB.Size = new System.Drawing.Size(130, 17);
            this.cbOIB.TabIndex = 7;
            this.cbOIB.Text = "[[config-openinbrows]]";
            this.cbOIB.UseVisualStyleBackColor = true;
            // 
            // cbShowNewEdits
            // 
            this.cbShowNewEdits.AutoSize = true;
            this.cbShowNewEdits.Location = new System.Drawing.Point(19, 111);
            this.cbShowNewEdits.Name = "cbShowNewEdits";
            this.cbShowNewEdits.Size = new System.Drawing.Size(38, 17);
            this.cbShowNewEdits.TabIndex = 8;
            this.cbShowNewEdits.Text = "[[]]";
            this.cbShowNewEdits.UseVisualStyleBackColor = true;
            // 
            // cbDiffPreload
            // 
            this.cbDiffPreload.AutoSize = true;
            this.cbDiffPreload.Location = new System.Drawing.Point(19, 134);
            this.cbDiffPreload.Name = "cbDiffPreload";
            this.cbDiffPreload.Size = new System.Drawing.Size(110, 17);
            this.cbDiffPreload.TabIndex = 9;
            this.cbDiffPreload.Text = "[[config-preloads]]";
            this.cbDiffPreload.UseVisualStyleBackColor = true;
            // 
            // cbIRC
            // 
            this.cbIRC.AutoSize = true;
            this.cbIRC.Location = new System.Drawing.Point(19, 157);
            this.cbIRC.Name = "cbIRC";
            this.cbIRC.Size = new System.Drawing.Size(98, 17);
            this.cbIRC.TabIndex = 10;
            this.cbIRC.Text = "[[config-useirc]]";
            this.cbIRC.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 450);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.groupBox2);
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox cbOIB;
        private System.Windows.Forms.CheckBox cbAutoWhitelist;
        private System.Windows.Forms.CheckBox cbRememberPassword;
        private System.Windows.Forms.CheckBox cbRememberUsername;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.ListBox listSize;
        private System.Windows.Forms.CheckBox cbShowNewEdits;
        private System.Windows.Forms.CheckBox cbDiffPreload;
        private System.Windows.Forms.CheckBox cbIRC;
    }
}

