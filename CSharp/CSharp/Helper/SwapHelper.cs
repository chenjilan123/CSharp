using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Helper
{
    public class SwapHelper
    {
        /// <summary>
        /// 交换引用, 对值类型、引用类型都适用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        public static void Swap<T>(ref T obj1, ref T obj2)
        {
            T swap = obj1;
            obj1 = obj2;
            obj2 = swap;
        }

        public static void Swap(ref int i1, ref int i2)
        {
            var i = i1;
            i1 = i2;
            i2 = i;
        }

        public static void Box(ref int i)
        {
            Console.WriteLine(i);
        }

        public static void Box(ref object obj) { }

        /// <summary>
        /// 交换引用类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static void SwapRef<T>(ref T v1, ref T v2)
            where T : class
        {
            v2 = System.Threading.Interlocked.Exchange<T>(ref v1, v2);
        }
    }
}
