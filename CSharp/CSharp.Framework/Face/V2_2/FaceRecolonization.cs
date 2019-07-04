using CSharp.Framework.Face.V2_2.Constant;
using CSharp.Framework.Face.V2_2.Data;
using CSharp.Framework.Face.V2_2.Error;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            if (originalImage.Width % 4 != 0)
            {
                originalImage = ImageUtil.ScaleImage(originalImage, originalImage.Width - (originalImage.Width % 4), originalImage.Height);
            }
            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(_hEngine, originalImage);

            if (multiFaceInfo.faceNum <= 0)
            {
                return null;
            }
            var featureLst = new List<byte[]>();
            var rectSize = MemoryUtil.SizeOf<MRECT>();
            var faceAreas = multiFaceInfo.GetFaceArea();
            for (int i = 0; i < multiFaceInfo.faceNum; i++)
            {
                //这种方法, 后面的操作会导致指针返回值出错。
                //var ptr = multiFaceInfo.faceRect + rectSize * i;
                //var rect = new MRECT();
                //rect = Marshal.PtrToStructure<MRECT>(ptr);
                var rect = faceAreas[i];
                var faceImage = ImageUtil.CutImage(originalImage, rect.left, rect.top, rect.right, rect.bottom);
                if (faceImage == null)
                {
                    continue;
                }
                ASF_SingleFaceInfo singleFaceInfo;
                //捕捉脸部信息
                IntPtr ptrFeature = FaceUtil.ExtractFeature(_hEngine, faceImage, out singleFaceInfo);
                var rectCut = singleFaceInfo.faceRect;
                var faceFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(ptrFeature);
                byte[] featureData = new byte[faceFeature.featureSize];
                MemoryUtil.Copy(faceFeature.feature, featureData, 0, featureData.Length);
                featureLst.Add(featureData);
            }
            return featureLst;
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="img1"></param>
        /// <param name="img2"></param>
        /// <param name="confidenceLevel"></param>
        /// <returns></returns>
        public bool Compare(byte[] featureData1, byte[] featureData2, float confidenceLevel)
        {
            var feature1 = new ASF_FaceFeature();
            var feature2 = new ASF_FaceFeature();
            feature1.feature = MemoryUtil.Malloc(featureData1.Length);
            feature1.featureSize = featureData1.Length;
            feature2.feature = MemoryUtil.Malloc(featureData2.Length);
            feature2.featureSize = featureData2.Length;
            MemoryUtil.Copy(featureData1, 0, feature1.feature, featureData1.Length);
            MemoryUtil.Copy(featureData2, 0, feature2.feature, featureData2.Length);
            IntPtr p1 = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
            IntPtr p2 = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
            MemoryUtil.StructureToPtr<ASF_FaceFeature>(feature1, p1);
            MemoryUtil.StructureToPtr<ASF_FaceFeature>(feature2, p2);
            var outLevel = 0.0F;
            var result = ASF_API.Compare(_hEngine, p1, p2, ref outLevel);
            if ((int)ASF_ErrorCode.MOK != result)
            {
                return false;
            }
            Console.WriteLine($"相似度: {outLevel}");
            return outLevel >= confidenceLevel;
        }

    }
}
