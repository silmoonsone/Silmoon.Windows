using System;
using System.Collections.Generic;
using System.Text;

namespace Silmoon.Windows.Win32Api.Structs
{
    public struct WindowInfo
    {
        public nint hWnd;
        public string szWindowName;
        public string szClassName;
    }
}
