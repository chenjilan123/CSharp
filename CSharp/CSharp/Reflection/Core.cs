using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Reflection
{
    public class Core
    {
        IAnimal _animal = new Human();

        public void Play()
        {
            var methods = _animal.GetType().GetMethods();
            foreach (var method in methods)
            {
                Console.WriteLine($"Method name: {method.Name}");
            }
        }
    }

    public interface IAnimal
    {
        string GetSpecies();
    }

    public class Human : IAnimal
    {
        public string GetSpecies()
        {
            return "Mankind";
        }

        public string GetName()
        {
            return "";
        }
    }
}
