using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Tcp
{
    /// <summary>
    /// 处理器
    /// </summary>
    public class NHandler
    {
        private PipeWriter Writer { get; }
        private NClient Client { get; }
        private EndPoint RemoteEndPoint { get; }
        public NHandler(PipeWriter writer, NClient client)
        {
            this.Writer = writer;
            this.Client = client;
            this.RemoteEndPoint = client.Client.Client.RemoteEndPoint;
        }

        public void HandlePackage(ReadOnlyCollection<byte> data)
        {
            var sb = new StringBuilder(data.Count);
            foreach (var b in data)
            {
                sb.Append(b.ToString("X2"));
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}-{RemoteEndPoint}-Receive package：{sb.ToString()}");
            Console.ForegroundColor = ConsoleColor.White;


            //var 
            //data
        }

        private Task Send(byte[] data)
        {
            try
            {
                var memory = Writer.GetMemory(data.Length);
                data.AsSpan().CopyTo(memory.Span);
                Writer.Advance(memory.Length);
                var result = Writer.FlushAsync();
                if (result.IsCanceled || result.IsCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Send failed bacause the channel is canceled or completed");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"NHandler.Send: {ex.ToString()}");
                return null;
            }
        }
    }
}
