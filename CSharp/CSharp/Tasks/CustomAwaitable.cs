using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    public class CustomAwaitable
    {
        private readonly bool _completeSynchronously;
        public CustomAwaitable(bool completeSynchronously)
        {
            this._completeSynchronously = completeSynchronously;
        }
        public CustomAwaiter GetAwaiter()
        {
            return new CustomAwaiter(_completeSynchronously);
        }
    }

    public class CustomAwaiter : INotifyCompletion
    {
        private string _result;
        private readonly bool _completeSynchronously;

        public bool IsCompleted => _completeSynchronously;

        public CustomAwaiter(bool completeSynchronously)
        {
            this._completeSynchronously = completeSynchronously;
        }

        /// <summary>
        /// Support await keyword
        /// </summary>
        /// <returns></returns>
        public string GetResult()
        {
            return _result;
        }

        public void OnCompleted(Action continuation)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1D));
                _result = "Hello World! ... ";
                continuation?.Invoke();
            });
        }
    }
}
