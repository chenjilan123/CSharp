using CSharp.Framework.Face.V2_2.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Framework.Face.V2_2
{
    public static class ASF_API
    {
        #region 激活
        /// <summary>
        /// 用于在线激活 SDK
        ///         注：
        ///             (1) 初次使用 SDK 时需要对 SDK 先进行激活，激活后无需重复调用；
        ///             (2) 调用此接口时必须为联网状态，激活成功后即可离线使用；
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="SDKKey"></param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFOnlineActivation")]
        public static extern int OnlineActivation(string AppId, string SDKKey);
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFActivation")]
        public static extern int Activation(string AppId, string SDKKey);
        #endregion

        #region 获取激活文件信息
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFGetActiveFileInfo")]
        public static extern int GetActiveFileInfo(IntPtr activeFileInfo);
        #endregion

        #region 初始化引擎
        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="detectMode">
        /// VIDEO 模式/IMAGE 模式
        ///     VIDEO 模式:处理连续帧的图像数据
        ///     IMAGE 模式:处理单张的图像数据
        /// </param>
        /// <param name="detectFaceOrientPriority">
        /// 人脸检测角度，推荐单一角度检测;
        /// IMAGE模式下不支持全角度（ASF_OP_0_HIGHER_EXT）检测
        /// </param>
        /// <param name="detectFaceScaleVal">
        /// 识别的最小人脸比例（图片长边与人脸框长边的比值）
        ///     VIDEO 模式取值范围[2,32]，推荐值为 16
        ///     IMAGE 模式取值范围[2,32]，推荐值为 30
        /// </param>
        /// <param name="detectFaceMaxNum">最大需要检测的人脸个数，取值范围[1,50]</param>
        /// <param name="combinedMask">需要启用的功能组合，可多选</param>
        /// <param name="hEngine">引擎句柄</param>
        /// <returns>成功返回 MOK，失败详见 3.2 错误码列表</returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFInitEngine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int InititalEngine(ASF_DetectMode detectMode, ASF_OrientPriority detectFaceOrientPriority, int detectFaceScaleVal, int detectFaceMaxNum, ASF_Operation combinedMask, ref IntPtr hEngine);
        #endregion

        #region 人脸检测
        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="hEngine">引擎句柄</param>
        /// <param name="width">图片宽度，为 4 的倍数</param>
        /// <param name="height">
        ///     图片高度，YUYV/I420/NV21/NV12 格式为 2 的倍数；
        ///     BGR24/GRAY/DEPTH_U16 格式无限制
        /// </param>
        /// <param name="format">颜色空间格式</param>
        /// <param name="imgData">图片数据</param>
        /// <param name="detectedFaces">检测到的人脸信息</param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFDetectFaces")]
        public static extern int DetectFaces(IntPtr hEngine, int width, int height, int format, IntPtr imgData, IntPtr detectedFaces);
        #endregion

        #region 单人脸特征提取
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFFaceFeatureExtract")]
        public static extern int ExtractFeature(IntPtr hEngine, int width, int height, int format, IntPtr imgData, IntPtr faceInfo, IntPtr feature);
        #endregion

        #region 人脸特征比对
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFFaceFeatureCompare")]
        public static extern int Compare(IntPtr hEngine, IntPtr feature1, IntPtr feature2, ref float confidenceLevel);
        #endregion
    }
}
