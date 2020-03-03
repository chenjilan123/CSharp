using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace CSharp.Framework.Model
{
    public class CER
    {
        public void Demo()
        {
            //准备约束执行区域
            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                Console.WriteLine("Instance method...");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                var i = CERStatic.V0();
                //CERStatic.M0();
            }
        }
    }

    public class CERStatic
    {
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static void M0() { }
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int V0() { return 5; }
        static CERStatic()
        {
            Console.WriteLine("Static method...");
        }
    }
}
