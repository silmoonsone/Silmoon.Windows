using Silmoon.Windows.Win32Api.EnumDefined;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Silmoon.Windows.Win32Api.Structs
{
    public struct WTS_SESSION_INFO
    {
        public int SessionID;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pWinStationName;
        public WTS_CONNECTSTATE_CLASS state;
    }
}
