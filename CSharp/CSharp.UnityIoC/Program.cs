using System;
using Unity;

namespace CSharp.UnityIoC
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer register = new Unity.UnityContainer();
            //register.RegisterType<ILogger>

            var container = new Unity.UnityContainer();

        }
    }
}
