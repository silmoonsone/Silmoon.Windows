using Silmoon.Windows.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Forms.Extensions
{
    public class FormHelper
    {
        [DllImport("shell32.dll")]
        private static extern int SHGetPropertyStoreForWindow(IntPtr hwnd, ref Guid iid, [Out, MarshalAs(UnmanagedType.Interface)] out IPropertyStore propertyStore);

        private static readonly Guid IID_IPropertyStore = new Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99");

        // System.AppUserModel.ID
        private static readonly PROPERTYKEY PKEY_AppUserModel_ID = new PROPERTYKEY { fmtid = new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), pid = 5 };


        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct PROPERTYKEY
        {
            public Guid fmtid;
            public uint pid;
        }

        [ComImport]
        [Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IPropertyStore
        {
            int GetCount(out uint cProps);
            int GetAt(uint iProp, out PROPERTYKEY pkey);
            int GetValue(ref PROPERTYKEY key, out PropVariant pv);
            int SetValue(ref PROPERTYKEY key, [In] PropVariant pv);
            int Commit();
        }

        [StructLayout(LayoutKind.Explicit)]
        private sealed class PropVariant : IDisposable
        {
            [FieldOffset(0)] private ushort vt;
            [FieldOffset(8)] private IntPtr ptr;

            public PropVariant(string value)
            {
                vt = 31; // VT_LPWSTR
                ptr = Marshal.StringToCoTaskMemUni(value);
            }

            public void Dispose()
            {
                PropVariantClear(this);
                GC.SuppressFinalize(this);
            }

            ~PropVariant()
            {
                Dispose();
            }

            [DllImport("ole32.dll")]
            private static extern int PropVariantClear([In, Out] PropVariant pvar);
        }

        public static void SetWindowAppId(IntPtr hwnd, string appId)
        {
            Guid iid = IID_IPropertyStore;
            int hr = SHGetPropertyStoreForWindow(hwnd, ref iid, out var store);
            if (hr != 0) Marshal.ThrowExceptionForHR(hr);

            using var pv = new PropVariant(appId);

            var key = PKEY_AppUserModel_ID;
            hr = store.SetValue(ref key, pv);
            if (hr != 0) Marshal.ThrowExceptionForHR(hr);

            hr = store.Commit();
            if (hr != 0) Marshal.ThrowExceptionForHR(hr);
        }

        public static void LockWindowUpdate(nint handle)
        {
            Win32ApiHelper.LockWindowUpdate(handle);
        }
        public static void UnlockWindowUpdate(nint handle, Control control = null)
        {
            Win32ApiHelper.UnlockWindowUpdate(handle);

            if (control is not null)
            {
                control.Invalidate();
                control.Update();
            }
        }
        public static void UnlockWindowUpdate(Control control) => UnlockWindowUpdate(control.Handle, control);
    }
}
