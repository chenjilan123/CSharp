using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;

namespace CSharp.Framework.Model
{
    public class AppDomainModel
    {
        public static void Marshalling()
        {
            var adCallingThreadDomain = AppDomain.CurrentDomain;//Thread.GetDomain();
            IsDefaultAppDomain(adCallingThreadDomain);
            string callingDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine($"Default AppDomain's friendly name: {callingDomainName}");

            var exeAssembly = Assembly.GetEntryAssembly();
            Console.WriteLine($"Main assembly: {exeAssembly.FullName}");
            Console.WriteLine();

            //.NET Core 不支持
            Console.WriteLine($"Demo #1");
            AppDomain ad2;
            ad2 = AppDomain.CreateDomain("AD #2");
            IsDefaultAppDomain(ad2);
            MarshalByRefType mbrt0;
            MarshalByRefType mbrt1 = new MarshalByRefType();
            mbrt0 = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly.FullName, "CSharp.Framework.Model.MarshalByRefType");
            Console.WriteLine($"Type={mbrt0.GetType()}");
            Console.WriteLine($"mbrt0 is proxy: {RemotingServices.IsTransparentProxy(mbrt0)}");
            Console.WriteLine($"mbrt1 is proxy: {RemotingServices.IsTransparentProxy(mbrt1)}");
            mbrt0.Method();
            AppDomain.Unload(ad2);
            try { mbrt0.Method(); }
            catch (AppDomainUnloadedException e) { Console.WriteLine(e.Message); }

            Console.WriteLine();
            Console.WriteLine($"Demo #2");
            ad2 = AppDomain.CreateDomain("AD #2");
            mbrt0 = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly.FullName, "CSharp.Framework.Model.MarshalByRefType");
            var value = mbrt0.ReturnValue();
            Console.WriteLine($"value is proxy: {RemotingServices.IsTransparentProxy(value)}");
            value.Method();
            AppDomain.Unload(ad2);
            value.Method();



            Console.ReadLine();
        }

        private static void IsDefaultAppDomain(AppDomain ad)
        {
            Console.WriteLine("{1} is{0} default AppDomain", ad.IsDefaultAppDomain() ? string.Empty : "n't", ad.FriendlyName);
        }
    }

    public sealed class MarshalByRefType: MarshalByRefObject
    {
        public MarshalByRefType()
        {
            Console.WriteLine($"{nameof(MarshalByRefType)} ctro is running in {AppDomain.CurrentDomain.FriendlyName}");
        }

        public void Method()
        {
            Console.WriteLine($"An method is invoking in AppDomain: {AppDomain.CurrentDomain.FriendlyName}");
        }

        public MarshalByRefValue ReturnValue()
        {
            Console.WriteLine($"Return value from AppDomain: {AppDomain.CurrentDomain.FriendlyName}");
            return new MarshalByRefValue();
        }

        ~MarshalByRefType()
        {
            Console.WriteLine($"{this.GetType()} instance is released in {AppDomain.CurrentDomain.FriendlyName}");
        }
    }

    [Serializable]
    public sealed class MarshalByRefValue
    {
        //不同AppDomain不共享
        private static int _count = 0;
        public MarshalByRefValue()
        {
            Console.WriteLine($"{nameof(MarshalByRefValue)} ctro is running in {AppDomain.CurrentDomain.FriendlyName}({++_count})");
        }

        public void Method()
        {
            Console.WriteLine($"An method is invoking in AppDomain({++_count}): {AppDomain.CurrentDomain.FriendlyName}");
        }
    }
}
