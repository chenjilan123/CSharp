using CSharp.Helper;
using CSharp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

//using sb = System.Text.StringBuilder;

namespace CSharp
{
    class Program
    {
        private const int PadLength = 30;
        static void Main(string[] args)
        {
            try
            {
<<<<<<< HEAD
                Split();
=======
                StringBuilderExtended();
>>>>>>> 00ada4e4475776da53489db78f8993be501c64a6
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }

<<<<<<< HEAD
        #region Split
        static void Split()
        {
            var c = '0';
            var s = "8774x0";
            var sp = s.Split(c);
            Console.WriteLine($"Total Length: {sp.Length}");
            foreach (var item in sp)
            {
                Console.WriteLine($"Length: {item.Length}, Value: {item}");
            }
=======
        #region IEnumerableExtended
        static void IEnumerableExtended()
        {
            var lst = new[] { "1", "2", "3", "5" };
            lst.Show();
        }
        #endregion

        #region StringBuilderExtended
        static void StringBuilderExtended()
        {
            var sb = new StringBuilder("Hello World!");

            //callvirt
            //调用时立即抛出异常, 因为callvirt检验null
            //sb = null;
            sb.Append("!");
            //也可以这样调用, 就是静态方法的特化。
            //call
            var i = StringBuilderExtension.IndexOf(sb, '!');
            //实例方法语法
            //call
            //sb = null;
            //在方法内部抛出异常, 因为call不检验null
            i = sb.IndexOf('!');
            Console.WriteLine($"Index of '!': {i}");

            //创建委托调用扩展方法
            Func<char, int> d = sb.IndexOf;
            i = d.Invoke('o');
            Console.WriteLine($"Index of 'o': {i}");

            Func<char, char, StringBuilder> f = sb.Replace;
            f.Invoke('o', '0');
            Console.WriteLine(sb.ToString());
        }
        #endregion

        #region AppDomain_
        static void AppDomain_()
        {
            Console.WriteLine(AppDomain.CurrentDomain);
        }
        #endregion

        #region ConstFromRefLib
        static void ConstFromRefLib()
        {
            var s = CSharp.Utility.Const.HelloWorld;
            s += 1;
            //Console.WriteLine(s.ToString());
            Console.WriteLine("{0}", s);

        }
        #endregion

        #region StaticConstructor
        static void StaticConstructor()
        {
            //var s1 = new S1();
            ////Console.WriteLine(S1.Vs1);
            //var s2 = new S2();
            //不会调用C1类型构造器。
            //Console.WriteLine(C2.Value);
            //会调用C1类型构造器, 因为调用了基类构造函数
            //var c2 = new C2();

            //显式运行C2的类型构造器。
            RuntimeTypeHandle t = typeof(C2).TypeHandle;
            RuntimeHelpers.RunClassConstructor(t);

            Console.WriteLine("{0}", Type.GetTypeFromHandle(t));
>>>>>>> 00ada4e4475776da53489db78f8993be501c64a6
        }
        #endregion

        #region Flags
        static void Flags()
        {
            {
                var f = FlagBits1.B1 | FlagBits1.B2 | FlagBits1.B3 | FlagBits1.B4;
                Console.WriteLine(f);
            }
            {
                var f = FlagBits2.B1 | FlagBits2.B2 | FlagBits2.B3 | FlagBits2.B4;
                Console.WriteLine(f);
            }
        }
        #endregion

        #region Collection
        static void Collection()
        {
            //Queue<int> a = null;
            //SortedList<int, int> b;
            //LinkedList<int> c;

        }
        #endregion

        #region BitConvert
        static void BitConvert()
        {
            Console.WriteLine($"IsBigEndian: {!BitConverter.IsLittleEndian}");
            
            var bytes = BitConverter.GetBytes(111.54);
            Print(bytes);
            Console.WriteLine();
            bytes = BitConverter.GetBytes(5);
            Print(bytes);
            void Print(byte[] data)
            {
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(data);
                }
                foreach (var b in data)
                {
                    Console.Write(b.ToString("X2"));
                }
            }
        }
        #endregion

        #region ReadFile
        private static void ReadFile()
        {
            var file = new FileInfo("source/111.db");
            using (var fs = file.OpenRead())
            {
                var buffer = new byte[1024];
                var data = new byte[fs.Length + 1024];
                var index = 0;
                var len = fs.Read(buffer, 0, buffer.Length);
                while(len > 0)
                {
                    buffer.CopyTo(data, index);
                    index += len;
                    len = fs.Read(buffer, 0, buffer.Length);
                }
                for (int i = 0; i < index; i++)
                {
                    Console.Write(data[i]);
                }
            }
        }
        #endregion

        #region TrimByte
        private static void TrimByte()
        {

            //var data = TrimByte(new byte[] { 1, 2, 3, 4, 5, 0, 0, 0, 0 }, 0);
            var data = TrimByte(new byte[] { 1, 2, 3, 4, 5, 0, 0, 0, 0 }, 3, 5, 0);
            //var data = TrimByte(new byte[] { 0, 0, 0, 0 }, 0);
            //var data = TrimByte(new byte[] { 1, 2, 3, 0, 0, 5, 0, 0 }, 0);
            foreach (var b in data)
            {
                Console.Write(b);
            }
        }

        private static byte[] TrimByte(byte[] data, int begin, int length, byte trim)
        {
            var data1 = new byte[length];
            for (int i = 0; i < data1.Length; i++)
            {
                data1[i] = data[begin + i];
            }
            var index = data1.Length - 1;
            while (index >= 0 && data1[index] == trim)
                index--;
            if (index < 0) return new byte[0];
            return data1.Take(index + 1).ToArray();
        }

        private static byte[] TrimByte(byte[] data, byte trim)
        {
            return TrimByte(data, 0, data.Length, trim);
            //var index = data.Length - 1;
            //while (index >= 0 && data[index] == trim)
            //    index--;
            //if (index < 0) return new byte[0];
            //return data.Take(index + 1).ToArray();
        }
        #endregion

        #region QuesQues
        static void QuesQues()
        {
            var obj = GetObject();
            if (obj is int i)
            {
                Console.WriteLine($"Int: {i}");
            }
            if (obj is string s)
            {
                Console.WriteLine($"String: {s}");
            }
        }

        static Object GetObject()
        {
            object obj = 5;
            obj = null;
            return obj ?? "Halo";
            //等价
            //return obj == null ? "Halo" : obj;
            //return obj != null ? obj : "Halo";
        }
        #endregion

        #region Covariance 
        static void Covariance()
        {
            IEnumerable<string> a = new List<string>();
            IEnumerable<object> b;
            b = a;

            //GenericClass<string> a1 = new GenericClass<string>();
            //GenericClass<object> a2;
            //a2 = a1;

            //GenericInterface<string> c = null;
            //GenericInterface<object> d;
            //Error
            //d = c;

            Convariance<string> e = null;
            Convariance<object> f;
            f = e;
        }
        #endregion

        #region StaticIntial
        static void StaticIntial()
        {
            StaticCls.PrintI2();
        }
        #endregion

        #region DecimalCompute
        static void DecimalCompute()
        {
            var v1 = 298.2M;
            var v2 = 362.3M;

            var v3 = (v2 - v1) / (decimal)0.33333;

            Console.WriteLine(v3);
        }
        #endregion

        #region PadZero
        private static void PadZero()
        {
            Console.WriteLine($"{10000:D4}");
            Console.WriteLine($"{15:D4}");

            decimal m
                = 412423.51121524M
                //= 43544587.15343534543512454m; // False
                //= 5.403231M;  //True
                ;
            double d = (double)m;
            Console.WriteLine(m);
            Console.WriteLine(d);
            Console.WriteLine(m == (decimal)d);
        }
        #endregion

        #region Helper
        private class Helper
        {
            public void Run()
            {
                DecimalCompute();
            }

            #region DecimalCompute
            void DecimalCompute()
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

            #region ComputeMD5
            void ComputeMD5()
            {
                var arrFileList = new[] { "F:\\tools\\ODTwithODAC122010.zip", "F:\\tools\\NDP471-DevPack-ENU.exe", "F:\\tools\\TeamViewer_12.1.10277.0.exe" };

                foreach (var sFullName in arrFileList)
                {
                    var fileInfo = new FileInfo(sFullName);
                    if (!fileInfo.Exists)
                    {
                        Console.WriteLine($"File [{fileInfo.FullName}] doesn't exists.");
                    }
                    var sw = Stopwatch.StartNew();
                    var s1 = GetMD5HashFromFile(fileInfo.FullName).ToUpper();
                    var s2 = FileToMD5Hash(fileInfo.FullName);


                    //var retVal = BinaryHelper.GetASCIIString(s1);
                    //StringBuilder sb = new StringBuilder();

                    //for (int i = 0; i < retVal.Length; i++)
                    //{
                    //    sb.Append(retVal[i].ToString("x2"));
                    //}
                    //Console.WriteLine(sb.ToString());

                    sw.Stop();
                    Console.WriteLine(fileInfo);
                    Console.WriteLine($"  FileName: {fileInfo.FullName}");
                    Console.WriteLine($"FileLength: {fileInfo.Length} bytes");
                    Console.WriteLine($" Time: {sw.Elapsed.TotalSeconds.ToString("0.000")}");
                    Console.WriteLine($"Value1: {s1}");
                    Console.WriteLine($"Value2: {s2}");

                    //ToBCD
                    var sbMD5_1 = new StringBuilder();
                    foreach (var b in Encoding.UTF8.GetBytes(s1))
                    {
                        sbMD5_1.Append(b.ToString("X2"));
                    }
                    Console.WriteLine($"MD5 64_1: {sbMD5_1.ToString()}");
                    var sbMD5_2 = new StringBuilder();
                    foreach (var b in BinaryHelper.GetASCIIString(s2))
                    {
                        sbMD5_2.Append(b.ToString("X2"));
                    }
                    Console.WriteLine($"MD5 64_2: {sbMD5_2.ToString()}");
                }
            }
            /// <summary>
            /// 获取文件MD5值
            /// </summary>
            /// <param name="fileName">文件绝对路径</param>
            /// <returns>MD5值</returns>
            public static string GetMD5HashFromFile(string fileName)
            {
                try
                {
                    FileStream file = new FileStream(fileName, FileMode.Open);
                    System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    byte[] retVal = md5.ComputeHash(file);
                    file.Close();

                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }
                    //return BinaryHelper.ToASCIIString(retVal, 0, retVal.Length);
                    return sb.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
                }
            }
            public static string FileToMD5Hash(string sFileName)
            {
                var bFile = File.ReadAllBytes(sFileName);
                byte[] result = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5"))
            .ComputeHash(bFile);
                StringBuilder output = new StringBuilder(16);
                for (int i = 0; i < result.Length; i++)
                {
                    output.Append((result[i]).ToString("X2",
                    System.Globalization.CultureInfo.InvariantCulture));
                }
                return output.ToString();
            }
            public static string StringToMD5Hash(string inputString)
            {
                byte[] result = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5"))
                .ComputeHash(Encoding.UTF8.GetBytes(inputString));
                StringBuilder output = new StringBuilder(16);
                for (int i = 0; i < result.Length; i++)
                {
                    output.Append((result[i]).ToString("X2",
                    System.Globalization.CultureInfo.InvariantCulture));
                }
                return output.ToString();
            }
            #endregion

            #region PackData
            void PackData()
            {
                var inEntity = new UP_PREVENTION_MSG_FILELIST_REQ_ACK();

                List<byte> bodyData = new List<byte>();

                var server = BinaryHelper.GetGBKBytes(inEntity.SERVER);
                var username = BinaryHelper.GetGBKBytes(inEntity.USERNAME);
                var pwd = BinaryHelper.GetGBKBytes(inEntity.PSSSWORD);

                List<byte> fileListData = new List<byte>();
                StringBuilder sDesc = new StringBuilder();
                foreach (var item in inEntity.FILE_LIST)
                {
                    var filename = BinaryHelper.GetGBKBytes(item.FileName);
                    var fileurl = BinaryHelper.GetGBKBytes(item.FileUrl);
                    fileListData.Add((byte)filename.Length);
                    fileListData.AddRange(filename);
                    fileListData.Add(item.FileType);

                    //河南标准扩展
                    fileListData.Add(item.FileFormat);
                    fileListData.AddRange(BinaryHelper.StringToBCD(item.MD5));

                    fileListData.AddRange(BinaryHelper.GetBytes(item.FileSize));
                    fileListData.Add((byte)fileurl.Length);
                    fileListData.AddRange(fileurl);
                    sDesc.AppendLine(string.Format("FileType:{0} FileSize:{1} FileName:{2} FileUrl:{3}", item.FileType,
                        item.FileSize, item.FileName, item.FileUrl));
                }

                //bodyData.AddRange(BinaryHelper.GetGBKBytes(inEntity.Vehicle_No, 21));//车牌号
                //bodyData.Add(inEntity.Vehicle_Color);//车牌颜色
                //bodyData.AddRange(BinaryHelper.GetBytes((ushort)inEntity.JTB809SubCmdCode));//子业务类型
                bodyData.AddRange(BinaryHelper.GetBytes((UInt32)(6 + server.Length + username.Length + pwd.Length + fileListData.Count)));//后续数据长度

                //bodyData.AddRange(BinaryHelper.GetGBKBytes(inEntity.INFO_ID_STRING));//消息ID
                bodyData.Add((byte)server.Length);
                bodyData.AddRange(server);
                bodyData.AddRange(BinaryHelper.GetBytes(inEntity.TCP_PORT));
                bodyData.Add((byte)username.Length);
                bodyData.AddRange(username);
                bodyData.Add((byte)pwd.Length);
                bodyData.AddRange(pwd);

                bodyData.Add((byte)inEntity.FILE_LIST.Count);
                bodyData.AddRange(fileListData);
                //LogUtil.Info(string.Format("子业务类型:{4} 车牌号:{0} SERVER:{1} USERNAME:{2} PSSSWORD:{3} File:\n{5}", inEntity.Vehicle_No, inEntity.SERVER, inEntity.USERNAME, inEntity.PSSSWORD, inEntity.JTB809SubCmdCode.ToString(), sDesc.ToString()), BizModule.Tcp, LogModuleName.UpDataManagerBase);

                foreach (var c in bodyData.ToArray())
                {
                    Console.WriteLine(c);
                }

            }
            #endregion

            #region Common
            void Common()
            {
                //var sFullName = Path.Combine("F:\\Cgo8\\Web\\AttachFile", "20190313\\1265292870\\6502\\37d4a31c-e6b4-4723-94f8-e811f7b2fc90\03_00_6502_0_37d4a31ce6b4472394f8e811f7b2fc90.bin".Replace("\\", "/"));

                var sFullName = Path.Combine("D:\\Top7\\AttachFile", "20190313/1265292870/6502/37d4a31c-e6b4-4723-94f8-e811f7b2fc90/00_65_6502_0_37d4a31ce6b4472394f8e811f7b2fc90.jpg");
                Console.WriteLine(sFullName);
                //return;

                var s = "1A156FBC";
                foreach (var c in StringToBCD(s))
                {
                    Console.WriteLine(c);
                }
                //return;

                var fileInfo = new FileInfo("hahaha.jpg");
                Console.WriteLine(fileInfo.Extension);
                //return;

                var i = 5;
                Console.WriteLine("0X" + i.ToString("X2"));

                var d = 1090.50;
                Console.WriteLine((byte)d);
                //return;

                var list = new List<byte>();
                list.Add(1);
                list.Add(2);
                list.AddRange(new List<byte>());

                foreach (var a in list)
                {
                    Console.WriteLine(a);
                }

            }
            #endregion

            #region SplitByLength
            void SplitByLength()
            {
                var iMaxLength = 5;
                while (true)
                {
                    Console.WriteLine("Please input a string:");
                    var s = Console.ReadLine();
                    //var s = "1234567890abcdefghijklmn";
                    foreach (var sub in GroupStrByLength(s, iMaxLength))
                    {
                        Console.WriteLine(sub);
                    }
                }
            }

            /// <summary>
            /// 字符串按长度分组
            /// </summary>
            /// <param name="sStr"></param>
            /// <param name="iMaxLength"></param>
            /// <returns></returns>
            IEnumerable<string> GroupStrByLength(string sStr, int iMaxLength)
            {
                var iStrLength = sStr.Length;
                while (iStrLength > iMaxLength)
                {
                    yield return sStr.Substring(0, iMaxLength);
                    iStrLength = iStrLength - iMaxLength;
                    sStr = sStr.Substring(iMaxLength, iStrLength);
                }
                if (iStrLength != 0)
                {
                    yield return sStr;
                }
            }
            #endregion

            #region ReplaceChar
            void ReplaceChar()
            {
                var sContent = "hahahsadsaf@%&#jfdsjf7234^3D*dsfsd";
                //var arrRemove = new char[] { '!', '@', '#', '$', '%', '^', '&', '' };
                var arrRemove = new string[] { "!", "@", "#", "$", "%", "^", "&" };
                foreach (var c in arrRemove)
                {
                    sContent = sContent.Replace(c, "");
                }

                Console.WriteLine(sContent);
            }
            #endregion

            #region Convert
            void Convert()
            {
                var arrByte = new byte[16] { 4, 131, 23, 43, 42, 3, 2, 1, 5, 4, 3, 8, 100, 101, 102, 103 };
                var sb = new StringBuilder();
                AppendInfoContent(sb, "TEST", arrByte);
                Console.WriteLine(sb.ToString());
                //return;

                var t = DateTime.Now.ToString("yyMMddHHmmss");
                Console.WriteLine(t);
                var b = StringToBCD(t);
                foreach (var c in b)
                {
                    Console.WriteLine(c);
                }
                //return;
                var s = StringToBCD("0123456789");
                foreach (var c in s)
                {
                    Console.WriteLine(c);
                }
            }
            private void AppendInfoContent(StringBuilder sbContent, string sKey, byte[] arrValue)
            {
                sbContent.Append(sKey);
                sbContent.Append(":=");
                sbContent.Append("0X");
                foreach (var b in arrValue)
                {
                    sbContent.Append(b.ToString("X2"));
                }
                sbContent.Append(";");
            }
            #endregion

            /// <summary>
            /// String转BCD
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static byte[] StringToBCD(string value)
            {
                int iLength = value.Length / 2;
                byte[] ret = new byte[iLength];
                for (int i = 0; i < iLength; i++)
                    ret[i] = System.Convert.ToByte(value.Substring(i * 2, 2), 16);
                return ret;

            }
        }
        #endregion

