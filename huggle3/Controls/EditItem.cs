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
        public edit Edit = null;
        public page Page = null;

        public EditItem(page __Page, edit __Edit)
        {
            this.Edit = __Edit;
            this.Page = __Page;
            InitializeComponent();
        }

        private void Repaint(object sender, EventArgs e)
        {
            label1.Left = this.Width - 2;
            label1.Top = this.Width - 2;
        }

        private void EditItem_Load(object sender, EventArgs e)
        {
            Repaint(null, null);
        }
    }
}
