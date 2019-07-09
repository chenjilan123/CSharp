using System;
using Unity;

namespace CSharp.UnityIoC
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Unity.UnityContainer();
            container.RegisterType<Par>();
            //container.RegisterType<Son>();

            var par = container.Resolve<Par>();
            Console.WriteLine(par.HasSon());
        }
    }
}
