using CSharp.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CSharp.xml
{
    public class EntityToXml
    {
        public EntityToXml Serialize()
        {
            return this.LoadAsDataSet();

            var airports = new Airports()
            {
                Code = 503,
                Name = "GGAirport",
                Planes = new List<Airport>
                {
                    new Airport()
                    {
                        Id = 1,
                        Flag = "JE",
                        Name = "J20",
                        MaxSpeed = 625D,
                        Pilot = new Pilot()
                        {
                            Identity = "HuangHe",
                            Height = 175.4,
                            Weight = 63.5,
                        },
                    },
                },
            };
            MemoryStream Stream = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.UTF8;
            using (XmlWriter writer = XmlWriter.Create(Stream, settings))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Airports));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                //序列化对象  
                xml.Serialize(writer, airports, ns);
                Stream.Position = 0;
                StreamReader sr = new StreamReader(Stream);
                string str = sr.ReadToEnd();
                //sr.Dispose();
                //Stream.Dispose();
                Console.WriteLine(str);
                var xEle = XElement.Parse(str);
                Console.WriteLine(xEle);
            }
            return this;
        }

        public EntityToXml LoadAsDataSet()
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Data><PlatProtocolName>HEWL</PlatProtocolName></Data>";

            var ds = new DataSet();
            StringReader sr = new StringReader(xml);
            ds.ReadXml(sr);

            Console.WriteLine($"Tables in DataSet: {ds.Tables.Count}");
            return this;
        }
    }
}
