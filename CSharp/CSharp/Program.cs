﻿using CSharp.xml;
using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            new EntityToXml()
                .Serialize();
        }

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
