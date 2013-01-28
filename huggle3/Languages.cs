//This is a source code or part of Huggle project
//
//This file contains code for languages

/// <DOCUMENTATION>
/// Languages are parsed from internal source file, they are all in simple format key: text
/// key needs to be unique, otherwise first one is parsed. Non-existent keys will result
/// in coder-friendly borked word. If you see that, it means there is no definition for that
/// key in the default language.
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
using System.Text;
using System.Windows.Forms;

namespace huggle3
{
    public static class Languages
    {
        public static List<Control> GetControls(Control form)
        {
            var controlList = new List<Control>();

            foreach (Control childControl in form.Controls)
            {
                // Recurse child controls.
                controlList.AddRange(GetControls(childControl));
                controlList.Add(childControl);
            }
            return controlList;
        }

        public static List<ToolStripMenuItem> GetMenu(ToolStripMenuItem form)
        {
            var controlList = new List<ToolStripMenuItem>();

            foreach (ToolStripItem childControl in form.DropDownItems)
            {
                // Recurse child controls.
                if (typeof(ToolStripMenuItem) == childControl.GetType())
                {
                    controlList.AddRange(GetMenu((ToolStripMenuItem)childControl));
                    controlList.Add((ToolStripMenuItem)childControl);
                }
            }
            return controlList;
        }

        public static void Localize(Form form)
        {
            try
            {
                lock (form.Controls)
                {
                    foreach (Control control in GetControls(form))
                    {
                        if (control.Text.StartsWith("[["))
                        {
                            control.Text = Get(control.Text);
                        }
                        if (control.GetType() == typeof(MenuStrip))
                        {
                            MenuStrip menu = (MenuStrip)control;
                            foreach (ToolStripMenuItem item in menu.Items)
                            {
                                if (item.Text.StartsWith("[["))
                                {
                                    item.Text = Get(item.Text);
                                }
                                foreach(ToolStripMenuItem item2 in GetMenu(item))
                                if (item2.Text.StartsWith("[["))
                                {
                                    item2.Text = Get(item2.Text);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }


        public static string Get(string id)
        {
            try
            {
                if (id.Contains("["))
                {
                    id = id.Replace("]", "");
                    id = id.Replace("[", "");
                }
                // return string
                if (Config.Messages.ContainsKey(Config.DefaultLanguage) != true && Config.Messages.ContainsKey(Config.Language) != true)
                {
                    return "<invalid language:" + id + ">";
                }
                if (Config.Messages[Config.Language].ContainsKey(id) == false)
                { // if there is no such a language it returns the english one
                    if (Config.Messages.ContainsKey(Config.DefaultLanguage) != true)
                    {
                        return "<not present in dict$" + id +">";
                    }
                    if (Config.Messages[Config.DefaultLanguage].ContainsKey(id))
                    {
                        return Config.Messages[Config.DefaultLanguage][id];
                    }
                    else
                    {
                        return "<invalid string:" + id + ">";
                    }
                }
                else
                {
                    // got it
                    if (Config.Messages[Config.Language].ContainsKey(id))
                    {
                        return Config.Messages[Config.Language][id];
                    }
                    return "<invalid string:" + id + ">";
                }
            }
            catch (Exception A)
            {
                if (Config.devs)
                {
                    // This is ignored on production build
                    Core.ExceptionHandler(A);
                }
            }
            return "<invalid string:" + id + ">";
        }
    }
}
