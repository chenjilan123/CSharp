using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face.V2_2
{
    public interface IArcFace
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="sdkKey"></param>
        /// <returns></returns>
        bool Initialize(string appId, string sdkKey);

        /// <summary>
        /// 获取脸部特征值
        /// </summary>
        /// <param name="originalImage">原始图片</param>
        /// <returns>特征值列表, 每张脸对应一项</returns>
        List<byte[]> GetFaceFeature(Image originalImage);

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="feature1">特征值1</param>
        /// <param name="feature2">特征值2</param>
        /// <param name="confidenceLevel">相似度, 取值范围[0,1]</param>
        /// <returns></returns>
        bool Compare(byte[] feature1, byte[] feature2, float confidenceLevel);
    }
}
