using System;
using System.Collections.Generic;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {

            var jetArr = new JetArray()
            {
                new Jet() { Code = "NF5" },
                new Jet() { Code = "NH583"},
                new Jet() { Code = "FL440"},
                new Jet() { Code = "FL441"},
                new JetArray()
                {
                    new Jet() { Code = "FY4" },
                },
            };
            foreach (var jet in jetArr)
            {
                Console.WriteLine(jet.Code);
            }
        }

        #region EnumOrder
        private static void EnumOrder()
        {
            var collection = new List<int>();
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);
            collection.Add(4);
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
            return;
        }
        #endregion
    }
}
