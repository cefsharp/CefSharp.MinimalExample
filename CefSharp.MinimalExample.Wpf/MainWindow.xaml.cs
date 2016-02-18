using System;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Browser.Dispose();
                Cef.Shutdown();
            }));

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
