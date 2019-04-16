using CSharp.Handler;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CSharp.Server
{
    public class SocketServer1
    {
        private const string Ready = "Redy";
        private const string Connection = "Conn";

        private readonly SocketMsgHandler _msgHandler;
        public SocketServer1(SocketMsgHandler msgHandler)
        {
            _msgHandler = msgHandler;
        }

        public void Run()
        {
            const string hostName = "127.0.0.1";
            const int port = 8000;

            StartServer(hostName, port);
        }

        private void StartServer(string hostName, int port)
        {
            IPAddress ip = IPAddress.Parse(hostName);
            IPEndPoint server = new IPEndPoint(ip, port);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Conn
            socket.Bind(server);
            socket.Listen(10);
            PrintLog(Ready, $"Server({socket.LocalEndPoint.ToString()}) is ready, waiting for client connection...");
            var client = socket.Accept();
            PrintLog(Connection, $"Client connecting, remote endpoint: {client.RemoteEndPoint.ToString()}");

            //Recv
            _msgHandler.RecvMsg(client);
            //Send
            _msgHandler.SendMsg(client);
        }

        private void PrintLog(string operate, string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} - {operate}: {message}");
        }
    }
}
