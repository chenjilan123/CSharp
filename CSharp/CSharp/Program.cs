using CSharp.Client;
using CSharp.Handler;
using CSharp.Host;
using CSharp.Ping;
using CSharp.Server;
using CSharp.Tcp;
using CSharp.Udp;
using CSharp.Udp.Broadcast;
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
            UdpBroadcast();
        }

        #region UdpBroadcast
        private static void UdpBroadcast()
        {
            string ope = null;
            while (true)
            {
                Console.WriteLine("Run server or client ?");
                ope = Console.ReadLine();
                if (ope == "server" || ope == "client")
                {
                    break;
                }
                Console.WriteLine("Input error, please try again.");
            }
            var msgHandler = new SocketMsgHandler();
            switch (ope)
            {
                case "server":
                    new UdpBroadcastServer().Start();
                    break;
                case "client":
                    new UdpBroadcastClient().Start();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Udp
        private static void Udp()
        {
            string ope = null;
            while (true)
            {
                Console.WriteLine("Run server or client ?");
                ope = Console.ReadLine();
                if (ope == "server" || ope == "client")
                {
                    break;
                }
                Console.WriteLine("Input error, please try again.");
            }
            var msgHandler = new SocketMsgHandler();
            switch (ope)
            {
                case "server":
                    new UdpServerExtension().Start();
                    break;
                case "client":
                    new UdpClientExtension().Start();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Tcp
        private static void Tcp()
        {
            string ope = null;
            while (true)
            {
                Console.WriteLine("Run server or client ?");
                ope = Console.ReadLine();
                if (ope == "server" || ope == "client")
                {
                    break;
                }
                Console.WriteLine("Input error, please try again.");
            }
            var msgHandler = new SocketMsgHandler();
            switch (ope)
            {
                case "server":
                    new AsyncSocketServer1().Start();
                    break;
                case "client":
                    new TcpClientExtension().Start();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Socket1
        private static void RunSocket()
        {
            string ope = null;
            while (true)
            {
                Console.WriteLine("Run server or client ?");
                ope = Console.ReadLine();
                if (ope == "server" || ope == "client")
                {
                    break;
                }
                Console.WriteLine("Input error, please try again.");
            }
            var msgHandler = new SocketMsgHandler();
            switch (ope)
            {
                case "server":
                    new SocketServer1(msgHandler).Run();
                    break;
                case "client":
                    new SocketClient1(msgHandler).Start();
                    break;
                default:
                    break;
            }
        }

        private static void RunSocket1()
        {
            string ope = null;
            while (true)
            {
                Console.WriteLine("Run server or client ?");
                ope = Console.ReadLine();
                if (ope == "server" || ope == "client")
                {
                    break;
                }
                Console.WriteLine("Input error, please try again.");
            }
            var msgHandler = new SocketMsgHandler();
            switch (ope)
            {
                case "server":
                    new AsyncSocketServer1().Start();
                    break;
                case "client":
                    new AsyncSocketClient1().Start();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Socket
        static private void Socket()
        {
            new SocketServer().Run();
        }
        #endregion

        #region Host
        private static void Host()
        {
            new HostInformation().PrintInformation();
        }
        #endregion

        #region Ping
        private static void Ping()
        {
            new PingDemo().Start();
        }
        #endregion
    }
}
