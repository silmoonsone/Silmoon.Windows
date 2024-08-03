using System;
using System.Collections.Generic;
using System.Text;

namespace Silmoon.Windows.Win32.EnumDefined
{
    public enum WTS_CONNECTSTATE_CLASS
    {
        WTSActive = 0,
        WTSConnected,
        WTSConnectQuery,
        WTSShadow,
        WTSDisconnected,
        WTSIdle,
        WTSListen,
        WTSReset,
        WTSDown,
        WTSInit
    }
}
