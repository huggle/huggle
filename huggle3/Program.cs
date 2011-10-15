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
using System.Windows.Forms;

namespace huggle3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static main MainForm;
        public static LoginForm _LoginForm;

        public class ExceptionHandler 
        {
            public void ThreadExceptionHandle(object sender, UnhandledExceptionEventArgs t)
            {
                // exception
                Core.ExceptionHandler(new Exception("PANIC:" + t.ToString()), true);
            }
        }

        [STAThread]
        static void Main()
        {
                ExceptionHandler EH = new ExceptionHandler();
                AppDomain.CurrentDomain.UnhandledException += EH.ThreadExceptionHandle;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                _LoginForm = new LoginForm();
                Application.Run(_LoginForm); // spawn
        }
   
    }
}
