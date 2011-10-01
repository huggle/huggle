//This is a source code or part of Huggle project
//
//This file contains code
//last modified by Petrb

//Copyright (C) 2011 Huggle team
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

namespace huggle3
{
    public static class Processing
    {
        public static int ProcessEdit(edit _Edit)
        {
            Core.History("Processing.ProcessEdit(new edit)");
            if (_Edit.Oldid == null)
            { _Edit.Oldid = "prev"; }

            if (_Edit.Bot == true)
            {
                _Edit.User.Bot = true;
            }

            if (Config.PageBlankedPattern != null)
            {
                if (Config.PageBlankedPattern.IsMatch("") || _Edit.Size == 0)
                {
                    _Edit.type = edit.EditType.Blanked;
                }
            }
            if (Config.PageRedirectedPattern != null)
            {
                if (Config.PageRedirectedPattern.IsMatch(_Edit.Summary))
                {
                    _Edit.type = edit.EditType.Redirect;
                }
            }
            if (Config.PageReplacedPattern != null)
            {
                if (Config.PageReplacedPattern.IsMatch(_Edit.Summary))
                {
                    _Edit.type = edit.EditType.ReplacedWith;
                }
            }
            if (Config.Summary != "" && _Edit.Summary.EndsWith(Config.Summary) && _Edit.Summary != "")
            {
                _Edit.Assisted = true;
            }

            return 0;
        }
        public static bool ProcessNewEdit(edit _Edit)
        {
            Core.History("ProcessEdit( _Edit )");
            if (_Edit.User == null || _Edit.Page == null)
            {
                return false;
            }
            bool Redraw = false;

            if ( _Edit.Page.LastEdit != null )
            {
                _Edit.Prev.Next = _Edit;
                _Edit.Prev = _Edit.Page.LastEdit;

                if ( _Edit.Prev.Size >= 0 && _Edit.Change != 0 )
                {
                    _Edit.Size = _Edit.Prev.Size + _Edit.Change;
                }
                if (_Edit.Change != 0 && _Edit.Size >= 0)
                {
                    _Edit.Prev.Size = _Edit.Size - _Edit.Change;
                }
            }

            if ( _Edit.User == null )
            {
                _Edit.User = _Edit.Page.LastEdit.User;
            }

            if (_Edit.User.LastEdit != null)
            {
                _Edit.PrevByUser = _Edit.User.LastEdit;
                _Edit.PrevByUser.NextByUser = _Edit;
            }

            _Edit.Page.Exists = true;
            _Edit.Page.Text = null;
            _Edit.Page.SpeedyCrit = null;
            _Edit.Page.LastEdit = _Edit;
            _Edit.User.LastEdit = _Edit;

            

            return true;
        }

    }
}
