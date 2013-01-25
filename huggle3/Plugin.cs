//This is a source code or part of Huggle project
//
//This file contains code for extensions

/// <DOCUMENTATION>
/// This code is stolen from pidgeon client - see documentation of that
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
using System.Text;

namespace huggle3
{
    public class Plugin
    {
        public static List<Plugin> ExtensionList = new List<Plugin>();
        public Status status = Status.Loading;
        public string Name = null;
        public List<Thread> Threads = new List<Thread>();

        public Plugin()
        { 
            
        }

        public void Load()
        { 
            
        }

        ~Plugin()
        {
            try
            {

            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        public void Terminate()
        {
            status = Status.Terminating;
            if (Hook_OnTerminate())
            {
                lock (ExtensionList)
                {
                    if (ExtensionList.Contains(this))
                    {
                        ExtensionList.Remove(this);
                    }
                }
                status = Status.Terminated;
            }
            else
            {
                Kill();
            }
        }


        /// <summary>
        /// Mecilessly kill
        /// </summary>
        public void Kill()
        {
            try
            {
                status = Status.Terminating;
                lock (ExtensionList)
                {
                    if (ExtensionList.Contains(this))
                    {
                        ExtensionList.Remove(this);
                    }
                }
                status = Status.Terminated;
            }
            catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
        }

        public virtual bool Hook_RegisterSelf()
        {
            return true;
        }

        public virtual bool Hook_OnTerminate()
        {
            return true;
        }

        public enum Status
        { 
            Active,
            Loading,
            Terminating,
            Terminated,
            Stopped
        }
    }
}
