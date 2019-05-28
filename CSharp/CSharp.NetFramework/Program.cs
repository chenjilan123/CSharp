using CSharp.NetFramework.Gis;
using CSharp.NetFramework.Tcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.NetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            GisService();
        }

        #region StartTcpChannel
        private static void StartTcpChannel()
        {
            string ope = null;
            while (true)
            {
                Console.WriteLine("Run server or client ?");
                ope = Console.ReadLine();
                if (ope == "server" || ope == "client")
                {
                    break;
                }
                Console.WriteLine("Input error, please try again.");
            }
            switch (ope)
            {
                case "server":
                    new TcpChannelComm().StartServer();
                    break;
                case "client":
                    new TcpChannelComm().StartClient();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region StartMonitor
        private static void StartMonitor()
        {
            new TcpMonitorServer().Start();
        }
        #endregion

        #region GisService
        static void GisService()
        {
            new WebGis().GetLocation(117.50, 27.50);
        }
        #endregion
    }
}
