//This is a source code or part of Huggle project
//
//This file contains code for core

/// <DOCUMENTATION>
/// There is no documentation for this
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
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Text;
//using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Huggle3LX
{
    public partial class Core
    {
        /// <summary>
        /// Threads which are not managed by core
        /// </summary>
        public class SpecialThreads
        {
            public static System.Threading.Thread RecoveryThread = null;
            public static System.Threading.Thread ThreadManager = null;
        }

        /// <summary>
        /// Maximum number of threads in core
        /// </summary>
        public const int MaximumThreadCountLimit = 600;
        /// <summary>
        /// Container for system log which is in memory
        /// </summary>
        public static List<string> SystemLog = new List<string>();
        /// <summary>
        /// Time when system was loaded
        /// </summary>
        private static DateTime Uptime;
        /// <summary>
        /// For exception handler
        /// </summary>
        private static Exception core_er;
        /// <summary>
        /// Local path
        /// </summary>
        private static string _LocalPath = null;
        /// <summary>
        /// Main thread this core run as
        /// </summary>
        public static System.Threading.Thread MainThread;
        /// <summary>
        /// This is true in case that critical exception was thrown so that exception form knows it and doesn't offer recovery option
        /// </summary>
        public static bool Panic = false;
        /// <summary>
        /// Process of huggle
        /// </summary>
        public static System.Diagnostics.Process Process;
        /// <summary>
        /// Return true in case of fatal error when core is stopped
        /// </summary>
        public static bool Interrupted = false;

        public enum Status
        {
            Running,
            Stopped
        }

        public static void Load()
        {
            
        }

        /// <summary>
        /// Convert Drawing to Gdk color
        /// </summary>
        /// <returns>The color.</returns>
        /// <param name="color">Color.</param>
        public static Gdk.Color FromColor(System.Drawing.Color color)
        {
            return new Gdk.Color(color.R, color.G, color.B);
        }

        public static void WriteLog(string text)
        {
            string x = DateTime.Now.ToString() + ": " + text;
            lock (SystemLog)
            {
                while (SystemLog.Count > Configuration.Core.RingSize)
                {
                    SystemLog.RemoveAt(0);
                }
                SystemLog.Add(x);
            }
            Console.WriteLine(x);
        }

        /// <summary>
        /// stop everything in system, used for recovery
        /// </summary>
        /// <returns></returns>
        public static bool StopAll()
        {
            return false;
        }

        /// <summary>
        /// Suspend thread
        /// </summary>
        public static void Suspend()
        {
            Core.Interrupted = true;
            while (Core.Interrupted)
            {
                System.Threading.Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// Throw a huggle error dialog in case of error and recover from crash
        /// </summary>
        /// <param name="error_handle"></param>
        /// <param name="panic"></param>
        /// <returns></returns>
        public static bool ExceptionHandler(Exception error_handle, bool panic = false)
        {
            try
            {
                WriteLog("EXCEPTION: " + error_handle.Message);
                if (!panic)
                {
                    if (System.Threading.Thread.CurrentThread != MainThread)
                    {
                        if (typeof(System.Threading.ThreadAbortException) == error_handle.GetType())
                        {
                            WriteLog("Suppressing non panic exception ThreadAbortException: " + error_handle.StackTrace);
                            return true;
                        }
                    }
                }
                core_er = error_handle;
                if (SpecialThreads.RecoveryThread == null)
                {
                    SpecialThreads.RecoveryThread = new System.Threading.Thread(CreateEx);
                    SpecialThreads.RecoveryThread.Name = "Recovery thread";
                }
                else if (SpecialThreads.RecoveryThread.ThreadState == System.Threading.ThreadState.Running)
                {
                    Core.Suspend();
                    return false;
                }
                else if (SpecialThreads.RecoveryThread.ThreadState == System.Threading.ThreadState.Aborted || SpecialThreads.RecoveryThread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    SpecialThreads.RecoveryThread = new System.Threading.Thread(CreateEx);
                    SpecialThreads.RecoveryThread.Name = "Recovery thread";
                }
                SpecialThreads.RecoveryThread.Start();
                if (panic == true)
                {
                    StopAll();
                }
                if (MainThread == System.Threading.Thread.CurrentThread)
                {
                    Core.Suspend();
                }
                return true;
            }
            catch (Exception fail)
            {
                Console.WriteLine("Huggle3 thrown exception during handling of another one, killing");
                Console.WriteLine(fail.StackTrace + "\n\n" + fail.Message);
                Process.Kill();
                return false;
            }
        }

        /// <summary>
        /// Target build
        /// </summary>
        /// <returns></returns>
        public static string TargetBuild()
        {
            /*switch (Config._Platform)
            {
                case Config.platform.windows32:
                    return "Windows x86";
                case Config.platform.linux32:
                    return "Linux x86";
                case Config.platform.macos32:
                    return "MacOS x86";
                case Config.platform.linux64:
                    return "Linux x64";
                case Config.platform.windows64:
                    return "Windows x64";
            }
             */
            return "<unknown build>";
        }

        /// <summary>
        /// Create error
        /// </summary>
        public static void CreateEx()
        {
            Huggle3LX.Forms.ExceptionForm fx = new Huggle3LX.Forms.ExceptionForm();
            fx.error = core_er;
            Application.Run(fx);
        }

        public static void ShutdownSystem()
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }
    }
}
