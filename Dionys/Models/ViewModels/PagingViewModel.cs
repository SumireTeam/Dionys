using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionys.Models.ViewModels
{
    public class PagingViewModel<T>
    {
        public int Elements { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
