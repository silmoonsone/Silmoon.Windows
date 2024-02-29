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
    /// <summary>
    /// 将一个可观察枚举类型转换为NameValue类型的数组，用于绑定到下拉框以显示枚举在下拉框中的显示信息，和NameValueToEnumConverter配合使用。
    /// </summary>
    public class EnumToNameValueArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var type = value.GetType();
            var enums = Enum.GetValues(type);
            if (enums is Array array)
            {
                List<NameValue<Enum>> result = [];
                foreach (var item in array)
                {
                    var enumValue = item as Enum;
                    result.Add(new NameValue<Enum>(enumValue.GetDisplayName(), enumValue));
                }
                return result.ToArray();
            }
            else
            {
                throw new ArgumentException("value is not array");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
