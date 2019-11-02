using System;
using System.Collections.Generic;
using System.Linq;
using Dionys.Infrastructure.Extensions;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Infrastructure.Services
{
    public interface IConsumedProductService : ICrudService<ConsumedProduct>
    {
        IEnumerable<ConsumedProduct> GetAll();
        Product GetAssociatedProduct(ConsumedProduct entity);
    }

    public class ConsumedProductService : IConsumedProductService
    {
        private readonly DionysContext _context;

        public ConsumedProductService(DionysContext context)
        {
            _context = context;
        }

        public bool TryCreate(ConsumedProduct consumedProduct)
        {
            try
            {
                if (!Validate(consumedProduct))
                    return false;

                // Find assoc product
                consumedProduct.Product = _context.Products.Find(consumedProduct.Product.Id);
                _context.ConsumedProducts.Add(consumedProduct);

                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Create(ConsumedProduct consumedProduct)
        {
            Validate(consumedProduct);

            // Find assoc product
            var product = _context.Products.Find(consumedProduct.Product.Id);

            consumedProduct.Product = product;
            _context.ConsumedProducts.Add(consumedProduct);

            _context.SaveChanges();
        }

        public void Update(ConsumedProduct consumedProduct)
        {
            Validate(consumedProduct);

            // Do not update assoc product (member)
            consumedProduct.Product = null;
            _context.ConsumedProducts.Update(consumedProduct);

            _context.SaveChanges();
        }

        public void Delete(ConsumedProduct consumedProduct)
        {
            //if (!ignoreValidator && !Validate(consumedProduct))
            //    return false;

            _context.ConsumedProducts.Remove(consumedProduct);
            _context.SaveChanges();
        }

        public ConsumedProduct GetById(Guid id)
        {
            var consumedProduct = _context.ConsumedProducts.Single(x => x.Id == id);

            _context.Entry(consumedProduct).Reference(x => x.Product).Load();

            if (consumedProduct.Id == Guid.Empty)
                throw new NotFoundEntityServiceException($"Cannot find {consumedProduct.GetType()} entity by id: ${id}");

            return consumedProduct;
        }

        public ConsumedProduct GetByIdOr(Guid id, IDbModel entity)
        {
            throw new NotImplementedException();
        }

        public ConsumedProduct GetByIdOrDefault(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConsumedProduct> GetAll()
        {
            return _context.ConsumedProducts.OrderBy(cp => cp.Id).Include(e => e.Product);
        }

        public IEnumerable<ConsumedProduct> SearchByName(string searchPattern)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Guid id)
        {
            return _context.ConsumedProducts.Any(cp => cp.Id == id);
        }

        public Product GetAssociatedProduct(ConsumedProduct consumedProduct)
        {
            return _context.ConsumedProducts.Find(consumedProduct.Id).Product;
        }

        private bool Validate(ConsumedProduct consumedProduct)
        {
            return _context.Products.Any(x => !x.IsDeleted() && consumedProduct.ProductId == x.Id);
        }
    }
}
