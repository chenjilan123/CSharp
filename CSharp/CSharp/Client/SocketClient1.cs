using CSharp.Handler;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CSharp.Client
{
    public class SocketClient1
    {
        private readonly SocketMsgHandler _msgHandler;

        public SocketClient1(SocketMsgHandler msgHandler)
        {
            _msgHandler = msgHandler;
        }

        public void Start()
        {
            const string hostName = "127.0.0.1";
            const int port = 8000;

            IPAddress ip = IPAddress.Parse(hostName);
            IPEndPoint ep = new IPEndPoint(ip, port);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //1、仅建立连接的过程与Server不同
            socket.Connect(ep);

            //Recv
            _msgHandler.RecvMsg(socket);
            //Send
            _msgHandler.SendMsg(socket);
        }
    }
}
