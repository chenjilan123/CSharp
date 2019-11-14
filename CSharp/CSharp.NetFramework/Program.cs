using CSharp.Netframework.Sms.OneNet;
using CSharp.NetFramework.Gis;
using CSharp.NetFramework.Tcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.NetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            OneNetSms();
        }

        #region OneNetSms
        static void OneNetSms()
        {
            while (true)
            {
                Console.Write("请输入sicode: ");
                var sicode = Console.ReadLine();

                //const string sicode = "F0CC35DC-0D8E-4FC9-9249-6C2A6A7690D9";
                try
                {
                    var appendParam = "captcha=500101&expireTime=5&signId=101044";
                    Console.WriteLine("发送短信中...");
                    var result = new OneNetClient(sicode).SendSms("15980217471", "11423", appendParam);
                    Console.WriteLine($"结果: {result.Result}, 描述: {result.ResultInfo}, 错误信息: {result.ErrorInfo}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        #endregion

        #region StartTcpChannel
        private static void StartTcpChannel()
        {
            string ope = null;
            while (true)
            {
                Console.WriteLine("Run server or client ?");
                ope = Console.ReadLine();
                if (ope == "server" || ope == "client")
                {
                    break;
                }
                Console.WriteLine("Input error, please try again.");
            }
            switch (ope)
            {
                case "server":
                    new TcpChannelComm().StartServer();
                    break;
                case "client":
                    new TcpChannelComm().StartClient();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region StartMonitor
        private static void StartMonitor()
        {
            new TcpMonitorServer().Start();
        }
        #endregion

        #region GisService
        static void GisService()
        {
            new WebGis().GetLocation(117.50, 27.50);
        }
        #endregion
    }
}