        #region SubsequenceArray
        private static void SubsequenceArray()
        {
            var arr = new byte[] { 1, 2, 3, 4, 5 };
            var sub = new byte[3];

            Array.Copy(arr, sub, 3);

            foreach (var b in sub)
            {
                Console.WriteLine(b);
            }
        }
        #endregion

        #region 汉字长度
        private static void ChineseCharLength()
        {
            var s = "长度";
            Console.WriteLine($"{s}: {s.Length}");
        }
        #endregion

        #region IntToByte
        private static void IntToByte()
        {
            var i = 511;
            var b = (byte)i;
            Console.WriteLine($"Direct: {b}");
            b = (byte)(i & 255);
            Console.WriteLine($" & 255: {b}");
        }
        #endregion

        #region ParseTime
        /// <summary>
        /// 匹配时间
        /// </summary>
        private static void ParseTime()
        {
            var t1 = new DateTime(1970, 1, 1).AddSeconds(1562112000);
            var t2 = new DateTime(1970, 1, 1).AddSeconds(1593734400);
            Console.WriteLine(t1);
            Console.WriteLine(t2);
            //return;

            string msg;
            msg = !DateTime.TryParse("16:00:00", out var tTest) ? "Parse failure" : tTest.ToODBC();
            Console.WriteLine($"TimeOfDay: {tTest.TimeOfDay.ToString()}");
            Console.WriteLine($"     Time: {msg}");

            var tsNow = DateTime.Now.TimeOfDay;
            var tsBegin = TimeSpan.Parse("16:05:04");

            Console.WriteLine($" Now: {tsNow}");
            Console.WriteLine($"Then: {tsBegin}");

            var sCompare = tsNow > tsBegin ? ">" : "<=";
            Console.WriteLine($"{tsNow} {sCompare} {tsBegin}");

        }
        #endregion

