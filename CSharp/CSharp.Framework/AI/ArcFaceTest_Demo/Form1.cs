using ArcFaceTest.AFD;
using ArcFaceTest.AFR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ArcFaceTest
{
    public partial class Form1 : Form
    {

        //人脸检测引擎
        IntPtr detectEngine = IntPtr.Zero;
        IntPtr recognizeEngine = IntPtr.Zero;

        private String FaceLibraryPath = Environment.CurrentDirectory;//"K:/VS_Projects/WinForm/ArcFaceTest/bin/Debug/";

        public Form1()
        {
            InitializeComponent();
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
            int retCode = AFDFunction.AFD_FSDK_InitialFaceEngine(appId, sdkFDKey, pMem, detectSize, ref detectEngine, 5, nScale, nMaxFaceNum);


            if (retCode != 0)
            {
                MessageBox.Show("引擎FD初始化失败:错误码为:" + retCode);
                this.Close();
            }

            int retCode2 = AFR.AFRFunction.AFR_FSDK_InitialEngine(appId, sdkFRKey, pMemRecongnize, detectSize, ref recognizeEngine);


            if (retCode2 != 0)
            {
                MessageBox.Show("引擎FR初始化失败:错误码为:" + retCode2);
                this.Close();
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {


            IntPtr versionPtr = AFDFunction.AFD_FSDK_GetVersion(detectEngine);

            AFD_FSDK_Version version = (AFD_FSDK_Version)Marshal.PtrToStructure(versionPtr, typeof(AFD_FSDK_Version));

            String VersionInfo = String.Format("Version:{0} BuildDate:{1}", Marshal.PtrToStringAnsi(version.Version), Marshal.PtrToStringAnsi(version.BuildDate));

            MessageBox.Show(VersionInfo);

        }

        private void btnOpenPic_Click(object sender, EventArgs e)
        {
            //加载图片
            //识别图片
            //提取人脸
            //标记红框



            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png|所有文件|*.*;";

            openFile.Multiselect = false;

            openFile.FileName = "";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = null;

                Image image = Image.FromFile(openFile.FileName);

                this.pictureBox1.Image = new Bitmap(image);


                image.Dispose();

                byte[] featureArray = checkAndMarkFace(this.pictureBox1.Image);

                if (featureArray != null && featureArray.Length > 0)
                {
                    string sPath = Path.Combine(FaceLibraryPath, string.Format("{0}.dat", DateTime.Now.ToString("yyyyMMddHHmmss")));
                    System.IO.File.WriteAllBytes(sPath, featureArray);
                }
            }
        }

        private byte[] checkAndMarkFace(Image sourceImage)
        {
            byte[] feature = new byte[1];
            int width = 0;

            int height = 0;

            int pitch = 0;

            Bitmap bitmap = new Bitmap(sourceImage);


            // IntPtr imageDataPtr= bitmap.GetHbitmap();

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

            faceRes = (AFD_FSDK_FACERES)obj;

            label1.Text = "人脸识别成功，人数:" + faceRes.nFace + "人";


            foreach (Control item in this.panel1.Controls)
            {
                this.panel1.Controls.Remove(item);
            }


            if (faceRes.nFace > 0)
            {

                //定义用到保存识别到的图片的名称的数组
                List<string> faceImageName = new List<string>(faceRes.nFace);

                for (int i = 0; i < faceRes.nFace; i++)
                {
                    faceImageName.Add(Guid.NewGuid().ToString());
                }

                //识别每一幅图像，并将其保存到临时目录

                for (int i = 0; i < faceRes.nFace; i++)
                {
                    MRECT rect = (MRECT)Marshal.PtrToStructure(faceRes.rcFace + Marshal.SizeOf(typeof(MRECT)) * i, typeof(MRECT));
                    Image image = CutFace(bitmap, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

                    if (image == null)
                        continue;

                    if (i == 0)
                    {
                        this.pictureBox2.Image = image;
                        this.pictureBox2.Tag = faceImageName[i];
                    }
                    else
                    {
                        PictureBox tempPicture = new PictureBox();
                        tempPicture.Width = 100;
                        tempPicture.Height = 120;
                        tempPicture.SizeMode = PictureBoxSizeMode.Zoom;
                        tempPicture.Location = new System.Drawing.Point(10 + ((i - 1) % 7) * 120, 10 + ((i - 1) / 7) * 120);
                        tempPicture.Image = image;
                        tempPicture.Tag = faceImageName[i];
                        this.panel1.Controls.Add(tempPicture);
                    }


                    string sPath = Path.Combine(FaceLibraryPath, faceImageName[i] + ".jpg");

                    WriteLog(string.Format("image.size :{0},{1}", image.Size, sPath));

                    FileInfo file = new FileInfo(sPath);
                    if (!file.Exists)
                    {
                        using (Stream stream = file.Create())
                        {
                            stream.Flush();
                            stream.Close();
                            stream.Dispose();
                        }
                    }
                    image.Save(sPath, ImageFormat.Jpeg);

                    WriteLog(string.Format("image save completed "));

                    //原图上画框
                    this.pictureBox1.Image = DrawRectangleInPicture(i, pictureBox1.Image, new Point(rect.left, rect.top), new Point(rect.right, rect.bottom), Color.Red, pictureBox1.Image.Width / 300, DashStyle.Dash);
                }






                //定义人脸特征
                string faceFeature = string.Empty;

                //获取第一脸的特征
                AFR_FSDK_FaceInput faceinput = new AFR_FSDK_FaceInput();
                faceinput.lOrient = (int)Marshal.PtrToStructure(faceRes.lfaceOrient, typeof(int));


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

                    //   feature = featureContent;

                    faceFeature += Convert.ToBase64String(featureContent, Base64FormattingOptions.None);

                    //对每个特征点进行保存，目前前暂时保存在G:/Test目录。

                    System.IO.File.WriteAllBytes(Path.Combine(FaceLibraryPath, faceImageName[i] + ".dat"), featureContent);


                }

                //  MessageBox.Show(faceFeature.Length.ToString());
                feature = Convert.FromBase64String(faceFeature);
            }
            return feature;
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

        private Image DrawRectangleInPicture(int faceIndex, Image bmp, Point p0, Point p1, Color RectColor, int LineWidth, DashStyle ds)
        {
            if (bmp == null) return null;


            Graphics g = Graphics.FromImage(bmp);

            Brush brush = new SolidBrush(RectColor);
            Pen pen = new Pen(brush, LineWidth);
            pen.DashStyle = ds;

            g.DrawRectangle(pen, new Rectangle(p0.X, p0.Y, Math.Abs(p0.X - p1.X), Math.Abs(p0.Y - p1.Y)));
            g.DrawString(faceIndex.ToString(), new Font("Arial", 8 * LineWidth), brush, p0);

            g.Dispose();

            return bmp;
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //识别出Feature需求
            AFD.AFDFunction.AFD_FSDK_UninitialFaceEngine(detectEngine);
            AFRFunction.AFR_FSDK_UninitialEngine(recognizeEngine);
        }

        private void btnFaceCompaire_Click(object sender, EventArgs e)
        {
            //目的：
            //根据识别到的人脸，去人脸库中查询
            //当前人脸的特征数据已经获取到

            string faceFeaturePath = pictureBox2.Tag as string;

            if (!string.IsNullOrEmpty(faceFeaturePath))
            {
                /**/
                byte[] sourceFeature = System.IO.File.ReadAllBytes(Path.Combine(FaceLibraryPath, faceFeaturePath + ".dat"));

                float similar = 0f;

                AFR_FSDK_FaceModel localFaceModels = new AFR_FSDK_FaceModel();

                IntPtr sourceFeaturePtr = Marshal.AllocHGlobal(sourceFeature.Length);

                Marshal.Copy(sourceFeature, 0, sourceFeaturePtr, sourceFeature.Length);

                localFaceModels.lFeatureSize = sourceFeature.Length;

                localFaceModels.pbFeature = sourceFeaturePtr;

                foreach (var b in System.IO.Directory.GetFiles(FaceLibraryPath, "*.dat"))
                {
                    byte[] libaryFeature = System.IO.File.ReadAllBytes(b);

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
                    if (result > 0.1 && result < 0.99)
                    {
                        //MessageBox.Show(b);
                        try
                        {
                            Image image = Image.FromFile(b.Replace(".dat", ".jpg"));

                            this.pictureBox3.Image = new Bitmap(image);
                            MessageBox.Show(result.ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        continue;

                    }
                    else
                    {
                        //   MessageBox.Show(string.Format("{0}:{1},{2}", ret, b, result));
                    }
                }


            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }


    }
}
