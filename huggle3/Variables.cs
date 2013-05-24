//This is a source code or part of Huggle project
//
//This file contains code for misc

/// <DOCUMENTATION>
/// This is what used to be misc in older huggle
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

namespace huggle3
{
    class Variables
    {
        public static Dictionary<Page, string> CustomReverts = null;
        /// <summary>
        /// List of pending warnings
        /// </summary>
        public static List<Edit> PendingWarnings = new List<Edit>();
        /// <summary>
        /// Edit token which is currently useable
        /// </summary>
        public static string EditToken = null;
        
        /// <summary>
        /// Initalise class to its default values
        /// </summary>
        public static void Reset()
        {
            CustomReverts = new Dictionary<Page, string>();
        }
    }
}
