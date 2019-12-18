using CSharp.Http.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CSharp.Http
{
    public class HttpProxyClient
    {
        public void HttpGetByProxy()
        {
            while (true)
            {
                //堡垒机Http代理端口：60.212.191.26:8089


                Console.WriteLine("请输入访问地址: ");
                Console.Write("  Ip: ");
                var sIp = Console.ReadLine();
                Console.Write("端口: ");
                var sPort = Console.ReadLine();
                try
                {
                    var ipAddress = IPAddress.Parse(sIp);
                    if (!ushort.TryParse(sPort, out var port))
                    {
                        Console.WriteLine("请输入正确的端口");
                    }
                    var ep = new IPEndPoint(ipAddress, port);
                    string sUrl = string.Format("http://{0}/datadispatch/auth/{1}", $"{ep.ToString()}", "371000");
                    //WebProxy proxyObject = new WebProxy(GlobalConfig.ProxyIP, GlobalConfig.ProxyPort);


                    var webProxy = new WebProxy("60.212.191.26", 8089);
                    string sToken = WebHelper.TakeMethodPost(sUrl, "eyvoewCZ", null, "application/json", webProxy);
                    Console.WriteLine(sToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }


            //string[] Urls = new[]
            //    {
            //        "http://127.0.0.1:8088/Cgo8",
            //        "http://www.baidu.com",
            //        "http://"
            //    };

            //HttpWebRequest request = WebRequest.CreateHttp(Urls[1]);
            //request.Proxy = new WebProxy("127.0.0.1", 5678);
            ////request.Proxy = new WebProxy("192.168.3.149", 5678);

            ////http://10.250.2.3:9092/networkcar-city/datadispatch/auth/371000
            ////http://10.250.2.3:9092/networkcar-city/datadispatch/auth/371000

            //var response = request.GetResponse();
            //using (var stream = response.GetResponseStream())
            //using (var sr = new StreamReader(stream))
            //{
            //    Console.WriteLine(sr.ReadToEnd());
            //}
        }

        private void StartWebSocket()
        {

        }
    }
}
