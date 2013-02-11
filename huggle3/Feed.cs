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
        private string url = null;

        public void Manual()
        {
            Core.History("Feed.Manual");
            Core.WriteLog("We don't have irc feed, let's do the things from api");
            irc = null;
            url = MWStandardApi.RCtoXML.Replace("$COUNT", Config.FeedSize.ToString());
            FeedTh = Core.Threading.CreateThread(Feeder, "Feeder");
            Core.Threading.Execute(FeedTh);
        }

        /// <summary>
        /// RC feed
        /// </summary>
        private void Irc()
        {
            Core.History("Feed.Irc");
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

        /// <summary>
        /// This is a thread core which gets a feed
        /// </summary>
        private void Feeder()
        {
            while (true)
            {
                try
                {
                    // retrieve a list of edits
                    ApiResult result = request_core.Request.ApiRequest(url);
                    if (result.ResultInError)
                    {
                        Core.DebugLog("Unable to parse the feed from api: " + result.Error_Code + result.Error_Data);
                    }
                    else
                    {
                        XmlDocument data = new XmlDocument();
                        data.LoadXml(result.Result_Text);

                        // parse each item
                        foreach (XmlNode node in data.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes)
                        {
                            if (node.Attributes == null)
                            {
                                continue;
                            }
                            string type = null;
                            string title = null;
                            string ns = null;
                            string rcid = null;
                            string pageid = null;
                            string revid = null;
                            string old_revid = null;
                            string timestamp = null;
                            string comment = null;
                            string user = null;
                            string user_id = "0";
                            string oldlen = null;
                            string newlen = null;
                            string parsedcomment = null;
                            // <rc type="edit" ns="0" title="Melanie Kannokada" rcid="550573236" pageid="35857502" revid="534301614" 
                            //old_revid="534125323" user="Ileana Cruz" userid="16966521" oldlen="5531" newlen="5787" 
                            //timestamp="2013-01-22T08:56:08Z" comment="" parsedcomment=""><tags /></rc>
                            foreach (XmlAttribute value in node.Attributes)
                            {
                                switch (value.Name)
                                { 
                                    case "comment":
                                        comment = value.Value;
                                        break;
                                    case "user_id":
                                        user_id = value.Value;
                                        break;
                                    case "oldlen":
                                        oldlen = value.Value;
                                        break;
                                    case "user":
                                        user = value.Value;
                                        break;
                                    case "newlen":
                                        newlen = value.Value;
                                        break;
                                    case "parsedcomment":
                                        parsedcomment = value.Value;
                                        break;
                                    case "type":
                                        type = value.Value;
                                        break;
                                    case "ns":
                                        ns = value.Value;
                                        break;
                                    case "title":
                                        title = value.Value;
                                        break;
                                    case "rcid":
                                        rcid = value.Value;
                                        break;
                                    case "pageid":
                                        pageid = value.Value;
                                        break;
                                    case "revid":
                                        revid = value.Value;
                                        break;
                                    case "old_revid":
                                        old_revid = value.Value;
                                        break;
                                    case "timestamp":
                                        timestamp = value.Value;
                                        break;
                                }
                            }
                            if (type == "edit")
                            {
                                Edit edit = new Edit();
                                edit._Page = new Page(title);
                                edit.Rcid = rcid;
                                edit.Oldid = old_revid;
                                edit.Summary = comment;
                                edit.EditUser = new huggle3.User(user, int.Parse(user_id));
                                edit.Change = int.Parse(oldlen) - int.Parse(newlen);
                                edit.Time = DateTime.Parse(timestamp);
                                Queue.Enqueue(edit);
                            }
                        }
                    }
                    System.Threading.Thread.Sleep(10 * Config.RefreshInterval);
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
