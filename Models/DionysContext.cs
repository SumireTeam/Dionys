using Microsoft.EntityFrameworkCore;

namespace Dionys.Models
{
    public class DionysContext : DbContext
    {
        public DionysContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
