using System;
using System.Collections.Generic;
using System.Text;
using Dionys.Infrastructure.Models;

namespace Dionys.Infrastructure.Extensions
{
    public static class DbExtensions
    {
        public static bool IsNew(this IDbModel model) => model.Id == Guid.Empty;
    }
}
