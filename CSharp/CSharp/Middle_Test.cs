using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CSharp
{
    public class Middle_Test
    {
        private Middle _solution = new Middle();

        private struct CircleNumExcept
        {
            public int[][] M;
            public int Except;
        }
        private IEnumerable<CircleNumExcept> GetCircle()
        {
            yield return new CircleNumExcept()
            {
                M = new int[][]
                {
                    new [] { 1, 1, 0 },
                    new [] { 1, 1, 0 },
                    new [] { 0, 0, 1 },
                },
                Except = 2,
            };
            yield return new CircleNumExcept()
            {
                M = new int[][]
                {
                    new [] { 1, 1, 0 },
                    new [] { 1, 1, 1 },
                    new [] { 0, 1, 1 },
                },
                Except = 1,
            };
        }
        [Fact]
        public void FindCircleNum()
        {
            var enumerator = GetCircle().GetEnumerator();

            while(enumerator.MoveNext())
            {
                var result = _solution.FindCircleNum(enumerator.Current.M);
                Assert.Equal(enumerator.Current.Except, result);
            }
        }
    }
}
