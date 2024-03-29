﻿using Silmoon.Windows.Win32Api.Apis;
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
    }
    public struct MemoryInfo
    {
        public uint MemoryLoad { get; set; }
        public ulong TotalPhysicalMemory { get; set; }
        public ulong AvailablePhysicalMemory { get; set; }
    }
}
