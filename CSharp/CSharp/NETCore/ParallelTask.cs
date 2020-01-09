using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.NETCore
{
    public class ParallelTask : IProgram
    {
        private int _iCount = 0;

        public void Run()
        {
            using (new TaskTimer("Runner"))
            {
                var lst = new List<Task>();
                for (int i = 0; i < 100000; i++)
                {
                    var t = RequestAsync();
                    lst.Add(t);
                }
                Task.WhenAll(lst).Wait();
            }
        }

        private async Task RequestAsync()
        {
            var i = Interlocked.Increment(ref _iCount);
            using (new TaskTimer($"Request {i}"))
            {
                var client = new WebClient();
                var address = "http://www.baidu.com";
                var uri = new Uri(address);
                var response = await client.DownloadStringTaskAsync(uri);
            }
        }
    }
}
