using CefSharp.MinimalExample.Wpf.Binding;
using System.Windows.Input;

namespace CefSharp.MinimalExample.Wpf
{
    public class MainViewModel : BindableBase
    {
        private string html;

        public MainViewModel()
        {
            ViewHtmlCommand = new RelayCommand(OnViewHtml);
            Address = "www.google.com";
        }
        
        public string Html
        {
            get { return html; }
            set { SetProperty(ref html, value); }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        public ICommand ViewHtmlCommand { get; private set; }

        private void OnViewHtml()
        {
            Html = "<div dir=\"ltr\"><b><i>test</i></b></div>\r\n";
        }
    }
}
