using System;
using System.Collections.Generic;
using System.Text;

namespace Dionys.Common
{
    public static class DateTimeExtensions
    {
        public static string ToStringOnlyDate(this DateTime self)
        {
            return self.ToShortDateString();
        }
    }
}
