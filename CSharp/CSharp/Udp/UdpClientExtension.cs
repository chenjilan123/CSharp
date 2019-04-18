using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp.Udp
{
    public class UdpClientExtension
    {
        public void Start()
        {
            var client = new UdpClient();
            var ipEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            var data = Encoding.Unicode.GetBytes("你好!");
            client.Send(data, data.Length, ipEP);
            Console.WriteLine("发送消息成功");

            var dataReceived = client.Receive(ref ipEP);
            var message = Encoding.Unicode.GetString(dataReceived);
            Console.WriteLine($"接收消息: {message}");
        }
    }
}
