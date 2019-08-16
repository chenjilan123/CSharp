using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
                    command.CommandText = "UPDATE bas_Driver SET pic = @img WHERE DriverName = 'cr7'";

                    var param = factory.CreateParameter();
                    param.ParameterName = "@img";
                    var dataLst = GetImgData(Image.FromFile("cr7.jpg"));
                    param.Value = dataLst;

                    Console.WriteLine(dataLst.Length);
                    command.Parameters.Add(param);

                    //var result = command.ExecuteNonQuery();
                    //Console.WriteLine($"Row Effected: {result}");
                }
            }
        }

        static byte[] GetImgData(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Jpeg);
                ms.Position = 0;
                var data = new byte[1024];
                var dataLst = new List<byte>();
                var count = ms.Read(data, 0, data.Length);
                while(count > 0)
                {
                    dataLst.AddRange(data.AsEnumerable().Take(count));
                    //Console.WriteLine($"Read {count}bytes");
                    //ms.Length;
                    count = ms.Read(data, dataLst.Count, data.Length);
                }
                return dataLst.ToArray();
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
