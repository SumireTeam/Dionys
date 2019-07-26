using System;
using AutoMapper;
using Dionys.Models;
using Dionys.Models.DTO;

namespace Dionys
{
    public class MappingScenario : Profile
    {
        public MappingScenario(DionysContext context)
        { 
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();

            CreateMap<ConsumedProduct, ConsumedProductResponseDTO>()
                .ForMember(d => d.Product, opt => opt.MapFrom(p => p.Product))
                .ForMember(d => d.ProductId, opt => opt.MapFrom(p => p.Product.Id));


            CreateMap<ConsumedProductRequestDTO, ConsumedProduct>()
                .ForMember(d => d.Product, opt => opt.Ignore())
                .AfterMap((d, e) => { e.Product = context.Products.Find(d.ProductId); });
        }
    }
}
