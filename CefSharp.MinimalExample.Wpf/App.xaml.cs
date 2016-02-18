﻿using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        public App()
        {
            //Perform dependency check to make sure all relevant resources are in our output directory.
            var settings = new CefSettings();
            settings.EnableInternalPdfViewerOffScreen();
            settings.LogSeverity = LogSeverity.Verbose;
            Cef.Initialize(settings, shutdownOnProcessExit: false, performDependencyCheck: true);
        }
    }
}
