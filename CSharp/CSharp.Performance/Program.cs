using BenchmarkDotNet.Running;
using System;

namespace CSharp.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();
        }
    }
}
