using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Win32.EnumDefined
{
    public struct CONSOLE_INFO
    {
        internal COORD Size;
        internal COORD CursorPosition;
        internal short Attribute;
        internal RECT Window;
        internal COORD MaxSize;
    }
}
