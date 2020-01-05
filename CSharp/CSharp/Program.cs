using CSharp.Helper;
using CSharp.Model;
using CSharp.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace CSharp
{
    class Program
    {
        private const int PadLength = 30;

        static void Main(string[] args)
        {
            try
            {
                StringDemo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }

        #region StringDemo
        static void StringDemo()
        {
            new StringDemo().Run();
        }
        #endregion

        #region CastString
        static void CastString()
        {
            //需要Cast, 因为Environment.NewLine不是字面量。
            var s1 = "Hi." + Environment.NewLine + "Siri.";
            //需要Cast, 因为显示使用了+=操作符拼接字符串。
            var s2 = "Hi.";
            s2 += "Siri.";
            //不会编译, 因为从未使用过s3。
            //var s3 = "Hi." + " " + "Siri.";
            //不需要Cast
            var s4 = "Hi." + " " + "Siri.";
            Console.WriteLine(s4);
            //先调用int.ToString, 再用Cast连接
            var s5 = s4 + 1;
            Console.WriteLine(s5);
        }
        #endregion

        #region Contravariant
        static void Contravariant()
        {
            IContravariance<Object2, Object2> service1 = new Contravariance();

            IContravariance<Object3, Object1> service2 = service1;
            IContravariance<Object3, Object2> service21 = service1;
            IContravariance<Object2, Object1> service22 = service1;

            var obj2 = new Object2();
            var obj3 = new Object3();
            service1.GetValue(obj2);
            service1.GetValue(obj3);
            service2.GetValue(obj3);

            //值类型无法使用转换
            //IContravariance<int, int> service3 = new Contravariance1();
            //IContravariance<int, object> service4 = service3;

            //FCL
            Func<int, int> func = (_) => { return _; };


            var obj = new Object();
            int i = (int)obj;
            //无法编译
            //var cls = new Object1();
            //int i = (int)cls;
            //值类型不可用as操作。
            //int j = obj is int;

        }
        #endregion

        #region GenericField
        static void GenericField()
        {
            var i = new GenericModel<int>();
            GenericModel<int>.Count = 5;
            Console.WriteLine($"{nameof(GenericModel<int>)}: {GenericModel<int>.Count}, {nameof(GenericModel<string>)}: {GenericModel<string>.Count}");

            //GenericStaticConstrator
            GenericModel<IList>.Count = 1;
            GenericModel<int[]>.Count = 1;
        }
        #endregion

        #region Hashcode
        static void Hashcode()
        {
            var key1 = new EventKey();
            var key2 = new EventKey();
            //为什么哈希码会不一样？
            Console.WriteLine($"Key1 code: {key1.GetHashCode()}, Key2 code: {key2.GetHashCode()}");
        }
        #endregion

        #region DynamicInvoke
        static void DynamicInvoke()
        {
            new DynamicDelegateSet().Invoke();
        }
        #endregion

        #region ActivatorCreate
        static void ActivatorCreate()
        {
            var type = typeof(ActivatorModel);
            //自动进行类型安全的匹配
            var obj1 = Activator.CreateInstance(type);
            var obj2 = Activator.CreateInstance(type, 1);
            var obj3 = Activator.CreateInstance(type, "");

            //隐式类型转换的可以调用得到。
            ushort i = 1;
            var obj4 = Activator.CreateInstance(type, i);
            //调用异常
            //double d = 1D;
            //var obj5 = Activator.CreateInstance(type, d);

            var t = typeof(IList<>);
            var obj = Activator.CreateInstance(t);
        }
        #endregion

        #region PrintFormat
        static void PrintFormat()
        {
            Console.WriteLine("{0}， {1,4}, {2}, {3}", 1, 2, 3, 4, 5);
        }
        #endregion

        #region UsedTime
        static void UsedTime()
        {
            const int perfCount = 100000000;
            //.NET Core比.NET Framework快
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

        #region Generic
        static void Generic()
        {
            var array = new EventModel[10];
            array[1] = new EventModel();
        }
        #endregion

        #region Eve
        static event EventHandler eve;
        static void Eve()
        {
            //eve += (_, e) => { Console.WriteLine("Hehe"); };
            //eve?.Invoke(null, EventArgs.Empty);
            //eve(null, EventArgs.Empty);

            var eventHandler = new EventModel();
            eventHandler.Event0 += (_, e) =>
            {
                Console.WriteLine("Hello!");
            };
            eventHandler.Event0 += (_, e) =>
            {
                Console.WriteLine("Halo!");
            };
            eventHandler.Run();

            //事件信息
            System.Reflection.EventInfo eventInfo = typeof(EventModel).GetEvent("Event0");
            Console.WriteLine("EventInfo: ");
            Console.WriteLine($"\t        Info: {eventInfo}");
            Console.WriteLine($"\t   AddMethod: {eventInfo.AddMethod}");
            Console.WriteLine($"\tRemoveMethod: {eventInfo.RemoveMethod}");
            Console.WriteLine($"\t RaiseMethod: {eventInfo.RaiseMethod}");
        }
        #endregion

        #region TupleDemo
        static void TupleDemo()
        {
            var t1 = new Tuple<int, string>(5, "Halo!");
            Console.WriteLine(t1.ToString());

            //var t2 = new Tuple<int, string>(5, "Halo!");
            var t2 = Tuple.Create(5, "Halo!");
            Console.WriteLine(t2.ToString());

            var t4 = t2.ToValueTuple();
            var t3 = (5, "Halo!");
            Console.WriteLine($"t1==t2? {t1 == t2}");
            Console.WriteLine($"t1 Equals t2? {t1.Equals(t2)}");
            //Console.WriteLine($"t3==t2? {t3 == t2}"); //不能编译
            Console.WriteLine($"t3 Equals t2? {t3.Equals(t2)}");
            Console.WriteLine($"t3 Equals t4? {t3.Equals(t4)}");
            Console.WriteLine($"t3==t4? {t3 == t4}");

            //Tuple
            var minmax = new TupleHelper().MinMax(1, 2);
            //值
            var maxmin = new TupleHelper().MaxMin(1, 2);

            Console.WriteLine($"Item1: {minmax.Item1}, Item2: {minmax.Item2}");
            Console.WriteLine($"Max: {maxmin.Max}, Min: {maxmin.Min}");

            //多于8个Tuple
            var t11 = Tuple.Create(8, 9);
            var t10 = Tuple.Create(1, "2", "3", 4, 5, 6, 7, t11);

            Console.WriteLine(t10.Rest);
            Console.WriteLine(t10.Rest.Item1);
            Console.WriteLine(t10);
        }
        #endregion

        #region GetFileInfo
        static void GetFileInfo()
        {
            new FolderHelper().GetChangedFileInMyDocument();
        }
        #endregion

        #region Anonymous
        static void Anonymous()
        {
            //匿名
            var v = new { Name = "Brush", Price = 5.50D };
            Console.WriteLine($"Name: {v.Name}, Price: {v.Price}");
            Console.WriteLine($"v: {v.ToString()}");
            Console.WriteLine($"Type: {v.GetType()}");

            var j = new { Name = "Towel", Price = 7.50D };
            Console.WriteLine($"j: {j}");
            Console.WriteLine($"Type: {j.GetType()}");

            var Price = 7.50D;
            var k = new { Name = "Towel", Price };
            Console.WriteLine($"v == j? {v == j}");
            Console.WriteLine($"v Equals j? {v.Equals(j)}");
            Console.WriteLine($"k == j? {k == j}");
            Console.WriteLine($"k Equals j? {k.Equals(j)}");  //True
            //每次运行生成的哈希码都不一样。
            Console.WriteLine($"Hashcode, v:{v.GetHashCode()}, j:{j.GetHashCode()}, k:{k.GetHashCode()}");

            v = j;
            Console.WriteLine($"v: {v}");
            Console.WriteLine($"v Equals j? {v.Equals(j)}");
            Console.WriteLine($"Hashcode, v:{v.GetHashCode()}, j:{j.GetHashCode()}, k:{k.GetHashCode()}");

            var array = new[]
            {
                new { Name = "j", Price = 4.5D },
                new { Name = "k", Price = 5D },
            };
        }
        #endregion

        #region Params
        static void Params()
        {
            //MyParams();//params不优先重载
            MyParams(null);
            //MyParams(param: default); //default = null
        }
        static void MyParams(params int[] param)
        {
            var length = param == null ? -1 : param.Length;
            Console.WriteLine($"IsNull: {param == null}, Length: {length}");
        }

        //签名是相同的
        //static void MyParams(int[] param)
        //{

        //}
        #endregion

        #region Box
        static void Box()
        {
            int i = 5;
            object obj = i;

            //var obj = new Object();
            //SwapHelper.Box(ref obj);

            //int i = 5, j = 6;
            //SwapHelper.Swap(ref i, ref j);
        }
        #endregion

        #region Swap
        static void Swap()
        {
            int i = 5;
            int j = 10;
            SwapHelper.Swap(ref i, ref j);
            Console.WriteLine($"i: {i}, j: {j}");


            var r1 = new Swapper(1);
            var r2 = new Swapper(2);

            SwapHelper.Swap(ref r1, ref r2);
            Console.WriteLine($"r1: {r1.Value}, r2: {r2.Value}");

            var v1 = new SwapperValue(1);
            var v2 = new SwapperValue(2);
            SwapHelper.Swap(ref v1, ref v2);
            Console.WriteLine($"v1: {v1.Value}, v2: {v2.Value}");

            Action<int> action1 = i => Console.WriteLine(i);
            Action<int> action2 = i => Console.WriteLine(i + 1);
            SwapHelper.Swap(ref action1, ref action2);
            action1(1);
            action2(1);

            Interlocked.CompareExchange<Swapper>(ref r1, r2, null);
            Console.WriteLine($"r1: {r1.Value}, r2: {r2.Value}");
            r2 = Interlocked.Exchange(ref r1, r2);
            Console.WriteLine($"r1: {r1.Value}, r2: {r2.Value}");
        }
        #endregion

        #region ExtensionMethod
        static void ExtensionMethod()
        {
            var type = typeof(StringBuilderExtension);
            //var type = typeof(InvisibleAttributeTarget<int>);

            var staticMethods = type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            foreach (var m in staticMethods)
            {
                Console.WriteLine($"Static Method: {m.Name}");
            }
            var instanceMethods = type.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var m in instanceMethods)
            {
                Console.WriteLine($"Instance Method: {m.Name}");
            }
        }
        #endregion

        #region ConstStatic
        static void ConstStatic()
        {
            Console.WriteLine($"S1: {Const1.S1}, C1: {Const1.C1}");
        }
        #endregion

        #region OptionalParameter1
        static void OptionalParameter1()
        {
            new Class007().Print();
        }
        #endregion

        #region OptionalParameter
        static void OptionalParameter()
        {
            OptionalParameter(5);

            OptionalParameter(5, s: "halo");

            OptionalParameter(5, s: "halo", param: default);

            Const.Method();
        }

        static void OptionalParameter(int i, int j = 5, string s = "hello", Guid guid = new Guid(), Guid guid1 = default(Guid), params int[] param)
        {
            //可选参数的默认值必须是常量，在编译时确定。
        }
        delegate void MyDele001x5(int i = 5);
        #endregion

        #region Version
        static void Version()
        {
            var version = new Version(5656450, 51, 51, 500000);
            Console.WriteLine(version);
        }
        #endregion

        #region PartialMethod
        static void PartialMethod()
        {
            //var pc = new PartialClass();
            //pc.Run();
            //pc.Print();

            new C1().Run();
        }
        #endregion

        #region DefaultArray
        static void DefaultArray()
        {
            var array = default(string[]);
            Console.WriteLine($"IsNull: {array == null}");
            Console.WriteLine($"  Type: {typeof(string[]).FullName}");
        }
        #endregion

        #region Split
        static void Split()
        {
            const char c = '0';
            const string s = "8774x0";

            //var sp = s.Split(c);
            //Console.WriteLine($"Total Length: {sp.Length}");
            //foreach (var item in sp)
            //{
            //    Console.WriteLine($"Length: {item.Length}, Value: {item}");
            //}

            //var sp = s.Split(c, -1);
            Action<Object> del = _ => s.Split(c, -1);
            del.InvokeAndCatch<ArgumentOutOfRangeException>(null);

            var sp = s.Split(c, (StringSplitOptions)(-1));
            del = _ => s.Split(c, (StringSplitOptions)(-1));
            del.InvokeAndCatch<ArgumentException>(null);
        }
        #endregion

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
            //var cc2 = new C22();
            RuntimeTypeHandle t = typeof(C22).TypeHandle;
            RuntimeHelpers.RunClassConstructor(t);

            Console.WriteLine(C22.Value);
            Console.WriteLine("{0}", Type.GetTypeFromHandle(t));
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
                while (len > 0)
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
            //for (int i = 0; i < data1.Length; i++)
            //while (true)
            //{
            //    data1[i] = data[begin + i];
            //}
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
