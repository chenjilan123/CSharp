using CSharp.Entity;
using CSharp.json;
using CSharp.xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CSharp
{
    class Program
    {
        public static bool IsTrue { get; set; }
        static void Main(string[] args)
        {
            try
            {
                //Split();

                //Console.WriteLine(IsTrue);
                //SizeMeasure();

                //new EntityToXml()
                //    .Serialize();


                //new ResolveXml().Run();
                //return;

                //new ParseString().Run();
                //return;

                //EntityToXml();
                //return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

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
