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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Copyright
            // 
            this.Copyright.Location = new System.Drawing.Point(12, 157);
            this.Copyright.Name = "Copyright";
            this.Copyright.Size = new System.Drawing.Size(296, 153);
            this.Copyright.TabIndex = 0;
            this.Copyright.Text = resources.GetString("Copyright.Text");
            // 
            // Dev1
            // 
            this.Dev1.AutoSize = true;
            this.Dev1.Location = new System.Drawing.Point(12, 108);
            this.Dev1.Name = "Dev1";
            this.Dev1.Size = new System.Drawing.Size(52, 13);
            this.Dev1.TabIndex = 1;
            this.Dev1.TabStop = true;
            this.Dev1.Text = "Addshore";
            // 
            // Dev2
            // 
            this.Dev2.AutoSize = true;
            this.Dev2.Location = new System.Drawing.Point(12, 133);
            this.Dev2.Name = "Dev2";
            this.Dev2.Size = new System.Drawing.Size(32, 13);
            this.Dev2.TabIndex = 2;
            this.Dev2.TabStop = true;
            this.Dev2.Text = "Petrb";
            // 
            // developers
            // 
            this.developers.AutoSize = true;
            this.developers.Location = new System.Drawing.Point(12, 84);
            this.developers.Name = "developers";
            this.developers.Size = new System.Drawing.Size(61, 13);
            this.developers.TabIndex = 4;
            this.developers.Text = "Developers";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::huggle3.Properties.Resources.hugglelogo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(325, 74);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 316);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.developers);
            this.Controls.Add(this.Dev2);
            this.Controls.Add(this.Dev1);
            this.Controls.Add(this.Copyright);
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
    }
}