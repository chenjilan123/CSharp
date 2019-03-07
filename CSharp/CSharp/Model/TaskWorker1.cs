using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Model
{
    public class TaskWorker1
    {
        private static int _idProvider = 1;

        public int Id { get; set; }

        public TaskWorker1()
        {
            this.Id = _idProvider++;
        }

        public Task StartWork()
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} id({Thread.CurrentThread.ManagedThreadId}) TaskWorker{Id}: {this.Id} start work.");
                var workerTasks = new List<Task>();
                for (int i = 0; i < 5; i++)
                {
                    var worker = new TaskWorker2();
                    worker.Name = "Worker" + i.ToString();
                    var task = worker.RunSpecialWork();
                    workerTasks.Add(task);
                }
                Task.WaitAll(workerTasks.ToArray());
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} id({Thread.CurrentThread.ManagedThreadId}) TaskWorker{Id}: {this.Id} end work.");
            });
        }
    }
}
