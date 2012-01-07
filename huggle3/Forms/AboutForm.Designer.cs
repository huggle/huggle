namespace huggle3.Forms
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.Copyright = new System.Windows.Forms.Label();
            this.Dev1 = new System.Windows.Forms.LinkLabel();
            this.Dev2 = new System.Windows.Forms.LinkLabel();
            this.developers = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Copyright
            // 
            this.Copyright.Location = new System.Drawing.Point(11, 222);
            this.Copyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Copyright.Name = "Copyright";
            this.Copyright.Size = new System.Drawing.Size(395, 188);
            this.Copyright.TabIndex = 0;
            this.Copyright.Text = resources.GetString("Copyright.Text");
            this.Copyright.Click += new System.EventHandler(this.Copyright_Click);
            // 
            // Dev1
            // 
            this.Dev1.AutoSize = true;
            this.Dev1.Location = new System.Drawing.Point(11, 168);
            this.Dev1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Dev1.Name = "Dev1";
            this.Dev1.Size = new System.Drawing.Size(69, 17);
            this.Dev1.TabIndex = 1;
            this.Dev1.TabStop = true;
            this.Dev1.Text = "Addshore";
            // 
            // Dev2
            // 
            this.Dev2.AutoSize = true;
            this.Dev2.Location = new System.Drawing.Point(11, 194);
            this.Dev2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Dev2.Name = "Dev2";
            this.Dev2.Size = new System.Drawing.Size(42, 17);
            this.Dev2.TabIndex = 2;
            this.Dev2.TabStop = true;
            this.Dev2.Text = "Petrb";
            // 
            // developers
            // 
            this.developers.AutoSize = true;
            this.developers.Location = new System.Drawing.Point(8, 141);
            this.developers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.developers.Name = "developers";
            this.developers.Size = new System.Drawing.Size(80, 17);
            this.developers.TabIndex = 4;
            this.developers.Text = "Developers";
            this.developers.Click += new System.EventHandler(this.developers_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(93, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(238, 122);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(117, 424);
            this.button.Margin = new System.Windows.Forms.Padding(4);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(167, 36);
            this.button.TabIndex = 6;
            this.button.Text = "OK";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 473);
            this.Controls.Add(this.button);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.developers);
            this.Controls.Add(this.Dev2);
            this.Controls.Add(this.Dev1);
            this.Controls.Add(this.Copyright);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AboutForm";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Copyright;
        private System.Windows.Forms.LinkLabel Dev1;
        private System.Windows.Forms.LinkLabel Dev2;
        private System.Windows.Forms.Label developers;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button;
    }
}