using CSharp.AsyncProcess;
using CSharp.Performance;
using System;
using System.Collections.Concurrent;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Crawling();
        }


        #region ComparePerformance
        private static void ComparePerformance()
        {
            new CompareInSingleThread()
                .Compare();
        }
        #endregion

        #region ProcessAsyncByQueue
        private static void ProcessAsyncByQueue()
        {
            new ProcessAsyncByQueue()
                .Run();
        }
        #endregion

        #region ProcessAsyncByStack
        private static void ProcessAsyncByStack()
        {
            new ProcessAsyncByStack()
                .Run();
        }
        #endregion

        #region Crawling
        private static void Crawling()
        {
            new Crawling()
                .Run();
        }
        #endregion

        #region BlockingCollection
        private static void BlockingCollection()
        {
            //IProducerConsumerCollection i;
            BlockingCollection<int> coll = new BlockingCollection<int>();
            var enumerable = coll.GetConsumingEnumerable();
        }
        #endregion
    }
}
