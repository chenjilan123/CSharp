using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CSharp.Http
{
    public class HttpWebClient
    {
        public void RunDemo()
        {
            //RequestBaidu();
            //Console.WriteLine("Halo1");

            TcpRequestHttp();
            Console.WriteLine("Halo2");
        }

        private async void TcpRequestHttp()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ipHostEntry = Dns.GetHostEntry("http://www.baidu.com");
            //ipHostEntry.
            await socket.ConnectAsync(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 15050));


        }

        /// <summary>
        /// 该示例演示了Task在执行时间不同时的不同表现。
        /// </summary>
        private async void RequestBaidu()
        {
            var webClient = new WebClient();
            //百度时间较短，返回后，上下文线程仍是主线程。
            //using (var stream = webClient.OpenRead("http://www.baidu.com"))
            //新浪时间较长，返回后，上下文线程变为子线程。
            using (var stream = webClient.OpenRead("http://sports.sina.com.cn"))
            using (var sr = new StreamReader(stream, Encoding.UTF8))
            {
                Console.WriteLine($"DateTime: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, Begin Read");
                var result = await sr.ReadToEndAsync().ConfigureAwait(false);
                Console.WriteLine($"DateTime: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}, ThreadId: {Thread.CurrentThread.ManagedThreadId}, End Read");
                //Console.WriteLine(result);
            }
        }
    }
}
