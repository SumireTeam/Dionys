using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dionys.Infrastructure.Models
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<DionysContext>
    {
        public DionysContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DionysContext>();
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Username=dionys;Password=dionys;Database=dionys;"
            );

            return new DionysContext(optionsBuilder.Options);
        }
    }
}
