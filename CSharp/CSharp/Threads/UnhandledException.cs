using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharp.Threads
{
    public class UnhandledException
    {
        public void Throw()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Application running...");
                    Thread.Sleep(1000);
                }
            }).Start();
            new Thread(() =>
            {
                throw new Exception("Message from sub thread unhandled exception.");
            }).Start();

        }
    }
}
