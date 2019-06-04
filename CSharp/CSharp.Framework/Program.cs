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
            CRC();
            return;

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

        #region CRC
        static void CRC()
        {
            //Get byte array from string.
            var msgContent =
                "0000040200000123920000A98AC70100010000000000C3F643393030353100000000000000000000000000019204000001CC5452414E535F545950453A3D37323B56494E3A3DC3F64339303035313B5452414354494F4E3A3D33303B545241494C45525F56494E3A3DC3F64339303035353B56454849434C455F4E4154494F4E414C4954593A3D3335303538323B56454849434C455F545950453A3D33323B5254504E3A3D31353135303031303B4F574552535F4E414D453A3DC3F6D4CBBCAFCDC53B4F574552535F4F5249475F49443A3D313031303130303130313B574F4552535F54454C3A3D31383132333435363738393B52544F4C4E3A3D3132333435363737373B56454849434C455F4D4F44453A3DB1BCB3DB41383B56454849434C455F434F4C4F523A3D313B56454849434C455F4F5249475F49443A3D32373031303B4452495645525F494E464F3A3DBCDDCABBD4B131BAC53B4755415244535F494E464F3A3DD1BAD4CBD4B131BAC53B415050524F5645445F544F4E4E4147453A3D35303B44475F545950453A3D30323230313B434152474F5F4E414D453A3DD4ADD3CD3B434152474F5F544F4E4E4147453A3D34323B5452414E53504F52545F4F524947494E3A3DBFA8CBFEB6FB3B5452414E53504F52545F4445533A3DD4C6C4CF3B5453534C3A3D313236313438363539317C313236343838383939";
                //"0000007500000198140000A98AC70100010000000000B2E2413930303031000000000000000000000000000114020000003F010080000000005CB42ABA11F6EFBA00000008BDF4BCB1B1A8BEAF00010100010000000682512101CB998E0043004300000000001E03220000000000000000";

            var msgBytes = new List<byte>();
            for (int i = 0; i < msgContent.Length / 2; i++)
                msgBytes.Add(byte.Parse(msgContent.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber));
            //foreach (var b in msgBytes)
            //    Console.Write(b.ToString("X2"));
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine(msgContent);

            var crc = GetCRCByte(msgBytes);
            Console.WriteLine(crc);
            var crcBytes = GetBytes(crc);
            foreach (var b in crcBytes)
                Console.Write(b.ToString("X2"));
            Console.WriteLine();
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
