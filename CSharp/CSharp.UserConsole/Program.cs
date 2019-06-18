﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace CSharp.UserConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(-2 % 3);

            IList<int> lst = new int[] { 1 };
            lst.Add(5);
            Console.WriteLine(lst.Count);
            return;

            // null == null => True
            int? v = null;
            int? y = null;
            Console.WriteLine(v == y);
            v = 1;
            Console.WriteLine(v == y);
            return;

            var curMethod = MethodInfo.GetCurrentMethod();
            var type = typeof(string[]);
            //True
            Console.WriteLine(type.IsEquivalentTo(curMethod.GetParameters()[0].ParameterType));
            ReturnInt();
        }

        static int ReturnInt()
        {
            var curMethod = MethodInfo.GetCurrentMethod();
            Console.WriteLine($"Return Type: {(curMethod as System.Reflection.MethodInfo).ReturnType}");
            return 0;
        }
    }
}
