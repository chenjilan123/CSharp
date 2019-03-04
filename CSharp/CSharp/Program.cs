using System;

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
    }
}
