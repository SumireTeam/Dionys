using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Models
{
    public interface IDionysContext
    {
        DbSet<Product>         Products         { get; }
        DbSet<ConsumedProduct> ConsumedProducts { get; }

        int        SaveChanges();
        Task<int>  SaveChangesAsync();
        void       MarkAsModified(IDbModel item);
    }
}
