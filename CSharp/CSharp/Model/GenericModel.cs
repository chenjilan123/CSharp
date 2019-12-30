using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Model
{
    public class GenericModel<T>
    {
        public static int Count = 0;

        static GenericModel()
        {
            if (typeof(T).IsArray)
                throw new Exception($"不支持的泛型实参: {typeof(T)}");
        }
    }
}
