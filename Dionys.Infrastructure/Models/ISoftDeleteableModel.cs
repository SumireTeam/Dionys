using System;
using System.Collections.Generic;
using System.Text;

namespace Dionys.Infrastructure.Models
{
    public interface ISoftDeleteableModel : IDbModel
    {
        DateTime  CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
