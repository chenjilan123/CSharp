using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class OperationTimer : IDisposable
    {
        private readonly string _name;
        private readonly DateTime _beginTime;
        private readonly int _gcCollection;
        public OperationTimer(string name)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            this._gcCollection = GC.CollectionCount(0);
            this._name = name;
            this._beginTime = DateTime.Now;
        }

        public void Dispose()
        {
            var usedTime = DateTime.Now - _beginTime;
            Console.WriteLine($"{_name}花费时间: {usedTime.TotalSeconds}秒, GCs={GC.CollectionCount(0) - _gcCollection}。");
        }
    }
}
