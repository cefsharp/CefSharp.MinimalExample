using System;
using System.Windows;
using System.Windows.Threading;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs startupEventArgs)
        {
            base.OnStartup(startupEventArgs);

            //Perform dependency check to make sure all relevant resources are in our output directory.
            var settings = new CefSettings();
            settings.MultiThreadedMessageLoop = false;
            settings.ExternalMessagePump = true;

            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: new BrowserProcessHandler(Dispatcher));
        }
    }
}
