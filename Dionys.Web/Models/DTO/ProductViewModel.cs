using System;

namespace Dionys.Web.Models.DTO
{
    public class ProductViewModel
    {
        public Guid   Id            { get; set; }
        public string Name          { get; set; }
        public float  Protein       { get; set; }
        public float  Fat           { get; set; }
        public float  Carbohydrates { get; set; }
        public float  Calories      { get; set; }
        public string Description   { get; set; }
    }
}
