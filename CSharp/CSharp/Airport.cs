using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    public class Airport
    {
        public IList<IAirbus> Buses{ get; set; }

        public Airport(IEnumerable<IAirbus> buses)
        {
            this.Buses = buses.ToList();
        }

        public void Run()
        {
            for (int i = 0; i < Buses.Count; i++)
            {
                Console.WriteLine($"Bus No.{i + 1} start working.");
            }
        }
    }
}
