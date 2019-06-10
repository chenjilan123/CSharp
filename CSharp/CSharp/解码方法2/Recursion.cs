using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.DecodingMethod
{
    public class Recursion
    {
        const int model = 1000000000 + 7;

        public int NumDecodings(string s)
        {
            return (int)Compute(0, s);
        }

        /// <summary>
        /// 递归法计算
        ///     缺点: 时间复杂度大
        /// </summary>
        /// <param name="cur"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        //private long Compute(int cur, string s)
        private long Compute(int cur, string s)
        {
            //if (cur == s.Length - 1) return s[cur] == '*' ? 9 : 1; 
            if (cur >= s.Length) return 1; //递归结束条件
            int combine = 0;
            int single = 1;
            switch (s[cur])
            {
                case '0': return 0;
                case '1': combine = CombineOne(cur, s); break;
                case '2': combine = CombineTwo(cur, s); break;
                case '*': combine = CombineOne(cur, s) + CombineTwo(cur, s); single = 9; break;
                default: break;
            }
            //此处var会自动匹配到long。
            //若result为int, 会匹配为int, 因为递归操作。
            var result = single * Compute(cur + 1, s) + (combine == 0 ? 0 : combine * Compute(cur + 2, s));
            if (result >= model) result %= model;
            return result;
        }

        /// <summary>
        /// 组合1
        /// </summary>
        /// <param name="cur"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        private int CombineOne(int cur, string s)
        {
            if (cur + 1 >= s.Length) return 0;
            if (s[cur + 1] == '*') return 9;
            return 1;
        }

        /// <summary>
        /// 组合2
        ///     只返回能结合的可能情况, 不能结合就返回0
        /// </summary>
        /// <param name="cur"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        private int CombineTwo(int cur, string s)
        {
            if (cur + 1 >= s.Length) return 0;
            if (s[cur + 1] == '*') return 6;
            if (s[cur + 1] > '6') return 0;
            return 1;
        }

        //注: 
        //var v1 = single * Compute(cur + 1, s);
        ////var v2 = 1 + combine == 0 ? 0 : combine * Compute(cur + 2, s); //任何数+(?:)都等于(?:)的结果, 不是预期的。
        //var v2 = combine == 0 ? 0 : combine * Compute(cur + 2, s);
        //return v1 + v2;
    }
}
