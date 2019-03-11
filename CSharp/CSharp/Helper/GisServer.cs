using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Helper
{
    public class GisServer
    {
        private ServiceReference1.MapServiceSoapClient _WebGis = null;

        private ServiceReference1.MapServiceSoapClient WebGis
        {
            get
            {
                if (_WebGis == null)
                {
                    string sUrl = string.Format("{0}map/openlayers/mapservice.asmx", "http://58.42.249.183:9660/TopTPM/");
                    _WebGis = new ServiceReference1.MapServiceSoapClient(ServiceReference1.MapServiceSoapClient.EndpointConfiguration.MapServiceSoap, sUrl);
                    //_WebGis = new ServiceReference1.MapServiceSoapClient("MapServiceSoap", sUrl);
                    //_WebGis.Url = string.Format("{0}map/openlayers/mapservice.asmx", Variable.WebRoot);
                }
                return _WebGis;
            }
        }

        private PositionCache _posCache = null;
        public GisServer()
        {
            _posCache = new PositionCache();
        }
    
        public static void Initialize(GisServer gisServer)
        {
            //初始化Gis服务
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} Begin initialize gis service");
            gisServer.QueryAllLayerByPoint(118.5, 24.51);
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} End initialize gis service");
        }

        public string QueryAllLayerByPoint(double lon, double lat)
        {
            //Thread.Sleep(500);
            //return $"{lon.ToString("0")}_{lat.ToString("0")}";
            try
            {
                string sPos = string.Empty;
                if (_posCache.TryGet(lon, lat, out sPos))
                {
                    Console.WriteLine("Get cached position");
                    return sPos;
                }
                var point = WebGis.QueryAllLayerByPointAsync(lon, lat).Result;
                string[] sSplit = { ":::" };
                string[] sResult = point.Split(sSplit, StringSplitOptions.RemoveEmptyEntries);
                if (sResult.Length <= 1)
                {
                    return "未知";
                }
                _posCache.TryAdd(lon, lat, sResult[1]);
                return sResult[1];
            }
            catch (System.Net.WebException webEx)
            {
                return "未知";
            }
            catch (Exception ex)
            {
                return "未知";
            }
        }
    }
}
