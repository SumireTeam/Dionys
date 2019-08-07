using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dionys.Infrastructure.Extensions;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Models.DTO;
using Dionys.Infrastructure.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Infrastructure.Services
{
    public class ConsumedProductService : IService, IConsumedProductService
    {
        private readonly IDionysContext _context;
        private readonly IMapper        _mapper ;

        public ConsumedProductService(IDionysContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public void Create(ConsumedProductDTO consumedProductDto)
        {
            Validate(consumedProductDto);

            // Find assoc product
            Product product = _context.Products.Find(consumedProductDto.ProductId);

            ConsumedProduct consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductDto);

            consumedProduct.Product = product;
            _context.ConsumedProducts.Add(consumedProduct);

            _context.SaveChanges();
        }

        public void Update(ConsumedProductDTO consumedProductDto, bool ignoreValidator = false)
        {
            if (!ignoreValidator)
            {
                Validate(consumedProductDto);
            }

            // Find assoc product
            Product product = _context.Products.Find(consumedProductDto.ProductId);
            ConsumedProduct consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductDto);
            consumedProduct.Product = product;

            // Do not update assoc product (member)
            _context.MarkAsUnchanged(consumedProduct.Product);
            _context.ConsumedProducts.Update(consumedProduct);

            _context.SaveChanges();
        }

        public void Delete(ConsumedProductDTO consumedProductDto, bool ignoreValidator = false)
        {
            if (!ignoreValidator)
            {
                Validate(consumedProductDto);
            }

            ConsumedProduct consumedProduct = _context.ConsumedProducts.Find(consumedProductDto.Id);
            _context.ConsumedProducts.Remove(consumedProduct);
            _context.SaveChanges();
        }

        public ConsumedProductDTO GetById(Guid id, bool includeCopmlexEntities = true)
        {
            ConsumedProduct consumedProduct = _context.ConsumedProducts.First(x => x.Id == id);

            if (includeCopmlexEntities)
            {
                // HACK: Load ref entity
                ((DbContext)_context).Entry(consumedProduct).Reference(x => x.Product).Load();
            }

            if (consumedProduct.IsNew())
                throw new NotFoundEntityServiceException($"Cannot find {consumedProduct.GetType()} entity by id: ${id}");

            return _mapper.Map<ConsumedProductDTO>(consumedProduct);
        }

        public IEnumerable<ConsumedProductDTO> GetAll(bool includeCopmlexEntities = true)
        {
            var consumedProducts = _context.ConsumedProducts;
            return consumedProducts.Select(x => _mapper.Map<ConsumedProductDTO>(x));
        }

        public IEnumerable<ConsumedProductDTO> SearchByName(string searchPattern)
        {
            throw new NotImplementedException();
        }


        public ProductDTO GetAssociatedProductDTO(ConsumedProductDTO consumedProductDto)
        {
            ConsumedProduct consumedProduct = _context.ConsumedProducts.Find(consumedProductDto.Id);
            Product product = _context.ConsumedProducts.Find(consumedProduct.Id).Product;

            return _mapper.Map<ProductDTO>(product);
        }

        private void Validate(ConsumedProductDTO consumedProductDto)
        {
            bool isProductValid = _context.Products.Any(x => !x.IsDeleted() && consumedProductDto.Id == x.Id);

            if (!isProductValid) throw new Exception("Invalid Product. Is product deleted?");
        }
    }

    public interface IConsumedProductService
    {
        ProductDTO GetAssociatedProductDTO(ConsumedProductDTO consumedProductDto);
        void Create(ConsumedProductDTO consumedProductDto);
        void Update(ConsumedProductDTO consumedProductDto, bool ignoreValidator = false);
        void Delete(ConsumedProductDTO consumedProductDto, bool ignoreValidator = false);
        ConsumedProductDTO GetById(Guid id, bool includeCopmlexEntities = true);
        IEnumerable<ConsumedProductDTO> GetAll(bool includeCopmlexEntities = true);

        IEnumerable<ConsumedProductDTO> SearchByName(string searchPa);

    }
}
