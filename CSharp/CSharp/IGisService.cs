using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public interface IGisService
    {
        string GetPosition(double dLon, double dLat);
    }
}
