using System;
using System.Collections.Generic;
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
        public IEnumerable<ConsumedProductResponseDTO> GetConsumedProducts()
        {
            var consumedProductDtos = from consumedProduct in _context.ConsumedProducts
                                select _mapper.Map<ConsumedProductResponseDTO>(consumedProduct);

            return consumedProductDtos;
        }

        // GET: api/ConsumedProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumedProductResponseDTO>> GetConsumedProduct(Guid id)
        {
            var consumedProduct = await _context.ConsumedProducts.FindAsync(id);

            if (consumedProduct == null)
            {
                return NotFound();
            }

            return _mapper.Map<ConsumedProductResponseDTO>(consumedProduct);
        }

        // PUT: api/ConsumedProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumedProduct(Guid id, ConsumedProductRequestDTO consumedProductRequestDTO)
        {
            if (id != consumedProductRequestDTO.Id)
            {
                return BadRequest();
            }

            var consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductRequestDTO);

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
        public async Task<ActionResult<ConsumedProductResponseDTO>> PostConsumedProduct(ConsumedProductRequestDTO consumedProductReqestDTO)
        {
            ConsumedProduct consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductReqestDTO);

            // HACK: EF trying to insert new entity Product
            Product product = _context.Products.Find(consumedProductReqestDTO.ProductId);
            consumedProduct.Product = product;

            _context.ConsumedProducts.Add(consumedProduct);
            await _context.SaveChangesAsync();

            var consumedProductResponseDTO = _mapper.Map<ConsumedProductResponseDTO>(consumedProduct);

            return CreatedAtAction("GetConsumedProduct", new { id = consumedProductResponseDTO.Id }, consumedProductResponseDTO);
        }

        // DELETE: api/ConsumedProducts/5
        [HttpDelete("{id}")]
        public ActionResult<ConsumedProductRequestDTO> DeleteConsumedProduct(Guid id)
        {
            var consumedProduct = _context.ConsumedProducts.Find(id);
            if (consumedProduct == null)
            {
                return NotFound();
            }

            _context.ConsumedProducts.Remove(consumedProduct);
            _context.SaveChanges();

            return _mapper.Map<ConsumedProductRequestDTO>(consumedProduct);
        }

        private bool ConsumedProductExists(Guid id)
        {
            return _context.ConsumedProducts.Any(e => e.Id == id);
        }
    }
}
