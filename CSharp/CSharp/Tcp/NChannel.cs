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
        private readonly List<byte> _buffer = new List<byte>();

        private bool _canStart = false;

        public NHandler Handler { get; }

        public NChannel(NHandler handler)
        {
            this.Handler = handler;
        }

        /// <summary>
        /// 推送
        /// </summary>
        /// <param name="buffer"></param>
        internal void Push(ReadOnlySpan<byte> span)
        {
            //Console.WriteLine($"Receive data. Length: {span.Length}bytes");
            var sb = new StringBuilder(span.Length);
            foreach (var b in span)
            {
                sb.Append(b.ToString("X2"));
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Receive data. Content：{sb.ToString()}");
            Console.ForegroundColor = ConsoleColor.White;
            Package(span);
        }

        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="buffer"></param>
        private void Package(ReadOnlySpan<byte> span)
        {
            var enumerator = span.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var b = enumerator.Current;
                if (b == NConst.StartFlag)
                {
                    _canStart = true;
                    //Console.WriteLine("Get package start flag");
                    if (this._buffer.Count > 0) this._buffer.Clear();
                    continue;
                }
                if (!_canStart)
                {
                    //Console.WriteLine("Get invalid data.");
                    continue;
                }
                if (b == NConst.EndFlag)
                {
                    _canStart = false;
                    //Console.WriteLine("Get package end flag");
                    this.Handler.HandlePackage(_buffer.AsReadOnly());
                    this._buffer.Clear();
                    continue;
                }
                this._buffer.Add(b);
            }
        }
    }
}
