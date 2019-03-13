using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Core
{
    public class MultiTaskFramework
    {
        public void Main()
        {
            var tasks = new List<Task<int>>();
            for (int i = 0; i < 4; i++)
            {
                var j = i;
                var task = new Task<int>(() => Worker(j));
                tasks.Add(task);
                task.Start();
            }

            while(tasks.Count > 0)
            {
                //Get Result
                var tComplated = Task.WhenAny(tasks).Result;
                tasks.Remove(tComplated);
                Console.WriteLine($"Result from task: {tComplated.Result}");
            }

            Console.WriteLine("End all.");
        }

        private int Worker(int i)
        {
            //Work
            Thread.Sleep(i * 1000);
            return i * 5 + 1;
        }
    }
}
