using CSharp.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.BestPractice
{
    public class GeoLocation
    {                                          //        Async(s)   Speed    Sync   Speed
        //private const int MaxPostion = 10;  //        57.08      17.1            9.2
        //private const int MaxPostion = 1000;
        //private const int MaxPostion = 5000;
        //private const int MaxPostion = 10000;
        //private const int MaxPostion = 50000;
        private static int MaxPostion = 100000;
        private static int GisServerNumber = 1;
        private const bool IsSync = false;
        private const string Position1 = "Position1";
        private const string Position2 = "Position2";
        private const string Lon = "Lon";
        private const string Lat = "Lat";

        public void GetPosition()
        {
            var table = new DataTable();
            table.Columns.Add("Position1");
            table.Columns.Add("Position2");
            table.Columns.Add("Lon", typeof(double));
            table.Columns.Add("Lat", typeof(double));

            //加载经纬度
            LoadLonLat(table).Wait();
            //输入参数
            InputParam();
            //PrintTable(table);
            //更新位置
            Console.WriteLine($"更新位置, 位置数：{table.Rows.Count}");
            Console.WriteLine($"         数据量：{MaxPostion}");
            Console.WriteLine($"         实例数：{GisServerNumber}");
            UpdatePosition(table);
            //PrintTable(table);

            SavePosition(table).Wait();
        }

        private void InputParam()
        {
            while (true)
            {
                Console.WriteLine("请输入数据量: ");
                var s = Console.ReadLine();
                if (int.TryParse(s, out MaxPostion))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("输入参数有误, 请重新输入。");
                }
            }
            while(true)
            {
                Console.WriteLine("请输入实例数: ");
                var s = Console.ReadLine();
                if (int.TryParse(s, out GisServerNumber))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("输入参数有误, 请重新输入。");
                }
            }
        }

        private async Task SavePosition(DataTable table)
        {
            using (var fs = new FileStream("Position/resolved.txt", FileMode.OpenOrCreate, FileAccess.Write))
            using (var sr = new StreamWriter(fs))
            {
                foreach (DataRow dr in table.Rows)
                {
                    var lon = (double)dr[Lon];
                    var lat = (double)dr[Lat];
                    await sr.WriteLineAsync($"{lon.ToString("0.000000")} {lat.ToString("0.000000")} {dr[Position1]} {dr[Position2]}");
                }
            }
        }

        private void PrintTable(DataTable table)
        {
            foreach (DataRow dr in table.Rows)
            {
                var lon = (double)dr[Lon];
                var lat = (double)dr[Lat];
                Console.WriteLine($"Lon: {lon.ToString("0.000000")}, Lat: {lat.ToString("0.000000")}, Position1: {dr[Position1]}, Position2: {dr[Position2]}");
            }
        }

        private void UpdatePosition(DataTable table)
        {

            //Print speed
            var updateCnt = 0;
            var lastCnt = 0;
            var isStop = false;
            Task.Factory.StartNew(() =>
            {
                //平均速度
                var avgSpeed = 0D;
                //最高速度
                var maxSpeed = 0D;
                //最低速度
                var minSpeed = Double.MinValue;
                //当前速度
                var curSpeed = 0D;
                var begin = DateTime.Now;
                var mode = IsSync ? "Sync" : "Async";
                //Print sync speed
                while (true)
                {
                    if (isStop)
                    {
                        break;
                    }
                    avgSpeed = ((double)updateCnt) / (DateTime.Now - begin).TotalSeconds;
                    curSpeed = updateCnt - lastCnt;
                    if (curSpeed > maxSpeed)
                    {
                        maxSpeed = curSpeed;
                    }
                    if (curSpeed < minSpeed || minSpeed == Double.MinValue)
                    {
                        minSpeed = curSpeed;
                    }
                    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}  {mode} get position AvgSpeed: {avgSpeed.ToString("0.0")} /s, CurrentSpeed: {curSpeed.ToString("0.0")} /s, MaxSpeed: {maxSpeed.ToString("0.0")}, count: {updateCnt}");
                    lastCnt = updateCnt;
                    Thread.Sleep(1000);
                }
            });

            var sw = Stopwatch.StartNew();
            //Sync
            if (IsSync)
            {
                var gisHelper = new GisServer();
                GisServer.Initialize(gisHelper);
                Console.WriteLine("Begin get position sync");
                foreach (DataRow dr in table.Rows)
                {
                    dr[Position2] = gisHelper.QueryAllLayerByPoint((double)dr[Lon], (double)dr[Lat]);
                    updateCnt++;
                }
                Console.WriteLine("End get position sync");
                Console.WriteLine($"Sync time: {sw.Elapsed.TotalSeconds}s");
            }
            //Async
            else
            {
                //sw.Restart();
                Console.WriteLine("Begin get position async");
                updateCnt = 0;
                object lck = new object();
                TableGisHelper.ResolveGeoAsync(ref table, Lon, Lat, Position1, GisServerNumber, () =>
                {
                    lock (lck)
                    {
                        updateCnt++;
                    }
                });


                Console.WriteLine("Start again");
                TableGisHelper.ResolveGeoAsync(ref table, Lon, Lat, Position1, GisServerNumber, () =>
                {
                    lock (lck)
                    {
                        updateCnt++;
                    }
                });
                //var rows = table.ToList();
                //var gisServers = new List<GisServer>();
                //for (int i = 0; i < GisServerNumber; i++)
                //{
                //    var gisServer = new GisServer();
                //    GisServer.Initialize(gisServer);
                //    gisServers.Add(gisServer);
                //}
                //var result = Parallel.ForEach(rows, dr =>
                //{
                //    try
                //    {
                //        var drNow = dr;
                //        var gisServerIndex = updateCnt % GisServerNumber;
                //        var gisServer = gisServers[gisServerIndex];
                //        var pos = gisServer.QueryAllLayerByPoint((double)drNow[Lon], (double)drNow[Lat]);
                //        lock (lck)
                //        {
                //            //这里必须有锁, 不然会有Index was out of range异常
                //            drNow[Position1] = pos;
                //        }
                //        lock (lck)
                //        {
                //            updateCnt++;
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine($"GetPosition Error: {ex.ToString()}");
                //    }
                //});
                Console.WriteLine("End get position async");
                Console.WriteLine($"Async time: {sw.Elapsed.TotalSeconds}s, Count Confim: {updateCnt}");
            }
            isStop = true;
        }

        private async Task LoadLonLat(DataTable table)
        {
            try
            {
                using (var fs = new FileStream("Position/lonlat.txt", FileMode.Open, FileAccess.Read))
                using (var sr = new StreamReader(fs))
                {
                    while (true)
                    {
                        var line = await sr.ReadLineAsync();
                        var (lon, lat) = GetLonLat(line);
                        var drNew = table.NewRow();
                        drNew[Lon] = lon;
                        drNew[Lat] = lat;
                        table.Rows.Add(drNew);
                        if (sr.EndOfStream || table.Rows.Count >= MaxPostion)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private (double, double) GetLonLat(string line)
        {
            var strs = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (!Double.TryParse(strs[0], out double result) || !Double.TryParse(strs[1], out result))
            {

                Console.WriteLine("Geo location parse error");
            }

            return (Double.Parse(strs[0]), Double.Parse(strs[1]));
        }
    }
}
