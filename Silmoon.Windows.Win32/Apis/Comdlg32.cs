using Silmoon.Windows.Win32.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Win32.Apis
{
    public class Comdlg32
    {
        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool GetOpenFileName(ref OPENFILENAME ofn);

        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool GetSaveFileName(ref OPENFILENAME ofn);
    }
}
