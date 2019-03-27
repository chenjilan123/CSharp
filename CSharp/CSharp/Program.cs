using CSharp.xml;
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
    }
}
