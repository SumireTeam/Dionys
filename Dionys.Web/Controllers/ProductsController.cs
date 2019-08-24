using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dionys.Infrastructure.Extensions;
using Dionys.Infrastructure.Models;
using Dionys.Web.Models.DTO;
using Dionys.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        // DTO to Database Entity mapper
        private readonly IMapper _mapper;

        // Database Context
        private readonly IDionysContext _context;

        public ProductsController(IDionysContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]

        public PagingViewModel<ProductViewModel> GetProducts([FromQuery] PagingParameterModel pagingModel)
        {
            var products = _context.Products.Where(x => !x.IsDeleted())
                .Skip(pagingModel.Page * pagingModel.ElementsPerPage)
                .Take(pagingModel.ElementsPerPage)
                .Select(x => _mapper.Map<ProductViewModel>(x));

            return new PagingViewModel<ProductViewModel> { Elements = products.Count(), Items = products };
        }

        // GET: api/Products/name/{name}
        [HttpGet("search/{name}")]
        public IQueryable<ProductViewModel> GetProductsSearch(string name)
        {
            var products = _context.Products.Where(p => EF.Functions.Like(p.Name, name))
                .Select(p => _mapper.Map<ProductViewModel>(p));

            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.IsDeleted())
                return NotFound();

            var dtoProduct = _mapper.Map<ProductViewModel>(product);
            return dtoProduct;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
                return BadRequest();

            var product = _mapper.Map<Product>(productViewModel);

            // Is deleted?
            var originalProduct = _context.Products.Find(productViewModel);

            if (originalProduct.Id != productViewModel.Id || originalProduct.IsDeleted())
                return NotFound();

            _context.MarkAsModified(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> PostProduct(ProductViewModel productViewModel)
        {
            Product product = _mapper.Map<Product>(productViewModel);

            do
            {
                product.Id = new Guid();
            } while (_context.Products.Find(product.Id) != null);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, _mapper.Map<ProductViewModel>(product));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.IsDeleted())
                return NotFound();

            product.SetDeleted();

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductViewModel>(product);
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
