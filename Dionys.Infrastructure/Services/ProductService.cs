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
            var entity = _context.Products.FirstOr(x => x.Id == id, new Product());

            if (entity.IsNew())
                throw new NotFoundEntityServiceException($"Cannot find {entity.GetType()} entity by id: ${id}");

            return entity;
        }

        public IEnumerable<Product> GetAll(bool includeDeleted = false)
        {
            var products = _context.Products.OrderBy(p => p.Id);

            return !includeDeleted ? products.Where(p => !p.IsDeleted()) : products;
        }

        public IEnumerable<Product> SearchByName(string searchParameter)
        {
            var products = GetAll()
                .Where(p => EF.Functions.Like(p.Name, searchParameter))
                .OrderBy(p => p.Name);

            return products;
        }

        public bool IsExist(Guid id)
        {
            return IsExist(id, false);
        }

        public bool IsExist(Guid id, bool includeDeleted)
        {
            if(includeDeleted)
                return _context.Products.Any(p => p.Id == id);
            return _context.Products.Any(p => p.Id == id && p.IsDeleted() == includeDeleted);
        }

        private bool Validate(Product product)
        {
            return product != null;
        }
    }
}
