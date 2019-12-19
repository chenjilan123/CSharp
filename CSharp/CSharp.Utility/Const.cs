using System;

namespace CSharp.Utility
{
    public class Const
    {
        //public const string HelloWorld = "Hello World!";
        //public const int HelloWorld = 100;
        public static readonly int HelloWorld = 100;

        public static void Method(int i = 500)
            //Missing Method Exception
        //public static void Method(int i = 500, string s = "hello")
        {
            Console.WriteLine(i);
            Console.WriteLine("Hello");
        }
    }
}
