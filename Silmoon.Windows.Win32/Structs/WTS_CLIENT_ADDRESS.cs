using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Silmoon.Windows.Win32.Structs
{
    public struct WTS_CLIENT_ADDRESS
    {
        public AddressFamily AF;
        public byte[] AddressBytes;
    }
}
