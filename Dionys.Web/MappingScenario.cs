using System;
using AutoMapper;
using Dionys.Infrastructure.Models;
using Dionys.Web.Models.DTO;

namespace Dionys.Web
{
    public class MappingScenario : Profile
    {
        public MappingScenario()
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<ConsumedProductRequestViewModel, ConsumedProduct>();
            CreateMap<ConsumedProduct, ConsumedProductResponseViewModel>();
            CreateMap<ConsumedProduct, ConsumedProductRequestViewModel>();
        }
    }
}
