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

            string sUrl = string.Format("http://{0}/datadispatch/auth/{1}", "10.250.2.3:9092/networkcar-city", "371000");
            //WebProxy proxyObject = new WebProxy(GlobalConfig.ProxyIP, GlobalConfig.ProxyPort);
            string sToken = WebHelper.TakeMethodPost(sUrl, "eyvoewCZ", null, "application/json", null);
            Console.WriteLine(sToken);
            return;

            string[] Urls = new []
                {
                    "http://127.0.0.1:8088/Cgo8",
                    "http://www.baidu.com",
                    "http://"
                };

            HttpWebRequest request = WebRequest.CreateHttp(Urls[1]);
            request.Proxy = new WebProxy("127.0.0.1", 5678);
            //request.Proxy = new WebProxy("192.168.3.149", 5678);

            //http://10.250.2.3:9092/networkcar-city/datadispatch/auth/371000
            //http://10.250.2.3:9092/networkcar-city/datadispatch/auth/371000

            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            using (var sr = new StreamReader(stream))
            {
                Console.WriteLine(sr.ReadToEnd());
            }
        }
    }
}
