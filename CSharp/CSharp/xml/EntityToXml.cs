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
        #region Serialize
        public EntityToXml Serialize()
        {
            //return this.LoadAsDataSet();

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
                Drivers = new[] { "Jack", "Mick", "Jodan" },
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

                //                str = @"<data>
                //    <Code>503</Code>
                //    <Name>GGAirport</Name>
                //    <Planes>
                //        <Airport>
                //            <Id>1</Id>
                //            <Name>J20</Name>
                //            <Flag>JE</Flag>
                //            <MaxSpeed>625</MaxSpeed>
                //            <Pilot>
                //                <Identity>HuangHe</Identity>
                //                <Weight>63.5</Weight>
                //                <Height>175.4</Height>
                //            </Pilot>
                //        </Airport>
                //    </Planes>
                //    <Drivers>
                //        <Driver>Jack</Driver>
                //        <Driver>Mick</Driver>
                //        <Driver>Jodan</Driver>
                //    </Drivers>
                //</data>";

                var xEle = XElement.Parse(str);
                Console.WriteLine(xEle);
            }
            return this;
        }
        #endregion

        #region LoadAsDataSet
        public EntityToXml LoadAsDataSet()
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Data><PlatProtocolName>HEWL</PlatProtocolName></Data>";

            var ds = new DataSet();
            StringReader sr = new StringReader(xml);
            ds.ReadXml(sr);

            Console.WriteLine($"Tables in DataSet: {ds.Tables.Count}");
            return this;
        }
        #endregion

        #region SerializeWithNamedRoot
        public EntityToXml SerializeWithNamedRoot()
        {
            var pilot = new Pilot
            {
                Height = 170.4,
                Weight = 60.4,
                Identity = "50IHX",
            };
            var airport = new Airport
            {
                Flag = "X1H3",
                Id = 1,
                MaxSpeed = 30.5D,
                Name = "bilibi airport",
                Pilot = pilot,
            };
            var xDoc = new XDocument();
            var memory = new MemoryStream();

            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.OmitXmlDeclaration = true;
            //settings.Indent = true;
            //settings.IndentChars = "    ";
            //settings.NewLineChars = "\r\n";
            //settings.Encoding = Encoding.UTF8;
            using (var writer = XmlWriter.Create(memory))
            {
                var serializer = new XmlSerializer(typeof(Airport));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(writer, airport, ns);

                //xDoc.CreateReader();

                memory.Position = 0;
                StreamReader sr = new StreamReader(memory);
                string str = sr.ReadToEnd();

                Console.WriteLine(str);
            }
            return this;
            //return this.Serialize();
        }
        #endregion

    }
}
