using CSharp.Client;
using CSharp.Client.HEZD;
using CSharp.Handler;
using CSharp.Host;
using CSharp.Model;
using CSharp.Ping;
using CSharp.Server;
using CSharp.Tcp;
using CSharp.Udp;
using CSharp.Udp.Broadcast;
using CSharp.Utility;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp
{
    class Program
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket?view=netframework-4.7.2
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            HEZDSender();
        }

        #region HEZDSender
        static void HEZDSender()
        {
            var alarmLst = new List<AlarmData>()
            {
                new AlarmData()
                {
                    AlarmName = "超速报警",
                    Duration = 50,
                    BeginAlarmTime = DateTime.Parse("2019-08-19 12:00:00"),
                    EndAlarmTime = DateTime.Parse("2019-08-19 12:01:00"),
                    BeginLongitude = 127.015712D,
                    BeginLatitude = 27.514214,
                    EndLongitude = 127.016712D,
                    EndLatitude = 27.513214,
                    AlarmFlag = 4,
                    PlateNum = "闽A12345",
                    SimNum = "18012345678",
                }
            };
            var jsonSerializer = new NewtonSerializer();
            var gisService = new CgoService();
            IHEZDSender sender = new HEZDHttp(jsonSerializer, gisService);
            sender.PostAlarm(alarmLst);
        }
        #endregion

        #region AliSms
        static void AliSms()
        {
            //Console.WriteLine("{0}-{1}--{3}", 0, 1, 2, 3);
            //return;

            //Console.WriteLine("{{ {0}  }}", 1);

            //new Sms.AliSmsClient().Send();
        }
        #endregion

        #region WebApi
        static void TestWebApiService()
        {
            const string url = "https://localhost:44340/api/Book";
            var client = new WebApiService();
            var result = client.TakeServicesMethod(url, "1", 3000);
            if (string.IsNullOrEmpty(result))
            {
                Console.WriteLine("Http调用失败");
            }
            else
            {
                Console.WriteLine("Http调用成功");
                Console.WriteLine("应答：");
                Console.WriteLine(result);
            }
        }

        static void WebApi()
        {
            const string url = "https://localhost:44340/api/Book";
            try
            {
                var client = new WebApiClient(url);
                Console.WriteLine(client.Request());
            }
            catch (WebException ex)
            {
                Console.WriteLine("WebException Happended");
                Console.WriteLine($"Status: {ex.Status}");
                Console.WriteLine(ex.ToString());

                WebApi(url, 5000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Happended");
                Console.WriteLine(ex.ToString());
            }
        }

        static void WebApi(string url, int timeOut)
        {
            Console.WriteLine("请求失败，重新发送请求");
            var client = new WebApiClient(url);
            client.TimeOut = timeOut;
            Console.WriteLine(client.Request());
        }
        #endregion

        #region UdpBroadcast
        private static void UdpBroadcast()
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
            var msgHandler = new SocketMsgHandler();
            switch (ope)
            {
                case "server":
                    new UdpBroadcastServer().Start();
                    break;
                case "client":
                    new UdpBroadcastClient().Start();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Udp
        private static void Udp()
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
            var msgHandler = new SocketMsgHandler();
            switch (ope)
            {
                case "server":
                    new UdpServerExtension().Start();
                    break;
                case "client":
                    new UdpClientExtension().Start();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Tcp
        private static void Tcp()
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
            var msgHandler = new SocketMsgHandler();
            switch (ope)
            {
                case "server":
                    new AsyncSocketServer1().Start();
                    break;
                case "client":
                    new TcpClientExtension().Start();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Socket1
        private static void RunSocket()
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
            var msgHandler = new SocketMsgHandler();
            switch (ope)
            {
                case "server":
                    new SocketServer1(msgHandler).Run();
                    break;
                case "client":
                    new SocketClient1(msgHandler).Start();
                    break;
                default:
                    break;
            }
        }

        private static void RunSocket1()
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
            var msgHandler = new SocketMsgHandler();
            switch (ope)
            {
                case "server":
                    new AsyncSocketServer1().Start();
                    break;
                case "client":
                    new AsyncSocketClient1().Start();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Socket
        static private void Socket()
        {
            new SocketServer().Run();
        }
        #endregion

        #region Host
        private static void Host()
        {
            new HostInformation().PrintInformation();
        }
        #endregion

        #region Ping
        private static void Ping()
        {
            new PingDemo().Start();
        }
        #endregion
    }
}
