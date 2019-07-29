using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Infrastructure.Models
{
    public interface IDionysContext
    {
        DbSet<Product>         Products         { get; }
        DbSet<ConsumedProduct> ConsumedProducts { get; }

        int        SaveChanges();
        Task<int>  SaveChangesAsync();
        void       MarkAsModified(IDbModel item);
        void       MarkAsUnchanged(IDbModel item);
    }
}
