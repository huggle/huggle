//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Petrb

//Copyright (C) 2011 Huggle team

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace huggle3
{
    public partial class main : Form
    {
        public static page _Currentpage;
        public static user _Currentuser;
        public static user _Currentedi;
        
        
        public bool Localize()
        {
            Core.History("Main.Localize()");
            //this.newTabToolStripMenuItem.Text = Languages.Get("main-browser").ToString();
            this.showNewMessagesToolStripMenuItem.Text = Languages.Get("main-new-messages");
            this.userToolStripMenuItem.Text = Languages.Get("main-user");
            this.showHistoryPageToolStripMenuItem.Text = Languages.Get("main-history");
            this.LContribs.Text = Languages.Get("main-contribs");
            this.systemToolStripMenuItem.Text = Languages.Get("main-system");
            this.lHistory.Text = Languages.Get("main-history");
            this.retrieveContributionsToolStripMenuItem.Text = Languages.Get("main-contribs");
            this.clearCurrentToolStripMenuItem.Text = Languages.Get("main-queue-clear");
           
            return false;
        }
        public main()
        {
            InitializeComponent();
        }

        public int AlignForm()
        {
            Core.History("main.AlignForm()");
            // queue
            Queue.Left = Config.QueueLeft;
            Queue.Top = Config.QueueTop + Usertool.Height + MainTool.Height;
            Queue.Height = Program.MainForm.Height - 280 - MainTool.Height - Usertool.Height;
            Queue.Width = Config.QueueWidth;
            // browser
            webBrowser.BringToFront();
            webBrowser.Top = Config.QueueTop + Usertool.Height + MainTool.Height;
            webBrowser.Left = Queue.Left + 20 + Queue.Width;
            webBrowser.Height = Program.MainForm.Height - 280 - MainTool.Height - Usertool.Height;
            webBrowser.Width = this.Width - Config.QueueWidth - 60;
            // 
            lsLog.Top = Config.QueueTop + 20 + Usertool.Height + MainTool.Height + Queue.Height;
            lsLog.Left = Config.QueueLeft - 10;
            lsLog.Width = Program.MainForm.Width - 40 - Config.QueueLeft;
            lsLog.Height = Program.MainForm.Height - (webBrowser.Top + webBrowser.Height) - 80;
            lsLog.Columns[0].Width = lsLog.Width;
            // 
            cbType1.Top = 10;
            cbType1.Width = Queue.Width;
            cbType2.Top = 80 + queuePanel1.Height;
            cbType2.Width = Queue.Width;
            queuePanel1.Top = cbType1.Top + 40;
            queuePanel1.Width = cbType1.Width;
            queuePanel2.Top = cbType2.Top + 40;
            return 1;
        }

        private void main_Close(object sender, EventArgs e)
        {
            Core.ShutdownSystem();
        }

        public void Browser_DisplayPage(page _Page)
        {
            try
            {
                if (_Page != null)
                {
                    if (_Page !=  _Currentpage)
                    { 
                        
                    }
                }
            }
            catch(Exception B)
            {
                Core.ExceptionHandler(B);
            }
        }

        public void Log(string text)
        {
            lsLog.Items.Add(text);
        }

        private void main_Resize(object sender, EventArgs e)
        {
            AlignForm();
        }

        private void main_Load(object sender, EventArgs e)
        {
            Core.History("main.main_Load()");
            //init
            AlignForm();
            Log("huggle init"); // there is supposed to be inital message concerning start up
            OpenInfo();
            Localize();
            lsLog.Columns.Add("");
            
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
        
        private void OpenInfo()
        {
            // startup page
            this.webBrowser.Navigate(Config.Projects[Config.Project] + Config.WikiPath + "index.php?title=" + Config.StartupPage + "&action=render");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            Core.ShutdownSystem();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            huggle3.Forms.AboutForm about = new huggle3.Forms.AboutForm();
            about.Show();
        }

        private void CurrentUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void LContribs_Click(object sender, EventArgs e)
        {
           
        }

        private void LUser_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void tmQueueUpdt_Tick(object sender, EventArgs e)
        {
            // update queue

        }

        private void CurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.History("main.CurrentPage()");
            page Page = new page();
            Page.Name = CurrentPage.Text;
            Browser_DisplayPage(Page);
        }

    }
}
