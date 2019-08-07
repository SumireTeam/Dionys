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
        public static void SetDeleted(this ISoftDeleteableModel self, bool ignoreError = false)
        {
            if ((self.IsDeleted() || self.IsNew()) && !ignoreError)
                throw new NotFoundEntityServiceException($"Cannot find {self.GetType()}. " +
                                                         $"Is Model with id {self.Id} already deleted or doesn't exist?");

            self.DeletedAt = DateTimeOffset.UtcNow.DateTime;
        }
    }
}
