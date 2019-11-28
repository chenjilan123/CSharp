using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Tcp.NTcpClient
{
    public class NTcpClient
    {
        List<TcpClient> Clients = new List<TcpClient>();
        public void Run()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"{i}-ThreadId:{Thread.CurrentThread.ManagedThreadId}-准备连接");
                var t = RunClient(i);
                Console.WriteLine($"{i}-ThreadId:{Thread.CurrentThread.ManagedThreadId}-连接完毕");
            }
            while (true)
            {
                var arrClients = Clients.ToArray();
                foreach (var client in arrClients)
                {
                    Send(client);
                }
                Thread.Sleep(5000);
            }
            Console.ReadLine();
        }

        private async Task RunClient(int i)
        {
            var client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 18000).ConfigureAwait(false);
            Console.WriteLine($"{client.Client.LocalEndPoint}-{i}-ThreadId:{Thread.CurrentThread.ManagedThreadId}-连接成功");
            Clients.Add(client);
            //有一定概率回到(主线程)上下文线程
            //var stream = client.GetStream();
            //while (client.Connected)
            //{
            //    try
            //    {
            //        var data = new byte[]
            //        {
            //            0x5B, 0x11, 0x22, 0x33, 0x44, 0x5D
            //        };
            //        Console.WriteLine($"{client.Client.LocalEndPoint}-{i}-ThreadId:{Thread.CurrentThread.ManagedThreadId}-开始发送");
            //        stream.Write(data, 0, data.Length);
            //        Thread.Sleep(5000);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
            //Console.WriteLine($"{client.Client.LocalEndPoint}-{i}-ThreadId:{Thread.CurrentThread.ManagedThreadId}-断开连接");
            //return;

            //这样不宜多个任务，线程池线程不宜长时间运行。
            //var t = new Task(() =>
            //{
            //    var stream = client.GetStream();
            //    while (client.Connected)
            //    {
            //        try
            //        {
            //            var data = new byte[]
            //            {
            //                0x5B, 0x11, 0x22, 0x33, 0x44, 0x5D
            //            };
            //            Console.WriteLine($"{client.Client.LocalEndPoint}-{i}-ThreadId:{Thread.CurrentThread.ManagedThreadId}-开始发送");
            //            stream.Write(data, 0, data.Length);
            //            Thread.Sleep(5000);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }
            //    }
            //    Console.WriteLine($"{client.Client.LocalEndPoint}-{i}-ThreadId:{Thread.CurrentThread.ManagedThreadId}-断开连接");
            //});
            //t.Start();
        }

        private void Send(TcpClient client)
        {
            var stream = client.GetStream();
            try
            {
                var data = new byte[]
                {
                            0x5B, 0x11, 0x22, 0x33, 0x44, 0x5D
                };
                Console.WriteLine($"{client.Client.LocalEndPoint}-ThreadId:{Thread.CurrentThread.ManagedThreadId}-开始发送");
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"{client.Client.LocalEndPoint}-ThreadId:{Thread.CurrentThread.ManagedThreadId}-发送完毕");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Console.WriteLine($"{client.Client.LocalEndPoint}-ThreadId:{Thread.CurrentThread.ManagedThreadId}-断开连接");
        }
    }
}
