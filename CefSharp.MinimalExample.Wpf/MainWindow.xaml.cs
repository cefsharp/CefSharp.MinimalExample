using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnTxtBoxAddressKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtBoxAddress.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }
    }
}
