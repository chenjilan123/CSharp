using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Entity
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public double MaxSpeed { get; set; }
        public Pilot Pilot { get; set; }
    }
}
