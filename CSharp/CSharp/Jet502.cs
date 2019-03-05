using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    public class Jet502
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public double Weight { get; set; }

        public string OilBox { get; set; }
        public string Code { get; set; }

        public Jet502(IOptions<JetOptions> options)
        {
            this.Name = "J20";
            this.OilBox = options.Value.OilBox;
            this.Code = options.Value.Code;
        }

        public void Fly()
        {
            Console.WriteLine($"{Name}({Code}) fly.");
        }

        public void Attack()
        {
            Console.WriteLine($"{Name}({Code}) attack.");
        }

        public void Information()
        {
            Console.WriteLine($"Name: {Name}, Id: {Id}, Weight: {Weight.ToString("0.00")}");
            Console.WriteLine($"Options: OilBox-{OilBox}, Code-{Code}");
        }
    }
}
