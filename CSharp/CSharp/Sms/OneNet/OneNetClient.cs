using CSharp.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CSharp.Sms.OneNet
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

        public OneNetSmsResult SendSms(string mobiles, string tempid, Dictionary<string, string> customParams)
        {
            var uri = this.BuildSmsRequestUri(mobiles, tempid, customParams);
            var request = HttpWebRequest.Create(uri);
            request.Method = HttpMethod.Post.Method;
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                return _serializer.Deserialize<OneNetSmsResult>(stream);
            }
        }

        private Uri BuildSmsRequestUri(string mobiles, string tempid, Dictionary<string, string> customParams)
        {
            var sb = new StringBuilder(_baseAddress);
            sb.Append("?");
            var signid = this.GetSignId();
            sb.AppendFormat("sicode={0}&mobiles={1}&tempid={2}&signId={3}", this._sicode, mobiles, tempid, signid);
            foreach (var item in customParams)
            {
                sb.AppendFormat("&{0}={1}", item.Key, item.Value);
            }
            return new Uri(sb.ToString());
        }

        private string GetSignId()
        {
            return "101044";
        }
    }
}
