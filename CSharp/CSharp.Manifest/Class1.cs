using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly:AssemblyKeyFileAttribute("csharp.snk")]
namespace CSharp.Manifest
{
    public class Class1
    {
        public void Hello()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
