using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Performance.EasyPerformance
{
    [MemoryDiagnoser]
    public class CountPrimes
    {
        Easy _solution = new Easy();

        [Benchmark]
        public void CountPrimes_25()
        {
            _solution.CountPrimes(25);
        }

        [Benchmark(Baseline = true)]
        public void CountPrimes_50()
        {
            _solution.CountPrimes(50);
        }

        [Benchmark]
        public void CountPrimes_100()
        {
            _solution.CountPrimes(100);
        }

        [Benchmark]
        public void CountPrimes_200()
        {
            _solution.CountPrimes(200);
        }
        [Benchmark]
        public void CountPrimes_500()
        {
            _solution.CountPrimes(500);
        }
        [Benchmark]
        public void CountPrimes_5000()
        {
            _solution.CountPrimes(5000);
        }
    }
}
