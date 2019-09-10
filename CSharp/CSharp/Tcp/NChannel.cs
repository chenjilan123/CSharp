using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Tcp
{
    /// <summary>
    /// 数据通道
    /// </summary>
    public class NChannel
    {
        public NHandler Handler { get; }

        public NChannel(NHandler handler)
        {
            this.Handler = handler;
        }

        internal void Push(ReadOnlyMemory<byte> buffer)
        {
            Console.WriteLine($"Receive data. Length: {buffer.Length}bytes");
        }
    }
}
