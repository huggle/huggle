using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }

        private void timer0_Tick(object sender, EventArgs e)
        {
            lock (Core.SystemLog)
            {
                textBox1.Lines = Core.SystemLog.ToArray();
            }
        }
    }
}
