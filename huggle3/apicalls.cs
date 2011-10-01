//This is a source code or part of Huggle project
//
//This file contains code for
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

/* This is strictly just an in-development file, and it may be burned with fire if the time comes.
 * Please ask 123Hedgehog456 if you'd like to know why a big mess is inside a file :P
 * And it's not finished yet.
 */
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
namespace huggle3
{
    public class apicalls
    {
        public static XmlDocument BaseApiCall(string getdata, string postdata)
        {
            var baserequest = (HttpWebRequest)WebRequest.Create("http://hub.tm-irc.org/test/w/api.php?format=xml&" + getdata);
            baserequest.UserAgent = "Huggle3 Integrated Editing Interface, Semi-Automated";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(postdata);
            baserequest.ContentLength = byte1.Length;
            baserequest.Method = "POST";
            Stream basestream = baserequest.GetRequestStream();
            basestream.Write(byte1, 0, byte1.Length);
            WebResponse baseresponse = baserequest.GetResponse();
            basestream.Close();
            Stream basestream2 = baseresponse.GetResponseStream();
            XmlDocument basedoc = new XmlDocument();
            basedoc.Load(basestream2);
            basestream2.Close();
            return basedoc;
        }
        /*		public static string LoginAPICall (string username, string password) {
                    var basexml = BaseApiCall("", "action=login&lgname=" + username + "&lgpassword=" + password);
                    switch (basexml.Item["result"]) {
                        case "Success":
                            return "true";
                            break;
                        case "NeedToken":
                                retryxml = BaseApiCall("", "action=login&lgname=" + username + "&lgpassword=" + password + "&lgtoken=" + basexml.Item["token"]);
                                if (retryxml.Item["result"] = "Success") {
                                    return "true";
                                    break;
                                }
                                else if (retryxml.Item["result"] = "NeedToken") {
                                    return "Something really weird's happening. Try again later.";
                                    break;
                                }
                                else if (retryxml.Item["result"] = "Blocked") {
                                    goto case "Blocked";
                                }
                                else if (retryxml.Item["result"] = "CreateBlocked") {
                                    goto case "CreateBlocked";
                                }
                                else if (retryxml.Item["result"] = "EmptyPass") {
                                    goto case "EmptyPass";
                                }
                                else if (retryxml.Item["result"] = "WrongPass") {
                                    goto case "WrongPass";
                                }
                                else if (retryxml.Item["result"] = "Illegal") {
                                    goto case "Illegal";
                                }
                                else if (retryxml.Item["result"] = "NoName") {
                                    goto case "NoName";
                                }
                                else if (retryxml.Item["result"] = "NotExists") {
                                    goto case "NotExists";
                                }
                                else if (retryxml.Item["result"] = "Throttled") {
                                    goto case "Throttled";
                                }
                        case "Blocked":
                            return "Looks like your account is blocked. You can appeal this block.";
                            break;
                        case "CreateBlocked":
                            return "The wiki tried to make an account for you but your IP address is blocked.";
                            break;
                        case "EmptyPass":
                            return "Please enter a password.";
                            break;
                        case "WrongPass":
                            return "Wrong password, please try again.";
                            break;
                        case "Illegal":
                            return "There's something fishy about that username.";
                            break;
                        case "NoName":
                            return "Please enter a username.";
                            break;
                        case "NotExists":
                            return "That account does not exist. Please make an account before using Huggle.";
                            break;
                        case "Throttled":
                            return "Throttle Error. Please wait for a few minutes before trying again.";
                            break;
                    }
			
			
                }
            } */
    }
}

