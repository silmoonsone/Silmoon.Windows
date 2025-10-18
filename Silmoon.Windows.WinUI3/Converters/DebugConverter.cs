using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.WinUI3.Converters
{
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            System.Diagnostics.Debug.WriteLine($"DebugConverter.Convert: {value}");
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            System.Diagnostics.Debug.WriteLine($"DebugConverter.ConvertBack: {value}");
            return value;
        }
    }
}
