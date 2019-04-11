using CSharp.Entity;
using CSharp.xml;
using System;
using System.Runtime.InteropServices;

namespace CSharp
{
    class Program
    {
        public static bool IsTrue { get; set; }
        static void Main(string[] args)
        {
            //Console.WriteLine(IsTrue);
            SizeMeasure();

            //new EntityToXml()
            //    .Serialize();
        }

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

        private static void InterfaceTest()
        {
            Intf i = new Imple();
            Console.WriteLine(i.ToString());
        }
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
