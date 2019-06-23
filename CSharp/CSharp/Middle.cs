using CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    public class Middle
    {
        #region 朋友圈-Unsolved
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

        #region 存在重复元素III-Unsolved
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

        #region 路径总和 II
        /// <summary>
        /// 给定一个二叉树和一个目标和，找到所有从根节点到叶子节点路径总和等于给定目标和的路径。
        ///     说明: 叶子节点是指没有子节点的节点。
        ///     示例:
        ///         给定如下二叉树，以及目标和 sum = 22，
        ///                       5
        ///                      / \
        ///                     4   8
        ///                    /   / \
        ///                   11  13  4
        ///                  /  \    / \
        ///                 7    2  5   1
        /// </summary>
        /// <param name="root"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public IList<IList<int>> PathSum(TreeNode root, int sum)
        {
            IList<IList<int>> result = new List<IList<int>>();
            var cur = new List<int>();
            PathSum(root, sum, result, cur);
            return result;
        }

        private void PathSum(TreeNode root, int rest, IList<IList<int>> result, IList<int> prev)
        {
            if (root == null) return;
            rest -= root.val;
            prev.Add(root.val);
            if (root.left == null && root.right == null)
                if (rest == 0) result.Add(new List<int>(prev));
            PathSum(root.left, rest, result, prev);
            PathSum(root.right, rest, result, prev);
            prev.RemoveAt(prev.Count - 1);

            //if (root == null) return;
            //rest -= root.val;
            //prev.Add(root.val);
            //if (root.left == null && root.right == null)
            //{
            //    if (rest == 0)
            //    {
            //        result.Add(prev);
            //    }
            //    return;
            //}
            //var clone = new List<int>();
            //foreach (var item in prev)
            //    clone.Add(item);
            //PathSum(root.left, rest, result, prev);
            //PathSum(root.right, rest, result, clone);
        }
        #endregion

        #region 二叉树

        #region 两数相加
        /// <summary>
        /// 给出两个 非空 的链表用来表示两个非负的整数。其中，它们各自的位数是按照 逆序 的方式存储的，并且它们的每个节点只能存储 一位 数字。
        /// 如果，我们将这两个数相加起来，则会返回一个新的链表来表示它们的和。
        /// 您可以假设除了数字 0 之外，这两个数都不会以 0 开头。
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null) return null;
            if (l1 == null) l1 = new ListNode(0);
            if (l2 == null) l2 = new ListNode(0);
            var sum = (l1.val + l2.val) % 10;
            var acd = (l1.val + l2.val) / 10;
            var node = new ListNode(sum);
            if (acd == 1)
            {
                if (l1.next == null)
                    l1.next = new ListNode(0);
                l1.next.val += 1;
            }
            node.next = AddTwoNumbers(l1.next, l2.next);
            return node;
        }
        #endregion

        #endregion

        #region 无重复字符的最长子串
        public int LengthOfLongestSubstring(string s)
        {
            //缺点, 创建n次字符串。//严重
            //var max = 0;
            //var cur = string.Empty;
            //foreach (var c in s)
            //{
            //    if (cur.Contains(c)) cur = cur.Substring(cur.IndexOf(c) + 1);
            //    cur += c;
            //    if (cur.Length > max) max = cur.Length;
            //}
            //return max;

            //②双遍历 + 字典。
            //自写
            var dicChar = new Dictionary<char, int>();
            var max = 0;
            var minIndex = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (dicChar.ContainsKey(s[i]))
                {
                    var removeIndex = dicChar[s[i]];
                    while (minIndex <= removeIndex)
                    {
                        dicChar.Remove(s[minIndex]);
                        minIndex++;
                    }
                }
                dicChar.Add(s[i], i);
                if (max < dicChar.Count)
                    max = dicChar.Count;
            }
            return max;

            //参考
            //if (s == null || s.Length < 1)
            //{
            //    return 0;
            //}
            //var i = 0;
            //var j = 0;
            //var ans = 0;
            //var dict = new Dictionary<char, int>();
            //while (i < s.Length && j < s.Length)
            //{
            //    if (!dict.ContainsKey(s[j]))
            //    {
            //        ans = ans < j - i + 1 ? j - i + 1 : ans;
            //        dict.Add(s[j], j);
            //        j++;
            //    }
            //    else
            //    {
            //        dict.Remove(s[i]);
            //        i++;
            //    }
            //}
            //return ans;
        }
        #endregion

        #region 最长回文子串
        /// <summary>
        /// 最长回文子串
        ///     回文字符串: 正读和反读都相同的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string LongestPalindrome(string s)
        {
            var cur = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                var s1 = GetPalindrome(s, i, i);
                var s2 = GetPalindrome(s, i, i + 1);

                s1 = s1.Length > s2.Length ? s1 : s2;
                if (s1.Length > cur.Length)
                {
                    cur = s1;
                }
            }
            return cur;
        }

        private string GetPalindrome(string s, int left, int right)
        {
            if (right >= s.Length) return string.Empty;
            while (left >= 0 && right < s.Length)
            {
                if (s[left] != s[right])
                {
                    break;
                }
                left--;
                right++;
            }
            left++;
            right--;
            var length = right - left + 1;
            return s.Substring(left, length);
        }
        #endregion

        #region Z字形变换
        /// <summary>
        /// 将一个给定字符串根据给定的行数，以从上往下、从左到右进行 Z 字形排列。
        /// 比如输入字符串为 "LEETCODEISHIRING" 行数为 3 时，排列如下
        /// L   C   I   R
        /// E T O E S I I G
        /// E   D   H   N
        /// 
        /// 输入: s = "LEETCODEISHIRING", numRows = 4
        /// 输出: "LDREOEIIECIHNTSG"
        ///     L     D     R
        ///     E   O E   I I
        ///     E C   I H   N
        ///     T     S     G
        /// </summary>
        /// <param name="s"></param>
        /// <param name="numRows"></param>
        /// <returns></returns>
        public string Convert(string s, int numRows)
        {
            //①用矩阵表示当前形状
            //if (numRows == 1) return s;
            ////二维转换
            //var numCols = (int)Math.Floor(((double)s.Length) / numRows) + 1;
            //var matrix = new char[numRows, s.Length];
            //var lastRow = numRows - 1;
            //var rowIndex = 0;
            //var colIndex = 0;
            //var pos = true;
            ////排列出Z型
            //for (int i = 0; i < s.Length; i++)
            //{
            //    matrix[rowIndex, colIndex] = s[i];
            //    if (rowIndex == lastRow)
            //    {
            //        pos = false;
            //    }
            //    else if (rowIndex == 0)
            //    {
            //        pos = true;
            //    }
            //    if (pos) rowIndex++;
            //    else
            //    {
            //        colIndex++;
            //        rowIndex--;
            //    }
            //}
            ////组合字符串
            //var sb = new StringBuilder(s.Length);
            //foreach (var c in matrix) if (c != '\0') sb.Append(c);
            //return sb.ToString();



            //②用StringBuilder数组表示每一行的当前值(未体现形状)
            if (numRows == 1) return s;
            //二维转换
            var matrix = new StringBuilder[numRows];
            for (int i = 0; i < numRows; i++)
                matrix[i] = new StringBuilder();
            var lastRow = numRows - 1;
            var rowIndex = 0;
            var pos = false;
            //排列出Z型
            foreach (var c in s)
            {
                matrix[rowIndex].Append(c);
                if (rowIndex == lastRow || rowIndex == 0) pos = !pos;
                rowIndex += pos ? 1 : -1;
            }
            //组合字符串
            var sb = new StringBuilder(s.Length);
            foreach (var sbSub in matrix) sb.Append(sbSub);
            return sb.ToString();
        }
        #endregion

        #region 字符串转换整数 (atoi)
        /// <summary>
        /// 请你来实现一个 atoi 函数，使其能将字符串转换成整数。
        /// 首先，该函数会根据需要丢弃无用的开头空格字符，直到寻找到第一个非空格的字符为止。
        /// 当我们寻找到的第一个非空字符为正或者负号时，则将该符号与之后面尽可能多的连续数字组合起来，作为该整数的正负号；假如第一个非空字符是数字，则直接将其与之后连续的数字字符组合起来，形成整数。
        /// 该字符串除了有效的整数部分之后也可能会存在多余的字符，这些字符可以被忽略，它们对于函数不应该造成影响。
        /// 注意：假如该字符串中的第一个非空格字符不是一个有效整数字符、字符串为空或字符串仅包含空白字符时，则你的函数不需要进行转换。
        /// 在任何情况下，若函数不能进行有效的转换时，请返回 0。
        /// 说明：
        /// 假设我们的环境只能存储 32 位大小的有符号整数，那么其数值范围为[−231, 231− 1]。如果数值超过这个范围，qing返回 INT_MAX(231 − 1) 或 INT_MIN(−231) 。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int MyAtoi(string str)
        {
            if (string.IsNullOrEmpty(str)) return 0;
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                if (sb.Length == 0 && (Char.IsWhiteSpace(c) || c == '0')) continue;
                if (sb.Length == 0 && c == '-')
                {
                    sb.Append(c);
                    continue;
                }
                if (!(c >= '0' && c <= '9') || sb.Length > 11) break;
                if (!char.IsWhiteSpace(c)) sb.Append(c);
            }
            long.TryParse(sb.ToString(), out var l);
            if (l > int.MaxValue) l = int.MaxValue;
            else if (l < int.MinValue) l = int.MinValue;
            return (int)l;
        }
        #endregion
    }
}
