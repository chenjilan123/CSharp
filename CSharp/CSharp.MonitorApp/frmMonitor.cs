using CSharp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp.MonitorApp
{
    public partial class frmMonitor : Form
    {
        private Monitor _remoteMonitor;
        private Bitmap _bitmap;
        private bool _control = false;

        public void Start()
        {

        }
        public frmMonitor()
        {
            InitializeComponent();
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {
            try
            {
                ChannelServices.RegisterChannel(new TcpChannel(), false);
                _remoteMonitor = (Monitor)Activator.GetObject(typeof(Monitor), "tcp://127.0.0.1:8000/MonitorServer");

                var deskTopSize = _remoteMonitor.GetDesktopBitmapSize();
                _bitmap = new Bitmap(deskTopSize.Width, deskTopSize.Height);
                this.AutoScrollMinSize = deskTopSize;

                var thread = new System.Threading.Thread(() =>
                {
                    while (!_control)
                    {
                        UpdateDisplay();
                    }
                })
                {
                    IsBackground = true
                };
                thread.Start();
            }
            catch (Exception)
            {

            }
        }

        private void UpdateDisplay()
        {
            System.Threading.Monitor.Enter(this);
            try
            {
                var dataBitmap = _remoteMonitor.GetDesktopBitmapBytes();
                if (dataBitmap == null || dataBitmap.Length <= 0) return;

                using (var ms = new MemoryStream(dataBitmap, false))
                {
                    _bitmap = (Bitmap)Image.FromStream(ms);
                    //绘制滚动位置的图像。
                    var drawPos = new Point(AutoScrollPosition.X, AutoScrollPosition.Y);
                    CreateGraphics().DrawImage(_bitmap, drawPos);
                }
            }
            catch { }
            finally
            {
                //若不放在finally里, 再try块内return后, 不会执行该内容。
                System.Threading.Monitor.Exit(this);
                System.Threading.Thread.Sleep(20);
            }
        }

        private void frmMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._control = true;
        }
    }
}
