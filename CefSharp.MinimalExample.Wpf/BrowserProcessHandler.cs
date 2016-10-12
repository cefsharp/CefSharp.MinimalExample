// Copyright © 2010-2016 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using CefSharp;

namespace CefSharp.MinimalExample.Wpf
{
    /// <summary>
    /// EXPERIMENTAL - this implementation is very simplistic and not ready for production use
    /// See the following link for the CEF reference implementation.
    /// https://bitbucket.org/chromiumembedded/cef/commits/1ff26aa02a656b3bc9f0712591c92849c5909e04?at=2785
    /// </summary>
    public class BrowserProcessHandler : IBrowserProcessHandler
    {
        /// <summary>
        /// The maximum number of milliseconds we're willing to wait between calls to OnScheduleMessagePumpWork().
        /// </summary>
        protected const int MaxTimerDelay = 1000 / 60;  // 60fps

        private DispatcherTimer messageLoopTimer;
        private Dispatcher dispatcher;

        public BrowserProcessHandler(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            this.dispatcher.ShutdownStarted += DispatcherShutdownStarted;

            //Run DoMessageLoopWork 60 times per second - this is the simplest message loop integration
            messageLoopTimer = new DispatcherTimer
            (
                TimeSpan.FromMilliseconds(MaxTimerDelay),
                DispatcherPriority.Render,
                (s, e) => Cef.DoMessageLoopWork(),
                dispatcher
            );
            messageLoopTimer.Start();
        }

        private void DispatcherShutdownStarted(object sender, EventArgs e)
        {
            //If the dispatcher is shutting down then we stop the timer
            if(messageLoopTimer != null)
            {
                messageLoopTimer.Stop();
            }
        }

        void IBrowserProcessHandler.OnContextInitialized()
        {

        }

        void IBrowserProcessHandler.OnScheduleMessagePumpWork(long delay)
        {
            //If the delay is greater than the Maximum then use MaxTimerDelay
            //instead - we do this to achieve a minimum number of FPS
            if(delay > MaxTimerDelay)
            {
                delay = MaxTimerDelay;
            }

            //When delay <= 0 we'll execute Cef.DoMessageLoopWork immediately
            // if it's greater than we'll just let the Timer which fires 30 times per second
            // care of the call
            if(delay <= 0)
            {
                dispatcher.BeginInvoke((Action)(() => Cef.DoMessageLoopWork()), DispatcherPriority.Normal);
            }
        }

        void IDisposable.Dispose()
        {
            if(dispatcher != null)
            {
                dispatcher.ShutdownStarted -= DispatcherShutdownStarted;
                dispatcher = null;
            }

            if (messageLoopTimer != null)
            {
                messageLoopTimer.Stop();
                messageLoopTimer = null;
            }
        }
    }
}
