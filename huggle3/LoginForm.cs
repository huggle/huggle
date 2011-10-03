//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Addshore

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
    public partial class LoginForm : Form
    {
        public bool LoggingOn;
        public LoginForm()
        {
                InitializeComponent();
        }

        /// <summary>
        /// Localize the form with the relevant language
        /// </summary>
        /// <returns></returns>
        private bool Localize()
        {
            this.lbl_Language.Text = Languages.Get("login-language");
            this.lbl_Project.Text = Languages.Get("login-project");
            this.btExit.Text = Languages.Get("exit"); //This used to be 'login-exit' but this doesn't exist so using 'exit' instead
            this.lPassword.Text = Languages.Get("login-password");
            this.Name = "Huggle";
            this.lblName.Text = Languages.Get("login-username");
            return false;
        }

        /// <summary>
        /// On 'Login' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Core.History("btLogin_Click()");
                if (Config.Projects.ContainsKey(this.cmProject.Text) == false)
                {
                    // ID is not in list
                    MessageBox.Show("Please select a project");
                    return;
                }
                if (Config.Languages.Contains(cmLanguage.Text))
                {
                    Config.Language = cmLanguage.Text;
                }
                else
                {
                    // Language is not in list
                    MessageBox.Show(Languages.Get("login-error2"));
                    return;
                }

                //Lock the form controls
                this.textName.Enabled = false;
                this.textPassword.Enabled = false;
                this.cmProject.Enabled = false;
                this.cmLanguage.Enabled = false;
                this.btLogin.Enabled = false;
                this.btExit.Text = Languages.Get("cancel");
                StatusBar.Value = 0;

                Config.Project = cmProject.Text;// set project
                Config.Password = textPassword.Text; // set password
                Config.Username = textName.Text;// set username
                login.LoggingOn = true;// set loggin in
                login.phase = login.LoginState.LoggingIn;// set phase
                LoginRequest lr = new LoginRequest();// start a new login request
                lr.Login_Fr = this;
                progress("Logging in...");
                lr.Start();
                timer.Enabled = true;
                //Hide();
            }
            catch (Exception A)
            {
                Core.ExceptionHandler(A);

                //Unlock the form controls if an error is thrown
                this.textName.Enabled = true;
                this.textPassword.Enabled = true;
                this.cmProject.Enabled = true;
                this.cmLanguage.Enabled = true;
                this.btLogin.Enabled = true;
                this.btExit.Enabled = true;
            }
        }

        /// <summary>
        /// Update the progress of the form with the given text
        /// </summary>
        /// <param name="text"></param>
        public void progress(string text)
        {
            this.StatusBox.Text = text;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
                Core.Initialise();
                progress("Please enter login details");
                textPassword.UseSystemPasswordChar = true;
                //Load the config
                Core_IO.LoadLocalConfig();
                //For each project listed in the config
                foreach (KeyValuePair<string, string> Pr in Config.Projects)
                {
                    //Add the project to the list
                    cmProject.Items.Add(Pr.Key);
                }

                //For each language listed in the config file
                foreach (string language in Config.Languages)
                {
                    // Add the language to the list
                    cmLanguage.Items.Add(language);
                }

                if (cmProject.Items.Contains(Config.Project))
                {
                    // Default project
                    cmProject.SelectedItem = Config.Project;
                }
                cmLanguage.SelectedItem = Config.DefaultLanguage; // Select the default language
        }
        /// <summary>
        /// On 'Exit' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btExit_Click(object sender, EventArgs e)
        {
            //Close the application
            if (login.LoggingOn)
            {
                this.btExit.Text = Languages.Get("exit");
                this.btExit.Enabled = false;
                login.LoggingOn = false;
                login.phase = login.LoginState.Error;
                return;
            }
            Application.Exit();
        }

        /// <summary>
        /// If the language selection is changed then try to load the language and localize the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Config.Language = cmLanguage.Text;
                Localize();
            }
            catch (Exception x)
            { 
                //unable to change language
                Core.ExceptionHandler(x); 
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (login.LoggingOn == true)
                {
                    if (login.LoggedIn == true)
                    {
                        switch (login.phase)
                        { 
                            case login.LoginState.LoggedIn:
                                StatusBar.Value = 40;
                                StatusBox.Text = Languages.Get("login-progress-global");
                                login.phase = login.LoginState.LoadingGlobal;
                                Requests.request_config_global global = new Requests.request_config_global();
                                global.Start();
                                break;
                            case login.LoginState.LoadedGlobal:
                                StatusBar.Value = 60;
                                StatusBox.Text = Languages.Get("login-progress-local");
                                login.phase = login.LoginState.LoadingLocal;
                                Requests.request_config_local local_cf = new Requests.request_config_local();
                                local_cf.Start();
                                break;
                            case login.LoginState.LoadedLocal:
                                StatusBar.Value = 80;
                                login.phase = login.LoginState.Whitelist;
                                Requests.request_white_list whitelist_request = new Requests.request_white_list();
                                whitelist_request.Start();
                                break;
                            case login.LoginState.Successful:
                                // show the form
                                login.LoggedIn = false;
                                Program.MainForm = new main();
                                Program.MainForm.Show();
                                timer.Enabled = false;
                                Hide();
                                break;
                            case login.LoginState.Error:
                                StatusBar.Value = 0;
                                progress("Error logging in:");
                                login.LoggingOn = false;
                                login.LoggedIn = false;
                                break;
                        }
                    }
                }
                else
                {
                    this.textName.Enabled = true;
                    this.textPassword.Enabled = true;
                    this.cmProject.Enabled = true;
                    this.cmLanguage.Enabled = true;
                    this.btLogin.Enabled = true;
                    this.btExit.Enabled = true;
                    if (login.Status != request_core.Request.LoginResult.Success)
                    {
                        this.progress(login.Error);
                    }
                    timer.Enabled = false;
                }
            }
            catch (Exception A)
            {
                Core.ExceptionHandler(A);
            }
        }

        /// <summary>
        /// This is run whenever one of the controls is changed on the login page
        /// This includes the two text boxes and two list boxes
        /// This then either enables or disables the login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlChanged(object sender, EventArgs e)
        {
            //If each of the controls has some length
            if (textName.Text.Length != 0 && textPassword.Text.Length != 0
                && cmLanguage.Text.Length != 0 && cmProject.Text.Length != 0)
            {
                //Allow the user to login
                progress("Please login");
                btLogin.Enabled = true;
            }
            else
            {
                //Disable the login button
                progress("Please enter login details");
                btLogin.Enabled = false;
            }
        }

    }
}
