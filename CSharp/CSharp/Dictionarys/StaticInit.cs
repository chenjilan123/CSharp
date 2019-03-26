using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Dictionarys
{
    public class StaticInit
    {
        private static Dictionary<string, string> _dic = new Dictionary<string, string>() { { "Heheda", "Hello"} };
        public static Dictionary<string, string> Dic
        {
            get
            {
                if (_dic.Count <= 1)
                {
                    _dic.Add("Hee", "Hello World!");
                }
                return _dic;
            }
        }

        public static void Print()
        {
            Console.WriteLine(_dic.Count);
        }
    }
}
