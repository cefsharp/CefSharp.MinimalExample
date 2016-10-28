using CefSharp.Wpf;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf.Binding.Behaviors
{
    public static class HtmlBindingHelper
    {
        // Using a DependencyProperty as the backing store for Html.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached(
                "Html",
                typeof(string),
                typeof(HtmlBindingHelper),
                new PropertyMetadata(string.Empty, OnHtmlChanged));

        public static string GetHtml(DependencyObject obj)
        {
            return (string)obj.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject obj, string value)
        {
            obj.SetValue(HtmlProperty, value);
        }
        
        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string htmlText = e.NewValue as string;

            if (string.IsNullOrWhiteSpace(htmlText))
            {
                htmlText = string.Empty;
            }
            
            ((ChromiumWebBrowser)d).LoadHtml(htmlText, "http://cefsharp/loadHtml");
        }
    }
}
