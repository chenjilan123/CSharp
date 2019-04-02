﻿using ConsoleApp1;
using CSharp.BestPractice;
using CSharp.Core;
using CSharp.IO;
using CSharp.PLINQs;
using CSharp.Rx;
using CSharp.Tasks;
using CSharp.Threads;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var doc = new XmlDocument();
            //doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><Data><PlatProtocolName>GDOC</PlatProtocolName><Id>5</Id><Name>广东省公务车转发</Name><IsOpen>true</IsOpen><UpdateTime>2019-05-24 01:01:00</UpdateTime><Field1>100234</Field1><Field2>343xsdafjhk</Field2><Field3>100.43</Field3><Field4>false</Field4><Level>10</Level><Descript>Hello Bike</Descript><Order>50.2</Order><IsEnabled>true</IsEnabled><SpField1>2100234</SpField1><SpField2>2343xsdafjhk</SpField2><SpField3>2100.43</SpField3><SpField4>false</SpField4></Data>");
            //var node = doc.CreateElement("Node1");
            //node.InnerText = "hehe";
            //doc.LastChild.AppendChild(node);
            //Console.WriteLine(doc.InnerXml.ToString());
            //return;

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
                AsyncIO();
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
            var timer = new Timers.TimerLoop();
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

        #region MultiTaskFramework
        private static void MultiTaskFramework()
        {
            new MultiTaskFramework()
                .Main();
        }
        #endregion

        #region TaskAsyncKeyword
        private static void TaskAsyncKeyword()
        {
            new TaskAsyncKeyword()
                .Await();
        }
        #endregion

        #region CustomAwaitable
        private static void CustomAwaitable()
        {
            Task t = ProcessAsync();
            t.Wait();
        }
        static async Task ProcessAsync()
        {
            var sync = new CustomAwaitable(true);
            string result = await sync;
            Console.WriteLine($"Result: {result}");

            var async = new CustomAwaitable(false);
            result = await async;
            Console.WriteLine($"Result: {result}");
        }
        #endregion

        #region DynamicAwait
        private static void DynamicAwait()
        {
            new DynamicAwait()
                .Start();
        }
        #endregion

        #region ParallelApi
        private static void ParallelApi()
        {
            new ParallelApi()
                .Invoke();
        }
        #endregion

        #region PLINQApi
        private static void PLINQApi()
        {
            new PLINQApi()
                .Exception();
        }
        #endregion

        #region Reactive
        private static void Reactive()
        {
            new Reactive().Run();
        }

        private static void SubjectExample()
        {
            Rx.SubjectExample.Run();
        }
        #endregion

        #region AsyncIO
        private static void AsyncIO()
        {
            new AsyncIO().Run();
        }
        #endregion
    }
}
