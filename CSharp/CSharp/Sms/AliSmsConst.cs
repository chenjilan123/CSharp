using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Sms
{
    public class AliSmsConst
    {
        ///常见错误
        ///1、模板不合法(不存在或被拉黑)
        ///2、没有访问权限
        ///3、签名不合法(不存在或被拉黑)
        ///4、账户余额不足
        ///5、模板变量缺少对应参数值

        internal const string AccessKeyId = "LTAIyXQzxcVhfE26";
        internal const string AccessKeySecret = "xjyLY5XE30XB6yexAGAdpcpwbyfdPW";
        internal const string PhoneNumbers = "15980217471";
        internal const string SignName = "星空联盟";
        /// <summary>
        /// 模板编号
        ///     内容：您的车辆$name于$time在$address发生$alarm，车速$speed
        /// </summary>
        internal const string TemplateCode = "SMS_172224746";
        internal const string TemplateParam =
@"{
    ""validate"":""180401""
}";
        //@"{
        //  ""platenum"": ""(车牌号)"",
        //  ""time"": ""(时间)"",
        //  ""address"": ""(地点)"",
        //  ""alarm"": ""(报警类型名称)"",
        //  ""speed"": ""(车速)""
        //}";
    }
}
