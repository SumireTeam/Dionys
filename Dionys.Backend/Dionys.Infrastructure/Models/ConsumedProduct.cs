using System;

namespace Dionys.Infrastructure.Models
{
    /// <summary>
    /// Consumed Product Entity
    /// </summary>
    public class ConsumedProduct : IDbModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Consumed product
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Consumed product id. For lazy load bypass.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Consumed product weight
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// When product was consumed
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }
    }
}
