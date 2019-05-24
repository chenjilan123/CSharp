using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Framework.Email
{
    public class TimerLog
    {
        public void Info(string msg)
        {
            Console.WriteLine(msg);
        }
        public void Error(string msg)
        {
            Console.WriteLine(msg);
        }
        public void Error(string msg, Exception ex)
        {
            this.Error(msg + ex.ToString());
        }

    }
}
