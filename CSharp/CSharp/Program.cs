using CSharp.AliCloudOss;
using CSharp.Client;
using CSharp.Client.HEZD;
using CSharp.Dns_;
using CSharp.Handler;
using CSharp.Host;
using CSharp.Http;
using CSharp.Model;
using CSharp.Ping;
using CSharp.Server;
using CSharp.Sms.OneNet;
using CSharp.Tcp;
using CSharp.Tcp.NTcpClient;
using CSharp.Udp;
using CSharp.Udp.Broadcast;
using CSharp.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCPProxy;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Base64String();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }

        #region Base64

        public static void Base64String()
        {
            // Header

            // Backward
            {
                Console.WriteLine("Header:");
                var base64 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
                var header = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
                Console.WriteLine(header);
            }

            // Forward
            {
                Console.WriteLine("Signature:");
                var header = "{\"alg\":\"HS256\",\"typ\":\"JWT\"}";
                var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(header));
                Console.WriteLine(base64);
            }

            // Payload
            Console.WriteLine();
            Console.WriteLine("Header:");
            PrintRaw("eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ==");
            PrintBase64("{\"sub\":\"1234567890\",\"name\":\"John Doe\",\"iat\":1516239022}");

            // Signature
            //PrintRaw("cThIIoDvwdueQB468K5xDc5633seEFoqwxjF_xSJyQQ");

            void PrintRaw(string base64)
            {
                var bytes = Convert.FromBase64String(base64);
                var msg = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(msg);
            }

            void PrintBase64(string msg)
            {
                Console.WriteLine("Signature:");
                var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(msg));
                Console.WriteLine(base64);
            }
        }

        #endregion

        #region TimeSpanFormat

        private static void TimeSpanFormat()
        {
            //System.ComponentModel.Int64Converter.StandardValuesCollection a;
            var ts = new TimeSpan(10, 0, 0);
            Console.WriteLine(ts.ToString("dd\\.hh\\:mm\\:ss"));
            Console.WriteLine($"{ts:hh\\:mm}");
        }

        #endregion

        #region Dns_
        static void Dns_()
        {
            new DnsResolver().Resolve();
        }
        #endregion

        #region ResolveUrl
        static void ResolveUrl()
        {
            //var sUrl = "www.baidu.com:443";
            //var sUri = "http://www.baidu.com";
            var sUri = "http://www.baidu.com:443";
            var uri = new Uri(sUri);
            var ipHost = Dns.Resolve(uri.Host);
            Console.WriteLine(ipHost.HostName);
        }
        #endregion

        #region WebProxyClient
        static void WebProxyClient()
        {
            new HttpProxyClient().HttpGetByProxy();
        }
        #endregion

        #region ProxyListen
        static void Listen(int port)
        {
            Console.WriteLine("准备监听端口:" + port);
            TcpListener tcplistener = new TcpListener(port);
            try
            {
                tcplistener.Start();
            }
            catch
            {
                Console.WriteLine("该端口已被占用,请更换端口号!!!");
                ReListen(tcplistener);
            }
            Console.WriteLine("确认:y/n (yes or no):");
            string isOK = Console.ReadLine();
            if (isOK == "y")
            {
                Console.WriteLine("成功监听端口:" + port);
                //侦听端口号 
                Socket socket;
                while (true)
                {
                    socket = tcplistener.AcceptSocket();
                    //并获取传送和接收数据的Scoket实例 
                    Proxy proxy = new Proxy(socket);
                    //Proxy类实例化 
                    Thread thread = new Thread(new ThreadStart(proxy.Run));
                    //创建线程 
                    thread.Start();
                    System.Threading.Thread.Sleep(10);
                    //启动线程 
                }
            }
            else
            {
                ReListen(tcplistener);
            }
        }
        static void ReListen(TcpListener listener)
        {
            if (listener != null)
            {
                listener.Stop();
                listener = null;
            }
            Console.WriteLine("请输入监听端口号:");
            string newPort = Console.ReadLine();
            int port;
            if (int.TryParse(newPort, out port))
            {
                Listen(port);
            }
            else
            {
                ReListen(listener);
            }
        }
        #endregion

        #region WebProxy
        static void WebProxy()
        {
            const int port = 8000;
            //定义端口号  
            TcpListener tcplistener = new TcpListener(port);
            Console.WriteLine("侦听端口号： " + port.ToString());
            tcplistener.Start();
            //侦听端口号  
            while (true)
            {
                Socket socket = tcplistener.AcceptSocket();
                //并获取传送和接收数据的Scoket实例  
                HttpProxyServer proxy = new HttpProxyServer(socket);
                //Proxy类实例化  
                Thread thread = new Thread(new ThreadStart(proxy.Run));
                //创建线程  
                thread.Start();
                //启动线程  
            }
        }
        #endregion

        #region TaskDemo
        static void TaskDemo()
        {
            PrintInfo("1");
            TaskDemoAsync();
            PrintInfo("2");
            Console.ReadLine();
        }

        static async void TaskDemoAsync()
        {
            PrintInfo("01");
            //var i = await GetResultAsync().ConfigureAwait(false);
            var i = await GetResultAsync("1").ConfigureAwait(true);
            PrintInfo("04");
            Console.WriteLine(i);
            i = await GetResultAsync("2").ConfigureAwait(false);
            PrintInfo("05");
            Console.WriteLine(i);
        }

        static Task<int> GetResultAsync(string i)
        {
            PrintInfo($"0{i}2");
            var task = new Task<int>(() =>
            {
                Thread.Sleep(500);
                PrintInfo($"0{i}3");
                return 5;
            });
            task.Start();
            return task;
        }

        static void PrintInfo(string i)
        {
            var currentThread = Thread.CurrentThread;
            Console.WriteLine($"{i}-CurrentThread: Id-{currentThread.ManagedThreadId}, Name-{currentThread.Name}" +
                $", IsBackground-{currentThread.IsBackground}, IsThreadPool: {currentThread.IsThreadPoolThread}");
        }
        #endregion

        #region WebClient
        private static void WebClient()
        {
            new Http.HttpWebClient().RunDemo();
        }
        #endregion

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

        #region NTcp
        static void NTcp()
        {
            var i = string.Empty;
            while (true)
            {
                Console.WriteLine("请选择运行客户端还是服务端: 0-客户端, 1-服务端");
                i = Console.ReadLine();
                if (i == "0")
                {
                    NClient();
                    break;
                }
                else if (i == "1")
                {
                    NServer();
                    break;
                }
            }
        }
        #endregion

        #region NClient
        static void NClient()
        {
            new NTcpClient().Run();
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
