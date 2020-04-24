﻿using CSharp.Framework.Email;
using CSharp.Framework.Face;
using CSharp.Framework.Face.V2_2;
using CSharp.Framework.Model;
using CSharp.Framework.Transfer;
using CSharp.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Windows.Controls;

namespace CSharp.Framework
{
    class Program
    {
        private const int PadLength = 30;

        static void Main(string[] args)
        {
            try
            {
                DependencyProperty();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #region DependencyProperty

        public static void DependencyProperty()
        {
            var p1 = new Personality() { Value = 1 };
            var p2 = new Personality() { Value = 2 };
            var p3 = new Personality() { Value = 3 };
            var p4 = new Personality() { Value = 4 };
            var p5 = new Personality() { Value = 5 };

            Console.WriteLine(p1.Value);
            Console.WriteLine(p2.Value);
            Console.WriteLine(p3.Value);
            Console.WriteLine(p4.Value);
            Console.WriteLine(p5.Value);
        }

        #endregion

        #region PropertyMachanism

        public static void PropertyMachanism()
        {
            var x = InstanceModel.Instance;
            var y = InstanceModel.Instance;
        }

        #endregion

        #region ShowInterfaceSpeedAndQueue
        public static void GetSpeed()
        {
            Stopwatch stopwatch = new Stopwatch();

            var count = 0;
            while (true)
            {
                stopwatch.Reset();
                stopwatch.Start();

                WebClient webClient = new WebClient();
                byte[] bytes = webClient.DownloadData("http://www.baidu.com");

                stopwatch.Stop();

                double seconds = stopwatch.Elapsed.TotalSeconds;

                double speed = bytes.Count() / seconds;

                Console.WriteLine(string.Format("Your download speed({1}): {0:#0.00} KB/s.", speed / 1024, ++count));

                Thread.Sleep(500);
            }
        }

        public static void GetSpeed1()
        {
            IPv4InterfaceStatistics v;
            IPGlobalStatistics s;

        }
        public static void ShowInterfaceSpeedAndQueue()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                IPv4InterfaceStatistics stats = adapter.GetIPv4Statistics();
                Console.WriteLine(adapter.Description);
                Console.WriteLine("     Speed .................................: {0}",
                    adapter.Speed);
                Console.WriteLine("     Output queue length....................: {0}",
                    stats.OutputQueueLength);
            }
        }
        public static void ShowIPStatistics(NetworkInterfaceComponent version)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPGlobalStatistics ipstat = null;
            switch (version)
            {
                case NetworkInterfaceComponent.IPv4:
                    ipstat = properties.GetIPv4GlobalStatistics();
                    Console.WriteLine("{0}IPv4 Statistics ", Environment.NewLine);
                    break;
                case NetworkInterfaceComponent.IPv6:
                    ipstat = properties.GetIPv4GlobalStatistics();
                    Console.WriteLine("{0}IPv6 Statistics ", Environment.NewLine);
                    break;
                default:
                    throw new ArgumentException("version");
                    //    break;
            }
            Console.WriteLine("  Forwarding enabled ...................... : {0}",
                ipstat.ForwardingEnabled);
            Console.WriteLine("  Interfaces .............................. : {0}",
                ipstat.NumberOfInterfaces);
            Console.WriteLine("  IP addresses ............................ : {0}",
                ipstat.NumberOfIPAddresses);
            Console.WriteLine("  Routes .................................. : {0}",
                ipstat.NumberOfRoutes);
            Console.WriteLine("  Default TTL ............................. : {0}",
                ipstat.DefaultTtl);
            Console.WriteLine("");
            Console.WriteLine("  Inbound Packet Data:");
            Console.WriteLine("      Received ............................ : {0}",
                ipstat.ReceivedPackets);
            Console.WriteLine("      Forwarded ........................... : {0}",
                ipstat.ReceivedPacketsForwarded);
            Console.WriteLine("      Delivered ........................... : {0}",
                ipstat.ReceivedPacketsDelivered);
            Console.WriteLine("      Discarded ........................... : {0}",
                ipstat.ReceivedPacketsDiscarded);
            Console.WriteLine("      Header Errors ....................... : {0}",
                ipstat.ReceivedPacketsWithHeadersErrors);
            Console.WriteLine("      Address Errors ...................... : {0}",
                ipstat.ReceivedPacketsWithAddressErrors);
            Console.WriteLine("      Unknown Protocol Errors ............. : {0}",
                ipstat.ReceivedPacketsWithUnknownProtocol);
            Console.WriteLine("");
            Console.WriteLine("  Outbound Packet Data:");
            Console.WriteLine("      Requested ........................... : {0}",
                 ipstat.OutputPacketRequests);
            Console.WriteLine("      Discarded ........................... : {0}",
                ipstat.OutputPacketsDiscarded);
            Console.WriteLine("      No Routing Discards ................. : {0}",
                ipstat.OutputPacketsWithNoRoute);
            Console.WriteLine("      Routing Entry Discards .............. : {0}",
                ipstat.OutputPacketRoutingDiscards);
            Console.WriteLine("");
            Console.WriteLine("  Reassembly Data:");
            Console.WriteLine("      Reassembly Timeout .................. : {0}",
                ipstat.PacketReassemblyTimeout);
            Console.WriteLine("      Reassemblies Required ............... : {0}",
                ipstat.PacketReassembliesRequired);
            Console.WriteLine("      Packets Reassembled ................. : {0}",
                ipstat.PacketsReassembled);
            Console.WriteLine("      Packets Fragmented .................. : {0}",
                ipstat.PacketsFragmented);
            Console.WriteLine("      Fragment Failures ................... : {0}",
                ipstat.PacketFragmentFailures);
            Console.WriteLine("");
        }
        #endregion

