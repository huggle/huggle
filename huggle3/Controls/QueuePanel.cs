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
        private Queue _Queue = null;

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
                    Rebuild();
                }
            }
            get
            {
                return _Queue;
            }
        }
        private int OffsetX = 0;

        public void Remove(int index)
        {
            try
            {
                lock (List)
                { 
                    if (List.Count < index)
                    {
                        // there is no such thing to remove, skip
                        return;
                    }
                    List[index].Visible = false;
                    List.RemoveAt(index);
                }
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        /// <summary>
        /// Move the first edit from queue into return value
        /// </summary>
        /// <returns></returns>
        public Edit GetNext()
        {
            lock (List)
            {
                if (List.Count > 0)
                {
                    Edit edit = List[0].Edit;
                    Remove(0);
                    return edit;
                    
                }
            }
            return null;
        }

        public void Remove(Edit edit)
        {
            lock (List)
            {
                List<EditItem> remove = new List<EditItem>();
                foreach (EditItem curr in List)
                {
                    if (curr.Edit == edit)
                    {
                        remove.Add(curr);
                    }
                }
                foreach (EditItem curr in remove)
                {
                    curr.Visible = false;
                    if (Controls.Contains(curr))
                    {
                        Controls.Remove(curr);
                    }
                    curr.Dispose();
                    List.Remove(curr);
                }
                Redraw();
            }
        }

        public void Rebuild()
        {
            Core.History("QueuePanel.Rebuild");
            lock (List)
            {
                Clear();
                if (_Queue != null)
                {
                    lock (_Queue.Edits)
                    {
                        foreach (Edit xx in _Queue.Edits)
                        {
                            Add(xx);
                        }
                    }
                }
            }
        }

        public void Add(Edit Edit)
        {
            try
            {
                lock (List)
                { 
                    List.Insert(0, new EditItem(Edit._Page, Edit));
                }
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        public void UpdateScroll()
        {
            try
            {
                if ((List.Count * 22) > this.Height)
                {
                    vScrollBar1.Enabled = true;
                    int maximum = List.Count * 22 - this.Height;
                    if (maximum < 1)
                    {
                        maximum = 1;
                    }
                    vScrollBar1.Maximum = maximum + 100;
                }
                else
                {
                    vScrollBar1.Enabled = false;
                }
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        public void Clear()
        {
            try
            {
                foreach (EditItem curr in List)
                {
                    if (Controls.Contains(curr))
                    {
                        Controls.Remove(curr);
                    }
                    curr.Dispose();
                }
                List.Clear();
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
                        if ((X + 20) > 0 && (X - 20) < this.Height)
                        {
                            curr.Top = X;
                            curr.Left = 2;
                            curr.Width = this.Width - 20;
                            curr.Repaint(null, null);
                            curr.Visible = true;
                        }
                        else
                        {
                            curr.Visible = false;
                        }
                        X = X + curr.Height + 2;
                    }
                    UpdateScroll();
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

        public void QueuePanel_Change(object sender, EventArgs e)
        {
            Redraw();
        }

        private void QueuePanel_Load(object sender, EventArgs e)
        {
            vScrollBar1.Enabled = false;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            OffsetX = vScrollBar1.Value;
            Redraw();
        }
    }
}
