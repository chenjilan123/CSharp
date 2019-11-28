using System;
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
        public TcpClient Client { get; }

        private PipeReader ReceiveReader { get; set; }
        private PipeWriter ReceiveWriter { get; set; }
        /// <summary>
        /// 发送管道
        /// </summary>
        private PipeReader SendReader { get; }
        /// <summary>
        /// 是否完成
        /// </summary>
        private bool IsComplete = false;

        public NChannel Channel { get; }

        public NClient(TcpClient client)
        {
            this.Client = client;
            var pipeSend = new Pipe();
            var handler = new NHandler(pipeSend.Writer, this);
            var channel = new NChannel(handler, this);
            this.Channel = channel;

            var pipe = new Pipe();
            this.ReceiveReader = pipe.Reader;
            this.ReceiveWriter = pipe.Writer;

            this.SendReader = pipeSend.Reader;
        }

        public Task StartAsync()
        {
            var t1 = PrepareHandleAsync();
            var t2 = ReceiveAsync();
            var t3 = SendAsync();
            return Task.CompletedTask;
        }

        private async Task SendAsync()
        {
            var stream = Client.GetStream();
            try
            {
                while (!IsComplete)
                {
                    var result = await SendReader.ReadAsync();
                    var buffer = result.Buffer;
                    var position = buffer.Start;

                    while (buffer.TryGet(ref position, out var memory) && stream.CanWrite)
                    {
                        await stream.WriteAsync(memory);
                    }
                    SendReader.AdvanceTo(buffer.End);
                    if (result.IsCanceled || result.IsCompleted) break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NClient.SendAsync: {ex.Message}");
            }
            SendReader.Complete();
            Console.WriteLine($"{Client.Client.RemoteEndPoint}-Send completed.");
        }

        private async Task PrepareHandleAsync()
        {
            while (true)
            {
                var result = await ReceiveReader.ReadAsync();
                var buffer = result.Buffer;
                var position = buffer.Start;
                while (buffer.TryGet(ref position, out var memory))
                {
                    this.Channel.Push(memory.Span);
                }
                ReceiveReader.AdvanceTo(buffer.End);
                if (result.IsCanceled || result.IsCompleted) break;
            }
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}-{Client.Client.RemoteEndPoint}-Handler completed.");
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

                    var memory = ReceiveWriter.GetMemory(count);
                    buffer.AsSpan(0, count).CopyTo(memory.Span);
                    ReceiveWriter.Advance(count);
                    var result = await ReceiveWriter.FlushAsync();
                    if (result.IsCanceled || result.IsCompleted)
                    {
                        break;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Client closed. " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            this.IsComplete = true;
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}-{Client.Client.RemoteEndPoint}-Receive completed.");
            ReceiveWriter.Complete();
        }
    }
}
