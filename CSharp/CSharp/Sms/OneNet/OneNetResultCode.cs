using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Sms.OneNet
{
    public enum OneNetResultCode
    {
        下发处理成功 = 11101,
        下发处理失败 = 11102,
        发送内容有敏感词 = 11103,
        下发号码包含黑名单 = 11104,
        接收号码包含黑名单 = 11105,
        接收号码中包含非法号码 = 11106,
        SI鉴权失败 = 11108,
        余额不足 = 11109,
        参数异常 = 11112,
    }
}
