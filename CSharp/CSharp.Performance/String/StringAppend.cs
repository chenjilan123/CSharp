using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Performance.String
{

    [MemoryDiagnoser]
    public class StringAppend
    {
        private const int Lng = 1000;

        [Benchmark(Baseline = true)]
        public void StringBuilder()
        {
            var sb = new StringBuilder(Lng + 2);
            for (int i = 0; i < Lng; i++)
            {
                sb.Append("1");
            }
        }
        [Benchmark]
        public void StringAdd()
        {
            var s = "";
            for (int i = 0; i < Lng; i++)
            {
                s += "1";
            }
        }
    }
}
