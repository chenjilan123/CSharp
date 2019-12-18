using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Extension
{
    public static class DelegateExtension
    {
        public static void InvokeAndCatch<TException>(this Action<Object> d, Object o)
            where TException: Exception
        {
            try
            { d(o); }
            catch (TException) { }
        }
    }
}
