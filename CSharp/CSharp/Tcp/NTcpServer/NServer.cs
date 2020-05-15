﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.IO.Pipelines;

namespace CSharp.Tcp
{
    /// <summary>
    /// 服务器
    /// </summary>
    public class NServer
    {
        //List<TcpClient> _clients = new List<TcpClient>();
        
        public async Task StartAsync()
        {
            var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 18000);
            var server = new TcpListener(ep);
            server.Start();
            Console.WriteLine($"Server ready. Local address: {ep}");
            while(true)
            {
                var client = await server.AcceptTcpClientAsync();
                Console.WriteLine($"New client accepted: {client.Client.RemoteEndPoint}");
                var nClient = CreateClient(client);
                await nClient.StartAsync();
            }
        }

        private NClient CreateClient(TcpClient client)
        {
            var nClient = new NClient(client);
            return nClient;
        }
    }
}