//This is a source code or part of Huggle project
//
//This file contains code for login form

/// <DOCUMENTATION>
/// This is a login form that is displayed when huggle is started, it's a main form of application actually which is referenced as static object
/// </DOCUMENTATION>

//Copyright (C) 2011-2012 Huggle team

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

using System.Drawing;
using System;
using Gtk;
using System.Text;

namespace huggle3.Forms
{
    /// <summary>
    /// Login form
    /// </summary>
    public partial class Login : Gtk.Window
    {
        /// <summary>
        /// Putting this to true will make it behave as if you are logging in, so that quit button will cancel
        /// the login instead of shutting down
        /// </summary>
        private bool LoggingIn = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="huggle3.Forms.Login"/> class.
        /// </summary>
        public Login () : base(Gtk.WindowType.Toplevel)
        {
            try
            {
                Core.Initialise();
                this.Build ();
                button1.Clicked += new EventHandler(btLogin_Click);
                button2.Clicked += new EventHandler(btExit_Click);
                this.DeleteEvent += new Gtk.DeleteEventHandler(onClose);
                this.label6.ModifyFg(Gtk.StateType.Normal, Core.fromColor(Color.Blue));
                this.label7.ModifyFg(Gtk.StateType.Normal, Core.fromColor(Color.Blue));
                this.label8.ModifyFg(Gtk.StateType.Normal, Core.fromColor(Color.Blue));
                Languages.Localize(this);
                this.entry2.Visibility = false;
                this.Title = "Huggle " + System.Windows.Forms.Application.ProductVersion.ToString() + " " + RevisionProvider.GetHash(true);
                if (Config.devs)
                {
                    this.Title = this.Title  + " [devs] - target: " + Core.TargetBuild();
                }
                else if (Config.Beta)
                {
                    this.Title = this.Title + " (Testing only)";
                }
                Clear();
                ListStore store1 = new ListStore(typeof(string));
                ListStore store2 = new ListStore(typeof(string));
                // load projects
                foreach (string project in Config.Projects.Keys)
                {
                    store1.AppendValues (project);
                }
                // load languages
                int dl = 0;
                int curr = 0;
                foreach (string language in Config.Languages)
                {
                    // we need to get the current language id
                    if (language == Config.Language)
                    {
                        dl = curr;
                    }
                    curr++;
                    store2.AppendValues (language);
                }
                combobox1.Model = store1;
                combobox2.Model = store2;
                combobox1.Active = 0;
                combobox2.Active = dl;
                button2.Label = Languages.Get("exit");
            } catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }
        
        /// <summary>
        /// Clear this form
        /// </summary>
        public void Clear()
        {
            this.label5.Text = "Please fill in your username and password in order to login";
            entry1.Text = "";
            entry2.Text = "";
        }

        private void CancelLogin()
        {
            EnableControls(true);
            LoggingIn = false;
            button2.Label = Languages.Get("exit");
        }

        /// <summary>
        /// On 'Login' button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLogin_Click(object sender, EventArgs e)
        {
            //Start a login attempt
            login();
        }
        
        private void login()
        {
            try
            {
                // disable all controls so that user can't change them while logging in
                EnableControls (false);
                button2.Label = Languages.Get("cancel");
                LoggingIn = true;
                Config.UseSsl = checkbutton1.Active;
                Config.Project = combobox1.ActiveText;
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
                EnableControls (true);
            }
        }
        
        private void btExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoggingIn)
                {
                    CancelLogin ();
                    return;
                }
                Close ();
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        /// <summary>
        /// This either enables or disables the controls on the form depending on their current value
        /// textName, textPassword, cmProject, cmLanguage, btLogin, btExit
        /// </summary>
        private void EnableControls(bool value)
        {
            this.entry1.Sensitive = value;
            this.entry2.Sensitive = value;
            this.combobox1.Sensitive = value;
            this.combobox2.Sensitive = value;
            this.checkbutton1.Sensitive = value;
            this.button1.Sensitive = value;
        }

        private void onClose(object sender, Gtk.DeleteEventArgs e)
        {
            try
            {
                Close ();
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        private void Close()
        {
            this.Hide();
            Core.ShutdownSystem();
        }
    }
}

