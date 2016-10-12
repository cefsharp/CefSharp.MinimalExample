using System;
using System.Windows;
using System.Windows.Threading;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        private DispatcherTimer messageLoopTimer;

        public App()
        {
            //Perform dependency check to make sure all relevant resources are in our output directory.
            var settings = new CefSettings();
            settings.MultiThreadedMessageLoop = false;

            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
        }

        protected override void OnStartup(StartupEventArgs startupEventArgs)
        {
            base.OnStartup(startupEventArgs);

            //Run DoMessageLoopWork 60 times per second - this is the simplest message loop integration
            messageLoopTimer = new DispatcherTimer
            (
                TimeSpan.FromMilliseconds(1000 / 60),
                DispatcherPriority.Render,
                (s, e) => Cef.DoMessageLoopWork(),
                Dispatcher
            );
            messageLoopTimer.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (messageLoopTimer != null)
            {
                messageLoopTimer.Stop();
                messageLoopTimer = null;
            }

            base.OnExit(e);
        }
    }
}
