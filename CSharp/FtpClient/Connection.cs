using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace FtpClient
{
    public class Connection
    {
        private readonly TcpClient _client;
        private Stream _dataStream;
        public event Action<byte[], int> OnReceiveData;
        public bool IsClosed { get; set; }
        public Connection()
        {
            _client = new TcpClient(AddressFamily.InterNetwork);
        }

        #region 启动
        public async Task StartAsync()
        {
            await _client.ConnectAsync("127.0.0.1", 21).ConfigureAwait(false);
            Console.WriteLine("启动成功");
            try
            {
                Task tReceive;
                _dataStream = _client.GetStream();
                tReceive = ReceiveAsync(_dataStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexcepted exceptions happended: {ex.ToString()}");
            }
        }
        #endregion

        #region 接收
        private async Task ReceiveAsync(Stream stream)
        {
            var buffer = new byte[1024];
            Console.WriteLine("开始接受数据");
            while (true)
            {
                var count = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (count == 0)
                {
                    IsClosed = true;
                    break;
                }
                OnReceiveData(buffer, count);
            }
        }
        #endregion

        #region 发送
        public async Task SendAsync(byte[] buffer, int offset, int count)
        {
            Console.WriteLine($"开始发送数据: {count}bytes");
            await _dataStream.WriteAsync(buffer, offset, count);
        }
        #endregion

        #region 关闭
        public void Close()
        {
            _dataStream.Dispose();
            _dataStream = null;

            _client.Close();

            Console.WriteLine("已关闭连接");
        }
        #endregion
    }
}
