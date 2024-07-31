using Silmoon.Windows.Win32.Structs;
using Silmoon.Windows.Win32Api.EnumDefined;
using Silmoon.Windows.Win32Api.Structs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Silmoon.Windows.Win32Api.Apis
{
    public class User32
    {
        public const int LVM_FIRST = 0x1000;
        public const int LVM_SETITEMSTATE = LVM_FIRST + 43;

        public delegate bool WNDENUMPROC(nint hWnd, int lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowTextW")]
        private static extern int _setWindowTextW(nint hWhd, [MarshalAs(UnmanagedType.BStr)] string lpString);
        [DllImport("user32.dll", EntryPoint = "GetWindowTextW")]
        private static extern int _getWindowTextW(nint hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", EntryPoint = "GetClassNameW")]
        private static extern int _getClassNameW(nint hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern nint _getForegroundWindow();
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        private static extern bool _setForegroundWindow(nint hWnd);
        [DllImport("user32.dll", EntryPoint = "EnumWindows")]
        private static extern bool _enumWindows(WNDENUMPROC lpEnumFunc, int lParam);
        [DllImport("User32.DLL", EntryPoint = "SendMessage")]
        private static extern int _sendMessage(int hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern nint _sendMessage(nint hWnd, int msg, bool wParam, nint lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern nint _sendMessage(IntPtr hWnd, int msg, int wParam, ref LVITEM lvi);
        [DllImport("user32.dll", EntryPoint = "ExitWindowsEx")]
        private static extern bool _exitWindowsEx(ShutdownEnum.ExitWindows uFlags, ShutdownEnum.ShutdownReason dwReason);

        /// <summary>
        /// 设置指定句柄的窗口的标题
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="text">标题</param>
        /// <returns></returns>
        public static int SetWindowTextW(nint hWnd, string text) => _setWindowTextW(hWnd, text);
        /// <summary>
        /// 获取指定句柄的窗口的标题
        /// </summary>
        /// <param name="hWnd">标题</param>
        /// <param name="sbuilder">已有缓冲区的StringBuilder</param>
        /// <param name="reffCount">返回长度</param>
        /// <returns></returns>
        public static int GetWindowTextW(nint hWnd, StringBuilder sbuilder, int reffCount) => _getWindowTextW(hWnd, sbuilder, reffCount);
        /// <summary>
        /// 返回指定窗口的类信息
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="sbuilder">已有缓冲区的StringBuilder</param>
        /// <param name="reffCount">返回长度</param>
        /// <returns></returns>
        public static int GetClassNameW(nint hWnd, StringBuilder sbuilder, int reffCount) => _getClassNameW(hWnd, sbuilder, reffCount);
        /// <summary>
        /// 获取当前活动窗口的句柄
        /// </summary>
        /// <returns></returns>
        public static nint GetForegroundWindow() => _getForegroundWindow();
        /// <summary>
        /// 设置当前的活动窗口
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns></returns>
        public static bool SetForegroundWindow(nint hWnd) => _setForegroundWindow(hWnd);
        public static bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam) => _enumWindows(lpEnumFunc, lParam);
        /// <summary>
        /// 向指定的句柄窗口发送消息
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="mSg">消息ID</param>
        /// <param name="WParam">参数1</param>
        /// <param name="LParam">参数2</param>
        /// <returns></returns>
        public static int SendMessage(int hWnd, int mSg, int WParam, int LParam) => _sendMessage(hWnd, mSg, WParam, LParam);
        /// <summary>
        /// 向指定的句柄窗口发送消息
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="mSg">消息ID</param>
        /// <param name="WParam">参数1</param>
        /// <param name="LParam">参数2</param>
        /// <returns></returns>
        public static nint SendMessage(nint hWnd, int mSg, bool WParam, nint LParam) => _sendMessage(hWnd, mSg, WParam, LParam);

        public static bool ExitWindowsEx(ShutdownEnum.ExitWindows Pew, ShutdownEnum.ShutdownReason Per) => _exitWindowsEx(Pew, Per);
        /// <summary>
        /// 发送给原生ListView的消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lvi"></param>
        /// <returns></returns>
        public static nint SendMessage(nint hWnd, int msg, int WParam, ref LVITEM lvi) => _sendMessage(hWnd, msg, WParam, ref lvi);
    }
}
