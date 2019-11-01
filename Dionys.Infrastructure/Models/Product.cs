using System;

namespace Dionys.Infrastructure.Models
{
    /// <summary>
    /// Product Entity
    /// </summary>
    public class Product : ISoftDeleteableModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrates { get; set; }
        public float Calories { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
