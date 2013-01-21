//This is a source code or part of Huggle project
//
//This file contains code for dispatcher

/// <DOCUMENTATION>
/// Feed dispatcher is thing responsible for parsing the feed.
/// 
/// There is a one global instance that is created when you login to huggle, based on configuration it attempt to connect
/// to irc, or uses api to parse the feed and forward it to other classes responsible for them
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
using System.Xml;
using System.Text;

namespace huggle3
{
    public class Feed
    {
        public bool UsingIRC = false;
        public IRC irc;
        public int FeedTh = 0;

        public void Manual()
        {
            Core.WriteLog("We don't have irc feed, let's do the things from api");
            irc = null;
            FeedTh = Core.Threading.CreateThread(Feeder, "Feeder");
            Core.Threading.Execute(FeedTh);
        }

        private void Irc()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 10000);
            irc = new IRC(Config.IrcServer, Config.IrcPort, "huggle3_" + DateTime.Now.ToBinary().ToString().Substring(10) + randomNumber.ToString(), Config.IrcChannel);
        }

        /// <summary>
        /// Creates a feed dispatcher
        /// </summary>
        public Feed(bool IRC)
        {
            Core.WriteLog("Creating a feed of changes");
            UsingIRC = IRC;
            if (!IRC)
            {
                Manual();
            }
            else
            {
                Irc();
            }
        }

        private void Feeder()
        {
            while (true)
            {
                try
                {
                    // retrieve a list of edits

                }
                catch (System.Threading.ThreadAbortException)
                {
                    return;
                }
                catch (Exception fail)
                {
                    Core.ExceptionHandler(fail);
                }
            }
        }
    }
}
