using System;
using System.Threading;
using System.Timers;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new System.Timers.Timer(3000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            Console.WriteLine("Press \"Enter\" to quit");
            Console.ReadLine();

            timer.Stop();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var id = Guid.NewGuid().ToString();
            var round = 1;
            while(true)
            {
                Console.WriteLine($"{id}: {round++}");
                Thread.Sleep(5000);
                ThreadPool.GetAvailableThreads(out int threads, out int completionPortThreads);
                Console.WriteLine($"Threads: {threads}, CompletionPortThreads: {completionPortThreads}");
            }
        }
    }
}
