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
        // Flags for the OpenFileDialog and SaveFileDialog
        public const int OFN_EXPLORER = 0x00080000;        // Use the new Explorer-style dialog
        public const int OFN_PATHMUSTEXIST = 0x00000800;   // Only valid paths are allowed
        public const int OFN_OVERWRITEPROMPT = 0x00000002; // Prompt before overwriting a file
        public const int OFN_NOVALIDATE = 0x00000100;      // Do not validate file name

        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool GetOpenFileName(ref OPENFILENAME ofn);

        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool GetSaveFileName(ref OPENFILENAME ofn);
    }
}
