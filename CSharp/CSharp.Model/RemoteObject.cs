using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Model
{
    public class RemoteObject : MarshalByRefObject
    {
        private int callCount = 0;
        public int GetCount()
        {
            callCount++;
            return callCount;
        }
    }
}
