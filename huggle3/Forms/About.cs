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

namespace huggle3.Forms
{
    /// <summary>
    /// About form
    /// </summary>
    public partial class About : Gtk.Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="huggle3.Forms.About"/> class.
        /// </summary>
        public About() : base(Gtk.WindowType.Toplevel)
        {
            try
            {
                this.Build ();
                this.label1.Text = System.Windows.Forms.Application.ProductVersion.ToString() + " " + RevisionProvider.GetHash(true);
            } catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }
    }
}

