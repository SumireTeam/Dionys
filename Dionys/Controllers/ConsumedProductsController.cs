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
    public class ConsumedProductsController : ControllerBase
    {
        private readonly IDionysContext _context;

        private readonly IMapper _mapper;

        public ConsumedProductsController(IDionysContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        // GET: api/ConsumedProducts
        [HttpGet]
        public IQueryable<ConsumedProductDTO> GetConsumedProducts()
        {
            var consumedProductDtos = from consumedProduct in _context.ConsumedProducts
                                select _mapper.Map<ConsumedProductDTO>(consumedProduct);

            return consumedProductDtos;
        }

        // GET: api/ConsumedProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumedProductDTO>> GetConsumedProduct(Guid id)
        {
            var consumedProduct = await _context.ConsumedProducts.FindAsync(id);

            if (consumedProduct == null)
            {
                return NotFound();
            }

            return _mapper.Map<ConsumedProductDTO>(consumedProduct);
        }

        // PUT: api/ConsumedProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumedProduct(Guid id, ConsumedProduct consumedProduct)
        {
            if (id != consumedProduct.Id)
            {
                return BadRequest();
            }

            _context.MarkAsModified(consumedProduct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumedProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ConsumedProducts
        [HttpPost]
        public async Task<ActionResult<ConsumedProduct>> PostConsumedProduct(ConsumedProductDTO consumedProductDTO)
        {
            ConsumedProduct consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductDTO);

            // HACK: EF trying to insert new entity Product
            Product product = _context.Products.Find(consumedProduct.Product.Id);
            consumedProduct.Product = product;

            _context.ConsumedProducts.Add(consumedProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsumedProduct", new { id = consumedProduct.Id }, consumedProduct);
        }

        // DELETE: api/ConsumedProducts/5
        [HttpDelete("{id}")]
        public ActionResult<ConsumedProductDTO> DeleteConsumedProduct(Guid id)
        {
            var consumedProduct = _context.ConsumedProducts.Find(id);
            if (consumedProduct == null)
            {
                return NotFound();
            }

            _context.ConsumedProducts.Remove(consumedProduct);
            _context.SaveChanges();

            return _mapper.Map<ConsumedProductDTO>(consumedProduct);
        }

        private bool ConsumedProductExists(Guid id)
        {
            return _context.ConsumedProducts.Any(e => e.Id == id);
        }
    }
}
