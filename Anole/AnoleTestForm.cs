using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Anole
{
    /// <summary>
    /// Summary description for AnoleTestForm.
    /// </summary>
    public class AnoleTestForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox txtSource;
        private HTMLPanel myHTMLPanel;

        protected string myHTMLFile;

        public AnoleTestForm()
        {
            //
            // Required for Windows Form Designer support
            //
            Application.EnableVisualStyles();
            InitializeComponent();

            //Create and add an HTML Panel
            myHTMLPanel = new HTMLPanel();
            this.myHTMLPanel.Size = new System.Drawing.Size(240, 250);
            myHTMLPanel.OnLinkClick += new Anole.HTMLPanel.LinkClickHandler(myHTMLPanel_OnLinkClick);
            this.Controls.Add(this.myHTMLPanel);

            this.LoadFile("Test1.txt");
        }

        public void LoadFile(string sFileName)
        {
            myHTMLFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\" + sFileName;
            this.myHTMLPanel.LoadHTMLFile(myHTMLFile);
            this.txtSource.Text = myHTMLPanel.HTML;

            //this.txtSource.Text=myHTMLPanel.ToString();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSource = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(253, 12);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSource.Size = new System.Drawing.Size(240, 248);
            this.txtSource.TabIndex = 0;
            this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
            // 
            // AnoleTestForm
            // 
            this.ClientSize = new System.Drawing.Size(524, 330);
            this.Controls.Add(this.txtSource);
            this.Name = "AnoleTestForm";
            this.Text = "Anole Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static void Main()
        {
            Application.Run(new AnoleTestForm());
        }

        private void myHTMLPanel_OnLinkClick(object HTMLPanel, HTMLEventArgs htmlEventInfo)
        {
            
        }

        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            this.myHTMLPanel.HTML = txtSource.Text;
        }
    }
}
