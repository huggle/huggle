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
            this.ErrorLog.Lines = new string[] {
                                    "Please send the following data for analysis \n\n",
                                    Core.history,  error.Message, "Source: " , error.Source , "Stack trace :\n",
                                    error.StackTrace };
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Core.MainThread.Abort(); // terminate
            Application.Exit();
        }

        private void btContinue_Click(object sender, EventArgs e)
        {
            Core.Interrupted = false;
            System.Threading.Thread.CurrentThread.Abort();
            Close();
        }
    }
}
