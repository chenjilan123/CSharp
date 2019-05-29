using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CSharp
{
    public class Easy
    {
        #region 两数之和
        /// <summary>
        /// 给定一个整数数组 nums 和一个目标值 target，请你在该数组中找出和为目标值的那 两个 整数，并返回他们的数组下标。
        /// 你可以假设每种输入只会对应一个答案。但是，你不能重复利用这个数组中同样的元素。
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int[] TwoSum(int[] nums, int target)
        {
            //遍历
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new[] { i, j };
                    }
                }
            }
            return new int[0];
        }

        /// <summary>
        /// 提升: 用字典的特性。
        /// Key存差, Value存索引。
        /// 遍历nums, 若字典包含当前值则找到。
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int[] TwoSumAdvance(int[] nums, int target)
        {
            //保存剩余值
            //var dic = new Dictionary<int, int>();
            //var index = 0;
            //foreach (var num in nums)
            //{
            //    if (dic.ContainsKey(num))
            //    {
            //        return new[] { dic[num], index };
            //    }
            //    dic[target - num] = index;
            //    index++;
            //}
            //return null;

            //保存实际值
            var dic = new Dictionary<int, int>();
            var index = 0;
            foreach (var num in nums)
            {
                var rest = target - num;
                if (dic.ContainsKey(rest))
                {
                    return new[] { dic[rest], index };
                }
                dic[num] = index;
                index++;
            }
            return null;
        }
        #endregion

        #region 最长公共前缀
        /// <summary>
        /// 编写一个函数来查找字符串数组中的最长公共前缀。
        /// 如果不存在公共前缀，返回空字符串 ""。
        /// 说明: 所有输入只包含小写字母 a-z 。
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs == null || strs.Length <= 0) return string.Empty;
            if (strs.Length == 1) return strs[0];
            var first = strs[0];
            int max = int.MaxValue, current = 0;
            for (int i = 1; i < strs.Length; i++, current = 0)
            {
                var curStr = strs[i];
                while(curStr.Length > current && first.Length > current && curStr[current] == first[current]) current++;
                if (current == 0) return "";
                if (current < max) max = current;
            }
            if (max != int.MaxValue) return first.Substring(0, max);
            //无公共前缀
            return string.Empty;
        }

        public string LongestCommonPrefixAdvance(string[] strs)
        {
            //TODO: 优化
            //参考: https://leetcode-cn.com/problems/longest-common-prefix/comments/3427
            //1、纵向扫描：从下标0开始，判断每一个字符串的下标0，判断是否全部相同。直到遇到不全部相同的下标。时间性能为O(n*m)。
            //2、横向扫描：前两个字符串找公共子串，将其结果和第三个字符串找公共子串……直到最后一个串。时间性能为O(n * m)。

            //3、借助trie字典树。将这些字符串存储到trie树中。那么trie树的第一个分叉口之前的单分支树的就是所求。
            //   Tire树核心思想是空间换取时间，利用字符串的公共前缀来节省查询时间, 其查询的时间复杂度是O(L)
            return string.Empty;
        }
        #endregion

        #region 机器人能否返回原点
        /// <summary>
        /// 在二维平面上，有一个机器人从原点 (0, 0) 开始。给出它的移动顺序，判断这个机器人在完成移动后是否在 (0, 0) 处结束。
        /// 移动顺序由字符串表示。字符 move[i] 表示其第 i 次移动。机器人的有效动作有 R（右），L（左），U（上）和 D（下）。如果机器人在完成所有动作后返回原点，则返回 true。否则，返回 false。
        /// 注意：机器人“面朝”的方向无关紧要。 “R” 将始终使机器人向右移动一次，“L” 将始终向左移动等。此外，假设每次移动机器人的移动幅度相同。
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        public bool JudgeCircle(string moves)
        {
            //①统计R,L,U,D的数目,若count(R)=count(L)且count(U)=count(D), 则能返回
            //②分别统计水平和垂直方向的相对移动步数，若相对移动步数均为0，则回到原点。
            int horizone = 0, vertical = 0;
            foreach (var move in moves.ToUpper())
                switch (move)
                {
                    case 'U': vertical++; break;
                    case 'D': vertical--; break;
                    case 'R': horizone++; break;
                    case 'L': horizone--; break;
                    default: break;
                }
            return horizone == 0 && vertical == 0;
        }

        /// <summary>
        /// ref: 可变步数, 可定制步数。
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        public bool JudgeCircleAdvance(string moves)
        {
            var verticalPoint = 0;
            var horizonPoint = 0;
            var verticalDict = new Dictionary<char, int>();
            verticalDict.Add('U', 1);
            verticalDict.Add('D', -1);

            var horizonDict = new Dictionary<char, int>();
            horizonDict.Add('L', 1);
            horizonDict.Add('R', -1);

            char[] movs = moves.ToArray();

            foreach (char m in movs)
            {
                if (verticalDict.ContainsKey(m))
                {
                    verticalPoint += verticalDict[m];
                }
            }

            foreach (char m in movs)
            {
                if (horizonDict.ContainsKey(m))
                {
                    horizonPoint += horizonDict[m];
                }
            }

            return (horizonPoint == 0 && verticalPoint == 0);
        }
        #endregion
    }
}
