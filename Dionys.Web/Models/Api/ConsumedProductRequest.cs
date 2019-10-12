using System;

namespace Dionys.Web.Models.Api
{
    public class ConsumedProductRequest
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Consumed product ID
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
