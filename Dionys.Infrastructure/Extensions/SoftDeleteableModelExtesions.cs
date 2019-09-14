using System;
using System.Collections.Generic;
using System.Text;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Services.Exceptions;

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
