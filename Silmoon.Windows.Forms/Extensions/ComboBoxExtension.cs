using Silmoon.Extension;
using Silmoon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Forms.Extensions
{
    public static class ComboBoxExtension
    {
        public static void BindEnum<T>(this ComboBox comboBox) where T : Enum
        {
            comboBox.DisplayMember = nameof(NameValue<T>.Name);
            comboBox.ValueMember = nameof(NameValue<T>.Value);
            comboBox.DataSource = EnumExtension2.ToDisplayNameValues<T>();
        }
    }
}
