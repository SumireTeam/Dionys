using System;

namespace Dionys.Models
{
    /// <summary>
    /// Product Entity
    /// </summary>
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Protein { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }
        public float Energy { get; set; }
        public string Description { get; set; }
    }
}
