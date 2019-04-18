using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp.Udp
{
    public class UdpServerExtension
    {
        public void Start()
        {
            var server = new UdpClient(8000);
            var ipEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001);
            var dataReceived = server.Receive(ref ipEP);
            var message = Encoding.Unicode.GetString(dataReceived);
            Console.WriteLine($"接收消息: {message}");

            var data = Encoding.Unicode.GetBytes("欢迎您!");
            server.Send(data, data.Length, ipEP);
            Console.WriteLine($"发送消息成功");
        }
    }
}
