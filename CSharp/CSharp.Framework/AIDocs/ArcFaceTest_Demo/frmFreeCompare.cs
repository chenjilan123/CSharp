using ArcFaceTest.AFD;
using ArcFaceTest.AFR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ArcFaceTest
{
    public partial class frmFreeCompare : Form
    {
        private String FaceLibraryPath = Environment.CurrentDirectory;//"K:/VS_Projects/WinForm/ArcFaceTest/bin/Debug/";

        public frmFreeCompare()
        {
            InitializeComponent();
        }

        private void btnLoadFree2_Click(object sender, EventArgs e)
        {
            Image img = OpenImg();
            if (img == null)
                return;
            pbFree2.Image = img;

        }

        private Image OpenImg()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png|所有文件|*.*;";
            openFile.Multiselect = false;
            openFile.FileName = "";
            if (openFile.ShowDialog() != DialogResult.OK)
                return null;

            if (string.IsNullOrEmpty(openFile.FileName))
                return null;
            Image img = Image.FromFile(openFile.FileName);
            if (img == null)
            {
                MessageBox.Show("图片获取失败");
                return null;
            }
            return img;
        }

        private void btnLoadFree1_Click(object sender, EventArgs e)
        {
            Image img = OpenImg();
            if (img == null)
                return;
            pbFree1.Image = img;

        }

        List<byte[]> listFeature1 = null;
        List<byte[]> listFeature2 = null;

        private void btnGetFree1_Click(object sender, EventArgs e)
        {
            InitArcFace();
            List<Image> listImg = null;
            listFeature1 = null;
            checkAndMarkFace(pbFree1.Image, out listImg, out listFeature1);

            if (listImg == null || listImg.Count <= 0)
                return;
            pbFreeResult1.Image = listImg[0];
        }

        private void btnGetFree2_Click(object sender, EventArgs e)
        {
            InitArcFace();
            List<Image> listImg = null;
            listFeature2 = null;
            checkAndMarkFace(pbFree2.Image, out listImg, out listFeature2);
            if (listImg == null || listImg.Count <= 0)
                return;
            pbFreeResult2.Image = listImg[0];
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            InitArcFace();
            if (listFeature1 == null || listFeature1.Count <= 0)
            {
                WriteLog(string.Format("picture1 feature is null"));
                return;
            }
            if (listFeature2 == null || listFeature2.Count <= 0)
            {
                WriteLog(string.Format("picture2 feature is null"));
                return;
            }
            float fValue = FacePairMatching(listFeature1[0], listFeature2[0]);
            lblResult.Text = string.Format("match result:{0}", fValue);
            WriteLog(string.Format("match result:{0}", fValue));
        }

        private float FacePairMatching(byte[] bFeature1, byte[] bFeature2)
        {
            if (bFeature1 == null || bFeature1.Length <= 0)
                return -1;
            if (bFeature2 == null || bFeature2.Length <= 0)
                return -1;
            //目的：
            //根据识别到的人脸，去人脸库中查询
            //当前人脸的特征数据已经获取到
            byte[] sourceFeature = bFeature1;//
            float similar = 0f;
            AFR_FSDK_FaceModel localFaceModels = new AFR_FSDK_FaceModel();
            IntPtr sourceFeaturePtr = Marshal.AllocHGlobal(sourceFeature.Length);
            Marshal.Copy(sourceFeature, 0, sourceFeaturePtr, sourceFeature.Length);
            localFaceModels.lFeatureSize = sourceFeature.Length;
            localFaceModels.pbFeature = sourceFeaturePtr;
            byte[] libaryFeature = bFeature2;//System.IO.File.ReadAllBytes(b);
            /*根据取到的人脸信息来进行搜索，所以，这里我们不用原图*/
            IntPtr libaryFeaturePtr = Marshal.AllocHGlobal(libaryFeature.Length);
            Marshal.Copy(libaryFeature, 0, libaryFeaturePtr, libaryFeature.Length);
            AFR_FSDK_FaceModel localFaceModels2 = new AFR_FSDK_FaceModel();
            localFaceModels2.lFeatureSize = libaryFeature.Length;
            localFaceModels2.pbFeature = libaryFeaturePtr;
            IntPtr firstPtr = Marshal.AllocHGlobal(Marshal.SizeOf(localFaceModels));
            Marshal.StructureToPtr(localFaceModels, firstPtr, false);
            IntPtr secondPtr = Marshal.AllocHGlobal(Marshal.SizeOf(localFaceModels2));
            Marshal.StructureToPtr(localFaceModels2, secondPtr, false);
            float result = 0f;
            int ret = AFRFunction.AFR_FSDK_FacePairMatching(recognizeEngine, firstPtr, secondPtr, ref result);
            return result;
        }

        IntPtr detectEngine = IntPtr.Zero;
        IntPtr recognizeEngine = IntPtr.Zero;

        private void InitArcFace()
        {
            if (detectEngine != IntPtr.Zero && recognizeEngine != IntPtr.Zero)
                return;

            int detectSize = 100 * 1024 * 1024;
            int nScale = 50;
            int nMaxFaceNum = 50;
            IntPtr pMem = Marshal.AllocHGlobal(detectSize);
            IntPtr pMemRecongnize = Marshal.AllocHGlobal(detectSize);

            //此处填写你申请的到APPID和对应的KEY
            //申请地址:http://www.arcsoft.com.cn/ai/arcface.html

            string appId = "GcggqbW5hwPvXwayc7886Dx2D4maUiqKsPQ5Ep9ifomx";//"bCx99etK9Ns4Saou1EbFdB919TUrVDF52YQAE7F****";

            string sdkFDKey = "Hq7KqvwwmRMSQQxGhJdXKP4vNDsqZ3F1dKtLweKY9V31";//"DpYhgf1jRkNf3o4biiazvpDrNEW38rbB1XNq6V4****";
            string sdkFRKey = "Hq7KqvwwmRMSQQxGhJdXKP5R1pvWhmLH7U5gQ1sq4Kxg";//"DpYhgf1jRkNf3o4biiazvpDyXdmBajqLRpXvhYKU****";

            if (detectEngine == IntPtr.Zero)
            {
                int retCode = AFDFunction.AFD_FSDK_InitialFaceEngine(appId, sdkFDKey, pMem, detectSize, ref detectEngine, 5, nScale, nMaxFaceNum);
                if (retCode != 0)
                {
                    MessageBox.Show("引擎FD初始化失败:错误码为:" + retCode);
                    this.Close();
                }
            }

            if (recognizeEngine == IntPtr.Zero)
            {
                int retCode2 = AFR.AFRFunction.AFR_FSDK_InitialEngine(appId, sdkFRKey, pMemRecongnize, detectSize, ref recognizeEngine);


                if (retCode2 != 0)
                {
                    MessageBox.Show("引擎FR初始化失败:错误码为:" + retCode2);
                    this.Close();
                }
            }
        }

        private object GetAFD_FSDK_FACERES(Bitmap bitmap)
        {
            int width = 0;
            int height = 0;
            int pitch = 0;
            byte[] imageData = readBmp(bitmap, ref width, ref height, ref pitch);
            IntPtr imageDataPtr = Marshal.AllocHGlobal(imageData.Length);
            Marshal.Copy(imageData, 0, imageDataPtr, imageData.Length);
            ASVLOFFSCREEN offInput = new ASVLOFFSCREEN();
            offInput.u32PixelArrayFormat = 513;
            offInput.ppu8Plane = new IntPtr[4];
            offInput.ppu8Plane[0] = imageDataPtr;
            offInput.i32Width = width;
            offInput.i32Height = height;
            offInput.pi32Pitch = new int[4];
            offInput.pi32Pitch[0] = pitch;
            AFD_FSDK_FACERES faceRes = new AFD_FSDK_FACERES();
            IntPtr offInputPtr = Marshal.AllocHGlobal(Marshal.SizeOf(offInput));
            Marshal.StructureToPtr(offInput, offInputPtr, false);
            IntPtr faceResPtr = Marshal.AllocHGlobal(Marshal.SizeOf(faceRes));
            //DECTED FACE
            int detectResult = AFDFunction.AFD_FSDK_StillImageFaceDetection(detectEngine, offInputPtr, ref faceResPtr);
            object obj = Marshal.PtrToStructure(faceResPtr, typeof(AFD_FSDK_FACERES));
            return obj;
        }

        private List<Image> GetFaceIamge(AFD_FSDK_FACERES faceRes, Bitmap bitmap)
        {
            if (faceRes.nFace <= 0)
                return null;
            List<Image> listImg = new List<Image>();
            //识别每一幅图像，并将其保存到临时目录
            for (int i = 0; i < faceRes.nFace; i++)
            {
                MRECT rect = (MRECT)Marshal.PtrToStructure(faceRes.rcFace + Marshal.SizeOf(typeof(MRECT)) * i, typeof(MRECT));
                Image image = CutFace(bitmap, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
                if (image == null)
                    continue;
                listImg.Add(image);

                //原图上画框
                //this.pictureBox1.Image = DrawRectangleInPicture(i, pictureBox1.Image, new Point(rect.left, rect.top), new Point(rect.right, rect.bottom), Color.Red, pictureBox1.Image.Width / 300, DashStyle.Dash);
            }
            return listImg;
        }

        private List<byte[]> GetFaceFeature(AFD_FSDK_FACERES faceRes, Bitmap bitmap)
        {
            int width = 0;
            int height = 0;
            int pitch = 0;
            byte[] imageData = readBmp(bitmap, ref width, ref height, ref pitch);
            IntPtr imageDataPtr = Marshal.AllocHGlobal(imageData.Length);
            Marshal.Copy(imageData, 0, imageDataPtr, imageData.Length);
            ASVLOFFSCREEN offInput = new ASVLOFFSCREEN();
            offInput.u32PixelArrayFormat = 513;
            offInput.ppu8Plane = new IntPtr[4];
            offInput.ppu8Plane[0] = imageDataPtr;
            offInput.i32Width = width;
            offInput.i32Height = height;
            offInput.pi32Pitch = new int[4];
            offInput.pi32Pitch[0] = pitch;
            IntPtr offInputPtr = Marshal.AllocHGlobal(Marshal.SizeOf(offInput));
            Marshal.StructureToPtr(offInput, offInputPtr, false);

            //定义人脸特征
            string faceFeature = string.Empty;
            //获取第一脸的特征
            AFR_FSDK_FaceInput faceinput = new AFR_FSDK_FaceInput();
            faceinput.lOrient = (int)Marshal.PtrToStructure(faceRes.lfaceOrient, typeof(int));
            List<byte[]> listFeature = new List<byte[]>();
            for (int i = 0; i < faceRes.nFace; i++)
            {
                MRECT rect = (MRECT)Marshal.PtrToStructure(faceRes.rcFace + Marshal.SizeOf(typeof(MRECT)) * i, typeof(MRECT));
                faceinput.rcFace = rect;
                IntPtr faceInputPtr = Marshal.AllocHGlobal(Marshal.SizeOf(faceinput));
                Marshal.StructureToPtr(faceinput, faceInputPtr, false);
                //定义特征的变量用于保存特征值
                AFR_FSDK_FaceModel faceModel = new AFR_FSDK_FaceModel();
                IntPtr faceModelPtr = Marshal.AllocHGlobal(Marshal.SizeOf(faceModel));
                int ret = AFRFunction.AFR_FSDK_ExtractFRFeature(recognizeEngine, offInputPtr, faceInputPtr,
                    faceModelPtr);
                faceModel = (AFR_FSDK_FaceModel)Marshal.PtrToStructure(faceModelPtr, typeof(AFR_FSDK_FaceModel));
                Marshal.FreeHGlobal(faceModelPtr);
                byte[] featureContent = new byte[faceModel.lFeatureSize];
                Marshal.Copy(faceModel.pbFeature, featureContent, 0, faceModel.lFeatureSize);

                listFeature.Add(featureContent);

                //faceFeature += Convert.ToBase64String(featureContent, Base64FormattingOptions.None);
                //对每个特征点进行保存，目前前暂时保存在G:/Test目录。
                //System.IO.File.WriteAllBytes(Path.Combine(FaceLibraryPath, faceImageName[i] + ".dat"), featureContent);
            }
            return listFeature;
        }


        private void checkAndMarkFace(Image sourceImage, out List<Image> listImg, out List<byte[]> listFeature)
        {
            listImg = null;
            listFeature = null;
            Bitmap bitmap = new Bitmap(sourceImage);
            byte[] feature = new byte[1];
            object obj = GetAFD_FSDK_FACERES(bitmap);
            if (obj == null)
                return;
            AFD_FSDK_FACERES faceRes = (AFD_FSDK_FACERES)obj;
            WriteLog(string.Format("end AFD_FSDK_StillImageFaceDetection,get face count:{0}", faceRes.nFace));
            if (faceRes.nFace <= 0)
                return;
            //定义用到保存识别到的图片的名称的数组
            //List<string> faceImageName = new List<string>(faceRes.nFace);
            //for (int i = 0; i < faceRes.nFace; i++)
            //{
            //    faceImageName.Add(Guid.NewGuid().ToString());
            //}
            listImg = GetFaceIamge(faceRes, bitmap);

            listFeature = GetFaceFeature(faceRes, bitmap);

            //feature = Convert.FromBase64String(faceFeature);
        }

        private byte[] readBmp(Bitmap image, ref int width, ref int height, ref int pitch)
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

        public static Bitmap CutFace(Bitmap srcImage, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (srcImage == null)
            {
                return null;
            }
            int w = srcImage.Width;
            int h = srcImage.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(srcImage, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        private void WriteLog(string sLog)
        {
            try
            {
                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke(new MethodInvoker(() =>
                    {
                        txtLog.AppendText(string.Format("{0}\t{1}\r\n", DateTime.Now.ToString("HH:mm:ss"), sLog));
                        txtLog.ScrollToCaret();
                    }));
                }
                else
                {
                    txtLog.AppendText(string.Format("{0}\t{1}\r\n", DateTime.Now.ToString("HH:mm:ss"), sLog));
                    txtLog.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("writelog error :{0}", ex.Message));
            }
        }
    }
}
