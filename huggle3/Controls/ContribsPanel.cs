//This is a source code or part of Huggle project
//
//This file contains code for
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
    public partial class ContribsPanel : UserControl
    {
        public user ThisUser;
        public int Offset;
        public edit SelectedEdit;

        public void Draw(Object s, PaintEventArgs b)
        {
            Graphics gf = b.Graphics;

            gf.Clear(Color.FromKnownColor(KnownColor.Control));
            gf.DrawImage(huggle3.Properties.Resources.gradient, 2, 2, Width - 4, Height - 4);
            gf.DrawRectangle(Pens.DarkGray, 1, 1, Width - 3, Height - 3);

        }

        public ContribsPanel()
        {
            InitializeComponent();
        }
    }
}
