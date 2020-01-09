using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class TaskTimer : IDisposable
    {
        private DateTime _begin;
        private string _name;

        public TaskTimer(string name)
        {
            this._begin = DateTime.Now;
            this._name = name;
        }

        public void Dispose()
        {
            var elapsed = DateTime.Now - _begin;
            Console.WriteLine($"Task {_name}: {elapsed.TotalSeconds.ToString("0.000")}s");
        }
    }
}