        #region ThreadAbort
        static void ThreadAbort()
        {
            var t0 = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: Hello.");
                        Thread.Sleep(TimeSpan.FromSeconds(1D));
                    }

                }
                catch (ThreadAbortException e)
                {
                    //添加Microsoft.CSharp
                    dynamic v = e.ExceptionState;
                    Console.WriteLine($"{v.Id}: {v.Message}");
                }
            });
            t0.Start();

            Thread.Sleep(TimeSpan.FromSeconds(2.5D));
            //t0.Abort("I just abort you....");
            t0.Abort(new { Id = 5, Message = "Hello are you ok?" });
        }
        #endregion

        #region ExceptionFlow
        static void ExceptionFlow()
        {
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            ThrowException();
        }

        static void ThrowException()
        {
            throw new IOException();
        }

        private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            //堆栈追踪只有一层。
            Console.WriteLine($"A exception occur: {e.Exception.ToString()}");
        }
        #endregion

        #region AppDomainDemo
        static void AppDomainDemo()
        {
            CSharp.Framework.Model.AppDomainModel.Marshalling();
        }
        #endregion

        #region CER
        static void CER()
        {
            new CER().Demo();
        }
        #endregion

        #region Transaction
        static void Transaction()
        {
            using (TransactionScope ts = new TransactionScope())
            {

            }
        }
        #endregion

        #region PerfGeneric
        static void PerfGeneric()
        {

            const int perfCount = 100000000;
            //添加了容量会快一点。 
            //无需GC
            using (new OperationTimer("List<int> with suitable capacity"))
            {
                var lst = new List<int>(perfCount + 1);
                for (int i = 0; i < perfCount; i++)
                {
                    lst.Add(i);
                    var t = lst[i];
                }
                lst = null;
            }
            //int: 泛型比非泛型快10倍。
            using (new OperationTimer("List<int>"))
            {
                var lst = new List<int>();
                for (int i = 0; i < perfCount; i++)
                {
                    lst.Add(i);
                    var t = lst[i];
                }
                lst = null;
            }
            using (new OperationTimer("ArrayList with int"))
            {
                var lst = new ArrayList();
                for (int i = 0; i < perfCount; i++)
                {
                    lst.Add(i);
                    var t = (int)lst[i];
                }
                lst = null;
            }

            using (new OperationTimer("List<DateTime> with suitable capacity"))
            {
                var lst = new List<DateTime>(perfCount + 1);
                for (int i = 0; i < perfCount; i++)
                {
                    lst.Add(DateTime.Now);
                    var t = lst[i];
                }
                lst = null;
            }
            using (new OperationTimer("List<DateTime>"))
            {
                var lst = new List<DateTime>();
                for (int i = 0; i < perfCount; i++)
                {
                    lst.Add(DateTime.Now);
                    var t = lst[i];
                }
                lst = null;
            }
            using (new OperationTimer("ArrayList with DateTime"))
            {
                var lst = new ArrayList();
                for (int i = 0; i < perfCount; i++)
                {
                    lst.Add(DateTime.Now);
                    var t = (DateTime)lst[i];
                }
                lst = null;
            }
        }
        #endregion

        #region OpenFolder
        static void OpenFolder()
        {
            var file = new FileInfo("CSharp.Framework.exe");

            System.Diagnostics.Process.Start("explorer.exe", file.DirectoryName);
        }
        #endregion

        #region HashOrder
        static void HashOrder()
        {
            var hash = new Hashtable();

            //var str = "127.0.0.1:19096:3440719e-6e90-447a-ab49-880d134b1a83|127.0.0.1:49096:8a2b1723-e659-4eda-94a4-35ac758ec7a6|127.0.0.1:59096:|127.0.0.1:59196:|127.0.0.1:29096:a94b4129-9c3e-4495-af55-6176a1dedadd|127.0.0.1:9096:d4a6d783-e9a3-4254-9dec-b0c67b2ecf19|127.0.0.1:39096:e32484b6-dca9-421a-a559-42540f299b03";
            var str = "127.0.0.1:19096|127.0.0.1:49096|127.0.0.1:59096|127.0.0.1:59196|127.0.0.1:29096|127.0.0.1:9096|127.0.0.1:39096";

            int i = 0;
            foreach (var item in str.Split('|').Reverse())
            //foreach (var item in str.Split('|'))
            {
                hash.Add(item, item);
                //hash.Add(item, i++);
            }
            Console.WriteLine($"数目：{hash.Count}");
            Console.WriteLine("键：");
            foreach (var item in hash.Keys)
            {
                Console.WriteLine(item);
            }
            var enumerator = hash.Keys.GetEnumerator();
            enumerator.MoveNext();
            Console.WriteLine($"键首项: {enumerator.Current}");
            foreach (var item in hash.Keys)
            {
                Console.WriteLine($"键首项: {item}");
                break;
            }
            Console.WriteLine("值：");
            foreach (var item in hash.Values)
            {
                Console.WriteLine(item);
            }
            foreach (var item in hash.Values)
            {
                Console.WriteLine($"值首项: {item}");
                break;
            }
        }
        #endregion

        #region OrAndOut
        static void OrAndOut()
        {
            //if (OrAndOut(10, out int iError) | OrAndOut(5, out iError) | OrAndOut(7, out iError)) //8
            if (OrAndOut(10, out int iError) || OrAndOut(5, out iError) || OrAndOut(7, out iError)) //11
            {
                Console.WriteLine(iError);
            }

        }
        static bool OrAndOut(int input, out int iError)
        {
            iError = input + 1;
            return input == 10;
        }
        #endregion

        #region StructConstruct
        static void StructConstruct()
        {
            Home home = new Home();
            Console.WriteLine(home.Computer.Value);
        }
        private struct Home
        {
            public Computer Computer;
        }
        private struct Computer
        {
            public int Value => 5;
        }
        #endregion

        #region ReverseRow
        static void ReverseRow()
        {
            //while (true)
            {
                Console.WriteLine("请输入文件地址: ");
                //var sPath = Console.ReadLine();
                //var sPath = @"C:\Users\11\Desktop\机场-公园.txt";
                var sPath = @"C:\Users\11\Desktop\绿地-公园.txt";
                if (File.Exists(sPath))
                {
                    var fileInfo = new FileInfo(sPath);
                    var lines = GetLines(fileInfo).Reverse();

                    var text = lines.Aggregate<string>((s1, s2) => s1 + "\n" + s2);
                    Console.WriteLine(text);
                    SaveReverseLines(fileInfo, text);
                }
                else
                {
                    Console.WriteLine("文件不存在。");
                }
            }
        }

        private static void SaveReverseLines(FileInfo fileInfo, string sText)
        {
            var bytes = Encoding.ASCII.GetBytes(sText);
            using (var sw = fileInfo.OpenWrite())
            {
                sw.Write(bytes, 0, bytes.Length);
            }
        }

        static IEnumerable<string> GetLines(FileInfo fileInfo)
        {
            using (var sr = fileInfo.OpenText())
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
                yield return sr.ReadLine();
            }
        }
        #endregion

        #region Round
        static void Round()
        {
            Console.WriteLine(Math.Truncate(1.001));
            Console.WriteLine(Math.Round(5.153214, 3));
        }
        #endregion

        #region EnqueueNull
        static void EnqueueNull()
        {
            var que = new Queue<MainMenu>();
            que.Enqueue(null);
            Console.WriteLine(que.Count);
            var m = que.Dequeue();
            Console.WriteLine(que.Count);
            Console.WriteLine(m == null);
        }
        #endregion

        #region ArcFace
        static void ArcFace() => new ArcFaceTest().Run();
        #endregion

        #region StructSize
        static void StructSize()
        {
            var @struct = new Struct001();
            Console.WriteLine(Marshal.SizeOf(@struct));
            //Console.WriteLine(Marshal.SizeOf(typeof(Struct001)));
            //Console.WriteLine(Marshal.SizeOf<Struct001>());
            //Console.WriteLine(Marshal.SizeOf<Struct001>(@struct));
            //Console.WriteLine(Marshal.SizeOf<bool>());
            //Console.WriteLine(Marshal.SizeOf<byte>());
            ////Console.WriteLine(Marshal.SizeOf<DateTime>());
            //Console.WriteLine(Marshal.SizeOf<int>());
            //Console.WriteLine(Marshal.SizeOf<uint>());
            //Console.WriteLine(Marshal.SizeOf<long>());
            //Console.WriteLine(Marshal.SizeOf<ulong>());
            //Console.WriteLine(Marshal.SizeOf<float>());
            //Console.WriteLine(Marshal.SizeOf<double>());
            //Console.WriteLine(Marshal.SizeOf<decimal>());
        }

        //private class Struct001
        private struct Struct001
        {
            //public IntPtr Ptr; //8
            //public DateTime Value0; //8
            //public int Value1; //4
            //public int Value2; //4
            //public long Value3; //8
            //public float Value4; //4-8
            //public float Value5; //4-8
            //public float Value6; //4-8
            //public float Value7; //4-8
            //public decimal Value8; //16
        }
        #endregion

        #region GetFiles
        static void GetFiles()
        {
            var search = "CSharp.*";
            var path = Path.Combine(Application.StartupPath, search);
            var curDir = new DirectoryInfo(Application.StartupPath);
            Console.WriteLine($"Current Directory: {curDir.FullName}");
            var files = curDir.GetFiles(search, SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                Console.WriteLine($"    Matched Files: {file}");
            }

        }
        #endregion

        #region TryParseFailureValue
        static void TryParseFailureValue()
        {
            var i = 10;
            int.TryParse("0x1", NumberStyles.HexNumber, null, out i);
            int.TryParse("0x1", out i);
            Console.WriteLine(i);

            var t = DateTime.Now;
            DateTime.TryParse("xx", out t); //UTC零点
            Console.WriteLine($"Time: {t.ToLocalTime()}, Kind: {t.Kind}"); //UTC标识
            Console.WriteLine($"Time: {t}, Kind: {t.Kind}");
            Console.WriteLine($"Time: {DateTime.Now.ToLocalTime()}, Kind: {DateTime.Now.Kind}");
            Console.WriteLine($"Time: {DateTime.Now}, Kind: {DateTime.Now.Kind}");

        }
        #endregion

        #region LambdaParse
        static void LambdaParse()
        {
            Action<string> action = s => s += "1";
            Func<string, string> func = s => s += "1";
            //Action
            LambdaParse(action);
            //Func
            LambdaParse(func);
            //Func => Func > Action
            LambdaParse(s => s += "1");
        }

        static void LambdaParse(Action<string> action)
        {

        }
        static void LambdaParse(Func<string, string> func)
        {

        }
        #endregion

        #region BCD_String
        static void BCD_String()
        {
            var t = new DateTime(1990, 2, 1, 10, 20, 30);
            var bcd = StringToBCD(t.ToString("yyMMddHHmmss"));
            foreach (var b in bcd)
            {
                Console.Write(b.ToString("X2"));
            }
            Console.WriteLine();
        }
        static DateTime BCDToDateTime(byte[] bytes, int startIndex, int length, string format)
        {
            DateTime dateTime;
            try
            {
                //string sDateTime = "";
                string sDateTime = BinaryHelper.BCDToString(bytes, startIndex, length);
                bool bResult = DateTime.TryParseExact(sDateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                if (!bResult)
                    dateTime = DateTime.Now;
            }
            catch
            {
                dateTime = DateTime.Now;
            }
            return dateTime;
        }
        static byte[] StringToBCD(string value)
        {
            int iLength = value.Length / 2;
            byte[] ret = new byte[iLength];
            for (int i = 0; i < iLength; i++)
                ret[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
            return ret;

        }
        #endregion

        #region DicAdd
        static void DicAdd()
        {
            var dic = new Dictionary<string, int>();
            dic.Add("h", 1);
            dic["h"]++;
            dic["h"]++;
            dic["h"]++;
            Console.WriteLine(dic["h"]);
        }
        #endregion

        #region Overflow
        static void Overflow()
        {
            int i = -24575;
            short s = (short)i;
            Console.WriteLine(s.ToString("x"));
            Console.WriteLine(s);

            short h = -24575;//(short)0xFFFFA001;
            //short j = (short)0xa001; //直接报错
            short j = (short)i; //溢出(j的值为0xFFFFA001溢出了，short也是按int方式显示的。)
            Console.WriteLine(h.ToString("x"));
            Console.WriteLine(j.ToString("x"));
        }
        #endregion

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

        #region PathTest
        private static void PathTest()
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
