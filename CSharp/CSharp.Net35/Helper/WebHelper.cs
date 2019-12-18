using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CSharp.Net35.Helper
{
    public  class WebHelper
    {
       /// <summary>
        /// 调用web api服务
       /// </summary>
       /// <param name="url"></param>
       /// <param name="urlparames"></param>
       /// <param name="head"></param>
       /// <param name="bNeedCompress">是否需要数据压缩</param>
        ///<param name="iCompressLimit">超过限制长度的</param>
       /// <param name="sContentType"></param>
       /// <returns></returns>
        public static string TakeMethodPost(string url, string urlparames, Dictionary<string, string> head, bool bNeedCompress, int iCompressLimit = 3, string sContentType = "application/x-www-form-urlencoded;charset=utf-8", WebProxy proxyObject = null)
        {
            string jsonStr = "";
            try
            {
                byte[] postBytes = Encoding.UTF8.GetBytes(urlparames);
                Uri address = new Uri(url);
                System.Net.ServicePointManager.Expect100Continue = false;
                bool bSLS = false;
                if (head!=null && head.ContainsKey("SLS"))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    //req.ProtocolVersion = HttpVersion.Version10;
                    bSLS = true;
                }
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(address);
                req.KeepAlive = true;
                req.Method = "POST";
                //req.Timeout = 5 * 1000;//五秒超时
                if (proxyObject != null)
                {
                    req.Proxy = proxyObject;
                }
                if (head != null)
                {
                    foreach (var kv in head)
                    {
                        if (kv.Key.ToString().ToUpper() == "SLS")//HTTPS请求
                        {
                            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                            continue;
                        }
                        req.Headers.Set(kv.Key, kv.Value);
                        
                    }
                }
                //if (bNeedCompress && iCompressLimit*1024<=postBytes.Length)//开启压缩 并且上报长度大于限制
                //{
                //    req.Headers.Set("Content-Encoding", "gzip");
                //    postBytes = UnityToolHelper.GZipCompress(postBytes);
                //}
                Console.WriteLine(string.Format("Request Url :{0} Content:\r\n{1} \r\n SLS:{2} ContentType:{3}", url, urlparames, bSLS, sContentType));
                req.ContentType =sContentType;
                req.ContentLength = postBytes.Length;
                using (Stream postStream = req.GetRequestStream())
                {
                    postStream.Write(postBytes, 0, postBytes.Length);
                }
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream s = response.GetResponseStream();
                    if (!string.IsNullOrEmpty(response.ContentEncoding))//存在格式化类型的 需要解析
                    {
                        byte[] bRead = new byte[s.Length];
                        s.Read(bRead, 0, bRead.Length);
                        bRead = UnityToolHelper.DeCompressByType(response.ContentEncoding,bRead);
                        s = new MemoryStream(UnityToolHelper.GZipDecompress(bRead));
                    }
                    using (StreamReader sr = new StreamReader(s, Encoding.GetEncoding("utf-8")))//
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                    s.Close();
                }
                //else
                //{
                //    throw new Exception("web api error");
                //}
            }
            catch (Exception ex)
            {
                //throw ex;
                //jsonStr = ex.Message;
                Console.WriteLine(ex);
            }
            return jsonStr;
        }
        //https://www.cnblogs.com/greenerycn/archive/2010/05/15/csharp_http_post.html
        //Form-data  格式 https://www.cnblogs.com/wonyun/p/7966967.html
        public static string GetFormData(string sName, string sData, string sBoundary)
        {
            StringBuilder sb = new StringBuilder();
            //string sBoundary = DateTime.Now.Ticks.ToString("x");
            string sBoundary_Begin = "--" + sBoundary + "\r\n";
            string sBoundary_End = "--" + sBoundary + "--\r\n";
            sb.Append(sBoundary_Begin);
            sb.Append(string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n", sName)); //"Content-Type: application/octet-stream\r\n\r\n"
            sb.Append("\r\n");
            sb.Append(sData);
            sb.Append("\r\n");
            sb.Append(sBoundary_End);
            return sb.ToString();
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        /// <summary>
        /// 调用web api服务
        /// </summary>
        /// <param name="url"></param>
        /// <param name="urlparames"></param>
        /// <param name="head"></param>
        /// <param name="bNeedCompress">是否需要数据压缩</param>
        ///<param name="iCompressLimit">超过限制长度的</param>
        /// <param name="sContentType"></param>
        /// <returns></returns>
        public static string TakeMethodPost(string url, byte[] postBytes, Dictionary<string, string> head, string sContentType = "application/x-www-form-urlencoded;charset=utf-8",WebProxy proxyObject=null)
        {
            string jsonStr = "";
            try
            {
                Uri address = new Uri(url);
                System.Net.ServicePointManager.Expect100Continue = false;
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(address);
                req.KeepAlive = true;
                req.Method = "POST";
                if (proxyObject != null)
                {
                    req.Proxy = proxyObject;
                }
                if (head != null)
                {
                    foreach (var kv in head)
                    {
                        req.Headers.Set(kv.Key, kv.Value);
                    }
                }
                //if (bNeedCompress && iCompressLimit*1024<=postBytes.Length)//开启压缩 并且上报长度大于限制
                //{
                //    req.Headers.Set("Content-Encoding", "gzip");
                //    postBytes = UnityToolHelper.GZipCompress(postBytes);
                //}
                Console.WriteLine(string.Format("Request Url :{0} Content:{1} Proxy:{2}", url, postBytes.Length,req.Proxy));//BinaryHelper.BCDToString(postBytes,0,postBytes.Length)
                req.ContentType = sContentType;
                req.ContentLength = postBytes.Length;
                using (Stream postStream = req.GetRequestStream())
                {
                    postStream.Write(postBytes, 0, postBytes.Length);
                }
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream s = response.GetResponseStream();
                    if (!string.IsNullOrEmpty(response.ContentEncoding))//存在格式化类型的 需要解析
                    {
                        byte[] bRead = new byte[s.Length];
                        s.Read(bRead, 0, bRead.Length);
                        bRead = UnityToolHelper.DeCompressByType(response.ContentEncoding, bRead);
                        s = new MemoryStream(UnityToolHelper.GZipDecompress(bRead));
                    }
                    using (StreamReader sr = new StreamReader(s, Encoding.GetEncoding("utf-8")))//
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                    s.Close();
                }
                //else
                //{
                //    throw new Exception("web api error");
                //}
            }
            catch (Exception ex)
            {
                //throw ex;
                jsonStr = ex.Message;
                Console.WriteLine(ex);
            }
            return jsonStr;
        }

        public static string TakeMethodPost(string url, string urlparames, Dictionary<string, string> head = null, string sContentType = "application/x-www-form-urlencoded;charset=utf-8", WebProxy proxyObject = null)
        {
            return TakeMethodPost(url, urlparames, head, false, 10, sContentType, proxyObject);
        }
        public static string TakeMethodGet(string url, Dictionary<string, string> parames, Dictionary<string, string> head )
        {
            return TakeMethodGet(GetParamesString(url, parames));
        }
        /// <summary>
        /// http get
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string TakeMethodGet(string Url, string type = "utf-8", bool bLog = true)
        {
            try
            {
                if (bLog)
                {
                    Console.WriteLine(string.Format("HttpGet Request Url :{0} ", Url));
                }
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                // Get the response instance.
                System.Net.WebResponse wResp = wReq.GetResponse();
                
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream)
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取url参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parames"></param>
        /// <returns></returns>
        private static string GetParamesString(string url,Dictionary<string, string> parames)
        {
            if (parames == null || parames.Count() == 0)
                return url;
            StringBuilder sb = new StringBuilder();
            sb.Append(url);
            sb.Append("?");
            int iIndex = 1;
            foreach (var kv in parames)
            {
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (iIndex != parames.Count())
                    sb.Append("&");   
                iIndex++;
            }

            return sb.ToString();
        }
    }
}
