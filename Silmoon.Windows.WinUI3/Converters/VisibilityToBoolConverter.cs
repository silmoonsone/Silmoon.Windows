using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Silmoon.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.WinUI3.Converters
{
    public class VisibilityToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isInvert = (parameter as string).ToBool();
            bool isVisual = value is bool b1 && b1;
            return !isInvert
                ? isVisual ? Visibility.Visible : Visibility.Collapsed
                : isVisual ? Visibility.Collapsed : (object)Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool isInvert = (parameter as string).ToBool();
            Visibility visibility = value is Visibility v ? v : Visibility.Collapsed;
            if (isInvert) visibility = visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            return visibility == Visibility.Visible;
        }
    }
}