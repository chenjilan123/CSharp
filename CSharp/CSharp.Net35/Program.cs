using CSharp.Net35.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CSharp.Net35
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest();
        }

        static void WebRequest()
        {

            while (true)
            {
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
                    string sUrl = string.Format("http://{0}/datadispatch/auth/{1}", $"{ep.ToString()}/networkcar-city", "371000");
                    //WebProxy proxyObject = new WebProxy(GlobalConfig.ProxyIP, GlobalConfig.ProxyPort);
                    string sToken = WebHelper.TakeMethodPost(sUrl, "eyvoewCZ", null, "application/json", null);
                    Console.WriteLine(sToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }
    }
}
