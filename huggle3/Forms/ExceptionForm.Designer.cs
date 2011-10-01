namespace huggle3.Forms
{
    partial class ExceptionForm
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
            this.btContinue = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ErrorLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btContinue
            // 
            this.btContinue.Location = new System.Drawing.Point(355, 307);
            this.btContinue.Name = "btContinue";
            this.btContinue.Size = new System.Drawing.Size(95, 25);
            this.btContinue.TabIndex = 0;
            this.btContinue.Text = "Continue";
            this.btContinue.UseVisualStyleBackColor = true;
            this.btContinue.Click += new System.EventHandler(this.btContinue_Click);
            // 
            // btExit
            // 
            this.btExit.Location = new System.Drawing.Point(456, 307);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(95, 25);
            this.btExit.TabIndex = 1;
            this.btExit.Text = "Exit";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Huggle has crashed! Bellow is the log:";
            // 
            // ErrorLog
            // 
            this.ErrorLog.Location = new System.Drawing.Point(15, 42);
            this.ErrorLog.Multiline = true;
            this.ErrorLog.Name = "ErrorLog";
            this.ErrorLog.ReadOnly = true;
            this.ErrorLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ErrorLog.Size = new System.Drawing.Size(587, 244);
            this.ErrorLog.TabIndex = 4;
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 344);
            this.ControlBox = false;
            this.Controls.Add(this.ErrorLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btContinue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ExceptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Huggle error";
            this.Load += new System.EventHandler(this.ExceptionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ErrorLog;
        public System.Windows.Forms.Button btContinue;
    }
}