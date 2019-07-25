using System.Collections.Generic;

namespace Dionys.Models.ViewModels.Statistic
{
    /// <summary>
    /// Consumed products api response
    /// </summary>
    public class ConsumedProductsTotal
    {
        /// <summary>
        /// Products collection by days
        /// </summary>
        public IEnumerable<ConsumedProduct> Products { get; set; }

        /// <summary>
        /// Sum of products' calories on Days collection
        /// </summary>
        public long TotalCalories { get; set; }
    }
}
