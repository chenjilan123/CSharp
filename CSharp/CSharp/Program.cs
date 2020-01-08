using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            GetFramework();
        }

        static void GetFramework()
        {
            Console.WriteLine($"OS: {Environment.OSVersion}");
            Console.WriteLine($"x64: {Environment.Is64BitOperatingSystem}");
            Console.WriteLine($"Version: {Environment.Version}");
            Console.WriteLine($"Runtime: {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"OS: {RuntimeInformation.OSDescription}");
        }

        static void PrintTimes()
        {
            while (true)
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
                Task.Delay(TimeSpan.FromSeconds(1D)).Wait();
            }
        }
    }
}
