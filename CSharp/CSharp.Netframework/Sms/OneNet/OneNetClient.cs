using CSharp.Netframework.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CSharp.Netframework.Sms.OneNet
{
    public class OneNetClient
    {
        private const string _baseAddress = "http://api.sms.heclouds.com/tempSignSmsSend";
        private readonly string _sicode;
        private readonly NewtonSerializer _serializer;

        public OneNetClient(string sicode)
        {
            this._sicode = sicode;
            _serializer = new NewtonSerializer();
        }

        public OneNetSmsResult SendSms(string mobiles, string tempid, string appendParam)
        {
            var uri = this.BuildSmsRequestUri(mobiles, tempid, appendParam);
            var request = HttpWebRequest.Create(uri);
            request.Method = HttpMethod.Post.Method;
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                return _serializer.Deserialize<OneNetSmsResult>(stream);
            }
        }

        private Uri BuildSmsRequestUri(string mobiles, string tempid, string appendParam)
        {
            var sb = new StringBuilder(_baseAddress);
            sb.Append("?");
            sb.AppendFormat("sicode={0}&mobiles={1}&tempid={2}", this._sicode, mobiles, tempid);
            sb.AppendFormat("&{0}", appendParam);
            return new Uri(sb.ToString());
        }
    }
}
