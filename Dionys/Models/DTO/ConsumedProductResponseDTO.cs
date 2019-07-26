using System;

namespace Dionys.Models.DTO
{
    public class ConsumedProductResponseDTO
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Consumed product
        /// </summary>
        public ProductDTO Product { get; set; }

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
