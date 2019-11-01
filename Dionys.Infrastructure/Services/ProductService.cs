using System;
using System.Collections.Generic;
using System.Linq;
using Dionys.Infrastructure.Extensions;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Infrastructure.Services
{
    public interface IProductService : ICrudService<Product>
    {
        IEnumerable<Product> GetAll(bool includeDeleted = false);
        bool IsExist(Guid id, bool includeDeleted);
    }

    public class ProductService : IProductService
    {
        private readonly DionysContext _context;

        public ProductService(DionysContext context)
        {
            _context = context;
        }

        public bool Create(Product product, bool ignoreValidator = false)
        {
            if (!ignoreValidator && !Validate(product))
                return false;

            _context.Products.Add(product);

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

        public bool Update(Product product, bool ignoreValidator = false)
        {
            if (!ignoreValidator && !Validate(product))
                return false;

            _context.Products.Update(product);

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

        public bool Delete(Product product, bool ignoreValidator = false)
        {
            try
            {
                var dbProduct = _context.Products.First(p => p.Id == product.Id && !p.IsDeleted());
                dbProduct.SetDeleted();

                return Update(product);
            }
            catch
            {
                return false;
            }
        }

        public Product GetById(Guid id, bool includeCopmlexEntities = true)
        {
            var products = _context.Products.Where(x => x.Id == id);

            if (!products.Any())
                throw new NotFoundEntityServiceException($"Cannot find entity by id: ${id}");

            return products.First();
        }

        public IEnumerable<Product> GetAll(bool includeDeleted = false)
        {
            return _context.Products.OrderBy(p => p.Id).Where(p => !p.DeletedAt.HasValue || includeDeleted);
        }

        public IEnumerable<Product> SearchByName(string searchParameter)
        {
            return GetAll().Where(p => EF.Functions.Like(p.Name, searchParameter)).OrderBy(p => p.Name);
        }

        public bool IsExist(Guid id)
        {
            return IsExist(id, false);
        }

        public bool IsExist(Guid id, bool includeDeleted)
        {
            return _context.Products.Any(p => p.Id == id && (includeDeleted || !p.DeletedAt.HasValue));
        }

        private static bool Validate(IDbModel product)
        {
            return product != null;
        }
    }
}
