using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CSharp.Model
{
    public class StringDemo
    {
        public void Run()
        {
            //var s = "Hello!";
            //var y = s.ToUpperInvariant();
            //var z = s.ToUpper();
            //Console.WriteLine(y == z); //true

            Console.WriteLine(CultureInfo.CurrentCulture); //zh-CN
            for (int i = 0x41; i <= 0x5A; i++)
            {
                var lower = (char)i;
                var higher = (char)(i + 0x20);
                Console.Write($"{i}: {lower}-{higher}");
                Console.WriteLine($", Upper: {lower.ToString().ToUpper()}, UpperInvariant: {lower.ToString().ToUpperInvariant()}");
            }
        }
    }
}
