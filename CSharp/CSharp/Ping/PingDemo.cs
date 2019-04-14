using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp.Ping
{
    public class PingDemo
    {
        public void Start()
        {
            var hostInfo = GetHostInfo();
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            socket.ReceiveTimeout = 1000;

            EndPoint ep = new IPEndPoint(hostInfo.AddressList[0], 0);


            Console.WriteLine("End");
        }

        private IPHostEntry GetHostInfo()
        {
            IPHostEntry hostInfo = null;
            bool isHostRight = false;
            while (!isHostRight)
            {
                Console.WriteLine("Please input the host address: ");
                var hostName = Console.ReadLine();

                try
                {
                    hostInfo = Dns.GetHostEntry(hostName);
                    isHostRight = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Unrecognizable host name");
                }
            }

            return hostInfo;
        }
    }
}
