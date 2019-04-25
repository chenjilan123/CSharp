using CSharp.SimpleConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Netframework
{
    class Program
    {
        static void Main(string[] args)
        {
            var hashark = SHA256("haha");
            Console.WriteLine(hashark);
            return;

        }

        #region SHA256
        public static string SHA256(string str)
        {
            //如果str有中文，不同Encoding的sha是不同的！！
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);

            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(SHA256Data);

            return BitConverter.ToString(by).Replace("-", "").ToLower(); //64
                                                                         //return Convert.ToBase64String(by);                         //44
        }
        #endregion

        #region HMACSHA256
        /// <summary>
        /// HMACSHA256
        /// </summary>
        /// <param name="message"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        #endregion

        #region 生成8位长度数字

        private static void PrintEightNumber()
        {
            while (true)
            {
                Console.WriteLine(GetEightNumber());
            }
        }

        private static uint _orderId;

        static Program()
        {
            _orderId = uint.MaxValue - 5000;//(uint)new Random(DateTime.Now.Millisecond).Next();
        }

        private static string GetEightNumber()
        {
            var s = (_orderId++).ToString();
            if (s.Length > 8)
                return s.Substring(s.Length - 8, 8);
            return s.PadLeft(8, '0');
        }
        #endregion

        #region SimpleConfig
        private static void SimpleConfig()
        {
            XmlFileHandler.LoadConfig();
            Print(nameof(Variable.SysTitle), Variable.SysTitle);
            Print(nameof(Variable.IsOpenAlarm), Variable.IsOpenAlarm);
            Print(nameof(Variable.Value), Variable.Value);
        }

        private static void Print(string key, object value)
        {
            var pad = (key.Length / 20 + 1) * 20;
            Console.WriteLine($"{key.PadLeft(pad, ' ')}: {value}");
        }
        #endregion

    }
}
