using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ImpromptuInterface;

namespace CSharp.Tasks
{
    public class DynamicAwait
    {
        public void Start()
        {
            Task t = AsynchronousProcessing();
            t.Wait();
        }

        static async Task AsynchronousProcessing()
        {
            string result = await GetDynamicAwaitableObject(true);
            Console.WriteLine(result);

            result = await GetDynamicAwaitableObject(false);
            Console.WriteLine(result);
        }

        static dynamic GetDynamicAwaitableObject(bool completeSynchronously)
        {
            dynamic result = new ExpandoObject();
            dynamic awaiter = new ExpandoObject();

            awaiter.Message = "Completed synchronously";
            awaiter.IsCompleted = completeSynchronously;
            awaiter.GetResult = (Func<string>)(() => awaiter.Message);

            awaiter.OnCompleted = (Action<Action>)(callback =>
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1D));
                    awaiter.Message = GetInfo();
                    callback?.Invoke();
                })
            );
            IAwaiter<string> proxy = Impromptu.ActLike(awaiter);

            result.GetAwaiter = (Func<dynamic>)(() => proxy);

            return result;
        }
        static string GetInfo()
        {
            return "Hello World!";
        }
    }
    
    public interface IAwaiter<T> : INotifyCompletion
    {
        bool IsCompleted { get; }
        T GetResult();
    }
}
