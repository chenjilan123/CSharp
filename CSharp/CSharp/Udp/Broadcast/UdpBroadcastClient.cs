using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp.Udp.Broadcast
{
    public class UdpBroadcastClient
    {
        //接收来自8010端口的广播
        public void Start()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var localEP = new IPEndPoint(IPAddress.Loopback, 8010);
            socket.Bind(localEP);

            var ep = (EndPoint)localEP;
            var data = new byte[1024];
            while(true)
            {
                socket.ReceiveFrom(data, ref ep);
                var message = Encoding.Unicode.GetString(data);
                Console.WriteLine($"接收消息: {message}");
            }
        }
    }
}
