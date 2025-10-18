using Microsoft.UI.Xaml.Data;
using Silmoon.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.WinUI3.Converters
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return value;
            else
            {
                string formatString = parameter as string;
                if (!formatString.IsNullOrEmpty())
                    return string.Format(formatString, value);
                else
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
