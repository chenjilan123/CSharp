using System;
using System.Collections.Generic;
using System.Data.Common;
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
                TenMinTask();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #region TenMinTask
        static void TenMinTask()
        {
            DbProviderFactory factory = SqlClientFactory.Instance;
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = "Data Source=192.168.3.99;Initial Catalog=TopDB;uid=sa;pwd=top@db123";
                connection.Open();
                using (DbCommand command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE bas_Driver SET pic = null WHERE DriverName = 'cr7'";

                    var result = command.ExecuteNonQuery();
                    Console.WriteLine($"Row Effected: {result}");
                }
            }
        }
        #endregion

        #region StringToInt
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
        #endregion
    }
}
