using CSharp.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace CSharp.xml
{
    public class BuildXmlData
    {
        public void Build()
        {
            // <Data />
            //var xEle = new XElement("Data");

            //<Data><Set /></Data>
            //var xEle = new XElement("Data");
            //xEle.Add(new XElement("Set"));
            // 等价于=>
            //var xEle = new XElement("Data", new XElement("Set"));

            //<Data><Set><Member>1</Member><Member /></Set></Data>
            var xEle = new XElement("Data"
                , new XElement("Set"
                    , new XElement("Member", 1)
                    , new XElement("Member", true)));


            
            Console.WriteLine($"Unformatted: \n{xEle.ToString(SaveOptions.DisableFormatting)}");
            Console.WriteLine($"  formatted: \n{xEle}");
        }
    }
}
