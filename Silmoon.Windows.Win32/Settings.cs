using Microsoft.Win32;
using Silmoon.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Silmoon.Windows.Win32
{
    public sealed class Settings
    {
        public static void SetAutoRun(string objectName, string path)
        {
            using RegistryKey hklm = Registry.LocalMachine;
            CSharpHelper.TryRun(() =>
            {
                RegistryKey run = hklm.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", RegistryKeyPermissionCheck.ReadWriteSubTree);
                run.SetValue(objectName, path, RegistryValueKind.String);
                hklm.Close();
            });
        }
        public static void DelAutoRun(string objectName)
        {
            using RegistryKey hklm = Registry.LocalMachine;
            CSharpHelper.TryRun(() =>
            {
                RegistryKey run = hklm.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", RegistryKeyPermissionCheck.ReadWriteSubTree);
                run.DeleteValue(objectName, false);
            });
        }
        public static bool IsAutoRun(string objectName)
        {
            using RegistryKey hklm = Registry.LocalMachine;
            var result = CSharpHelper.TryRun(() =>
            {
                RegistryKey run = hklm.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                return run.GetValueNames().Contains(objectName);
            });
            return result.State;
        }
    }
}
