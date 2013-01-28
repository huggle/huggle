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
        private QueuePanel Parent = null;


        public EditItem(Page __Page, Edit __Edit, QueuePanel parent)
        {
            this.Edit = __Edit;
            this.Page = __Page;
            Parent = parent;
            lock (__Edit)
            {
                if (__Edit != null && __Edit.Processed != true)
                {
                    Processing.ProcessEdit(__Edit);
                }
            }
            InitializeComponent();
        }

        public void Repaint(object sender, EventArgs e)
        {
            label1.Left = this.Width - 20;
            label1.Height = this.Height - 2;
            label2.Width = this.Width - label1.Width - 2;
        }

        private void EditItem_Load(object sender, EventArgs e)
        {
            Repaint(null, null);
            label2.Text = Page.Name;
        }

        public void Requested()
        {
            try
            {
                lock (Parent.List)
                {
                    if (Parent.List.Contains(this))
                    {
                        Parent.List.Remove(this);
                    }
                    this.Visible = false;
                    Processing.DisplayEdit(Edit);
                }
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Requested();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Requested();
        }
    }
}
