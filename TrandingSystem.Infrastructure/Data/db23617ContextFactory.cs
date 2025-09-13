using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Infrastructure.Data
{
    public class db23617ContextFactory : IDesignTimeDbContextFactory<db23617Context>
    {
        public db23617Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<db23617Context>();
            optionsBuilder.UseSqlServer("Data Source=SQL8010.site4now.net;Initial Catalog=db_abe268_saifalqadi;User Id=db_abe268_saifalqadi_admin;Password=123456789@AG; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;")
                          .UseLazyLoadingProxies();

            return new db23617Context(optionsBuilder.Options);
        }
    }
}
