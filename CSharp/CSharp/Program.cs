using System;
using System.Threading;
using System.Timers;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //File IO Demo
            //Threads.OfficialThreadPoolFileIODemo.Run();
            //return;

            try
            {
                TaskParallel();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Main thread get a exception.");
                Console.WriteLine(ex.ToString());
            }
        }

        private static void TimerLoop()
        {
            var timer = new Timer.TimerLoop();
            timer.Run();
        }

        private static void ManagedThreadPool()
        {
            TimerLoop();
            Thread.Sleep(5000);
            var managedThreadPool = new Threads.ManagedThreadPool();
            managedThreadPool
                .Information()
                //.ThreadPoolException()
                .RequestThread();
        }

        private static void TaskParallel()
        {
            var taskParallel = new Tasks.TaskParallel();
            taskParallel
                //.TaskIsThreadPool()
                .ParellelTask();
        }
    }
}
