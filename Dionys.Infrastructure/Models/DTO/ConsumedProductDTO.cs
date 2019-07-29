using System;

namespace Dionys.Infrastructure.Models.DTO
{
    public class ConsumedProductDTO
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Consumed product id
        /// </summary>
        public Guid ProductId { get; set; }

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
