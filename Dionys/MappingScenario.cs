using System;
using AutoMapper;
using Dionys.Infrastructure.Models;
using Dionys.Models.DTO;

namespace Dionys
{
    public class MappingScenario : Profile
    {
        public MappingScenario(IDionysContext context)
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<ConsumedProductRequestViewModel, ConsumedProduct>()
                .ForMember(d => d.Product, opt => opt.Ignore())
                .AfterMap((d, e) => { e.Product = context.Products.Find(d.ProductId); });
        }
    }

    public class NestedMappingScenario : Profile
    {
        public NestedMappingScenario(IDionysContext context, IMapper mapper)
        {
            CreateMap<ConsumedProduct, ConsumedProductResponseViewModel>()
                .ForMember(d => d.Product, opt => opt.Ignore())
                .ForMember(d => d.ProductId, opt => opt.Ignore())
                .AfterMap((s, d) => { d.Product = mapper.Map<ProductViewModel>(context.Products.Find(s.Product?.Id)); })
                .AfterMap((s, d) => { d.ProductId = s.Product?.Id ?? Guid.Empty; });

            CreateMap<ConsumedProduct, ConsumedProductRequestViewModel>()
                .ForMember(d => d.ProductId, opt => opt.Ignore())
                .AfterMap((s, d) => { d.ProductId = s.Product?.Id ?? Guid.Empty; });
        }
    }
}
