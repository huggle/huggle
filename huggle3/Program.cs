//This is a source code or part of Huggle project
//
//This file contains code for huggle

/// <DOCUMENTATION>
/// This is a entry point for program
/// this file contains the main class and definition for 2 objects - the login form and main form
/// as you can see the application is actually not started by calling Core.Init() but,
/// by creating instance of Login form which then call the Core and start everything.
///
/// This instance is loaded during whole run of huggle. You can't unload it, only hide it.
/// Login form also creates main form, which can be unloaded anytime.
/// </DOCUMENTATION>

//Copyright (C) 2011-2013 Huggle team

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//GNU General Public License for more details.
using System;
using System.Collections.Generic;
using Gtk;

namespace huggle3
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		///
		//public static main MainForm;
		public static Forms.LoginForm LoginForm = null;
        public static Forms.HuggleForm HuggleForm = null;
		
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
			Application.Init ();
			LoginForm = new Forms.LoginForm();
			LoginForm.Show ();
			Application.Run ();
		}
		
	}
}