using Silmoon.Windows.Win32.EnumDefined;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Silmoon.Windows.Win32.Structs
{
    public struct WTS_SESSION_INFO
    {
        public int SessionID;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pWinStationName;
        public WTS_CONNECTSTATE_CLASS state;
    }
}
