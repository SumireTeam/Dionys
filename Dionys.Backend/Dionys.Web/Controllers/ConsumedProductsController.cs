using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Services;
using Dionys.Web.Models.Api;
using Dionys.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dionys.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumedProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConsumedProductService _consumedProductService;

        public ConsumedProductsController(IMapper mapper, IConsumedProductService consumedProductService)
        {
            _mapper = mapper;
            _consumedProductService = consumedProductService;
        }

        // GET: api/ConsumedProducts/?page={page}&count={count}
        [DisplayName("ConsumedProductList")]
        [HttpGet]
        public PagingViewModel<ConsumedProductResponse> GetConsumedProducts([FromQuery] PagingParameterModel paging)
        {
            // TODO: Write mappings.

            var consumedProducts = _consumedProductService.GetAll().Select(x => new ConsumedProductResponse
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Timestamp = x.Timestamp,
                Weight = x.Weight,
                Product = new ProductViewModel
                {
                    Id = x.ProductId,
                    Calories = x.Product.Calories,
                    Carbohydrates = x.Product.Carbohydrates,
                    Description = x.Product.Description,
                    Fat = x.Product.Fat,
                    Name = x.Product.Name,
                    Protein = x.Product.Protein
                }
            })
                .Select(x => _mapper.Map<ConsumedProductResponse>(x))
                .Skip(paging.Page * paging.ElementsPerPage).Take(paging.ElementsPerPage).ToImmutableArray();

            return new PagingViewModel<ConsumedProductResponse>(consumedProducts);
        }

        // GET: api/ConsumedProducts/5
        [HttpGet("{id}")]
        public ActionResult<ConsumedProductResponse> GetConsumedProduct(Guid id)
        {
            var consumedProduct = _consumedProductService.GetById(id);

            if (consumedProduct == null)
                return NotFound();

            return _mapper.Map<ConsumedProductResponse>(consumedProduct);
        }

        // PUT: api/ConsumedProducts/5
        [HttpPut("{id}")]
        public IActionResult PutConsumedProduct(Guid id, ConsumedProductRequest consumedProductRequest)
        {
            if (id != consumedProductRequest.Id)
                return BadRequest();

            var consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductRequest);

            _consumedProductService.Update(consumedProduct);

            return NoContent();
        }

        // POST: api/ConsumedProducts
        [HttpPost]
        public ActionResult<ConsumedProductResponse> PostConsumedProduct(ConsumedProductRequest consumedProductReqest)
        {
            var consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductReqest);
            _consumedProductService.Create(consumedProduct);

            return CreatedAtAction("GetConsumedProduct", new { id = consumedProduct.Id }, consumedProduct);
        }

        // DELETE: api/ConsumedProducts/5
        [HttpDelete("{id}")]
        public ActionResult<ConsumedProductRequest> DeleteConsumedProduct(Guid id)
        {
            var consuledProduct = _consumedProductService.GetById(id);
            _consumedProductService.Delete(consuledProduct);

            return _mapper.Map<ConsumedProductRequest>(consuledProduct);
        }
    }
}
