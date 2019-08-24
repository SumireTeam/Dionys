using System;
using System.Collections.Generic;
using System.Text;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Dionys.Web.Tests.Unit
{
    public class ConsumedProductServiceTests
    {
        public DbContextOptions<DionysContext> ContextOptions;

        [SetUp]
        public void SetUp()
        {
            ContextOptions = new DbContextOptionsBuilder<DionysContext>()
                .UseInMemoryDatabase("tests")
                .Options;
        }

        [Test]
        public void IsConsumedProductExists_Pass()
        {
            // arrange
            var consumedProduct = new ConsumedProduct
            {
                ProductId = Guid.Parse("274684A2-D52B-4FB8-8BAD-1F065BA76071"),
                Timestamp = DateTime.UtcNow,
            };
            var ctx = new DionysContext(ContextOptions);

            ctx.ConsumedProducts.Add(consumedProduct);
            ctx.SaveChanges();
            var service = new ConsumedProductService(ctx);

            // act
            var result = service.IsExists(consumedProduct.Id);

            ctx.Dispose();

            // assert
            Assert.That(result == true);
        }

        [Test]
        public void IsConsumedProductExists_Fail()
        {
            // arrange
            var consumedProduct = new ConsumedProduct
            {
                ProductId = Guid.Parse("274684A2-D52B-4FB8-8BAD-1F065BA76071"),
                Timestamp = DateTime.UtcNow,
            };
            var ctx = new DionysContext(ContextOptions);

            ctx.ConsumedProducts.Add(consumedProduct);
            ctx.SaveChanges();
            var service = new ConsumedProductService(ctx);

            // act
            service.Delete(consumedProduct);
            var result = service.IsExists(consumedProduct.Id);

            ctx.Dispose();

            // assert
            Assert.That(result == false);
        }
    }
}
