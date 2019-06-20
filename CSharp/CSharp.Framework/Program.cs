﻿using CSharp.Framework.Email;
using CSharp.Framework.Transfer;
using CSharp.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace CSharp.Framework
{
    class Program
    {
        private const int PadLength = 30;

        static void Main(string[] args)
        {
            Email();
        }

        #region DynamicExtension
        static void DynamicExtension()
        {
            var cur = MethodInfo.GetCurrentMethod();
            Console.WriteLine(cur.Name);

            var methods = typeof(Program).GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            //var methods = typeof(Program).GetMethods();
            foreach (var method in methods)
            {
                Console.WriteLine($"Method name:{method.Name.PadRight(20, ' ')}\t\tIsStatic:{method.IsStatic}\t\tIsPrivate:{method.IsPrivate}\t\tIsPublic:{method.IsPublic}");
            }
        }
        #endregion

        #region 编码测试
        static void EncodeTest()
        {
            var bytes = BinaryHelper.GetGBKBytes("MARK_STATUS:=0X00;EVENT_TYPE:=0X02;ALARM_GRADE:=0X02;AHEAD_SPEED:=0X00;AHEAD_DISTANCE:=0X00;DIVERGE_TYPE:=0X01;ROAD_SIGN_TYPE:=0X00;ROAD_SIGN_DATA:=0X00;SPEED:=0X47;ALTITUDE:=98;LONGITUDE:=113989118;LATITUDE:=37997353;VEHICLE_STATUS:=1025;ALARM_ID:=0X303039313432341906091605220005000A995D");
            Console.WriteLine(bytes.Length);
        }
        #endregion

        #region HashtableOrder
        static void HashtableOrder()
        {
            var hs = new Hashtable();
            hs.Add("fff", 1);
            hs.Add("hafg", 1);
            //hs.Add("hafg", 1);
            //hs.Add("fff", 1);
            foreach (var item in hs.Keys)
            {
                Console.WriteLine(item);
            }
        }
        #endregion

        #region ToStringWithX
        private static void ToStringWithX()
        {
            long i = 3;
            Console.WriteLine(i.ToString("X1"));
            Console.WriteLine(i.ToString("X2"));
            Console.WriteLine(i.ToString("X3"));
            Console.WriteLine(i.ToString("X4"));
        }
        #endregion

        #region TimeSpanFormat
        static void TimeSpanFormat()
        {
            var ts = DateTime.Now - new DateTime(2019, 5, 30, 16, 0, 0);
            //var s = string.Format(@"{0:h\小\时m\分s\秒}", DateTime.Now);

            //.NET 3.5不支持该格式化
            var s = string.Format(@"{0:h\小\时m\分s\秒}", ts);
            //var s = string.Format(@"{0}小时{1}分{2}秒", ts.Hours.ToString(), ts.Minutes.ToString(), ts.Seconds.ToString());
            Console.WriteLine(s);
        }
        #endregion

        #region GetUtcTime
        static void GetUtcTime()
        {
            //var t = DateTime.FromFileTimeUtc(1263085674);

            //var t = DateTime.UtcNow;
            var tUtcZero = new DateTime(1970, 1, 1);
            var t = new DateTime(2010, 1, 10, 9, 7, 54).ToUniversalTime();
            var l = (t - tUtcZero).TotalSeconds;
            Console.WriteLine($"Seconds: {l}");
            Console.WriteLine($"  Ticks: {t.Ticks}");
            Console.WriteLine($" Second: {t.Second}");
            Console.WriteLine(TimeZone.CurrentTimeZone.StandardName);


            l = 0x000000005CFDEE4E;
            Console.WriteLine($"  Local: {tUtcZero.AddSeconds((double)l).ToLocalTime()}");
            //Console.WriteLine($"   Test: {t}");
            //var t = new DateTime(2010, 1, 9, 9, 7, 54);

            //return (long)(t.ToUniversalTime() - DateTime.MinValue).TotalSeconds;
            //DateTime.UtcNow.ToFileTimeUtc();
            //DateTime.Now.ToFileTimeUtc();
        }
        #endregion

        #region CRC
        const string ValideteString =
            "0000005B00010501920000A98AC70100010000000000C3F6433930303531000000000000000000000000000192020000002500070607E314322805EE511000E9F3730050004000005010016713880000001F00000FFF";
            //格式有问题                                                                                                                    0和1之间有个符号
            //"0000005B00010501920000A98AC70100010000000000C3F6433930303531000000000000000000000000000192020000002500050607E311453006C3EF5A0‭15798FE‬0047004600002C510050228C0000000100000000";

        static void CRC()
        {
            //Get byte array from string.
            //while (true)
            {
                Console.WriteLine("请输入待CRC验证字符串：");
                var msgContent =
                ValideteString;

                var msgBytes = new List<byte>();
                for (int i = 0; i < msgContent.Length / 2; i++)
                {
                    var s = msgContent.Substring(i * 2, 2);
                    msgBytes.Add(byte.Parse(s, System.Globalization.NumberStyles.HexNumber));
                }
                //foreach (var b in msgBytes)
                //    Console.Write(b.ToString("X2"));
                //Console.WriteLine();
                //Console.WriteLine();
                //Console.WriteLine(msgContent);

                var crc = GetCRCByte(msgBytes);
                //Console.WriteLine(crc);
                var crcBytes = GetBytes(crc);
                Console.Write("CRC验证结果: ");
                foreach (var b in crcBytes)
                    Console.Write(b.ToString("X2"));
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        static short GetCRCByte(List<byte> data)
        {
            int crc_reg = 0xFFFF;//TODO:????网上初始值为 0
            short current;
            for (int i = 0; i < data.Count; i++)
            {
                current = (short)(data[i] << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((short)(crc_reg ^ current) < 0)
                        crc_reg = (short)((crc_reg << 1) ^ 0x1021);
                    else
                        crc_reg <<= 1;
                    current <<= 1;
                }
            }
            return (short)crc_reg;
        }
        static byte[] GetBytes(short value)
        {
            byte[] reBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(reBytes);
            }
            return reBytes;
        }
        #endregion

        #region Path
        private static void Path()
        {
            System.Uri uri = new Uri(typeof(string).Assembly.CodeBase);
            string runtimePath = System.IO.Path.GetDirectoryName(uri.LocalPath);
            string installUtilPath = System.IO.Path.Combine(runtimePath, "InstallUtil.exe");
            string a = System.Windows.Forms.Application.ExecutablePath;

            PrintMessage("Runtime Path", runtimePath);
            PrintMessage("Install Util Path", installUtilPath);
            PrintMessage("Execitable Path", a);
        }

        private static void PrintMessage(string key, string msg)
        {
            var pad = (key.Length / PadLength + 1) * PadLength;
            Console.WriteLine($"{key.PadLeft(pad, ' ')}: {msg}");
        }
        #endregion

        #region DecimalCompute
        static void DecimalCompute()
        {
            var t1 = new DateTime(1990, 1, 1, 1, 1, 10);
            var t2 = new DateTime(1990, 1, 1, 1, 1, 30);
            var mut = (t2 - t1).TotalMinutes;
            //decimal x = 5m / 20m;
            decimal x = 5m / (decimal)mut;
            Console.WriteLine(x);
            Console.WriteLine(x.ToString("0.0"));
        }
        #endregion

        #region Email
        static void Email()
        {
            //内容不规范会被服务器判断为垃圾邮件
            //new EmailHelper().SendEmail(Guid.NewGuid().ToString(), "不是垃圾邮件!", "你好", "", "357592895@qq.com", "h");

            //成功
            //new EmailHelper().SendEmail(Guid.NewGuid().ToString(), "第三方监控平台"
            //    , "您好，这里是第三方监控平台，相关数据汇总详见附件，请及时查收处理。"
            //    , ""
            //    , "357592895@qq.com", "h");

            //发送附件
            //const string sAttachPath = @"TopTimeServerTPM_导出报表文件.rar";
            //const string _40M = @"40M.rar";
            //const string _90M = @"90M.exe";
            //const string sAp = @"报警明细_日报_2019-05-21_636941020856560817.xlsx";

            while (true)
            {
                new EmailHelper().SendEmail(Guid.NewGuid().ToString(), "第三方监控平台"
                , "您好，第三方监控平台测试。"
                , "报警明细_日报_2019-06-18_两客一危_636965136070493296.xlsx"
                , "357592895@qq.com", "h");
                Thread.Sleep(5000);
            }
        }
        #endregion

        #region Transfer
        static void Transfer()
        {
            new AnalysisGDOCPosInfo().OnWork();
        }
        #endregion

        #region Des
        private static string quote =
            "Things may come to those who wait, but only the " +
            "things left by those who hustle. -- Abraham Lincoln";

        static void Des()
        {
            AesCryptoServiceProvider aesCSP = new AesCryptoServiceProvider();

            aesCSP.GenerateKey();
            aesCSP.GenerateIV();
            byte[] encQuote = EncryptString(aesCSP, quote);

            Console.WriteLine("Encrypted Quote:\n");
            Console.WriteLine(Convert.ToBase64String(encQuote));

            Console.WriteLine("\nDecrypted Quote:\n");
            Console.WriteLine(DecryptBytes(aesCSP, encQuote));
        }

        public static byte[] EncryptString(SymmetricAlgorithm symAlg, string inString)
        {
            byte[] inBlock = UnicodeEncoding.Unicode.GetBytes(inString);
            ICryptoTransform xfrm = symAlg.CreateEncryptor();
            byte[] outBlock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);

            return outBlock;
        }

        public static string DecryptBytes(SymmetricAlgorithm symAlg, byte[] inBytes)
        {
            ICryptoTransform xfrm = symAlg.CreateDecryptor();
            byte[] outBlock = xfrm.TransformFinalBlock(inBytes, 0, inBytes.Length);

            return UnicodeEncoding.Unicode.GetString(outBlock);
        }
        #endregion

        #region Aggregate
        static void Aggregate()
        {
            var lst = new List<string>
            {
                "500x", "ppxl", "hehehehe"
            };

            var s = lst.Aggregate("",
            (rst, cur) =>
            {
                rst += ",'" + cur + "'";
                return rst;
            });
            Console.WriteLine(s.Trim(','));
        }
        #endregion
    }
}
