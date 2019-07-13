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
        public float Proteins { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }
        public float Energy { get; set; }
        public string Commentary { get; set; }
    }
}
