using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CSharp
{
    public class Easy_Test
    {
        private Easy _solution = new Easy();


        [Theory]
        [InlineData(new int[] { 1, 4 }, 5, new int[] { 0, 1 })]
        [InlineData(new int[] { 1, 4, 5, 10 }, 9, new int[] { 1, 2 })]
        [InlineData(new int[] { 1, 4, 1, 5, 10 }, 15, new int[] { 3, 4 })]
        [InlineData(new int[] { 1, 4, 1, 5, 10 }, 20, new int[0])]
        void TwoSum(int[] nums, int target, int[] excepts)
        {
            //var result = _solution.TwoSum(nums, target);
            var result = _solution.TwoSumAdvance(nums, target);

            //无结果的不验证
            if (excepts.Length == 0)
            {
                return;
            }
            Assert.Equal(excepts.Length, result.Length);
            Assert.Equal(excepts[0], result[0]);
            Assert.Equal(excepts[1], result[1]);
        }

        [Theory]
        [InlineData(new [] { "hello", "halo" }, "h")]
        [InlineData(new [] { "hehe", "ga" }, "")]
        [InlineData(new [] { "heheheheda", "hehehehedaggggg", "hehehehedacxzv" }, "heheheheda")]
        [InlineData(new string[0], "")]
        [InlineData(new[] { "a" }, "a")]
        void LongestCommonPrefix(string[] strs, string except)
        {
            //获取结果并与预期值比较的过程可自动化。
            var result = _solution.LongestCommonPrefix(strs);

            Assert.Equal(except, result);
        }

        [Theory]
        [InlineData("RLUD", true)]
        [InlineData("RLU", false)]
        [InlineData("RRRUUULLLDDD", true)]
        void JudgeCircle(string moves, bool except)
        {
            var result = _solution.JudgeCircle(moves);
            Assert.Equal(except, result);
        }
    }
}
