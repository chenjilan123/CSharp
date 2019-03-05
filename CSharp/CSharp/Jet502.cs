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

        public int Missiles { get; set; }

        public Jet502(IOptions<JetOptions> options, IOptions<JetExtensionOptions> extensionOptions)
        {
            this.Name = "J20";
            this.OilBox = options.Value.OilBox;
            this.Code = options.Value.Code;
            this.Weight = extensionOptions.Value.Weight;
            this.Id = extensionOptions.Value.Id;
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
            Console.WriteLine($"Missiles: {Missiles}");
            Console.WriteLine($"Options: OilBox-{OilBox}, Code-{Code}");
        }
    }
}
