using CSharp.AliCloudOss;
using CSharp.Client;
using CSharp.Client.HEZD;
using CSharp.Handler;
using CSharp.Host;
using CSharp.Model;
using CSharp.Ping;
using CSharp.Server;
using CSharp.Sms.OneNet;
using CSharp.Tcp;
using CSharp.Udp;
using CSharp.Udp.Broadcast;
using CSharp.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace CSharp
{
    class Program
    {
        private const int PadLength = 30;
        static void Main(string[] args)
        {
            try
            {
                TcpFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #region TcpFile
        private static void TcpFile()
        {
            new TcpFile().RunDemo();
        }
        #endregion

        #region OneNetSms
        static void OneNetSms()
        {
            const string sicode = "F0CC35DC-0D8E-4FC9-9249-6C2A6A7690D9";
            var customParams = new Dictionary<string, string>
            {
                { "code", "505641" },
                { "time", "5" },
            };
            try
            {
                var result = new OneNetClient(sicode).SendSms("15980217471", "111423", customParams);
                Console.WriteLine($"Result: {result.Result}, ResultInfo: {result.ResultInfo}, ErrorInfo: {result.ErrorInfo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region Nullable
        protected internal void Nullable()
        {
            //int? value = new Nullable<int>();
            int? value = default(int?);
            Console.WriteLine(value.ToString());
        }
        #endregion

        #region Oss
        static void Oss()
        {
            new AliYunOss().GetBucket();
        }
        #endregion

        #region NServer
        static void NServer()
        {
            new NServer().StartAsync().Wait();
        }
        #endregion

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

            new Sms.AliSmsClient().Send();
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

        #region DifTypeCompare
        private static void DifTypeCompare()
        {
            decimal m
                = 412423.51121524M
                //= 43544587.15343534543512454m; // False
                //= 5.403231M;  //True
                ;
            double d = (double)m;

            Console.WriteLine(m);
            Console.WriteLine(d);
            Console.WriteLine(m == (decimal)d);
        }
        #endregion

        #region PadZero
        private static void PadZero()
        {
            Console.WriteLine($"{10000:D4}");
            Console.WriteLine($"{15:D4}");
        }
        #endregion

        #region Helper
        private class Helper
        {
            public void Run()
            {
                DecimalCompute();
            }

            #region DecimalCompute
            void DecimalCompute()
            {
                var t1 = new DateTime(1990, 1, 1, 1, 1, 10);
                var t2 = new DateTime(1990, 1, 1, 1, 1, 30);
                var mut = (t2 - t1).TotalMinutes;
                //decimal x = 5m / 20m;
                decimal x = 5m / (decimal)mut;
                Console.WriteLine(x);
                Console.WriteLine(x.ToString("0.0"));
            }
            #endregion
        }
        #endregion

        #region ComputeMD5
        void ComputeMD5()
        {
            var arrFileList = new[] { "F:\\tools\\ODTwithODAC122010.zip", "F:\\tools\\NDP471-DevPack-ENU.exe", "F:\\tools\\TeamViewer_12.1.10277.0.exe" };

            foreach (var sFullName in arrFileList)
            {
                var fileInfo = new FileInfo(sFullName);
                if (!fileInfo.Exists)
                {
                    Console.WriteLine($"File [{fileInfo.FullName}] doesn't exists.");
                }
                var sw = Stopwatch.StartNew();
                var s1 = GetMD5HashFromFile(fileInfo.FullName).ToUpper();
                var s2 = FileToMD5Hash(fileInfo.FullName);


                //var retVal = BinaryHelper.GetASCIIString(s1);
                //StringBuilder sb = new StringBuilder();

                //for (int i = 0; i < retVal.Length; i++)
                //{
                //    sb.Append(retVal[i].ToString("x2"));
                //}
                //Console.WriteLine(sb.ToString());

                sw.Stop();
                Console.WriteLine(fileInfo);
                Console.WriteLine($"  FileName: {fileInfo.FullName}");
                Console.WriteLine($"FileLength: {fileInfo.Length} bytes");
                Console.WriteLine($" Time: {sw.Elapsed.TotalSeconds.ToString("0.000")}");
                Console.WriteLine($"Value1: {s1}");
                Console.WriteLine($"Value2: {s2}");

                //ToBCD
                var sbMD5_1 = new StringBuilder();
                foreach (var b in Encoding.UTF8.GetBytes(s1))
                {
                    sbMD5_1.Append(b.ToString("X2"));
                }
                Console.WriteLine($"MD5 64_1: {sbMD5_1.ToString()}");
                var sbMD5_2 = new StringBuilder();
                //foreach (var b in BinaryHelper.GetASCIIString(s2))
                //{
                //    sbMD5_2.Append(b.ToString("X2"));
                //}
                Console.WriteLine($"MD5 64_2: {sbMD5_2.ToString()}");
            }
        }
        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns>MD5值</returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                //return BinaryHelper.ToASCIIString(retVal, 0, retVal.Length);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
        public static string FileToMD5Hash(string sFileName)
        {
            var bFile = File.ReadAllBytes(sFileName);
            byte[] result = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5"))
        .ComputeHash(bFile);
            StringBuilder output = new StringBuilder(16);
            for (int i = 0; i < result.Length; i++)
            {
                output.Append((result[i]).ToString("X2",
                System.Globalization.CultureInfo.InvariantCulture));
            }
            return output.ToString();
        }
        public static string StringToMD5Hash(string inputString)
        {
            byte[] result = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5"))
            .ComputeHash(Encoding.UTF8.GetBytes(inputString));
            StringBuilder output = new StringBuilder(16);
            for (int i = 0; i < result.Length; i++)
            {
                output.Append((result[i]).ToString("X2",
                System.Globalization.CultureInfo.InvariantCulture));
            }
            return output.ToString();
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
