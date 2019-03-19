using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.AsyncProcess
{
    public class ProcessAsyncByQueue
    {
        public void Run()
        {
            Console.WriteLine("Produce 20 tasks and 4 processors...");
            var t = RunProgram();
            t.Wait();
        }

        public static async Task RunProgram()
        {
            var taskQueue = new ConcurrentQueue<CustomTask>();
            var cts = new CancellationTokenSource();

            //Task for produce
            var taskSorce = Task.Run(() => TaskProducer(taskQueue));
            Task[] processors = new Task[4];
            for (int i = 1; i <= 4; i++)
            {
                string processorId = i.ToString();
                //Task for process
                processors[i - 1] = Task.Run(() =>
                TaskProcessor(taskQueue, $"Processor {processorId}", cts.Token));
            }
            await taskSorce;
            cts.CancelAfter(TimeSpan.FromSeconds(2D));

            await Task.WhenAll(processors);
        }

        static async Task TaskProducer(ConcurrentQueue<CustomTask> queue)
        {
            for (int i = 1; i <= 20; i++)
            {
                #region Produce Framework
                //模拟创建对象的耗时
                await Task.Delay(50);
                //Here lies the process for produce CustomTask
                var workItem = new CustomTask { Id = i };
                #endregion

                queue.Enqueue(workItem);
                Console.WriteLine($"Task {workItem.Id} has been posted");
            }
        }

        static async Task TaskProcessor(ConcurrentQueue<CustomTask> queue, string name, CancellationToken token)
        {
            CustomTask workItem;
            bool dequeueSuccessful = false;
            await GetRandomDelay();
            do
            {
                dequeueSuccessful = queue.TryDequeue(out workItem);
                if (dequeueSuccessful)
                {
               
                    #region Process Framework
                    //Here lies the process code for Custom Task.
                    //Use workItem for process all that is necessory.
                    //The process procedure can be capsulate inside CustomTask.
                    Console.WriteLine($"Task {workItem.Id} has been processed by {name}");
                    #endregion

                }
                await GetRandomDelay();
            }
            while (!token.IsCancellationRequested);
        }

        static Task GetRandomDelay()
        {
            int delay = new Random(DateTime.Now.Millisecond).Next(1, 500);
            return Task.Delay(delay);
        }
    }

}
