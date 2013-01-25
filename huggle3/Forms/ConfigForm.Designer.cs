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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbIRC = new System.Windows.Forms.CheckBox();
            this.cbDiffPreload = new System.Windows.Forms.CheckBox();
            this.cbShowNewEdits = new System.Windows.Forms.CheckBox();
            this.cbOIB = new System.Windows.Forms.CheckBox();
            this.cbAutoWhitelist = new System.Windows.Forms.CheckBox();
            this.cbRememberPassword = new System.Windows.Forms.CheckBox();
            this.cbRememberUsername = new System.Windows.Forms.CheckBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.textPort = new System.Windows.Forms.TextBox();
            this.listSize = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Cancel = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(789, 450);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox9);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox8);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox7);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(783, 384);
            this.splitContainer1.SplitterDistance = 135;
            this.splitContainer1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(135, 384);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(462, 189);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(109, 130);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "[[config-reverting]]";
            // 
            // groupBox9
            // 
            this.groupBox9.Location = new System.Drawing.Point(390, 68);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(89, 113);
            this.groupBox9.TabIndex = 17;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "[[config-admin]]";
            // 
            // groupBox8
            // 
            this.groupBox8.Location = new System.Drawing.Point(241, 208);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(100, 118);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "[[config-editor]]";
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(347, 208);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(98, 111);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "[[config-reporting]]";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(191, 208);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(37, 118);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "[[config-editing]]";
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(241, 70);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(87, 113);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "[[config-templates]]";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(347, 70);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(37, 113);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "[[config-keyboard]]";
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
            this.groupBox1.Location = new System.Drawing.Point(14, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 325);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[[config-general]]";
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
            // cbShowNewEdits
            // 
            this.cbShowNewEdits.AutoSize = true;
            this.cbShowNewEdits.Location = new System.Drawing.Point(19, 111);
            this.cbShowNewEdits.Name = "cbShowNewEdits";
            this.cbShowNewEdits.Size = new System.Drawing.Size(67, 17);
            this.cbShowNewEdits.TabIndex = 8;
            this.cbShowNewEdits.Text = "[[config]]";
            this.cbShowNewEdits.UseVisualStyleBackColor = true;
            // 
            // cbOIB
            // 
            this.cbOIB.AutoSize = true;
            this.cbOIB.Location = new System.Drawing.Point(19, 88);
            this.cbOIB.Name = "cbOIB";
            this.cbOIB.Size = new System.Drawing.Size(139, 17);
            this.cbOIB.TabIndex = 7;
            this.cbOIB.Text = "[[config-openinbrowser]]";
            this.cbOIB.UseVisualStyleBackColor = true;
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
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(16, 268);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(83, 13);
            this.labelSize.TabIndex = 3;
            this.labelSize.Text = "[[config-diffsize]]";
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
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(153, 240);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(87, 20);
            this.textPort.TabIndex = 1;
            // 
            // listSize
            // 
            this.listSize.FormattingEnabled = true;
            this.listSize.Location = new System.Drawing.Point(154, 268);
            this.listSize.Name = "listSize";
            this.listSize.Size = new System.Drawing.Size(87, 17);
            this.listSize.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(485, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(156, 84);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "[[config-interface]]";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel2.Controls.Add(this.Cancel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.bSave, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 393);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(783, 54);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(606, 3);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(104, 23);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "[[config-close]]";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.Location = new System.Drawing.Point(501, 3);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(99, 23);
            this.bSave.TabIndex = 2;
            this.bSave.Text = "[[config-save]]";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[[config-title]]";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbIRC;
        private System.Windows.Forms.CheckBox cbDiffPreload;
        private System.Windows.Forms.CheckBox cbShowNewEdits;
        private System.Windows.Forms.CheckBox cbOIB;
        private System.Windows.Forms.CheckBox cbAutoWhitelist;
        private System.Windows.Forms.CheckBox cbRememberPassword;
        private System.Windows.Forms.CheckBox cbRememberUsername;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.ListBox listSize;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button bSave;
    }
}

