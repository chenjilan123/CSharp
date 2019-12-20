using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Utility
{
    public class Class007
    {
        //public void Print(int i = 50, string s = "Hello World!")
        public void Print(int i = 5, string s = "Hello")
        {
            Console.WriteLine($"i: {i}, s: {s}");
        }

        //public void Print(int i = 0, string s = null)
        //{
        //    Console.WriteLine($"i: {i}, s: {s}");
        //}
    }
}
