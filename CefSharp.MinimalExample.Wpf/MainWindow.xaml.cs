using System.Windows;
using System.Windows.Controls;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnvironmentExitOnClick(object sender, RoutedEventArgs e)
        {
            var parent = (Grid)Browser.Parent;
            parent.Children.RemoveAt(0);

            Browser.Dispose();

            Cef.Shutdown();
            System.Environment.Exit(0);
        }
    }
}
