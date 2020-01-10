using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.NETCore
{
    public class ParallelTask : IProgram
    {
        private const int TaskCount = 100;
        private int _iCount1 = 0;
        private int _iCount2 = 0;

        public void Run()
        {
            while (true)
            {
                //并行
                //RequestParallel1();
                //RequestParallel2();
                RequestParallelAdvance();

                //同步
                //RequestSync();
            }
        }

        #region 并行(优化)
        void RequestParallelAdvance()
        {
            const int waitTaskCount = 12;
            using (new TaskTimer("Parallel Advance Task"))
            {
                var completedTasks = new List<Task<string>>();
                var tasks = new List<Task>();
                for (int i = 0; i < TaskCount; i++)
                {
                    var t = RequestAsync();
                    tasks.Add(t);

                    if (i < waitTaskCount) continue;
                    WaitTaskCompleted(completedTasks, tasks);
                }
                while (tasks.Count > 0)
                {
                    WaitTaskCompleted(completedTasks, tasks);
                }

                Console.WriteLine($"All {completedTasks.Count} tasks completed.");
            }
        }

        private static void WaitTaskCompleted(List<Task<string>> completedTasks, List<Task> tasks)
        {
            var completedTask = Task.WhenAny(tasks).Result;
            tasks.Remove(completedTask);
            completedTasks.Add(completedTask as Task<string>);
        }
        #endregion

        #region 并行
        private void RequestParallel2()
        {
            using (new TaskTimer("Parallel Task"))
            {
                Parallel.For(0, TaskCount, _ => Request());
            }
        }

        private void RequestParallel1()
        {
            using (new TaskTimer("Parallel Task"))
            {
                var lst = new List<Task>();
                for (int i = 0; i < TaskCount; i++)
                {
                    var t = RequestAsync();
                    lst.Add(t);
                }
                Task.WhenAll(lst).Wait();
            }
            Console.WriteLine(_iCount1);
        }
        private async Task<string> RequestAsync()
        {
            var i = Interlocked.Increment(ref _iCount1);
            //using (new TaskTimer($"Request {i}"))
            {
                var client = new WebClient();
                var address = "http://www.baidu.com";
                var uri = new Uri(address);
                var response = await client.DownloadStringTaskAsync(uri);
                return response;
            }
        }
        #endregion

        #region 同步
        private void RequestSync()
        {
            using (new TaskTimer("Sync Task"))
            {
                for (int i = 0; i < TaskCount; i++)
                {
                    Request();
                }
            }
            Console.WriteLine(_iCount2);
        }

        private void Request()
        {
            var i = Interlocked.Increment(ref _iCount2);
            //var i = ++_iCount2;
            //using (new TaskTimer($"Request {i}"))
            {
                var client = new WebClient();
                var address = "http://www.baidu.com";
                var uri = new Uri(address);
                var response = client.DownloadString(uri);
            }
        }
        #endregion
    }
}
