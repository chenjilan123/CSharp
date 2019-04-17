using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp.Server
{
    public class AsyncSocketServer1
    {
        private Encoding _encoding = Encoding.Unicode;

        public void Start()
        {
            var localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8088);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(localEP);

            socket.Listen(10);

            socket.BeginAccept(new AsyncCallback(Accept), socket);

            Console.ReadLine();
        }

        private void Accept(IAsyncResult ar)
        {
            var socket = ar.AsyncState as Socket;
            var client = socket.EndAccept(ar);

            Console.WriteLine($"客户端连接: {client.RemoteEndPoint.ToString()}");

            var dataMessage = _encoding.GetBytes("欢迎您!");
            client.BeginSend(dataMessage, 0, dataMessage.Length, SocketFlags.None, new AsyncCallback(Send), client);

        }

        private void Send(IAsyncResult ar)
        {
            var client = ar.AsyncState as Socket;
            client.EndSend(ar);

            Console.WriteLine("消息发送成功");
            var pack = new ReceivePack(client);
            client.BeginReceive(pack.Data, 0, ReceivePack.DataSize, SocketFlags.None, new AsyncCallback(Receive), pack);
        }

        private void Receive(IAsyncResult ar)
        {
            var pack = ar.AsyncState as ReceivePack;
            var dataLength = pack.Socket.EndReceive(ar);
            var message = _encoding.GetString(pack.Data, 0, dataLength);


            Console.WriteLine($"客户：{message}");
        }
    }

    public class ReceivePack
    {
        public const int DataSize = 1024;
        public Socket Socket { get; }
        public byte[] Data { get; }

        public ReceivePack(Socket socket)
        {
            Socket = socket;
            Data = new byte[DataSize];
        }
    }
}
