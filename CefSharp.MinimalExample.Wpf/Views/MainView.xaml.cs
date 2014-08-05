using System.Windows.Controls;
using CefSharp.MinimalExample.Wpf.ViewModels;

namespace CefSharp.MinimalExample.Wpf.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
