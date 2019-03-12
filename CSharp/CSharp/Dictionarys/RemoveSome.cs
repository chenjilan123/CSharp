using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace CSharp.Dictionarys
{
    public class RemoveSome
    {
        public RemoveSome Remove()
        {
            var dic = new Dictionary<string, string>()
            {
                { "6", "m" },
                { "7", "n" },
                { "8", "b" },
                { "9", "v" },
                { "10", "c" },
                { "11", "x" },
                { "3", "d" },
                { "4", "s" },
                { "1", "g" },
                { "2", "f" },
                { "5", "a" },
            };

            var keys = dic.Keys.ToArray();
            int remove = (int)(((double)keys.Length) * 0.5);
            foreach (var key in keys)
            {
                if (remove-- <= 0)
                {
                    break;
                }
                dic.Remove(key);
            }

            foreach (var item in dic)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }
            return this.Cache();
        }

        public RemoveSome Cache()
        {
            var dic = new Dictionary<string, string>();

            int maxNumber;
            while(true)
            {
                Console.WriteLine("Please input max number: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out maxNumber))
                {
                    break;
                }
            }
            for (int i = 0; i < maxNumber; i++)
            {
                dic.Add(i.ToString(), "啊啊啊啊啊啊啊啊啊啊啊啊啊啊");
            }

            while(true)
            {
                Console.WriteLine($"dic count: {dic.Count}");
                Thread.Sleep(5000);
            }
            return this;
        }
    }
}
