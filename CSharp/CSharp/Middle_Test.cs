using CSharp.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace CSharp
{
    public class Middle_Test
    {
        private Middle _solution = new Middle();
        private DynamicAssert _assert;
        public Middle_Test()
        {
            _assert = new DynamicAssert(_solution);
        }

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

        #region 路径总和 II
        [Fact]
        public void PathSum()
        {
            TreeNode root = null;
            int sum = 0;
            IList<IList<int>> except;

            var result = _solution.PathSum(root, sum);
        }

        public class PathSumTestData
        {
            public IEnumerable<PathSumTestData> GetTestData()
            {
                //yield return new PathSumTestData()
                //{
                //    root = 
                //}
                yield break;
            }

            public TreeNode root;
            public int sum;
            public IList<IList<int>> except;
        }
        #endregion

        #region 二叉树

        #region 两数相加
        [Fact]
        public void AddTwoNumbers()
        {
            foreach (var test in AddTwoNumbersTestData.GetTestData())
            {
                ListNode except = test.except;
                var result = _solution.AddTwoNumbers(test.l1, test.l2);

                while(except != null)
                {
                    Assert.NotNull(result);
                    Assert.Equal(except.val, result.val);

                    result = result.next;
                    except = except.next;
                }
            }

        }

        public class AddTwoNumbersTestData
        {
            public static IEnumerable<AddTwoNumbersTestData> GetTestData()
            {
                yield return new AddTwoNumbersTestData()
                {
                    l1 = ListNode.CreateNode(new[] { 1 }),
                    l2 = ListNode.CreateNode(new[] { 8 }),
                    except = ListNode.CreateNode(new[] { 9 }),
                };
                yield return new AddTwoNumbersTestData()
                {
                    l1 = ListNode.CreateNode(new[] { 1 }),
                    l2 = ListNode.CreateNode(new[] { 9 }),
                    except = ListNode.CreateNode(new[] { 0, 1 }),
                };
                yield return new AddTwoNumbersTestData()
                {
                    l1 = ListNode.CreateNode(new[] { 1, 9, 9 }),
                    l2 = ListNode.CreateNode(new[] { 9 }),
                    except = ListNode.CreateNode(new[] { 0, 0, 0, 1 }),
                };
                yield return new AddTwoNumbersTestData()
                {
                    l1 = ListNode.CreateNode(new[] { 1, 8 }),
                    l2 = ListNode.CreateNode(new[] { 9 }),
                    except = ListNode.CreateNode(new[] { 0, 9 }),
                };
                yield return new AddTwoNumbersTestData()
                {
                    l1 = ListNode.CreateNode(new[] { 1, 2, 3 }),
                    l2 = ListNode.CreateNode(new[] { 2, 3, 4 }),
                    except = ListNode.CreateNode(new[] { 3, 5, 7 }),
                };
                yield return new AddTwoNumbersTestData()
                {
                    l1 = ListNode.CreateNode(new[] { 5, 2, 3 }),
                    l2 = ListNode.CreateNode(new[] { 2, 3, 4 }),
                    except = ListNode.CreateNode(new[] { 7, 5, 7 }),
                };
            }

            public ListNode l1;
            public ListNode l2;
            public ListNode except;
        }
        #endregion

        #endregion

        #region 无重复字符的最长子串
        [Theory]
        [InlineData("abcdeeg", 5)]
        [InlineData("efwwkef", 4)]
        [InlineData("abcdefg", 7)]
        [InlineData("aaaaa", 1)]
        [InlineData("", 0)]
        [InlineData("afdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieasafdsaklfeieas"
            , 8)]
        public void LengthOfLongestSubstring(string s, int except)
        {
            _assert.AssertMethod(MethodInfo.GetCurrentMethod(), s, except);
        }
        #endregion

        #region 最长回文子串
        [Theory]
        //奇数长度
        [InlineData("", "")]
        [InlineData("a", "a")]
        [InlineData("abc", "a")]
        [InlineData("abcdede", "ded")]
        [InlineData("abxs1sxba", "abxs1sxba")]
        ///偶数长度
        [InlineData("abba", "abba")]
        [InlineData("cdabba", "abba")]
        [InlineData("cdabbaccabb", "bbaccabb")]
        public void LongestPalindrome(string s, string except)
        {
            _assert.AssertMethod(MethodInfo.GetCurrentMethod(), s, except);
        }
        #endregion

        #region Z字形变换
        [Theory]
        [InlineData("LEETCODEISHIRING", 3, "LCIRETOESIIGEDHN")]
        [InlineData("LEETCODEISHIRING", 4, "LDREOEIIECIHNTSG")]
        [InlineData("AB", 1, "AB")]
        public void Convert(string s, int numRows, string except)
        {
            _assert.AssertMethod(MethodInfo.GetCurrentMethod(), s, numRows, except);
        }
        #endregion

        #region 字符串转换整数 (atoi)
        [Theory]
        [InlineData("", 0)]
        [InlineData("sdad", 0)]
        [InlineData("42", 42)]
        [InlineData("-42", -42)]
        [InlineData("42dcvfsfsdf", 42)]
        [InlineData("    -42", -42)]
        [InlineData("-1000000000000000", int.MinValue)]
        [InlineData("-10000000000000000000", int.MinValue)]
        [InlineData("-000000000000000000044444444", -44444444)]
        [InlineData("100000000000000", int.MaxValue)]
        public void MyAtoi(string str, int except)
        {
            _assert.AssertMethod(MethodInfo.GetCurrentMethod(), str, except);
        }
        #endregion
    }
}
