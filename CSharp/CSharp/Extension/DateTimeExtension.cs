using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class DateTimeExtension
    {
        public static string ToODBC(this DateTime t)
        {
            return t.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToODBCWithMillisecond(this DateTime t)
        {
            return t.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
