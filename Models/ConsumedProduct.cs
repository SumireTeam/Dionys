using System;

namespace Dionys.Models
{
    /// <summary>
    /// Consumed Product Entity
    /// </summary>
    public class ConsumedProduct
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Consumed product
        /// </summary>
        public Product Product { get; set; }

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
