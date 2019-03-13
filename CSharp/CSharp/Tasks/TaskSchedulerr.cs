using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Tasks
{
    public class TaskSchedulerr
    {
        public TaskSchedulerr Base()
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine();
            //return this.ContinueTask1();
            return this.Scheduler();
        }

        #region CintinueTask
        private TaskSchedulerr ContinueTask1()
        {
            var t1 = new Task(() => Worker("Task 1"));
            t1.Start();

            var t2 = t1.ContinueWith(t => Worker("Task 2"));

            t1.Wait();
            t2.Wait();
            Console.WriteLine("End all.");
            return this.ContinueTask2();
        }

        private void Worker(string taskName)
        {
            Console.WriteLine($"{taskName} start.");
            Thread.Sleep(1000);
            Console.WriteLine($"{taskName} end.");
        }

        private TaskSchedulerr ContinueTask2()
        {
            Console.WriteLine();
            var t1 = new Task<string>(() =>
            {
                var t2 = Task.Factory.StartNew(() => Worker("Task 2"), TaskCreationOptions.AttachedToParent);
                Thread.Sleep(200);
                return "Message from task 1";
            });
            t1.Start();

            //t2结束t1才算完成。
            //子任务结束, 父任务才算完成。
            t1.Wait();

            Console.WriteLine($"Task completed. Result: {t1.Result}");
            Console.WriteLine("End all.");
            return this;
        }
        #endregion

        #region ParallelTask
        private TaskSchedulerr ParallelTask()
        {
            var t1 = new Task<int>(() =>
            {
                Thread.Sleep(200);
                Console.WriteLine("Say hello in task 1");
                return 5;
            });
            var t2 = new Task<int>(() =>
            {
                Thread.Sleep(300);
                Console.WriteLine("Say helo in task 2");
                return 6;
            });
            t1.Start();
            t2.Start();
            var whenAllTask = Task.WhenAll(t1, t2);
            var t3 = whenAllTask.ContinueWith(t =>
            {
                var all = t.Result[0] + t.Result[1];
                Thread.Sleep(200);
                Console.WriteLine($"Get aggregrate result: {all}");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            whenAllTask.Wait();
            Console.WriteLine("Parallel end.");
            t3.Wait();
            Console.WriteLine("All end.");
            return this;
        }
        #endregion

        #region Scheduler
        private TaskSchedulerr Scheduler()
        {



            return this;
        }
        #endregion

        private async Task<int> GetMathAsync()
        {
            Task t1 = new Task(() => Console.WriteLine(""));
            await t1;
            return 5;
        }
    }
}
