using CefSharp.Wpf;
using System;
using System.Globalization;
using System.IO;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        public App()
        {
        //    //Monitor parent process exit and close subprocesses if parent process exits first
        //    //This will at some point in the future becomes the default
          CefSharpSettings.SubprocessExitIfParentProcessClosed = true;

            var settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
                Locale = CultureInfo.CurrentCulture.IetfLanguageTag,
                AcceptLanguageList = CultureInfo.CurrentCulture.IetfLanguageTag,
                RemoteDebuggingPort = 5555,
                LogSeverity = LogSeverity.Error,
                MultiThreadedMessageLoop = true,
                ExternalMessagePump = false,
                UncaughtExceptionStackSize = 10,
                PersistUserPreferences = false,
                WindowlessRenderingEnabled = true,
                IgnoreCertificateErrors = true,
            };
            CefSharpSettings.ShutdownOnExit = false;
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;

          Cef.EnableHighDPISupport();
        //    //Example of setting a command line argument
        //    //Enables WebRTC
           settings.CefCommandLineArgs.Add("enable-media-stream", "1");

     
        Cef.Initialize(settings);
       }
    }
}
