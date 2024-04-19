using Microsoft.UI.Xaml.Data;
using Silmoon.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.WindowsApp.Converters
{
    public class DateTimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;
            else
            {
                if (value is DateTime dateTime)
                {
                    string formatString = parameter as string;
                    if (!formatString.IsNullOrEmpty())
                        return dateTime.ToString(formatString);
                    else
                        return dateTime;
                }
                else
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
