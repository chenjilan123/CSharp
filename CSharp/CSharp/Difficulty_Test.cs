using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CSharp
{
    public class Difficulty_Test
    {
        Difficulty _solution = new Difficulty();


        #region 天际线
        [Fact]
        public void GetSkyline()
        {
            foreach (var test in GetSkylineTestData())
            {
                test.Judge(_solution);
            }
        }

        private IEnumerable<GetSkylineTest> GetSkylineTestData()
        {
            yield return new GetSkylineTest()
            {
                Buildings = new int[][]
                {
                    new [] { 2, 9, 10 },
                    new [] { 3, 7, 15 },
                    new [] { 5, 12, 12 },
                    new [] { 15, 20, 10 },
                    new [] { 19, 24, 8 },
                },
                Except = new List<IList<int>>
                {
                    new [] { 2, 10 },
                    new [] { 3, 15 },
                    new [] { 7, 12 },
                    new [] { 12, 0},
                    new [] { 15, 10 },
                    new [] { 20, 8 },
                    new [] { 24, 0 },
                },
            };
            yield return new GetSkylineTest()
            {
                Buildings = new int[][]
                 {
                    new [] { 0, 1, 3 },
                 },
                Except = new List<IList<int>>
                {
                    new [] { 0, 3 },
                    new [] { 1, 0 },
                },
            };
        }

        public class GetSkylineTest
        {
            public int[][] Buildings;
            public IList<IList<int>> Except;
            public void Judge(Difficulty solution)
            {
                var result = solution.GetSkyline(Buildings);
                for (int i = 0; i < result.Count; i++)
                {
                    var arr1 = result[i];
                    var arr2 = Except[i];

                    Assert.Equal(arr1[0], arr2[0]);
                    Assert.Equal(arr1[1], arr2[1]);
                }
            }
        }
        #endregion

        #region 解码方法 2
        [Theory]
        [InlineData("*", 9)]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("1*", 18)]
        [InlineData("**", 96)]
        [InlineData("1**", 177)]
        [InlineData("*1*1*0", 404)] //少考虑了0的特殊性, 零是不可以单独编码的。
        public void NumDecodings(string s, int except)
        {
            var result = _solution.NumDecodings(s);
            Assert.Equal(except, result);
        }
        #endregion
    }
}
