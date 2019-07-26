using System;
using AutoMapper;
using Dionys.Models;
using Dionys.Models.DTO;

namespace Dionys
{
    public class MappingScenario : Profile
    {
        public MappingScenario(IDionysContext context)
        { 
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();

            CreateMap<ConsumedProductRequestDTO, ConsumedProduct>()
                .ForMember(d => d.Product, opt => opt.Ignore())
                .AfterMap((d, e) => { e.Product = context.Products.Find(d.ProductId); });
        }
    }

    public class NestedMappingScenario : Profile
    {
        public NestedMappingScenario(IDionysContext context, IMapper mapper)
        {
            CreateMap<ConsumedProduct, ConsumedProductResponseDTO>()
                .ForMember(d => d.Product, opt => opt.Ignore())
                .ForMember(d => d.ProductId, opt => opt.Ignore())
                .AfterMap((s, d) => { d.Product = mapper.Map<ProductDTO>(context.Products.Find(s.Product?.Id)); })
                .AfterMap((s, d) => { d.ProductId = s.Product?.Id ?? Guid.Empty; });
        }
    }
}
