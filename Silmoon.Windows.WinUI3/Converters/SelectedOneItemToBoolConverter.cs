using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.WinUI3.Converters
{
    /// <summary>
    /// 用于各类ListView、DataGrid等控件的SelectedItems属性与bool类型的绑定，但是只能绑定单选，也就是只有一个选中项时返回true，否则返回false
    /// </summary>
    public class SelectedOneItemToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IEnumerable<object> items)
            {
                return items.Count() == 1;
            }
            else return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
