using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        public App()
        {
            //Perform dependency check to make sure all relevant resources are in our output directory.
            var settings = new CefSettings();
            settings.EnableInternalPdfViewerOffScreen();
            // Disable GPU in WPF and Offscreen examples until #1634 has been resolved
            settings.CefCommandLineArgs.Add("disable-gpu", "1");

            Cef.Initialize(settings, shutdownOnProcessExit: true, performDependencyCheck: true);
        }
    }
}
