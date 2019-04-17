using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp.Client
{
    public class AsyncSocketClient1
    {
        private readonly Encoding _encoding = Encoding.Unicode;

        public void Start()
        {
            var remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8088);
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            client.BeginConnect(remoteEP, new AsyncCallback(Connect), client);

            Console.ReadLine();
        }

        private void Connect(IAsyncResult ar)
        {
            var client = ar.AsyncState as Socket;
            client.EndConnect(ar);

            var pack = new ReceivePacker(client);
            client.BeginReceive(pack.Data, 0, ReceivePacker.DataSize, SocketFlags.None, new AsyncCallback(Receive), pack);
        }

        private void Receive(IAsyncResult ar)
        {
            var pack = ar.AsyncState as ReceivePacker;
            var dataLength = pack.Client.EndReceive(ar);

            var message = _encoding.GetString(pack.Data, 0, dataLength);
            Console.WriteLine($"服务器: {message}");

            var dataMessage = _encoding.GetBytes("你好！");
            pack.Client.BeginSend(dataMessage, 0, dataMessage.Length, SocketFlags.None, new AsyncCallback(Send), pack.Client);
        }

        private void Send(IAsyncResult ar)
        {
            var client = ar.AsyncState as Socket;
            client.EndSend(ar);

            Console.WriteLine("客户端: 发送信息成功");
        }
    }

    public class ReceivePacker
    {
        public const int DataSize = 1024;
        public Socket Client { get; }
        public byte[] Data { get; set; }

        public ReceivePacker(Socket client)
        {
            Client = client;
            Data = new byte[DataSize];
        }
    }
}
