using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.WinUI3.Converters
{
    public class EnumDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return null;

            var enumValue = value.GetType().GetField(value.ToString());
            if (enumValue == null) return value.ToString();

            var displayAttribute = enumValue.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            return displayAttribute?.Length > 0 ? displayAttribute[0].Name : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
