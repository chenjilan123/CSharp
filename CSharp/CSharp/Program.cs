﻿using CSharp.Entity;
using CSharp.File;
using CSharp.json;
using CSharp.JTB;
using CSharp.Pb;
using CSharp.xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
                ProtocolBuffer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
        #region ProtocolBuffer
        static void ProtocolBuffer()
        {
            new ProtocolBufferProgram().Run();
        }
        #endregion

        #region ImageFormat
        static void ImageFormat()
        {
            new ImageFormatSave().SaveImage();
        }
        #endregion
        
        #region Code
        static void Code()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var c = "4D41524B5F5354415455533A3D305830303B4556454E545F545950453A3D305830323B414C41524D5F47524144453A3D305830323B41484541445F53504545443A3D305830303B41484541445F44495354414E43453A3D305830303B444956455247455F545950453A3D305830313B524F41445F5349474E5F545950453A3D305830303B524F41445F5349474E5F444154413A3D305830303B53504545443A3D305835303B414C5449545544453A3D303B4C4F4E4749545544453A3D3131343530373231303B4C415449545544453A3D33373939353630373B56454849434C455F5354415455533A3D313032353B414C41524D5F49443A3D30583330344234413437343534373433313931313032313632353235303030353030000101000100000006D33DCA0243C4570050000000000000000100000000000000000000";
            var data = StrToToHexByte(c);
            //foreach (var b in data)
            //{
            //    if (b == 0x5E || b == 0x5A)
            //    {
            //        Console.WriteLine("hehe");
            //        return;
            //    }
            //}
            var gb2312 = Encoding.GetEncoding("GB2312");
            var s = gb2312.GetString(data);
            Console.Write(s);
            Console.WriteLine();
            var desc = "MARK_STATUS:=0X00;EVENT_TYPE:=0X02;ALARM_GRADE:=0X02;AHEAD_SPEED:=0X00;AHEAD_DISTANCE:=0X00;DIVERGE_TYPE:=0X01;ROAD_SIGN_TYPE:=0X00;ROAD_SIGN_DATA:=0X00;SPEED:=0X50;ALTITUDE:=0;LONGITUDE:=114507210;LATITUDE:=37995607;VEHICLE_STATUS:=1025;ALARM_ID:=0X304B4A47454743191102162525000500";
            var dec = gb2312.GetBytes(desc);
            foreach (var b in dec)
            {
                Console.Write(b.ToString("X2"));
            }
        }

        /// <summary> 
        /// 字符串转16进制字节数组 
        /// </summary> 
        /// <param name="hexString"></param> 
        /// <returns></returns> 
        static byte[] StrToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString = "0" + hexString;
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        #endregion

        #region JTB
        static void JTB_()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //Encoding Gb2312 = Encoding.GetEncoding("gb2312");
            //var s1 = "哈";
            //var s2 = "哈哈";
            //var s1Len = Gb2312.GetBytes(s1).Length;
            //var s2Len = Gb2312.GetBytes(s2).Length;
            //Console.WriteLine(s1.PadRight(6 - s1Len + s1.Length, '1'));
            //Console.WriteLine(s2.PadRight(6 - s2Len + s2.Length, '1'));
            //return;

            JTB.J808ProtocolRulerProvider.Instance.Load();

            //var s = new JTB.J808Command().GetDescription();
            //Console.WriteLine(s);

            //var command = JTB.J808ProtocolRulerProvider.Instance.GetJ808V2013Command(33029);
            //foreach (var field in command.Fields)
            //{
            //    Console.WriteLine($"Type: {field.Type}, Name: {field.Name}, Length: {field.Length}");
            //}

            var command = new J808Command
            {
                Property = 0x8010,
                Phone = "13800000050",
            };
            var serializer = new JTBSerializer();

            //驾驶员身份信息采集上报
            var data = new byte[]
            {
                //状态
                0x01,
                //时间
                0x19, 0x10, 0x29, 0x14, 0x06, 0x05,
                //IC卡读取结果
                0x00,
                //驾驶员姓名长度
                0x06,
                //驾驶员姓名
                0xC7, 0xF1, 0xD4, 0xC6, 0xB7, 0xC9,
                //从业资格证编码
                0x33, 0x35, 0x30, 0x31, 0x32, 0x33, 0x34, 0x34, 0x34, 0x30, 0x30, 0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                //发证机构名称长度
                0x0C,
                //发证机构名称
                0xB8, 0xA3, 0xBD, 0xA8, 0xCA, 0xA1, 0xBD, 0xBB, 0xCD, 0xA8, 0xCC, 0xFC,
                //证件有效期
                0x20, 0x24, 0x10, 0x01
            };
            command.Id = 0x0702;

            //终端通用应答
            data = new byte[]
            {
                0xF4,
                0xF3,
                0x55,
                0x44,
                0x00
            };
            command.Id = 0x0001;

            //终端心跳
            data = new byte[0];
            command.Id = 0x0002;

            var cmd = serializer.Deserialize(command, data);
            Console.WriteLine(cmd.GetDescription());
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
