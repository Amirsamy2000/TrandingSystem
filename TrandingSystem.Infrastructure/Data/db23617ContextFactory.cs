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
            optionsBuilder.UseSqlServer("Server=db23897.public.databaseasp.net;Database=db23897;User Id=db23897;Password=123456789;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;")
                          .UseLazyLoadingProxies();

            return new db23617Context(optionsBuilder.Options);
        }
    }
}
