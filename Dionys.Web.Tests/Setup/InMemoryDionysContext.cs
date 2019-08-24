using System.Threading.Tasks;
using Dionys.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Web.Tests.Setup
{
    public class InMemoryDionysContext : DbContext, IDionysContext
    {
        public InMemoryDionysContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ConsumedProduct> ConsumedProducts { get; set; }

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
