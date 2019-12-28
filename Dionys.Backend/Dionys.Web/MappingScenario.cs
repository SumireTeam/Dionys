using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Dionys.Infrastructure.Models;
using Dionys.Web.Models.Api;

namespace Dionys.Web
{
    public class MappingScenario : Profile
    {
        public MappingScenario()
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<ConsumedProductRequest, ConsumedProduct>();
            CreateMap<ConsumedProduct, ConsumedProductResponse>();
            CreateMap<ConsumedProduct, ConsumedProductRequest>();
        }
    }
}
