using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Sms.OneNet
{
    public class OneNetSmsResult
    {
        /// <summary>
        /// 处理结果码
        /// </summary>
        [JsonProperty("result"), JsonConverter(typeof(StringEnumConverter))]
        public OneNetResultCode Result { get; set; }
        /// <summary>
        /// 处理结果信息描述
        /// </summary>
        [JsonProperty("resultinfo")]
        public string ResultInfo { get; set; }
        /// <summary>
        /// 处理结果错误信息
        /// </summary>
        [JsonProperty("errorinfo")]
        public string ErrorInfo { get; set; }
    }
}
