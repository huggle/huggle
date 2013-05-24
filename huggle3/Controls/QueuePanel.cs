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

namespace huggle3.Controls
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class QueuePanel : Gtk.Bin
    {
        /// <summary>
        /// The list.
        /// </summary>
        public List<EditItem> List = new List<EditItem>();
        private Queue _Queue = null;
        private int OffsetX = 0;
        /// <summary>
        /// The queue which is connected to this panel
        /// </summary>
        public Queue queue
        {
            set
            {
                if (_Queue != null)
                {
                    _Queue.Panel = null;
                }
                _Queue = value;
                if (value != null)
                {
                    _Queue.Panel = this;
                    // $fixme$
                    //Rebuild();
                }
            }
            get
            {
                return _Queue;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="huggle3.Controls.QueuePanel"/> class.
        /// </summary>
        public QueuePanel ()
        {
            this.Build ();
        }
    }
}

