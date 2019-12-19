using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    public static class IEnumerableExtension
    {
        public static void Show<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Console.WriteLine(item);
        }
    }
}
