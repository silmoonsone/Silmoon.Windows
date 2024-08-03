using Silmoon.Windows.Win32.EnumDefined;
using Silmoon.Windows.Win32.Structs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Silmoon.Windows.Win32.Apis
{
    public class Kernel32
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern nint GetCurrentProcess();

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetSystemTimes(out FILETIME lpIdleTime, out FILETIME lpKernelTime, out FILETIME lpUserTime);
        [DllImport("kernel32")]
        public static extern void SetConsoleTitle(string lpTitleStr);
        [DllImport("kernel32")]
        public static extern void GetConsoleTitle(StringBuilder lpBuff, int buffSize);
        [DllImport("kernel32")]
        public static extern int SetConsoleTextAttribute(nint hConsoleOutput, int wAttributes);
        [DllImport("kernel32")]
        public static extern int FillConsoleOutputCharacter(nint Handle, char uChar, int Len, COORD start, ref int written);
        [DllImport("kernel32")]
        public static extern bool FillConsoleOutputAttribute(nint Handle, short att, int Len, COORD start, ref int writted);
        [DllImport("kernel32")]
        public static extern void GetConsoleScreenBufferInfo(nint Handle, ref CONSOLE_INFO info);
        [DllImport("kernel32")]
        public static extern bool SetConsoleCursorInfo(nint Handle, ref CURSOR_INFO info);
        [DllImport("kernel32")]
        public static extern bool SetConsoleCursorPosition(nint handle, int coord);
        [DllImport("kernel32")]
        public static extern nint GetStdHandle(int nStdHandle);
        [DllImport("kernel32")]
        public static extern void GetConsoleMode(nint hConsoleHandle, ref int lpMode);
        [DllImport("kernel32")]
        public static extern void SetConsoleMode(nint hConsoleHandle, int dwMode);
        [DllImport("kernel32")]
        public static extern int CloseHandle(nint hObject);
        [DllImport("kernel32")]
        public static extern int AllocConsole();
        [DllImport("kernel32")]
        public static extern int FreeConsole();

    }
}
