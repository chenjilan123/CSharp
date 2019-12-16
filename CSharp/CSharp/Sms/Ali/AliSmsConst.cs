//#define ML
//#define XG2
//#define XG3
#define XG4

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
#if XG1
        internal const string AccessKeyId = "LTAIDPIXar5akNvy";
        internal const string AccessKeySecret = "EykxULuW76mnHZOd6waZFsXKqbvrP8";
        internal const string PhoneNumbers = "15980217471";
        internal const string SignName = "星冠平台车辆报警通知";
        internal const string TemplateCode = "SMS_172356099";
        internal const string TemplateParam = "{\"platenum\":\"闽A12345蓝\",\"time\":\"2019-08-27 12:00:50\",\"address\":\"福建省福州市仓山区建新镇福建农林大学\",\"alarm\":\"超速报警\",\"speed\":\"70km/h\"}";
#elif XG2
        internal const string AccessKeyId = "LTAIDPIXar5akNvy";
        internal const string AccessKeySecret = "EykxULuW76mnHZOd6waZFsXKqbvrP8";
        internal const string PhoneNumbers = "15980217471";
        internal const string SignName = "星冠平台车辆报警通知";
        internal const string TemplateCode = "SMS_174580762";
        //您共有${ExceedNums}张卡本月数据流量已达到套餐的${Percentage}%，详请登录物联网平台查询!(温馨提示:数据存在一定的时延)${DataDate}
        //您共有1张卡本月数据流量已达到套餐的150.00%，详请登录物联网平台查询!(温馨提示:数据存在一定的时延)2019-10-11
        //{"ExceedNums":"1","Percentage":"150.00","DataDate":"2019-10-11"}
        internal const string TemplateParam = "{\"ExceedNums\":\"1\",\"Percentage\":\"150.00\",\"DataDate\":\"2019-10-11\"}";
#elif XG3
        internal const string AccessKeyId = "LTAIDPIXar5akNvy";
        internal const string AccessKeySecret = "EykxULuW76mnHZOd6waZFsXKqbvrP8";
        internal const string PhoneNumbers = "15980217471";
        internal const string SignName = "星冠平台车辆报警通知";
        internal const string TemplateCode = "SMS_174585572";
        internal const string TemplateParam = "{\"PackageName\":\"6G\",\"Percentage\":\"80.00\",\"UsableGprs\":\"1.757324G\",\"UseGprs\":\"298.242675G\",\"DataDate\":\"2019-10-22\"}";
        //您的${Name}使用已达到${Percentage}%，使用情况:${condition}，详情请登录物联网平台查询!(温馨提示:数据存在一定的时延)${DataDate}
        //您的6G流量池使用已达到150.00%，使用情况:1805421GB，详情请登录物联网平台查询!(温馨提示:数据存在一定的时延)2019-10-11
        //{"PackageName":"6G","Percentage":"80.00","UsableGprs":"1.757324G","UseGprs":"298.242675G","DataDate":"2019-10-22"}
        //{"Name":"6G流量池","Percentage":"150.00","condition":"1805421GB","DataDate":"2019-10-11"}
        //internal const string TemplateParam = "{\"Name\":\"6G流量池\",\"Percentage\":\"150.00\",\"condition\":\"1805421GB\",\"DataDate\":\"2019-10-11\"}";
#elif XG4
        internal const string AccessKeyId = "LTAIDPIXar5akNvy";
        internal const string AccessKeySecret = "EykxULuW76mnHZOd6waZFsXKqbvrP8";
        internal const string PhoneNumbers = "15980217471";
        internal const string SignName = "星冠平台车辆报警通知";
        internal const string TemplateCode = "SMS_174585572";
        //您正常服务期的数据卡为${Nums}张，其中${ExpireInfo}将到期，请及时处理，以免造成车辆离线，详请登录物联网平台查询!(温馨提示:数据存在一定的时延)${DataDate}
        //
        internal const string TemplateParam = "{\"PackageName\":\"6G\",\"Percentage\":\"80.00\",\"UsableGprs\":\"1.757324G\",\"UseGprs\":\"298.242675G\",\"DataDate\":\"2019-10-22\"}";
#elif ML
        internal const string AccessKeyId = "LTAI4FeBiPSorjMVSNkyX3RD";
        internal const string AccessKeySecret = "NLThp4Vutqp2JFfRH4LNLe9tWtyHQe";
        internal const string PhoneNumbers = "15980217471";
        internal const string SignName = "浙江马良通讯科技有限公司";
        internal const string TemplateCode = "SMS_172351371";
        //internal const string TemplateParam = "{\"platenum\":\"闽A12345蓝\",\"time\":\"2019-08-27 12:00:50\",\"address\":\"福建省福州市仓山区建新镇福建农林大学\",\"alarm\":\"超速报警\",\"speed\":\"70km/h\"}";
        internal const string TemplateParam = "{\"platenum\":\"1768231\",\"time\":\"2019/10/31 17:05:55\",\"address\":\"\",\"alarm\":\"超速报警\",\"speed\":\"67\"}";
#else
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
#endif
    }
}
