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
        //const string _dockerSQLServer4 = "Data Source=127.0.0.1;Initial Catalog=Test;Integrated Security=True";
        const string _dockerSQLServer4 = "Data Source=127.0.0.1,1401;Initial Catalog=Test;uid=sa;pwd=Qz8954167";

        [Theory]
        [InlineData(_topdbSQLServer2)]
        [InlineData(_dockerSQLServer4)]
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

        [Fact]
        public void DockerTest()
        {
            using (var conn = new SqlConnection(_dockerSQLServer4))
            {
                conn.Open();
                var command = new SqlCommand("select * from TbTest", conn);
                var reader = command.ExecuteReader();

                var except = 1;
                while(reader.Read())
                {
                    var s = reader.GetString(0);
                    Assert.Equal((except++).ToString(), s);
                }
                conn.Close();
            }
        }
    }
}
