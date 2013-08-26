//This is a source code or part of Huggle project
//
//This file contains code for core

/// <DOCUMENTATION>
/// This file is containing git gateway
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
using System.Reflection;
using System.IO;
using System.Text;


namespace Huggle3LX
{
    class RevisionProvider
    {
        public static string GetHash()
        {
            try
            {
                using (var stream = Assembly.GetExecutingAssembly()
                                            .GetManifestResourceStream(
                                            "Huggle3LX" + "." + "version.txt"))
                using (var reader = new StreamReader(stream))
                {
                    string result = reader.ReadLine();
                    if (!reader.EndOfStream)
                    {
                        result += " [" + reader.ReadLine() + "]";
                    }
                    return result;
                }
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
            return "";
        }
    }
}
