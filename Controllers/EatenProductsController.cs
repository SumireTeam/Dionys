using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dionys.Models;

namespace Dionys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EatenProductsController : ControllerBase
    {
        private readonly DionysContext _context;

        public EatenProductsController(DionysContext context)
        {
            _context = context;
        }

        // GET: api/EatenProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EatenProduct>>> GetEatenProducts()
        {
            return await _context.EatenProducts.ToListAsync();
        }

        // GET: api/EatenProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EatenProduct>> GetEatenProduct(Guid id)
        {
            var eatenProduct = await _context.EatenProducts.FindAsync(id);

            if (eatenProduct == null)
            {
                return NotFound();
            }

            return eatenProduct;
        }

        // PUT: api/EatenProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEatenProduct(Guid id, EatenProduct eatenProduct)
        {
            if (id != eatenProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(eatenProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EatenProductExists(id))
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

        // POST: api/EatenProducts
        [HttpPost]
        public async Task<ActionResult<EatenProduct>> PostEatenProduct(EatenProduct eatenProduct)
        {
            _context.EatenProducts.Add(eatenProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEatenProduct", new { id = eatenProduct.Id }, eatenProduct);
        }

        // DELETE: api/EatenProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EatenProduct>> DeleteEatenProduct(Guid id)
        {
            var eatenProduct = await _context.EatenProducts.FindAsync(id);
            if (eatenProduct == null)
            {
                return NotFound();
            }

            _context.EatenProducts.Remove(eatenProduct);
            await _context.SaveChangesAsync();

            return eatenProduct;
        }

        private bool EatenProductExists(Guid id)
        {
            return _context.EatenProducts.Any(e => e.Id == id);
        }
    }
}
