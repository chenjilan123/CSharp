using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CSharp.UnitTest
{
    public class SqlServerTest
    {
        #region Sapphire
        [Fact]
        public void Sapphire()
        {
            var sapphire = new SqlServer.Sapphire();

            //链路断开信息测试
            const string userId = "cjl";
            foreach (var testData in TestData.GetTestData())
            {
                var result = sapphire.GetLinkDisconectInfo(userId, testData.BeginTime, testData.EndTime);
                foreach (var value in result)
                {
                    Assert.Equal(testData.DisconnNum, value.DisconnNum);
                    Assert.Equal(testData.DisconnTime, value.DisconnTime);
                }
            }
        }

        public class TestData
        {    
            //Id    UserId  LoginTime                   LoginoutTime                UpdateTime
            //1     cjl     2019-03-02 12:00:00.000     2019-03-02 13:03:00.000     2019-03-02 13:03:00.000
            //2     cjl     2019-03-02 15:34:42.000     2019-03-02 18:10:32.000     2019-03-02 18:10:32.000
            //3     cjl     2019-03-02 19:00:00.000     2019-03-02 21:30:00.000     2019-03-02 21:30:00.000

            //4     cjl     2019-03-08 12:00:00.000     2019-03-08 14:20:00.000     2019-03-08 14:20:00.000
            //5     cjl     2019-03-08 14:25:00.000     2019-03-08 16:21:10.000     2019-03-08 16:21:10.000

            //6     cjl     2019-03-09 13:05:00.000     2019-03-09 17:30:00.000     2019-03-09 17:30:00.000
            //7     cjl     2019-03-09 18:00:00.000     2019-03-09 19:30:00.000     2019-03-09 19:30:00.000
            //8     cjl     2019-03-09 21:25:00.000     2019-03-09 23:10:00.000     2019-03-09 23:10:00.000
            //9     cjl     2019-03-09 23:20:00.000     2019-03-10 04:00:00.000     2019-03-10 04:00:00.000
            //10    cjl     2019-03-09 12:00:00.000     NULL                        2019-06-12 10:02:41.890
            public static IEnumerable<TestData> GetTestData()
            {
                //①结束时间小于起始时间
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 12:20:00"), EndTime = DateTime.Parse("2019-03-02 12:10:00"), DisconnNum = 0, DisconnTime = 0 };
                //②首项包含起始时间, 末项包含结束时间
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 12:20:00"), EndTime = DateTime.Parse("2019-03-02 20:10:00"), DisconnNum = 2, DisconnTime = 12070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 12:20:00"), EndTime = DateTime.Parse("2019-03-02 20:20:00"), DisconnNum = 2, DisconnTime = 12070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 12:20:00"), EndTime = DateTime.Parse("2019-03-02 21:30:00"), DisconnNum = 2, DisconnTime = 12070 };
                //③首项包含起始时间, 末项不包含结束时间
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 12:20:00"), EndTime = DateTime.Parse("2019-03-02 21:30:01"), DisconnNum = 3, DisconnTime = 12071 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 12:20:00"), EndTime = DateTime.Parse("2019-03-02 22:10:00"), DisconnNum = 3, DisconnTime = 14470 };
                //④首项不包含起始时间, 末项包含结束时间
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 10:20:00"), EndTime = DateTime.Parse("2019-03-02 20:10:00"), DisconnNum = 3, DisconnTime = 18070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-01 10:20:00"), EndTime = DateTime.Parse("2019-03-02 20:10:00"), DisconnNum = 3, DisconnTime = 104470 };
                //⑤首相不包含起始时间, 末项不包含结束时间
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 10:20:00"), EndTime = DateTime.Parse("2019-03-02 22:10:00"), DisconnNum = 4, DisconnTime = 20470 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 10:20:00"), EndTime = DateTime.Parse("2019-03-02 22:50:21"), DisconnNum = 4, DisconnTime = 22891 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 10:20:00"), EndTime = DateTime.Parse("2019-03-08 22:50:21"), DisconnNum = 6, DisconnTime = 525921 };
                //⑥测试边缘时间
                //左边缘
                //  末项包含结束时间
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 12:00:00"), EndTime = DateTime.Parse("2019-03-02 20:10:00"), DisconnNum = 2, DisconnTime = 12070 };
                //  末项不包含结束时间
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 12:00:00"), EndTime = DateTime.Parse("2019-03-02 22:10:00"), DisconnNum = 3, DisconnTime = 14470 };
                //右边缘
                //  首项不包含起始时间
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 10:20:00"), EndTime = DateTime.Parse("2019-03-02 21:30:00"), DisconnNum = 3, DisconnTime = 18070 };
                //  首项包含起始时间
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 12:20:00"), EndTime = DateTime.Parse("2019-03-02 21:30:00"), DisconnNum = 2, DisconnTime = 12070 };
                //左右边缘
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-02 12:00:00"), EndTime = DateTime.Parse("2019-03-02 21:30:00"), DisconnNum = 2, DisconnTime = 12070 };
                //⑦时间段包含当前正在连接的链路
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 12:20:00"), EndTime = DateTime.Parse("2019-03-10 12:10:00"), DisconnNum = 0, DisconnTime = 0 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 12:20:00"), EndTime = DateTime.Parse("2019-03-10 20:10:00"), DisconnNum = 2, DisconnTime = 12070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 12:20:00"), EndTime = DateTime.Parse("2019-03-10 20:20:00"), DisconnNum = 2, DisconnTime = 12070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 12:20:00"), EndTime = DateTime.Parse("2019-03-10 21:30:00"), DisconnNum = 2, DisconnTime = 12070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 12:20:00"), EndTime = DateTime.Parse("2019-03-10 22:30:00"), DisconnNum = 2, DisconnTime = 12070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 12:20:00"), EndTime = DateTime.Parse("2019-03-10 23:30:00"), DisconnNum = 2, DisconnTime = 12070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 10:20:00"), EndTime = DateTime.Parse("2019-03-10 20:10:00"), DisconnNum = 3, DisconnTime = 18070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 10:20:00"), EndTime = DateTime.Parse("2019-03-10 20:10:00"), DisconnNum = 3, DisconnTime = 18070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 10:20:00"), EndTime = DateTime.Parse("2019-03-10 22:10:00"), DisconnNum = 3, DisconnTime = 18070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 10:20:00"), EndTime = DateTime.Parse("2019-03-10 22:50:21"), DisconnNum = 3, DisconnTime = 18070 };
                yield return new TestData() { BeginTime = DateTime.Parse("2019-03-10 10:20:00"), EndTime = DateTime.Parse("2019-03-16 22:50:21"), DisconnNum = 3, DisconnTime = 18070 };


                //Severity	Code	Description	Project	File	Line	Suppression State
                //Error CS1056  Unexpected character '‬'    CSharp.UnitTest P:\GitHub\CSharp\CSharp\CSharp.UnitTest\SqlServer\SqlServerTest.cs  58  Active
                //yield return new TestData() { BeginTime = DateTime.Parse("2019-03-01 10:20:00"), EndTime = DateTime.Parse("2019-03-02 21:30:00"), DisconnNum = 2, DisconnTime = 104470‬ };
            }
            public DateTime BeginTime { get; set; }
            public DateTime EndTime { get; set; }
            public int DisconnNum { get; set; }
            public int DisconnTime { get; set; }
        }
        #endregion
    }
}
