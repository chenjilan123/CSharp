using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CSharp.DbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StringToInt();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void StringToInt()
        {
            var sId = "123456";
            long lId = long.Parse(sId);
            var param = new[]
            {
                new SqlParameter("@id", "123456"),
                new SqlParameter("@result", 1),
            };
            Dal.DbHelper.ExecSP("[spSms_UpdateCMIOTSendResult]", param);
        }
    }
}
