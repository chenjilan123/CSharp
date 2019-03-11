using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Helper
{
    public class TableGisHelper
    {
        public static void ResolveGeoAsync(ref DataTable table, string colLon, string colLat, string colPos, int instanceNum, Action append = null)
        {
            var rows = table.ToList();
            var count = 0;
            var lckSetValue = new Object();
            var gisServers = new List<GisServer>();
            for (int i = 0; i < instanceNum; i++)
            {
                var gisServer = new GisServer();
                gisServers.Add(gisServer);
            }
            var result = Parallel.ForEach(rows, dr =>
            {
                var drNow = dr;
                try
                {
                    var instanceIndex = count % instanceNum;
                    var gisServer = gisServers[instanceIndex];
                    var pos = gisServer.QueryAllLayerByPoint((double)drNow[colLon], (double)drNow[colLat]);
                    lock (lckSetValue)
                    {
                        drNow[colPos] = pos;
                        count++;
                    }
                    if (append != null)
                    {
                        append();
                    }
                }
                catch (Exception ex)
                {
                    //Log
                    Console.WriteLine($"Error: {ex.ToString()}");
                }
            });
        }
    }
}
