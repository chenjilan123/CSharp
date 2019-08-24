using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CSharp.Client
{
    public class WebApiClient
    {
        private readonly string _url;
        public int TimeOut = 1000;
        public WebApiClient(string url)
        {
            this._url = url;
        }

        public string Request()
        {
            var request = HttpWebRequest.Create(this._url);
            request.Timeout = TimeOut;
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
