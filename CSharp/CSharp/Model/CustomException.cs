using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

//禁用程序集内所有方法内联
//[assembly: System.Diagnostics.Debuggable(System.Diagnostics.DebuggableAttribute.DebuggingModes.DisableOptimizations)]
namespace CSharp.Model
{
    public class CustomException
    {
        //禁用方法内联
        //[MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThreadException()
        {
            //ThrowException();
            new Thread(() =>
            {
                ThrowException();
            }).Start();
        }

        private static void ThrowException()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine();

                Console.WriteLine("StrakTrack");
                Console.WriteLine(new System.Diagnostics.StackTrace().ToString());
            }
        }
    }
     public class Cus: CustomException
    {
        public static new void ThreadException()
        {
            var s = new { Value = 5 };
            Action j = delegate ()
            {

            };
        }

        delegate void Count(int i, int j);
    }
}
