using System;
using System.Collections.Generic;
using System.Text;
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
        private readonly IMapper          _mapper;

        public MappingScenarioTest()
        {
            IServiceProvider serviceProvider = new ContainerConfiguration()
                .GetServiceCollection()
                .BuildServiceProvider();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(serviceProvider.GetService<MappingScenario>());
            });

            _mapper = mappingConfig.CreateMapper();
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
    }
}
