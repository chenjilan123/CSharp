using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Pb
{
    public class ProtocolBufferProgram
    {
        public void Run()
        {
            var value = Google.ProtocolBuffers.CodedInputStream.DecodeZigZag32(1);
            Console.WriteLine(value);
        }
    }
}
