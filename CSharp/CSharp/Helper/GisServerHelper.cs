using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp.Helper
{
    public class GisServerHelper
    {
        private static ServiceReference1.MapServiceSoapClient _WebGis1 = null;

        private static ServiceReference1.MapServiceSoapClient WebGis1
        {
            get
            {
                if (_WebGis1 == null)
                {
                    string sUrl = string.Format("{0}map/openlayers/mapservice.asmx", "http://58.42.249.183:9660/TopTPM/");
                    _WebGis1 = new ServiceReference1.MapServiceSoapClient(ServiceReference1.MapServiceSoapClient.EndpointConfiguration.MapServiceSoap, sUrl);
                    //_WebGis = new ServiceReference1.MapServiceSoapClient("MapServiceSoap", sUrl);
                    //_WebGis.Url = string.Format("{0}map/openlayers/mapservice.asmx", Variable.WebRoot);
                }
                return _WebGis1;
            }
        }
        private static ServiceReference1.MapServiceSoapClient _WebGis2 = null;

        private static ServiceReference1.MapServiceSoapClient WebGis2
        {
            get
            {
                if (_WebGis2 == null)
                {
                    string sUrl = string.Format("{0}map/openlayers/mapservice.asmx", "http://58.42.249.183:9660/TopTPM/");
                    _WebGis2 = new ServiceReference1.MapServiceSoapClient(ServiceReference1.MapServiceSoapClient.EndpointConfiguration.MapServiceSoap, sUrl);
                    //_WebGis = new ServiceReference1.MapServiceSoapClient("MapServiceSoap", sUrl);
                    //_WebGis.Url = string.Format("{0}map/openlayers/mapservice.asmx", Variable.WebRoot);
                }
                return _WebGis2;
            }
        }


        public static void Initialize()
        {
            QueryAllLayerByPoint1(118.5, 24.51);
        }

        public static string QueryAllLayerByPoint1(double lon, double lat)
        {
            //Thread.Sleep(500);
            //return $"{lon.ToString("0")}_{lat.ToString("0")}";
            try
            {
                var point = WebGis1.QueryAllLayerByPointAsync(lon, lat).Result;
                string[] sSplit = { ":::" };
                string[] sResult = point.Split(sSplit, StringSplitOptions.RemoveEmptyEntries);
                if (sResult.Length <= 1)
                {
                    return "未知";
                }
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
        public static string QueryAllLayerByPoint2(double lon, double lat)
        {
            //Thread.Sleep(500);
            //return $"{lon.ToString("0")}_{lat.ToString("0")}";
            try
            {
                var point = WebGis2.QueryAllLayerByPointAsync(lon, lat).Result;
                string[] sSplit = { ":::" };
                string[] sResult = point.Split(sSplit, StringSplitOptions.RemoveEmptyEntries);
                if (sResult.Length <= 1)
                {
                    return "未知";
                }
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
