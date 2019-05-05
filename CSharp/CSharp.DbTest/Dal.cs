using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopDAL;

namespace CSharp.DbTest
{
    public static class Dal
    {
        private static DBHelper _dbHelper;
        public static DBHelper DbHelper
        {
            get
            {
                return _dbHelper;
            }
        }
        static Dal()
        {
            _dbHelper = new DBHelper("server=192.168.3.87;uid=sa;pwd=top@db123;database=CgoIOTDB;Connect Timeout=120");
        }
    }
}
