//This is a source code or part of Huggle project
//
//This file contains code for queue panel

/// <DOCUMENTATION>
/// There is no documentation for this
/// </DOCUMENTATION>

//last modified by Petrb

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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace huggle3.Controls
{
    public partial class QueuePanel : UserControl
    {
        public List<EditItem> List = new List<EditItem>();
        private int OffsetX = 0;

        public void Add(Edit Edit)
        {
            try
            {
                lock (List)
                { 
                    List.Add(new EditItem(Edit._Page, Edit));
                }
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        public void Redraw()
        {
            try
            {
                lock (List)
                {
                    int X = 10 - OffsetX;
                    foreach (EditItem curr in List)
                    {
                        if (!curr.Registered)
                        {
                            curr.Registered = true;
                            Controls.Add(curr);
                        }
                        if (X > 0)
                        {
                            curr.Top = X;
                            curr.Left = 2;
                            curr.Width = this.Width - 20;
                            curr.Repaint(null, null);
                        }
                        X = X + curr.Height + 6;
                    }
                }
            }
            catch (Exception ff)
            {
                Core.ExceptionHandler(ff);
            }
        }

        public QueuePanel()
        {
            InitializeComponent();
        }

        private void QueuePanel_Load(object sender, EventArgs e)
        {
            vScrollBar1.Enabled = false;
        }
    }
}
