using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dionys.Infrastructure.Extensions;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Services;
using Dionys.Web.Models.DTO;
using Dionys.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dionys.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]

        public PagingViewModel<ProductViewModel> GetProducts([FromQuery] PagingParameterModel pagingModel)
        {
            var products = _productService.GetAll().Skip(pagingModel.Page * pagingModel.ElementsPerPage)
                .Take(pagingModel.ElementsPerPage)
                .Select(x => _mapper.Map<ProductViewModel>(x)).ToList();

            return new PagingViewModel<ProductViewModel> { Elements = products.Count, Items = products };
        }

        // GET: api/Products/name/{name}
        [HttpGet("search/")]
        public IEnumerable<ProductViewModel> GetProductsSearch(string q)
        {
            var products = _productService.SearchByName(q);

            return products.Select(p => _mapper.Map<ProductViewModel>(p));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<ProductViewModel> GetProduct(Guid id)
        {
            var product = _productService.GetById(id);

            if (product == null || product.IsDeleted())
                return NotFound();

            return _mapper.Map<ProductViewModel>(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult PutProduct(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
                return BadRequest();

            var product = _mapper.Map<Product>(productViewModel);

            if(_productService.Update(product))
                return NoContent();
            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public ActionResult<ProductViewModel> PostProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);

            if(_productService.Create(product))
                return CreatedAtAction("GetProduct", new { id = product.Id }, _mapper.Map<ProductViewModel>(product));
            return Redirect("List");
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public ActionResult<ProductViewModel> DeleteProduct(Guid id)
        {
            var product = _productService.GetById(id);
            _productService.Delete(_productService.GetById(id));

            return _mapper.Map<ProductViewModel>(product);
        }
    }
}
