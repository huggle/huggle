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
	public class request_core
	{
		public class RequestResult
		{
			public string text;
			public string message;
			
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
			private States _State;
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
			
			/*
                 * 
                 *
                 * Protected Function PostEdit(ByVal Page As Page, ByVal Text As String, ByVal Summary As String, _
            Optional ByVal Section As String = Nothing, Optional ByVal Minor As Boolean = False, _
            Optional ByVal Watch As Boolean = False, Optional ByVal SuppressAutoSummary As Boolean = False, _
            Optional ByVal AllowCreate As Boolean = True) As ApiResult
            Dim BreakA As Integer = 0, BreakB As Integer = 0

            While BadToken = False And BreakB < Misc.GlExcess
                BreakB = BreakB + 1
                'Get edit token
                While Token Is Nothing And BreakA < Misc.GlExcess
                    BreakA = BreakA + 1
                    If Section IsNot Nothing OrElse EditToken Is Nothing Then
                        Result = DoApiRequest("action=query&prop=info&intoken=edit&titles=" & UrlEncode(Page.Name))
                        If Result.Error Then Return Result
                        Token = GetParameter(Result.Text, "edittoken")
                        If Section Is Nothing Then EditToken = Token
                    Else
                        Token = EditToken
                    End If

                    If EditToken IsNot Nothing Then
                        If EditToken.Length < 16 Then
                            'Logged out somehow, logging back in
                            Token = Nothing
                            EditToken = Nothing
                            LogProgress(Msg("error-loggedout"))

                            If DoLogin() <> LoginResult.Success _
                                Then Return New ApiResult(Nothing, "", Msg("error-reloginfail"))
                        End If
                    End If
                End While

                'Edit page
                Dim QueryString As String = "title=" & UrlEncode(Page.Name) & "&text=" & UrlEncode(Text) _
                    & "&summary=" & UrlEncode(Summary)

                If Config.Summary <> "" AndAlso Not SuppressAutoSummary _
                    Then QueryString &= UrlEncode(" " & Config.Summary)

                QueryString &= "&token=" & UrlEncode(Token)

                If Section IsNot Nothing Then QueryString &= "&section=" & UrlEncode(Section)
                If Minor Then QueryString &= "&minor"
                If Watch Then QueryString &= "&watch"

                Result = DoApiRequest("action=edit", QueryString)

                If Result.ErrorCode = "badtoken" Then
                    BadToken = True
                    EditToken = Nothing
                Else
                    BadToken = False
                    Return Result
                End If
            End While
            If Result IsNot Nothing Then
                If Not Result.Error AndAlso Not Watchlist.Contains(Page.SubjectPage) Then Watchlist.Add(Page.SubjectPage)
            End If
            Return Result
        End Function

        Protected Function PostEdit(ByVal PageName As String, ByVal Text As String, ByVal Summary As String, _
            Optional ByVal Section As String = Nothing, Optional ByVal Minor As Boolean = False, _
            Optional ByVal Watch As Boolean = False, Optional ByVal SuppressAutoSummary As Boolean = False) _
            As ApiResult

            Return PostEdit(GetPage(PageName), Text, Summary, Section, Minor, Watch, SuppressAutoSummary)
        End Function
                 * 
                 */
			
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
				_State = States.InProgress;
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
				_State = States.Cancelled;
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
				_State = States.Failed;
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
				_State = States.Complete;
				EndRequest();
			}
			
			/// <summary>
			/// Property returning status
			/// </summary>
			public States State
			{
				get { return _State; }
				
			}
			
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
