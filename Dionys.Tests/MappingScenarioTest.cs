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

            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);

            Assert.Equal(product.Calories, productViewModel.Calories);
            Assert.Equal(product.Carbohydrates, productViewModel.Carbohydrates);
            Assert.Equal(product.Description, productViewModel.Description);
            Assert.Equal(product.Fat, productViewModel.Fat);
            Assert.Equal(product.Id, productViewModel.Id);
            Assert.Equal(product.Name, productViewModel.Name);
            Assert.Equal(product.Protein, productViewModel.Protein);
        }

        [Fact]
        public void Test_ProductDTO_To_Product_Success()
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                Calories = 992.5F,
                Carbohydrates = 12.5F,
                Description = "Sample description",
                Fat = 42.6F,
                Id = Guid.Parse("A9B7FA6B-B6D7-436A-A925-14989655604B"),
                Name = "Sample name",
                Protein = 22.6F
            };

            Product product = _mapper.Map<Product>(productViewModel);

            Assert.Equal(product.Calories, productViewModel.Calories);
            Assert.Equal(product.Carbohydrates, productViewModel.Carbohydrates);
            Assert.Equal(product.Description, productViewModel.Description);
            Assert.Equal(product.Fat, productViewModel.Fat);
            Assert.Equal(product.Id, productViewModel.Id);
            Assert.Equal(product.Name, productViewModel.Name);
            Assert.Equal(product.Protein, productViewModel.Protein);
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

            ConsumedProductRequestViewModel consumedProductRequestViewModel = new ConsumedProductRequestViewModel
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
            ConsumedProduct consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductRequestViewModel);

            // Check
            Assert.Equal(consumedProduct.Id, consumedProductRequestViewModel.Id);
            Assert.Equal(consumedProduct.Product.Id, consumedProductRequestViewModel.ProductId);
            Assert.Equal(consumedProduct.Timestamp, consumedProductRequestViewModel.Timestamp);
            Assert.Equal(consumedProduct.Weight, consumedProductRequestViewModel.Weight);
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

            ConsumedProductResponseViewModel consumedProductResponseViewModel =
                _mapper.Map<ConsumedProductResponseViewModel>(consumedProduct);

            // Check
            Assert.Equal(consumedProduct.Id, consumedProductResponseViewModel.Id);
            Assert.Equal(consumedProduct.Product.Id, consumedProductResponseViewModel.ProductId);
            Assert.Equal(consumedProduct.Product.Id, consumedProductResponseViewModel.Product.Id);
            Assert.Equal(consumedProduct.Timestamp, consumedProductResponseViewModel.Timestamp);
            Assert.Equal(consumedProduct.Weight, consumedProductResponseViewModel.Weight);
        }
    }
}
