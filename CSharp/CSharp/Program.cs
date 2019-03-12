﻿using CSharp.BestPractice;
using CSharp.Tasks;
using CSharp.Threads;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task
            //RunTask();
            //Thread.Sleep(100);
            //Console.WriteLine("Hello World!");


            ////StopWatch
            //var sw = Stopwatch.StartNew();
            //Thread.Sleep(500);
            //Console.WriteLine("First elapsed" + sw.Elapsed);
            ////sw.Restart(); //Reset and start
            //sw.Reset();
            //sw.Start();
            //Thread.Sleep(500);
            //Console.WriteLine("Second elapsed" + sw.Elapsed);
            //return;

            //File IO Demo
            //Threads.OfficialThreadPoolFileIODemo.Run();
            //return;
            try
            {
                TaskSchedulerr();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Main thread get a exception.");
                Console.WriteLine(ex.ToString());
            }
        }

        #region Task
        private static async void RunTask()
        {
            Thread.Sleep(500);

            var task = Task.Run(() => Console.WriteLine("Hello"));
            await task;

            Console.WriteLine("Completed");
            Thread.Sleep(500);
        }
        #endregion

        #region TimerLoop
        private static void TimerLoop()
        {
            var timer = new Timer.TimerLoop();
            timer.Run();
        }
        #endregion

        #region ManagedThreadPool
        private static void ManagedThreadPool()
        {
            //TimerLoop();
            //Thread.Sleep(5000);
            var managedThreadPool = new Threads.ManagedThreadPool();
            managedThreadPool
                //.Information()
                //.ThreadPoolException()
                //.Cancellation();
                .Timeout();
                //.APM();
                //.RequestThread();
        }
        #endregion

        #region TaskParallel
        private static void TaskParallel()
        {
            var taskParallel = new Tasks.TaskParallel();
            taskParallel
                //.TaskIsThreadPool()
                .ParellelTask();
        }
        #endregion

        #region UpdateAsync
        private static void UpdateAsync()
        {
            var core = new UpdateAsync();

            //Base
            //Console.WriteLine("Enter method");
            //var task = core.Begin();
            //Console.WriteLine("Quit method");
            //Console.WriteLine("Begin wait...");
            //task.Wait();
            //Console.WriteLine($"Result: {task.Result}");

            //Parallel
            core.ParallelProcess();


        }
        #endregion

        #region GetPositionDemo
        private static void GetPositionDemo()
        {
            var geoLocation = new GeoLocation();
            geoLocation.GetPosition();
            Console.WriteLine("Get Position Completed!");

            geoLocation.GetPosition();
            Console.WriteLine("Get Position Completed!");


        }
        #endregion

        #region Monitor
        private static void Monitor()
        {
            var monitor = new MonitorLock();
            monitor.EnterLock();
        }
        #endregion

        #region UnhandledException
        private static void UnhandledException()
        {
            var core = new UnhandledException();
            core.Throw();
        }
        #endregion

        #region TaskSchedulerr
        private static void TaskSchedulerr()
        {
            new TaskSchedulerr()
                .Base();
        }
        #endregion
    }
}
