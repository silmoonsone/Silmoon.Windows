using Silmoon.Windows.Win32Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Windows.Forms.Extensions
{
    public class FormHelper
    {
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
