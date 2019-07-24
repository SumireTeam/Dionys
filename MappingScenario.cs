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

            CreateMap<ConsumedProduct, ConsumedProductDTO>()
                .ForMember(d => d.Product, opt => opt.Ignore())
                .AfterMap((d, e) => { e.Product = d.Product.Id; });

            CreateMap<ConsumedProductDTO, ConsumedProduct>()
                .ForMember(d => d.Product, opt => opt.Ignore())
                .AfterMap((d, e) => { e.Product = context.Products.Find(d.Product); });
        }
    }
}
