using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.AsyncProcess
{
    /// <summary>
    /// Stack 优先处理最近的任务
    /// </summary>
    public class ProcessAsyncByStack
    {
        public void Run()
        {
            Console.WriteLine("Produce 20 tasks and 4 processors...");
            var t = RunProgram();
            t.Wait();

            //为什么t.Wait立刻结束了？ 因为返回了void
            //Console.ReadLine();
        }

        public static async Task RunProgram()
        {
            var taskQueue = new ConcurrentStack<CustomTask>();
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

        static async void TaskProducer(ConcurrentStack<CustomTask> queue)
        {
            for (int i = 1; i <= 20; i++)
            {
                await Task.Delay(50);

                #region Produce Framework
                //Here lies the process for produce CustomTask
                var workItem = new CustomTask { Id = i };
                #endregion

                queue.Push(workItem);
                Console.WriteLine($"Task {workItem.Id} has been posted");
            }
        }

        //此处写成void将导致任务立即完成。
        //static async void TaskProcessor(ConcurrentStack<CustomTask> queue, string name, CancellationToken token)
        static async Task TaskProcessor(ConcurrentStack<CustomTask> queue, string name, CancellationToken token)
        {
            CustomTask workItem;
            bool dequeueSuccessful = false;
            await GetRandomDelay();
            do
            {
                dequeueSuccessful = queue.TryPop(out workItem);
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
