using System;
using System.Threading;
using System.Timers;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TimerLoop();
                Thread.Sleep(5000);
                ManagedThreadPool();

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
            var managedThreadPool = new Threads.ManagedThreadPool();
            managedThreadPool
                .Information()
                //.ThreadPoolException()
                .RequestThread();
        }
    }
}
