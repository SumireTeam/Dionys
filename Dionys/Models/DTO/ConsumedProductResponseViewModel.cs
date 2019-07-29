using System;

namespace Dionys.Models.DTO
{
    public class ConsumedProductResponseViewModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Consumed product id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Consumed product
        /// </summary>
        public ProductViewModel Product { get; set; }

        /// <summary>
        /// Consumed product weight
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// When product was consumed
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
