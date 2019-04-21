using CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.NetFramework.Tcp
{
    public class TcpMonitorServer
    {
        public void Start()
        {
            var channel = new TcpServerChannel(8000);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Monitor), "MonitorServer", WellKnownObjectMode.Singleton);

            Console.WriteLine("Server has started...");
            Console.ReadLine();
        }
    }
}
