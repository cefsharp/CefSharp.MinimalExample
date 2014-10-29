using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class App : Application
    {
        public App()
        {
            Cef.Initialize(new CefSettings());
        }
    }
}
