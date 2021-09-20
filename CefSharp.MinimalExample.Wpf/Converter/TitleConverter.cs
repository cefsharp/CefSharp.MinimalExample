using System;
using System.Globalization;
using System.Windows.Data;

namespace CefSharp.MinimalExample.Wpf.Converter
{
    public class TitleConverter : IValueConverter
    {
#if DEBUG
        private const string Build = "Debug";
#else
        private const string Build = "Release";
#endif
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "CefSharp.MinimalExample.Wpf (" + Build + ") - " + (value ?? "No Title Specified");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
