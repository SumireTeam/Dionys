namespace Dionys.Models.ViewModels
{
    public class PagingParameterModel
    {
        public int PageNumber      { get; set; } = 1;
        public int ElementsPerPage { get; set; } = 10;

        public bool Validate()
        {
            return PageNumber >= 0 && ElementsPerPage > 0;
        }
    }
}
