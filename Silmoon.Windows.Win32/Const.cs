using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Win32
{
    public class Const
    {
        public const int SE_PRIVILEGE_ENABLED = 0x00000002;
        public const int TOKEN_QUERY = 0x00000008;
        public const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        public const int EWX_LOGOFF = 0x00000000;
        public const int EWX_SHUTDOWN = 0x00000001;
        public const int EWX_REBOOT = 0x00000002;
        public const int EWX_FORCE = 0x00000004;
        public const int EWX_POWEROFF = 0x00000008;
        public const int EWX_FORCEIFHUNG = 0x00000010;

        // Flags for the OpenFileDialog and SaveFileDialog
        public const int OFN_EXPLORER = 0x00080000;        // Use the new Explorer-style dialog
        public const int OFN_PATHMUSTEXIST = 0x00000800;   // Only valid paths are allowed
        public const int OFN_OVERWRITEPROMPT = 0x00000002; // Prompt before overwriting a file
        public const int OFN_NOVALIDATE = 0x00000100;      // Do not validate file name


        public const int LVM_FIRST = 0x1000;
        public const int LVM_SETITEMSTATE = LVM_FIRST + 43;

        public delegate bool WNDENUMPROC(nint hWnd, int lParam);



        internal const int STD_OUTPUT_HANDLE = -11;
        internal const int STD_INPUT_HANDLE = -10;
        internal const short ENABLE_LINE_INPUT = 2;
        internal const short ENABLE_ECHO_INPUT = 4;
        internal const int FOREGROUND_BLUE = 1;
        internal const int FOREGROUND_GREEN = 2;
        internal const int FOREGROUND_RED = 4;
        internal const int FOREGROUND_INTENSIFY = 8;
        internal const int BACKGROUND_BLUE = 16;
        internal const int BACKGROUND_GREEN = 32;
        internal const int BACKGROUND_INTENSIFY = 128;

    }
}
