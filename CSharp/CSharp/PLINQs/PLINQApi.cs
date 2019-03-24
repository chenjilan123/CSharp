using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.PLINQs
{
    public class PLINQApi
    {
        #region Exception
        public PLINQApi Exception()
        {
            return this.Aggregate();

            //var nums = Enumerable.Range(-5, 20);
            var nums = new[] { 0, 1, 2, 0, 0};
                var query = from num in nums.AsParallel()
                        select 100 / num;
            try
            {
                query.ForAll(Console.WriteLine);
            }
            catch (AggregateException ex)
            {
                ex.Flatten().Handle(ex1 =>
                {
                    if (ex1 is DivideByZeroException)
                    {
                        Console.WriteLine("Divided by zero");
                        return true;
                    }
                    return false;
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return this;
        }
        #endregion

        #region DataPartition
        public PLINQApi DataPartition()
        {
            //foreach (var type in GetTypes())
            //{
            //    Console.WriteLine(type);
            //}
            Func<string, string> ProcessString = i => $"ThreadId: {Thread.CurrentThread.ManagedThreadId}, Info: H{i}";
            Action<string> PrintString = i => Console.WriteLine(i);
            //创建分区器，用于并行处理《奇数长度与偶数长度的字符串》。
            var partitioner = new StringPartitioner(GetTypes());
            var query = from t in partitioner.AsParallel()
                        select ProcessString(t);
            query.ForAll(PrintString);
            int count = query.Count();

            Console.WriteLine($"Total items processed: {count}");
            return this;
        }
        #endregion

        #region Aggregate
        public PLINQApi Aggregate()
        {
            var query = from t in GetTypes().AsParallel()
                        select t;
            Console.WriteLine("Aggregate begin.");
            var aggregator = query.Aggregate(
                () => new ConcurrentDictionary<char, int>(),
                Accumulate,
                Merge,
                total => total
                );

            Console.WriteLine("Aggregate end.");
            //return this;
            var dec = from s in aggregator.Keys
                      orderby aggregator[s] descending
                      select s;
            foreach (var key in dec)
            {
                Console.WriteLine($"Char: {key}, Count: {aggregator[key]}");
            }
            return this;
        }

        private ConcurrentDictionary<char, int> Accumulate(ConcurrentDictionary<char, int> total, string s)
        {
            foreach (var c in s)
            {
                if (total.ContainsKey(c))
                {
                    total[c] += 1;
                }
                else
                {
                    total[c] = 1;
                }
            }
            Console.WriteLine($"Accumulate Task ,ThreadId: {Thread.CurrentThread.ManagedThreadId}, IsPool: {Thread.CurrentThread.IsThreadPoolThread}, KeyCount: {total.Count}");
            return total;
        }
        private ConcurrentDictionary<char, int> Merge(ConcurrentDictionary<char, int> total, ConcurrentDictionary<char, int> merge)
        {
            foreach (var key in total.Keys)
            {
                if (merge.ContainsKey(key))
                {
                    merge[key] += total[key];
                }
                else
                {
                    merge[key] = total[key];
                }
            }
            Console.WriteLine($"Merge Task ,ThreadId: {Thread.CurrentThread.ManagedThreadId}, IsPool: {Thread.CurrentThread.IsThreadPoolThread}, KeyCount: {merge.Count}");
            return merge;
        }
        #endregion

        #region Common
        private IEnumerable<string> GetTypes()
        {
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetExportedTypes());
            return from type in types
                       //where type.Name.StartsWith("Web")
                   select type.Name;
        }
        #endregion
    }
}
