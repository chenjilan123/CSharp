﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    public class Difficulty
    {
        #region 天际线问题
        /// <summary>
        /// 天际线问题
        ///     城市的天际线是从远处观看该城市中所有建筑物形成的轮廓的外部轮廓。现在，假设您获得了城市风光照片（图A）上显示的所有建筑物的位置和高度，请编写一个程序以输出由这些建筑物形成的天际线（图B）。
        ///     每个建筑物的几何信息用三元组 [Li，Ri，Hi] 表示，其中 Li 和 Ri 分别是第 i 座建筑物左右边缘的 x 坐标，Hi 是其高度。可以保证 0 ≤ Li, Ri ≤ INT_MAX, 0 < Hi ≤ INT_MAX 和 Ri - Li > 0。您可以假设所有建筑物都是在绝对平坦且高度为 0 的表面上的完美矩形。
        ///     例如，图A中所有建筑物的尺寸记录为：[ [2 9 10], [3 7 15], [5 12 12], [15 20 10], [19 24 8] ] 。
        ///     输出是以[[x1, y1], [x2, y2], [x3, y3], ... ] 格式的“关键点”（图B中的红点）的列表，它们唯一地定义了天际线。关键点是水平线段的左端点。请注意，最右侧建筑物的最后一个关键点仅用于标记天际线的终点，并始终为零高度。此外，任何两个相邻建筑物之间的地面都应被视为天际线轮廓的一部分。
        ///     例如，图B中的天际线应该表示为：[ [2 10], [3 15], [7 12], [12 0], [15 10], [20 8], [24, 0] ]。
        /// ref: https://leetcode-cn.com/problems/the-skyline-problem/
        /// </summary>
        /// <param name="buildings"></param>
        /// <returns></returns>
        public IList<IList<int>> GetSkyline(int[][] buildings)
        {
            var skyline = new Buildings();
            foreach (var building in buildings)
                skyline.AddBuilding(building);
            return skyline.GetSkyline();
        }

        public class Buildings
        {
            #region 天际线
            /// <summary>
            /// 建筑粒子化后的
            /// </summary>
            Dictionary<int, int> _dicBuildingUnit;
            public IList<IList<int>> GetSkyline()
            {
                var skyline = new List<IList<int>>();
                var prevX = -2;
                var prevH = 0;
                foreach (var buildingUnit in _dicBuildingUnit)
                {
                    if (buildingUnit.Key > prevX + 1) skyline.Add(new[] { prevX + 1, 0 });
                    if (prevH != buildingUnit.Value) skyline.Add(new[] { buildingUnit.Key, buildingUnit.Value });
                    prevX = buildingUnit.Key;
                    prevH = buildingUnit.Value;
                }
                skyline.Add(new[] { prevX + 1, 0 });
                skyline.RemoveAt(0); //多了一个1, 0。
                return skyline;
            }
            #endregion

            #region 其他

            #endregion

            #region 构造函数
            public Buildings()
            {
                _dicBuildingUnit = new Dictionary<int, int>();
            }
            #endregion

            #region 新增建筑
            /// <summary>
            /// 新增建筑
            /// </summary>
            /// <param name="building">[0]:L, [1]:R, [2]: H</param>
            public void AddBuilding(int[] building)
            {
                //绘制天际线
                ///思路:
                ///  把建筑按横坐标粒子化
                ///      若当前坐标无建筑 => 新增
                ///      若当前坐标有建筑 => 高度小于 => 替换
                ///                         高度大于 => 下一项
                var h = building[2]; //间接访问building[2]和直接访问h速度差异大吗？
                for (int i = building[0]; i < building[1]; i++)
                    if (!_dicBuildingUnit.ContainsKey(i) || _dicBuildingUnit[i] < h) _dicBuildingUnit[i] = h;
            }
            #endregion
        }
        #endregion

        #region 解码方法 2
        Dictionary<int, char> dicDecoding = new Dictionary<int, char>
        {
            [1] = 'A',
            [2] = 'B',
            [3] = 'C',
            [4] = 'D',
            [5] = 'E',
            [6] = 'F',
            [7] = 'G',
            [8] = 'H',
            [9] = 'I',
            [10] = 'J',
            [11] = 'K',
            [12] = 'L',
            [13] = 'M',
            [14] = 'N',
            [15] = 'O',
            [16] = 'P',
            [17] = 'Q',
            [18] = 'R',
            [19] = 'S',
            [20] = 'T',
            [21] = 'U',
            [22] = 'V',
            [23] = 'W',
            [24] = 'X',
            [25] = 'Y',
            [26] = 'Z',
        };
        Dictionary<char, int> dicEncoding = new Dictionary<char, int>
        {
            ['A'] = 1,
            ['B'] = 2,
            ['C'] = 3,
            ['D'] = 4,
            ['E'] = 5,
            ['F'] = 6,
            ['G'] = 7,
            ['H'] = 8,
            ['I'] = 9,
            ['J'] = 10,
            ['K'] = 11,
            ['L'] = 12,
            ['M'] = 13,
            ['N'] = 14,
            ['O'] = 15,
            ['P'] = 16,
            ['Q'] = 17,
            ['R'] = 18,
            ['S'] = 19,
            ['T'] = 20,
            ['U'] = 21,
            ['V'] = 22,
            ['W'] = 23,
            ['X'] = 24,
            ['Y'] = 25,
            ['Z'] = 26,
        };

        /// <summary>
        /// 一条包含字母 A-Z 的消息通过以下的方式进行了编码：
        ///     'A' -> 1
        ///     'B' -> 2
        ///     ...
        ///     'Z' -> 26
        /// 除了上述的条件以外，现在加密字符串可以包含字符 '*'了，字符'*'可以被当做1到9当中的任意一个数字。
        /// 
        /// 给定一条包含数字和字符'*'的加密信息，请确定解码方法的总数。
        /// 
        /// 同时，由于结果值可能会相当的大，所以你应当对109 + 7取模。（翻译者标注：此处取模主要是为了防止溢出）
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int NumDecodings(string s)
        {
            return new DecodingMethod.Recursion().NumDecodings(s);
        }

        #endregion

        #region 寻找两个有序数组的中位数
        /// <summary>
        /// 在统计中，中位数被用来：
        ///  将一个集合划分为两个长度相等的子集，其中一个子集中的元素总是大于另一个子集中的元素。
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
            {
                var temp = nums1;
                nums1 = nums2;
                nums2 = temp;
            }
            var m = nums1.Length;
            var n = nums2.Length;

            var i = 0;
            var j = 0;
            var min = 0;
            var max = m;
            while(true)
            {
                i = (min + max) / 2;
                j = (m + n + 1) / 2 - i;
                if (i > min && nums1[i - 1] > nums2[j])
                {
                    max = i - 1;
                }
                else if (i < max && nums2[j - 1] > nums1[i])
                {
                    min = i + 1;
                }
                else
                {
                    break;
                }
            }
            var left = 0;
            if (i == 0) left = nums2[j - 1];
            else if (j == 0) left = nums1[i - 1];
            else left = Math.Max(nums1[i - 1], nums2[j - 1]);

            if ((m + n) % 2 == 1)
            {
                return left;
            }
            else
            {
                var right = 0;
                if (i == m) right = nums2[j];
                else if (j == n) right = nums1[i];
                else right = Math.Min(nums1[i], nums2[j]);
                return ((double)(left + right)) / 2;
            }
        }
        #endregion
    }
}
