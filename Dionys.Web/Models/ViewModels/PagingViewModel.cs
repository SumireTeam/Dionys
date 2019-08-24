using System.Collections.Generic;

namespace Dionys.Web.Models.ViewModels
{
    public class PagingViewModel<T>
    {
        public int Elements { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
