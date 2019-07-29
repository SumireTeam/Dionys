using System;
using AutoMapper;
using Dionys.Infrastructure.Models;
using Dionys.Models.DTO;
using Dionys.Tests.Setup;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dionys.Tests
{
    public class MappingScenarioTest
    {
        private readonly IMapper        _mapper;
        private readonly InMemoryDionysContext _context;

        public MappingScenarioTest()
        {
            IServiceProvider serviceProvider = new ContainerConfiguration().GetServiceProvider();

            _mapper  = serviceProvider.GetService<IMapper>();
            _context = serviceProvider.GetService<InMemoryDionysContext>();
        }

        [Fact]
        public void Test_Product_To_ProductDTO_Success()
        {
            Product product = new Product
            {
                Calories = 992.5F,
                Carbohydrates = 12.5F,
                Description = "Sample description",
                Fat = 42.6F,
                Id = Guid.Parse("A9B7FA6B-B6D7-436A-A925-14989655604B"),
                Name = "Sample name",
                Protein = 22.6F
            };

            ProductDTO productDTO = _mapper.Map<ProductDTO>(product);

            Assert.Equal(product.Calories, productDTO.Calories);
            Assert.Equal(product.Carbohydrates, productDTO.Carbohydrates);
            Assert.Equal(product.Description, productDTO.Description);
            Assert.Equal(product.Fat, productDTO.Fat);
            Assert.Equal(product.Id, productDTO.Id);
            Assert.Equal(product.Name, productDTO.Name);
            Assert.Equal(product.Protein, productDTO.Protein);
        }

        [Fact]
        public void Test_ProductDTO_To_Product_Success()
        {
            ProductDTO productDTO = new ProductDTO
            {
                Calories = 992.5F,
                Carbohydrates = 12.5F,
                Description = "Sample description",
                Fat = 42.6F,
                Id = Guid.Parse("A9B7FA6B-B6D7-436A-A925-14989655604B"),
                Name = "Sample name",
                Protein = 22.6F
            };

            Product product = _mapper.Map<Product>(productDTO);

            Assert.Equal(product.Calories, productDTO.Calories);
            Assert.Equal(product.Carbohydrates, productDTO.Carbohydrates);
            Assert.Equal(product.Description, productDTO.Description);
            Assert.Equal(product.Fat, productDTO.Fat);
            Assert.Equal(product.Id, productDTO.Id);
            Assert.Equal(product.Name, productDTO.Name);
            Assert.Equal(product.Protein, productDTO.Protein);
        }

        [Fact]
        public void ConsumedProductRequestDTO_To_ConsumedProduct_Success()
        {
            // Create Sample Product
            Product product = new Product
            {
                Id = new Guid("274684A2-D52B-4FB8-8BAD-1F065BA76071"),
                Name = "Баклажан",
                Protein = 1.2f,
                Fat = 0.1f,
                Carbohydrates = 4.5f,
                Calories = 24f,
                Description = "Баклажан как баклажан. На вкус как баклажан, на вид как баклажан. Ничего удивительного."
            };

            ConsumedProductRequestDTO consumedProductRequestDTO = new ConsumedProductRequestDTO
            {
                Id        = new Guid("274684A2-D52B-4FB8-8BAD-1F065BA760AA"),
                ProductId = new Guid("274684A2-D52B-4FB8-8BAD-1F065BA76071"),
                Timestamp = new DateTime(2017, 05, 06),
                Weight    = 600
            };
            
            // Put Sample Product to Database
            _context.Products.Add(product);
            _context.SaveChanges();

            // Convert
            ConsumedProduct consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductRequestDTO);

            // Check
            Assert.Equal(consumedProduct.Id, consumedProductRequestDTO.Id);
            Assert.Equal(consumedProduct.Product.Id, consumedProductRequestDTO.ProductId);
            Assert.Equal(consumedProduct.Timestamp, consumedProductRequestDTO.Timestamp);
            Assert.Equal(consumedProduct.Weight, consumedProductRequestDTO.Weight);
        }

        [Fact]
        public void ConsumedProduct_To_ConsumedProductResponseDTO_Success()
        {
            // Create Sample Product
            Product product = new Product
            {
                Id = new Guid("274684A2-D52B-4FB8-8BAD-1F065BA76071"),
                Name = "Баклажан",
                Protein = 1.2f,
                Fat = 0.1f,
                Carbohydrates = 4.5f,
                Calories = 24f,
                Description = "Баклажан как баклажан. На вкус как баклажан, на вид как баклажан. Ничего удивительного."
            };

            // Create Sample ConsumedProduct entity
            ConsumedProduct consumedProduct = new ConsumedProduct
            {
                Id = new Guid("274684A2-D52B-4FB8-8BAD-1F065BA76074"),
                Product = product,
                Timestamp = new DateTime(2017,05, 06),
                Weight = 90
            };

            ConsumedProductResponseDTO consumedProductResponseDTO =
                _mapper.Map<ConsumedProductResponseDTO>(consumedProduct);

            // Check
            Assert.Equal(consumedProduct.Id, consumedProductResponseDTO.Id);
            Assert.Equal(consumedProduct.Product.Id, consumedProductResponseDTO.ProductId);
            Assert.Equal(consumedProduct.Product.Id, consumedProductResponseDTO.Product.Id);
            Assert.Equal(consumedProduct.Timestamp, consumedProductResponseDTO.Timestamp);
            Assert.Equal(consumedProduct.Weight, consumedProductResponseDTO.Weight);
        }
    }
}
