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
        public static Controls.SpecialBrowser _CurrentBrowser;
        public static user _Currentuser;
        public static edit _CurrentEdit;

        private static ConfigForm config_form;
        
        
        public bool Localize()
        {
            Core.History("Main.Localize()");
            this.newTabToolStripMenuItem.Text = Languages.Get("main-browser");
            this.showNewMessagesToolStripMenuItem.Text = Languages.Get("main-system-messages");
            this.userToolStripMenuItem.Text = Languages.Get("main-user");
            this.showHistoryPageToolStripMenuItem.Text = Languages.Get("main-history");
            this.LContribs.Text = Languages.Get("main-contribs");
            this.systemToolStripMenuItem.Text = Languages.Get("main-system");
            this.lHistory.Text = Languages.Get("main-history");
            this.retrieveContributionsToolStripMenuItem.Text = Languages.Get("main-contribs");
            this.clearCurrentToolStripMenuItem.Text = Languages.Get("main-queue-clear");
           
            return false;
        }

        public void Refresh_Interface()
        {
            // make me
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
            contribsPanel.Width = this.Width - historyStrip.Left - 20;
            historyStrip.Width = this.Width - contribsPanel.Left - 20;
            // 
            lsLog.Top = Config.QueueTop + 20 + Usertool.Height + MainTool.Height + Queue.Height;
            lsLog.Left = Config.QueueLeft - 10;
            lsLog.Width = Program.MainForm.Width - 40 - Config.QueueLeft;
            lsLog.Height = Program.MainForm.Height - (webBrowser.Top + webBrowser.Height) - 80;
            lsLog.Columns[0].Width = lsLog.Width;
            // 
            cbType1.Top = 10;
            cbType1.Width = Queue.Width - 14;
            cbType2.Top = 80 + queuePanel1.Height;
            cbType2.Width = Queue.Width - 14;
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
                if (_Page != null && this.Visible)
                {
                    if (_Page !=  _Currentpage)
                    {
                        _CurrentEdit = _Page.LastEdit;
                        if (_Page.LastEdit == null)
                        {
                            _CurrentEdit = new edit();
                            
                            _CurrentEdit.Page = _Page;
                        }

                        Processing.DisplayEdit(_CurrentEdit);

                        if (_Page.LastEdit == Core.NullEdit)
                        {
                            
                        }
                        Refresh_Interface();

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

        public void Draw_Contributions()
        {
            Core.History("Draw_Contributions()");
            // todo
        }

        public void Draw_Queues()
        {
            Core.History("Draw_Queues");
            int Large = 0;
        }



        private void main_Resize(object sender, EventArgs e)
        {
            AlignForm();
        }

        public void StatusBar(string text)
        {
            toolStripStatus.Text = text;
        }

        private void main_Load(object sender, EventArgs e)
        {
            Core.History("main.main_Load()");
            //init
            AlignForm();
            toolStripStatus.Text = "Loading";
            toolStripSInfo.Text = "Wiki: " + Config.Project + " edit rate: <waiting>";
            toolStripSUser.Text = Config.Username;
            _CurrentBrowser = webBrowser;
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
            StatusBar("Done");
        }

        public bool Set_Current_Page(page _page)
        {
            _Currentpage = _page;
            CurrentPage.Text = _page.Name;
            if  ( ! CurrentPage.Items.Contains(_page.Name))
            {
                CurrentPage.Items.Add(_page.Name);
                if (CurrentPage.Items.Count > 20) // this needs to be in config
                {
                    CurrentPage.Items.RemoveAt(0);
                }
            }
            return true;
        }

        public bool Set_Current_User(user _user)
        {
            _Currentuser = _user;
            
            return true;
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

        private void CurrentUser_KeyPressed(object sender, EventArgs e)
        {
        
        }

        private void LUser_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (config_form == null)
            {
                config_form = new ConfigForm();
            }
            config_form.Show();
        }

        public void DisplayPage(string name)
        {
            Core.History("DisplayPage()");
            page Page = new page(CurrentPage.Text);
            Browser_DisplayPage(Page);
        }

        private void tmQueueUpdt_Tick(object sender, EventArgs e)
        {
            // update queue

        }

        private void CurrentPage_Trigger(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                DisplayPage(CurrentPage.Text);
            }
        }

        private void CurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.History("main.CurrentPage()");
            DisplayPage(CurrentPage.Text);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login.LoggedIn = false;
            Program._LoginForm.Visible = true;
            Core.InitConfig();
            Close();
        }

    }
}
