using FtpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FtpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            StartTcpConnection().Wait();
        }


        static async Task StartTcpConnection()
        {
            var conn = new Connection();
            conn.OnReceiveData += Conn_OnReceiveData;

            //启动连接
            await conn.StartAsync();
            //发送数据
            while (!conn.IsClosed)
            {
                Console.WriteLine("请输入Ftp指令: ");
                var cmd = Console.ReadLine();
                var data = Encoding.UTF8.GetBytes(cmd);//new byte[] { 1, 2, 3 };
                await conn.SendAsync(data, 0, data.Length);
            }
            //等待数据
            Thread.Sleep(300);
            //关闭连接
            conn.Close();
        }

        private static void Conn_OnReceiveData(byte[] data, int count)
        {
            Console.Write($"接收: {count.ToString().PadLeft(5, ' ')}bytes, ");
            Console.WriteLine($"内容: {ASCIIEncoding.ASCII.GetString(data, 0, count)}");
            Console.WriteLine("请输入Ftp指令: ");
        }
    }
}
