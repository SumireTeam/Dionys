using System;
using System.Collections.Generic;
using System.Linq;
using Dionys.Infrastructure.Extensions;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Dionys.Infrastructure.Services
{
    public interface IConsumedProductService : ICrudService<ConsumedProduct>
    {
        IEnumerable<ConsumedProduct> GetAll(bool includeCopmlexEntities = true);
        Product GetAssociatedProduct(ConsumedProduct entity);
    }

    public class ConsumedProductService : IConsumedProductService
    {
        private readonly DionysContext _context;

        public ConsumedProductService(DionysContext context)
        {
            _context = context;
        }

        public bool Create(ConsumedProduct consumedProduct, bool ignoreValidator = false)
        {
            if (!ignoreValidator && !Validate(consumedProduct))
                return false;

            // Find assoc product
            var product = _context.Products.Find(consumedProduct.Product.Id);

            consumedProduct.Product = product;
            _context.ConsumedProducts.Add(consumedProduct);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(ConsumedProduct consumedProduct, bool ignoreValidator = false)
        {
            if (!ignoreValidator && !Validate(consumedProduct))
                return false;

            // Do not update assoc product (member)
            consumedProduct.Product = null;
            _context.ConsumedProducts.Update(consumedProduct);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(ConsumedProduct consumedProduct, bool ignoreValidator = false)
        {
            //if (!ignoreValidator && !Validate(consumedProduct))
            //    return false;

            _context.ConsumedProducts.Remove(consumedProduct);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ConsumedProduct GetById(Guid id, bool includeCopmlexEntities = true)
        {
            var consumedProduct = _context.ConsumedProducts.FirstOr(x => x.Id == id, new ConsumedProduct());

            if (includeCopmlexEntities)
                _context.Entry(consumedProduct).Reference(x => x.Product).Load();

            if (consumedProduct.IsNew())
                throw new NotFoundEntityServiceException($"Cannot find {consumedProduct.GetType()} entity by id: ${id}");

            return consumedProduct;
        }

        public IEnumerable<ConsumedProduct> GetAll(bool includeCopmlexEntities = true)
        {
            var consumedProductList = _context.ConsumedProducts.OrderBy(cp => cp.Id);

            if (includeCopmlexEntities)
                return consumedProductList.Include(e => e.Product);
            return consumedProductList;
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
