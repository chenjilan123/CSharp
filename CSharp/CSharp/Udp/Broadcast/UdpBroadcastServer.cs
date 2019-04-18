using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp.Udp.Broadcast
{
    public class UdpBroadcastServer
    {
        //广播到8010端口, 广播时无需绑定端口
        public void Start()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var remoteEP = new IPEndPoint(IPAddress.Loopback, 8010);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

            //发送
            var data = Encoding.Unicode.GetBytes("欢迎您!");
            socket.SendTo(data, remoteEP);

            //关闭
            socket.Close();
        }
    }
}
