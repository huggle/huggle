﻿//This is a source code or part of Huggle project
//
//This file contains code for edit requests

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
using System.Text;

namespace huggle3.Requests
{
    public class request_edit : request_core.Request
    {
        public Page page = null;
        public string text = null;
        public string summary = null;
        public bool minor = false;
        public bool watch = false;
        public bool noautosummary = false;

        public override void Process()
        {
            ApiResult result = null;
        }
    }
}
