// Copyright © 2010-2016 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.MinimalExample.WinForms.Controls;

namespace CefSharp.MinimalExample.WinForms
{
    /// <summary>
    /// EXPERIMENTAL - this implementation is very simplistic and not ready for production use
    /// See the following link for the CEF reference implementation.
    /// https://bitbucket.org/chromiumembedded/cef/commits/1ff26aa02a656b3bc9f0712591c92849c5909e04?at=2785
    /// </summary>
    public class BrowserProcessHandler : IBrowserProcessHandler
    {
        private Timer timer;
        private Control control;

        /// <summary>
        /// The maximum number of milliseconds we're willing to wait between calls to OnScheduleMessagePumpWork().
        /// </summary>
        protected const int MaxTimerDelay = 1000 / 60;  // 60fps

        public BrowserProcessHandler(Control control)
        {
            this.control = control;
            timer = new Timer { Interval = MaxTimerDelay };
            timer.Start();
            timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            //Basically execute Cef.DoMessageLoopWork 60 times per second
            //Execute DoMessageLoopWork on UI thread
            Cef.DoMessageLoopWork();
        }

        void IBrowserProcessHandler.OnContextInitialized()
        {
            
        }

        void IBrowserProcessHandler.OnScheduleMessagePumpWork(long delay)
        {
            //when delay <= 0 queue the Task up for execution on the UI thread.
            //Otherwise we just let the timer execute as per normal
            if (delay <= 0)
            {
                //Update the timer to execute almost immediately
                control.InvokeOnUiThreadIfRequired(() => Cef.DoMessageLoopWork());
            }
        }

        void IDisposable.Dispose()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }

            control = null;
        }
    }
}

