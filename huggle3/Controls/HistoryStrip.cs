//This is a source code or part of Huggle project
//
//This file contains code for history strip
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
    public partial class HistoryStrip : UserControl
    {
        public page _Page;
        public int Offset;
        public edit OlderEdit, NewerEdit;

        public void Draw(Object s, PaintEventArgs pe)
        {
            Graphics gf = pe.Graphics;

            gf.Clear(Color.FromKnownColor(KnownColor.Control));
            gf.DrawImage(huggle3.Properties.Resources.gradient, 2, 2, Width - 4, Height - 4);
            gf.DrawRectangle(Pens.DarkGray, 1, 1, Width - 3, Height - 3);

            if (_Page != null)
            {
                int NewerPos = -1;
                int OlderPos = -1;
                edit Edit = _Page.LastEdit;
                int x;
                x = Width - 18 + (Offset * Config.ItemSize);
                int check = 0;

                while (check < 800 && Edit != null && Edit != Core.NullEdit)
                {
                    if (x < 0)
                    {
                        break;
                    }

                    if (x < Width)
                    {
                        //gf.DrawImage();


                        switch (Edit.WarningLevel)
                        {
                            case user.UserLevel.Warn1:
                                gf.DrawString("!1", Font, Brushes.Black, x + 2, 3);
                                break;
                            case user.UserLevel.Warn2:
                                gf.DrawString("!2", Font, Brushes.Black, x + 2, 3);
                                break;
                            case user.UserLevel.Warn3:
                                gf.DrawString("!3", Font, Brushes.Black, x + 2, 3);
                                break;
                            case user.UserLevel.WarnFinal:
                            case user.UserLevel.Warn4im:
                                gf.DrawString("!4", Font, Brushes.Black, x + 2, 3);
                                break;
                        }

                        if (OlderEdit != null)
                        {
                            OlderPos = x - 1;
                        }
                        if (NewerEdit != null)
                        {
                            NewerPos = x - 1;
                        }

                        if (Edit._Page != null)
                        {
                            if (Edit == Edit._Page.LastEdit)
                            {
                                gf.DrawRectangle(Pens.DarkBlue, x, 2, 15, 15);
                            }
                            gf.DrawLine(Pens.DarkGray, x + 16, 2, x + 16, Config.ItemSize);
                        }
                    }
                    check = check + 1;
                    x = x - 17;
                }

                if (x < 0 && Edit != null && Edit == main._CurrentEdit)
                {
                    if (Edit.Id == main._CurrentEdit.Oldid && _Page.LastEdit != Edit)
                    {
                        Offset++;
                        Draw(s, pe);
                    }
                }
                gf.DrawLine(Pens.DarkGray, x + 16, 2, x + 16, Config.ItemSize);
                if (NewerPos > -1 && OlderPos > -1)
                {
                    if (OlderEdit != null && NewerEdit != null)
                    {
                        if (OlderEdit.Id == NewerEdit.Prev.Id)
                        {
                            gf.DrawRectangle(new Pen(Color.Red, 2), OlderPos, 1, 35, 18);
                        }
                    }
                }
            }

        }

        public HistoryStrip()
        {
            InitializeComponent();
        }
    }
}
