//This is a source code or part of Huggle project
//
//This file contains code for exception form

/// <DOCUMENTATION>
/// There is no documentation for this
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace huggle3.Forms
{
    public partial class ExceptionForm : Form
    {
        public Exception error;
        public ExceptionForm()
        {
            InitializeComponent();
        }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (Core.Panic)
                {
                    btContinue.Enabled = false;
                }
                List<string> text = new List<string>();
                text.Add("Please send the following data for analysis \n\n");
                text.Add(Core.history);
                text.Add(error.Message);
                text.Add("Source: ");
                text.Add(error.Source);
                text.Add("Stack trace :\n");
                text.Add(error.StackTrace);
                text.Add("Logs");
                lock (Core.SystemLog)
                {
                    text.AddRange(Core.SystemLog);
                }

                this.ErrorLog.Lines = text.ToArray();
            }
            catch (Exception fail)
            {
                Console.WriteLine("Unable to handle the exception, killing the program: " + fail.Message);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            try
            {
                Core.Threading.DestroyCore(); // abort running threads
                Core.MainThread.Abort(); // terminate
                Core.Process.Kill();
            }
            catch (Exception)
            {
                Core.Process.Kill();
            }
        }

        private void btContinue_Click(object sender, EventArgs e)
        {
            Core.Interrupted = false;
            System.Threading.Thread.CurrentThread.Abort();
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
