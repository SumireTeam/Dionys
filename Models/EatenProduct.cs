using System;

namespace Dionys.Models
{
    /// <summary>
    /// Eaten Product Entity
    /// </summary>
    public class EatenProduct
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Eaten product
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Eaten product weight
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// When product was eaten
        /// </summary>
        public DateTime Time { get; set; }
    }
}
