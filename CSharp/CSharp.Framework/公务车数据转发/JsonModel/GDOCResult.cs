using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Framework.Transfer.JsonModel
{
    public class GDOCResult
    {
        public string Cmd { get; set; }
        /// <summary>
        /// 结果: 1-成功, 其他-失败
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 成功或失败原因
        /// </summary>
        public string Remark { get; set; }


        public virtual string GetDescription()
        {
            return Result == 1 ? "成功" : "失败";
        }
    }
}
