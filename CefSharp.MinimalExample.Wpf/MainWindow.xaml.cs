using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            StackPanel1.Children.Remove(Browser);
            new Window { Content = Browser}.Show();
            Close();
        }
    }
}
