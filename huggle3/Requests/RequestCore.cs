//This is a source code or part of Huggle project
//
//This file contains code for requests

/// <DOCUMENTATION>
/// Huggle is using so called requests in order to download data
/// 
/// Each request is a class inherited from main core request class with bunch of virtual functions
/// the request basically consist of some initial functions and special function Process which
/// spawn a new thread which handle the request itself.
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
using System.Threading;
using System.Text.RegularExpressions;
using System.Text;

namespace huggle3
{
    /// <summary>
    /// Request core
    /// </summary>
    public class RequestCore
    {
        /// <summary>
        /// Request result
        /// </summary>
        public class RequestResult
        {
            /// <summary>
            /// The text.
            /// </summary>
            public string text;
            /// <summary>
            /// The message.
            /// </summary>
            public string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="huggle3.RequestCore+RequestResult"/> class.
            /// </summary>
            /// <param name="Text">Text.</param>
            /// <param name="Message">Message.</param>
            public RequestResult(string Text, string Message = "")
            {
                text = Text;
                message = Message;
            }
        }
        
        public class Request
        {
            /// <summary>
            /// query for api
            /// </summary>
            public static string Query;
            /// <summary>
            /// Status
            /// </summary>
            private Status _State;
            /// <summary>
            /// When request is started
            /// </summary>
            protected System.DateTime startdate;
            /// <summary>
            /// ID of thread
            /// </summary>
            private int THREAD;
            /// <summary>
            /// Cookie container for request
            /// </summary>
            public static System.Net.CookieContainer _cookies = new System.Net.CookieContainer();
            /// <summary>
            /// Value
            /// </summary>
            public RequestResult result;
            /// <summary>
            /// Function that is used as return point
            /// </summary>
            public delegate void RequestCallback();
            /// <summary>
            /// Function that is used as return point
            /// </summary>
            public RequestCallback Callback;
            
