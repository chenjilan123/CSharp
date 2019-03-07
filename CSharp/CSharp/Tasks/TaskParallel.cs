using CSharp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Tasks
{
    public class TaskParallel
    {
        public TaskParallel TaskIsThreadPool()
        {
            Console.WriteLine($"Main thread is thread pool thread: {Thread.CurrentThread.IsThreadPoolThread}");

            new Thread(() =>
            {
                Console.WriteLine($"Thread is thread pool thread: {Thread.CurrentThread.IsThreadPoolThread}");
            }).Start();

            var task = new Task(() =>
            {
                //True
                Console.WriteLine($"Task is thread pool thread: {Thread.CurrentThread.IsThreadPoolThread}");
            });
            task.Start();

            return this;
        }

        public TaskParallel ParellelTask()
        {
            var tasks = new List<Task>();
            for (int i = 0; i < 5; i++)
            {
                var worker = new TaskWorker1();
                var task = worker.StartWork();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            return this;
        }


    }
}
