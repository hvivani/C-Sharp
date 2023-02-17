using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace tcplistener_console
{
    public class ClientState
    {
        public TcpClient client;
        public byte[] buffer;
    }
}
