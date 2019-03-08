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
        private const int MaxPostion1 = 1000;  //        57.08      17.1            9.2
        private const int MaxPostion2 = 5000;
        private const int MaxPostion3 = 10000;
        private const int MaxPostion4 = 50000;
        private const int MaxPostion5 = 100000;
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
            //PrintTable(table);
            //更新位置
            Console.WriteLine($"更新位置, 位置数：{table.Rows.Count}");
            UpdatePosition(table);
            //PrintTable(table);


        }

        private void PrintTable(DataTable table)
        {
            foreach (DataRow dr in table.Rows)
            {
                Console.WriteLine($"Lon: {dr[Lon]}, Lat: {dr[Lat]}, Position1: {dr[Position1]}, Position2: {dr[Position2]}");
            }
        }

        private void UpdatePosition(DataTable table)
        {
            //初始化Gis服务
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Begin initialize gis service");
            GisServerHelper.Initialize();
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} End initialize gis service");

            //Print speed
            var updateCnt = 0;
            var lastCnt = 0;
            var isStop = false;
            Task.Factory.StartNew(() =>
            {
                var avgSpeed = 0D;
                var curSpeed = 0D;
                var begin = DateTime.Now;
                var mode = IsSync ? "Sync" : "Async";
                //Print sync speed
                while (true)
                {
                    if (!isStop)
                    {
                        avgSpeed = ((double)updateCnt) / (DateTime.Now - begin).TotalSeconds;
                        curSpeed = updateCnt - lastCnt;
                    }
                    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}  {mode} get position AvgSpeed: {avgSpeed.ToString("0.0")} /s, CurrentSpeed: {curSpeed.ToString("0.0")} /s, count: {updateCnt}");
                    lastCnt = updateCnt;
                    Thread.Sleep(1000);
                }
            });

            var sw = Stopwatch.StartNew();
            //Sync
            if (IsSync)
            {
                Console.WriteLine("Begin get position sync");
                foreach (DataRow dr in table.Rows)
                {
                    dr[Position2] = GisServerHelper.QueryAllLayerByPoint1((double)dr[Lon], (double)dr[Lat]);
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
                var result = Parallel.ForEach(table.AsEnumerable(), dr =>
                {
                    try
                    {
                        if (updateCnt % 2 == 1)
                        {
                            dr[Position1] = GisServerHelper.QueryAllLayerByPoint1((double)dr[Lon], (double)dr[Lat]);
                        }
                        else
                        {
                            dr[Position1] = GisServerHelper.QueryAllLayerByPoint2((double)dr[Lon], (double)dr[Lat]);
                        }
                        //lock (lck)
                        //{
                        updateCnt++;
                        //}
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"GetPosition Error: {ex.ToString()}");
                    }
                });
                Console.WriteLine("End get position async");
                Console.WriteLine($"Async time: {sw.Elapsed.TotalSeconds}s");
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
                        if (sr.EndOfStream || table.Rows.Count >= MaxPostion1)
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
