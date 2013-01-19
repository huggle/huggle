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
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Text;

namespace huggle3
{
    public class IRC
    {
        /// <summary>
        /// Tells you if you are connected to network
        /// </summary>
        public bool Connected;
        /// <summary>
        /// Name of server
        /// </summary>
        public string Network = null;
        /// <summary>
        /// Port
        /// </summary>
        public int Port = 6667;
        /// <summary>
        /// Nickname
        /// </summary>
        public string Nick = null;
        /// <summary>
        /// ID of thread
        /// </summary>
        public int thread;
        /// <summary>
        /// ID of pinger
        /// </summary>
        private int pt;
        /// <summary>
        /// Connectiong class
        /// </summary>
        private System.Net.Sockets.NetworkStream Connection;
        /// <summary>
        /// Stream reader
        /// </summary>
        private StreamReader SR = null;
        private StreamWriter SW = null;
        public static Regex line =
            new Regex(":rc-pmtpa!~rc-pmtpa@[^ ]* PRIVMSG #[^:]*:14\\[\\[07([^]*)14\\]\\]4 (M?)(B?)10 02.*di" +
                      "ff=([^&]*)&oldid=([^]*) 5\\* 03([^]*) 5\\* \\(?([^]*)?\\) 10([^]*)?");

        public static List<string> Channels = new List<string>();


        public void Join(string channel)
        { 
            
        }

        /// <summary>
        /// Creates an irc class and start a connection
        /// </summary>
        /// <param name="network">Network you want to connect to</param>
        /// <param name="port">Port of the server</param>
        /// <param name="nick">Nickname to use</param>
        /// <param name="channel">Channel to join</param>
        public IRC(string network, int port, string nick, string channel)
        {
            try
            {
                Nick = nick;
                Port = port;
                Channels.Add(channel);
                Network = network;
                thread = Core.Threading.CreateThread(Run, "IRC");
                Core.Threading.Execute(thread);
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        public void Pinger()
        {
            try
            {
                while (true)
                {
                    Send("PING");
                    Thread.Sleep(20000);
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        private void Send(string text)
        {
            SW.WriteLine(text);
            SW.Flush();
        }

        private void Connect()
        {
            Core.WriteLog("IRC: connecting to " + Network + ":" + Port);
            Connection = new System.Net.Sockets.TcpClient(Network, Port).GetStream();
            SW = new StreamWriter(Connection);
            SR = new StreamReader(Connection, System.Text.Encoding.UTF8);
            pt = Core.Threading.CreateThread(Pinger, "Pinger for wm feed");
            Core.Threading.Execute(pt);
            Send("USER " + "huggle3" + " 8 * :" + "huggle3");
            Send("NICK " + Nick);
        }

        private void Run()
        {
            try
            {
                Connect();

            }
            catch (ThreadAbortException)
            {
                Core.WriteLog("IRC thread was requested to stop");
                Connected = false;
                return;
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }
    }
}