            /// <summary>
            /// Perform API request
            /// </summary>
            /// <param name="Query">Query data</param>
            /// <param name="Post">Post string</param>
            /// <param name="CurrentProject"></param>
            /// <returns></returns>
            public static ApiResult ApiRequest(string Query, string Post = "", string CurrentProject = "")
            {
                Core.History("Request.ApiRequest()");
                if (CurrentProject == "")
                {
                    // retrieve project in case we don't know it
                    CurrentProject = Config.Project;
                }
                string URL; // url of request
                if (CurrentProject == "meta")
                {
                    URL = Config.Metawiki; // meta has specific url so we need to put it from config file
                }
                else
                {
                    URL = Config.Projects[CurrentProject];
                }
                
                ApiResult return_value = new ApiResult();
                
                string Result = "";
                int Retries = Config.RequestAttempts;
                
                while (Retries > 0 && (Result == ""))
                {
                    if (Retries < Config.RequestAttempts)
                    {
                        System.Threading.Thread.Sleep(Config.RequestRetryInterval); // timeout before requests to save resources
                    }
                    Retries--;
                    
                    try
                    {
                        // Perform a request now and catch all exceptions caused by connection
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
            
            /// <summary>
            /// Return a value of request
            /// </summary>
            /// <param name="url"></param>
            /// <param name="poststring"></param>
            /// <returns></returns>
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
                    while (Result == "" && Retries > 0) ;
                    
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
            
            /// <summary>
            /// Clear
            /// </summary>
            public void ClearCookies()
            {
                _cookies = new System.Net.CookieContainer(); // let GC do it
            }
            
            public ApiResult PostEdit(Page page, string text, string reason, string section = null, bool minor = false, bool watch = false, bool SuppressSummary = false, bool Create = true)
            { 
                ApiResult result = new ApiResult(null, null, null);
                
                string token = null;
                bool InvalidToken = false;
                
                while (!InvalidToken)
                {
                    while (token == null)
                    {
                        if (section != null || Variables.EditToken == null)
                        {
                            result = ApiRequest("action=query&prop=info&intoken=edit&titles=" + System.Web.HttpUtility.UrlEncode(page.Name));
                            if (result.ResultInError)
                            {
                                return result;
                            }
                            token = GetParameter(result.Text, "edittoken");
                        }
                    }
                }
                
                return result;
            }

            /// <summary>
            /// Start web req
            /// </summary>
            /// <param name="URL">Url of request</param>
            /// <param name="PostString">Post data</param>
            /// <param name="OverrideSsl">If we need to turn off ssl</param>
            /// <returns></returns>
            public static string DoWebRequest(string URL, string PostString = "", bool OverrideSsl = false)
            {       // http request
                System.Net.ServicePointManager.Expect100Continue = false;
                Core.History("RequestWebRequest()");
                if (OverrideSsl != true)
                {
                    if (Config.UseSsl)
                    {
                        if (URL.Contains("http://") != false)
                        {
                            URL = URL.Replace("http://", "https://");
                        }
                    }
                }
                System.Net.HttpWebRequest Request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                string return_value = "";
                System.IO.StreamReader ResponseStream;
                try
                {
                    byte[] post_data; // convert to byte
                    if (PostString != null)
                    {
                        post_data = System.Text.Encoding.UTF8.GetBytes(PostString);
                    }
                    else
                    {
                        post_data = System.Text.Encoding.UTF8.GetBytes("");
                    }
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
            
            /// <summary>
            /// Constructor
            /// </summary>
            public Request()
            {
                Core.History("Request.Create()");
            }
            
            /// <summary>
            /// Return a parameter of request
            /// </summary>
            /// <param name="source">Source text</param>
            /// <param name="parameter">String</param>
            /// <returns></returns>
            public static string GetParameter(string source, string parameter)
            {
                Core.History("GetParameter()");
                if (parameter == null) return null;
                if (source == null) return null;
                
                if (source.Contains(parameter + "=") == false)
                {
                    return null;
                }
                
                if (source.Contains("\"") != true)
                {
                    return null;
                }
                string return_value = source.Substring(source.IndexOf(parameter + "=\"") + parameter.Length + 2);
                
                return System.Web.HttpUtility.HtmlDecode(return_value);
            }
            
            /// <summary>
            /// Start
            /// </summary>
            /// <param name="Done"></param>
            public void Start(RequestCallback Done = null)
            {
                Callback = Done;
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
            
            /// <summary>
            /// This is a function which needs to be overriden by request, in case it's not do nothing
            /// </summary>
            public virtual void Process()
            {
                // nothing
            }
            
            /// <summary>
            /// This is a function which is called when request is done, by default it log to core
            /// </summary>
            public virtual void ThreadDone()
            {
                Core.History("ThreadDone()");
                EndRequest();
            }
            
            /// <summary>
            /// This is a function started before creation of new thread and executing it
            /// </summary>
            public void ProcessThread()
            {
                _State = Status.InProgress;
                Process();
            }
            
            /// <summary>
            /// This is function which starts on every correct end of request
            /// </summary>
            public virtual void EndRequest(bool terminate = false)
            {
                if (terminate)
                {
                    Core.Threading.KillThread(THREAD);
                }
                if (Callback != null)
                {
                    Callback();
                }
                Core.Threading.ReleaseHandle(THREAD);
            }
            
            /// <summary>
            /// Cancel all
            /// </summary>
            public void Cancel()
            {
                Core.History("Request.Cancel()");
                _State = Status.Cancelled;
                EndRequest( true );
            }
            
            /// <summary>
            /// This is function which starts on every correct end of request
            /// </summary>
            public virtual void EndRequest()
            {
                if (Callback != null)
                {
                    Callback();
                }
                Core.Threading.ReleaseHandle(THREAD);
            }
            
            /// <summary>
            /// Called on fail of request we do
            /// </summary>
            /// <param name="description">Description of error</param>
            /// <param name="reason">Why it fallen to error</param>
            public virtual void Fail(string description = "", string reason = "")
            {
                _State = Status.Failed;
                EndRequest();
            }
            
            /// <summary>
            /// Called when the request is completed, before delivering result
            /// </summary>
            /// <param name="Message"></param>
            /// <param name="Text"></param>
            public virtual void Complete(string Message = "", string Text = "")
            {
                Core.History("Request.Complete()");
                result = new RequestResult(Text, Message);
                _State = Status.Complete;
                EndRequest();
            }
            
            /// <summary>
            /// Property returning status
            /// </summary>
            public Status State
            {
                get { return _State; }
                
            }

            /// <summary>
            /// Statuses
            /// </summary>
            public enum Status
            { 
                /// <summary>
                /// In progress
                /// </summary>
                InProgress,
                /// <summary>
                /// Failed.
                /// </summary>
                Failed,
                /// <summary>
                /// Cancelled.
                /// </summary>
                Cancelled,
                /// <summary>
                /// Spam filter.
                /// </summary>
                SpamFilter,
                /// <summary>
                /// The complete.
                /// </summary>
                Complete
            }

            /// <summary>
            /// Login result
            /// </summary>
            public enum LoginResult
            { 
                /// <summary>
                /// None.
                /// </summary>
                None,
                /// <summary>
                /// Cancelled.
                /// </summary>
                Cancelled,
                /// <summary>
                /// The success.
                /// </summary>
                Success,
                /// <summary>
                /// The illegal.
                /// </summary>
                Illegal,
                /// <summary>
                /// Login does not exists.
                /// </summary>
                NotExists,
                /// <summary>
                /// Wrong pass.
                /// </summary>
                WrongPass,
                /// <summary>
                /// The failed.
                /// </summary>
                Failed,
                /// <summary>
                /// The name of the no.
                /// </summary>
                NoName,
                /// <summary>
                /// The empty pass.
                /// </summary>
                EmptyPass,
                /// <summary>
                /// The throttled.
                /// </summary>
                Throttled,
                /// <summary>
                /// The blocked.
                /// </summary>
                Blocked,
                /// <summary>
                /// The need token.
                /// </summary>
                NeedToken
                
                //TODO: This result we have not yet accounted for (see list below)
                //WrongPluginPass
                //CreateBlocked
                //mustbeposted
            }
        }
    }
}
