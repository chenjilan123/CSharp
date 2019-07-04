using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face.V2_2.Data
{
    /// <summary>
    /// 人脸特征结构体
    /// </summary>
    public struct ASF_FaceFeature
    {
        /// <summary>
        /// 特征值 byte[]
        /// </summary>
        public IntPtr feature;

        /// <summary>
        /// 结果集大小
        /// </summary>
        public int featureSize;
    }
}
