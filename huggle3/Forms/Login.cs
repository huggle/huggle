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

using System;

namespace huggle3.Forms
{
	public partial class Login : Gtk.Window
	{
		public Login () : base(Gtk.WindowType.Toplevel)
		{
			Core.Initialise();
			this.Build ();
			this.DeleteEvent += new Gtk.DeleteEventHandler(onClose);
		}

		public void onClose(object sender, Gtk.DeleteEventArgs e)
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

		public void Close()
		{
			this.Hide();
			Core.ShutdownSystem();
		}
	}
}

