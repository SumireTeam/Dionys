using System;

namespace Dionys.Infrastructure.Models
{
    public interface IDbModel
    {
        Guid Id { get; set; }
    }
}
