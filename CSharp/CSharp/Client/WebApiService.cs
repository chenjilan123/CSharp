using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace CSharp.Client
{
    public  class WebApiService
    {
        /// <summary>
        /// 调用web api服务
        /// </summary>
        /// <param name=\"urlparames"></param>
        /// <returns></returns>
        public string TakeServicesMethod(string url, string urlparames, int iTimeOut = 0, int iResendTime = 2)
        {
            string jsonStr = "";
            try
            {
                byte[] postBytes = Encoding.UTF8.GetBytes(urlparames);
                Uri address = new Uri(url);
                System.Net.ServicePointManager.Expect100Continue = false;
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(address);
                req.KeepAlive = true;
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                //req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = postBytes.Length;
                //req.ContentLength = Encoding.UTF8.GetByteCount(urlparames);
                if (iTimeOut == 0)
                    req.Timeout = 1000;//默认1000毫秒超时
                else
                    req.Timeout = iTimeOut;
                //Stream postStream = req.GetRequestStream();
                //StreamWriter myStreamWriter = new StreamWriter(postStream, Encoding.UTF8);
                //myStreamWriter.Write(urlparames);
                //myStreamWriter.Close();
                using (Stream postStream = req.GetRequestStream())
                {
                    postStream.Write(postBytes, 0, postBytes.Length);
                }
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream s = response.GetResponseStream();

                    using (StreamReader sr = new StreamReader(s))
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                    s.Close();
                }
                else
                {
                    throw new Exception("web api error");
                }
            }
            catch (WebException tx)
            {
                if (tx.Status == WebExceptionStatus.Timeout && iResendTime > 0)//超时，重新发一次
                {
                    Console.WriteLine("Http请求超时，进行重发");
                    jsonStr = this.TakeServicesMethod(url, urlparames, iTimeOut == 0 ? 5000 : iTimeOut + 5000, --iResendTime);
                }
                else
                {
                    Console.WriteLine("TakeServicesMethod txException." + tx.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TakeServicesMethod exException." + ex.Message);
            }
            return jsonStr;
        }
    }
}
