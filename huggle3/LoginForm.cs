//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Addshore

//Copyright (C) 2011-2012 Huggle team

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
        private bool Localize()
        {
            this.lbl_Language.Text = Languages.Get("login-language");
            this.lbl_Project.Text = Languages.Get("login-project");
            this.btExit.Text = Languages.Get("exit"); //This used to be 'login-exit' but this doesn't exist so using 'exit' instead
            this.btLogin.Text = Languages.Get("login-start");
            this.lPassword.Text = Languages.Get("login-password");
            this.checkBox.Text = Languages.Get("login-ssl");
            this.Text = "Huggle " + Application.ProductVersion.ToString();
            if (Config.devs)
            { 
                this.Text = this.Text  + " [devs] - target: " + Core.TargetBuild();
            }
            else if (Config.Beta)
            {
                this.Text = this.Text + " (Testing only)";
            }
            this.lblName.Text = Languages.Get("login-username");
            this.lProxy.Text = Languages.Get("login-proxygroup");
            this.lTranslate.Text = Languages.Get("login-translate");
            return false;
        }

        /// <summary>
        /// This either enables or disables the controls on the form depending on their current value
        /// textName, textPassword, cmProject, cmLanguage, btLogin, btExit
        /// </summary>
        private void EnableControls(bool value)
        {
            this.textName.Enabled = value;
            this.textPassword.Enabled = value;
            this.cmProject.Enabled = value;
            this.cmLanguage.Enabled = value;
            this.checkBox.Enabled = value;
            this.btLogin.Enabled = value;
            this.btExit.Enabled = true;
            this.btExit.Text = Languages.Get("exit");
        }

        /// <summary>
        /// This checks to see if return is hit when one of the data entry controls is selected on the form
        /// If it is then it starts a login attempt
        /// </summary>
        private void CheckKeyPressLogin(object sender, KeyPressEventArgs e)
        {
            //If the key that has been pressed is the enter button (\r)
            if (e.KeyChar.Equals('\r'))
            {
                //And logging in is enabled
                if (btLogin.Enabled.Equals(true))
                {
                    //Start a login attempt
                    loginattempt();
                }
                else
                {
                    //Select the next control on the form as if pushing enter when finished typing in a box
                    this.SelectNextControl(this.ActiveControl,true,true,true,true);
                }
            }
        }

        /// <summary>
        /// Start a login
        /// </summary>
        private void loginattempt()
        {
            try
            {
                Core.History("btLogin_Click()");
                EnableControls(false);
                this.btExit.Text = Languages.Get("cancel");
                StatusBar.Value = 0;

                if (Config.Languages.Contains(cmLanguage.Text)) { Config.Language = cmLanguage.Text; } // set language (if needed)
                Config.UseSsl = checkBox.Checked;
                Config.Project = cmProject.Text; // set project
                Config.Password = textPassword.Text; // set password
                Config.Username = textName.Text; // set username
                login.LoggingOn = true; // set loggin in
                login.phase = login.LoginState.LoggingIn; // set phase
                LoginRequest lr = new LoginRequest(); // start a new login request
                lr.Login_Fr = this;
                progress(Languages.Get("login-progress-start"));
                lr.Start();
                timer.Enabled = true;
                //Hide();
            }
            catch (Exception A)
            {
                Core.ExceptionHandler(A);
                EnableControls(true);
            }
        }

        /// <summary>
        /// On 'Login' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLogin_Click(object sender, EventArgs e)
        {
            //Start a login attempt
            loginattempt();
        }

        /// <summary>
        /// Update the progress of the form with the given text
        /// </summary>
        /// <param name="text"></param>
        public void progress(string text)
        {
            this.StatusBox.Text = text;
        }

        public void PrepareForm()
        {
            Core.History("PrepareForm()");
            Config.UserAgent = "Huggle/" + Application.ProductVersion.ToString() + " http://en.wikipedia.org/wiki/Wikipedia:Huggle";
            progress("Please enter login details");
            textPassword.UseSystemPasswordChar = true;
            checkBox.Checked = Config.UseSsl;
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

                StatusBar.Value = 0;

                cmProject.SelectedItem = Config.Project; // Select the default project
                EnableControls(true);
                cmLanguage.SelectedItem = Config.DefaultLanguage; // Select the default language

                textName.Select(); //Select the name text box straight away to enable a quicker login
                textPassword.Text = "";
        }
        
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
                Core.Initialise();
                if (Config._Platform == Config.platform.linux32 || Config._Platform == Config.platform.linux64)
                {
                    // gnome fix
                    this.Width = this.Width - 80;
                }
                PrepareForm();
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
                                //Load the global config
                                StatusBar.Value = 40;
                                StatusBox.Text = Languages.Get("login-progress-global");
                                login.phase = login.LoginState.LoadingGlobal;
                                Requests.request_config_global global = new Requests.request_config_global();
                                global.Start();
                                break;
                            case login.LoginState.LoadedGlobal:
                                //Load the local config
                                StatusBar.Value = 60;
                                StatusBox.Text = Languages.Get("login-progress-local");
                                login.phase = login.LoginState.LoadingLocal;
                                Requests.request_config_local local_cf = new Requests.request_config_local();
                                local_cf.Start();
                                break;
                            case login.LoginState.LoadedLocal:
                                //Load the whitelist
                                StatusBar.Value = 80;
                                login.phase = login.LoginState.Whitelist;
                                Requests.request_white_list whitelist_request = new Requests.request_white_list();
                                whitelist_request.Start();
                                break;
                            case login.LoginState.Successful:
                                //Logging in done
                                // show the form
                                login.LoggedIn = false;
                                Program.MainForm = new main();
                                Program.MainForm.Show();
                                timer.Enabled = false;
                                Hide();
                                break;
                            case login.LoginState.Error:
                                //Something has gone wrong (show error)
                                StatusBar.Value = 0;
                                progress(Languages.Get("login-error-unknown"));
                                login.LoggingOn = false;
                                login.LoggedIn = false;
                                break;
                        }
                    }
                }
                else
                {
                    EnableControls(true);
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

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked == false)
            {
                textPassword.BackColor = Color.White;
                textName.BackColor = Color.White;
            }
            else
            {
                textName.BackColor = Color.LightYellow;
                textPassword.BackColor = Color.LightYellow;
            }
        }

    }
}
