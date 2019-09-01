using System;
using System.Threading.Tasks;
using Dionys.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Infrastructure.Models
{
    public class DionysContext : DbContext, IDionysContext
    {
        public DionysContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ConsumedProduct> ConsumedProducts { get; set; }
        public DbSet<User> Users { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public void MarkAsModified(IDbModel item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void MarkAsUnchanged(IDbModel item)
        {
            Entry(item).State = EntityState.Unchanged;
        }
    }
}
