using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseMenuItemOnClick(object sender, RoutedEventArgs e)
        {
            viewModel.DisposeBrowserAndShutdown();

            Close();
        }
    }
}
