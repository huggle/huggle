using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace huggle3.Forms
{
    public partial class Syslog : Form
    {
        public Syslog()
        {
            InitializeComponent();
        }

        private void Syslog_Load(object sender, EventArgs e)
        {
            lock (Core.SystemLog)
            {
                textBox1.Lines = Core.SystemLog.ToArray();
            }
            RefreshData();
        }

        private void RefreshData()
        {
            try
            {
                listView1.Items.Clear();
                lock (Core.Process.Threads)
                {
                    foreach (Core.Threading.ThreadS xx in Core.Threading.Threads)
                    {
                        ListViewItem item = null;
                        if (xx.handle == null)
                        {
                            continue;
                        }
                        if (xx.handle.Name != null)
                        {
                            item = new ListViewItem(xx.handle.Name);
                        }
                        else
                        {
                            item = new ListViewItem("Unknown managed thread");
                        }
                        item.SubItems.Add(xx.handle.ManagedThreadId.ToString());
                        item.SubItems.Add(xx.handle.ThreadState.ToString());
                        listView1.Items.Add(item);
                    }
                    foreach (System.Diagnostics.ProcessThread xx in Core.Process.Threads)
                    {
                        ListViewItem item = new ListViewItem("Process thread");
                        item.SubItems.Add(xx.Id.ToString());
                        item.SubItems.Add(xx.ThreadState.ToString());
                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception fail)
            {
                timer1.Enabled = false;
                Core.ExceptionHandler(fail);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void timer0_Tick_1(object sender, EventArgs e)
        {
            lock (Core.SystemLog)
            {
                textBox1.Lines = Core.SystemLog.ToArray();
            }
        }
    }
}
