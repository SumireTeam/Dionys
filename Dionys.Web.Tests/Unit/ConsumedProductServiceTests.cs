using System;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Dionys.Web.Tests.Unit
{
    public class ConsumedProductServiceTests
    {
        [Test]
        public void IsConsumedProductExists_Pass()
        {
            // arrange
            var contextOptions = new DbContextOptionsBuilder<DionysContext>()
                .UseInMemoryDatabase("IsConsumedProductExists_Pass")
                .Options;

            var consumedProduct = new ConsumedProduct
            {
                ProductId = Guid.Parse("274684A2-D52B-4FB8-8BAD-1F065BA76071"),
                Timestamp = DateTime.UtcNow,
            };

            using (var ctx = new DionysContext(contextOptions))
            {
                ctx.ConsumedProducts.Add(consumedProduct);
                ctx.SaveChanges();
            }

            // act
            bool result;
            using (var ctx = new DionysContext(contextOptions))
            {
                var service = new ConsumedProductService(ctx);
                result = service.IsExist(consumedProduct.Id);
            }

            // assert
            Assert.That(result == true);
        }

        [Test]
        public void IsConsumedProductExists_Fail()
        {
            // arrange
            var contextOptions = new DbContextOptionsBuilder<DionysContext>()
                .UseInMemoryDatabase("IsConsumedProductExists_Fail")
                .Options;

            var consumedProduct = new ConsumedProduct
            {
                ProductId = Guid.Parse("274684A2-D52B-4FB8-8BAD-1F065BA76071"),
                Timestamp = DateTime.UtcNow,
            };

            using (var ctx = new DionysContext(contextOptions))
            {
                ctx.ConsumedProducts.Add(consumedProduct);
                ctx.SaveChanges();
            }

            // act
            bool result;
            using (var ctx = new DionysContext(contextOptions))
            {
                new ConsumedProductService(ctx).Delete(consumedProduct);
                ctx.SaveChanges();
            }

            using (var ctx = new DionysContext(contextOptions))
            {
                result = new ConsumedProductService(ctx).IsExist(consumedProduct.Id);
            }

            // assert
            Assert.That(result == false);
        }
    }
}
