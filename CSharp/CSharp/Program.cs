using CSharp.Host;
using CSharp.Ping;
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
            Ping();
            return;

            Socket();
            return;

            Host();
            return;
        }

        #region Socket
        static private void Socket()
        {
            new SocketServer().Run();
        }
        #endregion

        #region Host
        private static void Host()
        {
            new HostInformation().PrintInformation();
        }
        #endregion

        #region Ping
        private static void Ping()
        {
            new PingDemo().Start();
        }
        #endregion
    }
}
