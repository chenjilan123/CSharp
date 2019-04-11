using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CSharp.Host
{
    public class HostInformation
    {
        public void PrintInformation()
        {
            //Host name
            var name = Dns.GetHostName();
            Console.WriteLine($"Local host name: {name}");

            //Obsolete
            //IPHostEntry localHost = Dns.GetHostByName(name);
            //PrintIPAddress(localHost);

            //IP
            var host = Dns.GetHostEntry(name);
            PrintIPAddress(host);

        }

        private void PrintIPAddress(IPHostEntry entry)
        {
            foreach (var localIP in entry.AddressList)
            {
                Console.WriteLine($"IP address: {localIP.ToString()}, Address family: {localIP.AddressFamily}");
                //IP - Port
                var localEP = new IPEndPoint(localIP, 8000);
                Console.WriteLine($"     Local endpoint name: {localEP.ToString()}, Address family: {localEP.AddressFamily}");
                Console.WriteLine();
            }
        }
    }
}
