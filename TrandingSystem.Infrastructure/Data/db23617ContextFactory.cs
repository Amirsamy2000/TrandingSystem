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
            optionsBuilder.UseSqlServer("Server=db26424.public.databaseasp.net; Database=db26424; User Id=db26424; Password=Bo8@7W-wd!6J; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;")
                          .UseLazyLoadingProxies();

            return new db23617Context(optionsBuilder.Options);
        }
    }
}
