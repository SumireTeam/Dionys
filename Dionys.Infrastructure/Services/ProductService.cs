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
        IEnumerable<Product> GetAll();
        bool IsExist(Guid id, bool includeDeleted);
    }

    public class ProductService : IProductService
    {
        private readonly DionysContext _context;

        public ProductService(DionysContext context)
        {
            _context = context;
        }

        public void Create(Product product)
        {
            Validate(product);

            _context.Products.Add(product);

            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            Validate(product);
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            var dbProduct = _context.Products.First(p => p.Id == product.Id && !p.IsDeleted());
            dbProduct.SetDeleted();

            Update(product);
        }

        public Product GetById(Guid id)
        {
            var products = _context.Products.Where(x => x.Id == id);

            if (!products.Any())
                throw new NotFoundEntityServiceException($"Cannot find entity by id: ${id}");

            return products.First();
        }

        public Product GetByIdOr(Guid id, IDbModel entity)
        {
            throw new NotImplementedException();
        }

        public Product GetByIdOrDefault(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.OrderBy(p => p.Id).Where(p => !p.DeletedAt.HasValue);
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

        private static void Validate(IDbModel product)
        {
            if(product != null)
                throw new Exception();
        }
    }
}
