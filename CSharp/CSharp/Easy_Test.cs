using CSharp.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using static CSharp.Easy;

namespace CSharp
{
    public class Easy_Test
    {
        private Easy _solution = new Easy();
        private DynamicAssert _assert;
        public Easy_Test()
        {
            _assert = new DynamicAssert(_solution);
        }

        #region 构建通用测试用例
        [Theory]
        [InlineData(1, true, 3D)]
        private void Test(int i, bool b, double d)
        {
            var curMethod = System.Reflection.MethodInfo.GetCurrentMethod();
            var param = curMethod.GetParameters();
            Assert.True(typeof(int).IsEquivalentTo(param[0].ParameterType));
            Assert.True(typeof(bool).IsEquivalentTo(param[1].ParameterType));
            Assert.True(typeof(double).IsEquivalentTo(param[2].ParameterType));
        }
        #endregion

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
        //[InlineData(1000000000, 50847534)] //18sec
        public void CountPrimesAdvance(int n, int except)
        //public void CountPrimes(int n, int except)
        {
            _assert.AssertMethod(MethodInfo.GetCurrentMethod(), n, except);
            //2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37
            //var result = _solution.CountPrimes(n); 
            //var result = _solution.CountPrimesAdvance(n);
            //Assert.Equal(except, result);
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
        //private struct RemoveElementsTest
        private class RemoveElementsTest
        {
            public ListNode head;
            public int val;
            public ListNode except;
        }
        private IEnumerable<RemoveElementsTest> GetListNodeElementTest()
        {
            yield return new RemoveElementsTest()
            {
                val = 1,
                head = new ListNode(1),
                except = null,
            };
            yield return new RemoveElementsTest()
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
            yield return new RemoveElementsTest()
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
            yield return new RemoveElementsTest()
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

        #region 反转链表
        [Fact]
        public void ReverseList()
        {
            foreach (var test in GetReverseListTest())
            {
                //var result = _solution.RemoveElements(test.head, test.val);
                var result = _solution.ReverseList(test.head);

                if (result == null)
                {
                    Assert.True(test.except == null);
                    continue;
                }
                while (test.except.next != null)
                {
                    Assert.Equal(result.next.val, test.except.next.val);

                    result = result.next;
                    test.except = test.except.next;
                }
            }
        }

        private class ReverseListTest
        {
            public ListNode head;
            public ListNode except;
        }
        private IEnumerable<ReverseListTest> GetReverseListTest()
        {
            yield return new ReverseListTest()
            {
                head = new ListNode(1),
                except = new ListNode(1),
            };
            yield return new ReverseListTest()
            {
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
                except = new ListNode(4)
                {
                    next = new ListNode(3)
                    {
                        next = new ListNode(2)
                        {
                            next = new ListNode(1)
                        }
                    }
                },
            };
            yield return new ReverseListTest()
            {
                head = new ListNode(1)
                {
                    next = new ListNode(1)
                },
                except = new ListNode(1)
                {
                    next = new ListNode(1)
                },
            };
        }
        #endregion

        #region 最大子序和
        [Theory]
        [InlineData(new[] { -3, 1, -2, 5, -1, -2, -1, 4, 3, -1, -1, 5 }, 11)]
        [InlineData(new[] { 1 }, 1)]
        [InlineData(new[] { -1 }, -1)]
        [InlineData(new[] { -2 }, -2)]
        [InlineData(new[] { -2, -1 }, -1)]
        [InlineData(new[] { -2, -1, -3 }, -1)]
        [InlineData(new[] { -2, -1, 3 }, 3)]
        [InlineData(new[] { 2, -1, -3 }, 2)]
        public void MaxSubArray(int[] nums, int except)
        {
            //var result = _solution.MaxSubArray(nums);
            var result = _solution.MaxSubArrayAdvance(nums);
            //var result = _solution.MaxSubArrayRef(nums);
            Assert.Equal(except, result);
        }
        #endregion

        #region 二叉树

        #region 对称二叉树
        [Theory]
        [InlineData(new int[] { 1, 2, 2, 3, 4, 4, 3 }, true)]
        ///    1
        ///   / \
        ///  2   2
        /// / \ / \
        ///3  4 4  3
        public void IsSymmetric(int[] data, bool except)
        {
            TreeNode root = new TreeNode(1)
            {
                left = new TreeNode(2)
                {
                    left = new TreeNode(3),
                    right = new TreeNode(4),
                },
                right = new TreeNode(2)
                {
                    left = new TreeNode(4),
                    right = new TreeNode(3),
                },
            };
            var result = _solution.IsSymmetric(root);
            Assert.Equal(except, result);
        }
        #endregion

        #region 路径总和
        [Fact]
        public void HasPathSum()
        {
            //TreeNode root = new TreeNode(5)
            //{
            //    left = new TreeNode(4)
            //    {
            //        left = new TreeNode(11)
            //        {
            //            left = new TreeNode(7),
            //            right = new TreeNode(2),
            //        }
            //    },
            //    right = new TreeNode(8)
            //    {
            //        left = new TreeNode(13),
            //        right = new TreeNode(4)
            //        {
            //            right = new TreeNode(1)
            //        },
            //    }
            //};
            //var sum = 22;
            //var except = true;
            foreach (var testData in HashPathSumTestData.GetTestData())
            {
                var result = _solution.HasPathSum(testData.root, testData.sum);
                Assert.Equal(testData.except, result);
            }
        }

        public class HashPathSumTestData
        {
            public static IEnumerable<HashPathSumTestData> GetTestData()
            {
                ///                   5
                ///                  / \
                ///                 4   8
                ///                /   / \
                ///               11  13  4
                ///              /  \      \
                ///             7    2      1
                yield return new HashPathSumTestData()
                {
                    root = TreeNode.GetTreeNode(new int?[] { 5, 4, 8, 11, null, 13, 4, 7, 2, null, null, null, null, null, 1 }),
                    sum = 22,
                    except = true,
                };

                ///                    7
                ///                  /   \
                ///                 3     4
                ///                / \   / \  
                ///               2   1 -4  7
                yield return new HashPathSumTestData()
                {
                    root = TreeNode.GetTreeNode(new int?[] { 7, 3, 4, 2, 1, -4, 7 }),
                    sum = 12,
                    except = true,
                };
                yield return new HashPathSumTestData()
                {
                    root = TreeNode.GetTreeNode(new int?[] { 7, 3, 4, 2, 1, -4, 7 }),
                    sum = 10,
                    except = false,
                };
                yield return new HashPathSumTestData()
                {
                    root = TreeNode.GetTreeNode(new int?[] { 7, 3, 4, 2, 1, -4, 7 }),
                    sum = 7,
                    except = true,
                };

                yield return new HashPathSumTestData()
                {
                    //只包含根节点的不算。
                    root = TreeNode.GetTreeNode(new int?[] { 1, 2 }),
                    sum = 1,
                    except = false,
                };
                yield return new HashPathSumTestData()
                {
                    //只包含根节点的不算。
                    root = TreeNode.GetTreeNode(new int?[] { 1, null, 2 }),
                    sum = 1,
                    except = false,
                };
                yield return new HashPathSumTestData()
                {
                    //只包含根节点的不算。
                    root = TreeNode.GetTreeNode(new int?[] { 1, 2, null, 3, null, 4, null, 5 }),
                    sum = 6,
                    except = false,
                };
            }

            public TreeNode root;
            public int sum;
            public bool except;
        }
        #endregion

        #endregion

        #region x的平方根
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        [InlineData(4, 2)]
        [InlineData(5, 2)]
        [InlineData(9, 3)]
        [InlineData(10, 3)]
        [InlineData(100, 10)]
        [InlineData(101, 10)]
        [InlineData(120, 10)]
        [InlineData(121, 11)]
        [InlineData(2147395600, 46340)]
        public void MySqrt(int x, int except)
        {
            var result = _solution.MySqrt(x);
            Assert.Equal(except, result);
        }
        #endregion

        #region 回文数
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        [InlineData(2, true)]
        [InlineData(0, true)]
        [InlineData(13, false)]
        [InlineData(11, true)]
        [InlineData(121, true)]
        [InlineData(12121, true)]
        [InlineData(12521, true)]
        [InlineData(-12521, false)]
        [InlineData(147959741, true)]
        [InlineData(-147959741, false)]
        [InlineData(147959743, false)]
        [InlineData(74321, false)]
        [InlineData(-74321, false)]
        public void IsPalindrome(int x, bool except)
        {
            _assert.AssertMethod(MethodInfo.GetCurrentMethod(), x, except);
        }
        #endregion
    }
}
