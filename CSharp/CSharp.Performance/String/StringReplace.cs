using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Performance.String
{
    [MemoryDiagnoser]
    public class StringReplace
    {
        private List<int> _params = new List<int>() { 1, 2, 3, 4 };
        [ParamsSource(nameof(_params))]
        public int Param { get; set; }

        [Benchmark(Baseline = true)]
        public void ReplaceString()
        {
            var s = string.Format("{0}, {1}, {2}", Param.ToString(), Param.ToString(), Param.ToString());
        }

        [Benchmark]
        public void ReplaceNonString()
        {
            //装箱_更快？
            var s = string.Format("{0}, {1}, {2}", Param, Param, Param);
        }
    }
}
