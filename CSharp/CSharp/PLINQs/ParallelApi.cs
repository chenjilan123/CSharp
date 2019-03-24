using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.PLINQs
{
    class ParallelApi
    {
        #region Invoke
        public ParallelApi Invoke()
        {
            return this.ForEach();
            //Parallel.Invoke
            //Invoke会阻塞其他线程, 知道所有线程完成
            Parallel.Invoke(
                () => ProcessString("Hello"),
                () => ProcessString("Good morning"),
                () => ProcessString("Nice to meet you")
            );

            Console.WriteLine("All done");
            return this;
        }
        #endregion

        #region ForEach
        public ParallelApi ForEach()
        { 
            //Parallel.ForEach
            //处理任何IEnumerable<T>集合
            var cts = new CancellationTokenSource();
            var sw = Stopwatch.StartNew();
            var result = Parallel.ForEach(
                Enumerable.Range(1, 30),
                new ParallelOptions
                {
                    CancellationToken = cts.Token,
                    MaxDegreeOfParallelism = Environment.ProcessorCount,
                    TaskScheduler = TaskScheduler.Default,
                },
                (i, state) =>
                {
                    Thread.Sleep(500);
                    Console.WriteLine(i);
                    if (i == 20)
                    {
                        //state.Break();
                        state.Stop();
                        Console.WriteLine($"Loop is stopped: {state.IsStopped}");
                    }
                });
            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.Elapsed.TotalSeconds}s");
            //
            Console.WriteLine($"IsCompleted: {result.IsCompleted}");
            Console.WriteLine($"Lowest break iteration: {result.LowestBreakIteration}");
            return this;
        }
        #endregion


        private void ProcessString(string input)
        {
            Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(250, 350));
            Console.WriteLine($"{input} has been processed");
        }
    }
}
