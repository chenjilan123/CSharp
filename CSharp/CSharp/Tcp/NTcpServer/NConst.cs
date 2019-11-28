using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Tcp
{
    public class NConst
    {
        /// <summary>
        /// 头标识
        /// </summary>
        internal const byte StartFlag = 0x5B;
        /// <summary>
        /// 尾标识
        /// </summary>
        internal const byte EndFlag = 0x5D;
        /// <summary>
        /// 头标识转义字符
        /// </summary>
        internal const byte StartShiftFlag = 0x5A;
        /// <summary>
        /// 尾标识转义字符
        /// </summary>
        internal const byte EndShiftFlag = 0x5E;

    }
}
