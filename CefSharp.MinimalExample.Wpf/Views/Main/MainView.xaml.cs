using System.Windows.Controls;

namespace CefSharp.MinimalExample.Wpf.Views.Main
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
