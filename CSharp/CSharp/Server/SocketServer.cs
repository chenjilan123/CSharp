using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CSharp.Server
{
    public class SocketServer
    {
        public void RunDemo()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 8088);

        }
    }
}
