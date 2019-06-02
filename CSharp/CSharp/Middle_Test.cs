using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CSharp
{
    public class Middle_Test
    {
        private Middle _solution = new Middle();

        #region 朋友圈
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
                //Assert.Equal(enumerator.Current.Except, result);
            }
        }
        #endregion

        #region 存在重复元素III
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k">索引差的最大绝对值</param>
        /// <param name="t">差的最大绝对值</param>
        /// <param name="except"></param>
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 1, 0, false)]
        [InlineData(new[] { 1, 3, 5 }, 1, 2, true)]
        [InlineData(new[] { 1, 3, 5 }, 1, 1, false)]
        [InlineData(new[] { 1, 13, 25, 39, 65, 47, 81 }, 2, 10, true)]
        [InlineData(new[] { 1, 13, 25, 39, 65, 47, 81 }, 1, 10, false)]
        [InlineData(new[] { -1, 2147483647 }, 1, 2147483647, false)]
        public void ContainsNearbyAlmostDuplicate(int[] nums, int k, int t, bool except)
        {
            var result = _solution.ContainsNearbyAlmostDuplicate(nums, k, t);
            Assert.Equal(except, result);
        }
        #endregion

    }
}
