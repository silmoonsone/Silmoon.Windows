using Silmoon.Windows.Win32.Apis;
using Silmoon.Windows.Win32.Structs;
using Silmoon.Windows.Win32Api.Apis;
using Silmoon.Windows.Win32Api.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Win32Api
{
    public class Win32ApiHelper
    {
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
        public static string OpenFileDialog(string[] filters, string filterName, string dialogTitle, string startingDirectory = null)
        {
            var ofn = new OPENFILENAME();
            ofn.lStructSize = Marshal.SizeOf(ofn);

            // 构建过滤器字符串
            string filter = $"{filterName} (";
            foreach (string filterExt in filters)
            {
                filter += $"*.{filterExt};";
            }
            filter = filter.TrimEnd(';') + ")\0";  // 对于每种文件类型结束时添加 '\0'
            foreach (string filterExt in filters)
            {
                filter += $"*.{filterExt};";
            }
            filter = filter.TrimEnd(';') + "\0\0";  // 字符串结束添加 '\0\0'
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

        public static string SaveFileDialog(string[] filters, string filterName, string dialogTitle, bool fileExistOverwriteAlert = true, string startingDirectory = null)
        {
            var ofn = new OPENFILENAME();
            ofn.lStructSize = Marshal.SizeOf(ofn);

            // 构建过滤器字符串
            string filter = $"{filterName} (";
            foreach (string filterExt in filters)
            {
                filter += $"*.{filterExt};";
            }
            filter = filter.TrimEnd(';') + ")\0";  // 对于每种文件类型结束时添加 '\0'
            foreach (string filterExt in filters)
            {
                filter += $"*.{filterExt};";
            }
            filter = filter.TrimEnd(';') + "\0\0";  // 字符串结束添加 '\0\0'
            ofn.lpstrFilter = filter;
            ofn.lpstrFile = new string(new char[256]);
            ofn.nMaxFile = ofn.lpstrFile.Length;
            if (!string.IsNullOrEmpty(startingDirectory)) ofn.lpstrInitialDir = startingDirectory;
            ofn.lpstrFileTitle = new string(new char[64]);
            ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
            ofn.lpstrTitle = dialogTitle;
            ofn.Flags = fileExistOverwriteAlert ? 0x00000002 : 0x00000000;
            if (Comdlg32.GetSaveFileName(ref ofn))
                return ofn.lpstrFile;
            return string.Empty;
        }
    }
    public struct MemoryInfo
    {
        public uint MemoryLoad { get; set; }
        public ulong TotalPhysicalMemory { get; set; }
        public ulong AvailablePhysicalMemory { get; set; }
    }
}
