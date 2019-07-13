//#define Version_2_0

using CSharp.Framework.Face.V2_2;
using CSharp.Framework.Face.V2_2.Constant;
using CSharp.Framework.Face.V2_2.Data;
using CSharp.Framework.Face.V2_2.Error;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace CSharp.Framework.Face
{
    public class ArcFaceTest
    {
        private IArcFace _faceApi;

        public ArcFaceTest(IArcFace faceApi)
        {
            _faceApi = faceApi;
        }

        public void Run()
        {
            //this.TestExternalMethod();

            Console.WriteLine($"Is64BitOperatingSystem: {Environment.Is64BitOperatingSystem}");
            Console.WriteLine($"        Is64BitProcess: {Environment.Is64BitProcess}");

            this.TestSelfApi();

            //Console.WriteLine("按Enter键退出。");
            Console.ReadLine();
        }

        #region TestSelfApi
        void TestSelfApi()
        {
            if (_faceApi.Initialize(ASF_ApiKey.AppId, ASF_ApiKey.SDKKey_V2_2_x64))
            {
                Console.WriteLine("初始化引擎成功");
            }
            else
            {
                Console.WriteLine("初始化引擎失败");
            }

            //var featureLst = _faceApi.GetFaceFeature(GetImage());
            //if (featureLst == null || featureLst.Count <= 0)
            //{
            //    Console.WriteLine("捕捉脸部特征值失败");
            //    return;
            //}
            //Console.WriteLine($"捕捉脸部特征值成功, 脸数: {featureLst.Count}");
            //foreach (var feature in featureLst)
            //{
            //    Console.WriteLine($"\t特征长度: {feature.Length}");
            //}

            //var person1 = cr7_1;
            //var person2 = cr7_2;
            //var person1 = cr7_2;
            //var person2 = cr7_1;
            var person1 = m10_2;
            var person2 = errorImg;
            //var person2 = m10_2;

            var feature1 = _faceApi.GetFaceFeature(GetImage(person1))[0];
            var feature2 = _faceApi.GetFaceFeature(GetImage(person2))[0];

            var confidenceLevel = _faceApi.CompareFace(feature1, feature2);
            if (0.4F <= confidenceLevel)
            {
                Console.WriteLine("通过");
            }
            else
            {
                Console.WriteLine("不通过");
            }
        }
        #endregion

        #region 测试外部方法
        /// <summary>
        /// 测试外部方法
        /// </summary>
        void TestExternalMethod()
        {
            Console.WriteLine($"处理器是否为64位架构: {Environment.Is64BitProcess}");

#if Version_2_0
            var activeResult = ASF_API.Activation(ASF_ApiKey.AppId, ASF_ApiKey.SDKKey_V2_2_x64);
#else
            //激活
            var activeResult = ASF_API.OnlineActivation(ASF_ApiKey.AppId, ASF_ApiKey.SDKKey_V2_2_x64);
#endif
            //已激活也是成功
            if ((int)ASF_ErrorCode.MOK == activeResult || (int)ASF_ErrorCode.MERR_ASF_ALREADY_ACTIVATED == activeResult)
                Console.WriteLine("激活成功");
            else
                Console.WriteLine($"激活失败, 错误码: {activeResult}");
            //初始化引擎
            var detectMode = ASF_DetectMode.Image;
            //var detectMode = ASF_DetectMode.Voide;
            //var detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_ONLY;
            //var detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_90_ONLY;
#if Version_2_0
            var detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
#else
            var detectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_ONLY;
#endif
            var detectFaceScaleVal = 16;
            var detectFaceMaxNum = 20;
            var combinedMask = ASF_Operation.ASF_FACE_DETECT | ASF_Operation.ASF_FACERECOGNITION;
            //var combinedMask = ASF_Operation.ASF_FACE_DETECT
            //    | ASF_Operation.ASF_FACERECOGNITION
            //    | ASF_Operation.ASF_AGE
            //    | ASF_Operation.ASF_GENDER
            //    | ASF_Operation.ASF_FACE3DANGLE
            //    | ASF_Operation.ASF_LIVENESS
            //    | ASF_Operation.ASF_IR_LIVENESS;
            var hEngine = IntPtr.Zero;
            var initResult = ASF_API.InititalEngine(detectMode, detectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref hEngine);
            if ((int)ASF_ErrorCode.MOK == initResult)
                Console.WriteLine("初始化成功");
            else
                Console.WriteLine($"初始化失败, 错误码: {initResult}");

#if !Version_2_0
            //激活文件信息
            var ptrActiveFileInfo = Marshal.AllocHGlobal(MemoryUtil.SizeOf<ASF_ActiveFileInfo>());
            var activeFileInfoResult = ASF_API.GetActiveFileInfo(ptrActiveFileInfo);
            if ((int)ASF_ErrorCode.MOK == activeFileInfoResult)
            {
                Console.WriteLine("获取激活文件信息成功");
                var activeFileInfos = MemoryUtil.PtrToStructure<ASF_ActiveFileInfo>(ptrActiveFileInfo);
                activeFileInfos.PrintInfo();
            }
            else
            {
                Console.WriteLine($"获取激活文件信息失败, 错误码: {activeFileInfoResult}");
            }
#endif
            //捕捉脸部位置
            var detectedFaces = IntPtr.Zero;
            detectedFaces = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_MultiFaceInfo>());
            var img = GetImage();
            var imgInfo = GetImageInfo(img);

            var detectFacesResult = ASF_API.DetectFaces(hEngine, imgInfo.width, imgInfo.height, imgInfo.format, imgInfo.imgData, detectedFaces);
            if ((int)ASF_ErrorCode.MOK != detectFacesResult)
            {
                Console.WriteLine($"捕捉脸部位置失败, 错误码: {detectFacesResult}");
                return;
            }
            Console.WriteLine("捕捉脸部位置成功");
            var faceInfo = MemoryUtil.PtrToStructure<ASF_MultiFaceInfo>(detectedFaces);
            //var faceInfo = (ASF_MultiFaceInfo)Marshal.PtrToStructure(detectedFaces, typeof(ASF_MultiFaceInfo));
            faceInfo.PrintInfo();
            //提取脸部信息
            var rectSize = MemoryUtil.SizeOf<MRECT>();
            var imgPathLst = new List<string>();
            for (int i = 0; i < faceInfo.faceNum; i++)
            {
                var rect = MemoryUtil.PtrToStructure<MRECT>(faceInfo.faceRect + rectSize * i);
                var faceImg = ImageUtil.CutImage(img, rect.left, rect.top, rect.right, rect.bottom);
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), $"Face\\{DateTime.Now.ToString("yyMMddHHmmss")}.jpg");
                faceImg.Save(fileName);
                Console.WriteLine($"保存脸部图片: {fileName}");
                imgPathLst.Add(fileName);
            }

            ASF_SingleFaceInfo singleFaceInfo;
            IntPtr ptrFeature = FaceUtil.ExtractFeature(hEngine, Image.FromFile(imgPathLst[0]), out singleFaceInfo);
            var faceFeature = MemoryUtil.PtrToStructure<ASF_FaceFeature>(ptrFeature);
            Console.WriteLine(faceFeature.featureSize);

            //比较脸部信息
            float compareLevel = 0.0F;
            var compareResult = ASF_API.Compare(hEngine, ptrFeature, ptrFeature, ref compareLevel);
            if ((int)ASF_ErrorCode.MOK != compareResult)
            {
                Console.WriteLine("比较脸部信息失败");
                return;
            }
            else
            {
                Console.WriteLine($"比较脸部信息成功, 相似度: {compareLevel}");
            }

        }
        #endregion

        const string errorImg = "510223198207153111.jpg";
        const string cr7_1 = "cr7_1.jpg";
        const string cr7_2 = "cr7_2.jpg";
        const string cr7_3 = "cr7_3.jpg";
        const string cr7_4 = "cr7_4.jpg";
        const string m10_1 = "m10_1.jpg";
        const string m10_2 = "m10_2.jpg";
        const string m10_3 = "m10_3.jpg";
        const string RM_1 = "RM_1.jpg";
        const string HG_1 = "HG_1.jpg";
        private Image GetImage()
        {
            return Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), $@"Face\Image\{RM_1}"));
        }
        private Image GetImage(string fileName)
        {
            return Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), $@"Face\Image\{fileName}"));
        }

        private ImageInfo GetImageInfo(Image img)
        {
            ImageUtil.ScaleImage(img, img.Width, img.Height);
            var imgInfo = ImageUtil.ReadBMP(img);
            return imgInfo;
        }
        private byte[] ReadBmp(Bitmap image, ref int width, ref int height, ref int pitch)
        {
            try
            {
                //将Bitmap锁定到系统内存中,获得BitmapData
                BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                //位图中第一个像素数据的地址。它也可以看成是位图中的第一个扫描行
                IntPtr ptr = data.Scan0;
                //定义数组长度
                int soureBitArrayLength = data.Height * Math.Abs(data.Stride);
                byte[] sourceBitArray = new byte[soureBitArrayLength];
                //将bitmap中的内容拷贝到ptr_bgr数组中
                Marshal.Copy(ptr, sourceBitArray, 0, soureBitArrayLength);
                width = data.Width;
                height = data.Height;
                pitch = Math.Abs(data.Stride);
                int line = width * 3;
                int bgr_len = line * height;
                byte[] destBitArray = new byte[bgr_len];
                for (int i = 0; i < height; ++i)
                {
                    Array.Copy(sourceBitArray, i * pitch, destBitArray, i * line, line);
                }
                pitch = line;
                image.UnlockBits(data);
                return destBitArray;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
