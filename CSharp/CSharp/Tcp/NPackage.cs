using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Tcp
{
    public class NPackage
    {
        public byte[] body { get; private set; }
        public byte[] validate { get; private set; }

        public NPackage(byte[] body, byte[] validate)
        {
            this.body = body;
            this.validate = validate;
        }
    }
}
