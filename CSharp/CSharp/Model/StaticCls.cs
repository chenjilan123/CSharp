using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    public class StaticCls
    {
        public static int i2 = i1 + 4;
        public static int i1 = 5;

        public static int i3 = i4 + 5;
        public static int i4 = i1 + i2;

        public static void PrintI2()
        {
            //i2 = 4
            Console.WriteLine($"i2: {i2}");
            //i3 = 5
            Console.WriteLine($"i3: {i3}");
        }


        //public static int i1 = 5;
        //public static int i2 = i1 + 4;
        //public static void PrintI2()
        //{
        //    //i2 = 9
        //    Console.WriteLine($"i2: {i2}");
        //}
    }
}
