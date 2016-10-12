using System;
using System.Windows;
using System.Windows.Threading;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        public App()
        {
            //Perform dependency check to make sure all relevant resources are in our output directory.
            var settings = new CefSettings();
            settings.MultiThreadedMessageLoop = false;

            var osVersion = Environment.OSVersion;
            //Disable GPU for Windows 7
            if (osVersion.Version.Major == 6 && osVersion.Version.Minor == 1)
            {
                // Disable GPU in WPF and Offscreen examples until #1634 has been resolved
                settings.CefCommandLineArgs.Add("disable-gpu", "1");
            }

            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
        }

        protected override void OnStartup(StartupEventArgs startupEventArgs)
        {
            base.OnStartup(startupEventArgs);

            //Run DoMessageLoopWork 60 times per second - this is the simplest message loop integration
            var timer = new DispatcherTimer
            (
                TimeSpan.FromMilliseconds(1000 / 60),
                DispatcherPriority.Render,
                (s, e) => Cef.DoMessageLoopWork(),
                Dispatcher
            );
            timer.Start();
        }
    }
}
