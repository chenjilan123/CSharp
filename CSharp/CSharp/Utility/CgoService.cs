using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Utility
{
    public class CgoService : IGisService
    {
        public string GetPosition(double dLon, double dLat)
        {
            return "北京";
        }
    }
}
