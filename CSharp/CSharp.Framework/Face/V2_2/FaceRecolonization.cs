using CSharp.Framework.Face.V2_2.Constant;
using CSharp.Framework.Face.V2_2.Error;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face.V2_2
{
    public class FaceRecolonization : IArcFace
    {
        private IntPtr _hEngine;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="sdkKey"></param>
        /// <returns></returns>
        public bool Initialize(string appId, string sdkKey)
        {
            var activeResult = ASF_API.OnlineActivation(appId, sdkKey);
            if ((int)ASF_ErrorCode.MOK != activeResult && (int)ASF_ErrorCode.MERR_ASF_ALREADY_ACTIVATED != activeResult)
            {
                return false;
            }
            var detectMode = ASF_DetectMode.Image;
            var detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_ONLY;
            var detectFaceScaleVal = 30;
            var detectFaceMaxNum = 50;
            var combinedMask = ASF_Operation.ASF_FACE_DETECT | ASF_Operation.ASF_FACERECOGNITION;
            var initResult = ASF_API.InititalEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref _hEngine);
            if ((int)ASF_ErrorCode.MOK != initResult)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取脸部特征值
        /// </summary>
        /// <param name="originalImage"></param>
        /// <returns></returns>
        public List<byte[]> GetFaceFeature(Image originalImage)
        {
            //查找脸部位置
            
            //捕捉脸部信息


            return null;
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="img1"></param>
        /// <param name="img2"></param>
        /// <param name="confidenceLevel"></param>
        /// <returns></returns>
        public bool Compare(byte[] feature1, byte[] feature2, float confidenceLevel)
        {
            return true;
        }

    }
}
