using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.UnityIoC
{
    public class Par
    {
        Son _son;
        public Par(Son son)
        {
            _son = son;
        }

        public bool HasSon()
        {
            return _son != null;
        }
    }
}
