using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dionys.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dionys.Models.DTO;

namespace Dionys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumedProductsController : ControllerBase
    {
        private readonly DionysContext _context;

        private readonly IMapper _mapper;

        public ConsumedProductsController(DionysContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        // GET: api/ConsumedProducts
        [HttpGet]
        public IEnumerable<ConsumedProductResponseViewModel> GetConsumedProducts()
        {
            var consumedProductDtos = from consumedProduct in _context.ConsumedProducts.Include(x => x.Product)
                                select _mapper.Map<ConsumedProductResponseViewModel>(consumedProduct);

            return consumedProductDtos;
        }

        // GET: api/ConsumedProducts/5
        [HttpGet("{id}")]
        public ActionResult<ConsumedProductResponseViewModel> GetConsumedProduct(Guid id)
        {
            var consumedProduct = _context.ConsumedProducts.Find(id);

            if (consumedProduct == null)
            {
                return NotFound();
            }

            _context.Entry(consumedProduct)
                .Reference(x => x.Product)
                .Load();

            return _mapper.Map<ConsumedProductResponseViewModel>(consumedProduct);
        }

        // PUT: api/ConsumedProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumedProduct(Guid id, ConsumedProductRequestViewModel consumedProductRequestViewModel)
        {
            if (id != consumedProductRequestViewModel.Id)
            {
                return BadRequest();
            }

            var consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductRequestViewModel);

            _context.MarkAsModified(consumedProduct);

            try
            {
                _context.ConsumedProducts.Update(consumedProduct);
                _context.MarkAsModified(consumedProduct.Product);

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
        public async Task<ActionResult<ConsumedProductResponseViewModel>> PostConsumedProduct(ConsumedProductRequestViewModel consumedProductReqestViewModel)
        {
            ConsumedProduct consumedProduct = _mapper.Map<ConsumedProduct>(consumedProductReqestViewModel);

            // HACK: EF trying to insert new entity Product
            Product product = _context.Products.Find(consumedProductReqestViewModel.ProductId);
            consumedProduct.Product = product;

            _context.ConsumedProducts.Add(consumedProduct);
            await _context.SaveChangesAsync();

            var consumedProductResponseDTO = _mapper.Map<ConsumedProductResponseViewModel>(consumedProduct);

            return CreatedAtAction("GetConsumedProduct", new { id = consumedProductResponseDTO.Id }, consumedProductResponseDTO);
        }

        // DELETE: api/ConsumedProducts/5
        [HttpDelete("{id}")]
        public ActionResult<ConsumedProductRequestViewModel> DeleteConsumedProduct(Guid id)
        {
            var consumedProduct = _context.ConsumedProducts.Find(id);
            if (consumedProduct == null)
            {
                return NotFound();
            }

            _context.ConsumedProducts.Remove(consumedProduct);
            _context.SaveChanges();

            return _mapper.Map<ConsumedProductRequestViewModel>(consumedProduct);
        }

        private bool ConsumedProductExists(Guid id)
        {
            return _context.ConsumedProducts.Any(e => e.Id == id);
        }
    }
}
