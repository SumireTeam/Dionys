using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dionys.Models;
using Dionys.Models.DTO;

namespace Dionys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // DTO to Database Entity mapper
        private readonly IMapper _mapper;

        // Database Context
        private readonly IDionysContext _context;

        public ProductsController(IDionysContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public IQueryable<ProductDTO> GetProducts()
        {
            var products = from p in _context.Products select _mapper.Map<ProductDTO>(p);

            return products;
        }

        // GET: api/Products/name/{name}
        [HttpGet("search/{name}")]
        public IQueryable<ProductDTO> GetProductsSearch(string name)
        {
            var products = from p in _context.Products
                where EF.Functions.Like(p.Name, name)
                select _mapper.Map<ProductDTO>(p);

            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var dtoProduct = _mapper.Map<ProductDTO>(product);
            return dtoProduct;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, ProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDto);
            _context.MarkAsModified(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDto)
        {
            Product product = _mapper.Map<Product>(productDto);

            do
            {
                product.Id = new Guid();
            } while (_context.Products.Find(product.Id) != null);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, _mapper.Map<ProductDTO>(product));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
