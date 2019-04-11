using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Server
{
    public class SocketServer
    {
        public void Run()
        {
            //Run Server and Client
            this.RunServerClient();

            return;

            //Best Practice
            this.RunPage();
            return;

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 8088);
        }

        #region RunServerClient
        private void RunServerClient()
        {
            var tServer = RunServerAsync();
            Thread.Sleep(200);
            var tClient = RunClientAsync();

            Task.WhenAll(tServer, tClient);

            Console.ReadLine();
        }
        #endregion

        #region RunClientAsync
        private Task RunClientAsync()
        {
            return Task.Run(() => RunClient());
        }

        private async Task RunClient()
        {
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ep = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 10011);
            client.Connect(ep);

            var arraySegment = new ArraySegment<byte>(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            while (true)
            {
                var count = await client.SendAsync(arraySegment, SocketFlags.None);
                Console.WriteLine($"Client send data: {count} bytes");

                Thread.Sleep(3000);
            }
        }
        #endregion

        #region RunServerAsync
        private Task RunServerAsync()
        {
            return Task.Run(() => RunServer());
        }
        private async Task RunServer()
        {
            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ep = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1}), 10011);
            server.Bind(ep);
            server.Listen(100);

            var client = await server.AcceptAsync();

            while (true)
            {
                var arraySegment = new ArraySegment<byte>();
                var count = await client.ReceiveAsync(arraySegment, SocketFlags.None);

                Console.WriteLine($"Server received data: {count} bytes");
            }
        }
        #endregion

        #region RunPage
        private void RunPage()
        {
            const string localHost = "127.0.0.1";
            const string baidu = "www.baidu.com";
            const string sina = "www.sina.com.cn";
            const string sinasports = "sports.sina.com.cn";
            //const string baidu1 = "14.215.177.38";

            var server = sina;
            Socket socket = null;

            var hostEntry = Dns.GetHostEntry(server);
            if (hostEntry.AddressList.Length <= 0)
            {
                Console.WriteLine("No address");
            }
            foreach (var address in hostEntry.AddressList)
            {
                //Console.WriteLine(address);
                var ipe = new IPEndPoint(address, 80);
                Socket tempSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tempSocket.Connect(ipe);
                if (tempSocket.Connected)
                {
                    Console.WriteLine($"Socket connected, Address Type: {ipe.AddressFamily}");
                    socket = tempSocket;
                    break;
                }
            }
            if (socket == null)
            {
                Console.WriteLine("No socket connected.");
                return;
            }
            var request = $"GET /HTTP/1.1\r\n" +
                $"Host: {server}\r\n" +
                $"Connection: Close\r\n\r\n";
            var bytesSent = Encoding.ASCII.GetBytes(request);
            var bytesReceived = new byte[256];
            string page = "";
            using (socket)
            {
                socket.Send(bytesSent, bytesSent.Length, 0);
                int bytes = 0;
                page = $"Default HTML page on {server}:\r\n";
                do
                {
                    bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                    page += Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                }
                while (bytes > 0);
            }
            Console.WriteLine(page);
        }
        #endregion
    }
}
