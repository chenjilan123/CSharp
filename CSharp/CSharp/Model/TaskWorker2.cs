using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Model
{
    public class TaskWorker2
    {
        public string Name { get; set; }
        public Task RunSpecialWork()
        {
            return Task.Factory.StartNew(()=>
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} id({Thread.CurrentThread.ManagedThreadId}) {Name} start work.");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} id({Thread.CurrentThread.ManagedThreadId}) {Name} worker process: {i}");
                    Thread.Sleep(5000);
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} id({Thread.CurrentThread.ManagedThreadId}) {Name} end work.");
            });
        }
    }
}
