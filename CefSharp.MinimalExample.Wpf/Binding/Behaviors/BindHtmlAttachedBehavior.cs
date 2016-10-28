using CefSharp.Wpf;
using System.Windows;
using System.Windows.Interactivity;

namespace CefSharp.MinimalExample.Wpf.Binding.Behaviors
{
    public class LoadHtmlBehavior : Behavior<ChromiumWebBrowser>
    {
        // Using a DependencyProperty as the backing store for Html.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.Register(
                "Html", 
                typeof(string), 
                typeof(LoadHtmlBehavior), 
                new PropertyMetadata(string.Empty, OnHtmlChanged));

        // Using a DependencyProperty as the backing store for HtmlUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HtmlUrlProperty =
            DependencyProperty.Register(
                "HtmlUrl", 
                typeof(string), 
                typeof(LoadHtmlBehavior), 
                new PropertyMetadata(string.Empty));        

        public string Html
        {
            get { return (string)GetValue(HtmlProperty); }
            set { SetValue(HtmlProperty, value); }
        }
        
        public string HtmlUrl
        {
            get { return (string)GetValue(HtmlUrlProperty); }
            set { SetValue(HtmlUrlProperty, value); }
        }
        
        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null || string.IsNullOrWhiteSpace((string)e.NewValue))
            {
                return;
            }
            
            ((LoadHtmlBehavior)d).AssociatedObject.LoadHtml((string)e.NewValue, "about:blank");
        }
    }
}
