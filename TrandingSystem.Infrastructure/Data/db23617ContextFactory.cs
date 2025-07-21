using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TrandingSystem.Infrastructure.Data
{
    public class db23617ContextFactory : IDesignTimeDbContextFactory<db23617Context>
    {
        public db23617Context CreateDbContext(string[] args)
        {
            // Try to find appsettings.json in the TradingSystem project directory (union architecture)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "TradingSystem");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<db23617Context>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new db23617Context(optionsBuilder.Options);
        }
    }
}
