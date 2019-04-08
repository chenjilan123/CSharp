using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp
{
    class Program
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket?view=netframework-4.7.2
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            const string localHost = "127.0.0.1";
            const string baidu = "www.baidu.com";
            const string sina = "www.sina.com.cn";
            const string sinasports = "sports.sina.com.cn";
            //const string baidu1 = "14.215.177.38";

            var server = baidu;
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
    }
}
