using CSharp.Model;
using System;
using System.Collections;
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
                while (curStr.Length > current && first.Length > current && curStr[current] == first[current]) current++;
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

        #region 存在重复元素
        /// <summary>
        /// 给定一个整数数组，判断是否存在重复元素。
        /// 如果任何值在数组中出现至少两次，函数返回 true。如果数组中每个元素都不相同，则返回 false。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public bool ContainsDuplicate(int[] nums)
        {
            //①用列表: 超时
            //var list = new List<int>();
            //foreach (var num in nums)
            //{
            //    if (list.Contains(num)) return true;
            //    list.Add(num);
            //}
            //return false;

            //②用哈希表
            var hash = new Hashtable();
            foreach (var num in nums)
            {
                if (hash.Contains(num)) return true;
                hash.Add(num, 0);
            }
            return false;
        }

        /// <summary>
        /// 参考
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public bool ContainsDuplicateAdvance(int[] nums)
        {
            var hash = new HashSet<int>();
            foreach (var num in nums)
            {
                if (hash.Contains(num)) return true;
                hash.Add(num);
            }
            return false;
        }
        #endregion

        #region 存在重复元素II
        /// <summary>
        /// 给定一个整数数组和一个整数 k，判断数组中是否存在两个不同的索引 i 和 j，使得 nums [i] = nums [j]，并且 i 和 j 的差的绝对值最大为 k。
        /// 
        /// 关键: 使用字典, 建立[值-索引]关系。
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            //双遍历, 找出所有匹配的组合。
            //int dupNum = 0;
            //for (int i = 0; i < nums.Length; i++)
            //    for (int j = i + 1; j <= i + k && j < nums.Length; j++)
            //        if (nums[i] == nums[j]) dupNum++;
            //return dupNum >= 1;

            //双遍历, 找出一个符合要求的即返回true: 超时
            //for (int i = 0; i < nums.Length; i++)
            //    for (int j = i + 1; j <= i + k && j < nums.Length; j++)
            //        if (nums[i] == nums[j]) return true;
            //return false;

            //保存k个值。
            //遍历数组, 若缓存中包含当前项, 说明正确。
            //超时
            //var lst = new List<int>();
            //foreach (var num in nums)
            //{
            //    if (lst.Contains(num)) return true;
            //    lst.Add(num);
            //    if (lst.Count > k) lst.RemoveAt(0);
            //}
            //return false;

            //用数组保存k个值: 超时
            //if (k == 0) return false;
            //var arr = new int[k];
            //for (int i = 0; i < k; i++)
            //    arr[i] = int.MinValue;
            //var index = 0;
            //foreach (var num in nums)
            //{
            //    if (arr.Contains(num)) return true;
            //    arr[index] = num;
            //    if (++index == k)
            //        index = 0;
            //}
            //return false;

            //建立[值-索引]字典, 搜索很快。
            var dic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]) && i - dic[nums[i]] <= k) return true;
                dic[nums[i]] = i;
            }
            return false;
        }

        public bool ContainsNearbyDuplicateAdvance(int[] nums, int k)
        {
            //用字典, 比用列表，数组快得多。
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > k) set.Remove(nums[i - k - 1]);
                if (!set.Add(nums[i])) return true;
            }
            return false;
        }
        #endregion

        //满足某种条件的数
        #region 计数质数
        /// <summary>
        /// 统计所有小于非负整数 n 的质数的数量。
        ///     一、不统计偶数
        ///     二、统计时，只比较该数/2以下的数。
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int CountPrimes(int n)
        {
            /// 面向测试编程
            /// 写排名第一的那个答案的真他娘的是个人才，把特么20个测试用例全部写进去了。牛逼。
            /// 其实这也是一种思路，如果这种运算非常频繁的，而内存又比较充足，这种方法就是非常好的方式
            if (n <= 2) return 0;
            var hsPrime = new HashSet<int>();
            hsPrime.Add(2);
            //不统计偶数
            for (int i = 3; i < n; i += 2)
            {
                var max = (int)Math.Sqrt(i);
                var bIsPrime = true;
                foreach (var prime in hsPrime)
                {
                    if (prime > max) continue;
                    if (i % prime == 0)
                    {
                        bIsPrime = false;
                        break;
                    }
                }
                if (bIsPrime) hsPrime.Add(i);
            }
            return hsPrime.Count;
        }

        /// <summary>
        /// 厄拉多塞筛法. 比如说求20以内质数的个数,首先0,1不是质数.2是第一个质数,然后把20以内所有2的倍数划去.2后面紧跟的数即为下一个质数3,
        /// 然后把3所有的倍数划去.3后面紧跟的数即为下一个质数5,再把5所有的倍数划去.以此类推.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int CountPrimesAdvance(int n)
        {
            //var hsNoPrime = new HashSet<int>();
            //var hsPrime = new HashSet<int>();
            //var max = (int)Math.Sqrt(n);
            //for (int i = 2; i < n; i++)
            //{
            //    if (!hsNoPrime.Contains(i))
            //    {
            //        hsPrime.Add(i);
            //        if (i <= max)
            //        {
            //            AddNoPrime(hsNoPrime, i, n);
            //        }
            //    }
            //}
            //return hsPrime.Count;

            //可以不用统计质数值
            // =>

            //引用
            bool[] arr = new bool[n];
            int count = 0;
            for (int i = 2; i < n; ++i)
                if (!arr[i])
                {
                    ++count;
                    for (int j = i; j < n; j += i) arr[j] = true;
                }
            return count;
        }
        private void AddNoPrime(HashSet<int> hsNoPrime, int prime, int max)
        {
            var noPrime = prime * 2;
            while (noPrime < max)
            {
                hsNoPrime.Add(noPrime);
                noPrime += prime;
            }
        }
        #endregion

        #region 丑数
        /// <summary>
        /// 编写一个程序判断给定的数是否为丑数。
        /// 丑数就是只包含质因数 2, 3, 5 的整数。
        /// 1也是丑数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool IsUgly(int num)
        {
            if (num <= 0) return false; //测试用例12, 零是正整数？
            var arr = new[] { 2, 3, 5 };
            for (int i = 0; i < arr.Length; i++)
                while (num % arr[i] == 0) num /= arr[i];
            return num == 1;
        }
        #endregion
        //===============

        #region 移除链表元素
        /// <summary>
        /// 删除链表中等于给定值 val 的所有节点。
        ///     示例： 
        ///         输入: 1->2->6->3->4->5->6, val = 6
        ///         输出: 1->2->3->4->5
        /// </summary>
        /// <param name="head"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public ListNode RemoveElements(ListNode head, int val)
        {
            head = new ListNode(0) { next = head };
            var prev = head;
            var cur = prev.next;
            while (cur != null)
            {
                if (cur.val == val) prev.next = cur.next;
                else prev = cur;
                cur = cur.next;
            }
            return head.next;
        }


        /// <summary>
        /// 参考：递归
        /// </summary>
        /// <param name="head"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public ListNode RemoveElementsAdvance(ListNode head, int val)
        {
            if (head == null) return head;
            if (head.val == val) return RemoveElementsAdvance(head.next, val);
            head.next = RemoveElementsAdvance(head.next, val);
            return head;
        }
        #endregion

        #region 反转链表
        /// <summary>
        /// 反转一个单链表。
        ///     分析:https://leetcode-cn.com/articles/reverse-linked-list/
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public ListNode ReverseList(ListNode head)
        {
            //if (head == null) return null;
            //var reverse = new ListNode(head.val);
            //while (head.next != null)
            //{
            //    reverse = new ListNode(head.next.val) { next = reverse };
            //    head = head.next;
            //}
            //return reverse;

            if (head == null) return null;
            ListNode prev = null;
            ListNode next = head.next;
            while (next != null)
            {
                head.next = prev;
                prev = head;
                head = next;
                next = head.next;
            }
            head.next = prev;
            return head;
        }

        public ListNode ReverseListAdvance(ListNode head)
        {
            return Reverse(head, null);
        }
        /// <summary>
        /// 递归, 节约了对象资源
        /// </summary>
        /// <param name="node"></param>
        /// <param name="prev"></param>
        /// <returns></returns>
        private ListNode Reverse(ListNode node, ListNode prev)
        {
            if (node == null)
                return prev;
            ListNode next = node.next;
            node.next = prev;
            return Reverse(next, node);
        }
        #endregion

        #region 最大子序和
        /// <summary>
        /// 给定一个整数数组 nums ，找到一个具有最大和的连续子数组（子数组最少包含一个元素），返回其最大和。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MaxSubArray(int[] nums)
        {
            return MaxSubArrayDirect(nums);
        }

        /// <summary>
        /// 直接法
        /// </summary>
        public int MaxSubArrayDirect(int[] nums)
        {
            var max = nums[0];
            var sum = 0;
            for (int len = 1; len <= nums.Length; len++)
            {
                var end = nums.Length - len;
                for (int i = 0; i <= end; i++)
                {
                    var j = 0;
                    while (j < len)
                    {
                        sum += nums[j + i];
                        j++;
                    }
                    if (sum > max)
                    {
                        max = sum;
                    }
                    sum = 0;
                }
            }
            return max;
        }

        /// <summary>
        /// 分治法
        ///     不考虑溢出
        ///     时间复杂度: O(nlogn)
        ///     空间复杂度：I(n)?
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MaxSubArrayAdvance(int[] nums)
        {
            if (nums.Length == 1) return nums[0];
            var l = MaxSubArrayAdvance(nums.Take(nums.Length / 2).ToArray());
            var r = MaxSubArrayAdvance(nums.Skip(nums.Length / 2).ToArray());

            var sum = 0;
            var ml = nums[nums.Length / 2 - 1];
            for (int i = nums.Length / 2 - 1; i >= 0; i--)
            {
                sum += nums[i];
                ml = Math.Max(ml, sum);
            }
            sum = 0;
            var mr = nums[nums.Length / 2];
            for (int i = nums.Length / 2; i < nums.Length; i++)
            {
                sum += nums[i];
                mr = Math.Max(mr, sum);
            }
            return Math.Max(Math.Max(l, r), ml + mr);
        }

        /// <summary>
        /// 参考
        ///     时间复杂度: O(n)
        ///     空间复杂度：I(l)?
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MaxSubArrayRef(int[] nums)
        {
            //int max = nums[0], val = 0;
            //for (int i = 0; i < nums.Length; i++)
            //{
            //    val = val + nums[i];
            //    max = val > max ? val : max;
            //    val = 0 > val ? 0 : val;
            //}
            //return max;

            int max = nums[0], pre = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                pre = Math.Max(pre + nums[i], nums[i]);
                max = Math.Max(max, pre);
            }
            return max;
        }
        #endregion

        //二叉树
        #region 对称二叉树
        /// <summary>
        /// 给定一个二叉树，检查它是否是镜像对称的。
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public bool IsSymmetric(TreeNode root)
        {
            //递归
            //  时间复杂度：O(n)
            //  空间复杂度：O(n)
            if (root == null) return true;
            return IsSymmetric(root.left, root.right);
            //参考
            //少一行代码，多了一倍运算（多一层）
            //return IsSymmetric(root, root);
        }

        public bool IsSymmetric(TreeNode left, TreeNode right)
        {
            //考虑重写Equal或"=="，用于对称判断
            if (left == null || right == null || left.val != right.val) return left == right;
            return IsSymmetric(left.left, right.right) && IsSymmetric(left.right, right.left);

        }
        #endregion

        #region 路径总和
        /// <summary>
        /// 给定一个二叉树和一个目标和，判断该树中是否存在根节点到叶子节点的路径，这条路径上所有节点值相加等于目标和。
        ///  说明: 叶子节点是指没有子节点的节点
        ///  示例: 
        ///     给定如下二叉树，以及目标和 sum = 22
        ///                   5
        ///                  / \
        ///                 4   8
        ///                /   / \
        ///               11  13  4
        ///              /  \      \
        ///             7    2      1
        ///    
        ///     只包含根节点的总和不算
        /// </summary>
        /// <param name="root"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public bool HasPathSum(TreeNode root, int sum)
        {
            //递归
            if (root == null) return false;
            if (root.left == null && root.right == null) return sum == root.val;
            sum -= root.val;
            return HasPathSum(root.left, sum)
                || HasPathSum(root.right, sum);
        }
        #endregion
        //==============
    }
}
