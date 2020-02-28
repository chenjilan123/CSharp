using CSharp.SqlServer.Panda.Web;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.SqlServer.Panda
{
    class PandaContext : DbContext
    {
        public PandaContext([NotNullAttribute] DbContextOptions<PandaContext> options) : base(options) { }
        public DbSet<Baidu> Baidus { get; set; }
    }
}
