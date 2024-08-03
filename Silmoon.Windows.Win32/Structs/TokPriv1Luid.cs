using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Silmoon.Windows.Win32.Structs
{
    // 这个结构体将会传递给API。使用StructLayout(...特性，确保其中的成员是按顺序排列的，C#编译器不会对其进行调整。 
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TokPriv1Luid
    {
        public int Count;
        public long Luid;
        public int Attr;
    }
}
