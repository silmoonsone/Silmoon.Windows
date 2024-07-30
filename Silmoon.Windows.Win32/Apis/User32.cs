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

        private delegate bool WNDENUMPROC(nint hWnd, int lParam);

        [DllImport("user32.dll")]
        private static extern int SetWindowTextW(nint hWhd, [MarshalAs(UnmanagedType.BStr)] string lpString);
        [DllImport("user32.dll")]
        private static extern int GetWindowTextW(nint hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        private static extern int GetClassNameW(nint hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern nint GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(nint hWnd);
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);
        [DllImport("User32.DLL")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern nint SendMessage(nint hWnd, int msg, bool wParam, nint lParam);
        [DllImport("user32.dll")]
        private static extern bool ExitWindowsEx(ShutdownEnum.ExitWindows uFlags, ShutdownEnum.ShutdownReason dwReason);

        /// <summary>
        /// 设置指定句柄的窗口的标题
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="text">标题</param>
        /// <returns></returns>
        public static int setWindowTextW(nint hWnd, string text)
        {
            return SetWindowTextW(hWnd, text);
        }
        /// <summary>
        /// 获取指定句柄的窗口的标题
        /// </summary>
        /// <param name="hWnd">标题</param>
        /// <param name="sbuilder">已有缓冲区的StringBuilder</param>
        /// <param name="reffCount">返回长度</param>
        /// <returns></returns>
        public static int getWindowTextW(nint hWnd, StringBuilder sbuilder, int reffCount)
        {
            return GetWindowTextW(hWnd, sbuilder, reffCount);
        }
        /// <summary>
        /// 返回指定窗口的类信息
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="sbuilder">已有缓冲区的StringBuilder</param>
        /// <param name="reffCount">返回长度</param>
        /// <returns></returns>
        public static int getClassNameW(nint hWnd, StringBuilder sbuilder, int reffCount)
        {
            return GetClassNameW(hWnd, sbuilder, reffCount);
        }
        /// <summary>
        /// 获取当前活动窗口的句柄
        /// </summary>
        /// <returns></returns>
        public static nint getForegroundWindow()
        {
            return GetForegroundWindow();
        }
        /// <summary>
        /// 设置当前的活动窗口
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns></returns>
        public static bool setForegroundWindow(nint hWnd)
        {
            return SetForegroundWindow(hWnd);
        }
        /// <summary>
        /// 枚举当前桌面所有的句柄！
        /// </summary>
        /// <returns></returns>
        public static WindowInfo[] enumWindows()
        {
            List<WindowInfo> wndList = new List<WindowInfo>();

            //enum all desktop windows 
            EnumWindows(delegate (nint hWnd, int lParam)
            {
                WindowInfo wnd = new WindowInfo();
                StringBuilder sb = new StringBuilder(256);
                //get hwnd 
                wnd.hWnd = hWnd;
                //get window name 
                GetWindowTextW(hWnd, sb, sb.Capacity);
                wnd.szWindowName = sb.ToString();
                //get window class 
                GetClassNameW(hWnd, sb, sb.Capacity);
                wnd.szClassName = sb.ToString();
                //add it into list 
                wndList.Add(wnd);
                return true;
            }, 0);

            return wndList.ToArray();
        }
        /// <summary>
        /// 向指定的句柄窗口发送消息
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="mSg">消息ID</param>
        /// <param name="WParam">参数1</param>
        /// <param name="LParam">参数2</param>
        /// <returns></returns>
        public static int sendMessage(int hWnd, int mSg, int WParam, int LParam)
        {
            return SendMessage(hWnd, mSg, WParam, LParam);
        }
        /// <summary>
        /// 向指定的句柄窗口发送消息
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="mSg">消息ID</param>
        /// <param name="WParam">参数1</param>
        /// <param name="LParam">参数2</param>
        /// <returns></returns>
        public static nint sendMessage(nint hWnd, int mSg, bool WParam, nint LParam)
        {
            return SendMessage(hWnd, mSg, WParam, LParam);
        }

        public static bool exitWindowsEx(ShutdownEnum.ExitWindows Pew, ShutdownEnum.ShutdownReason Per)
        {
            return ExitWindowsEx(Pew, Per);
        }
        /// <summary>
        /// 发送给原生ListView的消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lvi"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageLVItem(IntPtr hWnd, int msg, int wParam, ref LVITEM lvi);
    }
}
