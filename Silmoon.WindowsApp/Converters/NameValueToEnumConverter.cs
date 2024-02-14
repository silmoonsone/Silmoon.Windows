using Microsoft.UI.Xaml.Data;
using Silmoon.Extension;
using Silmoon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.WindowsApp.Converters
{
    public class NameValueToEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Enum enumValue)
            {
                NameValue<Enum> nameValue = new NameValue<Enum>(enumValue.GetDisplayName(), enumValue);
                return nameValue;
            }
            else
            {
                throw new ArgumentException("value is not Enum");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is NameValue<Enum> nameValue)
            {
                return nameValue.Value;
            }
            else
            {
                Array enumValues = Enum.GetValues(targetType);
                object defaultValue = enumValues.Length > 0 ? enumValues.GetValue(0) : null;
                return defaultValue;
            }
        }
    }
}
