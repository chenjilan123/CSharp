using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.IO
{
    public class AsyncHttpServer
    {
        private readonly HttpListener _listener;

        const string RESPONSE_TEMPLATE =
            "<html><head><title>Test</title></head><body><h2>TestPage</h2><h4>Today is a good day</h4></body></html>";

        public AsyncHttpServer(int portNumber)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{portNumber}/");
        }

        public async Task Start()
        {
            _listener.Start();

            while(true)
            {
                var ctx = await _listener.GetContextAsync();// 使用I/O线程
                Console.WriteLine("Client connected...");

                using (var sw = new StreamWriter(ctx.Response.OutputStream))
                {
                    await sw.WriteAsync(RESPONSE_TEMPLATE);
                    await sw.FlushAsync();
                }
            }
        }

        public async Task Stop()
        {
            _listener.Abort();
        }
    }
}
