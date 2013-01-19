//This is a source code or part of Huggle project
//
//This file contains code for irc
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
using System.Text;

namespace huggle3
{
    public class irc
    {
        public bool Connected;
        public string Network = null;
        public int thread;
        public bool IrcConnect()
        {
            Core.History("IrcConnect()");
            thread = Core.Threading.CreateThread(Irc, "Irc");
            Core.Threading.Execute(thread);

            return false;
        }

        static void Irc()
        {

        }
    }

    public static class rc
    {
        private static irc Network;
        public static void IrcConnect()
        {
            rc.Network = new irc();
            rc.IrcConnect();
        }
            
    }
}
