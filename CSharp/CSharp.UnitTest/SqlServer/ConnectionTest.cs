using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Xunit;

namespace CSharp.UnitTest.SqlServer
{
    public class ConnectionTest
    {
        //const string _topdbSQLServer1 = "Data Source=127.0.0.1;Initial Catalog=TopDB;Integrated Security=True";
        const string _topdbSQLServer2 = "Data Source=127.0.0.1;Initial Catalog=TopDB;uid=sa;pwd=357592895";

        //const string _dockerSQLServer4 = "Data Source=127.0.0.1;Initial Catalog=TopDB;Integrated Security=True";

        [Theory]
        [InlineData(_topdbSQLServer2)]
        public void RunTest(string connString)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var command = new SqlCommand("select 1", conn);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    var i = reader.GetInt32(0);
                    Assert.Equal(1, i);
                }
                conn.Close();
            }
        }
    }
}
