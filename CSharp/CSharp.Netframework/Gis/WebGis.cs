using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.NetFramework.Gis
{
    public class WebGis
    {
        GisService.MapService service = new GisService.MapService();
        public string GetLocation(double dLon, double dLat)
        {
            var sLoc = service.QueryAllLayerByPoint(dLon, dLat);
            Console.WriteLine(sLoc);
            return sLoc;
        }
    }
}
