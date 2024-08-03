using Silmoon.Extension;
using Silmoon.Windows.Win32.Apis;
using Silmoon.Windows.Win32.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Win32
{
    public class Win32ApiHelper
    {
        // 定义WM_SETREDRAW消息
        private const int WM_SETREDRAW = 0x000B;

        private static long prevSystemIdle = 0;
        private static long prevSystemKernel = 0;
        private static long prevSystemUser = 0;

        public static MemoryInfo GetSystemMemoryInfo()
        {
            MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
            MemoryInfo memoryInfo = new MemoryInfo();
            if (Kernel32.GlobalMemoryStatusEx(memStatus))
            {
                memoryInfo.MemoryLoad = memStatus.dwMemoryLoad;
                memoryInfo.TotalPhysicalMemory = memStatus.ullTotalPhys;
                memoryInfo.AvailablePhysicalMemory = memStatus.ullAvailPhys;
            }
            return memoryInfo;
        }
        public static double GetCpuUsage()
        {
            Kernel32.GetSystemTimes(out FILETIME idleTime, out FILETIME kernelTime, out FILETIME userTime);

            long systemIdle = idleTime.dwLowDateTime | (long)idleTime.dwHighDateTime << 32;
            long systemKernel = kernelTime.dwLowDateTime | (long)kernelTime.dwHighDateTime << 32;
            long systemUser = userTime.dwLowDateTime | (long)userTime.dwHighDateTime << 32;

            long sysIdleDiff = systemIdle - prevSystemIdle;
            long sysKernelDiff = systemKernel - prevSystemKernel;
            long sysUserDiff = systemUser - prevSystemUser;

            long sysTotal = sysKernelDiff + sysUserDiff;
            long sysUsed = sysTotal - sysIdleDiff;

            prevSystemIdle = systemIdle;
            prevSystemKernel = systemKernel;
            prevSystemUser = systemUser;

            return sysUsed * 100.0 / sysTotal;
        }

        public static string OpenFileDialog(string[] filters, string[] filterNames, string dialogTitle, string startingDirectory = null)
        {
            var ofn = new OPENFILENAME();
            ofn.lStructSize = Marshal.SizeOf(ofn);

            if (filters.Length != filterNames.Length) throw new ArgumentException("filters and filterNames must have the same length.");
            // 构建过滤器字符串
            string filter = string.Empty;

            for (int i = 0; i < filters.Length; i++)
            {
                filter += $"{filterNames[i]} (*.{filters[i]})\0*.{filters[i]}\0";
            }
            filter += "\0";  // 最终结束符

            ofn.lpstrFilter = filter;
            ofn.lpstrFile = new string(new char[256]);
            ofn.nMaxFile = ofn.lpstrFile.Length;
            if (!string.IsNullOrEmpty(startingDirectory)) ofn.lpstrInitialDir = startingDirectory;
            ofn.lpstrFileTitle = new string(new char[64]);
            ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
            ofn.lpstrTitle = dialogTitle;
            if (Comdlg32.GetOpenFileName(ref ofn))
                return ofn.lpstrFile;
            return string.Empty;
        }
        public static string SaveFileDialog(string[] filters, string[] filterNames, string dialogTitle, bool fileExistOverwriteAlert = true, string startingDirectory = null)
        {
            var ofn = new OPENFILENAME();
            ofn.lStructSize = Marshal.SizeOf(ofn);

            if (filters.Length != filterNames.Length) throw new ArgumentException("filters and filterNames must have the same length.");
            // 构建过滤器字符串
            string filter = string.Empty;

            for (int i = 0; i < filters.Length; i++)
            {
                filter += $"{filterNames[i]} (*.{filters[i]})\0*.{filters[i]}\0";
            }
            filter += "\0";  // 最终结束符

            ofn.lpstrFilter = filter;
            ofn.lpstrFile = new string(new char[256]);
            ofn.nMaxFile = ofn.lpstrFile.Length;
            if (!string.IsNullOrEmpty(startingDirectory)) ofn.lpstrInitialDir = startingDirectory;
            ofn.lpstrFileTitle = new string(new char[64]);
            ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
            ofn.lpstrTitle = dialogTitle;
            ofn.Flags = Const.OFN_EXPLORER | Const.OFN_PATHMUSTEXIST;
            if (fileExistOverwriteAlert) ofn.Flags |= Const.OFN_OVERWRITEPROMPT;
            if (!filter.IsNullOrEmpty()) ofn.lpstrDefExt = filters[0];

            if (Comdlg32.GetSaveFileName(ref ofn)) return ofn.lpstrFile;
            else return string.Empty;
        }
        public static void LockWindowUpdate(nint handle)
        {
            User32.SendMessage(handle, WM_SETREDRAW, false, nint.Zero);
        }
        public static void UnlockWindowUpdate(nint handle)
        {
            User32.SendMessage(handle, WM_SETREDRAW, true, nint.Zero);
        }
        /// <summary>
        /// 枚举当前桌面所有的句柄！
        /// </summary>
        /// <returns></returns>
        public static WindowInfo[] EnumWindows()
        {
            List<WindowInfo> wndList = new List<WindowInfo>();

            //enum all desktop windows 
            User32.EnumWindows(delegate (nint hWnd, int lParam)
            {
                WindowInfo wnd = new WindowInfo();
                StringBuilder sb = new StringBuilder(256);
                //get hwnd 
                wnd.hWnd = hWnd;
                //get window name 
                User32.GetWindowTextW(hWnd, sb, sb.Capacity);
                wnd.szWindowName = sb.ToString();
                //get window class 
                User32.GetClassNameW(hWnd, sb, sb.Capacity);
                wnd.szClassName = sb.ToString();
                //add it into list 
                wndList.Add(wnd);
                return true;
            }, 0);

            return wndList.ToArray();
        }
    }
    public struct MemoryInfo
    {
        public uint MemoryLoad { get; set; }
        public ulong TotalPhysicalMemory { get; set; }
        public ulong AvailablePhysicalMemory { get; set; }
    }
}
