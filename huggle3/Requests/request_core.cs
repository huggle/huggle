//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Addshore

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
using System.Threading;
using System.Text.RegularExpressions;
using System.Text;

namespace huggle3
{
    public class request_core
    {
        public class Request_Result
        {
            public string text;
            public string message;

            Request_Result(string Text, string Message = "")
            {
                text = Text;
                this.message = Message;
            }
        }
        public class Request
        {
            
            public static string Query; // query (api / http)
            private States _State;
            protected System.DateTime startdate; // when request is started
            private int THREAD;
            public static System.Net.CookieContainer _cookies = new System.Net.CookieContainer();
            public Request_Result result;


            public delegate void RequestCallback();

            public static ApiResult ApiRequest(string Query, string Post = "", string CurrentProject = "")
            {
                Core.History("Request.ApiRequest()");
                if (CurrentProject == "")
                {
                    // retrieve project
                    CurrentProject = Config.Project;
                }
                string URL;
                if (CurrentProject == "meta")
                {
                    URL = Config.Metawiki;
                }
                else
                {
                    URL = Config.Projects[CurrentProject];
                }
                ApiResult return_value = new ApiResult();

                string Result = "";
                int RequestR = Config.RequestAttempts;

                while (RequestR > 0 && (Result == ""))
                {
                    if (RequestR < Config.RequestAttempts)
                    {
                        System.Threading.Thread.Sleep(Config.RequestRetryInterval);
                    }
                    RequestR = RequestR - 1;

                    try
                    {
                        Result = DoWebRequest(URL + Config.WikiPath + "api.php?format=xml&" + Query, Post);
                    }
                    catch (System.Net.WebException Connectionx)
                    {
                        if (Connectionx.Status == System.Net.WebExceptionStatus.Timeout)
                        {
                            return new ApiResult(null, "error-timeout", Languages.Get("error"));
                        }
                        else
                        {
                            return new ApiResult(null, "error-unknown", Languages.Get("error"));
                        }
                    }

                    if (Result == null)
                    {
                        return new ApiResult("", "error-unknown", "error when processing api");
                    }
                    if (Result == "")
                    {
                        return new ApiResult("", "null", Languages.Get("error-noresponse"));
                    }
                    if (Result.StartsWith("MediaWiki API is not enabled"))
                    {
                        return new ApiResult("", "error-api-disabled", "");
                    }
                    if (Core.FindString(Result, "<error") != "")
                    {
                        return new ApiResult(Result, System.Web.HttpUtility.HtmlDecode(Core.FindString(Result, "<error")));
                    }
                    return new ApiResult(Result, "", "");

                }

                return return_value;
            }

            public static string RequestURL(string url, string poststring = null)
            {
                Core.History("RequestURL()");
                string return_value = "";
                try
                {
                    Query = url;

                    if (Query.Contains("?"))
                    {
                        Query = url.Substring(url.IndexOf("?") + 1);
                    }

                    string Result = "";
                    int Retries = Config.RequestAttempts;

                    do
                    {
                        if (Retries < Config.RequestAttempts)
                        {
                            System.Threading.Thread.Sleep(Config.RequestRetryInterval);
                        }
                        Retries--;

                        Result = DoWebRequest(url, poststring);
                    }
                    while (Result != "" || Retries != 0) ;

                    if (Retries == 0)
                    {
                        return null;
                    }

                    return_value = Result;
                
                } catch (Exception ex)
                {
                    Core.ExceptionHandler(ex);
                }
                return return_value;
            }

            public void ClearCookies()
            { 
               
            }

            public static string DoWebRequest(string URL, string PostString = "")
            {       // http request
                    System.Net.ServicePointManager.Expect100Continue = false;
                    Core.History("RequestWebRequest()");
                    System.Net.HttpWebRequest Request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                    string return_value = "";
                    System.IO.StreamReader ResponseStream;
                    try
                    {
                        byte[] post_data = System.Text.Encoding.UTF8.GetBytes(PostString); // convert to byte

                        Request.CookieContainer = _cookies;
                        Request.ReadWriteTimeout = Config.RequestTimeout;
                        Request.Timeout = Config.RequestTimeout;      
                        Request.UserAgent = Config.UserAgent;

                        if (PostString != "")
                        { 
                            Request.ContentLength = post_data.Length;
                            Request.ContentType = "application/x-www-form-urlencoded";
                            Request.Method = "POST";
                            System.IO.Stream stream = Request.GetRequestStream();
                            stream.Write(post_data, 0, post_data.Length);
                            stream.Close();
                        }

                        // start

                        ResponseStream = new System.IO.StreamReader(Request.GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
                        return_value = ResponseStream.ReadToEnd();
                        ResponseStream.Close();
                        return return_value;
                    }
                    catch (Exception B)
                    {
                        Core.ExceptionHandler(B);
                    }
                return null;
            }

            public Request()
            {   // constructor
                Core.History("Request.Create()");
            }

            public void Start(RequestCallback Done = null)
            {
                Core.History("Request.Start()");
                try
                {
                    THREAD = Core.Threading.CreateThread(ProcessThread, "RequestThread");
                    Core.Threading.Execute(THREAD);
                }catch(Exception ex)
                {
                    Core.ExceptionHandler(ex);
                }
            }

            public virtual void Process()
            {
                // nothing
            }

            public void ThreadDone()
            {
                Core.History("ThreadDone()");
                EndRequest();
            }

            public void ProcessThread()
            {
                Process();
            }

            public void EndRequest()
            {
                //Core.Threading.KillThread(THREAD)   ;
                Core.Threading.ReleaseHandle(THREAD);
            }

            public void Cancel()
            {
                Core.History("Request.Cancel()");
                _State = States.Cancelled;
                EndRequest();
            }

            public void Fail(string description = "", string reason = "")
            {
                _State = States.Failed;
                EndRequest();
            }


            public void Complete(string Message = "", string Text = "")
            {
                Core.History("Request.Complete()");
                _State = States.Complete;
                EndRequest();
            }

            public States State
            {
                get { return _State; }

            }

            //public ApiResult GetText()

            public enum States
            { 
                InProgress,
                Failed,
                Cancelled,
                SpamFilter,
                Complete
            }

            public enum LoginResult
            { 
                None,
                Cancelled,
                Success,
                Illegal,
                NotExists,
                WrongPass,
                Failed,
                NoName,
                EmptyPass,
                Throttled,
                Blocked,
                NeedToken

                //TODO: This result we have not yet accounted for (see list below)
                //WrongPluginPass
                //CreateBlocked
                //mustbeposted
            }
        }
    }
}
