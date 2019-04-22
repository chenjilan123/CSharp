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

namespace CSharp.Model
{
    /// <summary>
    /// 怎么监控是哪个客户端调用的？
    /// </summary>
    public class Monitor : MarshalByRefObject
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

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

        public Size GetDesktopBitmapSize()
        {
            return new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        public byte[] GetDesktopBitmapBytes()
        {
            var curBitmap = GetDesktopBitmap();

            using (var ms = new MemoryStream())
            {
                curBitmap.Save(ms, ImageFormat.Bmp);
                return ms.GetBuffer();
            }
        }

        public Bitmap GetDesktopBitmap()
        {
            //创建等比例的桌面图像
            var dskBmpSize = GetDesktopBitmapSize();
            var dskGraphic = Graphics.FromHwnd(GetDesktopWindow());
            var memImage = new Bitmap(dskBmpSize.Width, dskBmpSize.Height, dskGraphic);
            var memGraphic = Graphics.FromImage(memImage);

            var dc1 = dskGraphic.GetHdc();
            var dc2 = memGraphic.GetHdc();

            BitBlt(dc2, 0, 0, dskBmpSize.Width, dskBmpSize.Height, dc1, 0, 0, 0xCC0200);
            dskGraphic.ReleaseHdc(dc1);
            memGraphic.ReleaseHdc(dc2);
            dskGraphic.Dispose();
            memGraphic.Dispose();
            //memImage.Save("test.bmp");
            return memImage;
        }
    }
}
