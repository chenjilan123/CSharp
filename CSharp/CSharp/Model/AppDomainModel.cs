using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace CSharp.Model
{
    public class AppDomainModel
    {
        public static void Marshalling()
        {
            var adCallingThreadDomain = AppDomain.CurrentDomain;//Thread.GetDomain();
            string callingDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine($"Default AppDomain's friendly name: {callingDomainName}");

            var exeAssembly = Assembly.GetEntryAssembly();
            Console.WriteLine($"Main assembly: {exeAssembly.FullName}");
            Console.WriteLine();
            
            //.NET Core 不支持
            AppDomain ad2;
            ad2 = AppDomain.CreateDomain("AD #2");
            //MarshalByRefObject mbrt;


            
        }
    }
}
