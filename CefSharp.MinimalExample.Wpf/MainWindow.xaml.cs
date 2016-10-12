using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Browser.BrowserSettings.WindowlessFrameRate = 60;
        }
    }
}
