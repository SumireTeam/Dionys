using Dionys.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Models
{
    public class DionysContext : DbContext
    {
        public DionysContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<Product>         Products         { get; set; }
        public DbSet<ConsumedProduct> ConsumedProducts { get; set; }
    }
}
