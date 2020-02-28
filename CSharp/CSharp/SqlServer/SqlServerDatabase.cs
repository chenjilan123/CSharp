using CSharp.SqlServer.Panda;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.SqlServer
{
    class SqlServerDatabase //: IProgram
    {
        public void Run()
        {
            DbContextOptions<PandaContext> options = new DbContextOptions<PandaContext>();

            var db = new PandaContext(options);
            //db.
            
        }
    }
}
