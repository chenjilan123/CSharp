using System;
using System.Collections.Generic;
using Xunit;

namespace CSharp.Test
{
    public class UnitTest1
    {
        private Dictionary<string, DateTime> expectTimes = null;

        public UnitTest1()
        {
            expectTimes = new Dictionary<string, DateTime>()
            {
                { "", DateTime.Now.Date.AddDays(-1D) },
                { "2019-03-09", new DateTime(2019, 3, 10) },
                { "2019-03-10", new DateTime(2019, 3, 11) },
                { "2019-03-11", new DateTime(2019, 3, 12) },
                { "2019-03-12", new DateTime(2019, 3, 13) },
                { "2019-03-13", new DateTime(2019, 3, 14) },
                { "2019-03-14", DateTime.Now.Date.AddDays(-1D) },
                { "2019-03-15", DateTime.Now.Date.AddDays(-1D) },
                { "2019-03-16", DateTime.Now.Date.AddDays(-1D) },
                { "2019-03-17", DateTime.Now.Date.AddDays(-1D) },
                { "2019-03-18", DateTime.Now.Date.AddDays(-1D) },
            };
        }

        [Fact]
        public void TestAnalysisTime()
        {
            foreach (var timePair in expectTimes)
            {
                var lastUpdate = timePair.Key;
                var tExpect = timePair.Value;

                var tNextAnalysis = GetNextAnalysisTime(lastUpdate);

                Assert.Equal(tExpect, tNextAnalysis);
            }
        }

        public DateTime GetNextAnalysisTime(string lastUpdate)
        {
            DateTime tNow = DateTime.Now;
            var tLastUpdate = GetLastUpdateTime(lastUpdate);
            if (tLastUpdate == default(DateTime) || tLastUpdate.Date >= tNow.Date)
            {
                return tNow.Date.AddDays(-1D);
            }
            else
            {
                return tLastUpdate.Date.AddDays(1D);
            }
        }

        public DateTime GetLastUpdateTime(string lastUpdate)
        {
            if (DateTime.TryParse(lastUpdate, out DateTime tLastUpdate))
            {
                return tLastUpdate;
            }
            else
            {
                return default(DateTime);
            }
        }
    }
}
