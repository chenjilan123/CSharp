using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Threading.ThreadPool;

namespace CSharp.Threads
{
    /// <summary>
    /// 线程池演示
    /// TPL(任务并行库), 异步I/O, 计时器回调, 注册等待操作, 使用委托的异步方法调用和System.Net 套接字连接
    /// 
    /// Ref: https://docs.microsoft.com/zh-cn/dotnet/standard/threading/the-managed-thread-pool
    /// </summary>
    public class ManagedThreadPool
    {
        //Ⅰ一旦线程池中的线程完成任务，它将返回到等待线程队列中。 这时开始即可重用它。通过这种重复使用，应用程序可以避免产生为每个任务创建新线程的开销。
        //Ⅱ 每个进程只有一个线程池
        //III 可以排队到线程池中的操作数仅受可用内存限制。 但是，线程池会限制进程中可同时处于活动状态的线程数。 
        //    如果所有线程池线程都处于忙碌状态，则其他工作项将进行排队，直到要执行它们的线程空闲。
        #region Information
        public ManagedThreadPool Information()
        {
            PrintInformation();

            //Max: 32768 
            SetMaxThreads(30000, 2000);
            PrintInformation();
            return this;
        }


        private void PrintInformation()
        {
            GetMaxThreads(out int workerThreads, out int completionPortThreads);
            Console.WriteLine($"      Max worker threads: {workerThreads}");
            Console.WriteLine($"Max completion port threads: {completionPortThreads}");
            GetAvailableThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"      Avaliable threads: {workerThreads}");
            Console.WriteLine($"Completion port threads: {completionPortThreads}");

        }
        #endregion

        #region ThreadPoolException
        public ManagedThreadPool ThreadPoolException()
        {
            //Ⅰ Not threadpool
            //      Unhandled Common Exception => result in a unhandled exception.

            //Console.WriteLine("Begin not thread pool work.");
            //var thread = new Thread(WorkItem)
            //{ IsBackground = true };
            //thread.Start();
            ////Abort
            //PrintState(thread);
            ////Ⅱ Abort thread and throw ThreadAbortException.
            ////thread.Abort();
            //PrintState(thread);
            //Thread.Sleep(1000);
            //Console.WriteLine("End not thread pool work.");

            Console.WriteLine("Begin thread pool work.");
            ThreadPool.QueueUserWorkItem(ThreadPoolProc);
            Thread.Sleep(3000);
            //Result in process aborted
            Console.WriteLine("End thread pool work.");
            return this;
        }

        private static void PrintState(Thread thread)
        {
            Console.WriteLine($"    Is Alive: {thread.IsAlive}");
            Console.WriteLine($"Thread stare:{thread.ThreadState}");
        }

        private void ThreadPoolProc(object obj)
        {
            WorkItem();
        }

        private void WorkItem()
        {
            Console.WriteLine($"IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
            throw new ArgumentException("Argument out of range.");
            try
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                while (true)
                {

                    Thread.Sleep(2000);
                    Console.WriteLine($"Thread({threadId}) running...");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Background thread get a exception.");
            }
        }
        #endregion

        #region RequestThread
        public ManagedThreadPool RequestThread()
        {
            //1
            Task.Run(() => Console.WriteLine());

            //2
            QueueUserWorkItem(_ => { });

            //3
            WaitHandle waitHandle = null;
            RegisterWaitForSingleObject(waitHandle, null, null, 0, true);

            return this;
        }
        #endregion


        #region APM(Simple ThreadPool)
        /// <summary>
        /// APM不可再.NET Core中使用, 再.NET Framework中使用正常。
        /// </summary>
        private delegate string RunOnThreadPool(out int threadId);
        public ManagedThreadPool APM()
        {
            RunOnThreadPool del = AsynchronousTask;
            //此处out参数TaskThreadId无意义。
            var ar = del.BeginInvoke(out int taskThreadId, AsynchronousTaskCallback, "Message to callback");
            //Console.WriteLine($"Thread is: {taskThreadId}");
            ar.AsyncWaitHandle.WaitOne();
            var result = del.EndInvoke(out taskThreadId, ar);
            Console.WriteLine($"Result from asynchronous task, out param: {result}, thread id: {taskThreadId}");

            Thread.Sleep(5000);
            Console.WriteLine("End.");
            return this;
        }

        private string AsynchronousTask(out int threadId)
        {
            Console.WriteLine("Task started...");
            Thread.Sleep(2000);
            threadId = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine("Task end...");

            return "Message from asynchronous task.";
        }

        private void AsynchronousTaskCallback(IAsyncResult ar)
        {
            Console.WriteLine("Task callback start.");
            Thread.Sleep(2000);
            Console.WriteLine($"State: {ar.AsyncState}");
            Console.WriteLine($"Is thread pool: {Thread.CurrentThread.IsThreadPoolThread}");

            Console.WriteLine("Task callback end.");
        }
        #endregion
    }
}
