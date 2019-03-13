using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Tasks
{
    public class TaskAsyncKeyword
    {
        public TaskAsyncKeyword Base()
        {
            Console.WriteLine("Begin.");
            Async();
            Console.WriteLine("End all.");
            return this;
        }

        private async Task Async()
        {
            Console.WriteLine("Begin Async.");

            await Task.Delay(5000);

            Console.WriteLine("End async.");
        }
    }
}
