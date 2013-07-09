//This is a source code or part of Huggle project
//
//This file contains code for form

/// <DOCUMENTATION>
/// This is a primary form, it contains most of stuff you need
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
	public partial class HuggleForm : Gtk.Window
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="huggle3.Forms.HuggleForm"/> class.
		/// </summary>
		public HuggleForm () : 	base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.DeleteEvent += new Gtk.DeleteEventHandler(onClose);
            this.Icon = global::Gdk.Pixbuf.LoadFromResource("huggle3.Pictures.huggle.ico");
            this.Maximize();
			Languages.Localize(this);
			this.Title = "Huggle " + System.Windows.Forms.Application.ProductVersion.ToString() + " " + RevisionProvider.GetHash(true);
            if (Config.devs)
            {
                this.Title = this.Title  + " [devs] - target: " + Core.TargetBuild();
            }
            else if (Config.Beta)
            {
                this.Title = this.Title + " (Testing only)";
            }
		}
		
		private void onClose(object sender, Gtk.DeleteEventArgs e)
		{
            Core.ShutdownSystem();
		}
	}
}
