namespace Dionys.Web.Models.ViewModels
{
    public class PagingParameterModel
    {
        public int Page { get; set; } = 0;
        public int ElementsPerPage { get; set; } = 300;

        public bool Validate()
        {
            return Page >= 0 && ElementsPerPage >= 0;
        }
    }
}
