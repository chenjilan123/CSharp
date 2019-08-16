using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face.V2_2.Constant
{
    public enum ASF_OrientPriority
    {
        ASF_OP_0_ONLY = 0x1, // 仅检测 0 度
        ASF_OP_90_ONLY = 0x2, // 仅检测 90 度
        ASF_OP_270_ONLY = 0x3, // 仅检测 270 度
        ASF_OP_180_ONLY = 0x4, // 仅检测 180 度
        ASF_OP_0_HIGHER_EXT = 0x5, // 全角度检测

        //ASF_OC_0 = 0x1,
        //ASF_OC_90 = 0x1,
        //ASF_OC_270 = 0x1,
        //ASF_OC_180 = 0x1,
        //ASF_OC_0 = 0x1,
        //ASF_OC_0 = 0x1,
    }
}
