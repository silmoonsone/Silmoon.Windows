using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Silmoon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.WinUI3.Converters
{
    public class HasContentToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string s)
            {
                return s.IsNullOrEmpty() ? Visibility.Collapsed : Visibility.Visible;
            }
            else if (value is IEnumerable<object> list)
            {
                return list.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return value != null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}