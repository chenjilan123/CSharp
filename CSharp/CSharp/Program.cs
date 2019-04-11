using CSharp.Host;
using CSharp.Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSharp
{
    class Program
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket?view=netframework-4.7.2
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Socket();
        }

        static private void Socket()
        {
            new SocketServer().Run();
        }

        #region Host
        private static void Host()
        {
            new HostInformation().PrintInformation();
        }
        #endregion
    }
}