        #region 获取Web请求参数
        private static void GetWebRequestParam()
        {
            //var sUrl = "http://baidu.com/index.html?search=12x&key=504&mki=xxkfig";

            var sUrl = "http://baidu.com/haha?sdaf3=&fds=3";
            var param = GetPairs(sUrl).ToList();
            foreach (var pair in param)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }
        private static IEnumerable<KeyValuePair<string, string>> GetPairs(string sUrl)
        {
            int iParamBegin = sUrl.IndexOf('?');
            if (iParamBegin < 0)
            {
                yield break;
            }
            var sKeyPair = sUrl.Substring(iParamBegin + 1, sUrl.Length - iParamBegin - 1);
            var arrPairs = sKeyPair.Split('&');
            foreach (var sPair in arrPairs)
            {
                var keyValue = sPair.Split('=');
                if (keyValue == null || keyValue.Length != 2)
                {
                    continue;
                }
                yield return new KeyValuePair<string, string>(keyValue[0], keyValue[1]);
            }
        }
        #endregion

        #region TestNullIEnumerable
        private static void TestNullIEnumerable()
        {
            foreach (var item in GetNUllIEnmerable())
            {
                Console.WriteLine(item);
            }
        }

        private static IEnumerable<int> GetNUllIEnmerable()
        {
            return null;
        }
        #endregion

