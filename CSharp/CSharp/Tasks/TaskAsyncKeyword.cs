using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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

            await Task.Delay(500);

            Console.WriteLine("End async.");
        }

        #region Await
        public void Await()
        {
            Test();
            Console.WriteLine("End all.");
            return;

            Console.WriteLine("1_1 Begin Await Method.");
            var t = ProcessAsync();
            Console.WriteLine("5_4 End Await Method.");
            t.Wait();
            Console.WriteLine("8_8 All end..");
        }

        private async Task ProcessAsync()
        {
            Func<string, Task<DateTime>> funTask = async name =>
            {
                var t = DateTime.Now;
                Console.WriteLine("3_3 Begin lambda task async...");

                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(50);
                    //Task.Delay(50);
                    Console.WriteLine("Doing something computing...");
                }
                //await会立即解放外部的操作。没await会阻塞。
                //await Task.Delay(500);
                //Task.Delay(500);
                Thread.Sleep(500);

                Console.WriteLine("4_5     lambda task async begin await...");
                await Task.Delay(500);
                Console.WriteLine("6_6 End lambda task async...");
                return t;
            };
            Console.WriteLine("2_2 Begin process async.");
            Task.Delay(3000);
            await funTask(""); //并未立即退出, 而是等内部再次调用await时才立即返回。(记住只有使用await后才会立即返回)
            Console.WriteLine("7_7 End process async.");
        }
        #endregion

        #region Test
        private async void Test()
        {
            await Task1();
            Console.WriteLine("Test done.");
        }

        private Task Task1()
        {
            //加了这句End all会比Task1 end晚。理解Task.Delay。
            //Console.WriteLine("Begin Task1");
            //Task.Delay(50);
            return Task.Run(() =>
            {
                Console.WriteLine("Task1 begin.");
                Task.Delay(500);
                Console.WriteLine("Task1 end.");
            });
        }
        #endregion
    }
}
