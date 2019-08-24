using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Models.DTO;
using Dionys.Infrastructure.Services;
using Dionys.Web.Models.DTO;
using Dionys.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumedProductsController : ControllerBase
    {
        private readonly DionysContext _context;

        private readonly IMapper _mapper;

        private readonly IConsumedProductService _consumedProductService;

        public ConsumedProductsController(DionysContext context, IMapper mapper, IConsumedProductService consumedProductService)
        {
            _context = context;
            _mapper  = mapper;
            _consumedProductService = consumedProductService;
        }

        // GET: api/ConsumedProducts
        [HttpGet]
        public PagingViewModel<ConsumedProductResponseViewModel> GetConsumedProducts()
        {
            var consumedProductDtos = _context.ConsumedProducts.Include(x => x.Product)
                .Select(x => _mapper.Map<ConsumedProductResponseViewModel>(x));

            return new PagingViewModel<ConsumedProductResponseViewModel>()
            {
                Elements = consumedProductDtos.Count(),
                Items    = consumedProductDtos
            };
        }

        // GET: api/ConsumedProducts/?page={page}&count={count}
        public PagingViewModel<ConsumedProductResponseViewModel> GetConsumedProducts(int page, int count)
        {
            var consumedProductDtos = _context.ConsumedProducts.OrderBy(x => x.Timestamp)
                .Include(x => x.Product)
                .Select(x => _mapper.Map<ConsumedProductResponseViewModel>(x)).Skip(page * count).Take(count);

            return new PagingViewModel<ConsumedProductResponseViewModel>
            {
                Elements = consumedProductDtos.Count(),
                Items    = consumedProductDtos
            };
        }

        // GET: api/ConsumedProducts/5
        [HttpGet("{id}")]
        public ActionResult<ConsumedProductResponseViewModel> GetConsumedProduct(Guid id)
        {
            ConsumedProductDTO consumedProduct = _consumedProductService.GetById(id);

            if (consumedProduct == null)
            {
                return NotFound();
            }

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
