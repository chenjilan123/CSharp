using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static CSharp.Easy;

namespace CSharp
{
    public class Easy_Test
    {
        private Easy _solution = new Easy();

        #region 两数之和
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
        #endregion

        #region 最长公共前缀
        [Theory]
        [InlineData(new[] { "hello", "halo" }, "h")]
        [InlineData(new[] { "hehe", "ga" }, "")]
        [InlineData(new[] { "heheheheda", "hehehehedaggggg", "hehehehedacxzv" }, "heheheheda")]
        [InlineData(new string[0], "")]
        [InlineData(new[] { "a" }, "a")]
        void LongestCommonPrefix(string[] strs, string except)
        {
            //获取结果并与预期值比较的过程可自动化。
            var result = _solution.LongestCommonPrefix(strs);

            Assert.Equal(except, result);
        }
        #endregion

        #region 机器人能否返回原点
        [Theory]
        [InlineData("RLUD", true)]
        [InlineData("RLU", false)]
        [InlineData("RRRUUULLLDDD", true)]
        void JudgeCircle(string moves, bool except)
        {
            var result = _solution.JudgeCircle(moves);
            Assert.Equal(except, result);
        }
        #endregion

        #region 存在重复元素
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7 }, false)]
        [InlineData(new[] { 1, 2, 3, 4, 5, 1, 7 }, true)]
        [InlineData(new[] { 9, 2, 3, 4, 5, 3, 7 }, true)]
        public void ContainsDuplicate(int[] nums, bool except)
        {
            var result = _solution.ContainsDuplicate(nums);
            Assert.Equal(except, result);
        }
        #endregion

        #region 存在重复元素II
        [Theory]
        [InlineData(new[] { 1, 2, 3, 1 }, 3, true)]
        [InlineData(new[] { 1, 0, 1, 1 }, 1, true)]
        [InlineData(new[] { 1, 2, 3, 1, 2, 3 }, 2, false)]
        [InlineData(new[] { 1, 2, 3, 4, 5, 1 }, 5, true)]
        [InlineData(new[] { 1 }, 0, false)] //特例
        [InlineData(new[] { 1, 2, 3 }, 0, false)] //特例
        public void ContainsNearbyDuplicate(int[] nums, int k, bool except)
        {
            var result = _solution.ContainsNearbyDuplicate(nums, k);
            Assert.Equal(except, result);
        }

        [Fact]
        public void ContainsNearbyDuplicateLongTime()
        {
            var nums = new int[1024 * 100];
            for (int i = 0; i < nums.Length; i++)
                nums[i] = i;
            var result = _solution.ContainsNearbyDuplicate(nums, 3500);
            Assert.False(result);
        }
        #endregion

        #region 计数质数
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 1)]
        [InlineData(4, 2)]
        [InlineData(5, 2)]
        [InlineData(6, 3)]
        [InlineData(7, 3)]
        [InlineData(8, 4)]
        [InlineData(9, 4)]
        [InlineData(10, 4)]
        [InlineData(11, 4)]
        [InlineData(12, 5)]
        [InlineData(13, 5)]
        [InlineData(17, 6)]
        [InlineData(18, 7)]
        [InlineData(23, 8)]
        [InlineData(24, 9)]
        [InlineData(29, 9)]
        [InlineData(30, 10)]
        [InlineData(31, 10)]
        [InlineData(37, 11)]
        [InlineData(38, 12)]
        [InlineData(49999, 5132)]
        [InlineData(499990, 41538)]
        [InlineData(100000000, 5761455)]
        [InlineData(1000000000, 50847534)]
        public void CountPrimes(int n, int except)
        {
            //2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37
            //var result = _solution.CountPrimes(n); 
            var result = _solution.CountPrimesAdvance(n);
            Assert.Equal(except, result);
        }
        #endregion

        #region 丑数
        [Theory]
        [InlineData(1, true)]
        [InlineData(5, true)]
        [InlineData(6, true)]
        [InlineData(7, false)]
        [InlineData(70, false)]
        [InlineData(71, false)]
        [InlineData(100, true)]
        [InlineData(65536, true)]
        public void IsUgly(int num, bool except)
        {
            var result = _solution.IsUgly(num);
            Assert.Equal(except, result);
        }
        #endregion

        #region 移除链表元素
        [Fact]
        public void RemoveElements()
        {
            foreach (var test in GetListNodeElementTest())
            {
                //var result = _solution.RemoveElements(test.head, test.val);
                var result = _solution.RemoveElementsAdvance(test.head, test.val);

                if (result == null)
                {
                    Assert.True(test.except == null);
                    continue;
                }
                while (result.next != null)
                {
                    Assert.Equal(result.next.val, test.except.next.val);

                    result = result.next;
                    test.except = test.except.next;
                }
            }
        }

        /// <summary>
        /// Severity	Code	Description	Project	File	Line	Suppression State
        /// Error CS1654  Cannot modify members of 'test' because it is a 'foreach iteration variable'	CSharp H:\C#\CSharp\CSharp\CSharp\Easy_Test.cs	170	Active
        /// </summary>
        //private struct ListNodeElementTest
        private class ListNodeElementTest
        {
            public ListNode head;
            public int val;
            public ListNode except;
        }
        private IEnumerable<ListNodeElementTest> GetListNodeElementTest()
        {
            yield return new ListNodeElementTest()
            {
                val = 1,
                head = new ListNode(1),
                except = null,
            };
            yield return new ListNodeElementTest()
            {
                val = 3,
                head = new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(4)
                        }
                    }
                },
                except = new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(4)
                    }
                },
            };
            yield return new ListNodeElementTest()
            {
                val = 1,
                head = new ListNode(1)
                {
                    next = new ListNode(2)
                    {
                        next = new ListNode(3)
                        {
                            next = new ListNode(4)
                        }
                    }
                },
                except = new ListNode(2)
                {
                    next = new ListNode(3)
                    {
                        next = new ListNode(4)
                    }
                },
            };
            yield return new ListNodeElementTest()
            {
                val = 1,
                head = new ListNode(1)
                {
                    next = new ListNode(1)
                },
                except = null,
            };
        }
        #endregion
    }
}
