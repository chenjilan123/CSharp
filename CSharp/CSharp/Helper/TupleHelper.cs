using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Helper
{
    public class TupleHelper
    {
        public Tuple<int, int> MinMax(int i, int j)
        {
            //return new Tuple<int, int>(Math.Min(i, j), Math.Max(i, j));
            return Tuple.Create(Math.Min(i, j), Math.Max(i, j));
        }

        public (int Max, int Min) MaxMin(int i, int j)
        {
            return (Math.Max(i, j), Math.Min(i, j));
        }
    }
}
