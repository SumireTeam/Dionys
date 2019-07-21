using System.Collections.Generic;

namespace Dionys.Models.ViewModels.Statistic
{
    /// <summary>
    /// Eaten products api response
    /// </summary>
    public class EatenProductsTotal
    {
        /// <summary>
        /// Products collection by days
        /// </summary>
        public IEnumerable<EatenProduct> Products { get; set; }

        /// <summary>
        /// Sum of products' calories on Days collection
        /// </summary>
        public long TotalCalories { get; set; }
    }
}
