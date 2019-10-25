﻿using CSharp.Entity;
using CSharp.File;
using CSharp.json;
using CSharp.xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;

namespace CSharp
{
    class Program
    {
        public static bool IsTrue { get; set; }
        static void Main(string[] args)
        {
            try
            {
                ImageFormat();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
        #region ImageFormat
        static void ImageFormat()
        {
            new ImageFormatSave().SaveImage();
        }
        #endregion

        #region JTB
        static void JTB()
        {
            var s = new JTB.J808Command().GetDescription();
            Console.WriteLine(s);
        }
        #endregion

        #region StringFormat
        static void StringFormat()
        {
            // Define cultures whose formatting conventions are to be used.
            CultureInfo[] cultures = { CultureInfo.CreateSpecificCulture("en-US"),
                                 CultureInfo.CreateSpecificCulture("fr-FR"),
                                 CultureInfo.CreateSpecificCulture("es-ES") };
            string[] specifiers = { "G", "C", "D4", "E2", "F", "N", "P", "X2" };
            ushort value = 22042;
            foreach (string specifier in specifiers)
            {
                foreach (CultureInfo culture in cultures)
                    Console.WriteLine("{0,2} format using {1} culture: {2, 16}",
                                      specifier, culture.Name,
                                      value.ToString(specifier, culture));
                Console.WriteLine();
            }
        }
        #endregion

        #region SourceManager
        static void SourceManager()
        {
            //new CSharp.Manifest.Class1().Hello();
            new Resource.ImageResource().GetImage();
        }
        #endregion

        #region SelectXmlElement
        static void SelectXmlElement()
        {
            //var s = "<Destinations><Poi><PoiName>测试</PoiName><Longitude>119.290000</Longitude><Latitude>26.030000</Latitude></Poi><Poi><PoiName>测试1</PoiName><Longitude>119.270000</Longitude><Latitude>26.010000</Latitude></Poi><Poi><PoiName>测试1</PoiName><Longitude>119.270000</Longitude><Latitude>26.010000</Latitude></Poi></Destinations>";
            //var xElement = XElement.Parse(s);
            //foreach (var item in xElement.Elements("Poi"))
            //{
            //    Console.WriteLine(item);
            //}
            var lst = new List<string>
            {
                "1",
                "2",
                "3",
            };

            //以某符号作分隔。
            var c = ';';
            Console.WriteLine(lst.Aggregate((prev, cur) => $"{prev}{c}{cur}"));
        }
        #endregion

        #region LogicOr
        static void LogicOr()
        {
            var u = 0x2000;
            u |= 0x8000;
            Console.WriteLine(u.ToString("X"));
        }
        #endregion

        #region SplitAndParse
        static void SplitAndParse()
        {
            var trimChars = new char[] { '0', 'x', 'X' };
            var i = int.Parse("0x0EF1".TrimStart(trimChars), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine(i);
            return;

            var sFilterAlarm = "0x0001;0x0002;0x0003";
            var filterAlarm =
                sFilterAlarm
                .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s, System.Globalization.NumberStyles.HexNumber))
                .ToList();

            foreach (var alarm in filterAlarm)
            {
                Console.WriteLine(alarm);
            }
        }
        #endregion

        #region BuildXmlData
        static void BuildXmlData()
        {
            new BuildXmlData().Build();
        }
        #endregion

        #region XmlResolve
        private static void XmlResolve()
        {
            var s = xmlParam_1002X59;
            var xDoc = new XmlDocument();
            xDoc.LoadXml(s);

            XmlNodeList xList = xDoc.SelectNodes("//PlatItem");
            foreach (XmlElement element in xList)
            {
                if (element.GetAttribute("url") != "222.76.211.247:8088")
                {
                    continue;
                }

                //这样查会顺延到下个节点去找。
                //var xlist = element.SelectSingleNode("//UserList");
                //Console.WriteLine(xlist.InnerXml);

                XmlNode xUserLst = null;
                foreach (XmlNode node in element.ChildNodes)
                {
                    if (node.Name == "UserList")
                    {
                        xUserLst = node;
                        break;
                    }
                }
                if (xUserLst != null)
                {
                    Console.WriteLine(xUserLst.InnerXml);
                }
                else
                {
                    Console.WriteLine("404 Not found");
                }
            }
        }

        private const string xmlParam_1002X59 = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<Param>
  <PlatList>
    <PlatItem url=""222.76.211.247:8088"">
      <IsShowSystemName>True</IsShowSystemName>
      <SystemName>锐讯卫星定位综合服务平台</SystemName>
      <LogoUrl>
      </LogoUrl>
      <BgImgUrl>
      </BgImgUrl>
      <FontColor>
      </FontColor>
      <LoginButtonBgColor>
      </LoginButtonBgColor>
      <LoginButtonFontColor>
      </LoginButtonFontColor>
      <Ip>222.76.211.247</Ip>
      <Port>8088</Port>
      <UserList>
        <User id=""admin"" pwd=""8B4D62C133480463636CE0EBBFAA1C47"" />
      </UserList>
    </PlatItem>
    <PlatItem url=""124.225.212.14:8088"" >
      <Ip>124.225.212.14</Ip>
      <Port>8088</Port>
      <IsShowSystemName>True</IsShowSystemName>
      <SystemName>佳创天狮GPS智能管理平台</SystemName>
      <BgImgUrl>
      </BgImgUrl>
      <FontColor>
      </FontColor>
      <LoginButtonBgColor>
      </LoginButtonBgColor>
      <LoginButtonFontColor>
      </LoginButtonFontColor>
      <LogoUrl>
      </LogoUrl>
    </PlatItem>
    <PlatItem url=""192.168.3.99:8088"" >
      <Ip>192.168.3.99</Ip>
      <Port>8088</Port>
      <IsShowSystemName>True</IsShowSystemName>
      <SystemName>CGO8主动安全监控平台</SystemName>
      <BgImgUrl>
      </BgImgUrl>
      <FontColor>
      </FontColor>
      <LoginButtonBgColor>
      </LoginButtonBgColor>
      <LoginButtonFontColor>
      </LoginButtonFontColor>
      <LogoUrl>
      </LogoUrl>
      <UserList>
        <User id=""admin"" pwd=""000000"" />
        <User id= ""admin"" pwd=""8B4D62C133480463636CE0EBBFAA1C47"" />
        <User id=""admin"" pwd=""8B4D62C133480463636CE0EBBFAA1C47"" />
      </UserList>
    </PlatItem>
  </PlatList>
  <LastUrl>124.225.212.14:8088</LastUrl>
  <LastLogonUser>admin</LastLogonUser>
  <LastLogonPwd>8B4D62C133480463636CE0EBBFAA1C47</LastLogonPwd>
</Param>";
        #endregion

        #region EntityToXml
        private static void EntityToXml()
        {
            new EntityToXml().SerializeWithNamedRoot();
        }
        #endregion

        #region GetType
        private static void GetNullType()
        {
            Intf intf = new Imple();
            Console.WriteLine(intf.GetType());
        }
        #endregion

        #region Split
        private static void Split()
        {
            var input = "abcd,ab,c,ab,d,a,bc,a,b,,acd,";
            foreach (var item in GetArray(input))
            {
                Console.WriteLine(item);
            }
        }

        private static IEnumerable<string> GetArray(string input)
        {
            var current = 0;
            while (current <= input.Length - 4)
            {
                var output = input.Substring(current, 4);
                yield return output;
                current += 5;
            }
        }
        #endregion

        #region Math
        private static void Math()
        {
            var i1 = System.Math.Ceiling(5.5D);
            var i2 = System.Math.Ceiling(6.1);
            var i3 = System.Math.Ceiling(6.9);
            Console.WriteLine($"i1: {i1}-5, i2: {i2}-6, i3: {i3}-6");
        }
        #endregion

        #region SizeMeasure
        private static void SizeMeasure()
        {
            uint u = 0;
            var b1 = Marshal.SizeOf(u);
            Console.WriteLine($"Size of uint: {b1}");
            int i = 0;
            var b2 = Marshal.SizeOf(i);
            Console.WriteLine($" Size of int: {b2}");
            long l = 0;
            var b3 = Marshal.SizeOf(l);
            Console.WriteLine($"Size of long: {b3}");

            //Error
            //var x = sizeof(Pilot);
            //Error
            //Console.WriteLine($"Size of Polot: {Marshal.SizeOf(typeof(Pilot))}");
            //Error： cant be marshaled
            //var pilot = new Pilot();
            //var b4 = Marshal.SizeOf(pilot);
            //Console.WriteLine($"Size of pilot: {b4}");
        }
        #endregion

        #region InterfaceTest
        private static void InterfaceTest()
        {
            Intf i = new Imple();
            Console.WriteLine(i.ToString());
        }
        #endregion
    }

    public interface Intf
    {
        int MyProperty { get; set; }
    }

    public class Imple : Intf
    {
        public int MyProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
