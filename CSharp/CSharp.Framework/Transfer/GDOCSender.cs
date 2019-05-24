using CSharp.Framework.Helper;
using CSharp.Framework.Transfer.JsonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace CSharp.Framework.Transfer
{
    public class GDOCSender<T1, T2>
        where T2 : GDOCResult
    {
        private const int MaxBatchSendCount = 20;
        /// <summary>
        /// 指令名称
        /// </summary>
        public string Cmd { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 键值
        /// </summary>
        public string IV { get; set; }
        /// <summary>
        /// 平台ID
        /// </summary>
        public int PlatformID { get; set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public GDOCSender()
        {
        }

        #region 发送
        public void Send()
        {
            var sArgs = string.Empty;
            var result = GetResponse(sArgs, false);
            if (result != null)
            {
                Console.WriteLine(string.Format("结束发送数据, Cmd: {0}, Result: {1}", this.Cmd, result.GetDescription()));
            }
            else
            {
                Console.WriteLine(string.Format("结束发送数据 - 无应答, Cmd: {0}", this.Cmd));
            }
        }

        public void Send(T1 arg)
        {
            var sArgs = SerializeHelper.ObjectToJson(arg);
            var result = GetResponse(sArgs, false);
            if (result != null)
            {
                Console.WriteLine(string.Format("结束发送数据, Cmd: {0}, Result: {1}", this.Cmd, result.GetDescription()));
            }
            else
            {
                Console.WriteLine(string.Format("结束发送数据 - 无应答, Cmd: {0}", this.Cmd));
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="argLst"></param>
        public void Send(IEnumerable<T1> argLst)
        {
            var argsToSend = new List<T1>();
            foreach (var arg in argLst)
            {
                if (argsToSend.Count >= MaxBatchSendCount)
                {
                    SendBatch(argsToSend);
                    Thread.Sleep(200);
                }
                argsToSend.Add(arg);
            }
            if (argsToSend.Count > 0)
            {
                SendBatch(argsToSend);
            }
        }
        #endregion

        #region 批量发送
        private void SendBatch(List<T1> argsToSend)
        {
            Console.WriteLine(string.Format("开始发送数据, Cmd: {0}, Count: {1}", this.Cmd, argsToSend.Count));
            var sArgs = SerializeHelper.ObjectToJson(argsToSend);
            var result = GetResponse(sArgs, true);
            if (result != null)
            {
                Console.WriteLine(string.Format("结束发送数据, Cmd: {0}, Count: {1}, Result: {2}", this.Cmd, argsToSend.Count, result.GetDescription()));
            }
            else
            {
                Console.WriteLine(string.Format("结束发送数据 - 无应答, Cmd: {0}, Count: {1}", this.Cmd, argsToSend.Count));
            }
            argsToSend.Clear();
        }
        private T2 GetResponse(string sArgs, bool bIsBatch)
        {
            //生成签名
            byte[] key;
            var sKey = GetKey(out key);
            var sSign = GetSign(sArgs, key);

            //发送数据
            Dictionary<string, string> httpParams = new Dictionary<string, string>
            {
                { nameof(Cmd), this.Cmd },
                { "Key", sKey },
                { nameof(PlatformID), this.PlatformID.ToString() },
            };
            //单量参数数据放请求参数里，批量接口数据放在Post参数体。
            if (!bIsBatch)
            {
                httpParams.Add("Args", sArgs);
            }
            httpParams.Add("Sign", sSign);

            var sUrl = WebHelper.GetParamesString(this.Url, httpParams);

            Console.WriteLine($"Args: {sArgs}");
            string sResp = WebHelper.TakeMethodPost(sUrl, sArgs);

            var sbParam = new StringBuilder();
            foreach (var param in httpParams)
            {
                sbParam.AppendFormat("    Key: {0}, Value: {1}\r\n", param.Key, param.Value);
            }
            Console.WriteLine(string.Format("Http请求: \r\nCmd: {0}\r\nUrl: {1}\r\nParam: \r\n{2}", this.Cmd, sUrl, sbParam.ToString()));
            Console.WriteLine("Http应答: " + sResp);
            
            T2 result = SerializeHelper.JsonToObject<T2>(sResp);
            return result;
        }
        #endregion

        #region 获取签名
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="sArgs"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetSign(string sArgs, byte[] key)
        {
            //var iv = StringHelper.StrToToHexByte(this.IV);
            var iv = Encoding.UTF8.GetBytes(this.IV);
            var sDes = SecurityHelper.EncryptDesString(sArgs.Trim(), key, iv);

            var sbKey = new StringBuilder();
            foreach (var b in key)
            {
                sbKey.Append(b.ToString("X2"));
            }
            //Console.WriteLine($"Key: {sbKey.ToString()}");
            //Console.WriteLine($" IV: {Encoding.UTF8.GetString(iv)}");
            //Console.WriteLine($"Des: {sDes}");
            var sMD5 = SecurityHelper.EncryptMD5String(sDes).ToLower();
            //Console.WriteLine($"Md5: {sMD5}");
            return sMD5;
        }
        #endregion

        #region 动态生成Key
        private string GetKey(out byte[] bytes)
        {
            var sKey = GenerateKey();
            bytes = Encoding.UTF8.GetBytes(sKey);
            return sKey;
        }

        private string GenerateKey()
        {
            var bytes = new byte[8];
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)random.Next(0x30, 0x39);
            }
            var sKey = Encoding.UTF8.GetString(bytes);
            return sKey;
        }
        #endregion

    }
}
