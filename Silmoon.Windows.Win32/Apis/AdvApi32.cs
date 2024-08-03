using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Silmoon.Windows.Win32.Structs;

namespace Silmoon.Windows.Win32.Apis
{
    public class AdvApi32
    {

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool OpenProcessToken(nint h, int acc, ref nint phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool AdjustTokenPrivileges(nint htok, bool disall, ref TokPriv1Luid newst, int len, nint prev, nint relen);
    }
}
