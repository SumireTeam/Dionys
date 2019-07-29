using AutoMapper;
using Dionys.Infrastructure.Models;

namespace Dionys.Infrastructure.Services
{
    public class ConsumedProductService : IService
    {
        private IDionysContext _context;
        private IMapper        _mapper ;

        public ConsumedProductService(IDionysContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }
    }
}
