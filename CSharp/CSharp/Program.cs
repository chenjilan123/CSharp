using System;
using System.Threading.Tasks;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
                Task.Delay(TimeSpan.FromSeconds(1D)).Wait();
            }
        }
    }
}
