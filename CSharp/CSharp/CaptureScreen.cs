using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp
{
    public class CaptureScreen
    {
        /// <summary>
        /// 获取设备描述表
        /// </summary>
        /// <param name="lpszDriver">驱动名称</param>
        /// <param name="lpszDevice">设备名称</param>
        /// <param name="lpszOutput">无用, 可设定为null</param>
        /// <param name="lpInitData">任意的打印机数据</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDC(
            string lpszDriver, 
            string lpszDevice, 
            string lpszOutput, 
            IntPtr lpInitData
            );
        /// <summary>
        /// 获取位图
        /// </summary>
        /// <param name="hdcDest">目标设备句柄</param>
        /// <param name="nXDest">目标对象X坐标</param>
        /// <param name="nYDest">目标对象Y坐标</param>
        /// <param name="nWidth">目标对象宽度</param>
        /// <param name="nHeight">目标对象长度</param>
        /// <param name="hdcSrc">源设备句柄</param>
        /// <param name="nXSrc">源设备X坐标</param>
        /// <param name="nYSrc">源设备Y坐标</param>
        /// <param name="dwRop">光栅操作值</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        private static extern IntPtr BitBlt(
            IntPtr hdcDest,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            int dwRop
            );

        public static void SaveScreenPictureAtBasePath()
        {
            //根据屏幕大小创建等比例的位图(空白图像)
            var dcScreen = CreateDC("DISPLAY", null, null, (IntPtr)null);
            //Hdc: Handle to the device context
            var g1 = Graphics.FromHdc(dcScreen);
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;
            var image = new Bitmap(width, height, g1);
            //获取屏幕句柄及位图句柄
            var g2 = Graphics.FromImage(image); //获取等比例位图的Graphic对象
            var dc3 = g1.GetHdc(); //获取屏幕句柄 (源设备句柄)
            var dc2 = g2.GetHdc(); //获取位图句柄 (目标设备句柄)
            //捕获屏幕
            BitBlt(dc2, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, dc3, 0, 0, 13369376); 
            g1.ReleaseHdc();
            g2.ReleaseHdc();

            SaveImage(image);
            CopyImageToCopyboard(image);
        }

        private static void SaveImage(Bitmap image)
        {
            var path = Environment.CurrentDirectory;
            var dir = Directory.CreateDirectory(Path.Combine(path, "Captures"));
            if (!dir.Exists) dir.Create();

            image.Save(Path.Combine(dir.FullName, $"屏幕_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.png"));
            //image.Save(Path.Combine(dir.FullName, $"屏幕_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.bmp"));
            //image.Save(Path.Combine(dir.FullName, $"屏幕_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.jpg"), ImageFormat.Jpeg);
            //image.Save(Path.Combine(dir.FullName, $"屏幕_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.png"), ImageFormat.Png);
        }

        /// <summary>
        /// 扩展: 复制到剪切栏
        /// </summary>
        /// <param name="image"></param>
        private static void CopyImageToCopyboard(Bitmap image)
        {

        }

    }
}
