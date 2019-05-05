using CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp
{
    class Program
    {
        private const int PadLength = 30;
        static void Main(string[] args)
        {
            TestNullIEnumerable();
        }

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
