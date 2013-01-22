using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace huggle3.Controls
{
    public partial class EditItem : UserControl
    {
        public Edit Edit = null;
        public Page Page = null;
        public bool Registered = false;


        public EditItem(Page __Page, Edit __Edit)
        {
            this.Edit = __Edit;
            this.Page = __Page;
            InitializeComponent();
        }

        public void Repaint(object sender, EventArgs e)
        {
            label1.Left = this.Width - 20;
            label1.Top = this.Width - 2;
        }

        private void EditItem_Load(object sender, EventArgs e)
        {
            Repaint(null, null);
            label2.Text = Page.Name;
        }
    }
}
