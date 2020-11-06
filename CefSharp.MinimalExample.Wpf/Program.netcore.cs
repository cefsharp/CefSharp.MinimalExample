using CefSharp.Wpf;
using System;
using System.IO;

namespace CefSharp.MinimalExample.Wpf
{
    public static class Program
    {
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread]
        public static int Main(string[] args)
        {
            //For Windows 7 and above, app.manifest entries will take precedences of this call
            Cef.EnableHighDPISupport();

            var settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
            };

            //Example of setting a command line argument
            //Enables WebRTC
            settings.CefCommandLineArgs.Add("enable-media-stream");

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true);

            var app = new App();
            app.InitializeComponent();
            return app.Run();
        }
    }
}