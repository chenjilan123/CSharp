using CSharp.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.NetFramework.Tcp
{
    public class TcpMonitorClient
    {
        private Monitor _remoteMonitor;
        private Bitmap _bitmap;
        private bool _control = false;

        public void Start()
        {
            ChannelServices.RegisterChannel(new TcpChannel(), false);
            _remoteMonitor = (Monitor)Activator.GetObject(typeof(Monitor), "tcp://127.0.0.1:8000/MonitorServer");

        }
    }
}
