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
    /// <summary>
    /// Languages
    /// </summary>
    public static class Languages
    {
        /// <summary>
        /// Localize the specified text.
        /// </summary>
        /// <param name="text">Text.</param>
        public static string Localize(string text)
        {
            if (text.StartsWith("[["))
            {
                return Get(text);
            }
            return text;
        }

        /// <summary>
        /// Localize the specified form.
        /// </summary>
        /// <param name="form">Form.</param>
        public static void Localize(Gtk.Window form)
        {
            try
            {
                foreach (Gtk.Widget widget in form.Children)
                {
                    LocalizeWidget(widget);
                }
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        /// <summary>
        /// Localizes the widget.
        /// </summary>
        /// <param name="widget">Widget.</param>
        public static void LocalizeWidget(Gtk.Widget widget)
        {
            if (widget.GetType() == typeof(Gtk.Label))
            {
                Gtk.Label label = (Gtk.Label)widget;
                label.Text = Localize(label.Text);
            }

            if (widget.GetType() == typeof(Gtk.CheckButton))
            {
                Gtk.CheckButton checkButton = (Gtk.CheckButton)widget;
                checkButton.Label = Localize(checkButton.Label);
            }
            LocalizeWChildrens(widget);
        }

        /// <summary>
        /// Localizes the W childrens.
        /// </summary>
        /// <param name="widget">Widget.</param>
        public static void LocalizeWChildrens(Gtk.Widget widget)
        {
            if (widget.GetType() == typeof(Gtk.VBox))
            {
                foreach (Gtk.Widget ch in ((Gtk.VBox)widget).Children)
                {
                    LocalizeWidget(ch);
                }
            }

            if (widget.GetType() == typeof(Gtk.HBox))
            {
                foreach (Gtk.Widget ch in ((Gtk.HBox)widget).Children)
                {
                    LocalizeWidget(ch);
                }
            }

            if (widget.GetType() == typeof(Gtk.Table))
            {
                foreach (Gtk.Widget ch in ((Gtk.Table)widget).Children)
                {
                    LocalizeWidget(ch);
                }
            }
        }

        /// <summary>
        /// Get the specified id.
        /// </summary>
        /// <param name="id">Identifier.</param>
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
