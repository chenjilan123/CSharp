using CSharp.Framework.Email;
using CSharp.Framework.Transfer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CSharp.Framework
{
    class Program
    {
        private const int PadLength = 30;

        static void Main(string[] args)
        {
            var ts = DateTime.Now - new DateTime(2019, 5, 30, 16, 0, 0);
            //var s = string.Format(@"{0:h\小\时m\分s\秒}", DateTime.Now);

            //.NET 3.5不支持该格式化
            var s = string.Format(@"{0:h\小\时m\分s\秒}", ts);
            //var s = string.Format(@"{0}小时{1}分{2}秒", ts.Hours.ToString(), ts.Minutes.ToString(), ts.Seconds.ToString());
            Console.WriteLine(s);
            return;
            //var hs = new Hashtable();
            //hs.Add("fff", 1);
            //hs.Add("hafg", 1);
            ////hs.Add("hafg", 1);
            ////hs.Add("fff", 1);
            //foreach (var item in hs.Keys)
            //{
            //    Console.WriteLine(item);
            //}

            Transfer();
        }

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
            const string sAttachPath = @"TopTimeServerTPM_导出报表文件.rar";
            const string _40M = @"40M.rar";
            const string _90M = @"90M.exe";
            const string sAp = @"报警明细_日报_2019-05-21_636941020856560817.xlsx";
            new EmailHelper().SendEmail(Guid.NewGuid().ToString(), "第三方监控平台"
                , "您好，这里是第三方监控平台，相关数据汇总详见附件，请及时查收处理。"
                , ""
                , "357592895@qq.com", "h");
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
