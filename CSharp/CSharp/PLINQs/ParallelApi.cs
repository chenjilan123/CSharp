using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.PLINQs
{
    class ParallelApi
    {
        public ParallelApi Invoke()
        {
            //Parallel.Invoke
            //Invoke会阻塞其他线程, 知道所有线程完成
            Parallel.Invoke(
                () => ProcessString("Hello"),
                () => ProcessString("Good morning"),
                () => ProcessString("Nice to meet you")
            );
            return this;
        }
        public ParallelApi ForEach()
        { 
            //Parallel.ForEach
            //处理任何IEnumerable<T>集合
            var cts = new CancellationTokenSource();
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

                });

            //
            Console.WriteLine(result.IsCompleted);
            Console.WriteLine(result.LowestBreakIteration);
            return this;
        }

        private void ProcessString(string input)
        {
            Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(250, 350));
            Console.WriteLine($"{input} has been processed");
        }
    }
}
