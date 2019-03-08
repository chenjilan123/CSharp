using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Tasks
{
    public class UpdateAsync
    {
        #region Begin
        //public async Task Begin() //再外部使用Task.Wait(), 可以阻塞。
        //public async void Begin() //若返回空, 则再方法内使用await关键字时, 调用方可执行下面的指令。
        public async Task<string> Begin()
        {
            Console.WriteLine(" Begin worker");
            var s = await Update();
            Console.WriteLine($"     Result: {s}");
            Console.WriteLine(" End worker");
            return s;
        }

        private Task<string> Update()
        {
            return Task.Factory.StartNew<string>(() =>
            {
                Thread.Sleep(2000);
                return "5004485";
            });
        }
        #endregion

        #region ParallelProcess
        public void ParallelProcess()
        {
            var table = new DataTable();
            table.Columns.Add("Position");
            Parallel.ForEach<DataRow>(table.AsEnumerable(), dr =>
            {
                dr["Position"] = "1";
            });

            var source = new List<int>();
            source.Add(1);
            source.Add(2);
            source.Add(3);
            source.Add(4);
            source.Add(5);
            source.Add(6);
            source.Add(7);
            var result = from x in source.AsParallel().WithDegreeOfParallelism(50)
                         select Proc(x);
            Console.WriteLine("Done1.");
            foreach (var i in result)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Done2.");
        }

        private int Proc(int i)
        {
            Thread.Sleep(3000);
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread + " " + Thread.CurrentThread.IsBackground);
            return i + 1;
        }
        #endregion
    }
}
