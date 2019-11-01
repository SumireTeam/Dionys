using System;
using Dionys.Infrastructure.Models;

namespace Dionys.Infrastructure.Extensions
{
    public static class SoftDeleteableModelExtesions
    {
        public static bool IsDeleted(this ISoftDeleteableModel self) => self.DeletedAt.HasValue;
        public static void SetDeleted(this ISoftDeleteableModel self)
        {
            self.DeletedAt = DateTimeOffset.UtcNow.DateTime;
        }
    }
}
