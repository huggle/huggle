//This is a source code or part of Huggle project
//
//This file contains code for huggle

/// <DOCUMENTATION>
/// This is a part of core that handles the threads only. In fact we could replace this in future with more effective
/// system. It is desired to have control over ALL thread in huggle, as it controls tons of them. That's why this class
/// exist. It maintains overview of all threads managed by core.
/// </DOCUMENTATION>

//Copyright (C) 2011-2013 Huggle team

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
    public static partial class Core
    {
        public static class Threading
        {
            private static int ThreadLast = 0;
            private static int ThreadCount = 0;
            private static List<ThreadS> ThreadList = new List<ThreadS>();
            public class ThreadS
            {
                public string Decription;
                public System.Threading.Thread handle;
                public bool Active = false;
            }
            
            public static List<ThreadS> Threads
            {
                get
                {
                    lock (ThreadList)
                    {
                        List<ThreadS> threads = new List<ThreadS>();
                        threads.AddRange(ThreadList);
                        return threads;
                    }
                }
            }
            
            public static int SystemThreadCount
            {
                get
                {
                    int number = System.Diagnostics.Process.GetCurrentProcess().Threads.Count;
                    return number;
                }
            }
            
            /// <summary>
            /// This is only called when exiting
            /// </summary>
            /// <returns></returns>
            public static bool DestroyCore()
            {
                // All threads are aborted (usually when application die)
                int curr = 0;
                while (curr < Core.MThread)
                {
                    KillThread(curr);
                    curr++;
                }
                return true;
            }
            
            /// <summary>
            /// Kill thread id
            /// </summary>
            /// <param name="N"></param>
            /// <returns></returns>
            public static bool KillThread(int N)
            {
                // request thread to be aborted
                if (ThreadList[N].Active == false)
                {
                    return false;
                }
                if (ThreadList[N].handle.ThreadState == System.Threading.ThreadState.Aborted)
                {
                    // it's already down
                    ThreadList[N].Active = false;
                    ThreadList[N].Decription = "";
                    ThreadCount = ThreadCount - 1;
                    return false;
                }
                ThreadList[N].handle.Abort();
                ThreadList[N].Active = false;
                ThreadList[N].Decription = "";
                ThreadCount = ThreadCount - 1;
                return true;
            }
            
            /// <summary>
            /// Insted of kill thread, safe method to release handle of thread which is about to be aborted, should be called as last action of thread
            /// </summary>
            /// <param name="Thread"></param>
            public static void ReleaseHandle(int Thread)
            {
                if (ThreadList[Thread].Active == true)
                {
                    ThreadList[Thread].handle = null;
                    ThreadList[Thread].Active = false;
                    ThreadCount = ThreadCount - 1;
                }
            }
            
            /// <summary>
            /// Create a thread with name
            /// </summary>
            /// <param name="ThreadStart">Callback function</param>
            /// <param name="name"></param>
            /// <returns></returns>
            public static int CreateThread(System.Threading.ThreadStart ThreadStart, string name)
            {
                try
                {
                    ThreadLast++;
                    int ThreadID = ThreadLast;
                    while (ThreadList[ThreadID].Active != false)
                    {
                        if (ThreadID > Core.MThread || ThreadID > ThreadList.Count)
                        {
                            ThreadID = 0;
                        }
                        else
                        {
                            ThreadID++;
                        }
                    }
                    ThreadLast = ThreadID;
                    ThreadList[ThreadID].Active = true;
                    ThreadList[ThreadID].Decription = name;
                    ThreadList[ThreadID].handle = new System.Threading.Thread(ThreadStart);
                    ThreadList[ThreadID].handle.Name = name;
                    ThreadCount = ThreadCount + 1;
                    return ThreadID;
                }
                catch (Exception A)
                {
                    Core.ExceptionHandler(A);
                    return -1;
                }
            }
            
            /// <summary>
            /// Create thread with no name
            /// </summary>
            /// <param name="ThreadStart">Callback function</param>
            /// <returns></returns>
            public static int CreateThread(System.Threading.ThreadStart ThreadStart)
            {
                try
                {
                    ThreadLast++;
                    int ThreadID = ThreadLast;
                    while (ThreadList[ThreadID].Active != false)
                    {
                        if (ThreadID > Core.MThread)
                        {
                            ThreadID = 0;
                        }
                        else
                        {
                            ThreadID++;
                        }
                    }
                    ThreadLast = ThreadID;
                    ThreadList[ThreadID].Active = true;
                    ThreadList[ThreadID].Decription = "Huggle";
                    ThreadList[ThreadID].handle = new System.Threading.Thread(ThreadStart);
                    return ThreadID;
                }
                catch (Exception A)
                {
                    return -1;
                }
            }
            
            /// <summary>
            /// Thread manager core
            /// </summary>
            public static void ManagerThread()
            {
                System.Threading.Thread.Sleep(2000);
            }
            
            /// <summary>
            /// Only used for initialisation of the huggle
            /// </summary>
            public static void CreateList()
            {
                ThreadList.Clear();
                int curr = 0;
                while (curr < Core.MThread)
                {
                    curr++;
                    ThreadList.Add(new ThreadS());
                }
            }
            
            /// <summary>
            /// Thread count
            /// </summary>
            public static int ThCount
            {
                get { return ThreadCount; }
            }
            
            /// <summary>
            /// Return windows / linux handle of thread
            /// </summary>
            /// <param name="N"></param>
            /// <returns></returns>
            public static System.Threading.Thread GetHandle(int N)
            {
                return ThreadList[N].handle;
            }
            
            /// <summary>
            /// Start
            /// </summary>
            /// <param name="ID"></param>
            public static void Execute(int ID)
            {
                if (ThreadList[ID].Active == true)
                {
                    Core.WriteLog("Starting thread: " + ThreadList[ID].Decription);
                    ThreadList[ID].handle.Start();
                    return;
                }
                Core.WriteLog("Unable to start the thread, non existend handle");
            }
        }
    }
}
