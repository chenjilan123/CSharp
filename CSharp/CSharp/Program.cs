using CSharp.Performance;
using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            ComparePerformance();
        }


        #region ComparePerformance
        private static void ComparePerformance()
        {
            new CompareInSingleThread()
                .Compare();
        }
        #endregion
    }
}
