using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    public class Middle
    {
        #region 朋友圈
        /// <summary>
        /// 班上有 N 名学生。其中有些人是朋友，有些则不是。他们的友谊具有是传递性。如果已知 A 是 B 的朋友，B 是 C 的朋友，那么我们可以认为 A 也是 C 的朋友。所谓的朋友圈，是指所有朋友的集合。
        /// 给定一个 N * N 的矩阵 M，表示班级中学生之间的朋友关系。如果M[i][j] = 1，表示已知第 i 个和 j 个学生互为朋友关系，否则为不知道。你必须输出所有学生中的已知的朋友圈总数。
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        public int FindCircleNum(int[][] M)
        {

            return 0;
        }
        #endregion

        #region 存在重复元素III
        /// <summary>
        /// 给定一个整数数组，判断数组中是否有两个不同的索引 i 和 j，使得 nums [i] 和 nums [j] 的差的绝对值最大为 t，并且 i 和 j 之间的差的绝对值最大为 ķ。
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k">索引差的最大绝对值</param>
        /// <param name="t">差的最大绝对值, t = 0 就是特例"存在重复元素II"</param>
        /// <returns></returns>
        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            //时间复杂度过高。
            var abs = (long)t;
            var maxIIndex = nums.Length - 1;
            for (int i = 0; i < maxIIndex; i++)
            {
                var maxJIndex = i + k;
                for (int j = i + 1; j <= maxJIndex && j < nums.Length; j++)
                {
                    //小心整数溢出
                    if (Math.Abs((long)nums[i] - (long)nums[j]) <= abs) return true;
                }
            }
            return false;
        }
        #endregion
    }
}
