using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CSharp.Dns_
{
    public class DnsResolver
    {
        public void Resolve()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("请输入服务器地址: ");
                    var sAddress = Console.ReadLine();

                    //需网络IO
                    var ipHostEntry = Dns.GetHostEntry(sAddress);

                    Console.WriteLine($"服务器名: {ipHostEntry.HostName}");
                    foreach (var sAddressName in ipHostEntry.AddressList)
                    {
                        Console.WriteLine($"地址名称: {sAddressName}");
                    }
                    foreach (var sAlias in ipHostEntry.Aliases)
                    {
                        Console.WriteLine($"    别名: {sAlias}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
