using FtpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FtpServer
{
    class Program
    {
        /// <summary>
        /// Windows10文件系统访问Ftp服务器标准指令
        /// </summary>
        private static List<string> _win10CmdLst = new List<string>
        {
            "USER anonymous",
            "PASS 357592895@gmail.com",
            "opts utf8 on",
            "syst",
            "site help",
            "PWD",
            "noop",
            "CWD /",
            "TYPE A",
            "PASV",
            "LIST",
        };

        static void Main(string[] args)
        {
            StartTcpConnection().Wait();
        }


        static async Task StartTcpConnection()
        {
            IPEndPoint ep;
            while (true)
            {
                Console.WriteLine("请输入目标地址:");
                var address = Console.ReadLine();
                var addressArr = address.Split(',');
                if (addressArr.Length == 6
                    && int.TryParse(addressArr[4], out var port1)
                    && int.TryParse(addressArr[5], out var port2)
                    && IPAddress.TryParse($"{addressArr[0]}.{addressArr[1]}.{addressArr[2]}.{addressArr[3]}", out var ip))
                {
                    ep = new IPEndPoint(ip, port1 * 256 + port2);
                    break;
                }

                addressArr = address.Split(':');
                if (addressArr.Length == 2 
                    && int.TryParse(addressArr[1], out var port)
                    && IPAddress.TryParse(addressArr[0], out ip))
                {
                    ep = new IPEndPoint(ip, port);
                    break;
                }
            }
            var conn = new TcpConnection(ep);
            conn.OnReceiveData += Conn_OnReceiveData;
            Console.WriteLine("指令列表");
            for (int i = 0; i < _win10CmdLst.Count; i++)
            {
                Console.WriteLine($"{i}:{_win10CmdLst[i]}");
            }

            //启动连接
            await conn.StartAsync();
            //发送数据
            while (!conn.IsClosed)
            {
                //Console.WriteLine("请输入Ftp指令: ");
                var cmd = Console.ReadLine();
                if (uint.TryParse(cmd, out var index) && index < _win10CmdLst.Count)
                {
                    cmd = _win10CmdLst[(int)index];
                }
                //必须加换行符表示结束。
                cmd += "\r\n";
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
            //Console.Write($"接收: {count.ToString().PadLeft(5, ' ')}bytes, ");


            var collectedData = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                collectedData.Append(data[i].ToString("X2"));
            }
            Console.WriteLine(collectedData);
            Console.Write($"{UTF8Encoding.UTF8.GetString(data, 0, count)}");
            //Console.WriteLine("请输入Ftp指令: ");
        }
    }
}
