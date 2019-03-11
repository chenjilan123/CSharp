using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharp.Threads
{
    public class MonitorLock
    {
        public void EnterLock()
        {
            var lck1 = new object();

            var thread1 = new Thread(() =>
            {
                lock (lck1)
                {
                    Console.WriteLine("Thread1 enter lock");
                    Thread.Sleep(5000);
                }
                Console.WriteLine("Thread1 exit lck");
            });

            var thread2 = new Thread(() =>
            {
                Thread.Sleep(200);

                var acquireLck = false;
                try
                {
                    Monitor.Enter(lck1, ref acquireLck);
                    Console.WriteLine("Thread2 enter monitor lock");
                    Thread.Sleep(5000);
                }
                finally
                {
                    Monitor.Exit(lck1);
                    Console.WriteLine("Thread2 exit monitor lock");
                }
            });

            var thread3 = new Thread(() =>
            {
                if (Monitor.TryEnter(lck1, TimeSpan.FromSeconds(3D)))
                {
                    Console.WriteLine("Thread3 enter lock success.");
                }
                else
                {
                    Console.WriteLine("Thread3 enter lock failure.");
                }
            });

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();

            Console.WriteLine("End.");
        }
    }
}
