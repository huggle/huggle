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
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        ///<summary>
        ///Localize
        ///</summary>
        private void Localize()
        {
            Core.History("configuration.Localize()");
            this.Text = Languages.Get("config-title");
            this.bSave.Text = Languages.Get("config-save");
            this.Cancel.Text = Languages.Get("config-cancel");
            this.groupBox1.Text = Languages.Get("config-general");
            this.groupBox2.Text = Languages.Get("config-interface");
            this.groupBox3.Text = Languages.Get("config-keyboard");
            this.groupBox4.Text = Languages.Get("config-editing");
            this.groupBox5.Text = Languages.Get("config-reverting");
            this.groupBox6.Text = Languages.Get("config-reporting");
            this.groupBox7.Text = Languages.Get("config-templates-id");
            this.groupBox8.Text = Languages.Get("config-editor");
            this.groupBox9.Text = Languages.Get("config-admin");
        }

        public void Tab(string key)
        {
            foreach (GroupBox gb in this.Controls)
            {
                gb.Visible = false;
            }
            switch (key)
            {
                case "keyboard":
                    this.groupBox3.Visible = true;
                    break;
                case "general":
                    this.groupBox1.Visible = true;
                    break;
                case "interface":
                    this.groupBox2.Visible = true;
                    break;
                case "editing":
                    this.groupBox4.Visible = true;
                    break;
                case "reverting":
                    this.groupBox5.Visible = true;
                    break;
                case "reporting":
                    this.groupBox6.Visible = true;
                    break;
            }
        }

        public void Config_Load()
        {
            //Load all config values

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            Localize();
            Config_Load();
            listView1.Items.Add("general", Languages.Get("config-general"), 0);
            listView1.Items.Add("interface", Languages.Get("config-interface"), 0);
            listView1.Items.Add("keyboard", Languages.Get("config-keyboard"), 0);
            listView1.Items.Add("editing", Languages.Get("config-editing"), 0);
            listView1.Items.Add("reverting", Languages.Get("config-reverting"), 0);
            listView1.Items.Add("reporting", Languages.Get("config-reporting"), 0);
            listView1.Items.Add("templates", Languages.Get("config-templates-id"), 0);
            listView1.Items.Add("editor", Languages.Get("config-editor"), 0);
            listView1.Items.Add("admin", Languages.Get("config-admin"), 0);
            Tab("general");
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab(listView1.SelectedItems.ToString());
        }
    }
}
