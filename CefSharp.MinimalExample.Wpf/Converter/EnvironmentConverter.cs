using System;
using System.Globalization;
using System.Windows.Data;

namespace CefSharp.MinimalExample.Wpf.Converter
{
    public class EnvironmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Environment.Is64BitProcess ? "x64" : "x86";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