        #region 平台里程算法
        static decimal _decIncrementMileage = decimal.Zero;   //平台里程统计：平台里程计算增量(平台里程 = 数据库里程 + 增量)
        static decimal _decPrevMileage = decimal.Zero;        //平台里程统计：前一条轨迹里程(平台)(有效)
        static decimal _decPrevDBMileage = decimal.Zero;      //平台里程统计：前一条轨迹里程(数据库)(不论有效无效)
        static DateTime _tPreGpsTime = default(DateTime);     //平台里程统计：前一条轨迹时间
        private static void PlatMileageAlgorithm()
        {
            var arr = new List<Mileage>
            {
                new Mileage(){ Mil = 0,   GpsTime = "2019-04-30 10:00:00"},
                new Mileage(){ Mil = 100, GpsTime = "2019-04-30 10:00:01"},
                new Mileage(){ Mil = 101,   GpsTime = "2019-04-30 10:00:02"},
                new Mileage(){ Mil = 101, GpsTime = "2019-04-30 10:00:03"},
                new Mileage(){ Mil = 102, GpsTime = "2019-04-30 10:00:04"},
                new Mileage(){ Mil = 0,   GpsTime = "2019-04-30 10:00:05"},
                new Mileage(){ Mil = 1,   GpsTime = "2019-04-30 10:00:06"},
                new Mileage(){ Mil = 2,   GpsTime = "2019-04-30 10:00:07"},
                new Mileage(){ Mil = 2,   GpsTime = "2019-04-30 10:00:08"},
                new Mileage(){ Mil = 4,   GpsTime = "2019-04-30 10:00:09"},
                new Mileage(){ Mil = 5,   GpsTime = "2019-04-30 10:00:10"},
            };


            foreach (var mileage in arr)
            {
                var decMil = GetMileage(mileage.Mil, mileage.GpsTime);
                Console.WriteLine(decMil);
            }

        }
        private static decimal GetMileage(decimal decCurMileage, string sGpsTime)
        {
            //若里程数为0, 为无效点
            if (decCurMileage <= 0m)
            {
                return _decPrevMileage;
            }
            DateTime tGpsTime = default(DateTime);    //平台里程统计：当前条轨迹时间
            DateTime.TryParse(sGpsTime, out tGpsTime);
            //全部轨迹首条, 将增量修改为-decMileage
            if (_tPreGpsTime == default(DateTime))
            {
                _decIncrementMileage = 0;
            }
            //每日首条, 里程调整为0
            else if (DateTime.TryParse(sGpsTime, out tGpsTime) && tGpsTime.Date != _tPreGpsTime.Date)
            {
                _decIncrementMileage = 0;
            }
            //每日非首条轨迹
            else if (decCurMileage >= _decPrevDBMileage)
            {
                _decIncrementMileage = decCurMileage - _decPrevDBMileage;
            }
            //无效
            else
            {
                _decIncrementMileage = 0;
            }
            _tPreGpsTime = tGpsTime;
            _decPrevDBMileage = decCurMileage;
            decCurMileage = _decPrevMileage + _decIncrementMileage;
            //调整成功后, 才修改前一条轨迹里程
            _decPrevMileage = decCurMileage;
            return decCurMileage;
        }
        private static decimal GetMileageOld(decimal decCurMileage, string sGpsTime)
        {
            //若里程数为0, 为无效点
            if (decCurMileage <= 0m)
            {
                return _decPrevMileage;
            }
            DateTime tGpsTime = default(DateTime);    //平台里程统计：当前条轨迹时间
            DateTime.TryParse(sGpsTime, out tGpsTime);
            //全部轨迹首条, 将增量修改为-decMileage
            if (_tPreGpsTime == default(DateTime))
            {
                _decIncrementMileage = -decCurMileage;
            }
            //每日首条, 里程调整为0
            else if (DateTime.TryParse(sGpsTime, out tGpsTime) && tGpsTime.Date != _tPreGpsTime.Date)
            {
                _decIncrementMileage = _decPrevMileage - decCurMileage;
            }

            //调整里程数
            _tPreGpsTime = tGpsTime;
            decCurMileage += _decIncrementMileage;
            //调整后里程小于或等于前一条，使用前一条的数值。 //错误的数据。
            if (decCurMileage <= _decPrevMileage)
            {
                return _decPrevMileage;
            }
            //调整成功后, 才修改前一条轨迹里程
            _decPrevMileage = decCurMileage;
            return decCurMileage;
        }
        #endregion

        #region EnumeratorIntitalState
        private static void EnumeratorIntitalState()
        {
            var list = new List<int>();
            for (int i = 10; i < 20; i++)
            {
                list.Add(i);
            }

            var enumerator = list.GetEnumerator();
            Console.WriteLine(enumerator.Current); // 0
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            Console.WriteLine(enumerator.Current); // 0
        }
        #endregion

        #region Linq - GetRange
        private static void LinqGetRange()
        {
            var list = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(i);
            }

            var range1 = list.GetRange(990, 100);
            Console.WriteLine(range1[0]);

        }
        #endregion

        #region 路径
        private static void PrintPath()
        {
            System.Uri uri = new Uri(typeof(string).Assembly.CodeBase);
            string runtimePath = System.IO.Path.GetDirectoryName(uri.LocalPath);
            string installUtilPath = System.IO.Path.Combine(runtimePath, "InstallUtil.exe");
            string a = Environment.CurrentDirectory;

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

    }
}
