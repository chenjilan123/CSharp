using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace CSharp.Tcp
{
    public class TcpClientExtension
    {
        private readonly Encoding _encoding = Encoding.Unicode;

        public void Start()
        {
            var client = new TcpClient();

            //Conn
            client.Connect("127.0.0.1", 8088);

            //Recv
            using (var ns = client.GetStream())
            using (var sr = new StreamReader(ns, _encoding))
            {
                //TODO: 接收堵塞
                var message = sr.ReadLine();
                Console.WriteLine(message);
            }

            //Send
            using (var ns = client.GetStream())
            {
                var dataMessage = _encoding.GetBytes("欢迎！");
                ns.Write(dataMessage, 0, dataMessage.Length);
            }

            client.Close();

            Console.ReadLine();
        }
    }
}
