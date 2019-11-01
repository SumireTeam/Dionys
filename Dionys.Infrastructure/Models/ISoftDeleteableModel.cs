using System;

namespace Dionys.Infrastructure.Models
{
    public interface ISoftDeleteableModel : IDbModel
    {
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
    }
}
