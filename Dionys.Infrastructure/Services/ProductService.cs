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

        public bool Create(Product entity, bool ignoreValidator = false)
        {
            if (!ignoreValidator && !Validate(entity))
                return false;

            _context.Products.Add(entity);

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

        public bool Update(Product entity, bool ignoreValidator = false)
        {
            if (!ignoreValidator && !Validate(entity))
                return false;

            _context.Products.Update(entity);

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

        public bool Delete(Product entity, bool ignoreValidator = false)
        {
            try
            {
                var dbProduct = _context.Products.First(p => p.Id == entity.Id && !p.IsDeleted());
                dbProduct.DeletedAt = DateTime.Now;

                return Update(entity);
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

            if (!includeDeleted)
                return products.Where(p => !p.IsDeleted());
            return products;
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
            return true;
        }
    }
}
