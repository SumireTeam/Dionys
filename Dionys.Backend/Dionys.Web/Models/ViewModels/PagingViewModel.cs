using System.Collections.Immutable;

namespace Dionys.Web.Models.ViewModels
{
    public class PagingViewModel<T>
    {
        public PagingViewModel(ImmutableArray<T> data)
        {
            Items = data;
        }

        public int Elements => Items.Length;
        public ImmutableArray<T> Items { get; }
    }
}
