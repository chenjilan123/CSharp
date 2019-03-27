using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CSharp.Entity
{
    [XmlRoot("data")]
    public class Airports
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public List<Airport> Planes { get; set; }
    }
}
