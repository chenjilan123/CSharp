using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace System
{
    public static class EventArgsExtensions
    {
        //泛型扩展方法
        public static void Raise<TEventArgs>(this TEventArgs e, object sender, ref EventHandler<TEventArgs> eventDelegate)
            where TEventArgs: EventArgs
        {
            var temp = Volatile.Read(ref eventDelegate);
            temp?.Invoke(sender, e);
        }
    }
}
