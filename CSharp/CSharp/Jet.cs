using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Jet
    {
        private readonly string name;
        public Jet(string name)
        {
            this.name = name;
        }

        public void PrintName()
        {
            Console.WriteLine(this.name);
        }
    }
}
