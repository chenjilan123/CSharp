﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Tcp
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class NClient
    {
        public TcpClient Client { get; set; }
        public PipeReader Reader { get; set; }
        public PipeWriter Writer { get; set; }

        public NChannel Channel { get; }

        public NClient(NChannel channel)
        {
            this.Channel = channel;

            var pipe = new Pipe();
            Reader = pipe.Reader;
            Writer = pipe.Writer;
        }

        public Task StartAsync()
        {
            var t1 = PrepareHandleAsync();
            var t2 = ReceiveAsync();
            return Task.CompletedTask;
        }

        private async Task PrepareHandleAsync()
        {
            while (true)
            {
                var result = await Reader.ReadAsync();
                var buffer = result.Buffer;
                var position = buffer.Start;
                while (buffer.TryGet(ref position, out var memory))
                {
                    this.Channel.Push(memory);
                }
                Reader.AdvanceTo(buffer.End);
                if (result.IsCanceled || result.IsCompleted) break;
            }
            Console.WriteLine("Handler completed.");
        }

        public async Task ReceiveAsync()
        {
            try
            {
                var stream = Client.GetStream();
                var buffer = new byte[1024];
                while (true)
                {
                    var count = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (count == 0) break;

                    var memory = Writer.GetMemory(count);
                    buffer.AsSpan(0, count).CopyTo(memory.Span);
                    Writer.Advance(count);
                    var result = await Writer.FlushAsync();
                    if (result.IsCanceled || result.IsCompleted)
                    {
                        break;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Client closed. " + ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Receive completed.");
            Writer.Complete();
        }
    }
}
